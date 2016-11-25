#include "my_pid.h"
void PID_SetParam(pid_instance *s, float Kp, float Ki, float Kd, int IntegralMAX, int IntegralMIN, _Bool resetflag)
{
    s->Kp = Kp;
    s->Ki = Ki;
    s->Kd = Kd;
    s->IntegralMAX = IntegralMAX;
    s->IntegralMIN = IntegralMIN;

    if(resetflag)
    {
        s->Proportional = 0;
        s->Integral = 0;
        s->Derivative = 0;
    }
}
float PID_Control(pid_instance *s, float error)
{
    s->Proportional = s->Kp * error;
    s->Integral += s->Ki * error;

    s->Integral = s->Integral > s->IntegralMAX ? s->IntegralMAX : s->Integral;
    s->Integral = s->Integral < s->IntegralMIN ? s->IntegralMIN : s->Integral;

    return (s->Proportional + s->Integral + s->Derivative);
}
