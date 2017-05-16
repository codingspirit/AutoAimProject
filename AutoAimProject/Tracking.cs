using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emgu.CV;
using Emgu.CV.Structure;
using System.Drawing;
using Emgu.CV.Util;
using System.Diagnostics;

namespace AutoAimProject
{
    class Track
    {
        public Image<Gray, Byte> backprojection;
        public DenseHistogram hist;
        public Image<Hsv, byte> hsv;
        public Image<Gray, Byte> hue;
        public Image<Gray, Byte> mask;
        public bool _lost = false;
        public VectorOfVectorOfPoint vvp;
        public VectorOfVectorOfPoint vvpApprox;
        double targetDensity = 0;
        double[] vvpApproxDensity;
        Rectangle[] vvpApproxRect;
        public int targetVVPIndex = 0;
        private RotatedRect trackbox;
        private Rectangle trackingWindow;
        private int bins = 16;
        private Image<Gray, Byte> backcopy;
        private Stopwatch timer = new Stopwatch();


        public Track(Image<Bgr, Byte> img, Rectangle ROI)
        {
            trackbox = new RotatedRect();
            mask = new Image<Gray, byte>(img.Width, img.Height);
            hist = new DenseHistogram(bins, new RangeF(0, 180));
            hue = new Image<Gray, byte>(img.Width, img.Height);
            backprojection = new Image<Gray, byte>(img.Width, img.Height);

            // FindContours needed
            vvp = new VectorOfVectorOfPoint();
            vvpApprox = new VectorOfVectorOfPoint();
            backcopy = new Image<Gray, byte>(img.Width, img.Height);

            trackingWindow = ROI;
            CalcHist(img);
        }

        public RotatedRect Tracking(Image<Bgr, Byte> image)
        {
            GetFrameHue(image);

            // User changed bins num ,recalculate Hist
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

            // FindContours
            //CvInvoke.Canny(backprojection, backcopy, 3, 6);
            backprojection.CopyTo(backcopy);
            CvInvoke.FindContours(backcopy, vvp, null, Emgu.CV.CvEnum.RetrType.External, Emgu.CV.CvEnum.ChainApproxMethod.ChainApproxNone);
            int trackArea = trackingWindow.Height * trackingWindow.Width;
            FindTargetByArea(vvp, trackArea * 0.25, trackArea * 10, ref vvpApprox);
            vvpApproxDensity = GetVVPDensity(vvpApprox,out vvpApproxRect);
            targetVVPIndex = FindTargetByOverlap(vvpApprox, trackingWindow);
            //FindTargetByCenter(vvpApprox, new PointF(trackingWindow.X + trackingWindow.Width / 2, trackingWindow.Y + trackingWindow.Height / 2));


            // If lost trackbox
            if (trackingWindow.IsEmpty || trackingWindow.Width <= 10 || trackingWindow.Height <= 10 || _lost || targetVVPIndex == -1)
            {
                if (!timer.IsRunning) 
                {
                    timer.Start();
                }
                if (timer.ElapsedMilliseconds > 1000)
                {
                    _lost = true;
                }
                if (timer.ElapsedMilliseconds > 3000) 
                {
                    //targetVVPIndex = Array.IndexOf(vvpApproxDensity, vvpApproxDensity.Max());
                }
                for (int i = 0; i < vvpApproxDensity.Length; i++)
                {
                    if (vvpApproxDensity[i] >= targetDensity * 0.8) 
                    {
                        trackingWindow = vvpApproxRect[i];
                        _lost = false;
                        timer.Reset();
                    }
                }
            }
            else
            {
                trackbox = CvInvoke.CamShift(backprojection, ref trackingWindow, new MCvTermCriteria(10, 1));
                targetDensity += vvpApproxDensity[targetVVPIndex];
                targetDensity /= 2;
                if (timer.IsRunning)
                {
                    timer.Reset();
                }
            }
            return trackbox;
        }
        private void CalcHist(Image<Bgr, Byte> image)
        {
            GetFrameHue(image);

            // Set tracking object's ROI
            hue.ROI = trackingWindow;
            mask.ROI = trackingWindow;
            // Calculate Hist
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
        // this method find the  contour in given area region
        private void FindTargetByArea(VectorOfVectorOfPoint invvp, double areamin, double areamax, ref VectorOfVectorOfPoint outvvp)
        {
            int area = 0;
            outvvp.Clear();
            for (int i = 0; i < invvp.Size; i++)
            {
                using (VectorOfPoint contour = invvp[i])
                using (VectorOfPoint approxContour = new VectorOfPoint())
                {
                    CvInvoke.ApproxPolyDP(contour, approxContour, 5, true);
                    area = (int)Math.Abs(CvInvoke.ContourArea(approxContour));
                    //area in given region
                    if (area > areamin && area < areamax)
                    {
                        outvvp.Push(approxContour);
                    }
                }
            }
        }
        // this method find the contour which include given point return Index of invvp
        private int FindTargetByCenter(VectorOfVectorOfPoint invvp, PointF point)
        {
            for (int i = 0; i < invvp.Size; i++)
            {
                // ponit inside contour
                if (CvInvoke.PointPolygonTest(invvp[i], point, false) > 0)
                {
                    return i;
                }
            }
            return -1;
        }
        private int FindTargetByOverlap(VectorOfVectorOfPoint invvp, Rectangle window)
        {
            Rectangle boundingRectangle;
            for (int i = 0; i < invvp.Size; i++)
            {
                boundingRectangle = CvInvoke.BoundingRectangle(invvp[i]);
                if (RectOverlap(boundingRectangle, window) > 0.2)
                {
                    return i;
                }
            }
            return -1;
        }
        private double RectOverlap(Rectangle box1, Rectangle box2)
        {
            if (box1.X > box2.X + box2.Width) { return 0.0; }
            if (box1.Y > box2.Y + box2.Height) { return 0.0; }
            if (box1.X + box1.Width < box2.X) { return 0.0; }
            if (box1.Y + box1.Height < box2.Y) { return 0.0; }
            float colInt = Math.Min(box1.X + box1.Width, box2.X + box2.Width) - Math.Max(box1.X, box2.X);
            float rowInt = Math.Min(box1.Y + box1.Height, box2.Y + box2.Height) - Math.Max(box1.Y, box2.Y);
            float overlapArea = colInt * rowInt;
            float area1 = box1.Width * box1.Height;
            float area2 = box2.Width * box2.Height;
            return overlapArea / (area1 + area2 - overlapArea);
        }
        private double[] GetVVPDensity(VectorOfVectorOfPoint invvp,out Rectangle[] rect)
        {
            double[] density = new double[invvp.Size];
            rect = new Rectangle[invvp.Size];
            for (int i = 0; i < invvp.Size; i++)
            {
                rect[i] = CvInvoke.BoundingRectangle(invvp[i]);
                density[i] = CvInvoke.Moments(invvp[i]).M00 / (rect[i].Width * rect[i].Height);
            }
            return density;
        }
    }
}
