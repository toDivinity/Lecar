using SQLite;

namespace Lecar.Models
{
    public class Symptom
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; }

        [Ignore]
        public bool IsSelected { get; set; }
    }
}
