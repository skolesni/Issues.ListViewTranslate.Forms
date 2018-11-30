namespace Issues.ListViewTranslate.Forms
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class MainPageViewModel : INotifyPropertyChanged
    {
        private bool isMenuPresented;

        public MainPageViewModel()
        {
            this.HideMenuCommand = new Command(this.HideMenu);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public string Title { get; } = "ListView Transformation Issue";

        public bool IsMenuPresented
        {
            get => this.isMenuPresented;
            set => this.Set(nameof(this.IsMenuPresented), ref this.isMenuPresented, value);
        }

        public IList<string> MenuItems { get; } = new List<string> { "Home", "Messages", "Settings", "About" };

        public ICommand HideMenuCommand { get; }

        private void HideMenu() => this.IsMenuPresented = false;

        private bool Set<T>(string propertyName, ref T field, T newValue)
        {
            if (EqualityComparer<T>.Default.Equals(field, newValue))
            {
                return false;
            }

            field = newValue;
            this.RaisePropertyChanged(propertyName);
            return true;
        }

        private void RaisePropertyChanged(string propertyName)
            => this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
