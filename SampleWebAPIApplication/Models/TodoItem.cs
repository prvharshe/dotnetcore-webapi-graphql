using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleWebAPIApplication.Models
{
    public class TodoItem
    {
        public long Id { get; set; }
        public string Description { get; set; }
        public bool IsComplete { get; set; }
    }
}
