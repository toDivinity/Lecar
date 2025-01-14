using System.Collections.ObjectModel;

namespace Lecar;

public partial class AddPatientPage : ContentPage
{
    private readonly ObservableCollection<Models.Patient> _patients;

    public AddPatientPage(ObservableCollection<Models.Patient> patients)
    {
        InitializeComponent();
        _patients = patients;
    }

    private async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        var newPatient = new Models.Patient
        {
            Name = NameEntry.Text ?? string.Empty,
            Age = int.TryParse(AgeEntry.Text, out var age) ? age : 0,
            Symptoms = SymptomsEntry.Text ?? string.Empty
        };

        // Добавляем в базу данных
        await App.Database.AddPatientAsync(newPatient);

        // Обновляем коллекцию
        _patients.Add(newPatient);

        await Navigation.PopModalAsync();
    }

    private async void OnCancelButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }
}
