using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Core.Models
{
    public class Product
    {
        public string Id { get; set; }

        [StringLength(150)]
        [DisplayName("Navn")]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Category { get; set; }
        [Range(0, 10000)]
        public decimal Price { get; set; }

        public Product()
        {
            this.Id = Guid.NewGuid().ToString();
        }

    }
}
