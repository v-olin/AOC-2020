using System;
using System.Linq;
using System.IO;

string file = "1input.txt";
int[] nums = Array.ConvertAll(File.ReadAllLines(file), s => int.Parse(s));
for (int i = 0; i < (nums.Length - 2); i++){
    for (int j = i+1; j < (nums.Length - 1); j++){
        for (int k = i+2; k < nums.Length; k++){
            if (nums[i] + nums[j] + nums[k] == 2020){
                Console.WriteLine(nums[i] * nums[j] * nums[k]);
                break;
            }
        }
    }
}