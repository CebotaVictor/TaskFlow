using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.Application.WorkFlow.UserTask.Contracts;
using TaskFlow.Domain.Entities.Tasks;

namespace TaskFlow.Application.Mapping
{
    public static class TaskCompositeToUTaskMapperExtension
    {
        public static UTask ToUTask(this TaskComposite Task)
        {
            var toUTask = new UTask
            {
                Description = Task.Description,
                Title = Task.Title,
                Status = Task.Status,
                CreatedAt = Task.CreatedAt,
                DueDate = Task.DueDate,
                ParentTaskId = Task.ParentTaskId,
                SectionId = Task.SectionId
            };

            foreach(var subTask in Task.Tasks!)
            {
                if(subTask is LeafTask subLeaf)
                {
                    toUTask.Tasks.Add(subLeaf.ToUTask());
                }
                else if(subTask is TaskComposite subComposite)
                {
                    toUTask.Tasks.Add(subComposite.ToUTask());
                }
            }

            return toUTask;
        }

        public static UTask ToUTask(this LeafTask Task)
        {
            var toUTask = new UTask
            {
                Description = Task.Description,
                Title = Task.Title,
                Status = Task.Status,
                CreatedAt = Task.CreatedAt,
                DueDate = Task.DueDate,
                ParentTaskId = Task.ParentTaskId,
                SectionId = Task.SectionId
            };
            return toUTask;
        }
    }
}
