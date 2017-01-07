/**
  ******************************************************************************
  * File Name          : freertos.c
  * Description        : Code for freertos applications
  ******************************************************************************
  *
  * Copyright (c) 2016 STMicroelectronics International N.V.
  * All rights reserved.
  *
  * Redistribution and use in source and binary forms, with or without
  * modification, are permitted, provided that the following conditions are met:
  *
  * 1. Redistribution of source code must retain the above copyright notice,
  *    this list of conditions and the following disclaimer.
  * 2. Redistributions in binary form must reproduce the above copyright notice,
  *    this list of conditions and the following disclaimer in the documentation
  *    and/or other materials provided with the distribution.
  * 3. Neither the name of STMicroelectronics nor the names of other
  *    contributors to this software may be used to endorse or promote products
  *    derived from this software without specific written permission.
  * 4. This software, including modifications and/or derivative works of this
  *    software, must execute solely and exclusively on microcontroller or
  *    microprocessor devices manufactured by or for STMicroelectronics.
  * 5. Redistribution and use of this software other than as permitted under
  *    this license is void and will automatically terminate your rights under
  *    this license.
  *
  * THIS SOFTWARE IS PROVIDED BY STMICROELECTRONICS AND CONTRIBUTORS "AS IS"
  * AND ANY EXPRESS, IMPLIED OR STATUTORY WARRANTIES, INCLUDING, BUT NOT
  * LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY, FITNESS FOR A
  * PARTICULAR PURPOSE AND NON-INFRINGEMENT OF THIRD PARTY INTELLECTUAL PROPERTY
  * RIGHTS ARE DISCLAIMED TO THE FULLEST EXTENT PERMITTED BY LAW. IN NO EVENT
  * SHALL STMICROELECTRONICS OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT,
  * INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT
  * LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA,
  * OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF
  * LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING
  * NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE,
  * EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
  *
  ******************************************************************************
  */

/* Includes ------------------------------------------------------------------*/
#include "FreeRTOS.h"
#include "task.h"
#include "cmsis_os.h"

/* USER CODE BEGIN Includes */
#include "gpio.h"
#include "usbd_cdc_if.h"
#include "tim.h"
#define FIREDELAY 10
#define MANUALDELAY 2
/* USER CODE END Includes */

/* Variables -----------------------------------------------------------------*/
osThreadId getStateTaskHandle;
osThreadId keysTaskHandle;
osThreadId outPutTaskHandle;
osThreadId controlTaskHandle;

/* USER CODE BEGIN Variables */
typedef struct
{
    int xError;
    int yError;
    _Bool fireflag;
    _Bool manualflag;
} MotorState;
MotorState mstate = {0, 0, 0, 0};
static int Xpluse = 0, Ypluse = 0;
static float p = 0;
/* USER CODE END Variables */

/* Function prototypes -------------------------------------------------------*/
void StartGetStateTask(void const * argument);
void StartKeyTask(void const * argument);
void StartOutPutTask(void const * argument);
void StartControlTask(void const * argument);

extern void MX_USB_DEVICE_Init(void);
void MX_FREERTOS_Init(void); /* (MISRA C 2004 rule 8.1) */

/* USER CODE BEGIN FunctionPrototypes */
MotorState GetStates(uint8_t * RecieveData);
void StepMotorControl(_Bool motor, int pluse); //0-->X 1-->Y
void SetUserTxData(int x, int y);
/* USER CODE END FunctionPrototypes */

/* Hook prototypes */

/* Init FreeRTOS */

void MX_FREERTOS_Init(void)
{
    /* USER CODE BEGIN Init */

    /* USER CODE END Init */

    /* USER CODE BEGIN RTOS_MUTEX */
    /* add mutexes, ... */
    /* USER CODE END RTOS_MUTEX */

    /* USER CODE BEGIN RTOS_SEMAPHORES */
    /* add semaphores, ... */
    /* USER CODE END RTOS_SEMAPHORES */

    /* USER CODE BEGIN RTOS_TIMERS */
    /* start timers, add new ones, ... */
    /* USER CODE END RTOS_TIMERS */

    /* Create the thread(s) */
    /* definition and creation of getStateTask */
    osThreadDef(getStateTask, StartGetStateTask, osPriorityHigh, 0, 128);
    getStateTaskHandle = osThreadCreate(osThread(getStateTask), NULL);

    /* definition and creation of keysTask */
    osThreadDef(keysTask, StartKeyTask, osPriorityBelowNormal, 0, 128);
    keysTaskHandle = osThreadCreate(osThread(keysTask), NULL);

    /* definition and creation of outPutTask */
    osThreadDef(outPutTask, StartOutPutTask, osPriorityNormal, 0, 128);
    outPutTaskHandle = osThreadCreate(osThread(outPutTask), NULL);

    /* definition and creation of controlTask */
    osThreadDef(controlTask, StartControlTask, osPriorityHigh, 0, 128);
    controlTaskHandle = osThreadCreate(osThread(controlTask), NULL);

    /* USER CODE BEGIN RTOS_THREADS */
    /* add threads, ... */
    /* USER CODE END RTOS_THREADS */

    /* USER CODE BEGIN RTOS_QUEUES */
    /* add queues, ... */
    /* USER CODE END RTOS_QUEUES */
}

/* StartGetStateTask function */
void StartGetStateTask(void const * argument)
{
    /* init code for USB_DEVICE */
    MX_USB_DEVICE_Init();

    /* USER CODE BEGIN StartGetStateTask */
    HAL_TIM_Base_Start_IT(&htim3);

    /* Infinite loop */
    for(;;)
    {
        ulTaskNotifyTake(pdTRUE, osWaitForever);
        mstate = GetStates(UserRxData);
        xTaskNotifyGive(controlTaskHandle);
    }

    /* USER CODE END StartGetStateTask */
}

