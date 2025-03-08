
namespace Domain.Entities.Autentication
{
    public class RefreshToken
    {
        public int? Id { get; set; }
        public int? Token { get; set; }

        public string? UserId { get; set; }
    }
}
