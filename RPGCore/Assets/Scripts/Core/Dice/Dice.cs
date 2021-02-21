using UnityEngine;

namespace Core.Dice
{
    public static class Dice
    {
        public static int D(int sides) => Random.Range(1, sides + 1);
        public static int D4()         => D(4);
        public static int D6()         => D(6);
        public static int D8()         => D(8);
        public static int D10()        => D(10);
        public static int D12()        => D(12);
        public static int D20()        => D(20);
        public static int D100()       => Random.Range(0, 100);
    }
}