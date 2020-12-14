using System;
using System.IO;
using System.Linq;

string[] instructions = File.ReadAllLines("init.txt");
string mask = "";
var memory = new List<Data>();

foreach (string s in instructions){
    string[] t = s.Split(' ');
    if (t[0].Contains("mask")) mask = t[2];
    else{
        t = s.Split(new char[] { '[', ']', ' ' });
        WriteToMemory(ref memory, t[1], t[4], mask);
    }
}

long ans = memory.Select(d => long.Parse(d.Value)).Sum();
Console.WriteLine($"Part 1: {ans}");

memory.Clear();
foreach (string s in instructions){
    string[] t = s.Split(' ');
    if (t[0].Contains("mask")) mask = t[2];
    else{
        t = s.Split(new char[] { '[', ']', ' ' });
        List<char> address = MaskAdress(ToBinaryString(t[1], 36), mask).ToCharArray().ToList();
        List<char[]> flucBits = PossibleCombinations(address.Where(c => c == 'X').Count());
        List<string> addressList = GetPossibleAdressList(address, flucBits, mask);
        foreach (string a in addressList){
            WriteToMemory(ref memory, a, t[4], new string('X', 36));
        }
    }
}

long ansp2 = memory.Select(d => long.Parse(d.Value)).Sum();
Console.WriteLine($"Part 2: {ansp2}");

void WriteToMemory(ref List<Data> mem, string address, string val, string mask){
    char[] bwMask = mask.ToCharArray();
    char[] value = ToBinaryString(val, 36).ToCharArray();
    for (int i = 0; i < value.Count(); i++){
        value[i] = bwMask[i] == 'X' ? value[i] : bwMask[i];
    }
    List<Data> mad = mem.FindAll(d => d.Address == address);
    if (mad.Count() == 0) mem.Add(new Data() {
        Address = address,
        Value = Convert.ToInt64(new string(value), 2).ToString()
    });
    else {
        int index = mem.IndexOf(mad.First());
        mem[index].Value = Convert.ToInt64(new string(value), 2).ToString();
    }
}

string MaskAdress(string address, string mask){
    char[] maskbits = mask.ToCharArray();
    char[] add = address.ToCharArray();
    for (int i = 0; i < maskbits.Length; i++){        
        if (maskbits[i] == '1')         add[i] = '1';
        else if (maskbits[i] == 'X')    add[i] = 'X';
    }
    return new string(add);
}

string ToBinaryString(string s, int l){
    var sInBinary = Convert.ToString(int.Parse(s), 2);
    return sInBinary.Length < l ? new string('0', l - sInBinary.Length) + sInBinary : sInBinary;
}

List<string> GetPossibleAdressList(List<char> address, List<char[]> combinations, string mask){
    var newAddressList = new List<string>();
    var add = new List<char>(address);
    for (int i = 0; i < combinations.Count(); i++){
        for (int j = 0; j < combinations[0].Count(); j++){
            add[add.IndexOf('X')] = combinations[i][j];
        }
        newAddressList.Add(new string(add.ToArray()));
        add = new List<char>(address);
    }
    return newAddressList;
}

List<char[]> PossibleCombinations(int n){
    return Enumerable.Range(0, (int)Math.Pow(2, n))
        .Select(n => Convert.ToString(n))
        .Select(s => ToBinaryString(s, n))
        .Select(s => s.ToCharArray())
        .ToList();
}

public class Data {
    public string Address { get; set; }
    public string Value { get; set; }
}