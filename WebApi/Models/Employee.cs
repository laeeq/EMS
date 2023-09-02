namespace WebApi.Models
{
    public class Employee
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public string Department { get; set; }
        public override string ToString()
        {
            return $"{FirstName} {LastName} [Email={Email}]";
        }
    }
}
