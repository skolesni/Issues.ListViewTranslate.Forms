namespace Issues.ListViewTranslate.Forms
{
    using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

    public partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();

            this.On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);

            this.BindingContext = new MainPageViewModel();
        }
    }
}
