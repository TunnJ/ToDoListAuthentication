using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ToDoListAuthentication.Models
{
    public class Status
    {
        public string StatusId { get; set; }
        public string StatusName { get; set; }
    }
    public class ToDo
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter a name")]
        [StringLength(20)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter a description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Please enter a sprint number")]
        public string SprintNumber { get; set; }

        [Required(ErrorMessage = "Please enter a point value")]
        [Range(1,5)]
        public string PointValue { get; set; }

        [Required(ErrorMessage = "Please enter a status")]
        public string StatusId { get; set; }
        public string StatusName { get; set; }
    }
}
