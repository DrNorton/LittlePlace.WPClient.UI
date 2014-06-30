using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace LittlePlace.WPClient.UI.Controls
{
    public partial class TextBoxCaptioned : UserControl
    {
        public TextBoxCaptioned()
        {
            InitializeComponent();
        }
		
		public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public string Caption
        {
            get { return (string)GetValue(CaptionProperty); }
            set { SetValue(CaptionProperty, value); }
        }


        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(TextBoxCaptioned), new PropertyMetadata(null, TextPropertyChanged));

        public static readonly DependencyProperty CaptionProperty = DependencyProperty.Register("Caption", typeof(string), typeof(TextBoxCaptioned), new PropertyMetadata(null, CaptionPropertyChanged));

        private static void CaptionPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var obj = (TextBoxCaptioned)d;
            var str = (string)e.NewValue;
            obj.Label.Text = str;
        }

        private static void TextPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var obj = (TextBoxCaptioned) d;
            var str=(string)e.NewValue;
            obj.ResTextBox.Text = str;
        }

        private void ResTextBox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            var textbox = (TextBox) sender;
            Text = textbox.Text;
        }
    }
}
