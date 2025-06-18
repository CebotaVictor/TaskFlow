using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.Domain.Entities.Tasks;

namespace TaskFlow.Application.Interfaces.Repository
{
    public interface ITaskRepository
    {
        Task<IEnumerable<UTask>> GetAllTasks();
    }
}
