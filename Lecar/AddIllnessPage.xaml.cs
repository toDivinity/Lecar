using System.Collections.ObjectModel;
using Lecar.Models;

namespace Lecar;

public partial class AddIllnessPage : ContentPage
{
    private readonly ObservableCollection<Illness> _illnesses;
    public ObservableCollection<Symptom> Symptoms { get; set; } = new();

    public AddIllnessPage(ObservableCollection<Illness> illnesses)
    {
        InitializeComponent();
        _illnesses = illnesses;

        // Привязываем список симптомов
        BindingContext = this;

        // Загружаем симптомы из базы данных
        LoadSymptoms();
    }

    private async void LoadSymptoms()
    {
        if (App.SymptomService != null)
        {
            var symptoms = await App.SymptomService.GetSymptomsAsync();
            foreach (var symptom in symptoms)
            {
                Symptoms.Add(new Symptom
                {
                    Id = symptom.Id,
                    Name = symptom.Name,
                    IsSelected = false // Устанавливаем начальное состояние
                });
            }
        }
    }

    private async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        // Создаём новую болезнь
        var newIllness = new Illness
        {
            Name = NameEntry.Text ?? string.Empty,
            Symptoms = Symptoms.Where(s => s.IsSelected).Select(s => s.Name).ToList()
        };

        // Добавляем болезнь в коллекцию
        _illnesses.Add(newIllness);

        // Сохраняем болезнь в базу данных
        if (App.IllnessService != null)
        {
            await App.IllnessService.AddIllnessAsync(newIllness);
        }

        await Navigation.PopModalAsync();
    }

    private async void OnCancelButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }
}
