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
        // Создаём нового пациента
        var newPatient = new Patient
        {
            Name = NameEntry.Text ?? string.Empty,
            Age = int.TryParse(AgeEntry.Text, out var age) ? age : 0,
            Symptoms = SymptomsEntry.Text ?? string.Empty
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
