namespace CodeChallenge.Model
{
    public class TODO
    {

        public string Title { get; set; }

        public string Description { get; set; }

        public PriorityEnum Priority { get; set; }
    }


    public enum PriorityEnum
    {
        High, Normal, Low
    }
}
