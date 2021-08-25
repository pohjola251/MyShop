using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using MyShop.Core.Models;

namespace MyShop.DataAccess.InMemory
{
    public class ProductRepository
    {
        //Jeg skal først oprette cachen
        ObjectCache cache = MemoryCache.Default;

        //Så skal jeg lave en forbindelse til mine models
        List<Product> products;

        //Så skal jeg lave mig en constructor
        public ProductRepository()
        {
            //herinde skal jeg aktivere forbindelserne jeg har lavet ovenover
            //Jeg starter med at lave en querystring så jeg kan få fat i det given product i cachen
            products = cache["products"] as List<Product>;

            //Så skal jeg tjekke om der findes noget i cachen 
            if (products == null)
            {
                //Hvis produkterne i cachen er tom så sksal den oprettes
                products = new List<Product>();
            }
        }

        //Gem metode i cachen
        public void Commit()
        {
            //Her skal det som er i cachen gemmes i cachen
            cache["products"] = products;
        }

        public void insert(Product p)
        {
            //Her indsætter jeg produktet i cachen
            products.Add(p);
        }

        public void update(Product product)
        {
            //her skal jeg først finde det rigtige id på produktet
            Product productToUpdate = products.Find(p => p.Id == product.Id);

            //Her sakl jeg tjekke om der findes noget i det id jeg har fundet
            if (productToUpdate != null)
            {
                //Så skal productToUpdate tilføjes i listen produkter
                productToUpdate = product;
            }
            else
            {
                throw new Exception("Der blev ikke fundet nogen produkter");
            }
        }

        public Product Find(string ID)
        {
            //Her skal jeg finde det rigtige id på det jeg vil slette
            Product product = products.Find(p => p.Id == ID);

            //Her skal jeg så tjekke om det id er tomt eller ej
            if (product != null)
            {
                //Hvis den ikke er tom så skal den returnere produktet så det kan ses
                return product;
            }
            else
            {
                throw new Exception("der blev ikke fundet noget produkt");
            }
        }

        //Her skal Product laves til en queryable string
        public IQueryable<Product> collection()
        {
            //Her skal vi så returnere produktet når der efterspørges på det
            return products.AsQueryable();
        }

        public void DELETE(string id)
        {
            //Her skal jeg findet der rigtige id at slette
            Product productToDelete = products.Find(p => p.Id == id);

            //Så skal der tjekkes om id er tom
            if (productToDelete != null)
            {
                products.Remove(productToDelete);
            }
            else
            {
                throw new Exception("Der blev ikke fundet noget produkt");
            }
        }
    }
}
