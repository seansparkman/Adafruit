using System.Device.I2c;
using System.Device.Model;
using System.Device.Pwm;

namespace Adafruit
{
    [Interface("ServoHAT")]
    public sealed class ServoHat : IDisposable
    {
        public const int DefaultI2cBus = 1;

        private I2cBus Bus { get; }

        private bool ShouldDispose { get; set; }

        public ServoHat(
            I2cBus? bus = null,
            bool shouldDispose = false,
            int channels = 16)
        {
            ShouldDispose = shouldDispose || bus == null;
            Bus = bus ?? I2cBus.Create(DefaultI2cBus);
        }


        public void Dispose()
        {
        }
    }
}
