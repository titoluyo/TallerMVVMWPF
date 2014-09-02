using System;

namespace SlotMachine
{
    internal partial class Program
    {
        public static void Play(SlotMachine slot)
        {
            string option;
            do
            {
                Console.WriteLine("Type \"M\" to add money, \"S\" to spin, or \"Q\" to quit");
                option = Console.ReadLine();

                if (option.StartsWith("M", StringComparison.CurrentCultureIgnoreCase))
                {
                    slot.AddMoney(100);
                }
                else if (option.StartsWith("S", StringComparison.CurrentCultureIgnoreCase))
                {
                    slot.Spin();
                }
                else if (!option.StartsWith("Q", StringComparison.CurrentCultureIgnoreCase))
                {
                    Console.WriteLine("I don't understand.");
                }
            } while (!option.StartsWith("Q", StringComparison.CurrentCultureIgnoreCase));
        }
    }
}