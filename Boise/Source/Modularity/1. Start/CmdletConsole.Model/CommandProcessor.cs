using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CmdletConsole.Model
{
    //TODO: 03. A singleton command registry and processor

    public class CommandProcessor
    {
        private Dictionary<string, ICommand> commandRegistry = new Dictionary<string, ICommand>(StringComparer.CurrentCultureIgnoreCase);

        private static ICommandLog NoOpCommandLog = new NoOpCommandLog();

        private CommandProcessor()
        {

        }

        static CommandProcessor()
        {
            Current = new CommandProcessor();
        }

        public static CommandProcessor Current { get; private set; }

        public ICommandLog Log { get; set; }

        public void Register(string name, ICommand command)
        {
            commandRegistry[name] = command;
        }
        
        public void Execute(string name, string arguments)
        {
            ICommand command;
            if (this.commandRegistry.TryGetValue(name, out command))
            {                
                if (this.Log != null)
                {
                    command.Execute(arguments, this.Log);
                }
                else
                {
                    command.Execute(arguments, NoOpCommandLog);
                }
            }
            else
            {
                throw new Exception(string.Format("The '{0}' command is not supported.", name));
            }
        }
    }
}
