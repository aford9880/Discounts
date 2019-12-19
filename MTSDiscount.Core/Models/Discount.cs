using System;
using System.ComponentModel.DataAnnotations;

namespace MTSDiscount.Core.Models {
    public class Discount {
        [Required]
        public int ID { get; set; }        

        [StringLength(60, MinimumLength = 3), Required(ErrorMessage = "Необходимо ввести название бонуса")]
        [Display(Name = "Название")]
        public string Title { get; set; }

        // Первая буква должна быть прописной; пробелы, цифры и специальные символы не допускаются
        [RegularExpression(@"^[A-ZА-Я]+[a-zA-ZА-Яа-я""'\s-]*$"), StringLength(60, MinimumLength = 3)]
        [Required(ErrorMessage = "Необходимо ввести категорию"), Display(Name = "Категория")]
        public string Category { get; set; }

        // Дата и время добавления будут задаваться автоматом
        [Display(Name = "Добавлено"), DataType(DataType.DateTime)]
        public DateTime DateAdd { get; set; }

        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Display(Name = "Изображение")]
        public byte[] Image { get; set; }

        public Discount() {
            DateAdd = DateTime.Now;
        }
    }
}
