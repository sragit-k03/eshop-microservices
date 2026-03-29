using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuildingBlocks.CQRS
{
    public interface ICommand: ICommand<Unit>{}
    public interface ICommand<TResponse>: IRequest<TResponse> { }
 
}
