using DAL.Repository.Location;
using DAL.Repository.Weather;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using WeatherApp.Model;
using WeatherApp.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static WeatherApp.Collection.Enumeration;

namespace WeatherApp.View.Master
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SelectLocation : ContentPage
	{
        SetLocationViewModel vm;
        public SelectLocation ()
		{
            vm = new SetLocationViewModel();
            BindingContext = vm;
			InitializeComponent ();

            NavigationPage.SetHasBackButton(this, false);
            NavigationPage.SetHasNavigationBar(this, false);
		}



        protected override async void OnAppearing()
        {
            try
            {
                base.OnAppearing();
                await vm.Init(Navigation);
            }
            catch(Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
            }
        }

        protected override bool OnBackButtonPressed()
        {
            Navigation.PopAsync();
            return base.OnBackButtonPressed();
        }

        private async Task OnSetLocation(object sender, ItemTappedEventArgs e)
        {
            try
            {
                await vm.SetLocation(e.Item as Location);
                await MasterWeather.Instance.Detail.Navigation.PopAsync(true);
            }
            catch (Exception ex) { }
        }

        private async Task NewLocationButton_Clicked(object sender, EventArgs e)
        {
            if (IsValid(NewLocation.Text))
            {
                var exists = await vm.AddNewLocation(NewLocation.Text);

                if (LocationStatus.AlreadyExists == exists)
                {
                    await DisplayAlert("Warning", $"{NewLocation.Text} is already in the list!", "Ok");

                    return;
                }

                if (LocationStatus.NotFound == exists)
                {
                    await DisplayAlert("Warning", $"{NewLocation.Text} was not found!", "Ok");

                    return;
                }

                await Navigation.PopAsync();
            }
        }

        private bool IsValid(string location)
        {
            if(!IsLengthOk(location))
            {
                DisplayAlert("Length error", "The length of the location must be at least 2 characters!", "Ok");

                return false;
            }

            if(!ContainsOnlyLetters(location))
            {
                DisplayAlert("Format error", "The location can contain only letters!", "Ok");

                return false;
            }

            return true;
        }

        private bool ContainsOnlyLetters(string location)
        {
            if(Regex.IsMatch(location, @"^[a-zA-Z]+$"))
            {
                return true;
            }

            return false;
        }

        private bool IsLengthOk(string location)
        {
            if(location.Length > 2)
            {
                return true;
            }

            return false;
        }
    }
}