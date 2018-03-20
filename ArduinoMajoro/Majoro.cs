using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Threading;

namespace ArduinoMajoro
{
    public class Majoro
    {
        private SerialPort _serialPort;
        public Arduino Device { get; private set; }

        public Majoro(Arduino device)
        {
            Device = device;
            _serialPort = buildConnection(device.SerialPort);
        }

        public static Arduino Hello(string name)
        {
            return Hello().FirstOrDefault(x => x.Name == name); ;
        }

        public static IEnumerable<Arduino> Hello()
        {
            return Enumerable.Range(0, 3).SelectMany(x => hello()).Distinct().ToList();
        }

        private static IEnumerable<Arduino> hello()
        {
            var bytes = ByteBuilder.Hello();

            var list = new List<Arduino>();
            foreach (var serialPort in SerialPort.GetPortNames())
            {
                var connection = buildConnection(serialPort);
                string result = "";
                try
                {
                    connection.Open();
                    Thread.Sleep(15);
                    connection.Write(bytes, 0, bytes.Length);
                    Thread.Sleep(15);
                    result = connection.ReadExisting();
                    connection.Close();
                }
                catch (Exception)
                {
                }

                if (result.Contains("ARDUINO"))
                {
                    list.Add(new Arduino(result.Split('|').Last(), serialPort));
                }
            }

            return list;
        }

        public void Connect()
        {
            _serialPort.Open();
            Thread.Sleep(15);
        }

        public void Disconnect()
        {
            _serialPort.Close();
        }

        public bool Ping()
        {
            int hash = new Random().Next(0, 63);
            var bytes = ByteBuilder.Ping(hash);

            _serialPort.Write(bytes, 0, bytes.Length);
            Thread.Sleep(15);
            string result = _serialPort.ReadExisting();

            return result == string.Format("Pong|{0}", hash);
        }

        public bool WriteLow(int pin)
        {
            var bytes = ByteBuilder.WriteLow(pin);

            _serialPort.Write(bytes, 0, bytes.Length);
            Thread.Sleep(15);
            int result = _serialPort.ReadChar() - 48;

            return result == 0;
        }

        public bool WriteHigh(int pin)
        {
            var bytes = ByteBuilder.WriteHigh(pin);

            _serialPort.Write(bytes, 0, bytes.Length);
            Thread.Sleep(15);
            int result = _serialPort.ReadChar() - 48;

            return result == 1;
        }

        private static SerialPort buildConnection(string port)
        {
            return new SerialPort(port, 38400)
            {
                Parity = Parity.None,
                DataBits = 8,
                StopBits = StopBits.One
            };
        }
    }
}
