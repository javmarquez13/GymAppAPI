using GymAppAPI.Models.Response;
using GymAppAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymAppAPI.Controllers
{
    [ApiController]
    [Route("GymAppApi/V1.0/Membership")]
    [Authorize]
    public class MemberShipController : ControllerBase
    {
        private IMembershipService _iMembershipService;
        public MemberShipController(IMembershipService iMembershipService) 
        { 
            _iMembershipService = iMembershipService;
        }


        [HttpGet]
        [Route("Status")]
        public IActionResult FindStatusByIdOrEmail([FromQuery] int? Id, [FromQuery] string? Email)
        {
            Response response = new Response();

            try
            {
                var membershipStatusResponse = new MembershipStatusResponse();

                if (Id != null)
                    membershipStatusResponse = _iMembershipService.IsActiveById(Convert.ToInt32(Id));

                if (Email != null)
                    membershipStatusResponse = _iMembershipService.IsActiveByEmail(Email);


                response.success = true;
                response.message = string.Empty;
                response.data = membershipStatusResponse;
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
