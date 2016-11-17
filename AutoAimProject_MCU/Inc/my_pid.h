#define IntegralMAX 200
#define IntegralMIN -200
typedef struct
{
    float error[3];
    float Kp, Ki, Kd;
    float Proportional, Integral, Derivative;
} pid_instance;
float PID_Control(pid_instance *s, float error);
void PID_SetParam(pid_instance *s, float Kp, float Ki, float Kd, _Bool resetflag);
