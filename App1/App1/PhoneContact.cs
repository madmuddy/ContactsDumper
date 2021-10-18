namespace App1
{
    public partial class MainActivity
    {
        public class PhoneContact
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string PhoneNumber { get; set; }

            public string Name { get => $"{FirstName} {LastName}"; }

        }
    }
}
