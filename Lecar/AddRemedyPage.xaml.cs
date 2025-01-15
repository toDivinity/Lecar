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
        // Проверка на заполнение названия
        if (string.IsNullOrWhiteSpace(NameEntry.Text))
        {
            await DisplayAlert("Ошибка", "Пожалуйста, введите название лекарства.", "ОК");
            return;
        }

        // Проверка на заполнение количества
        if (string.IsNullOrWhiteSpace(UnitEntry.Text) || !int.TryParse(UnitEntry.Text, out var unit) || unit < 0)
        {
            await DisplayAlert("Ошибка", "Пожалуйста, введите корректное количество (целое число больше нуля).", "ОК");
            return;
        }

        // Создаем новый объект Remedy
        var newRemedy = new Remedy
        {
            Name = NameEntry.Text.Trim(),
            Unit = unit
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
