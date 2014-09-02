namespace SlotMachine
{
    internal interface IMachineDisplay
    {
        void ShowCurrentTotal(int amountInPennies);
        void MakeLotsOfNoise();
        void SorryYouDidntWin();
        void DisplayMessage(string message);
    }
}