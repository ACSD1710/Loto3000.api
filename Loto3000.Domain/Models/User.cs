namespace Loto3000.Domain.Models
{
    public class User : IEntity
    {
        public User() 
        { 
        }
       public User(string firstName, string lastName, string username, string pw, string email, DateTime dateOfBirth )
        {
           
            FirstName = firstName;
            LastName = lastName;
            Username = username;
            Password = pw;
            Email = email;
            DateOfBirth = dateOfBirth;
            Roles = new List<Role>();

        }

        public int Id { get; set; } 
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public double Credits { get; set; }
        public DateTime DateOfBirth { get; set; }
        public IList<Ticket> Tickets { get; set; } = new List<Ticket>();
        public IList<Role> Roles { get; set; } = new List<Role>();


        

    }
}