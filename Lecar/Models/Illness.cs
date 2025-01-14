using SQLite;
using System.Collections.Generic;
using System.Linq;

namespace Lecar.Models
{
    public class Illness
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; } // Уникальный идентификатор

        public string Name { get; set; }

        public string SymptomsSerialized { get; set; }

        [Ignore]
        public List<string> Symptoms
        {
            get => string.IsNullOrEmpty(SymptomsSerialized)
                ? new List<string>()
                : SymptomsSerialized.Split(',').ToList();
            set => SymptomsSerialized = value != null
                ? string.Join(",", value)
                : string.Empty;
        }

        [Ignore]
        public string SymptomsDisplay => Symptoms != null && Symptoms.Any()
            ? string.Join(", ", Symptoms)
            : "Нет симптомов";
    }
}
