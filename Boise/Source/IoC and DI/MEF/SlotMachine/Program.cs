using System;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;

namespace SlotMachine
{
    internal partial class Program
    {
        private static void Main(string[] args)
        {
            CompositionContainer container = GetConfiguredContainer();
            SlotMachine slot = container.GetExportedValue<SlotMachine>();
            Play(slot);
            Console.WriteLine("Thanks for playing, come back when you have more money!");
        }

        private static CompositionContainer GetConfiguredContainer()
        {
            ComposablePartCatalog catalog = new AssemblyCatalog(typeof(SlotMachine).Assembly);
            CompositionContainer container = new CompositionContainer(catalog);
            return container;
        }
    }
}