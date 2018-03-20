using System;
using Xunit;

namespace ArduinoMajoro.Tests
{
    public class ByteBuilderTests
    {
        [Fact]
        public void Hello_Test()
        {
            var bytes = ByteBuilder.Hello();

            var method = ByteBuilder.Encrypt(bytes[0], out int x);

            Assert.Equal(0, x);
            Assert.Equal(Method.Hello, method);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(17)]
        [InlineData(42)]
        [InlineData(63)]
        public void Ping_Test(int hash)
        {
            var bytes = ByteBuilder.Ping(hash);

            var method = ByteBuilder.Encrypt(bytes[0], out int hashOut);

            Assert.Equal(hash, hashOut);
            Assert.Equal(Method.Ping, method);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(64)]
        public void Ping_TestOutOfRange(int hash)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => ByteBuilder.Ping(hash));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(17)]
        [InlineData(42)]
        [InlineData(63)]
        public void WriteLow_Test(int pin)
        {
            var bytes = ByteBuilder.WriteLow(pin);

            var method = ByteBuilder.Encrypt(bytes[0], out int pinOut);

            Assert.Equal(pin, pinOut);
            Assert.Equal(Method.WriteLow, method);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(64)]
        public void WriteLow_TestOutOfRange(int hash)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => ByteBuilder.WriteLow(hash));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(17)]
        [InlineData(42)]
        [InlineData(63)]
        public void WriteHigh_Test(int pin)
        {
            var bytes = ByteBuilder.WriteHigh(pin);

            var method = ByteBuilder.Encrypt(bytes[0], out int pinOut);

            Assert.Equal(pin, pinOut);
            Assert.Equal(Method.WriteHigh, method);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(64)]
        public void WriteHigh_TestOutOfRange(int hash)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => ByteBuilder.WriteHigh(hash));
        }
    }
}
