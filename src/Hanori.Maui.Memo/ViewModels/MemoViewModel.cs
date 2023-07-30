using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Hanori.Maui.Memo.ViewModels
{
    
    public partial class MemoViewModel : ObservableObject, IQueryAttributable
    {
        private Models.Memo _memo;

        public MemoViewModel()
        {
            _memo = new Models.Memo();
        }

        public MemoViewModel(Models.Memo memo)
        {
            _memo = memo;
        }

        public string Text
        {
            get => _memo.Text;
            set
            {
                if(_memo.Text != value)
                {
                    _memo.Text = value;
                    OnPropertyChanged();
                }
            }
        }

        public DateTime Date => _memo.Date;

        public string Name => _memo.Name;

        [RelayCommand]
        private async Task Save()
        {
            _memo.Date = DateTime.Now;
            _memo.Save();
            await Shell.Current.GoToAsync($"..?saved={_memo.Name}");
        }

        void IQueryAttributable.ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.ContainsKey("load"))
            {
                _memo = Models.Memo.ReadOne(query["load"].ToString());
                RefreshProperties();
            }
        }

        public void ReRead()
        {
            _memo = Models.Memo.ReadOne(_memo.Name);
            RefreshProperties();
        }

        private void RefreshProperties()
        {
            OnPropertyChanged(nameof(Text));
            OnPropertyChanged(nameof(Date));
        }
    }
}
