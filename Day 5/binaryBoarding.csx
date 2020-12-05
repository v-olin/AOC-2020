using System;
using System.IO;
using System.Linq;

string batch = "boardingpasses.txt";
string[] passes = File.ReadAllLines(batch);

int maxID = -1;
var seatIDs = new List<int>();
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
                    row = (int)r;                    
                else
                    top = (int)r;
                break;

            case 'B':
                if (i == 6)
                    row = (int)r + 1;
                else
                    bottom = (int)r + 1;
                break;
            
            case 'L':
                if (i == 9)
                    column = (int)c;
                else
                    right = (int)c;
                break;

            case 'R':
                if (i == 9)
                    column = (int)c + 1;
                else
                    left = (int)c + 1;
                break;

            default:
                Console.WriteLine($"Unexpected character: '{chars[i]}'");
                break;
        }
    }
    if (row * 8 + column > maxID)
        maxID = row * 8 + column;
    seatIDs.Add(row * 8 + column);
    Console.WriteLine($"Seat: {s} \tRow: {row}  \tColumn: {column}  \tID: {row * 8 + column}");
}
Console.WriteLine($"Highest ID: {maxID}");

// Borde kunna förenklas med groupby men for-loop har nog bättre prestanda
int mySeat = -1;
seatIDs.Sort();
for (int i = 0; i < seatIDs.Count - 1; i++){
    if (seatIDs[i+1] - seatIDs[i] == 2)
        mySeat = seatIDs[i] + 1;
}
Console.WriteLine($"My seat: {mySeat}")