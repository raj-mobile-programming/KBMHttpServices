using KBMHttpService.Services;
using KBMGrpcService.Models;
using KBMGrpcService.Protos;
using Microsoft.AspNetCore.Mvc;
using KBMHttpService.Models;

namespace KBMHttpService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateUser(CreateUserRequestModel request)
        {
            try
            {
                var response = await _userService.CreateUserAsync(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode((int)System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetUser(long id)
        {
            try
            {
                var req = new GetUserRequest { UserId = id };
                var response = await _userService.GetUserAsync(req);

                if (response == null)
                {
                    return NotFound();
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode((int)System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost("Query")]
        public async Task<IActionResult> QueryUsers([FromQuery] QueryOrganizationRequestModel req)
        {
            try
            {
                var request = new QueryUserRequestModel
                {
                    page = req.page,
                    pageSize = req.page,
                    orderBy = req.orderBy,
                    direction = req.orderBy,
                    queryString = req.queryString,
                };
                var response = await _userService.QueryUsersAsync(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode((int)System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateUser(UpdateUserRequestModel req)
        {
            try
            {
                await _userService.UpdateUserAsync(req);
                return Ok("Success");
            }
            catch (Exception ex)
            {
                return StatusCode((int)System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteUser(long id)
        {
            var req = new DeleteUserRequestModel { UserId = id };
            await _userService.DeleteUserAsync(req);
            return Ok("Success");
        }

        [HttpPut("Associcate")]
        public async Task<IActionResult> AssociateUserToOrganization(AssociateUserRequestModel req)
        {
            await _userService.AssociateUserToOrganizationAsync(req);
            return Ok("Success");
        }

        [HttpPut("Disassociate")]
        public async Task<IActionResult> DisassociateUserFromOrganizationRequest(DisassociateUserRequestModel req)
        {
            await _userService.DisassociateUserFromOrganizationAsync(req);
            return Ok("Success");
        }
    }
}
