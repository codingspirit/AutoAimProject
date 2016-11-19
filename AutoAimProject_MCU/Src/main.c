/**
  ******************************************************************************
  * File Name          : main.c
  * Description        : Main program body
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
#include "main.h"
#include "stm32f4xx_hal.h"
#include "tim.h"
#include "usb_device.h"
#include "gpio.h"

/* USER CODE BEGIN Includes */
#include "usbd_cdc.h"
#include "usbd_cdc_if.h"
#include "my_pid.h"
#define XDEAD 170
#define YDEAD 170
#define XMAX 600
#define XMIN -600
#define YMAX 1000
#define YMIN -1000
/* USER CODE END Includes */

/* Private variables ---------------------------------------------------------*/

/* USER CODE BEGIN PV */
/* Private variables ---------------------------------------------------------*/
typedef struct
{
    int xError;
    int yError;
    _Bool fireflag;
    _Bool manualflag;
} MotorState;
MotorState mstate = {0, 0, 0};
uint8_t UserTxBuffer[] = "#00000000";
static int XPWM = 0, YPWM = 0;//CH2 CH3
static pid_instance XPID;
static pid_instance YPID;
static int XPIDout = 0;
static int YPIDout = 0;
static float p = 0, i = 0, d = 0;
int period = 0;

/* USER CODE END PV */

/* Private function prototypes -----------------------------------------------*/
void SystemClock_Config(void);
void Error_Handler(void);

/* USER CODE BEGIN PFP */
/* Private function prototypes -----------------------------------------------*/
void SystemClock_Config(void);
void Error_Handler(void);
void ManualControl(char dir);
/* USER CODE END PFP */

/* USER CODE BEGIN 0 */
/* protocal */
//length=9
//error: "@+000-000" x/y
//fire: "!+000-000"
//PID: "P00I00D00"
//manual: "[LLLLLLL]"  L/R/U/D
/* protocal */
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
        i = (RecieveData[4] - 48) * 0.01   + (RecieveData[5] - 48) * 0.001;
        d = (RecieveData[7] - 48)   + (RecieveData[8] - 48) * 0.1;
    }

    else if(RecieveData[0] == '[') //manual
    {
        mstemp.manualflag = 1;
        ManualControl(RecieveData[4]);
        memset(RecieveData, 0, 9);
    }

    //memset(RecieveData, 0, 9);

    return mstemp;
}

void ManualControl(char dir)
{
    PID_SetParam(&XPID, p, i, d, 1);
    PID_SetParam(&XPID, p, i, d, 1);

    switch(dir)
    {
    case 'L':
        XPIDout = 500;
        break;

    case 'R':
        XPIDout = -600;
        break;

    case 'U':
        YPIDout = 500;
        break;

    case 'D':
        YPIDout = -500;
        break;
    }
}

void MotorTest(uint8_t * RecieveData)
{
    int lpwm, rpwm;

    if(RecieveData[0] == '#')
    {
        lpwm = RecieveData[1] * 1000 + RecieveData[2] * 100 + RecieveData[3] * 10 + RecieveData[4];
        rpwm = RecieveData[5] * 1000 + RecieveData[6] * 100 + RecieveData[7] * 10 + RecieveData[8];
    }

    USER_TIM1_SetPWM(TIM_CHANNEL_2, lpwm);
    USER_TIM1_SetPWM(TIM_CHANNEL_3, rpwm);
}
void SetUserTxBuffer(int l, int r)
{
    UserTxBuffer[0] = '#';
    UserTxBuffer[1] = l / 1000 + 48;
    UserTxBuffer[2] = (l % 1000) / 100 + 48;
    UserTxBuffer[3] = (l % 100) / 10 + 48;
    UserTxBuffer[4] = (l % 10) + 48;
    UserTxBuffer[5] = r / 1000 + 48;
    UserTxBuffer[6] = (r % 1000) / 100 + 48;
    UserTxBuffer[7] = (r % 100) / 10 + 48;
    UserTxBuffer[8] = (r % 10) + 48;
}
/* USER CODE END 0 */

