using System;
using System.IO;
using System.Linq;

string[] instructions = File.ReadAllLines("instructions.txt");
int[] pos = new int[] { 0, 0, 0 }, wp = new int[] { 10, 1, 0, 0, 0};

foreach (string s in instructions){
    pos = ApplyInstruction(s, 1, pos);
    wp = ApplyInstruction(s, 2, wp);
};
Console.WriteLine($"Part 1: {Math.Abs(pos[0]) + Math.Abs(pos[1])}");
Console.WriteLine($"Part 2: {Math.Abs(wp[3]) + Math.Abs(wp[4])}");

int[] ApplyInstruction(string s, int f, int[] pos){
    int x = pos[0], y = pos[1], v = pos[2];
    char d = s.First();
    int val = int.Parse(new string(s.Skip(1).ToArray()));
    switch(s.First()){
        case 'N':
            y += val;
            break;
        case 'S':
            y -= val;
            break;
        case 'E':
            x += val;
            break;
        case 'W':
            x -= val;
            break;
        case 'L':
            if (f == 1) v = (v + val) % 360;
            else{
                if (val == 90) { x = pos[1]*-1; y = pos[0]; }
                else if (val == 180) { x = pos[0]*-1; y = pos[1]*-1; }
                else if (val == 270) { x = pos[1]; y = pos[0]*-1; }
            }
            break;
        case 'R':
            if (f == 1) v = (v + (360 - val)) % 360;
            else{
                if (val == 90) { x = pos[1]; y = pos[0]*-1; }
                else if (val == 180) { x = pos[0]*-1; y = pos[1]*-1; }
                else if (val == 270) { x = pos[1]*-1; y = pos[0]; }
            }
            break;
        default:
            if (f == 1){
                if (v == 0)         x += val;
                else if (v == 90)   y += val;
                else if (v == 180)  x -= val;
                else                y -= val;
            }
            else { pos[3] += x * val; pos[4] += y * val; }
            break;
    }
    if (f == 1) return new int[] { x, y, v };
    else return new int[] { x, y, v, pos[3], pos[4] };
}