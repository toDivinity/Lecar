using System.Collections.ObjectModel;

namespace Lecar;

public partial class RemedyPage : ContentPage
{
    public ObservableCollection<Models.Remedy> Remedies { get; set; } = new();

    public RemedyPage()
    {
        InitializeComponent();
        BindingContext = this;

        // Загрузка данных из базы
        LoadRemedies();
    }

    private async void LoadRemedies()
    {
        var remedies = await App.Database.GetRemediesAsync();
        foreach (var remedy in remedies)
        {
            Remedies.Add(remedy);
        }
    }

    private async void OnAddRemedyClicked(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new AddRemedyPage(Remedies));
    }

    private async void OnDeleteRemedyClicked(object sender, EventArgs e)
    {
        // Получаем лекарство из параметра кнопки
        if (sender is Button button && button.CommandParameter is Models.Remedy remedy)
        {
            // Удаляем лекарство из коллекции
            Remedies.Remove(remedy);

            // Удаляем лекарство из базы данных
            await App.Database.DeleteRemedyAsync(remedy);
        }
    }
}
