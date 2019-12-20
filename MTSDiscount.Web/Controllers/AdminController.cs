using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MTSDiscount.Core.Interfaces;
using MTSDiscount.Core.Models;
using MTSDiscount.Web.Models;
using System;
using System.Linq;

namespace MTSDiscount.Web.Controllers {
    public class AdminController : Controller {

        private IDiscountRepository _discountRepository;
        public int pageSize = 3;

        public AdminController(IDiscountRepository discountRepository) {
            _discountRepository = discountRepository;
        }

        // GET: Admin
        [Route("Admin")]
        [Route("Admin/page{page}")]
        public ViewResult List(int page = 1) {

            var discounts = _discountRepository.GetDiscounts;
      
            var count = discounts.Count();
            var items = discounts.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            ListViewModel viewModel = new ListViewModel {
                PageViewModel = pageViewModel,
                Discounts = items
            };
            ViewBag.Current = "Admin";
            return View(viewModel);
        }

        // GET: Admin/Details/5
        public IActionResult Details(int? id) {
            if (id == null) {
                return NotFound();
            }

            var discount = _discountRepository.GetDiscountById(id.Value);
                
            if (discount == null) {
                return NotFound();
            }

            return View(discount);
        }

        // GET: Admin/Create
        public IActionResult Create() {
            return View();
        }

        // POST: Admin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("ID,Title,Category,DateAdd,Description,Image")] Discount discount) {
            if (ModelState.IsValid) {
                _discountRepository.InsertDiscount(discount);
                _discountRepository.Save();
                return RedirectToAction("Index");
            }
            return View(discount);
        }

        // GET: Admin/Edit/5
        public IActionResult Edit(int? id) {
            if (id == null) {
                return NotFound();
            }

            var discount = _discountRepository.GetDiscountById(id.Value);
            
            if (discount == null) {
                return NotFound();
            }
            return View(discount);
        }

        // POST: Admin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("ID,Title,Category,DateAdd,Description,Image")] Discount discount) {
            if (id != discount.ID) {
                return NotFound();
            }

            if (ModelState.IsValid) {
                try {
                    _discountRepository.UpdateDiscount(discount);
                    _discountRepository.Save();
                }
                catch (DbUpdateConcurrencyException) {
                    if (_discountRepository.GetDiscountById(discount.ID) == null) {
                        return NotFound();
                    }
                    else {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(discount);
        }

        // GET: Admin/Delete/5
        public IActionResult Delete(int? id) {
            if (id == null) {
                return NotFound();
            }

            var discount = _discountRepository.GetDiscountById(id.Value);
            
            if (discount == null) {
                return NotFound();
            }

            return View(discount);
        }

        // POST: Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id) {           
            _discountRepository.DeleteDiscount(id);
            _discountRepository.Save();
            return RedirectToAction("Index");
        }
    }
}
