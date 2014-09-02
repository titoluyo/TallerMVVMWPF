namespace SlotMachine
{
    internal class SpinResult : ISpinResult
    {
        private readonly bool isWinner;
        private readonly int amount;

        public SpinResult(bool isWinner, int amount)
        {
            this.isWinner = isWinner;
            this.amount = amount;
        }

        public bool IsWinner
        {
            get { return this.isWinner; }
        }

        public int WinningsAmount
        {
            get { return this.amount; }
        }
    }
}