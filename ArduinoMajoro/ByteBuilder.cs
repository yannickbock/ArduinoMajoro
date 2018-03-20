using System;

namespace ArduinoMajoro
{
    public static class ByteBuilder
    {
        public static byte[] Hello()
        {
            return new byte[] { Decrypt(Method.Hello, 0) };
        }

        public static byte[] Ping(int hash)
        {
            if (hash < 0 || hash > 63)
            {
                throw new ArgumentOutOfRangeException("hash", hash, "Value must be between 0 and 63.");
            }

            return new byte[] { Decrypt(Method.Ping, hash) };
        }

        public static byte[] WriteLow(int pin)
        {
            if (pin < 0 || pin > 63)
            {
                throw new ArgumentOutOfRangeException("pin", pin, "Value must be between 0 and 63.");
            }

            return new byte[] { Decrypt(Method.WriteLow, pin) };
        }

        public static byte[] WriteHigh(int pin)
        {
            if (pin < 0 || pin > 63)
            {
                throw new ArgumentOutOfRangeException("pin", pin, "Value must be between 0 and 63.");
            }

            return new byte[] { Decrypt(Method.WriteHigh, pin) };
        }

        public static byte Decrypt(Method method, int pin)
        {
            return (byte)((pin << 2) + method);
        }

        public static Method Encrypt(byte value, out int pin)
        {
            var method = (Method)(value & 3);
            pin = (value - (int)method) >> 2;
            return method;
        }
    }
}
