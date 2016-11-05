#include "my_pid.h"
void PID_SetParam(pid_instance *s, float Kp, float Ki, float Kd, _Bool resetflag)
{
    s->Kp = Kp;
    s->Ki = Ki;
    s->Kd = Kd;

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

    if(s->Integral > IntegralMAX)
        s->Integral = IntegralMAX;

    if(s->Integral < IntegralMIN)
        s->Integral = IntegralMIN;

    return (s->Proportional + s->Integral + s->Derivative);
}
