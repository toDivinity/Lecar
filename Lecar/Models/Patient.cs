using SQLite;

namespace Lecar.Models
{
    public class Patient
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int Age { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Symptoms { get; set; } = string.Empty;

        public DateTime DateAdded { get; set; } = DateTime.Now;
    }
}