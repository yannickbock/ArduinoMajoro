String NAME = "Frodo";
// #########################
bool pinIsOutput[63];

enum Method
{
  Hello = 0,
  Ping = 1,
  WriteLow = 2,
  WriteHigh = 3
};

void setup()
{
  Serial.begin(38400);
  for (int i = 0; i < sizeof(pinIsOutput); i++)
  {
    pinIsOutput[i] = false;
  }
  delay(25);
}

void loop()
{
  
}

void serialEvent()
{
  int value = Serial.read();
  int method = (value & 3);
  int pin  = (value - (int)method) >> 2;

  switch (method)
  {
    case Hello:
      Serial.print("ARDUINO|" + NAME);
      break;
    case Ping:
      Serial.print("Pong|" + String(pin));
      break;
    case WriteLow:
      if (pinIsOutput[pin] == false)
      {
        pinMode(pin, OUTPUT);
        pinIsOutput[pin] = true;
      }
      digitalWrite(pin, LOW); 
      Serial.print(digitalReadOutputPin(pin));
      break;
    case WriteHigh:
      if (pinIsOutput[pin] == false)
      {
        pinMode(pin, OUTPUT);
        pinIsOutput[pin] = true;
      }
      digitalWrite(pin, HIGH); 
      Serial.print(digitalReadOutputPin(pin));
      break;
  }
  
  //delay(10); //IMPORTANT!
}

int digitalReadOutputPin(uint8_t pin)
{
 uint8_t bit = digitalPinToBitMask(pin);
 uint8_t port = digitalPinToPort(pin);
 if (port == NOT_A_PIN) 
   return LOW;

 return (*portOutputRegister(port) & bit) ? HIGH : LOW;
}