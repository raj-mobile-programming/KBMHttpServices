using KBMGrpcService.Protos;

namespace KBMHttpService.Services
{
    public interface IOrganizationService
    {
        public Task<CreateOrganizationResponse> CreateOrganizationAsync(CreateOrganizationRequest model);
        public Task<GetOrganizationResponse> GetOrganizationAsync(GetOrganizationRequest req);
    }

}
