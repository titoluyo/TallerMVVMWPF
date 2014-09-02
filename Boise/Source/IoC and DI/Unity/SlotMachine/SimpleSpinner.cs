using System;

namespace SlotMachine
{
    internal class SimpleSpinner : ISpinner
    {
        private readonly Random rand = new Random();

        public int[] Spin()
        {
            int[] spin = new int[3];
            for (int i = 0; i < spin.Length; ++i)
            {
                spin[i] = this.rand.Next(5);
            }
            return spin;
        }
    }
}