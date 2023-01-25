namespace DotnetPlayground.DataEntities
{
    public class Project
    {
        public Guid Id { get; set; }
        
        public string Title { get; set; } 

        public string Color { get; set; }

        public ICollection<Allocation> Allocations { get; set; }
    }
}
