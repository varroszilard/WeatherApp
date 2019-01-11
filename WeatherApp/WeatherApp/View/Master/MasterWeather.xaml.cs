using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Common.UI.ContentViews;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WeatherApp.View.Master
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterWeather : MasterDetailPage
    {
        public static MasterWeather Instance { get; set; }

        public MasterWeather()
        {
            InitializeComponent();
            Instance = this;

            Detail = new NavigationPage(new MainPage());
        }

        private async void OnCelsiusClicked(object sender, EventArgs e)
        {

        }

        private async void OnFahrenheitClicked(object sender, EventArgs e)
        {

        }

        private async void OnSelectedMenuButton(object sender, EventArgs e)
        {
            try
            {
                var button = sender as MenuButton;
                var myType = Type.GetType("WeatherApp.View.Master." + button.TargetType);
                var page = (Page)Activator.CreateInstance(myType);

                await Detail.Navigation.PushAsync(new NavigationPage(page));
                IsPresented = false;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

        }
    }
}