using System.Collections.ObjectModel;
using Lecar.Models;

namespace Lecar;

public partial class RemedyPage : ContentPage
{
    public ObservableCollection<Remedy> Remedies { get; set; } = new();

    public RemedyPage()
    {
        InitializeComponent();
        BindingContext = this;

        // Загрузка данных из базы
        LoadRemedies();
    }

    private async void LoadRemedies()
    {
        if (App.RemedyService != null)
        {
            var remedies = await App.RemedyService.GetRemediesAsync();
            foreach (var remedy in remedies)
            {
                Remedies.Add(remedy);
            }
        }
    }

    private async void OnAddRemedyClicked(object sender, EventArgs e)
    {
        // Открываем страницу добавления лекарства
        await Navigation.PushModalAsync(new AddRemedyPage(Remedies));
    }

    private async void OnDeleteRemedyClicked(object sender, EventArgs e)
    {
        // Получаем лекарство из параметра кнопки
        if (sender is Button button && button.CommandParameter is Remedy remedy)
        {
            // Удаляем лекарство из коллекции
            Remedies.Remove(remedy);

            // Удаляем лекарство из базы данных через сервис
            if (App.RemedyService != null)
            {
                await App.RemedyService.DeleteRemedyAsync(remedy);
            }
        }
    }
}
