using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CmdletConsole.Model;

namespace CmdletConsole.Math
{
    public class MultiplyCommand : ICommand
    {
        public void Execute(string arguments, ICommandLog log)
        {
            string[] parts = arguments.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length < 2)
            {
                log.WriteLine("Invalid number of arguments. The multiply command requires at least two values.");
                return;
            }

            double product = 1;

            StringBuilder result = new StringBuilder();

            foreach (var textValue in parts)
            {
                double numberValue;
                if (!double.TryParse(textValue, out numberValue))
                {
                    log.WriteLine(string.Format("'{0}' is not a number.", textValue));
                    return;
                }

                product *= numberValue;

                if (result.Length != 0)
                {
                    result.Append(" x ");
                }

                result.Append(textValue);
            }

            result.Append(" = ");
            result.Append(product);

            
            log.WriteLine(result.ToString());            
        }
    }
}
