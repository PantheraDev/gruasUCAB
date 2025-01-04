using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace JobMs.Common.Primitives
{
    //TODO: Aqui utiliza un record, revisar el porque
    public record DomainEvent(Guid id) : INotification;
}