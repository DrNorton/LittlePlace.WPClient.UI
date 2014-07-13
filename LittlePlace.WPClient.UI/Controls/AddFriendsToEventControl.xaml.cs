using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using LittlePlace.Api.ApiRequest.Commands.Result;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace LittlePlace.WPClient.UI.Controls
{
    public partial class AddFriendsToEventControl : UserControl
    {
        public List<User> Friends
        {
            get { return (List<User>)GetValue(FriendsProperty); }
            set { SetValue(FriendsProperty, value); }
        }

        public List<User> SelectedFriends
        {
            get { return (List<User>)GetValue(SelectedFriendsProperty); }
            set { SetValue(SelectedFriendsProperty, value); }
        }

        public static readonly DependencyProperty FriendsProperty = DependencyProperty.Register("Friends", typeof(List<User>), typeof(AddFriendsToEventControl), new PropertyMetadata(OnFriendsChanged));

        public static readonly DependencyProperty SelectedFriendsProperty = DependencyProperty.Register("SelectedFriends", typeof(List<User>), typeof(AddFriendsToEventControl), new PropertyMetadata(OnSelectedFriendsChanged));

        private static void OnSelectedFriendsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var self = (AddFriendsToEventControl)d;
            self.SelectedFriends = (List<User>) e.NewValue;
        }

        private static void OnFriendsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var self = (AddFriendsToEventControl)d;
            self.Friends = (List<User>)e.NewValue;
            self.FriendSelectList.ItemsSource = self.Friends;
        }


        public AddFriendsToEventControl()
        {
            InitializeComponent();
            
        }

        private void MultiselectList_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedFriends = e.AddedItems.Cast<User>().ToList();
        }
    }
}
