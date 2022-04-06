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
    public class News
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        [Required(ErrorMessage = " Поле Заголовок пустое")]
        [StringLength(50, ErrorMessage = "Введено больше 50 символов в поле Заголовок")]
        [DisplayName("Заголовок")]
        public string Title { get; set; }
        public string ImageName { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        [Required(ErrorMessage = " Поле ПОдзаголовок пустое")]
        [StringLength(50, ErrorMessage = "Введено больше 50 символов в поле Подзаголовок")]
        [DisplayName("Подзаголовок")]
        public string SubTitle { get; set; }
        [Required(ErrorMessage = "Поле Текст новости пустое")]
        [DisplayName("Текст новости")]
        [Column(TypeName = "nvarchar(250)")]
        [StringLength(250, ErrorMessage = "Введено больше 250 символов в поле Текст")]
        public string Text { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }

    }
}
