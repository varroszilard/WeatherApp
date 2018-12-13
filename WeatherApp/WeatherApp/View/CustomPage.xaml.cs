using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Common.UI.ContentViews;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WeatherApp.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CustomPage : ContentPage
	{
		public CustomPage ()
		{
			InitializeComponent ();
		}

        private void DisplayCustomText(object obj)
        {
            var cb = obj as CustomButton;

            DisplayAlert("OnTapped raised", $"Number: {cb.NumberText} Text: {cb.TitleText}", "Ok");
        }
    }
}