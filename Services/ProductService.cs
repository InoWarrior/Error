using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiFHT.Entities;
using WebApiFHT.Models;
using WebApiFHT.Exceptions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;


namespace WebApiFHT.Services
{  
    public interface IProductService
    {
        int Create(int CompanyId, CreateProductDto dto);

        ProductDto GetById(int CompanyId, int productId);
        List<ProductDto> GetAll(int companyId);
        void RemoveAll(int comapnyId);
    }
    public class ProductService : IProductService
    {
        private readonly FHTDbContext _context;
        private readonly IMapper _mapper;

        public ProductService(FHTDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public int Create(int companyId, CreateProductDto dto)
        {
            var company = getComapnyById(companyId);
            var productEntity = _mapper.Map<Product>(dto);

            productEntity.CompanyId = companyId;
            _context.Products.Add(productEntity);
            _context.SaveChanges();

            return productEntity.Id;

        }

        public ProductDto GetById(int companyId, int productId)
        {
            var company = getComapnyById(companyId);

            var product = _context.Products.FirstOrDefault(d => d.Id == productId);
            if(product == null || product.CompanyId != companyId)
            {
                throw new NotFoundException("Product not found");
            }

            var productDto = _mapper.Map<ProductDto>(product);
            return productDto;
        }

        public List<ProductDto> GetAll(int companyId)
        {
            
            var company = getComapnyById(companyId);
            var productDto = _mapper.Map<List<ProductDto>>(company.Products);

            return productDto;
        }
        public void RemoveAll(int companyId)
        {
            var company = getComapnyById(companyId);

            _context.RemoveRange(company.Products);
            _context.SaveChanges();
        }

        private Company getComapnyById(int companyId)
        {
            var company = _context.
                Company.
                Include(r => r.Products).
                FirstOrDefault(r => r.Id == companyId);
            if (company == null)
            {
                throw new NotFoundException("Company not found");
            }

            return company;
        }
        
    }
}
