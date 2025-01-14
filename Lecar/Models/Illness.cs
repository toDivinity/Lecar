using SQLite;

namespace Lecar.Models
{
    public class Illness
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Symptoms { get; set; } = string.Empty;
        public string Remedy { get; set; } = string.Empty;
    }
}