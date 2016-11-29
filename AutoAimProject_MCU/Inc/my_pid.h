#define XIntegralMAX 190
#define XIntegralMIN -200
#define YIntegralMAX 180
#define YIntegralMIN -180
typedef struct
{
    float error[3];
    float Kp, Ki, Kd;
    float Proportional, Integral, Derivative;
    int IntegralMAX;
    int IntegralMIN;
} pid_instance;
float PID_Control(pid_instance *s, float error);
void PID_SetParam(pid_instance *s, float Kp, float Ki, float Kd, int IntegralMAX, int IntegralMIN, _Bool resetflag);