int main(void)
{

    /* USER CODE BEGIN 1 */

    /* USER CODE END 1 */

    /* MCU Configuration----------------------------------------------------------*/

    /* Reset of all peripherals, Initializes the Flash interface and the Systick. */
    HAL_Init();

    /* Configure the system clock */
    SystemClock_Config();

    /* Initialize all configured peripherals */
    MX_GPIO_Init();
    MX_USB_DEVICE_Init();
    MX_TIM1_Init();
    MX_TIM2_Init();

    /* USER CODE BEGIN 2 */
    PID_SetParam(&XPID, p, i, d, 1);
    PID_SetParam(&YPID, p, i, d, 1);
    HAL_TIM_Base_Start_IT(&htim2);
    /* USER CODE END 2 */

    /* Infinite loop */
    /* USER CODE BEGIN WHILE */
    while(1)
    {
        /* USER CODE END WHILE */

        /* USER CODE BEGIN 3 */

    }

    /* USER CODE END 3 */

}

/** System Clock Configuration
*/
void SystemClock_Config(void)
{

    RCC_OscInitTypeDef RCC_OscInitStruct;
    RCC_ClkInitTypeDef RCC_ClkInitStruct;

    /**Configure the main internal regulator output voltage
    */
    __HAL_RCC_PWR_CLK_ENABLE();

    __HAL_PWR_VOLTAGESCALING_CONFIG(PWR_REGULATOR_VOLTAGE_SCALE1);

    /**Initializes the CPU, AHB and APB busses clocks
    */
    RCC_OscInitStruct.OscillatorType = RCC_OSCILLATORTYPE_HSE;
    RCC_OscInitStruct.HSEState = RCC_HSE_ON;
    RCC_OscInitStruct.PLL.PLLState = RCC_PLL_ON;
    RCC_OscInitStruct.PLL.PLLSource = RCC_PLLSOURCE_HSE;
    RCC_OscInitStruct.PLL.PLLM = 4;
    RCC_OscInitStruct.PLL.PLLN = 168;
    RCC_OscInitStruct.PLL.PLLP = RCC_PLLP_DIV2;
    RCC_OscInitStruct.PLL.PLLQ = 7;

    if(HAL_RCC_OscConfig(&RCC_OscInitStruct) != HAL_OK)
    {
        Error_Handler();
    }

    /**Initializes the CPU, AHB and APB busses clocks
    */
    RCC_ClkInitStruct.ClockType = RCC_CLOCKTYPE_HCLK | RCC_CLOCKTYPE_SYSCLK
                                  | RCC_CLOCKTYPE_PCLK1 | RCC_CLOCKTYPE_PCLK2;
    RCC_ClkInitStruct.SYSCLKSource = RCC_SYSCLKSOURCE_PLLCLK;
    RCC_ClkInitStruct.AHBCLKDivider = RCC_SYSCLK_DIV1;
    RCC_ClkInitStruct.APB1CLKDivider = RCC_HCLK_DIV4;
    RCC_ClkInitStruct.APB2CLKDivider = RCC_HCLK_DIV2;

    if(HAL_RCC_ClockConfig(&RCC_ClkInitStruct, FLASH_LATENCY_5) != HAL_OK)
    {
        Error_Handler();
    }

    /**Configure the Systick interrupt time
    */
    HAL_SYSTICK_Config(HAL_RCC_GetHCLKFreq() / 1000);

    /**Configure the Systick
    */
    HAL_SYSTICK_CLKSourceConfig(SYSTICK_CLKSOURCE_HCLK);

    /* SysTick_IRQn interrupt configuration */
    HAL_NVIC_SetPriority(SysTick_IRQn, 0, 0);
}

/* USER CODE BEGIN 4 */

