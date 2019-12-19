using System;

namespace MTSDiscount.Web.Models {
    public class PageViewModel {
        public int PageNumber { get; private set; } // Текущий номер страницы
        public int TotalPages { get; private set; } // Всего страниц
        public PageViewModel(int count, int pageNumber, int pageSize) {
            PageNumber = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize); // Всего сущностей/Размер страницы, получаем кол-во страниц
        }

        public bool HasPreviousPage {
            get {
                return (PageNumber > 1); // есть ли предыдущая страница
            }
        }

        public bool HasNextPage {
            get {
                return (PageNumber < TotalPages); // есть ли следующая страница
            }
        }
    }
}
