namespace DotnetPlayground.DataEntities
{
    public class People
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public String Tags { get; set; }

        public ICollection<Allocation> Allocations { get; set; }   
    }
}
