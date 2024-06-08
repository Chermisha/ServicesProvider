namespace ServicesProvider.Models
{
    public class StatusCode
    {
        public int Code { get; set; }
        public string Description { get; set; }

        public StatusCode(int id, string description)
        {
            Code = id;
            Description = description;
        }
    }
}
