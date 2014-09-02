using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CmdletConsole.Model
{
    public class NoOpCommandLog : ICommandLog
    {
        public void WriteLine(string message)
        {            
        }
    }
}
