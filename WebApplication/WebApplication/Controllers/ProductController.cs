using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;
using System.Web.Helpers;
using WebApplication.Models;
using WebApplication.ViewModels;
using System.Net;
using System.IO;

namespace WebApplication.Controllers
{
    public class ProductController : Controller
    {
        //DbContext to Access Database..
        private ApplicationDbContext _context;

        //Init DbContext in Constructor
        public ProductController()
        {
            _context = new ApplicationDbContext();
        }

        //Properly Disposing Dbcontext[Disposable Object]
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }


        //GET /Product
        public ActionResult Index()
        {

            //Eager Loading (Loading product with Category)
            var product = _context.Products.Include(c => c.CategoryType).ToList();

            if (User.IsInRole("Admin"))
            {
                return View(product);
                
            }
            return View("ReadOnlyList", product);

        }

        // GET: Products/Details/Id
        public ActionResult Details(int Id)
        {
            var product = _context.Products.Include(c => c.CategoryType).SingleOrDefault(c => c.Id == Id);
            return View(product);
        }

        [HttpGet]
        [Authorize]
        [Authorize(Roles = RoleName.Admin)]
        //Get: Product/Create
        public ActionResult Create()
        {
            
            var viewModel = new ProductViewModel
            {
                Product = new Product(),
                CategoryTypes = _context.CategoryTypes.ToList()
            };

            return View(viewModel);
        }
                                
