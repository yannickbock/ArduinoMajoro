using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ArduinoMajoro.Tests
{
    public class MajoroTests
    {
        [Theory]
        [InlineData("Bilbo")]
        [InlineData("Sam")]
        [InlineData("Gandalf")]
        public void Hello_TestNoDeviceAvailable(string name)
        {
            Assert.Null(Majoro.Hello(name));
        }

        [Theory]
        [InlineData("Frodo")]
        public void Hello_TestDeviceAvailable(string name)
        {
            var devices = Enumerable.Range(0, 20).Select(x => Majoro.Hello(name)).ToList();

            var count = devices.Count(x => x != null && x.Name == name);

            // 19 = 95%
            Assert.InRange<int>(count, 19, 20);
        }

        [Theory]
        [InlineData("Frodo")]
        public void Hello_TestSearchGlobalWithoutName(string name)
        {
            var devices = Enumerable.Range(0, 10).SelectMany(x => Majoro.Hello()).ToList();

            var count = devices.Count(x => x != null && x.Name == name);

            // 9 = 90%
            Assert.InRange<int>(count, 9, 10);
        }

        [Fact]
        public void Ping_Test()
        {
            var hello = Majoro.Hello("Frodo");
            var majoro = new Majoro(hello);
            majoro.Connect();

            var count = Enumerable.Range(0, 20).Count(x => majoro.Ping());

            majoro.Disconnect();

            Assert.Equal(20, count);
        }

        [Theory]
        [InlineData(12)] // LED
        [InlineData(11)] // LED
        [InlineData(10)] // LED
        [InlineData(9)] // LED
        [InlineData(8)] // Relay
        [InlineData(7)] // Relay
        [InlineData(6)] // Relay
        [InlineData(5)] // Relay
        public void WriteHighAndLow_Test(int pin)
        {
            var hello = Majoro.Hello("Frodo");
            var majoro = new Majoro(hello);
            majoro.Connect();

            var count = Enumerable.Range(0, 50).Count(x => majoro.WriteHigh(pin) && majoro.WriteLow(pin));

            majoro.Disconnect();

            Assert.Equal(50, count);
        }

        [Theory]
        [InlineData(12, 8)]
        [InlineData(11, 7)]
        [InlineData(10, 6)]
        [InlineData(9, 5)]
        public void WriteHighAndLow_TestMultiple(int pin1, int pin2)
        {
            var hello = Majoro.Hello("Frodo");
            var majoro = new Majoro(hello);
            majoro.Connect();

            var count = Enumerable.Range(0, 50).Count(x =>
            {
                return majoro.WriteHigh(pin1) &&
                    majoro.WriteLow(pin1) &&
                    majoro.WriteHigh(pin2) &&
                    majoro.WriteLow(pin2);
            });

            majoro.Disconnect();

            Assert.Equal(50, count);
        }
    }
}
