using System;

namespace SlotMachine
{
    internal class DisplaySpinner : ISpinner
    {
        private readonly IMachineDisplay display;
        private readonly Random rand = new Random();

        public DisplaySpinner(IMachineDisplay display)
        {
            this.display = display;
        }

        public int[] Spin()
        {
            int[] spin = new int[3];
            for (int i = 0; i < spin.Length; ++i)
            {
                spin[i] = this.rand.Next(5);
            }
            this.display.DisplayMessage(string.Format("++ {0} ++ {1} ++ {2} ++", spin[0], spin[1], spin[2]));

            return spin;
        }
    }
}