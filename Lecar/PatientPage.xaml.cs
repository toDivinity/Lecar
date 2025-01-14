using System.Collections.ObjectModel;

namespace Lecar;

public partial class PatientPage : ContentPage
{
    public ObservableCollection<Models.Patient> Patients { get; set; } = new();

    public PatientPage()
    {
        InitializeComponent();
        BindingContext = this;

        // Загрузка данных из базы
        LoadPatients();
    }

    private async void LoadPatients()
    {
        var patients = await App.Database.GetPatientsAsync();
        foreach (var patient in patients)
        {
            Patients.Add(patient);
        }
    }

    private async void OnAddPatientClicked(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new AddPatientPage(Patients));
    }

    private async void OnDeletePatientClicked(object sender, EventArgs e)
    {
        // Получаем пациента из параметра кнопки
        if (sender is Button button && button.CommandParameter is Models.Patient patient)
        {
            // Удаляем пациента из коллекции
            Patients.Remove(patient);

            // Удаляем пациента из базы данных
            await App.Database.DeletePatientAsync(patient);
        }
    }
}
