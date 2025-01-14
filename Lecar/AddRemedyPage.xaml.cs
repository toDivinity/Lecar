using System.Collections.ObjectModel;

namespace Lecar;

public partial class AddRemedyPage : ContentPage
{
    private readonly ObservableCollection<Models.Remedy> _remedies;

    public AddRemedyPage(ObservableCollection<Models.Remedy> remedies)
    {
        InitializeComponent();
        _remedies = remedies;
    }

    private async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        var newRemedy = new Models.Remedy
        {
            Name = NameEntry.Text ?? string.Empty,
            Illness = IllnessEntry.Text ?? string.Empty
        };

        // Добавляем в базу данных
        await App.Database.AddRemedyAsync(newRemedy);

        // Обновляем коллекцию
        _remedies.Add(newRemedy);

        await Navigation.PopModalAsync();
    }

    private async void OnCancelButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }
}
