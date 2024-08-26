namespace KBMGrpcService.Models
{
    public class UpdateUserRequestModel
    {
        public long UserId { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
    }

}
