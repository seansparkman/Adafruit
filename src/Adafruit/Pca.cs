using System;
using System.Collections.Generic;
using System.Device.I2c;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adafruit
{
    public sealed class Pca : IDisposable
    {
        private I2cBus Bus { get; }

        private I2cDevice Device { get; }

        private int ReferenceClockSpeed { get; }

        public Pca(
            int address,
            int referenceClockSpeed,
            I2cBus bus)
        {
            Bus = bus;
            Device = bus.CreateDevice(address);
            ReferenceClockSpeed = referenceClockSpeed;
            

        }

        public void Dispose()
        {
        }
    }
}
