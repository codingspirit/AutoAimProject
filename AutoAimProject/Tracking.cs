using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emgu.CV;
using Emgu.CV.Structure;
using System.Drawing;

namespace AutoAimProject
{
    class Track
    {
        public Image<Gray, Byte> backprojection;
        public DenseHistogram hist;
        public Image<Hsv, Byte> hsv;
        public Image<Gray, Byte> hue;
        public Image<Gray, Byte> mask;
        private RotatedRect trackbox;
        private Rectangle trackingWindow;
        private int bins = 16;
        public Track(Image<Bgr, Byte> img, Rectangle ROI)
        {
            trackbox = new RotatedRect();
            mask = new Image<Gray, byte>(img.Width, img.Height);
            hist = new DenseHistogram(bins, new RangeF(0, 180));
            hue = new Image<Gray, byte>(img.Width, img.Height);
            backprojection = new Image<Gray, byte>(img.Width, img.Height);
            trackingWindow = ROI;
            CalcHist(img);
        }

        public RotatedRect Tracking(Image<Bgr, Byte> image)
        {
            GetFrameHue(image);

            //User changed bins num ,recalculate Hist
            if (Main._advancedHsv)
            {
                if (bins != Main.HsvSetting.Getbins)
                {
                    bins = Main.HsvSetting.Getbins;
                    hist.Dispose();
                    hist = new DenseHistogram(bins, new RangeF(0, 180));
                    CalcHist(image);
                }
            }

            backprojection = hist.BackProject(new Image<Gray, Byte>[] { hue });

            // Add mask
            backprojection._And(mask);

            // If lost trackbox
            if (trackingWindow.IsEmpty || trackingWindow.Width == 0 || trackingWindow.Height == 0)
            {
                trackingWindow = new Rectangle(0, 0, 100, 100);
            }
            trackbox = CvInvoke.CamShift(backprojection, ref trackingWindow, new MCvTermCriteria(10, 1));
            return trackbox;
        }
        private void CalcHist(Image<Bgr, Byte> image)
        {
            GetFrameHue(image);

            // Set tracking object's ROI
            hue.ROI = trackingWindow;
            mask.ROI = trackingWindow;
            //Calculate Hist
            hist.Calculate(new Image<Gray, Byte>[] { hue }, false, mask);

            // Normalize Historgram
            CvInvoke.Normalize(hist, hist, 0, 255, Emgu.CV.CvEnum.NormType.MinMax);

            // Clear ROI
            hue.ROI = System.Drawing.Rectangle.Empty;
            mask.ROI = System.Drawing.Rectangle.Empty;

        }
        private void GetFrameHue(Image<Bgr, Byte> image)
        {
            // release previous image memory
            if (hsv != null) hsv.Dispose();
            hsv = image.Convert<Hsv, Byte>();

            // Get Hue
            hue = hsv.Split()[0];

            // if one pixel hsv value In Range,at this pixel mask==0xff,otherwise mask==0x00
            if (Main._advancedHsv)
            {
                Hsv l = new Hsv(0, Main.HsvSetting.Getsmin, Math.Min(Main.HsvSetting.Getvmin, Main.HsvSetting.Getvmax));
                Hsv h = new Hsv(180, 256, Math.Max(Main.HsvSetting.Getvmin, Main.HsvSetting.Getvmax));
                mask = hsv.InRange(l, h);
            }
            else
            {
                Hsv l = new Hsv(0, 30, 10);
                Hsv h = new Hsv(180, 256, 256);
                mask = hsv.InRange(l, h);
            }
        }
    }
}
