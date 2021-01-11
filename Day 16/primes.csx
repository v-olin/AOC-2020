using System;
using System.Linq;

List<int> l = Primes().Take(20).ToList();
Console.Read();

IEnumerable<int> Primes(){
    var ps = new List<int>() {2};
    int next = 3;
    while (true){
        int maxVal = (int)Math.Sqrt(next);
        bool isPrime = true;
        for (int i = 0; ps[i] <= maxVal; i++){
            if (next % ps[i] == 0){
                isPrime = false;
                break;
            }
        }
        if (isPrime){
            ps.Add(next);
            yield return next;
        }
        next += 2;
    }
}