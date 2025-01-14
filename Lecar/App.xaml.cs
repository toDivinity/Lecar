using Lecar.Services;
using System.IO;

namespace Lecar;

public partial class App : Application
{
    public static string DatabasePath { get; private set; }
    public static DatabaseConnection? DatabaseConnection { get; private set; }
    public static PatientService? PatientService { get; private set; }
    public static IllnessService? IllnessService { get; private set; }
    public static SymptomService? SymptomService { get; private set; }
    public static RemedyService? RemedyService { get; private set; }

    public App()
    {
        InitializeComponent();

        // Укажите путь к базе данных
        DatabasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Lecar.db");
        InitializeDatabase();
    }

    private void InitializeDatabase()
    {
        // Инициализация подключения к базе данных
        DatabaseConnection = new DatabaseConnection(DatabasePath);
        var connection = DatabaseConnection.GetConnection();

        // Инициализация сервисов
        PatientService = new PatientService(connection);
        IllnessService = new IllnessService(connection);
        SymptomService = new SymptomService(connection);
        RemedyService = new RemedyService(connection);
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        var window = new Window(new AppShell())
        {
            Width = 500,
            Height = 750
        };
        return window;
    }
}
