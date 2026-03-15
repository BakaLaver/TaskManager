using EnSoftGroopTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnSoftGroopTest.Services.Abstraction
{
    public interface IFileService
    {
        Task<List<TaskItem>> LoadTasksAsync();
        Task SaveTasksAsync(List<TaskItem> tasks);
    }
}
