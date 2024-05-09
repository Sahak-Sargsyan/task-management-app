namespace TaskBLL.Models
{
    public class CategoryModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<int> TaskIds { get; set; }

        public CategoryModel() { }
        public CategoryModel(int id, string name)
        {
            this.Id = id;
            this.Name = name;
            TaskIds = new List<int>();
        }
    }
}
