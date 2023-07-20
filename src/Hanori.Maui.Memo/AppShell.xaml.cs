namespace Hanori.Maui.Memo
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(Views.MemoPage), typeof(Views.MemoPage));
        }
    }
}