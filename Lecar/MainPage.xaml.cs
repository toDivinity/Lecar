using Lecar.Models;

namespace Lecar;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (App.Database != null)
        {
            var patients = await App.Database.GetPatientsAsync() ?? new List<Patient>();
            PatientsListView.ItemsSource = patients;
        }
        else
        {
            Console.WriteLine("Database is not initialized.");
        }
    }

    private async void OnAddPatientClicked(object sender, EventArgs e)
    {
        // Переход на страницу добавления пациента
        await Navigation.PushAsync(new AddPatientPage());
    }

    private async void OnDeletePatientClicked(object sender, EventArgs e)
    {
        if (App.Database == null)
        {
            Console.WriteLine("Database is not initialized.");
            return;
        }

        // Получение пациента из параметра кнопки
        var button = sender as Button;
        var patient = button?.CommandParameter as Patient;

        if (patient != null)
        {
            // Удаление пациента из базы данных
            await App.Database.DeletePatientAsync(patient);

            // Обновление списка пациентов
            var patients = await App.Database.GetPatientsAsync() ?? new List<Patient>();
            PatientsListView.ItemsSource = patients;
        }
    }
}
