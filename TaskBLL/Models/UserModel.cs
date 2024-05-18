namespace TaskBLL.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public ICollection<int> TaskIds { get; set; }

        public UserModel() { }
        public UserModel(int id, string name, string userName, string password)
        {
            Id = id;
            Name = name;
            UserName = userName;
            Password = password;
            TaskIds = new List<int>();
        }
    }
}
