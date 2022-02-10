const int buttonpin01 = 9;
const int buttonpin02 = 8;
const int buttonpin03 = 7;
const int buttonpin04 = 6;
const int buttonpin05 = 5;
const int buttonpin06 = 4;
void setup() {
  // put your setup code here, to run once:
  Serial.begin(9600);


  pinMode(buttonpin01, INPUT_PULLUP);
  pinMode(buttonpin02, INPUT_PULLUP);
  pinMode(buttonpin03, INPUT_PULLUP);
  pinMode(buttonpin04, INPUT_PULLUP);
  pinMode(buttonpin05, INPUT_PULLUP);
  pinMode(buttonpin06, INPUT_PULLUP);


  //Serial.setTimeout(150);
  Serial.setTimeout(75);
}

void loop() {
  // put your main code here, to run repeatedly:

 Serial.print(digitalRead(buttonpin01));
 Serial.print(",");
 Serial.print(digitalRead(buttonpin02));
 Serial.print(",");
 Serial.print(digitalRead(buttonpin03));
 Serial.print(",");
 Serial.print(digitalRead(buttonpin04));
 Serial.print(",");
 Serial.print(digitalRead(buttonpin05));
 Serial.print(",");
 Serial.println(digitalRead(buttonpin06));
        delay(30);
 // String readData = Serial.readStringUntil('\n');
//  if(readData.length() > 0 && !readData.equalsIgnoreCase("\n"))
//  {
//    Serial.println(readData);
//  }
}
