using Adafruit;
using Iot.Device.Pwm;
using System.Device.I2c;

public class Program
{
    private static Pca9685 device;
    private static ServoChannel[] channels = new ServoChannel[16];

    public static async Task Main(string[] args)
    {
        try
        {
            var busId = 1;  // /dev/i2c-1
            var deviceAddress = 0x40;
            var settings = new I2cConnectionSettings(busId, deviceAddress);
            var d = I2cDevice.Create(settings);
            
            device = new Pca9685(d);
            for (int i = 0; i < 16; i++)
            {
                channels[i] = new ServoChannel(i, device, 180, 1_000, 2_000);
            }

            channels[0].Start();

            channels[0].WriteAngle(60);

            await Task.Delay(1000);

            channels[0].WriteAngle(90);

            await Task.Delay(1000);

            channels[0].WriteAngle(30);

            await Task.Delay(1000);

            channels[0].Stop();
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
        }
    }


    public static async Task RunMotorForMsAsync(int channel, int angle)
    {
        await Console.Out.WriteLineAsync($"Channel: {channel} Duty Cycle: {device.GetDutyCycle(channel)}");
        await Console.Out.WriteLineAsync($"Channel: {channel} Frequency: {device.PwmFrequency}");



        //channels[channel].Frequency = angle * 4096;

        //Console.WriteLine($"Dutycycle after pwmChannel dutycycle set to {speed}: {device.GetDutyCycle(channel)}");
        

        //channels[channel].DutyCycle = angle / 180d;

        await Console.Out.WriteLineAsync($"New Angle: {angle / 180d}");
        channels[channel].Start();


        await Task.Delay(2000);

        channels[channel].Stop();
        await Console.Out.WriteLineAsync("DONE");
        await Console.Out.WriteLineAsync($"Channel: {channel} Duty Cycle: {device.GetDutyCycle(channel)}");
        await Console.Out.WriteLineAsync($"Channel: {channel} Frequency: {device.PwmFrequency}");
    }
}
