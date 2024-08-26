using Grpc.Core;
using KBMGrpcService.Models;
using KBMGrpcService.Protos;
using KBMHttpService.Models;

namespace KBMHttpService.Services
{
    public class UserService : IUserService
    {
        private readonly KBMGrpcService.Protos.UserService.UserServiceClient _client;

        public UserService(KBMGrpcService.Protos.UserService.UserServiceClient client)
        {
            _client = client;
        }

        public async Task<CreateUserResponseModel> CreateUserAsync(CreateUserRequestModel req)
        {
            try
            {
                var request = new CreateUserRequest
                {
                    Name = req.Name,
                    Username = req.Username,
                    Email = req.Email
                };

                var response = await _client.CreateUserAsync(request);
                return new CreateUserResponseModel { UserId = response.UserId }; 
            }
            catch (RpcException ex) when (ex.StatusCode == Grpc.Core.StatusCode.NotFound)
            {
                throw new RpcException(new Status(StatusCode.NotFound, ex.Message));
            }
        }


        public async Task<GetUserResponse> GetUserAsync(GetUserRequest req)
        {
            try
            {
                var response = await _client.GetUserAsync(req);
                return new GetUserResponse
                {
                    Name = response.Name,
                    Username = response.Username,
                    Email = response.Email,
                    CreatedAt = response.CreatedAt
                };
            }
            catch (RpcException ex) when (ex.StatusCode == Grpc.Core.StatusCode.NotFound)
            {
                throw new RpcException(new Status(StatusCode.NotFound, ex.Message));
            }
        }
    }

}
