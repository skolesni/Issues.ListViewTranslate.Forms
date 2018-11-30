namespace Issues.ListViewTranslate.Forms.Controls
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Input;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ActionBarControl
    {
        public static readonly BindableProperty TitleProperty =
            BindableProperty.Create(nameof(Title), typeof(string), typeof(ActionBarControl), default(string));

        public static readonly BindableProperty IsMenuPresentedProperty =
            BindableProperty.Create(
                nameof(IsMenuPresented),
                typeof(bool),
                typeof(ActionBarControl),
                default(bool),
                BindingMode.TwoWay,
                null,
                OnIsMenuPresentedPropertyChanged);

        public static readonly BindableProperty MenuItemsProperty =
            BindableProperty.Create(
                nameof(MenuItems),
                typeof(IList<string>),
                typeof(ActionBarControl),
                default(IList<string>),
                BindingMode.TwoWay);

        public static readonly BindableProperty HideMenuCommandProperty =
            BindableProperty.Create(
                nameof(HideMenuCommand),
                typeof(ICommand),
                typeof(ActionBarControl),
                default(ICommand));

        private View menuUnderlay;

        private View menuControl;

        private bool isMenuOpen;

        public ActionBarControl()
        {
            this.InitializeComponent();
            this.LayoutRoot.BindingContext = this;

            this.SizeChanged += this.OnActionBarControlSizeChanged;
        }

        public string Title
        {
            get => (string)this.GetValue(TitleProperty);
            set => this.SetValue(TitleProperty, value);
        }

        public bool IsMenuPresented
        {
            get => (bool)this.GetValue(IsMenuPresentedProperty);
            set => this.SetValue(IsMenuPresentedProperty, value);
        }

        public IList<string> MenuItems
        {
            get => (IList<string>)this.GetValue(MenuItemsProperty);
            set => this.SetValue(MenuItemsProperty, value);
        }

        public ICommand HideMenuCommand
        {
            get => (ICommand)this.GetValue(HideMenuCommandProperty);
            set => this.SetValue(HideMenuCommandProperty, value);
        }

        public ICommand MenuCommand => new Command(() => this.IsMenuPresented = !this.IsMenuPresented);

        private static void OnIsMenuPresentedPropertyChanged(
            BindableObject bindable,
            object oldValue,
            object newValue)
        {
            if (!(bindable is ActionBarControl control))
            {
                return;
            }

            control.ShowMenu((bool)newValue);
        }

        private void ShowMenu(bool show)
        {
            if (this.menuControl == null && !show)
            {
                return;
            }

            if (this.menuControl == null)
            {
                CreateMenu();
            }

            if (this.menuControl == null)
            {
                return;
            }

            if (this.isMenuOpen == show)
            {
                return;
            }

            if (this.isMenuOpen)
            {
                this.menuUnderlay.IsVisible = false;
                this.menuUnderlay.BackgroundColor = Color.FromRgba(0, 0, 0, 0);
                this.menuControl.TranslateTo(-this.menuControl.Width, 0, 400, Easing.CubicOut);
                this.isMenuOpen = false;
            }
            else
            {
                this.isMenuOpen = true;

                this.menuUnderlay.IsVisible = true;
                this.menuUnderlay.BackgroundColor = Color.FromRgba(0, 0, 0, 128);
                this.menuControl.TranslateTo(0, 0, 400, Easing.CubicOut);
            }

            void CreateMenu()
            {
                if (this.menuControl != null)
                {
                    return;
                }

                if (!(this.Parent is Grid parentGrid))
                {
                    return;
                }

                var pageWidth = parentGrid.Width;

                this.menuUnderlay = new ContentView
                {
                    IsVisible = false
                };

                this.menuControl = new NavigationMenuControl
                {
                    TranslationX = -pageWidth,
                    BindingContext = this
                };

                parentGrid.Children.Add(this.menuUnderlay);
                if (parentGrid.RowDefinitions.Count > 0)
                {
                    Grid.SetRowSpan(this.menuUnderlay, parentGrid.RowDefinitions.Count);
                }

                if (parentGrid.ColumnDefinitions.Count > 0)
                {
                    Grid.SetColumnSpan(this.menuUnderlay, parentGrid.ColumnDefinitions.Count);
                }

                parentGrid.Children.Add(this.menuControl);
                if (parentGrid.RowDefinitions.Count > 0)
                {
                    Grid.SetRowSpan(this.menuControl, parentGrid.RowDefinitions.Count);
                }

                if (parentGrid.ColumnDefinitions.Count > 0)
                {
                    Grid.SetColumnSpan(this.menuControl, parentGrid.ColumnDefinitions.Count);
                }
            }
        }

        private void OnActionBarControlSizeChanged(object sender, EventArgs e) => this.UpdateNavigationMenuPanel();

        private void UpdateNavigationMenuPanel()
        {
            if (this.menuControl == null)
            {
                return;
            }

            if (!(this.Parent is Grid parentGrid))
            {
                return;
            }

            var pageWidth = parentGrid.Width;
            if (!this.IsMenuPresented)
            {
                this.menuControl.TranslationX = -pageWidth;
            }
        }
    }
}
