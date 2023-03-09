using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Mission9_Water.Infrastructure;
using Mission9_Water.Models;

namespace Mission9_Water.Pages
{
    public class BuyModel : PageModel
    {
            private IBookstoreRepository repo { get; set; }
            public BuyModel (IBookstoreRepository temp)
            {
                repo = temp; 
            }
            public Cart cart { get; set; }
            public string ReturnUrl { get; set; }
        public void OnGet(string returnUrl)
        {
            ReturnUrl = returnUrl ?? "/";
            cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
        }
        public IActionResult OnPost(int bookid, string returnUrl)
        {
            Book b = repo.Books.FirstOrDefault(x => x.BookId == bookid);

            cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();

            cart.AddItem(b, 1);

            HttpContext.Session.SetJson("cart", cart);

            return RedirectToPage(new { ReturnUrl = returnUrl});

        }
    }
}
