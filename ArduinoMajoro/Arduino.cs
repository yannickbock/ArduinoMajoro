using System;

namespace ArduinoMajoro
{
    public class Arduino : IEquatable<Arduino>
    {
        public Arduino(string name, string serialPort)
        {
            Name = name;
            SerialPort = serialPort;
        }

        public string Name { get; private set; }

        public string SerialPort { get; private set; }

        public bool Equals(Arduino other)
        {
            return Name == other.Name && SerialPort == other.SerialPort;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode() + SerialPort.GetHashCode();
        }
    }
}
