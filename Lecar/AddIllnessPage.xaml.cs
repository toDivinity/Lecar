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
        // Проверка на заполненность названия
        if (string.IsNullOrWhiteSpace(NameEntry.Text))
        {
            await DisplayAlert("Ошибка", "Пожалуйста, введите название болезни.", "ОК");
            return;
        }

        // Проверка на наличие выбранных симптомов
        var selectedSymptoms = Symptoms.Where(s => s.IsSelected).Select(s => s.Name).ToList();
        if (!selectedSymptoms.Any())
        {
            await DisplayAlert("Ошибка", "Пожалуйста, выберите хотя бы один симптом.", "ОК");
            return;
        }

        // Создаём новую болезнь
        var newIllness = new Illness
        {
            Name = NameEntry.Text.Trim(),
            Symptoms = selectedSymptoms
        };

        // Добавляем болезнь в коллекцию
        _illnesses.Add(newIllness);

        // Сохраняем болезнь в базу данных
        if (App.IllnessService != null)
        {
            await App.IllnessService.AddIllnessAsync(newIllness);
        }

        // Возвращаемся на предыдущую страницу
        await Navigation.PopModalAsync();
    }


    private async void OnCancelButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }
}
