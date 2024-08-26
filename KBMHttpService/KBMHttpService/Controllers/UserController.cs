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

        /// <summary>
        /// Create new user
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
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
                var errorResponse = new
                {
                    StatusCode = (int)System.Net.HttpStatusCode.InternalServerError,
                    Message = ex.Message
                };

                return StatusCode((int)System.Net.HttpStatusCode.InternalServerError, errorResponse);
            }
        }

        /// <summary>
        /// Get user details on the bsis of the user id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
                var errorResponse = new
                {
                    StatusCode = (int)System.Net.HttpStatusCode.InternalServerError,
                    Message = ex.Message
                };

                return StatusCode((int)System.Net.HttpStatusCode.InternalServerError, errorResponse);
            }
        }

        /// <summary>
        /// Query the user data on the basis of different parameters
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("Query")]
        public async Task<IActionResult> QueryUsers([FromBody] QueryRequestModel request)
        {
            try
            {
                var req = new QueryRequestModel
                {
                    page = request.page,
                    pageSize = request.page,
                    orderBy = request.orderBy,
                    direction = request.orderBy,
                    queryString = request.queryString,
                };
                var response = await _userService.QueryUsersAsync(req);
                return Ok(response);
            }
            catch (Exception ex)
            {
                var errorResponse = new
                {
                    StatusCode = (int)System.Net.HttpStatusCode.InternalServerError,
                    Message = ex.Message
                };

                return StatusCode((int)System.Net.HttpStatusCode.InternalServerError, errorResponse);
            }
        }

        /// <summary>
        /// Update user data on the basis of user id
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("Update")]
        public async Task<IActionResult> UpdateUser(UpdateUserRequestModel request)
        {
            try
            {
                await _userService.UpdateUserAsync(request);
                return Ok("Success");
            }
            catch (Exception ex)
            {
                var errorResponse = new
                {
                    StatusCode = (int)System.Net.HttpStatusCode.InternalServerError,
                    Message = ex.Message
                };

                return StatusCode((int)System.Net.HttpStatusCode.InternalServerError, errorResponse);
            }
        }

        /// <summary>
        /// Soft delete user on the basis of the user id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteUser(long id)
        {
            try
            {
                var req = new DeleteUserRequestModel { UserId = id };
                await _userService.DeleteUserAsync(req);
                return Ok("User deleted successfully");
            }
            catch (Exception ex)
            {
                var errorResponse = new
                {
                    StatusCode = (int)System.Net.HttpStatusCode.InternalServerError,
                    Message = ex.Message
                };

                return StatusCode((int)System.Net.HttpStatusCode.InternalServerError, errorResponse);
            }
        }

        /// <summary>
        /// Associate an active user to any existing and active organization
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("Associcate")]
        public async Task<IActionResult> AssociateUserToOrganization(AssociateUserRequestModel request)
        {
            try
            {
                await _userService.AssociateUserToOrganizationAsync(request);
                return Ok("User associated successfully");
            }
            catch (Exception ex)
            {
                var errorResponse = new
                {
                    StatusCode = (int)System.Net.HttpStatusCode.InternalServerError,
                    Message = ex.Message
                };

                return StatusCode((int)System.Net.HttpStatusCode.InternalServerError, errorResponse);
            }
        }

        /// <summary>
        /// Disassociate user from the organization
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("Disassociate")]
        public async Task<IActionResult> DisassociateUserFromOrganizationRequest(DisassociateUserRequestModel request)
        {
            try
            {
                await _userService.DisassociateUserFromOrganizationAsync(request);
                return Ok("User diassociated successfully.");
            }
            catch (Exception ex)
            {
                var errorResponse = new
                {
                    StatusCode = (int)System.Net.HttpStatusCode.InternalServerError,
                    Message = ex.Message
                };

                return StatusCode((int)System.Net.HttpStatusCode.InternalServerError, errorResponse);
            }
        }
    }
}
