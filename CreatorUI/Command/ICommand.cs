using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreatorUI.Command
{
    interface ICommand
    {
        void Redo();
        void Undo();
    }
}
