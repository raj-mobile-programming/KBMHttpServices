using KBMGrpcService.Models;
using KBMGrpcService.Protos;
using KBMHttpService.Models;

namespace KBMHttpService.Services
{
    public interface IUserService
    {
        public Task<CreateUserResponseModel> CreateUserAsync(CreateUserRequestModel req);
        public Task<GetUserResponse> GetUserAsync(GetUserRequest req);
        public Task<QueryUsersResponseModel> QueryUsersAsync(QueryRequestModel req);
        public Task<UpdateUserResponseModel> UpdateUserAsync(UpdateUserRequestModel req);
        public Task<DeleteUserResponseModel> DeleteUserAsync(DeleteUserRequestModel req);
        public Task<string> AssociateUserToOrganizationAsync(AssociateUserRequestModel req);
        public Task<string> DisassociateUserFromOrganizationAsync(DisassociateUserRequestModel req);
    }
}
