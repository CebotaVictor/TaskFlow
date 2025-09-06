using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.Domain.Entities.WSections;
using TaskFlow.Domain.Entities.Tasks;

namespace TaskFlow.Application.Interfaces.Repository
{
    public interface ISectionRepository
    {
        Task<IEnumerable<Section>> GetAllSectionsAsync();
        Task<bool> DeleteSectionByIdAsync(ushort Id);
    }
}
