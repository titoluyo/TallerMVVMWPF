using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CmdletConsole.Model
{
    public interface ICommandLog
    {
        void WriteLine(string message);
    }
}
