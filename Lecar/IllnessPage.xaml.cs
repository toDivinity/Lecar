using System.Collections.ObjectModel;
using Lecar.Models;

namespace Lecar;

public partial class IllnessPage : ContentPage
{
    public ObservableCollection<Illness> Illnesses { get; set; } = new();

    public IllnessPage()
    {
        InitializeComponent();
        BindingContext = this;

        // Загрузка данных из базы
        LoadIllnesses();
    }

    private async void LoadIllnesses()
    {
        if (App.IllnessService != null)
        {
            var illnesses = await App.IllnessService.GetIllnessesAsync();
            foreach (var illness in illnesses)
            {
                Illnesses.Add(illness);
            }
        }
    }

    private async void OnAddIllnessClicked(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new AddIllnessPage(Illnesses));
    }

    private async void OnDeleteIllnessClicked(object sender, EventArgs e)
    {
        // Получаем болезнь из параметра кнопки
        if (sender is Button button && button.CommandParameter is Illness illness)
        {
            // Показываем подтверждение удаления
            bool confirm = await DisplayAlert(
                "Удаление болезни",
                $"Вы уверены, что хотите удалить болезнь \"{illness.Name}\"?",
                "Да",
                "Нет");

            if (confirm)
            {
                // Удаляем болезнь из коллекции
                Illnesses.Remove(illness);

                // Удаляем болезнь из базы данных через сервис
                if (App.IllnessService != null)
                {
                    await App.IllnessService.DeleteIllnessAsync(illness);
                }
            }
        }
    }
}
