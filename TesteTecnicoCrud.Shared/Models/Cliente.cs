namespace TesteTecnicoCrud.Shared.Models
{
    public class Cliente
    {
        public int Id { get; set; }            
        public string Name { get; set; } = string.Empty;
        public string Lastname { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Address { get; set; } = string.Empty;
    }
}
