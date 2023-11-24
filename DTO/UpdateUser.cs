
namespace Backend.DTO
{
    public class UpdateUser
    {
        public string Id { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }
        public long? DistrictId { get; set; }
        public string? Email { get; set; }
    }
}
