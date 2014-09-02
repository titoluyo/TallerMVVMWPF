namespace SlotMachine
{
    internal interface IWinningsCalculator
    {
        ISpinResult CalculateResult(int[] spin);
    }

    internal interface ISpinResult
    {
        bool IsWinner { get; }
        int WinningsAmount { get; }
    }
}