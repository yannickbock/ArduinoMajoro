namespace ArduinoMajoro
{
    public interface IMajoro
    {
        Arduino Device { get; }

        void Connect();
        void Disconnect();
        bool Ping();
        bool WriteHigh(int pin);
        bool WriteLow(int pin);
    }
}