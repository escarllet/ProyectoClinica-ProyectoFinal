
namespace Domain.Entities.Authentication
{
    public class RefreshToken
    {
        public int? Id { get; set; }
        public int? Token { get; set; }

        public string? UserId { get; set; }
    }
}
