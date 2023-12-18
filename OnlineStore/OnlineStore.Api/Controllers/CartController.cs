using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OnlineStore.Api.Controllers
{
    [Route("api/Cart")]
    public class CartController : BaseController
    {
        public CartController(
            ILogger<BaseController> logger,
            IMapper mapper)
            : base(logger, mapper)
        {
        }

        //Show product trong cart
        [HttpGet]
        public IActionResult Cart()
        {
            return Ok();
        }

        //[HttpPost("{id}")]
        //public IActionResult EditProduct();
    }
}
