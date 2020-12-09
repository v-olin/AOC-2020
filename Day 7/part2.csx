using System;
using System.IO;
using System.Linq;

string data = "test.txt";
string[] lines = File.ReadAllLines(data);
var parsedData = new List<Bag>();
string[] uBags;
foreach (string l in lines){
    string[] m = l.Split(new string[] { " contain " }, StringSplitOptions.None);
    string bagColour = m[0].Split(' ')[0] + " " + m[0].Split(' ')[1];
    uBags = m[1].Split(new string[] { ", " }, StringSplitOptions.None);
    if (m[1] == "no other bags."){
        parsedData.Add(new Bag(){
            ContainsNoOtherBags = true,
            Colour = bagColour
        });
    }
    else{
        parsedData.Add(new Bag(){
            BagsInfo = uBags.ToList(),
            Colour = bagColour
        });
    }
}

foreach (Bag b in parsedData){
    if (b.BagsInfo != null){
        foreach (string s in b.BagsInfo){
            b.ContainedBags.Add(parsedData.Where(k => k.Colour == ParseBagInfo(s)).FirstOrDefault(), int.Parse(s.Split(' ')[0]));
        }
    }   
}

Bag root = parsedData.Where(b => b.Colour == "shiny gold").FirstOrDefault();
int test = BagsNeeded(root);

System.Console.WriteLine(" ");
int BagsNeeded(Bag b){
    int totalBags = 0;
    if (!b.ContainsNoOtherBags){
        foreach (var t in b.ContainedBags){
            totalBags += t.Value + t.Key.SumOfSubBags() * t.Value;
        }
        return totalBags;
    }
    else {
        return 1;
    }
}

string ParseBagInfo(string s){
    string[] temp = s.Split(' ');
    return (temp[1] + " " + temp[2]);
}


class Bag {
    public Bag(){
        ContainedBags = new Dictionary<Bag, int>();
    }
    public string Colour { get; set; }
    public bool ContainsNoOtherBags { get; set; }
    public List<string> BagsInfo { get; set; }
    public Dictionary<Bag, int> ContainedBags { get; set; }
    public int SumOfSubBags(){
        int n = 0;
        foreach (var kv in ContainedBags){
            n += kv.Value;
        }
        return n;
    }
}