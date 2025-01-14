using System.Collections.ObjectModel;
using Lecar.Models;

namespace Lecar;

public partial class SymptomsPage : ContentPage
{
    public ObservableCollection<Symptom> Symptoms { get; set; } = new();

    public SymptomsPage()
    {
        InitializeComponent();
        BindingContext = this;

        // Загрузка данных из базы
        LoadSymptoms();
    }

    private async void LoadSymptoms()
    {
        if (App.SymptomService != null)
        {
            var symptoms = await App.SymptomService.GetSymptomsAsync();
            foreach (var symptom in symptoms)
            {
                Symptoms.Add(symptom);
            }
        }
    }

    private async void OnAddSymptomClicked(object sender, EventArgs e)
    {
        // Открываем страницу добавления симптома
        await Navigation.PushModalAsync(new AddSymptomPage(Symptoms));
    }
}
