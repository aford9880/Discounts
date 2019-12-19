using Microsoft.AspNetCore.Mvc;
using MTSDiscount.Core.Interfaces;
using System.Linq;
using MTSDiscount.Web.Models;

namespace MTSDiscount.Web.Controllers {
    public class DiscountsController : Controller {
        
        private readonly IDiscountRepository _discountRepository;
        public int pageSize = 3;

        public DiscountsController(IDiscountRepository discountRepository) {
            _discountRepository = discountRepository;
        }

        // GET: Discounts        
        [Route("")]
        [Route("page{page}")]        
        public ViewResult List(int page = 1) {
           
            var discounts = _discountRepository.GetDiscounts;
            var count = discounts.Count();
            var items = discounts.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            ListViewModel viewModel = new ListViewModel {
                PageViewModel = pageViewModel,
                Discounts = items
            };
            return View(viewModel);
        }

        // GET: Discounts/Details/5
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
    }
}
