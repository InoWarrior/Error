using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WebApiFHT.Models
{
    public class CreateProductDto
    {
        [Required]
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        public bool IsAvailable { get; set; }
        public int CompanyId { get; set; }
    }
}
