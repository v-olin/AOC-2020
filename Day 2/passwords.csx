using System;
using System.IO;
using System.Linq;

var fp = "db.txt";
string[] entries = File.ReadAllLines(fp);
var enList = new List<Entry>();

foreach (string s in entries)
    enList.Add(new Entry(s));
    
int validPasswords = enList.FindAll(e => e.CharInValidPosition()).Count;
Console.WriteLine($"Antalet giltiga lösenord: {validPasswords}");

public class Entry {
    public Entry(string s){
        var data = s.Split('-', ' ', ':');
        Pos1 = int.Parse(data[0]);
        Pos2 = int.Parse(data[1]);
        Character = char.Parse(data[2]);
        Password = data[4];
    }
    public int Pos1 { get; set; }
    public int Pos2 { get; set; }
    public char Character { get; set; }
    public string Password { get; set; }
    public bool CharInValidPosition(){
        return ((Password[Pos1 - 1] == Character) != (Password[Pos2 - 1] == Character));
    }
}