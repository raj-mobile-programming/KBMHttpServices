using KBMGrpcService.Models;
using KBMGrpcService.Protos;
using KBMHttpService.Models;

namespace KBMHttpService.Services
{
    public interface IUserService
    {
        public Task<CreateUserResponseModel> CreateUserAsync(CreateUserRequestModel req);
        public Task<GetUserResponse> GetUserAsync(GetUserRequest req);
    }
}
