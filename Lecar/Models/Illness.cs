using SQLite;

namespace Lecar.Models
{
    public class Remedy
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Illness { get; set; } = string.Empty;
    }
}