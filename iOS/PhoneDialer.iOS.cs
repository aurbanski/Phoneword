using Foundation;
using UIKit;
using Phoneword.iOS;

[assembly: Dependency(typeof(PhoneDialer))]

namespace Phoneword.iOS
{
    public class PhoneDialer : IDialer
    {
        public bool Dial(string number)
        {
            return UIApplication.SharedApplication.OpenUrl(
				new NSUrl("tel:" + number));
        }
    }
}
