using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MyShop.Core.Models;
using MyShop.DataAccess.InMemory;

namespace MyShop.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {
        //Vi skal have en adgang til vores models
        ProductRepository context;

        //her skal jeg lave en constructor der aktivere adgangen til min models
        public ProductManagerController()
        {
            context = new ProductRepository();
        }
        
        // GET: ProductManager
        public ActionResult Index()
        {
            //Vi skal have index til at vise en list med produkter
            List<Product> products = context.collection().ToList();


            //Så sender vi produkter til index
            return View(products);
        }

        //Metode til at oprette et produkt
        //den her er kun til at vise siden create
        public ActionResult Create()
        {
            //Her skal vi have en instance til product model
            Product product = new Product();
            return View(product);
        }


        [HttpPost]
        public ActionResult Create(Product product)
        {
            //Først skal vi tjekke om siden er valid
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            else
            {
                //Her skal vi insætte produkter i samlingen af produkter
                context.insert(product);

                //Dernæst skal det gemmes
                context.Commit();

                //Til sidst hvis alt er ok så skal vi sende brugeren tilbage til index siden
                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(string Id)
        {
            //Først skal vi have fat i produkterne med det rigtige id
            Product product = context.Find(Id);

            //Hvis der ikke er nogen produkter så skal vi vise en fejl 
            //Ellers skal vi retunere brugeren til den rigtige side
            if (product == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(product);
            }
        }

        [HttpPost]
        public ActionResult Edit(Product product, string Id)
        {
            //Først skal vi have adgang til product models
            Product productToEdit = context.Find(Id);

            //Hvis der ikke er nogen produkter så skal vi vise en fejl 
            //Ellers skal vi retunere brugeren til den rigtige side
            if (productToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                //Hvis den ikke er der skal vi tjekke modelstate
                if (!ModelState.IsValid)
                {
                    return View(product);
                }
                //hvis alting er som det skal være så skal vi manuelt opdatere product
                productToEdit.Name = product.Name;
                productToEdit.Description = product.Description;
                productToEdit.Category = product.Category;
                productToEdit.Price = product.Price;
                productToEdit.Image = product.Image;

                //Så skal det hele gemmes             
                context.Commit();

                //Send brugeren tilbage til index siden
                return RedirectToAction("Index");
            }
        }


        public ActionResult Delete(string Id)
        {
            //Først skal jeg have adgang til produkt modellen
            Product productToDelete = context.Find(Id);

            //Så skal jeg tjekke om der er noget med edt id
            if (productToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(productToDelete);
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string id)
        {
            //Finde det rigtige id der skal slettes
            Product productToDelete = context.Find(id);


            //Så skal vi tjekke inde vi sletter at vi har det rigtige produkt
            if (productToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                context.DELETE(id);
                context.Commit();
                return RedirectToAction("Index");
            }

        }


    }
}