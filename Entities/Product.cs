using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiFHT.Entities
{
    public class Product
    {

        public int Id { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        public bool IsAvailable { get; set; }
        public int CompanyId { get; set; }

        public virtual Company Company { get; set; }
    }
}
