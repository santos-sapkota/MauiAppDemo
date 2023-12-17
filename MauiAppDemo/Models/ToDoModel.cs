using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiAppDemo.Models
{
    internal class ToDoModel
    {
        public int ID { get; set; }
        public string? Task { get; set; }
        public bool IsComplete { get; set; }
    }
}
