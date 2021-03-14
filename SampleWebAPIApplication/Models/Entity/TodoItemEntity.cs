using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SampleWebAPIApplication.Models.Entity
{
    public class TodoItemEntity
    {
        [Key]
        public int Id { get; set; }
        public string Description { get; set; }
        public bool IsComplete { get; set; }
    }
}
