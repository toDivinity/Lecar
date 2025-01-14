using System.Collections.ObjectModel;
using Lecar.Models;

namespace Lecar;

public partial class PatientPage : ContentPage
{
    public ObservableCollection<Patient> Patients { get; set; } = new();

    public PatientPage()
    {
        InitializeComponent();
        BindingContext = this;

        // Загрузка данных из базы
        LoadPatients();
    }

    private async void LoadPatients()
    {
        if (App.PatientService != null)
        {
            var patients = await App.PatientService.GetPatientsAsync();
            foreach (var patient in patients)
            {
                Patients.Add(patient);
            }
        }
    }

    private async void OnAddPatientClicked(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new AddPatientPage(Patients));
    }

    private async void OnDeletePatientClicked(object sender, EventArgs e)
    {
        // Получаем пациента из параметра кнопки
        if (sender is Button button && button.CommandParameter is Patient patient)
        {
            // Удаляем пациента из коллекции
            Patients.Remove(patient);

            // Удаляем пациента из базы данных через сервис
            if (App.PatientService != null)
            {
                await App.PatientService.DeletePatientAsync(patient);
            }
        }
    }

    private async void OnPatientSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem is Patient selectedPatient)
        {
            // Открываем страницу редактирования
            await Navigation.PushModalAsync(new EditPatientPage(selectedPatient));
        }

        // Сбрасываем выделение
        ((ListView)sender).SelectedItem = null;
    }
}
