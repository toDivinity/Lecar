using System.Collections.ObjectModel;
using Lecar.Models;

namespace Lecar;

public partial class AddRemedyPage : ContentPage
{
    private readonly ObservableCollection<Remedy> _remedies;

    public AddRemedyPage(ObservableCollection<Remedy> remedies)
    {
        InitializeComponent();
        _remedies = remedies;
    }

    private async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        // Преобразуем UnitEntry.Text в число
        int unit = 0;
        if (!string.IsNullOrWhiteSpace(UnitEntry.Text) && int.TryParse(UnitEntry.Text, out int parsedUnit))
        {
            unit = parsedUnit;
        }

        // Создаем новый объект Remedy
        var newRemedy = new Remedy
        {
            Name = NameEntry.Text ?? string.Empty,
            Illness = IllnessEntry.Text ?? string.Empty,
            Unit = unit, // Преобразованное значение
            ReplaceableRemedy = ReplaceableRemedyEntry.Text ?? string.Empty,
        };

        // Добавляем в базу данных через сервис
        if (App.RemedyService != null)
        {
            await App.RemedyService.AddRemedyAsync(newRemedy);
        }

        // Обновляем коллекцию
        _remedies.Add(newRemedy);

        // Возвращаемся на предыдущую страницу
        await Navigation.PopModalAsync();
    }

    private async void OnCancelButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }
}
