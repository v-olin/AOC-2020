using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

var fp = "db.txt";
string[] entries = File.ReadAllLines(fp);
Console.WriteLine($"Antalet lösenord: {entries.Length}");
List<Entry> enList = new List<Entry>();

foreach (string s in entries){
    string[] parsedEntry = s.Split('-', ' ', ':');
    enList.Add(new Entry {
        Pos1 = int.Parse(parsedEntry[0]),
        Pos2 = int.Parse(parsedEntry[1]),
        Character = char.Parse(parsedEntry[2]),
        Password = parsedEntry[4]
    });
}
int validPasswords = 0;

foreach (Entry e in enList){
    Console.WriteLine(e.ToString());
    bool cAtFst = e.Password[e.Pos1 - 1] == e.Character;
    bool cAtSnd = e.Password[e.Pos2 - 1] == e.Character;
    System.Console.WriteLine(cAtFst != cAtSnd);
    if (cAtFst != cAtSnd)
        validPasswords++;
}

System.Console.WriteLine($"Antalet giltiga lösenord: {validPasswords}");

public class Entry {
    private int min;
    private int max;
    private char c;
    private string pwd;
    public int Pos1 { get { return min; } set { min = value; }}
    public int Pos2 { get { return max; } set { max = value; }}
    public char Character { get { return c; } set { c = value; }}
    public string Password { get { return pwd; } set { pwd = value; }}
    public override string ToString()
    {
        return $"Pwd: {Password}\nChar: {Character}\nPos: {Pos1}, {Pos2}";
    }
}