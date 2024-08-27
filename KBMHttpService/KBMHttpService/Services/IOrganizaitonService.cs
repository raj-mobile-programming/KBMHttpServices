using KBMGrpcService.Protos;
using KBMHttpService.Models;

namespace KBMHttpService.Services
{
    public interface IOrganizationService
    {
        public Task<CreateOrganizationResponseModel> CreateOrganizationAsync(CreateOrganizationRequestModel model);
        public Task<GetOrganizationResponseModel> GetOrganizationAsync(GetOrganizationRequestModel req);
        public Task<KBMHttpService.Models.QueryOrganizationResponseModel> QueryOrganizationsAsync(QueryRequestModel req);
        public Task<UpdateOrganizationResponseModel> UpdateOrganizationAsync(UpdateOrganizationRequestModel req);
        public Task DeleteOrganizationAsync(DeleteOrganizationRequestModel req);
    }

}
