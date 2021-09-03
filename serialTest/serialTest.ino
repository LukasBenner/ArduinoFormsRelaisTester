int ledState = 0;  

void setup() {
  // initialize digital pin LED_BUILTIN as an output.
  Serial.begin(9600);
  pinMode(LED_BUILTIN,OUTPUT); 
  
  String receiveVal;
  while(1){
    if(Serial.available() > 0)  
    {          
          
       if(Serial.find("start"))
       {
          digitalWrite(LED_BUILTIN, HIGH);
          delay(100);
          digitalWrite(LED_BUILTIN, LOW);
          delay(100);
          digitalWrite(LED_BUILTIN, HIGH);
          delay(100);
          digitalWrite(LED_BUILTIN, LOW);
          break;
       } 
       delay(50);
    }
  }
}

// the loop function runs over and over again forever
void loop() {   
     
    Serial.println("Starting test");
    delay(100);
}
