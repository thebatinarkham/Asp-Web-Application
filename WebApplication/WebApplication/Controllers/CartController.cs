using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Services;
using System.Web.Services;
using WebApplication.Models;
using WebApplication.ViewModels;
using Microsoft.AspNet.Identity;
using System.Net.Mail;
using System.Net;

namespace WebApplication.Controllers
{
    public class CartController : Controller
    {

        //DbContext to Access Database..
        private ApplicationDbContext _context;

        //Init DbContext in Constructor
        public CartController()
        {
            _context = new ApplicationDbContext();
        }

        //Properly Disposing Dbcontext[Disposable Object]
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Cart
        public ActionResult Index()
        {
            //Init the list
            var cart = Session["cart"] as List<Cart> ?? new List<Cart>();

            //Check if cart is empty
            if (cart.Count == 0 || Session["cart"] == null)
            {
                ViewBag.Message = "Your cart is Empty";
                return View();
            }
            //Calculate total and save to ViewBag
            decimal total = 0m;

            foreach (var item in cart)
            {
                total += item.Total;
            }

            ViewBag.GrandTotal = total;



            //Return View with list
            return View(cart);
        }


        public ActionResult CartPartial()
        {
            //Init Cart
            Cart model = new Cart();

            //Init Quantity
            int qty = 0;

            //Init Price
            decimal price = 0m;

            //check for cart session
            if (Session["cart"] != null)
            {
                //Get total qty and price
                var list = (List<Cart>)Session["cart"];


                foreach (var item in list)
                {
                    qty += item.Quantity;
                    price += item.Quantity * item.Price;
                }

                model.Quantity = qty;
                model.Price = price;
            }
            else
            {
                //or set qty and price to 0
                model.Quantity = 0;
                model.Price = 0m;
            }





            //Return patrial view with model


            return PartialView(model);
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = true)]
        public ActionResult AddToCartPartial(int id)
        {
            // Init CartVM list
            List<Cart> cart = Session["cart"] as List<Cart> ?? new List<Cart>();

            // Init CartVM
            Cart model = new Cart();

            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                // Get the product
                Product product = db.Products.Find(id);

                // Check if the product is already in cart
                var productInCart = cart.FirstOrDefault(x => x.ProductId == id);

                // If not, add new
                if (productInCart == null)
                {
                    cart.Add(new Cart()
                    {
                        ProductId = product.Id,
                        ProductName = product.Name,
                        ProductDescription = product.Description,
                        Quantity = 1,
                        Price = product.Price,
                        Image = product.ImagePath
                    });
                }
                else
                {
                    // If it is, increment
                    productInCart.Quantity++;
                }
            }

            // Get total qty and price and add to model

            int qty = 0;
            decimal price = 0m;

            foreach (var item in cart)
            {
                qty += item.Quantity;
                price += item.Quantity * item.Price;
            }

            model.Quantity = qty;
            model.Price = price;

            // Save cart back to session
            Session["cart"] = cart;


            _context.SaveChanges();
            // Return partial view with model
            return PartialView(model);
        }

        //GET : /Cart/IncrementProduct
        public JsonResult IncrementProduct(int productId)
        {
            //Init cart List
            List<Cart> cart = Session["cart"] as List<Cart>;


            using (ApplicationDbContext applicationDb = new ApplicationDbContext())
            {
                //Get cart from List
                Cart model = cart.FirstOrDefault(x => x.ProductId == productId);

                //Increment qty
                model.Quantity++;

                //store needed data
                var result = new { qty = model.Quantity, price = model.Price };

                //return json with data
                return Json(result, JsonRequestBehavior.AllowGet);

            };


        }

        //GET : /Cart/DecrementProduct
        public JsonResult DecrementProduct(int productId)
        {
            //Init cart
            List<Cart> cart = Session["cart"] as List<Cart>;

            //Get model from list
            using (ApplicationDbContext applicationDb = new ApplicationDbContext())
            {
                //Get cart from List
                Cart model = cart.FirstOrDefault(x => x.ProductId == productId);

                //decrement qty
                if (model.Quantity > 1)
                {
                    model.Quantity--;
                }
                else
                {
                    model.Quantity = 0;
                    cart.Remove(model);
                }

                //store needed data
                var result = new { qty = model.Quantity, price = model.Price };

                //return json with data
                return Json(result, JsonRequestBehavior.AllowGet);

            };


        }

        //GET : /Cart/RemoveProduct
        public void RemoveProduct(int productId)
        {
            //Init cart
            List<Cart> cart = Session["cart"] as List<Cart>;

            //Get model from list
            using (ApplicationDbContext applicationDb = new ApplicationDbContext())
            {
                //Get cart from List
                Cart model = cart.FirstOrDefault(x => x.ProductId == productId);

                cart.Remove(model);

            };
        }


        public ActionResult CartDemo()
        {
            //var product = _context.Products.Include(c => c.CategoryType).SingleOrDefault(c => c.Id == Id);




            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                // Get the product


                // Check if the product is already in cart
                //    var productInCart = _context.Carts.FirstOrDefault(x => x.ProductId == product.Id);

                //    // If not, add new
                //    if (productInCart == null)
                //    {
                //        _context.Carts.Add(new Cart()
                //        {
                //            ProductId = product.Id,
                //            ProductName = product.Name,
                //            ProductDescription = product.Description,
                //            Quantity = 1,
                //            Price = product.Price,
                //            User = User.Identity.GetUserId(),
                //            Image = product.ImagePath

                //        });
                //    }
                //    else
                //    {
                //        // If it is, increment
                //        productInCart.Quantity++;
                //    }
                //}




                //// Save cart back to session
                //_context.SaveChanges();
                return View();
            }


        }


        public ActionResult PaypalPartial()
        {
            //Init cart List
            List<Cart> cart = Session["cart"] as List<Cart>;
            return PartialView(cart);
        }

        //POST : /Cart/PlaceOrder
        [HttpPost]
        public void PlaceOrder()
        {
            //Get cart List
            //Init cart List
            List<Cart> cart = Session["cart"] as List<Cart>;

            //Get UserId
            string userId = User.Identity.GetUserId();

            int orderId = 0;


            //Init Order
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                //Init Order
                Order order = new Order();

                //Get UserId

                //Add To order and save
                //order.UserId = username;
                order.CreatedAt = DateTime.Now;


                context.Orders.Add(order);

                _context.SaveChanges();

                //Get InsertedId
                orderId = order.Id ;




                //Init Orderdetails
                OrderDetails orderDetails = new OrderDetails();

                //Add To OrderDetails
                foreach(var item in cart)
                {
                    orderDetails.OrderId = orderId;
                    orderDetails.UserId = userId;
                    orderDetails.ProductId = item.ProductId;
                    orderDetails.Quantity = item.Quantity;

                    context.OrderDetails.Add(orderDetails);

                    context.SaveChanges();
                }



            };



            //Email Admin
            var client = new SmtpClient("smtp.mailtrap.io", 2525)
            {
                Credentials = new NetworkCredential("67e6b2a4d4b46d", "194ee3f85e1f91"),
                EnableSsl = true
            };
            client.Send("admin@example.com", "admin@example.com", "New Order", "You Have New Order..." + orderId);

            //Reset Session
            Session["cart"] = null;


        }
    }
}

