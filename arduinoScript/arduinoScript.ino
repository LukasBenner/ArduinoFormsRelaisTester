#define StateWaitingInstruction 0
#define StateReadingCommand 1
#define StateReadingValue 2
#define TransitionOpeningBracket 3
#define TransitionClosingBracket 4
#define TransitionColon 5
#define TransitionUndefinedLetter 6

#define NUMBER_RELAIS_GROUPS 4
#define PIN_TABLE_OFFSET 2
#define RelaisTrigger 2

int repetitions = 0;
boolean runTest = false;

String instruction[2];
boolean instructionComplete = false;
int state = StateWaitingInstruction;
int transition = TransitionUndefinedLetter;

int group1[3] = {3,7,A0};
int group2[3] = {4,8,A1};
int group3[3] = {5,9,A2};
int group4[3] = {6,10,A3};
// [output1, output2, input]

int* groups[] = {group1,group2,group3,group4};

void setup() {
  Serial.begin(9600);
  //initialize all pins
  for(int i = 0; i < NUMBER_RELAIS_GROUPS; i++){
    int* group = groups[i];
    pinMode(group[0], OUTPUT);
    pinMode(group[1], OUTPUT);
    pinMode(group[2], INPUT);
  }
  pinMode(RelaisTrigger, OUTPUT);

}

void checkPair(int output, int input){
  String message = "";
  Serial.println(analogRead(input));
  if(analogRead(input) > 800){
    Serial.println(message + "<PINS:" + (output-PIN_TABLE_OFFSET) + " - " + input + " CONNECTION GOOD!>");
  }
  else{
    Serial.println(message + "<PINS:" + (output-PIN_TABLE_OFFSET) + " - " + input + " CONNECTION BAD!>");
  }
}

void checkGroup(int group[]){
  digitalWrite(group[0],HIGH);
  digitalWrite(group[1],LOW);
  checkPair(group[0], group[2]);

  digitalWrite(RelaisTrigger, HIGH);
  delay(500);
  
  digitalWrite(group[0],LOW);
  digitalWrite(group[1],HIGH);
  checkPair(group[1], group[2]);
  
  digitalWrite(RelaisTrigger, LOW);
  delay(500);
}

int repCounter = 1;
void loop() {
  if (Serial.available()) {
    serialParse();
  }
  else{
    if(instructionComplete)
    {
      String command = getCommand();
      String value = getValue();
      instructionComplete = false;
      
      if(command.equals("START"))
      {
        repCounter = 1;
        runTest = true;
        Serial.println("Setting start flag");
      }
      else if(command.equals("STOP"))
      {
        runTest = false;  
      }
      else if(command.equals("REP"))
      {
        setRepetitions();
        Serial.println("Setting the repetition number");
      }
      
    }
    else if(runTest && repCounter <= repetitions){
      for(int i = 0; i < NUMBER_RELAIS_GROUPS; i++){
        String info = "<INFO:Run";info += repCounter; info += " Group "; info += i+1; info += ">";
        Serial.println(info);
        int * group = groups[i];
        checkGroup(group);
        String progress = "<PROGRESS:"; progress += (double)((repCounter-1)*NUMBER_RELAIS_GROUPS+i+1)/(double)(repetitions*NUMBER_RELAIS_GROUPS); progress+= ">";
        Serial.println(progress);
        delay(1000);
      }
      repCounter++;
      delay(1000);
    }
    else if(runTest){
      Serial.println("<FINISHED>");
      runTest = false;  
      repCounter = 1;
      repetitions = 0;
    }
  }
}

String getCommand()
{
  return instruction[0];
}

String getValue()
{
  return instruction[1];
}


void setRepetitions(){
  repetitions = getValue().toInt();
}


void serialParse() {
    char inChar = (char)Serial.read();
    
    if(inChar == '<'){
      transition = TransitionOpeningBracket;
    }
    else if(inChar == '>'){
      transition = TransitionClosingBracket;
    }
    else if(inChar == ':'){
      transition = TransitionColon;
    }
    else
      transition = TransitionUndefinedLetter;


    if(state==StateWaitingInstruction && transition==TransitionOpeningBracket){
      state = StateReadingCommand;
      instruction[0] = "";
      instruction[1] = "";
    }
    else if(state==StateReadingCommand && transition==TransitionUndefinedLetter){
      instruction[0] += inChar;
    }
    else if(state==StateReadingCommand && transition==TransitionClosingBracket){
      state = StateWaitingInstruction;
      instructionComplete = true;
    }
    else if(state==StateReadingCommand && transition==TransitionColon){
      state = StateReadingValue;
    }
    else if(state==StateReadingValue && transition==TransitionUndefinedLetter){
      instruction[1] += inChar;
    }
    else if(state==StateReadingValue && transition==TransitionClosingBracket){
      state = StateWaitingInstruction;
      instructionComplete = true;
    }
}
