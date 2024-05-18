namespace TaskBLL.Models
{
    public class TaskModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public int? UserId { get; set; }

        public TaskModel() { }
        public TaskModel(int id, string title, string description, DateTime dueDate, int? userId)
        {
            Id = id;
            Title = title;
            Description = description;
            DueDate = dueDate;
            UserId = userId;
        }
    }
}
