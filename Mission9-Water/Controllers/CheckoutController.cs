using Microsoft.AspNetCore.Mvc;
using Mission9_Water.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mission9_Water.Controllers
{
    public class CheckoutController : Controller
    {
        private ICheckoutRepository Repo { get; set; }
        private Cart Cart { get; set; }
        public CheckoutController (ICheckoutRepository temp, Cart c)
        {
            Repo = temp;
            Cart = c; 
        }
        [HttpGet]
        public IActionResult Checkout()
        {
            return View(new Checkout());
        }
        [HttpPost]
        public IActionResult Checkout(Checkout checkout)
        {
            if (Cart.Items.Count() == 0)
            {
                ModelState.AddModelError("", "Sorry your cart is empty!");
            }
            if (ModelState.IsValid)
            {
                checkout.Lines = Cart.Items.ToArray();
                Repo.SaveCheckout(checkout);
                Cart.ClearCart();
                return RedirectToPage("/OrderCompleted"); 
            }
            else
            {
                return View();
            }
        }
    }
}
