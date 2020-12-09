using System;
using System.IO;
using System.Linq;

string bootSequence = "ops.txt";
string[] boot = File.ReadAllLines(bootSequence);
var (res, flag) = CheckSequence(boot);
int instructionFlipped = -1;

for (int i = boot.Length - 1; i >= 0; i--){
    if (flag == 'T'){
        Console.WriteLine($"Accumulated: {res}");
        break;
    }
    if (instructionFlipped != -1){
        FlipOP(instructionFlipped);
    }
    if (boot[i].Split(' ')[0] != "acc"){
        instructionFlipped = i;
        FlipOP(i);
        (res, flag) = CheckSequence(boot);
    }
}

void FlipOP(int i){
    switch (boot[i].Split(' ')[0]){
        case "nop":
            boot[i] = "jmp " + boot[i].Split(' ')[1];
            break;
        case "jmp":
            boot[i] = "nop " + boot[i].Split(' ')[1];
            break;
        default:
            throw new Exception("Haha loser you suck");
    }
}

(int res, char flag) CheckSequence(string[] s){
    int accumulator = 0;
    int index = 0;
    var executedInstructions = new List<int>();

    try{
        while(!executedInstructions.Contains(index) && index != s.Length){
            executedInstructions.Add(index);
            string[] instruction = boot[index].Split(' ');
            switch(instruction[0]){
                case "nop":
                    index++;
                    break;
                case "acc":
                    accumulator += int.Parse(instruction[1]);
                    index++;
                    break;
                case "jmp":
                    index += int.Parse(instruction[1]);
                    break;
                default:
                    throw new Exception($"Unknown instruction: {boot[index]}");
            }
        }
    }
    catch(Exception){ }    

    if (index == s.Length){
        return (accumulator, 'T');
    }
    else {
        return (executedInstructions.Last(), 'F');
    }
}