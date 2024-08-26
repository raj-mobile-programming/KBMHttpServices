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
    }
}
