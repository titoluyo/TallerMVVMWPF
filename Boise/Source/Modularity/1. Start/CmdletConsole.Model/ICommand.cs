using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CmdletConsole.Model
{
    //TODO: 01. The command interface

    public interface ICommand
    {             
        void Execute(string arguments, ICommandLog log);
    }
}
