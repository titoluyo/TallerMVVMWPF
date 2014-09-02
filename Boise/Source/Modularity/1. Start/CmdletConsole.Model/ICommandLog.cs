using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CmdletConsole.Model
{
    //TODO: 02. The command output mechanism

    public interface ICommandLog
    {
        void WriteLine(string message);
    }
}
