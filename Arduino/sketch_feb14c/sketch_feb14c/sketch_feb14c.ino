const int NUM_BUTTONS = 6;
const int BUTTON_PIN_START = 4;

int button_pins[NUM_BUTTONS];
boolean button_state[NUM_BUTTONS];
boolean prev_button_state[NUM_BUTTONS];

void setup() {
  // put your setup code here, to run once:
  Serial.begin(9600);

  while(!Serial){
    ; // wait for serial port to connect. Needed for native USB
  }

  for(int index = 0; index < NUM_BUTTONS; index++){
    button_pins[index] = index + BUTTON_PIN_START;
    pinMode(button_pins[index], INPUT_PULLUP);
    button_state[index] = 0;
    prev_button_state[index] = 0;
    
  }
  
  //Serial.setTimeout(150);
  Serial.setTimeout(75);
}

// This code helps unity ensure it connects to the correct device
void Handshake() {
  if(Serial.available() > 0) {
    String readData = Serial.readStringUntil("\n");
    if(readData.length() > 0 && !readData.equalsIgnoreCase("\n")){
      if(readData.startsWith("UNITY_HANDSHAKE")){
        Serial.println("ARDUINO_HANDSHAKE");
      }
    }
  }
}


void PollInputs() {
  for (int index = 0; index < NUM_BUTTONS; index++){
    button_state[index] = digitalRead(button_pins[index]);
    if (button_state[index] != prev_button_state[index]){
      prev_button_state[index] = button_state[index];
      if(button_state[index] == LOW){
        Serial.println(index);
      }
    }
  }
}

void loop() {
  // Check for Unity Handshake
  Handshake();
  // Check for button inputs
  PollInputs();
  delay(8);
}
