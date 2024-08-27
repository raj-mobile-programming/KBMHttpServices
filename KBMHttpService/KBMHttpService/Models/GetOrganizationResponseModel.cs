namespace KBMHttpService.Models
{
    public class GetOrganizationResponseModel
    {
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public long CreatedAt { get; set; }
        public long UpdatedAt { get; set; }
    }
}
