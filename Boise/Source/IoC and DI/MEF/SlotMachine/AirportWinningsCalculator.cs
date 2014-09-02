using System.ComponentModel.Composition;

namespace SlotMachine
{
    [Export(typeof(IWinningsCalculator))]
    internal class AirportWinningsCalculator : IWinningsCalculator
    {
        public ISpinResult CalculateResult(int[] spin)
        {
            return new SpinResult(false, 0);
        }
    }
}