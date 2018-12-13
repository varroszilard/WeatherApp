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
	public partial class CustomButton : ContentView
	{
        public CustomButton()
        {
            InitializeComponent();
        }

        public string NumberText
        {
            get { return GetValue(NumberTextProperty).ToString(); }
            set { SetValue(NumberTextProperty, value); }
        }

        public static readonly BindableProperty NumberTextProperty = BindableProperty.Create("NumberText", typeof(string), typeof(CustomButton), "", BindingMode.TwoWay, propertyChanged: NumberTextPropertyChanged);

        private static void NumberTextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (CustomButton)bindable;
            control.number.Text = newValue.ToString();
        }

        public string TitleText
        {
            get { return GetValue(TitleTextProperty).ToString(); }
            set { SetValue(TitleTextProperty, value); }
        }

        public static readonly BindableProperty TitleTextProperty = BindableProperty.Create("TitleText", typeof(string), typeof(CustomButton), "", BindingMode.TwoWay, propertyChanged: TitleTextPropertyChanged);

        private static void TitleTextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (CustomButton)bindable;
            control.title.Text = newValue.ToString();
        }

        public event Action<object> OnTapped;

        public void OnTextTapped(object sender, EventArgs e)
        {
            OnTapped?.Invoke(this);
        }
    }
}