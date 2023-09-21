using api1.Entities;
using api1.MessageBrokers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace api1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly AppDbContext _appDbContext;
        private readonly IMessageProducer _messageProducer;
        public UserController(ILogger<UserController> logger, AppDbContext appDbContext, IMessageProducer messageProducer)
        {
            _logger = logger;
            _appDbContext = appDbContext;
            _messageProducer = messageProducer;
        }

        [HttpPost]
        public async Task<ActionResult> Create(UserEntity model)
        {
            try
            {
                _appDbContext.Set<UserEntity>().Add(model);
                
                await _appDbContext.SaveChangesAsync();

                _logger.LogInformation($"Create user {model.Username}");

                _messageProducer.SendMessage(model, "add_user");

                return new OkObjectResult(model);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Create user error : {ex.Message}");
                return new StatusCodeResult(500);
            }
        }

        [HttpPut]
        public async Task<ActionResult> Update(UserEntity model)
        {
            try
            {
                _appDbContext.Set<UserEntity>().Update(model);

                await _appDbContext.SaveChangesAsync();

                _logger.LogInformation($"Update user {model.Username}");

                _messageProducer.SendMessage(model, "update_user");

                return new OkObjectResult(model);
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(Guid id)
        {
            try
            {
                var data = await _appDbContext.Set<UserEntity>().FindAsync(id);

                _appDbContext.Set<UserEntity>().Remove(data);

                _logger.LogInformation($"Delete user {data.Username}");

                _messageProducer.SendMessage(data, "delete_user");

                return new OkObjectResult(data);
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }
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