using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreatorUI.Command
{
    class Invoker
    {
        ICommand command;

        public Invoker() { }

        public void SetCommand(ICommand command)
        {
            this.command = command;
        }

        public void Execute()
        {
            command.Redo();
        }

        public void Unexecute()
        {
            command.Undo();
        }
    }
}
