using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Hanori.Maui.Memo.ViewModels
{
    
    public partial class MemoViewModel : ObservableObject
    {
        private Models.MemoItem _memo;

        [ObservableProperty]
        private string text;
        public DateTime Date => _memo.Date;

        public string Name => _memo.Name;

        [RelayCommand]
        private async Task Save()
        {

        }
    }
}
