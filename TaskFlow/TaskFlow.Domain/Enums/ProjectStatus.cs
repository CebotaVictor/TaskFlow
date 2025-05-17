using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskFlow.Domain.Enums
{
    public enum ProjectStatus : ushort
    {
        Active = 0,
        Completed,
        OnHold
    }
}
