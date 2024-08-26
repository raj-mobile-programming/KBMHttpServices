using KBMGrpcService.Models;

namespace KBMHttpService.Models
{
    public class QueryOrganizationResponseModel
    {
            public int Page { get; set; }
            public int PageSize { get; set; }
            public int Total { get; set; }
            public IEnumerable<OrganizationsModel> Organizations { get; set; } = Enumerable.Empty<OrganizationsModel>();
    }
}
