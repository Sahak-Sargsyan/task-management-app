namespace TaskBLL.Models
{
    public class CategoryModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<int> TaskIds { get; set; }

        public CategoryModel() { }
        public CategoryModel(int id, string name, ICollection<int> taskIds)
        {
            this.Id = id;
            this.Name = name;
            TaskIds = taskIds;
        }
    }
}
