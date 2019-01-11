using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WeatherApp.Common.UI.ContentViews
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MenuButton : ContentView
	{
        public MenuButton ()
		{
			InitializeComponent ();
		}

        public string ImageIcon
        {
            get { return GetValue(ImageIconProperty).ToString(); }
            set { SetValue(ImageIconProperty, value); }
        }

        public static readonly BindableProperty ImageIconProperty = BindableProperty.Create("ImageIcon", typeof(string), typeof(MenuButton), defaultBindingMode: BindingMode.TwoWay, propertyChanged: ImageIconPropertyChanged);

        private static void ImageIconPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (MenuButton)bindable;
            control.MenuIcon.Source = newValue.ToString();
        }

        public string MyMenuText
        {
            get { return GetValue(MenuTextProperty).ToString(); }
            set { SetValue(MenuTextProperty, value); }
        }

        public static readonly BindableProperty MenuTextProperty = BindableProperty.Create("MyMenuText", typeof(string), typeof(MenuButton), defaultBindingMode: BindingMode.TwoWay, propertyChanged: MenuTextPropertyChanged);

        private static void MenuTextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (MenuButton)bindable;
            control.MenuText.Text = newValue.ToString();
        }

        public string TargetType
        {
            get { return GetValue(TargetTypeProperty).ToString(); }
            set { SetValue(TargetTypeProperty, value); }
        }

        public static readonly BindableProperty TargetTypeProperty = BindableProperty.Create("TargetType", typeof(string), typeof(MenuButton), defaultBindingMode: BindingMode.TwoWay);

        public event Action<object> OnMenuButtonTapped;

        public void OnMenuTapped(object sender, EventArgs e)
        {
            OnMenuButtonTapped?.Invoke(this);
        }
    }
}