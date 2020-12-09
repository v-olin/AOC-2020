using System;
using System.IO;

string[] bootsequence = File.ReadAllLines("ops.txt");
int ansP1 = Part1(bootsequence);
int ansP2 = Part2(bootsequence);
Console.WriteLine($"Part 1: {ansP1}\tPart 2: {ansP2}");

int Part1(string[] s){
    int index = 0;
    int accumulator = 0;
    var commands = new HashSet<int>();

    while (commands.Add(index)){
        string[] t = s[index].Split(' ');
        string instruction = t[0];
        int arg = int.Parse(t[1]);

        if (instruction == "nop"){
            index++;
        }
        else if (instruction == "acc"){
            accumulator += arg;
            index++;
        }
        else if (instruction == "jmp"){
            index += arg;
        }
    }

    return accumulator;
}

int Part2(string[] s){
    int index = 0;
    int accumulator = 0;
    bool instructionSwapped = false;
    var cmds = new HashSet<int>();
    var nops = new HashSet<int>();
    var jmps = new HashSet<int>();

    while (true){
        string[] t = s[index].Split(' ');
        string instruction = t[0];
        int arg = int.Parse(t[1]);

        if (!instructionSwapped && instruction == "nop" && nops.Add(index)){
            instructionSwapped = true;
            instruction = "jmp";
        }
        else if (!instructionSwapped && instruction == "jmp" && jmps.Add(index)){
            instructionSwapped = true;
            instruction = "nop";
        }

        if (instruction == "nop"){
            index++;
        }
        else if (instruction == "acc"){
            accumulator += arg;
            index++;
        }
        else if (instruction == "jmp"){
            index += arg;
        }

        if (index == s.Length){
            break;
        }
        else if (!cmds.Add(index)){
            index = 0;
            accumulator = 0;
            instructionSwapped = false;
            cmds.Clear();
        }
    }
    
    return accumulator;
}