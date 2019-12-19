using MTSDiscount.Core.Models;
using System;
using System.Collections.Generic;

namespace MTSDiscount.Core.Interfaces {
    public interface IDiscountRepository : IDisposable {
        IEnumerable<Discount> GetDiscounts { get; }
        Discount GetDiscountById(int discountId);
        void InsertDiscount(Discount discount);
        void DeleteDiscount(int discountId);
        void UpdateDiscount(Discount discount);
        Boolean IsEmpty();
        void Save();
    }
}
