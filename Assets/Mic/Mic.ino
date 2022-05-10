int pinA = 3; // Connected to CLK on KY-040
int pinB = 4; // Connected to DT on KY-040
int encoderPosCount = 0;
int pinALast;
int aVal;
boolean bCW;
int dir;

//Parameters
const int micPin  = A0;

//Variables
int micVal  = 0;

void setup() {

//Init Serial USB
  Serial.begin(9600);
  Serial.println(F("Initialize System"));
  //Init Microphone
  pinMode(micPin, INPUT);
  
 pinMode (pinA,INPUT);
 pinMode (pinB,INPUT);
 /* Read Pin A
 Whatever state it's in will reflect the last position
 */
 pinALast = digitalRead(pinA);
 Serial.begin (9600);
}

void ReadMicrophone(){

  ////Test routine for Microphone
  micVal = analogRead(micPin);
  //Serial.print(F("mic val ")); 
  Serial.println(micVal);
  
}

void InputDirection(){

  aVal = digitalRead(pinA);
 if (aVal != pinALast){ // Means the knob is rotating
 // if the knob is rotating, we need to determine direction
 // We do that by reading pin B.
 if (digitalRead(pinB) != aVal) { // Means pin A Changed first - We're Rotating Clockwise.
 encoderPosCount ++;
 //dir = 1;
 bCW = true;
 } else 
 {// Otherwise B changed first and we're moving CCW
 bCW = false;
 encoderPosCount--;
 //dir = -1;
 }

  Serial.println(encoderPosCount);
 //Serial.println(dir);
 
 }
 pinALast = aVal;
}

void loop() {
  ReadMicrophone();
}
