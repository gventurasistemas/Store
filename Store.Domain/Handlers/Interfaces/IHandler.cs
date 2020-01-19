using Store.Domain.Commands.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Domain.Handlers.Interfaces
{
    //Onde T só pode ser do tipo ICommand (where T : ICommand)
    public interface IHandler<T> where T : ICommand
    {
        ICommandResult Handle(T command);
    }
}
