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
        private void New(object obj)
        {

        }

        [RelayCommand]
        private void SelectMemo(object obj)
        {

        }
        #endregion RelayCommand
    }
}
