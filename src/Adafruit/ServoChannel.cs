using Iot.Device.Pwm;
using System.Device.I2c;
using System.Device.Model;
using System.Device.Pwm;

namespace Adafruit;

[Interface("Servo")]
public sealed class ServoChannel : IDisposable
{
    private PwmChannel Channel { get; }

    private double MaximumAngle { get; }

    private double MinimumPulseWidthMicroseconds { get; }

    private double MaximumPulseWidthMicroseconds { get; }

    private double AngleToMicroseconds { get; }

    //private int ActuationRange { get; }

    //private int DutyRange { get; set; }

    //private int MaxDuty { get; set; }

    //private int MaxPulse { get; }

    //private int MinDuty { get; set; }

    //private int MinPulse { get; }

    public ServoChannel(
        int channel,
        Pca9685 pca9685,
        double maximumAngle = 180,
        double minimumPulseWidthMicroseconds = 1_000,
        double maximumPulseWidthMicroseconds = 2_000)
    {
        MaximumAngle = maximumAngle;
        MinimumPulseWidthMicroseconds = minimumPulseWidthMicroseconds;
        MaximumPulseWidthMicroseconds = maximumPulseWidthMicroseconds;
        Channel = pca9685.CreatePwmChannel(channel);

        Calibrate();

        AngleToMicroseconds = (maximumPulseWidthMicroseconds - minimumPulseWidthMicroseconds) / maximumAngle;
    }

    public void Dispose() => Channel?.Dispose();

    /// <summary>
    /// Sets calibration parameters
    /// </summary>
    /// <param name="maximumAngle">The maximum angle the servo motor can move represented as a value between 0 and 360.</param>
    /// <param name="minimumPulseWidthMicroseconds">The minimum pulse width, in microseconds, that represent an angle for 0 degrees.</param>
    /// <param name="maximumPulseWidthMicroseconds">The maxnimum pulse width, in microseconds, that represent an angle for maximum angle.</param>
    private void Calibrate()
    {
        if (MaximumAngle < 0 || MaximumAngle > 360)
        {
            throw new ArgumentOutOfRangeException(nameof(MaximumAngle), MaximumAngle, "Value must be between 0 and 360.");
        }

        if (MinimumPulseWidthMicroseconds < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(MinimumPulseWidthMicroseconds), MinimumPulseWidthMicroseconds, "Value must not be negative.");
        }

        if (MaximumPulseWidthMicroseconds < MaximumPulseWidthMicroseconds)
        {
            throw new ArgumentOutOfRangeException(nameof(MaximumPulseWidthMicroseconds), MaximumPulseWidthMicroseconds, $"Value must be greater than or equal to {MaximumPulseWidthMicroseconds}.");
        }        
    }

    /// <summary>
    /// Starts the servo motor.
    /// </summary>
    public void Start() => Channel.Start();

    /// <summary>
    /// Stops the servo motor.
    /// </summary>
    public void Stop() => Channel.Stop();


    /// <summary>
    /// Writes an angle to the servo motor.
    /// </summary>
    /// <param name="angle">The angle to write to the servo motor.</param>
    public void WriteAngle(double angle)
    {
        if (angle < 0 || angle > MaximumAngle)
        {
            throw new ArgumentOutOfRangeException(nameof(angle), angle, $"Value must be between 0 and {MaximumAngle}.");
        }

        Console.WriteLine($"Moving to angle of {angle}");

        WritePulseWidth((int)(AngleToMicroseconds * angle + MinimumPulseWidthMicroseconds));

        Console.WriteLine($"Moved to angle of {angle}");
    }

    /// <summary>
    /// Writes a pulse width to the servo motor.
    /// </summary>
    /// <param name="microseconds">The pulse width, in microseconds, to write to the servo motor.</param>
    public void WritePulseWidth(int microseconds)
    {
        double dutyCycle = (double)microseconds / 1_000_000 * Channel.Frequency; // Convert to seconds 1st.
        Channel.DutyCycle = dutyCycle;
    }
}
