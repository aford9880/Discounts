using Microsoft.EntityFrameworkCore;
using MTSDiscount.Core.Infrastructure;
using MTSDiscount.Core.Interfaces;
using MTSDiscount.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MTSDiscount.Core.Repository {
    public class DiscountRepository : IDiscountRepository {

        private AppDbContext _context;

        public DiscountRepository(AppDbContext context) {
            _context = context;
        }

        public IEnumerable<Discount> GetDiscounts => _context.Discounts.ToList();

        public Discount GetDiscountById(int discountId) => _context.Discounts.Find(discountId);
        
        public void InsertDiscount(Discount discount) => _context.Discounts.Add(discount);        

        public void DeleteDiscount(int discountId) {
            Discount discount = _context.Discounts.Find(discountId);
            _context.Discounts.Remove(discount);
        }

        public void UpdateDiscount(Discount discount) => _context.Entry(discount).State = EntityState.Modified;        

        public void Save() => _context.SaveChanges();
        
        public Boolean IsEmpty() => !(_context.Discounts.Any());
        
        private bool disposed = false;

        protected virtual void Dispose(bool disposing) {
            if (!this.disposed) {
                if (disposing) {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
