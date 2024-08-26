namespace KBMHttpService.Models
{
    public class UpdateOrganizationRequestModel
    {
        public long OrganizationId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }
}
