using System;
using System.IO;
using System.Linq;

string batch = "boardingpasses.txt";
string[] passes = File.ReadAllLines(batch);

int maxID = -1;
foreach (string s in passes){
    (int bottom, int top, int left, int right) = (0, 127, 0, 7);
    (int row, int column) = (-1, -1);
    char[] chars = s.ToCharArray();
    for (int i = 0; i < chars.Length; i++){
        double r = (bottom + top) / 2;
        double c = (left + right) / 2;
        switch (chars[i]){
            case 'F':
                if (i == 6)
                    row = (int)Math.Floor(r);                    
                else
                    top = (int)Math.Floor(r);
                break;

            case 'B':
                if (i == 6)
                    row = (int)Math.Ceiling(r);
                else
                    bottom = (int)Math.Ceiling(r);
                break;
            
            case 'L':
                if (i == 9)
                    column = (int)Math.Floor(c);
                else
                    right = (int)Math.Floor(c);
                break;

            case 'R':
                if (i == 9)
                    column = (int)Math.Ceiling(c);
                else
                    left = (int)Math.Ceiling(c);
                break;

            default:
                Console.WriteLine($"Unexpected character: '{chars[i]}'");
                break;
        }
    }
    if (row * 8 + column > maxID)
        maxID = row * 8 + column;
    Console.WriteLine($"Seat: {s} \tRow: {row}  \tColumn: {column}  \tID: {row * 8 + column}");
}
Console.WriteLine($"Highest ID: {maxID}");