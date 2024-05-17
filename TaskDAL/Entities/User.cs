namespace TaskDAL.Entities
{

    public class User
    {
        public User(string name, string userName, string password)
        {
            Name = name;
            UserName = userName;
            Password = password;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public virtual ICollection<Task> Tasks { get; set; }
    }
}
