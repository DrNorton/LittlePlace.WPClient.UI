using System.Windows.Controls;
using Microsoft.Phone.Controls;

namespace LittlePlace.WPClient.UI.Views.Dialogs
{
    public partial class DialogView : PhoneApplicationPage
    {
        public DialogView()
        {
            InitializeComponent();
        }

        private void TextInput_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            ((PhoneTextBox)sender).GetBindingExpression(PhoneTextBox.TextProperty).UpdateSource();
        }
    }
}