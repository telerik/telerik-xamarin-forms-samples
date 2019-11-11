namespace TodoApp.Models
{
    public class Priority
    {
        public Priority(int iD, string name, int weight, string icon)
        {
            ID = iD;
            Name = name;
            Weight = weight;
            Icon = icon;
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public int Weight { get; set; }
        public string Icon { get; set; }
    }
}
