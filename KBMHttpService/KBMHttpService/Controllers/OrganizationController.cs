using KBMHttpService.Services;
using KBMGrpcService.Protos;
using Microsoft.AspNetCore.Mvc;
using KBMHttpService.Models;

namespace KBMHttpService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrganizationController : ControllerBase
    {
        private readonly IOrganizationService _organizationService;

        public OrganizationController(IOrganizationService organizationService)
        {
            _organizationService = organizationService;
        }

        /// <summary>
        /// Create new organization.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("Create")]
        public async Task<IActionResult> CreateOrganization(CreateOrganizationRequestModel request)
        {
            try
            {
                var response = await _organizationService.CreateOrganizationAsync(request);
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
        /// Get organization data by organizationId.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetOrganization(long id)
        {
            try
            {
                var request = new GetOrganizationRequestModel { organizationId = id };
                var response = await _organizationService.GetOrganizationAsync(request);

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
        /// Query organization and get data.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("Query")]
        public async Task<IActionResult> QueryOrganizations([FromBody] QueryRequestModel request)
        {
            try
            {
                var response = await _organizationService.QueryOrganizationsAsync(request);
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
        /// Update organization on the basis of organization id
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("Update")]
        public async Task<IActionResult> UpdateOrganization(KBMHttpService.Models.UpdateOrganizationRequestModel request)
        {
            try
            {
                var req = new UpdateOrganizationRequestModel { OrganizationId = request.OrganizationId, Name = request.Name, Address = request.Address };
                 var response = await _organizationService.UpdateOrganizationAsync(req);
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
        /// Soft delete organization on the basis of organization id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteOrganization(long id)
        {
            try
            {
                var request = new DeleteOrganizationRequestModel { OrganizationId = id };
                await _organizationService.DeleteOrganizationAsync(request);
                return Ok("Organization deleted successfully.");
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
