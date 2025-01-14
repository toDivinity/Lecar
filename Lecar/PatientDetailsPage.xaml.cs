using Lecar.Models;

namespace Lecar;

public partial class PatientDetailsPage : ContentPage
{
    public PatientDetailsPage(Patient patient)
    {
        InitializeComponent();
        BindingContext = patient;
    }
}
