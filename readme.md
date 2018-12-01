This solution demonstrates an issue with Xamarin.Forms 3.4 logged at https://github.com/xamarin/Xamarin.Forms/issues/4599

The issue is where the appearance of the ListView control on iOS X devices changes after the ListView is shifted outside the screen boundary.

The description of the application flow:

1. The app starts and navigates to the MainPage.xaml
2. MainPage.xaml initializes, sets SetUseSafeArea to True, and sets the BindingContext to a new instance of MainPageViewModel class
3. The MainPage contains ActionBarControl, which displays "Menu" button and a Title. The properties of the ActionBarControl are bound to the corresponding properties of the ViewModel in MVVM manner
4. When the user taps on the "Menu" button the first time, a new a new instance of NavigationMenuControl is created with transformation on X axis set to be outside of the screen boundary. Then the NavigationMenuControl is attached as a child of the ActionBarControl's top Grid control ("LayoutRoot"). Then, TransformX animation slides the panel from the left.
5. When user taps anywhere of outside of the NavigationMenuControl, the menu slides out to the left by the means of TransformX animation.
6. The next time thh user clicks on tht "Menu" the exsting instance of NavigationMenuControl slides in, and then out when dismissed.






