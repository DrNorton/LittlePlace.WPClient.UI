﻿//      *********    DO NOT MODIFY THIS FILE     *********
//      This file is regenerated by a design tool. Making
//      changes to this file can cause errors.
namespace Expression.Blend.SampleData.FriendViewDataSource
{
	using System; 

// To significantly reduce the sample data footprint in your production application, you can set
// the DISABLE_SAMPLE_DATA conditional compilation constant and disable sample data at runtime.
#if DISABLE_SAMPLE_DATA
	internal class FriendViewDataSource { }
#else

	public class FriendViewDataSource : System.ComponentModel.INotifyPropertyChanged
	{
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}

		public FriendViewDataSource()
		{
			try
			{
				System.Uri resourceUri = new System.Uri("/LittlePlace.WPClient.UI;component/SampleData/FriendViewDataSource/FriendViewDataSource.xaml", System.UriKind.Relative);
				if (System.Windows.Application.GetResourceStream(resourceUri) != null)
				{
					System.Windows.Application.LoadComponent(this, resourceUri);
				}
			}
			catch (System.Exception)
			{
			}
		}

		private Friends _Friends = new Friends();

		public Friends Friends
		{
			get
			{
				return this._Friends;
			}
		}
	}

	public class FriendsItem : System.ComponentModel.INotifyPropertyChanged
	{
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}

		private string _Login = string.Empty;

		public string Login
		{
			get
			{
				return this._Login;
			}

			set
			{
				if (this._Login != value)
				{
					this._Login = value;
					this.OnPropertyChanged("Login");
				}
			}
		}

		private System.Windows.Media.ImageSource _Photo = null;

		public System.Windows.Media.ImageSource Photo
		{
			get
			{
				return this._Photo;
			}

			set
			{
				if (this._Photo != value)
				{
					this._Photo = value;
					this.OnPropertyChanged("Photo");
				}
			}
		}
	}

	public class Friends : System.Collections.ObjectModel.ObservableCollection<FriendsItem>
	{ 
	}
#endif
}
