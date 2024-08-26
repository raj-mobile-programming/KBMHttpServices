namespace KBMGrpcService.Models
{
    public class CreateUserRequestModel
    {
        public string Name { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }

}
