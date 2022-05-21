int pinA = 3; // Connected to CLK on KY-040
int pinB = 4; // Connected to DT on KY-040
int encoderPosCount = 0;
int pinALast;
int aVal;
boolean bCW;
 
int sensorPin = 2; //define analog pin 2
int lightSensorValue = 0; 

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


 pinALast = digitalRead(pinA);
 Serial.begin (9600);
}


void GetInput(){
 
  aVal = digitalRead(pinA);
 if (aVal != pinALast){ // Means the knob is rotating
 // if the knob is rotating, we need to determine direction
 // We do that by reading pin B.
 if (digitalRead(pinB) != aVal) { // Means pin A Changed first - We're Rotating Clockwise.
 encoderPosCount ++;
 bCW = true;
 } else 
 {// Otherwise B changed first and we're moving CCW
 bCW = false;
 encoderPosCount--;
 }

  micVal = analogRead(micPin);
  lightSensorValue = analogRead(sensorPin);
  
  Serial.println(String(encoderPosCount) + ";" + String(micVal) + ";" + String(lightSensorValue));

 }
 pinALast = aVal;

}

void loop() {
  
GetInput();

}