void HAL_GPIO_EXTI_Callback(uint16_t GPIO_Pin)
{
    if(!HAL_GPIO_ReadPin(KEY1_GPIO_Port, GPIO_Pin))
    {
        if(GPIO_Pin == KEY1_Pin)
        {
            CDC_Transmit_FS(UserTxBuffer, sizeof(UserTxBuffer));
            HAL_GPIO_TogglePin(GPIOE, GPIO_PIN_3);
            USER_TIM1_SetPWM(TIM_CHANNEL_2, 5300);
        }

        if(GPIO_Pin == KEY2_Pin)
        {
            USER_TIM1_SetPWM(TIM_CHANNEL_2, 4700);
            HAL_GPIO_TogglePin(LED1_GPIO_Port, LED1_Pin);
        }

        if(GPIO_Pin == KEY3_Pin)
        {
            USER_TIM1_SetPWM(TIM_CHANNEL_3, 4500);
        }

        if(GPIO_Pin == KEY4_Pin)
        {
            USER_TIM1_SetPWM(TIM_CHANNEL_3, 5500);
        }
    }

    else if(HAL_GPIO_ReadPin(KEY1_GPIO_Port, GPIO_Pin))
    {
        USER_TIM1_SetPWM(TIM_CHANNEL_2, 5000);
        USER_TIM1_SetPWM(TIM_CHANNEL_3, 5000);
    }
}

void HAL_TIM_PeriodElapsedCallback(TIM_HandleTypeDef *htim)
{
    if(htim->Instance == TIM2)
    {
        if(period == 1)
        {
            XPIDout = 0;
            YPIDout = 0;
            mstate = GetStates(UserRxData);
            PID_SetParam(&XPID, p, i, d, 0);

            //PID_SetParam(&YPID, p, i, d, 0);
            if(!mstate.manualflag)
            {
                XPIDout = PID_Control(&XPID, mstate.xError);
                YPIDout = PID_Control(&YPID, mstate.yError);
            }

        }

        if(period == 2)
        {
            XPIDout += XPIDout > 0 ? XDEAD : 0;

            XPIDout -= XPIDout < 0 ? XDEAD : 0;

            YPIDout += YPIDout > 0 ? YDEAD : 0;

            YPIDout -= YPIDout < 0 ? YDEAD : 0;


            XPIDout = XPIDout > XMAX ? XMAX : XPIDout;

            XPIDout = XPIDout < XMIN ? XMIN : XPIDout;

            YPIDout = YPIDout > YMAX ? YMAX : YPIDout;

            YPIDout = YPIDout < YMIN ? YMIN : YPIDout;

            XPWM = 5000 + XPIDout;
            YPWM = 5000 + YPIDout;

            USER_TIM1_SetPWM(TIM_CHANNEL_2, XPWM);
            USER_TIM1_SetPWM(TIM_CHANNEL_3, YPWM);
        }

        if(period >= 3)
        {
            SetUserTxBuffer((int)XPWM, (int)YPWM);
            CDC_Transmit_FS(UserTxBuffer, sizeof(UserTxBuffer));
            period = 0;
        }

        period++;
    }
}

/* USER CODE END 4 */

/**
  * @brief  This function is executed in case of error occurrence.
  * @param  None
  * @retval None
  */
void Error_Handler(void)
{
    /* USER CODE BEGIN Error_Handler */
    /* User can add his own implementation to report the HAL error return state */
    while(1)
    {
    }

    /* USER CODE END Error_Handler */
}

#ifdef USE_FULL_ASSERT

/**
   * @brief Reports the name of the source file and the source line number
   * where the assert_param error has occurred.
   * @param file: pointer to the source file name
   * @param line: assert_param error line source number
   * @retval None
   */
void assert_failed(uint8_t* file, uint32_t line)
{
    /* USER CODE BEGIN 6 */
    /* User can add his own implementation to report the file name and line number,
      ex: printf("Wrong parameters value: file %s on line %d\r\n", file, line) */
    /* USER CODE END 6 */

}

#endif

/**
  * @}
  */

/**
  * @}
*/

/************************ (C) COPYRIGHT STMicroelectronics *****END OF FILE****/
