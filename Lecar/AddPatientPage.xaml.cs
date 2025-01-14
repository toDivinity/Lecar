using System.Collections.ObjectModel;
using Lecar.Models;

namespace Lecar;

public partial class AddPatientPage : ContentPage
{
    private readonly ObservableCollection<Patient> _patients;

    public AddPatientPage(ObservableCollection<Patient> patients)
    {
        InitializeComponent();
        _patients = patients;
    }

    private async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        // Проверка на заполненность имени
        if (string.IsNullOrWhiteSpace(NameEntry.Text))
        {
            await DisplayAlert("Ошибка", "Пожалуйста, введите имя пациента.", "ОК");
            return;
        }

        // Проверка на корректность возраста
        if (!int.TryParse(AgeEntry.Text, out var age) || age <= 0)
        {
            await DisplayAlert("Ошибка", "Пожалуйста, введите корректный возраст (целое число больше нуля).", "ОК");
            return;
        }

        // Проверка на заполненность симптомов
        if (string.IsNullOrWhiteSpace(SymptomsEntry.Text))
        {
            await DisplayAlert("Ошибка", "Пожалуйста, введите симптомы пациента.", "ОК");
            return;
        }

        // Создаём нового пациента
        var newPatient = new Patient
        {
            Name = NameEntry.Text.Trim(),
            Age = age,
            Symptoms = SymptomsEntry.Text.Trim()
        };

        // Добавляем пациента в базу данных через сервис
        if (App.PatientService != null)
        {
            await App.PatientService.AddPatientAsync(newPatient);
        }

        // Обновляем коллекцию
        _patients.Add(newPatient);

        // Возвращаемся на предыдущую страницу
        await Navigation.PopModalAsync();
    }

    private async void OnCancelButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }
}
