namespace DotnetPlayground.DataEntities
{
    public class Allocation
    {
        public Guid PeopleId { get; set; }

        public People People { get; set; }

        public Project Project { get; set; }

        public Guid ProjectId { get; set; }

        public string Title { get; set; }

        public int Start { get; set; }

        public int End { get; set; }

        public int HoursPerDay { get; set; }
    }
}
