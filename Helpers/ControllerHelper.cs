using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Book_Keep.Helpers
{
    [Route("")]
    [ApiController]
    public abstract class BaseApiController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        protected BaseApiController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        protected ActionResult HandleException(Exception e)
        {
            return new ObjectResult(e.InnerException?.Message ?? e.Message)
            {
                StatusCode = 500
            };
        }

    }
}
