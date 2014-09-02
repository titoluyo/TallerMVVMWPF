namespace SlotMachine
{
    internal class AirportWinningsCalculator : IWinningsCalculator
    {
        public ISpinResult CalculateResult(int[] spin)
        {
            return new SpinResult(false, 0);
        }
    }
}