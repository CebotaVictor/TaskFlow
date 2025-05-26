using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.Application.WorkFlow.UserTask.Interface;
using TaskFlow.Domain.Enums;
using TaskFlow.Domain.Entities.Tasks;
using System.Reflection;
using TaskFlow.Application.WorkFlow.UserTask.Mapping;

namespace TaskFlow.Application.WorkFlow.UserTask.Contracts
{
    public sealed class TaskComposite : ITask
    {
        public ushort Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public TaskState Status { get; set; } = TaskState.InProgres;
        public DateTime CreatedAt { get; set; } = DateTime.Now.Date;
        public DateTime? DueDate { get; set; }
        public ushort? ParentTaskId { get; set; }
        public ushort ProjectId { get; set; }
        public List<ITask> ?Tasks { get; set; }

        public TaskState isCompleted()
        {
            if(Tasks.Count() == 0)
            {
                return this.Status;
            }
            return Tasks.All(task => task.isCompleted() == TaskState.Complete) ? TaskState.Complete : TaskState.InProgres;
        }
    }
}
