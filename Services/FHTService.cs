using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiFHT.Models;
using WebApiFHT.Entities;
using WebApiFHT.Controllers;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using WebApiFHT.Exceptions;



namespace WebApiFHT.Services
{

    public interface IWebApiFHTService
    {
        FhtDto GetById(int id);
        IEnumerable<FhtDto> GetAll();
        int Create(CreateCompanyDto dto);
        void Delete(int id);

        void Update(int id, UpdateComapanyDto dto);
    }
    public class FHTService : IWebApiFHTService
    {

        private readonly FHTDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public FHTService(FHTDbContext dbContext, IMapper mapper, ILogger<FHTService> logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
        }
        public FhtDto GetById(int id)
        {
            var Companies = _dbContext
                .Company
                .Include(r => r.Address)
                .Include(r => r.Products)
                .FirstOrDefault(r => r.Id == id);

            if (Companies == null) 
                throw new NotFoundException("Company Not Found");

            var result = _mapper.Map<FhtDto>(Companies);
            return result;

        }

        public IEnumerable<FhtDto> GetAll()
        {
            var Companies = _dbContext
                .Company
                .Include(r => r.Address)
                .Include(r => r.Products)
                .ToList();

            var CompaniesDto = _mapper.Map<List<FhtDto>>(Companies);

            return CompaniesDto;
        }

        public int Create(CreateCompanyDto dto)
        {
            var companies = _mapper.Map<Company>(dto);
            _dbContext.Company.Add(companies);
            _dbContext.SaveChanges();

            return companies.Id;
        }
        public void Delete(int id)
        {

            _logger.LogError($"Restaurant with id: {id} Delete Action Invoke");

            var Companies = _dbContext
                .Company
                .FirstOrDefault(r => r.Id == id);

            if (Companies == null) 
                throw new NotFoundException("Company Not Found");

            _dbContext.Company.Remove(Companies);
            _dbContext.SaveChanges();
        }
        public void Update(int id, UpdateComapanyDto dto)
        {
            var Companies = _dbContext
                .Company
                .FirstOrDefault(r => r.Id == id);

            if (Companies is null) 
                throw new NotFoundException("Company Not Found");

            Companies.Name = dto.Name;
            Companies.Description = dto.Description;
            Companies.HasDelivery = dto.HasDelivery;

            _dbContext.SaveChanges();



        }
    }
}
