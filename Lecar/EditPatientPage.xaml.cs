namespace Lecar;

public partial class EditPatientPage : ContentPage
{
    private readonly Models.Patient _patient;

    public EditPatientPage(Models.Patient patient)
    {
        InitializeComponent();
        _patient = patient;

        // Привязка данных пациента к форме
        NameEntry.Text = _patient.Name;
        AgeEntry.Text = _patient.Age.ToString();
        SymptomsEntry.Text = _patient.Symptoms;
    }

    private async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        // Обновляем данные пациента
        _patient.Name = NameEntry.Text ?? string.Empty;
        _patient.Age = int.TryParse(AgeEntry.Text, out var age) ? age : _patient.Age;
        _patient.Symptoms = SymptomsEntry.Text ?? string.Empty;

        // Сохраняем изменения в базе данных через сервис
        if (App.PatientService != null)
        {
            await App.PatientService.UpdatePatientAsync(_patient);
        }

        // Возвращаемся на предыдущую страницу
        await Navigation.PopModalAsync();
    }

    private async void OnCancelButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }
}