/* StartKeyTask function */
void StartKeyTask(void const * argument)
{
    /* USER CODE BEGIN StartKeyTask */
    uint32_t Value = 0;

    /* Infinite loop */
    for(;;)
    {
        xTaskNotifyWait(0, 0xFFFFFFFF, &Value, osWaitForever);

        if(Value == 0x01)
        {
            HAL_GPIO_WritePin(LED1_GPIO_Port, LED1_Pin, GPIO_PIN_SET);
            StepMotorControl(0, 10);
        }

        if(Value == 0x02)
        {
            HAL_GPIO_WritePin(LED1_GPIO_Port, LED1_Pin, GPIO_PIN_RESET);
            StepMotorControl(0, -10);
        }

        if(Value == 0x03)
        {
            HAL_GPIO_WritePin(LED2_GPIO_Port, LED2_Pin, GPIO_PIN_SET);
            StepMotorControl(1, 10);
        }

        if(Value == 0x04)
        {
            HAL_GPIO_WritePin(LED2_GPIO_Port, LED2_Pin, GPIO_PIN_RESET);
            StepMotorControl(1, -10);
        }
    }

    /* USER CODE END StartKeyTask */
}

/* StartOutPutTask function */
void StartOutPutTask(void const * argument)
{
    /* USER CODE BEGIN StartOutPutTask */
    /* Infinite loop */
    for(;;)
    {
        ulTaskNotifyTake(pdTRUE, osWaitForever);
        StepMotorControl(0, Xpluse);
        StepMotorControl(1, Ypluse);
    }

    /* USER CODE END StartOutPutTask */
}

/* StartControlTask function */
void StartControlTask(void const * argument)
{
    /* USER CODE BEGIN StartControlTask */
    /* Infinite loop */
    for(;;)
    {
        ulTaskNotifyTake(pdTRUE, osWaitForever);

        if(!mstate.manualflag)
        {
            Xpluse = (int)(p * mstate.xError);
            Ypluse = (int)(p * mstate.yError);
        }

        else
        {
            Xpluse = (int)(0.1 * mstate.xError);
            Ypluse = (int)(0.1 * mstate.yError);
        }

        //SetUserTxData(5000+Xpluse,5000+Ypluse);
        //CDC_Transmit_FS(UserTxData,9);
        xTaskNotifyGive(outPutTaskHandle);
    }

    /* USER CODE END StartControlTask */
}

/* USER CODE BEGIN Application */
MotorState GetStates(uint8_t * RecieveData)
{
    MotorState mstemp = {0, 0, 0, 0};

    if(RecieveData[0] == '@' || RecieveData[0] == '!') //error
    {
        mstemp.xError = (RecieveData[2] - 48) * 100 + (RecieveData[3] - 48) * 10 + (RecieveData[4] - 48);

        if(RecieveData[1] == '-')
        {
            mstemp.xError = mstemp.xError * (-1);
        }

        mstemp.yError = (RecieveData[6] - 48) * 100 + (RecieveData[7] - 48) * 10 + (RecieveData[8] - 48);

        if(RecieveData[5] == '-')
        {
            mstemp.yError = mstemp.yError * (-1);
        }

        if(RecieveData[0] == '!')//fire
        {
            mstemp.fireflag = 1;
        }
    }

    else if(RecieveData[0] == 'P')//PID parameter
    {
        p = (RecieveData[1] - 48)   + (RecieveData[2] - 48) * 0.1;
        //i = (RecieveData[4] - 48) * 0.01   + (RecieveData[5] - 48) * 0.001;
        //d = (RecieveData[7] - 48)   + (RecieveData[8] - 48) * 0.1;
    }

    else if(RecieveData[0] == '[') //manual
    {
        mstemp.fireflag = 1;

        switch(RecieveData[4])
        {
        case 'L':
            mstemp.xError = -20;
            break;

        case 'R':
            mstemp.xError = 20;
            break;

        case 'U':
            mstemp.yError = -20;
            break;

        case 'D':
            mstemp.yError = 20;
            break;

        case 'F':
            mstemp.fireflag = 1;
            break;
        }

        memset(RecieveData, 0, 9);
    }

    //memset(RecieveData, 0, 9);

    return mstemp;
}
void StepMotorControl(_Bool motor, int pluse)
{
    if(!motor)//X
    {
        if(pluse > 0)
            HAL_GPIO_WritePin(DIRX_GPIO_Port, DIRX_Pin, GPIO_PIN_RESET);

        else
            HAL_GPIO_WritePin(DIRX_GPIO_Port, DIRX_Pin, GPIO_PIN_SET);

        User_TIM1_SetPluse(abs(pluse));
    }

    else
    {
        if(pluse > 0)
            HAL_GPIO_WritePin(DIRY_GPIO_Port, DIRY_Pin, GPIO_PIN_RESET);

        else
            HAL_GPIO_WritePin(DIRY_GPIO_Port, DIRY_Pin, GPIO_PIN_SET);

        User_TIM8_SetPluse(abs(pluse));
    }
}
void SetUserTxData(int x, int y)
{
    UserTxData[0] = '#';
    UserTxData[1] = x / 1000 + 48;
    UserTxData[2] = (x % 1000) / 100 + 48;
    UserTxData[3] = (x % 100) / 10 + 48;
    UserTxData[4] = (x % 10) + 48;
    UserTxData[5] = y / 1000 + 48;
    UserTxData[6] = (y % 1000) / 100 + 48;
    UserTxData[7] = (y % 100) / 10 + 48;
    UserTxData[8] = (y % 10) + 48;
}
/* USER CODE END Application */

/************************ (C) COPYRIGHT STMicroelectronics *****END OF FILE****/
