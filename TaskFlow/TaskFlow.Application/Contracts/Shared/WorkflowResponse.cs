﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskFlow.Application.Contracts.Shared
{
    public record WorkflowResponse (object value) : IRequest<string>;
}
