using System.Windows;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Tasks;

namespace Entrance.Views
{
    public partial class AboutView : PhoneApplicationPage
    {
        public AboutView()
        {
            InitializeComponent();
        }

        private void btnFeedback_Click(object sender, RoutedEventArgs e)
        {
            EmailComposeTask emailTask = new EmailComposeTask();
            emailTask.Subject = "EnTrance 1.0 Feedback";
            emailTask.To = "hilltopdev@outlook.com";
            emailTask.Show();
        }

        private void btnRate_Click(object sender, RoutedEventArgs e)
        {
            MarketplaceReviewTask reviewTask = new MarketplaceReviewTask();
            reviewTask.Show();
        }
    }
}