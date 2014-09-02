using System;
using Microsoft.Practices.Unity;

namespace SlotMachine
{
    internal partial class Program
    {
        private static void Main(string[] args)
        {
            IUnityContainer container = GetConfiguredContainer();
            SlotMachine slot = container.Resolve<SlotMachine>();

            Play(slot);
            Console.WriteLine("Thanks for playing, come back when you have more money!");
        }

        private static IUnityContainer GetConfiguredContainer()
        {
            IUnityContainer container = new UnityContainer()
                .RegisterType<IMachineDisplay, ConsoleDisplay>(new ContainerControlledLifetimeManager())
                .RegisterType<ISpinner, DisplaySpinner>()
                .RegisterType<IWinningsCalculator, AirportWinningsCalculator>();

            return container;
        }
    }
}