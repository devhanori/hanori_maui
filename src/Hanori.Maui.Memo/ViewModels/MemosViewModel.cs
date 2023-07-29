using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace Hanori.Maui.Memo.ViewModels
{
    internal partial class MemosViewModel : ObservableObject
    {
        public ObservableCollection<ViewModels.MemoViewModel> Memos { get; }

        public MemosViewModel()
        {

        }

        #region RelayCommand
        [RelayCommand]
        private async Task New(object obj)
        {
            await Shell.Current.GoToAsync(nameof(Views.MemoPage));
        }

        [RelayCommand]
        private async Task SelectMemo(ViewModels.MemoViewModel memo)
        {
            if (memo != null)
                await Shell.Current.GoToAsync($"{nameof(Views.MemoPage)}?load={memo.Name}");
        }
        #endregion RelayCommand
    }
}
