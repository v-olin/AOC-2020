using System;
using System.IO;
using System.Linq;

List<string> rows = File.ReadAllLines("layout.txt").ToList();
var ns = new List<string>();

do {
    ns = rows;
    rows = ApplyRules(ns);
} while (!rows.SequenceEqual(ns));

int seatsOccupied = rows
    .Aggregate((t, n) => t + n)
    .ToCharArray()
    .Where(c => c == '#')
    .Count();
Console.WriteLine($"Part 1: {seatsOccupied}");

List<string> ApplyRules(List<string> rows){
    List<string> d = rows;
    var dp = new List<string>();
    for (int i = 0; i < rows.Count(); i++){
        string s = "";
        for (int j = 0; j < rows[0].Length; j++){
            List<int[]> adjSeats = AdjacentSeats(i,j);
            int occupiedAdjacentSeats = adjSeats.Select(arr => SeatIsOccupied(arr)).Sum();
            if (rows[i][j] == 'L' && occupiedAdjacentSeats < 1) s += "#";
            else if (rows[i][j] == '#' && occupiedAdjacentSeats > 3) s += "L";
            else s += rows[i][j];
        }
        dp.Add(s);
    }
    return dp;
}

int SeatIsOccupied(int[] arr){
    if (rows[arr[0]][arr[1]] == '#') return 1;
    else return 0;
}

List<int[]> AdjacentSeats(int i, int j){
    List<int[]> cp = (from ip in new int[] {i-1, i, i+1}
                        from jp in new int[] {j-1, j, j+1}
                        select new int[] {ip, jp})
                        .ToList();
    return cp
        .Where(arr => arr[0] >= 0 && arr[0] < rows.Count())
        .Where(arr => arr[1] >= 0 && arr[1] < rows[0].Length)
        .Where(arr => !(arr[0] == i && arr[1] == j))
        .ToList();
}