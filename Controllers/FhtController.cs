using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApiFHT.Entities;
using WebApiFHT.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApiFHT.Services;

namespace WebApiFHT.Controllers
{
    [Route("api/fht")]
    [ApiController]
    public class FhtController : ControllerBase
    {
        private readonly FHTDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IWebApiFHTService _webApiFHTService;
        public FhtController(FHTDbContext dbContext, IMapper mapper,IWebApiFHTService webApiFHTService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _webApiFHTService = webApiFHTService;   
        }
        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            _webApiFHTService.Delete(id);

            return NoContent();
        }

        [HttpPost]

        public ActionResult CreateCompany([FromBody] CreateCompanyDto dto)
        {
            var id =   _webApiFHTService.Create(dto);

            return Created($"/api/fht/{id}", null);
        }

        [HttpGet]
        public ActionResult<IEnumerable<FhtDto>> GetAll()
        {
           

            var CompaniesDto = _webApiFHTService.GetAll();
                
            return Ok(CompaniesDto);
        }
        [HttpGet("{id}")]
        public ActionResult<FhtDto> GetOne([FromRoute] int id)
        {
            var Companies = _webApiFHTService.GetById(id);

            return Ok(Companies);
        }

        [HttpPut("{id}")]
        public ActionResult Update([FromBody] UpdateComapanyDto dto, [FromRoute] int id)
        {
            
           _webApiFHTService.Update(id, dto);

            return Ok();

        }
    }

    
}
