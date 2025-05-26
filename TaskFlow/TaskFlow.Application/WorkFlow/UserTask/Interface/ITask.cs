using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskFlow.Application.WorkFlow.UserTask.Contracts;
using TaskFlow.Domain.Entities.Tasks;
using TaskFlow.Domain.Enums;

namespace TaskFlow.Application.WorkFlow.UserTask.Interface
{
    public interface ITask
    {
        TaskState isCompleted();
    }
}




/*
 * 
 * 
task - isComplete
|
V
[

task,...,
|
V
task
]
 */