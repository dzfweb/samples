// Copyright (c) Microsoft. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using Windows.ApplicationModel.Background;
using Windows.Devices.Gpio;
using Windows.System.Threading;
using System.Threading.Tasks;

namespace BlinkyHeadlessCS
{
    public sealed class StartupTask : IBackgroundTask
    {
        BackgroundTaskDeferral deferral;
        private GpioPinValue valueOne = GpioPinValue.High;
        private GpioPinValue valueTwo = GpioPinValue.Low;
        private const int LED_PIN_ONE = 5;
        private const int LED_PIN_TWO = 6;
        private const int LDR_SENSOR_PIN = 4;
        private GpioPin pinOne;
        private GpioPin pinTwo;
        private GpioPin ldrSensorPin;
        private ThreadPoolTimer timer;

        public void Run(IBackgroundTaskInstance taskInstance)
        {
            deferral = taskInstance.GetDeferral();
            InitGPIO();
            timer = ThreadPoolTimer.CreatePeriodicTimer(Timer_Tick, TimeSpan.FromMilliseconds(500));

        }
        private void InitGPIO()
        {
            pinOne = GpioController.GetDefault().OpenPin(LED_PIN_ONE);
            pinTwo = GpioController.GetDefault().OpenPin(LED_PIN_TWO);
            ldrSensorPin = GpioController.GetDefault().OpenPin(LDR_SENSOR_PIN);

            pinOne.Write(valueOne);
            pinTwo.Write(valueTwo);


            pinOne.SetDriveMode(GpioPinDriveMode.Output);
            pinTwo.SetDriveMode(GpioPinDriveMode.Output);

        }

        private void Timer_Tick(ThreadPoolTimer timer)
        {
            
            valueOne = (valueOne == GpioPinValue.High) ? GpioPinValue.Low : GpioPinValue.High;
            pinOne.Write(valueOne);


            valueTwo = (valueTwo == GpioPinValue.High) ? GpioPinValue.Low : GpioPinValue.High;
            pinTwo.Write(valueTwo);

            
        }
    }
}
