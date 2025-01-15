using SQLite;

namespace Lecar.Models
{
    public class Remedy
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int Unit { get; set; }
        public string Name { get; set; } = string.Empty;

    }
}