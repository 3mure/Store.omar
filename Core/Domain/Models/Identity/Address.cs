namespace Domain.Models.Identity
{
    public class Address
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int Id { get; set; }
        public string AppUserId { get; set; }

    }
}