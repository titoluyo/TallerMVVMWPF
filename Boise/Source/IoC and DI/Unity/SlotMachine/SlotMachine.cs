namespace SlotMachine
{
    internal class SlotMachine
    {
        private readonly IMachineDisplay display;
        private readonly ISpinner spinner;
        private readonly IWinningsCalculator calculator;

        private int currentAmount;

        public SlotMachine(IMachineDisplay display, ISpinner spinner, IWinningsCalculator calculator)
        {
            this.currentAmount = 0;

            this.display = display;
            this.spinner = spinner;
            this.calculator = calculator;
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