using KBMGrpcService.Protos;
using KBMHttpService.Models;

namespace KBMHttpService.Services
{
    public interface IOrganizationService
    {
        public Task<CreateOrganizationResponse> CreateOrganizationAsync(CreateOrganizationRequestModel model);
        public Task<GetOrganizationResponse> GetOrganizationAsync(GetOrganizationRequest req);
        public Task<KBMHttpService.Models.QueryOrganizationResponseModel> QueryOrganizationsAsync(QueryRequestModel req);
        public Task<UpdateOrganizationResponse> UpdateOrganizationAsync(UpdateOrganizationRequest req);
        public Task DeleteOrganizationAsync(DeleteOrganizationRequest req);
    }

}
