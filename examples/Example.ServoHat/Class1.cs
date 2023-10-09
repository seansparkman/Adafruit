//using System;
//using System.Collections.Generic;
//using System.Device.I2c;
//using System.Text;
//using System.Threading;
//using Iot.Device.Mcp25xxx.Register;
//using RPi.I2C.Net;

//public class I2C_PWM
//{
//    const int __MODE1 = 0x00;
//    const int __MODE2 = 0x01;
//    const int __SUBADR1 = 0x02;
//    const int __SUBADR2 = 0x03;
//    const int __SUBADR3 = 0x04;
//    const int __PRESCALE = 0xFE;
//    const int __LED0_ON_L = 0x06;
//    const int __LED0_ON_H = 0x07;
//    const int __LED0_OFF_L = 0x08;
//    const int __LED0_OFF_H = 0x09;
//    const int __ALL_LED_ON_L = 0xFA;
//    const int __ALL_LED_ON_H = 0xFB;
//    const int __ALL_LED_OFF_L = 0xFC;
//    const int __ALL_LED_OFF_H = 0xFD;

//    //  # Bits
//    const int __RESTART = 0x80;
//    const int __SLEEP = 0x10;
//    const int __ALLCALL = 0x01;
//    const int __INVRT = 0x10;
//    const int __OUTDRV = 0x04;

//    int address;
//    I2CBus bus;

//    public I2C_PWM(int a_address)
//    {
//        address = a_address;
//        bus = I2CBus.Open();
//        if (bus == null)
//        {
//            log("Cannot connect to I2C, Address = " + a_address.ToString());
//            return;
//        }
//        setAllPWM(0, 0);
//        write8(__MODE2, __OUTDRV);
//        write8(__MODE1, __ALLCALL);
//        Thread.Sleep(5); //                                      # wait for oscillator

//        byte mode1 = readU8(self.__MODE1);
//        mode1 = mode1 & ~self.__SLEEP; //                 # wake up (reset sleep)
//        write8(self.__MODE1, mode1);
//        Thread.Sleep(5); //                                      # wait for oscillator
//    }
//    public SetPWMFreq(int freq)
//    {
//        //    "Sets the PWM frequency"
//        double prescaleval = 25000000.0;//    # 25MHz
//        prescaleval /= 4096.0;//       # 12-bit
//        prescaleval /= double(freq);
//        prescaleval -= 1.0;
//        prescale = Math.floor(prescaleval + 0.5);

//        byte oldmode = readU8(self.__MODE1);
//        byte newmode = (oldmode & 0x7F) | 0x10; //             # sleep
//        write8(__MODE1, newmode); //        # go to sleep
//        write8(__PRESCALE, int(math.floor(prescale)));
//        write8(__MODE1, oldmode);
//        Thread.Sleep(5); //                                      # wait for oscillator
//        write8(__MODE1, oldmode | 0x80);
//    }
//    public SetPWM(int pwm_channel, int value)
//    {
//        int on = 0; // change it if you need to start PWM cycle with phase delay
//        int off = a_value;
//        write8(__LED0_ON_L + 4 * channel, on & 0xFF);
//        write8(__LED0_ON_H + 4 * channel, on >> 8);
//        write8(__LED0_OFF_L + 4 * channel, off & 0xFF);
//        write8(__LED0_OFF_H + 4 * channel, off >> 8);
//    }
//    public SetAllPWM(int a_value)
//    {
//        int on = 0; // change it if you need to start PWM cycle with phase delay
//        int off = a_value;
//        // "Sets all PWM channels"
//        write8(__ALL_LED_ON_L, on & 0xFF);
//        write8(__ALL_LED_ON_H, on >> 8);
//        write8(__ALL_LED_OFF_L, off & 0xFF);
//        write8(__ALL_LED_OFF_H, off >> 8);
//    }

//    private void write8(int address, byte b);
//	{
//		bus.WriteByte(address, b);
//	}

//private byte readU8(int address);
//{
//    byte[] buff = bus.ReadBytes(address, 1);
//    if (buff != null && buff.Length > 0)
//    {
//        return buff[0];
//    }
//    else
//    {
//        log("ERROR Reading byte from address " + address.ToString());
//        return 0;
//    }
//}

//private void log(string msg)
//{
//    Console.Write(msg);
//}
//}
