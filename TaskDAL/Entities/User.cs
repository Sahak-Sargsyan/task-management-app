namespace TaskDAL.Entities
{
    public class User
    {
        public User(int id, string name, string userName, string email, string password)
        {
            Id = id;
            Name = name;
            UserName = userName;
            Email = email;
            Password = password;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public virtual ICollection<Task> Tasks { get; set; }
    }
}
