using System.ComponentModel.Composition;

namespace SlotMachine
{
    [Export(typeof(SlotMachine))]
    internal class SlotMachine
    {
        [Import] private IMachineDisplay display;

        [Import] private ISpinner spinner;

        [Import] private IWinningsCalculator calculator;

        private int currentAmount;

        public SlotMachine()
        {
            this.currentAmount = 0;
        }

        public void AddMoney(int amountInPennies)
        {
            this.currentAmount += amountInPennies;
            this.display.ShowCurrentTotal(this.currentAmount);
        }

        public void Spin()
        {
            int[] spin = this.spinner.Spin();
            ISpinResult result = this.calculator.CalculateResult(spin);
            if (result.IsWinner)
            {
                // Dispense winnings and ...
                this.display.MakeLotsOfNoise();
            }
            else
            {
                this.display.SorryYouDidntWin();
            }
            this.currentAmount = 0;
        }
    }
}