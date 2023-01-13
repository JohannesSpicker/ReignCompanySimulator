using System.Collections.Generic;
using TeppichsTools.Math.Randomness;
using UnityEngine;

namespace TeppichsDice
{
    public static class Dice
    {
        public static int D4()   => D(4);
        public static int D6()   => D(6);
        public static int D8()   => D(8);
        public static int D10()  => D(10);
        public static int D12()  => D(12);
        public static int D20()  => D(20);
        public static int D100() => ThreadSafeRandom.ThisThreadsRandom.Next(0, 100);

        public static List<int> D4(int   amount) => D(4,  amount);
        public static List<int> D6(int   amount) => D(6,  amount);
        public static List<int> D8(int   amount) => D(8,  amount);
        public static List<int> D10(int  amount) => D(10, amount);
        public static List<int> D12(int  amount) => D(12, amount);
        public static List<int> D20(int  amount) => D(20, amount);
        public static List<int> D100(int amount) => RandomNumbers.GetRandomInts(amount, new Vector2Int(0, 100));

        public static int D(int sides) => ThreadSafeRandom.ThisThreadsRandom.Next(1, sides + 1);
        public static List<int> D(int sides, int amount) =>
            RandomNumbers.GetRandomInts(amount, new Vector2Int(1, sides));
    }
}