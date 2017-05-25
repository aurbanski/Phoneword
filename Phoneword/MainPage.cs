using System;
using Xamarin.Forms;
namespace Phoneword
{
    public class MainPage : ContentPage
    {
        Entry phoneNumberText;
        Button translateButton;
        Button callButton;

        public MainPage()
        {
            this.Padding = new Thickness(20, Device.OnPlatform<double>(40, 20, 20), 20, 20);

            StackLayout panel = new StackLayout
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Orientation = StackOrientation.Vertical,
                Spacing = 15
            };

            panel.Children.Add(new Label
            {
                Text = "Enter a password"
            });

            panel.Children.Add(phoneNumberText = new Entry
			{
				Text = "1-855-XAMARIN"
			});

            panel.Children.Add(translateButton = new Button
			{
				Text = "Translate"
			});

            panel.Children.Add(callButton = new Button
            {
                Text = "Call",
                IsEnabled = false
			});
            translateButton.Clicked += OnTranslate;
            callButton.Clicked += OnCall;
            this.Content = panel;
        }

        private void OnTranslate(object sender, EventArgs e)
        {
            string enteredNumber = phoneNumberText.Text;
            translatedNumber = Core.PhonewordTranslator.ToNumber(enteredNumber);
            if (!string.IsNullOrEmpty(translatedNumber))
            {
                callButton.IsEnabled = true;
                callButton.Text = "Call " + translatedNumber;
            }
            else
            {
                callButton.IsEnabled = false;
				callButton.Text = "Call ";
            }
        }

        async void OnCall(object sender, System.EventArgs e)
        {
            if (await this.DisplayAlert(
                "Dial a number",
                "Would you like to call " + translatedNumber + "?",
                "Yes",
                "No"))
                {
                var dialer = DependencyService.Get<IDialer>();
                if (dialer != null)
                {
                    dialer.Dial(translatedNumber);
                }
            }
        }

        private string translatedNumber;
    }
}
