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
using System.Collections;

namespace TaskFlow.Application.WorkFlow.UserTask.Contracts
{
    public sealed class LeafTask : ITask, IPrototype
    {
        public ushort Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public TaskState Status { get; set; } = TaskState.InProgres;
        public DateTime CreatedAt { get; set; } = DateTime.Now.Date;
        public DateTime DueDate { get; set; }
        public ushort ParentTaskId { get; set; }
        public ushort SectionId { get; set; }

        public TaskState isCompleted()
        {
            return this.Status;
        }

        public IPrototype Clone()
        {
            Console.WriteLine("Copy is being made");
            LeafTask task = null;
            task = (LeafTask)this.MemberwiseClone();
            return task;
        }
    }
}
