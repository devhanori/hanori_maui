using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace Hanori.Maui.Memo.ViewModels
{
    internal partial class MemosViewModel : ObservableObject, IQueryAttributable
    {
        public ObservableCollection<ViewModels.MemoViewModel> Memos { get; }

        public MemosViewModel()
        {
            Memos = new ObservableCollection<ViewModels.MemoViewModel>(Models.Memo
                .ReadAll()
                .Select(n => new MemoViewModel(n)));
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

        void IQueryAttributable.ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.ContainsKey("deleted"))
            {
                string noteId = query["deleted"].ToString();
                MemoViewModel matchedNote = Memos.Where((n) => n.Name == noteId).FirstOrDefault();

                // If note exists, delete it
                if (matchedNote != null)
                    Memos.Remove(matchedNote);
            }
            else if (query.ContainsKey("saved"))
            {
                string noteId = query["saved"].ToString();
                MemoViewModel matchedNote = Memos.Where((n) => n.Name == noteId).FirstOrDefault();

                // If note is found, update it
                if (matchedNote != null)
                {
                    matchedNote.ReRead();
                    Memos.Move(Memos.IndexOf(matchedNote), 0);
                }
                // If note isn't found, it's new; add it.
                else
                    Memos.Insert(0, new MemoViewModel(Models.Memo.ReadOne(noteId)));
            }
        }
    }
}
