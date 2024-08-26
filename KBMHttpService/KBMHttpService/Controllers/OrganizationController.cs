using KBMHttpService.Services;
using KBMGrpcService.Protos;
using Microsoft.AspNetCore.Mvc;
using Grpc.Core;

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

        [HttpPost("Create")]
        public async Task<IActionResult> CreateOrganization(CreateOrganizationRequest request)
        {
            try
            {
                var response = await _organizationService.CreateOrganizationAsync(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode((int)System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetOrganization(long id)
        {
            try
            {
                var request = new GetOrganizationRequest { Id = id };
                var response = await _organizationService.GetOrganizationAsync(request);

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
        public async Task<IActionResult> QueryOrganizations([FromQuery] QueryOrganizationsRequest request)
        {
            try
            {
                var response = await _organizationService.QueryOrganizationsAsync(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode((int)System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
