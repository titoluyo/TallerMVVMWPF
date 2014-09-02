using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.Modularity;
using CmdletConsole.Model;

namespace CmdletConsole.Math
{
    // TODO: 02. IModule.Initialize registers the commands

    public class MathModule : IModule
    {
        public void Initialize()
        {
            ICommand command;
            
            command = new AddCommand();
            CommandProcessor.Current.Register("+", command);
            CommandProcessor.Current.Register("add", command);

            command = new SubtractCommand();
            CommandProcessor.Current.Register("-", command);
            CommandProcessor.Current.Register("sub", command);
            CommandProcessor.Current.Register("subtract", command);

            command = new MultiplyCommand();
            CommandProcessor.Current.Register("*", command);
            CommandProcessor.Current.Register("x", command);
            CommandProcessor.Current.Register("multiply", command);

            command = new DivideCommand();
            CommandProcessor.Current.Register("/", command);
            CommandProcessor.Current.Register("divide", command);                        
        }
    }
}
