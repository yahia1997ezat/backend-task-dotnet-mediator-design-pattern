using System;
using System.ComponentModel.DataAnnotations;

namespace TaskApi.Models.v1
{
    public class TaskItemModel
    {
        [Required] public string Title { get; set; }
        [Required]   public string Description { get; set; }
    }
}