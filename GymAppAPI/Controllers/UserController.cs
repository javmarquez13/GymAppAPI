using GymAppAPI.Models.Request;
using GymAppAPI.Models.Response;
using GymAppAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymAppAPI.Controllers
{
    [ApiController]
    [Route("GymAppApi/V1.0/Users")]
    public class UserController : ControllerBase
    {
        private IUserService _iUserService;

        public UserController(IUserService iUserService) 
        {
            _iUserService = iUserService;
        }


        [HttpPost]
        public IActionResult Login(AuthRequest oModel)
        {
            Response response = new Response();

            try
            {
                var userResponse = _iUserService.Auth(oModel);

                if(userResponse == null)
                {
                    response.success = false;
                    response.message = "User or password are incorrect";
                    response.data = userResponse;
                    return BadRequest(response);
                }

                response.success = true;
                response.message =string.Empty;
                response.data = userResponse;               
            }
            catch (Exception ex)
            {
                response.success = false;
                response.message = ex.Message;
                response.data = "";
            }

            return Ok(response);
        }



        [HttpPost]
        [Route("CreateAccount")]
        public IActionResult CreateAccount(UserRequest oModel) 
        {
            var response = new Response();

            try
            {
                _iUserService.CreateAccount(oModel);
                response.success = true;
                response.message = "Account created successfully";
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
