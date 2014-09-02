using System;

namespace SlotMachine
{
    internal class ConsoleDisplay : IMachineDisplay
    {
        public ConsoleDisplay()
        {
            
        }
        public void ShowCurrentTotal(int amountInPennies)
        {
            Console.WriteLine("You've entered ${0}", amountInPennies/100.0);
        }

        public void MakeLotsOfNoise()
        {
            Console.WriteLine("\n\n**** YOU WIN! ****\n\n");
        }

        public void SorryYouDidntWin()
        {
            Console.WriteLine("Sorry, you didn't win, but I bet if you put in more coins you're sure to win next time.");
        }

        public void DisplayMessage(string message)
        {
            Console.WriteLine(message);
        }
    }
}