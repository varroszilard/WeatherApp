using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace WeatherApp.Common.UI.Behaviors
{
    public class LocationLengthValidationBehavior : Behavior<Entry>
    {
        protected override void OnAttachedTo(Entry entry)
        {
            entry.TextChanged += OnEntryTextChanged;
            base.OnAttachedTo(entry);
        }

        protected override void OnDetachingFrom(Entry entry)
        {
            entry.TextChanged += OnEntryTextChanged;
            base.OnDetachingFrom(entry);
        }

        private void OnEntryTextChanged(object sender, TextChangedEventArgs e)
        {
            bool lengthIsOk = e.NewTextValue.Length > 2 ? true : false;
            ((Entry)sender).TextColor = lengthIsOk ? Color.Default : Color.Red;
        }
    }
}
