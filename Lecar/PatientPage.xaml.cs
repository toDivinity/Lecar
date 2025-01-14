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
            // Показываем подтверждение удаления
            bool confirm = await DisplayAlert(
                "Удаление пациента",
                $"Вы уверены, что хотите удалить пациента {patient.Name}?",
                "Да",
                "Нет");

            if (confirm)
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
    }

    private async void OnPatientSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is Patient selectedPatient)
        {
            // Открываем страницу с данными пациента
            await Navigation.PushAsync(new PatientDetailsPage(selectedPatient));
        }

        // Сбрасываем выделение
        ((CollectionView)sender).SelectedItem = null;
    }
}