        [HttpPost]
        //POST Product/Save
        [Authorize(Roles = RoleName.Admin)]
        public ActionResult Create(Product product,HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {


                // Declare product id
                int Id;

                using (ApplicationDbContext applicationDb = new ApplicationDbContext())
                {
                    _context.Products.Add(product);

                    //save
                    _context.SaveChanges();

                    // Get the id
                    Id = product.Id;


                }

                #region Upload Image

                // Create necessary directories
                var originalDirectory = new DirectoryInfo(string.Format("{0}Images\\Uploads", Server.MapPath(@"\")));

                var pathString1 = Path.Combine(originalDirectory.ToString(), "Products");
                var pathString2 = Path.Combine(originalDirectory.ToString(), "Products\\" + Id.ToString());
                var pathString3 = Path.Combine(originalDirectory.ToString(), "Products\\" + Id.ToString() + "\\Thumbs");
                var pathString4 = Path.Combine(originalDirectory.ToString(), "Products\\" + Id.ToString() + "\\Gallery");
                var pathString5 = Path.Combine(originalDirectory.ToString(), "Products\\" + Id.ToString() + "\\Gallery\\Thumbs");

                if (!Directory.Exists(pathString1))
                    Directory.CreateDirectory(pathString1);

                if (!Directory.Exists(pathString2))
                    Directory.CreateDirectory(pathString2);

                if (!Directory.Exists(pathString3))
                    Directory.CreateDirectory(pathString3);

                if (!Directory.Exists(pathString4))
                    Directory.CreateDirectory(pathString4);

                if (!Directory.Exists(pathString5))
                    Directory.CreateDirectory(pathString5);

                // Check if a file was uploaded
                if (file != null && file.ContentLength > 0)
                {
                    // Get file extension
                    string ext = file.ContentType.ToLower();
                    

                    // Init image name
                    string imageName = file.FileName;


                    // Set original and thumb image paths
                    var path = string.Format("{0}\\{1}", pathString2, imageName);
                    var path2 = string.Format("{0}\\{1}", pathString3, imageName);

                    // Save image name to DTO

                    Product dto = _context.Products.Find(Id);
                    dto.ImagePath = "\\Images\\Uploads\\Products\\" + Id.ToString() + "\\" + file.FileName;

                    // Save original
                    file.SaveAs(path);


                    // Create and save thumb
                    WebImage img = new WebImage(file.InputStream);
                    img.Resize(200, 200);
                    img.Save(path2);
                }


                #endregion


                _context.SaveChanges();

                return RedirectToAction("Index", "Product");

            }

            var viewModel = new ProductViewModel
            {
                Product = new Product(),
                CategoryTypes = _context.CategoryTypes.ToList()

            };

                return View(viewModel);

        }


        [HttpPost]
        [Authorize]
        [Authorize(Roles = RoleName.Admin)]
        //POST Product/Save
        public ActionResult Edit(Product product,HttpPostedFileBase file)
        {
            // Declare product id
            int Id = 0;

            //Checking Form Validation to Save ..
            if (!ModelState.IsValid)
            {
                var viewModel = new ProductViewModel
                {

                    CategoryTypes = _context.CategoryTypes.ToList()

                };
                return View(viewModel);
            }

            ////Checking Product Not Exists in Db
            if (product.Id == 0)
            {    //Create product
                _context.Products.Add(product);
            }
            else
            {
                //updating Product in Db
                var productInDb = _context.Products.Single(c => c.Id == product.Id);

                //TryUpdateModel(productInDb);
                productInDb.Name = product.Name;
                productInDb.Description = product.Description;
                productInDb.Price = product.Price;
                productInDb.CategoryTypeId = product.CategoryTypeId;
                productInDb.ImagePath = product.ImagePath;



                //save
                _context.SaveChanges();

                // Get the id
                Id = product.Id;
            }

            #region Upload Image

            // Create necessary directories
            var originalDirectory = new DirectoryInfo(string.Format("{0}Images\\Uploads", Server.MapPath(@"\")));

            var pathString1 = Path.Combine(originalDirectory.ToString(), "Products");
            var pathString2 = Path.Combine(originalDirectory.ToString(), "Products\\" + Id.ToString());
            var pathString3 = Path.Combine(originalDirectory.ToString(), "Products\\" + Id.ToString() + "\\Thumbs");
            var pathString4 = Path.Combine(originalDirectory.ToString(), "Products\\" + Id.ToString() + "\\Gallery");
            var pathString5 = Path.Combine(originalDirectory.ToString(), "Products\\" + Id.ToString() + "\\Gallery\\Thumbs");

            if (!Directory.Exists(pathString1))
                Directory.CreateDirectory(pathString1);

            if (!Directory.Exists(pathString2))
                Directory.CreateDirectory(pathString2);

            if (!Directory.Exists(pathString3))
                Directory.CreateDirectory(pathString3);

            if (!Directory.Exists(pathString4))
                Directory.CreateDirectory(pathString4);

            if (!Directory.Exists(pathString5))
                Directory.CreateDirectory(pathString5);

            // Check if a file was uploaded
            if (file != null && file.ContentLength > 0)
            {
                // Get file extension
                string ext = file.ContentType.ToLower();


                // Init image name
                string imageName = file.FileName;


                // Set original and thumb image paths
                var path = string.Format("{0}\\{1}", pathString2, imageName);
                var path2 = string.Format("{0}\\{1}", pathString3, imageName);

                // Save image name to DTO

                Product dto = _context.Products.Find(Id);
                dto.ImagePath = "\\Images\\Uploads\\Products\\" + Id.ToString() + "\\" + file.FileName;

                // Save original
                file.SaveAs(path);


                // Create and save thumb
                WebImage img = new WebImage(file.InputStream);
                img.Resize(200, 200);
                img.Save(path2);
            }


            #endregion

            //save
            _context.SaveChanges();

            return RedirectToAction("Index", "Product");
        }

        [HttpGet]
        [Authorize]
        [Authorize(Roles = RoleName.Admin)]
        public ActionResult Edit(int Id)
        {
            var product = _context.Products.SingleOrDefault(c => c.Id == Id);

            if (product == null)
                return HttpNotFound();

            var viewModel = new ProductViewModel
            {
                Product = product,
                CategoryTypes = _context.CategoryTypes.ToList()

            };

            return View(viewModel);
        }

        
        [Authorize]
        [Authorize(Roles = RoleName.Admin)]
        // GET: NewProducts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            

            Product product = _context.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }


        // POST: NewProducts/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        [Authorize(Roles = RoleName.Admin)]
        public ActionResult Delete(int id)
        {
            Product product = _context.Products.Find(id);
            _context.Products.Remove(product);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }


    }
}