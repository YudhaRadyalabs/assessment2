using api2;
using api2.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace api1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductivityController : ControllerBase
    {
        private readonly ILogger<ProductivityController> _logger;
        private readonly AppDbContext _appDbContext;
        public ProductivityController(ILogger<ProductivityController> logger, AppDbContext appDbContext)
        {
            _logger = logger;
            _appDbContext = appDbContext;
        }

        [HttpPost]
        public async Task<ActionResult> Create(ProductivityEntity model)
        {
            try
            {
                _appDbContext.Set<ProductivityEntity>().Add(model);
                
                await _appDbContext.SaveChangesAsync();

                _logger.LogInformation($"Create Productivity {model.Name}");

                return new OkObjectResult(model);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Create Productivity error : {ex.Message}");
                return new StatusCodeResult(500);
            }
        }

        [HttpPut]
        public async Task<ActionResult> Update(ProductivityEntity model)
        {
            try
            {
                _appDbContext.Set<ProductivityEntity>().Update(model);

                await _appDbContext.SaveChangesAsync();

                _logger.LogInformation($"Update Productivity {model.Name}");

                return new OkObjectResult(model);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Update Productivity error : {ex.Message}");

                return new StatusCodeResult(500);
            }
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(Guid id)
        {
            try
            {
                var data = await _appDbContext.Set<ProductivityEntity>().FindAsync(id);

                _appDbContext.Set<ProductivityEntity>().Remove(data);

                _logger.LogInformation($"Delete Productivity {data.Name}");

                return new OkObjectResult(data);
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }
        }

        [HttpGet("{id}")]
        public virtual async Task<ActionResult<ProductivityEntity>> GetById(Guid id)
        {
            try
            {
                var data = await _appDbContext.Set<ProductivityEntity>().FindAsync(id);

                return new OkObjectResult(data);
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }
        }

        [HttpGet("list")]
        public virtual async Task<ActionResult<List<ProductivityEntity>>> GetList()
        {
            try
            {
                var data = await _appDbContext.Set<ProductivityEntity>().ToListAsync();

                return new OkObjectResult(data);
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }
        }
    }
}