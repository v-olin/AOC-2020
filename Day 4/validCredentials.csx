using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

var db = "creds.txt";
string[] batch = File.ReadAllLines(db);
var concatenatedBatch = new List<string>();

// Behöver fixas, klipper sista passet/sista raden i databasen
string tempEntry = "";
for (int i = 0; i < batch.Length; i++){
    if (batch[i] == "" || (i == batch.Length)){
        concatenatedBatch.Add(tempEntry);
        tempEntry = "";
    }
    else {
        tempEntry += " " + batch[i];
    }
}

var listOfPassports = new List<Passport>();

foreach (string s in concatenatedBatch){
    listOfPassports.Add(new Passport(s.Split(' ')
        .Skip(1)
        .Select(s => s.Split(':'))
        .ToDictionary(kv => kv[0], kv => kv[1])));
}

var validPassports = listOfPassports.Where(p => p.PassportIsValid() == true);
Console.WriteLine($"Valid passports: {validPassports.Count()}");

public class Passport{
    public Passport(Dictionary<string,string> d){
        byr = d.Keys.Contains("byr") ? d["byr"] : null;
        iyr = d.Keys.Contains("iyr") ? d["iyr"] : null;
        eyr = d.Keys.Contains("eyr") ? d["eyr"] : null;
        hgt = d.Keys.Contains("hgt") ? d["hgt"] : null;
        hcl = d.Keys.Contains("hcl") ? d["hcl"] : null;
        ecl = d.Keys.Contains("ecl") ? d["ecl"] : null;
        pid = d.Keys.Contains("pid") ? d["pid"] : null;
        cid = d.Keys.Contains("cid") ? d["cid"] : null;
    }

    #nullable enable
    public bool PassportIsValid(){
        string?[] props = {byr, iyr, eyr, hgt, hcl, ecl, pid};
        // Console.WriteLine(Array.FindAll(props, p => p == null).Count());
        return !(Array.FindAll(props, p => p == null).Count() > 0);
    }
    public string? byr { get; set; }
    public string? iyr { get; set; }
    public string? eyr { get; set; }
    public string? hgt { get; set; }
    public string? hcl { get; set; }
    public string? ecl { get; set; }
    public string? pid { get; set; }
    public string? cid { get; set; }
}