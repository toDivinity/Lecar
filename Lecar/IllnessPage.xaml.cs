using System.Collections.ObjectModel;

namespace Lecar;

public partial class IllnessPage : ContentPage
{
    public ObservableCollection<Models.Illness> Illnesses { get; set; } = new();

    public IllnessPage()
    {
        InitializeComponent();
        BindingContext = this;

        // Загрузка данных из базы
        LoadIllnesses();
    }

    private async void LoadIllnesses()
    {
        var illnesses = await App.Database.GetIllnessesAsync();
        foreach (var illness in illnesses)
        {
            Illnesses.Add(illness);
        }
    }

    private async void OnAddIllnessClicked(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new AddIllnessPage(Illnesses));
    }

    private async void OnDeleteIllnessClicked(object sender, EventArgs e)
    {
        // Получаем болезнь из параметра кнопки
        if (sender is Button button && button.CommandParameter is Models.Illness illness)
        {
            // Удаляем болезнь из коллекции
            Illnesses.Remove(illness);

            // Удаляем болезнь из базы данных
            await App.Database.DeleteIllnessAsync(illness);
        }
    }
}
