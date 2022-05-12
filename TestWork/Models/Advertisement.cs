using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TestWork.Models
{
    public class Advertisement
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        [Required(ErrorMessage = "Field title is empty")]
        [StringLength(50, ErrorMessage = "More than 50 characters entered in a field Title")]
        [DisplayName("Title")]
        public string Title { get; set; }

        public string ImageName { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        [Required(ErrorMessage = "Field Company is empty")]
        [StringLength(50, ErrorMessage = "More than 50 characters entered in a field Subtitle")]
        [DisplayName("Company")]
        public string Company { get; set; }
        [Required(ErrorMessage = "Field TextNews is empty")]
        [DisplayName("TextNews")]
        [Column(TypeName = "nvarchar(250)")]
        [StringLength(250, ErrorMessage = "More than 250 characters entered in a field TextNews")]
        public string Text { get; set; }
        public int Visits { get; set; }
        public int Likes { get; set; }
        public int Dislikes { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }

    }
}
