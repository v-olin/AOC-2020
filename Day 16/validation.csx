using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

string[] input = File.ReadAllLines("tickets.txt");
var holder = new List<string>();
var splitInput = new List<string[]>();
for (int i = 0; i <= input.Length; i++){
    if (i == input.Length || input[i] == ""){
        splitInput.Add(holder.ToArray());
        holder.Clear();
    }  
    else holder.Add(input[i]);
}

var rx = new Regex(@"(\d{1,})([-])(\d{1,})");
IEnumerable<(int,int)> ranges = rx
    .Matches(string.Join("",input))
    .Cast<Match>()
    .Select(m => ParseRange(m.Value));
var concatRanges = new List<(int,int)>();
foreach ((int,int) r in ranges) AddRangeToList(concatRanges, r);
List<int[]> tickets = splitInput[2]
    .Skip(1)
    .Select(s => s.Split(',')
        .Select(int.Parse)
        .ToArray())
    .ToList();

var invalidValsInTickets = new List<int>();
AddInvalidValsToList(invalidValsInTickets, ranges, tickets);
Console.WriteLine($"Part 1: {invalidValsInTickets.Sum()}");

void AddInvalidValsToList(List<int> invalidVals, IEnumerable<(int,int)> ranges, IEnumerable<int[]> tickets){
    foreach (int[] ticket in tickets){
        foreach (int val in ticket){
            bool isValid = false;
            foreach ((int min, int max) range in ranges){
                if (val >= range.min && val <= range.max) isValid = true;
            }
            if (!isValid) invalidVals.Add(val);
        }
    }
}

(int min, int max) ParseRange(string s){
    string[] arr = s.Split('-');
    return (int.Parse(arr[0]), int.Parse(arr[1]));
}

void AddRangeToList(List<(int, int)> ranges, (int min, int max) r){
    var supers = ranges
    .FindAll(t => t.Overlaps(r));
    if (supers.Count() > 0){
        (int min, int max) super = supers.First();
            super.min = r.min < super.min ? r.min : super.min;
            super.max = r.max > super.max ? r.max : super.max;
    }
    else ranges.Add(r);
}

static bool Overlaps(this (int min, int max) org, (int min, int max) cmp){
    if (org.min > cmp.max || org.max < cmp.min) return false;
    else return true;
}