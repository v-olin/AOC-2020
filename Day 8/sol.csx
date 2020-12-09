using System;
using System.IO;

Main();

void Main()
{
  var input = File.ReadAllLines("ops.txt");
  var part1 = Part1(input);
  var part2 = Part2(input);
  Console.WriteLine($"Part 1: {part1}\tPart 2: {part2}");
}

int Part1(string[] input)
{
  var i = 0;
  var acc = 0;
  var cmds = new HashSet<int>();

  while (cmds.Add(i))
  {
    var lin = input[i];
    var spl = lin.Split(' ');
    var cmd = spl[0];
    var arg = int.Parse(spl[1]);

    if (cmd == "acc") { acc += arg; i++; }
    else if (cmd == "jmp") { i += arg; }
    else if (cmd == "nop") { i++; }
  }

  return acc;
}

int Part2(string[] input)
{
  var i = 0;
  var acc = 0;
  var swap = false;
  var cmds = new HashSet<int>();
  var nops = new HashSet<int>();
  var jmps = new HashSet<int>();

  while (true)
  {
    var lin = input[i];
    var spl = lin.Split(' ');
    var cmd = spl[0];
    var arg = int.Parse(spl[1]);

    if (!swap && cmd == "nop" && nops.Add(i)) { swap = true; cmd = "jmp"; }
    else if (!swap && cmd == "jmp" && jmps.Add(i)) { swap = true; cmd = "nop"; }

    if (cmd == "acc") { acc += arg; i++; }
    else if (cmd == "jmp") { i += arg; }
    else if (cmd == "nop") { i++; }

    if (i == input.Length) { break; }
    else if (!cmds.Add(i)) { i = 0; acc = 0; swap = false; cmds.Clear(); }
  }

  return acc;
}