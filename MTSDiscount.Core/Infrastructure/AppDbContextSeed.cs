using MTSDiscount.Core.Models;
using System;
using System.Linq;

namespace MTSDiscount.Core.Infrastructure {
    public class AppDbContextSeed {               
        public static void Initial(AppDbContext context) {            
            if (!context.Discounts.Any()) {             
                context.AddRange(
                    
                    new Discount {
                        Title = "Магазин цветов \"Розочка\"",
                        Category = "Разное",
                        DateAdd = DateTime.Now,
                        Description = "Скидка 10% при предьявлении пропуска. Адреса магазинов: бла бла бла..."
                    },

                    new Discount {
                        Title = "Магазин косметики \"Душок\"",
                        Category = "Разное",
                        DateAdd = DateTime.Now,
                        Description = "Скидка 15% при предьявлении пропуска. Адреса магазинов: бла бла бла..."
                    },

                    new Discount {
                        Title = "Кафе \"Вкусняха\"",
                        Category = "Еда",
                        DateAdd = DateTime.Now,
                        Description = "Скидка 5% при предьявлении пропуска. Адрес: бла бла бла..."
                    },

                    new Discount {
                        Title = "Кафе \"Большой бургер\"",
                        Category = "Еда",
                        DateAdd = DateTime.Now,
                        Description = "Скидка 10% при предьявлении пропуска. Адреса: бла бла бла..."
                    },

                    new Discount {
                        Title = "Пейнтбол \"Постреляйка\"",
                        Category = "Развлечения",
                        DateAdd = DateTime.Now,
                        Description = "100 пулек и каска в подарок. Адрес: бла бла бла..."
                    }
                );                
                context.SaveChanges();
            }                
        }
    }
}
