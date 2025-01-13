using Lecar.Models;

namespace Lecar;

public partial class AddPatientPage : ContentPage
{
    public AddPatientPage()
    {
        InitializeComponent();
    }

    private async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        // Проверяем, что введены все данные
        string? name = NameEntry.Text?.Trim();
        string? ageText = AgeEntry.Text?.Trim();
        string? symptoms = SymptomsEntry.Text?.Trim();

        if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(ageText) || string.IsNullOrWhiteSpace(symptoms))
        {
            await DisplayAlert("Error", "Пожалуйста, заполните все поля.", "OK");
            return;
        }

        if (!int.TryParse(ageText, out int age))
        {
            await DisplayAlert("Error", "Возраст задан некорректно.", "OK");
            return;
        }

        // Создаем нового пациента
        var newPatient = new Patient
        {
            Name = name,
            Age = age,
            Symptoms = symptoms,
            DateAdded = DateTime.Now
        };

        // Добавляем пациента в базу данных
        await App.Database.AddPatientAsync(newPatient);

        // Закрываем страницу
        await Navigation.PopAsync();
    }

    private async void OnCancelButtonClicked(object sender, EventArgs e)
    {
        // Закрываем страницу без сохранения
        await Navigation.PopAsync();
    }
}
