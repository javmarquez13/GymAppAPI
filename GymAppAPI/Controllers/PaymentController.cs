using GymAppAPI.Models.Request;
using GymAppAPI.Models.Response;
using GymAppAPI.Services;
using GymAppAPI.Tools;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System.Security.Claims;

namespace GymAppAPI.Controllers
{
    [ApiController]
    [Route("GymAppApi/V1.0/Payment")]
    [Authorize]
    public class PaymentController : ControllerBase
    {
        IPaymentService _iPaymentService;

        public PaymentController(IPaymentService iPaymentService)
        {
            _iPaymentService = iPaymentService;
        }


        [HttpPost]
        public IActionResult Add()
        {
            var email = User.Claims.Where(x => x.Type == ClaimTypes.Email).FirstOrDefault()?.Value;

            Response response = new Response();

            try
            {
                //_iPaymentService.Add(oModel);
                response.success = true;
                response.message = "";
                response.data = "";
            }
            catch (Exception ex) 
            {
                response.success = false;
                response.message = ex.Message;
                response.data = "";
            }

            return Ok(response);
        }

    }
}
