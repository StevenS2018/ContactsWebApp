namespace ContactsWebApp.Models
{
    public class ContactsModel
    {
        public int id { get; set; }
        public string Name { get; set; } = string.Empty; 
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;

        public string State = string.Empty;

        public string PostalCode { get; set; } = string.Empty;

        public string Country { get; set; } = string.Empty;

    }
}
