using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace WeatherApp.Common.UI.Behaviors
{
    public class StringValidationBehavior: Behavior<Entry>
    {
        protected override void OnAttachedTo(Entry entry)
        {
            entry.TextChanged += OnTextChanged;
            base.OnAttachedTo(entry);
        }

        protected override void OnDetachingFrom(Entry entry)
        {
            entry.TextChanged -= OnTextChanged;
            base.OnDetachingFrom(entry);
        }

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            bool isString = Regex.IsMatch(e.NewTextValue, @"^[a-zA-Z]+$");
            ((Entry)sender).TextColor = isString ? Color.Default : Color.Red;
        }
    }
}
