namespace Lecar
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
        }
        private void OnExitButtonClicked(object sender, EventArgs e)
        {
            // Проверяем, что Application.Current не равен null
            Application.Current?.Quit();
        }
    }
}
