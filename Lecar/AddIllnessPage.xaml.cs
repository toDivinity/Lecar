using System.Collections.ObjectModel;

namespace Lecar;

public partial class AddIllnessPage : ContentPage
{
    private ObservableCollection<Models.Illness> _illnesses;

    public AddIllnessPage(ObservableCollection<Models.Illness> illnesses)
    {
        InitializeComponent();
        _illnesses = illnesses;
    }

    private async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        var newIllness = new Models.Illness
        {
            Name = NameEntry.Text ?? string.Empty,
            Symptoms = SymptomsEntry.Text ?? string.Empty,
            Remedy = RemedyEntry.Text ?? string.Empty
        };

        // Добавляем болезнь в коллекцию и возвращаемся
        _illnesses.Add(newIllness);
        await App.Database.AddIllnessAsync(newIllness); // Сохранение в базу данных
        await Navigation.PopModalAsync();
    }

    private async void OnCancelButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }
}
