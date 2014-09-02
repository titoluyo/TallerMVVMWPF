using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CmdletConsole.Model;

namespace CmdletConsole.Math
{
    public class DivideCommand : ICommand
    {
        public void Execute(string arguments, ICommandLog log)
        {
            string[] parts = arguments.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length < 2)
            {
                log.WriteLine("Invalid number of arguments. The divide command requires at least two values.");
                return;
            }

            bool isFirstValue = true;
            double quotient = 0;

            StringBuilder result = new StringBuilder();

            foreach (var textValue in parts)
            {
                double numberValue;
                if (!double.TryParse(textValue, out numberValue))
                {
                    log.WriteLine(string.Format("'{0}' is not a number.", textValue));
                    return;
                }

                if (isFirstValue)
                {
                    quotient = numberValue;
                    isFirstValue = false;
                }
                else
                {
                    quotient /= numberValue;
                }

                if (result.Length != 0)
                {
                    result.Append(" / ");
                }

                result.Append(textValue);
            }

            result.Append(" = ");
            result.Append(quotient);

            
            log.WriteLine(result.ToString());            
        }
    }
}
