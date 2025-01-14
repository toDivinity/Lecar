namespace Lecar
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
        }

        private async void OnExitButtonClicked(object sender, EventArgs e)
        {
            // Отображение диалога подтверждения
            bool confirm = await Application.Current.MainPage.DisplayAlert(
                "Выход из приложения",
                "Вы уверены, что хотите выйти?",
                "Да",
                "Нет");

            // Если пользователь подтвердил, выходим из приложения
            if (confirm)
            {
                Application.Current?.Quit();
            }
        }
    }
}
