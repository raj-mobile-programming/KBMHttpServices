using Grpc.Core;
using KBMGrpcService.Models;
using KBMGrpcService.Protos;
using KBMHttpService.Models;

namespace KBMHttpService.Services
{
    public class OrganizationService : IOrganizationService
    {
        private readonly KBMGrpcService.Protos.OrganizationService.OrganizationServiceClient _client;

        public OrganizationService(KBMGrpcService.Protos.OrganizationService.OrganizationServiceClient client)
        {
            _client = client;
        }

        public async Task<CreateOrganizationResponseModel> CreateOrganizationAsync(CreateOrganizationRequestModel request)
        {
            try
            {
                var req = new CreateOrganizationRequest
                {
                    Name = request.Name,
                    Address = request.Address
                };

                var response = await _client.CreateOrganizationAsync(req);
                return new CreateOrganizationResponseModel { OrganizationId = response.OrganizationId};
            }
            catch (Exception ex)
            {
                throw new RpcException(new Status(StatusCode.NotFound, ex.Message));
            }
        }
        public async Task<GetOrganizationResponseModel> GetOrganizationAsync(GetOrganizationRequestModel req)
        {
            var request = new GetOrganizationRequest { Id = req.organizationId };

            try
            {
                var response = await _client.GetOrganizationAsync(request);
                return new GetOrganizationResponseModel
                {
                    Name = response.Name,
                    Address = response.Address,
                    CreatedAt = response.CreatedAt,
                    UpdatedAt = response.UpdatedAt
                };
            }
            catch (RpcException ex) when (ex.StatusCode == Grpc.Core.StatusCode.NotFound)
            {
                throw new RpcException(new Status(StatusCode.NotFound, ex.Message));
            }
        }
        //Query organization
        public async Task<KBMHttpService.Models.QueryOrganizationResponseModel> QueryOrganizationsAsync(QueryRequestModel req)
        {
            try
            {
                var request = new QueryOrganizationsRequest
                {
                    Page = req.page,
                    PageSize = req.pageSize,
                    Direction = req.direction ?? "asc", 
                    OrderBy = req.orderBy ?? "Name", 
                    QueryString = req.queryString ?? string.Empty
                };

                var response = await _client.QueryOrganizationsAsync(request);

                // Constructed response model
                var responseModel = new QueryOrganizationResponseModel
                {
                    Page = response.Page,
                    PageSize = response.PageSize,
                    Organizations = response.Organizations.Select(x => new OrganizationsModel
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Address = x.Address,
                        CreatedAt = x.CreatedAt,
                        UpdatedAt = x.UpdatedAt
                    }).ToList()
                };

                responseModel.Total = responseModel.Organizations.Count();
                return responseModel;
            }
            catch (RpcException ex) when (ex.StatusCode == Grpc.Core.StatusCode.NotFound)
            {
                throw new RpcException(new Status(StatusCode.NotFound, ex.Message));
            }
        }

        //Update organization
        public async Task<UpdateOrganizationResponseModel> UpdateOrganizationAsync(UpdateOrganizationRequestModel req)
        {
            try
            {
                var request = new UpdateOrganizationRequest
                {
                    OrganizationId = req.OrganizationId,
                    Name = req.Name,
                    Address = req.Address
                };

               var response = await _client.UpdateOrganizationAsync(request);
                return new UpdateOrganizationResponseModel { Message = response.Message };
            }
            catch (RpcException ex) when (ex.StatusCode == Grpc.Core.StatusCode.NotFound)
            {
                throw new RpcException(new Status(StatusCode.NotFound, ex.Message));
            }
        }

        //Soft delete organization
        public async Task DeleteOrganizationAsync(DeleteOrganizationRequestModel req)
        {
            try
            {
                var request = new DeleteOrganizationRequest { OrganizationId = req.OrganizationId };

                await _client.DeleteOrganizationAsync(request);
            }
            catch (RpcException ex) when (ex.StatusCode == Grpc.Core.StatusCode.NotFound)
            {
                throw new RpcException(new Status(StatusCode.NotFound, ex.Message));
            }
        }
    }
}

