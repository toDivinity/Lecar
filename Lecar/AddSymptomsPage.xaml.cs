using System.Collections.ObjectModel;
using Lecar.Models;

namespace Lecar;

public partial class AddSymptomPage : ContentPage
{
    private readonly ObservableCollection<Symptom> _symptoms;

    public AddSymptomPage(ObservableCollection<Symptom> symptoms)
    {
        InitializeComponent();
        _symptoms = symptoms;
    }

    private async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        // Проверка на корректное заполнение имени
        if (string.IsNullOrWhiteSpace(SymptomNameEntry.Text))
        {
            await DisplayAlert("Ошибка", "Пожалуйста, введите название симптома.", "ОК");
            return;
        }

        // Создаем новый симптом
        var newSymptom = new Symptom
        {
            Name = SymptomNameEntry.Text.Trim()
        };

        // Добавляем в базу данных через сервис
        if (App.SymptomService != null)
        {
            await App.SymptomService.AddSymptomAsync(newSymptom);
        }

        // Обновляем коллекцию
        _symptoms.Add(newSymptom);

        await Navigation.PopModalAsync();
    }

    private async void OnCancelButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }
}
