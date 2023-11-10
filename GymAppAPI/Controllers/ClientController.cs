using GymAppAPI.Models.Request;
using GymAppAPI.Models.Response;
using GymAppAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymAppAPI.Controllers
{
    [ApiController]
    [Route("GymAppApi/V1.0/Client")]
    [Authorize]
    public class ClientController : ControllerBase
    {
        private IClientService _iClientService;
        public ClientController(IClientService iClientService ) 
        {
            _iClientService = iClientService;
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            Response Response = new Response();

            try
            {
                var _list = _iClientService.GetAll();
                Response.success = true;
                Response.message = string.Empty;
                Response.data = _list;
            }
            catch (Exception ex)
            {
                Response.success = false;
                Response.message = ex.Message;
                Response.data = "";               
            }

            return Ok(Response);
        }

        
        [HttpPost]
        public IActionResult Add(ClientRequest oModel)
        {
            Response response = new Response();

            try
            {
                var membershipStatusResponse = _iClientService.Add(oModel);
                response.success = true;
                response.message = string.Empty;
                response.data = membershipStatusResponse;
            }
            catch(Exception ex)
            {
                response.success = false;
                response.message = ex.Message;
                response.data = "";
            }

            return Ok(response);
        }



        [HttpDelete("Delete/{Id}")]
        public IActionResult Delete(long Id)
        {
            Response response = new Response();

            try
            {
                _iClientService.Delete(Id);
                response.success = true;
                response.message = string.Empty;
                response.data = "";
            }
            catch(Exception ex)
            {
                response.success = false;
                response.message = ex.Message;
                response.data = "";
            }


            return Ok(response);
        }
    }
}
