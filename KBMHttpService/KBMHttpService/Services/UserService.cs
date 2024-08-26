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

        public async Task<CreateUserResponseModel> CreateUserAsync(CreateUserRequestModel request)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Email))
                {
                    throw new RpcException(new Status(StatusCode.InvalidArgument, "Name, Username and Email are required."));
                }
                var req = new CreateUserRequest
                {
                    Name = request.Name,
                    Username = request.Username,
                    Email = request.Email
                };

                var response = await _client.CreateUserAsync(req);
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

        //Query User

        public async Task<QueryUsersResponseModel> QueryUsersAsync(QueryRequestModel req)
        {
            var request = new QueryUsersRequest
            {
                Page = req.page,
                PageSize = req.pageSize,
                Direction = req.direction ?? "asc", 
                OrderBy = req.orderBy ?? "Username",
                QueryString = req.queryString ?? string.Empty
            };

            var response = await _client.QueryUsersAsync(request);

            var responseModel = new QueryUsersResponseModel
            {
                Page = response.Page,
                PageSize = response.PageSize,
                Users = response.Users.Select(u => new UsersModel
                {
                    Id = u.Id,
                    Name = u.Name,
                    Username = u.Username,
                    Email = u.Email,
                    CreatedAt = u.CreatedAt
                }).ToList()
            };

            responseModel.Total = responseModel.Users.Count();
            return responseModel;
        }

        public async Task<UpdateUserResponseModel> UpdateUserAsync(UpdateUserRequestModel req)
        {
            var request = new UpdateUserRequest
            {
                UserId = req.UserId,
                Name = req.Name,
                Username = req.Username,
                Email = req.Email
            };

            var response = await _client.UpdateUserAsync(request);
            return new UpdateUserResponseModel { Message = "User updated successfully." };
        }

        public async Task<DeleteUserResponseModel> DeleteUserAsync(DeleteUserRequestModel req)
        {
            var request = new DeleteUserRequest { UserId = req.UserId };

            var response = await _client.DeleteUserAsync(request);
            return new DeleteUserResponseModel { Message = "User deleted successfully." };
        }

        public async Task<string> AssociateUserToOrganizationAsync(AssociateUserRequestModel req)
        {
            var request = new AssociateUserToOrganizationRequest { UserId = req.UserId, OrganizationId = req.OrganizationId };

            var response = await _client.AssociateUserToOrganizationAsync(request);
            return "User associated successfully";
        }

        public async Task<string> DisassociateUserFromOrganizationAsync(DisassociateUserRequestModel req)
        {
            var request = new DisassociateUserFromOrganizationRequest { UserId = req.UsertId };

            var response = await _client.DisassociateUserFromOrganizationAsync(request);
            return "User disassociated successfully";
        }

    }

}
