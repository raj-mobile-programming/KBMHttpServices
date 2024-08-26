using Grpc.Core;
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

        public async Task<CreateOrganizationResponse> CreateOrganizationAsync(CreateOrganizationRequest model)
        {
            try
            {
                var request = new CreateOrganizationRequest
                {
                    Name = model.Name,
                    Address = model.Address
                };

                return await _client.CreateOrganizationAsync(request);
            }
            catch (Exception ex)
            {
                throw new RpcException(new Status(StatusCode.NotFound, ex.Message));
            }
        }
        public async Task<GetOrganizationResponse> GetOrganizationAsync(GetOrganizationRequest req)
        {
            var request = new GetOrganizationRequest { Id = req.Id };

            try
            {
                var response = await _client.GetOrganizationAsync(request);
                return new KBMGrpcService.Protos.GetOrganizationResponse
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
        public async Task<QueryOrganizationsResponse> QueryOrganizationsAsync(QueryOrganizationsRequest req)
        {
            try
            {
                var response = await _client.QueryOrganizationsAsync(req);

                // Constructed response model
                var responseModel = new QueryOrganizationsResponse
                {
                    Page = response.Page,
                    PageSize = response.PageSize,
                    Organizations =
            {
                response.Organizations.Select(x => new KBMGrpcService.Protos.OrganizationModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Address = x.Address,
                    CreatedAt = x.CreatedAt
                }).ToList()

            }
                };
                responseModel.Total = responseModel.Organizations.Count;
                return responseModel;
            }
            catch (RpcException ex) when (ex.StatusCode == Grpc.Core.StatusCode.NotFound)
            {
                throw new RpcException(new Status(StatusCode.NotFound, ex.Message));
            }
        }
        //Update organization
        public async Task UpdateOrganizationAsync(UpdateOrganizationRequest req)
        {
            try
            {
                var request = new UpdateOrganizationRequest
                {
                    OrganizationId = req.OrganizationId,
                    Name = req.Name,
                    Address = req.Address
                };

                await _client.UpdateOrganizationAsync(request);
            }
            catch (RpcException ex) when (ex.StatusCode == Grpc.Core.StatusCode.NotFound)
            {
                throw new RpcException(new Status(StatusCode.NotFound, ex.Message));
            }
        }

        //Soft delete organization
        public async Task DeleteOrganizationAsync(DeleteOrganizationRequest req)
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

