using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskFlow.Application.WorkFlow.UserTask.Interface
{
    public interface IPrototype
    {
        public IPrototype Clone();
    }
}
