using Lecar.Services;
using System.IO;

namespace Lecar;

public partial class App : Application
{
    public static DatabaseService? Database { get; private set; }

    public App()
    {
        InitializeComponent();

        string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Lecar.db");
        Database = new DatabaseService(dbPath);
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Lecar.db");
        Database = new DatabaseService(dbPath);

        return new Window(new AppShell());
    }
}