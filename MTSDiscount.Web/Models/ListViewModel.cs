using MTSDiscount.Core.Models;
using System.Collections.Generic;

namespace MTSDiscount.Web.Models {
    public class ListViewModel {
        public IEnumerable<Discount> Discounts { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}
