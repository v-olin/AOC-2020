using System;
using System.IO;
using System.Linq;


void Part1(string[] lines){
    var parsedData = new List<Bag>();
    foreach (string l in lines){
        string[] m = l.Split(new string[] { " contain " }, StringSplitOptions.None);
        string bagColour = m[0].Split(' ')[0] + " " + m[0].Split(' ')[1];
        string[] uBags = m[1].Split(new string[] { ", " }, StringSplitOptions.None);
        if (m[1] == "no other bags."){
            parsedData.Add(new Bag(){
                ContainsNoOtherBags = true,
                Colour = bagColour
            });
        }
        else{
            parsedData.Add(new Bag(){
                BagsContained = uBags.Select(s => s.Split(' ')[1] + " " + s.Split(' ')[2]).ToList(),
                Colour = bagColour
            });
        }
    }

    List<Bag> leafBags = parsedData.Where(b => b.ContainsColor("shiny gold")).ToList();
    int bagsAdded = 0;
    do{
        bagsAdded = 0;
        var newBags = new List<Bag>();
        foreach (Bag b in leafBags){
            List<Bag> temp = parsedData.Where(bp => bp.ContainsColor(b.Colour)).ToList();
            newBags.AddRange(temp.Where(bpp => !leafBags.Contains(bpp)));
        }
        bagsAdded += newBags.Distinct().Count();
        leafBags.AddRange(newBags.Distinct());
    }
    while (bagsAdded > 0);

    Console.WriteLine($"Part 1: {leafBags.Count()}");
}

void Part2(string[] lines){
    var parsedData = new List<Bag>();
    foreach (string l in lines){
        string[] m = l.Split(new string[] { " contain " }, StringSplitOptions.None);
        string bagColour = m[0].Split(' ')[0] + " " + m[0].Split(' ')[1];
        string[] uBags = m[1].Split(new string[] { ", " }, StringSplitOptions.None);
        if (m[1] == "no other bags."){
            parsedData.Add(new Bag(){
                ContainsNoOtherBags = true,
                Colour = bagColour
            });
        }
        else{
            parsedData.Add(new Bag(){
                BagsContained = uBags.ToDictionary(s => s.Split(' ')[1] + " " + s.Split(' ')[2],  s => int.Parse(s.Split(' ')[0])),
                Colour = bagColour
            });
        }
    }
    
    Bag b = parsedData.Where(s => s.Colour == "shiny gold").FirstOrDefault();
    int n = 0;
    // List<Bag> bagsInShiny = b.BagsContained

    // Console.WriteLine($"Part 2: {leafBags.Count()}");
}

class Bag {
    public Dictionary<string, int> BagsContained { get; set; }
    public string Colour { get; set; }
    public bool ContainsNoOtherBags { get; set; }
    public bool ContainsColor(string c){
        return BagsContained != null ? BagsContained.ContainsKey(c) : false;
    }
}