using api2.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace api2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly AppDbContext _appDbContext;

        public UserController(ILogger<UserController> logger, AppDbContext appDbContext)
        {
            _logger = logger;
            _appDbContext = appDbContext;
        }

        [HttpGet("{id}")]
        public virtual async Task<ActionResult<UserEntity>> GetById(Guid id)
        {
            try
            {
                var data = await _appDbContext.Set<UserEntity>().FindAsync(id);

                return new OkObjectResult(data);
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }
        }

        [HttpGet("list")]
        public virtual async Task<ActionResult<List<UserEntity>>> GetList()
        {
            try
            {
                var data = await _appDbContext.Set<UserEntity>().ToListAsync();

                return new OkObjectResult(data);
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }
        }
    }
}