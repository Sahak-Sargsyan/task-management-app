namespace TaskDAL.Entities
{
    public class Task
    {
        public Task(int id, string title, string description, DateTime dueDate, string priority, int categoryId, int userId)
        {
            Id = id;
            Title = title;
            Description = description;
            DueDate = dueDate;
            Priority = priority;
            CategoryId = categoryId;
            UserId = userId;
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public string Priority { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }


    }
}
