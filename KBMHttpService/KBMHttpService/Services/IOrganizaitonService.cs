using KBMGrpcService.Protos;

namespace KBMHttpService.Services
{
    public interface IOrganizationService
    {
        public Task<CreateOrganizationResponse> CreateOrganizationAsync(CreateOrganizationRequest model);
        public Task<GetOrganizationResponse> GetOrganizationAsync(GetOrganizationRequest req);
        public Task<QueryOrganizationsResponse> QueryOrganizationsAsync(QueryOrganizationsRequest req);
        public Task UpdateOrganizationAsync(UpdateOrganizationRequest req);
        public Task DeleteOrganizationAsync(DeleteOrganizationRequest req);
    }

}
