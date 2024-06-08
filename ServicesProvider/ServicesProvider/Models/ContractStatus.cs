namespace ServicesProvider.Models
{
    public class ContractStatus
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public List<Contract> Contracts { get; set; }
    }
}
