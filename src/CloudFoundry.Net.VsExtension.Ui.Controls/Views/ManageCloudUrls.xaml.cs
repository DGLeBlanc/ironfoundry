﻿using System.Windows;
using GalaSoft.MvvmLight.Messaging;
using CloudFoundry.Net.VsExtension.Ui.Controls.Utilities;

namespace CloudFoundry.Net.VsExtension.Ui.Controls.Views
{
	/// <summary>
	/// Interaction logic for ManageCloudUrls.xaml
	/// </summary>
	public partial class ManageCloudUrls : Window
	{
		public ManageCloudUrls()
		{
			this.InitializeComponent();
            Messenger.Default.Register<NotificationMessage<bool>>(this,
                message =>
                {
                    if (message.Notification.Equals(Messages.ManageCloudUrlsDialogResult))
                    {
                        this.DialogResult = message.Content;
                        this.Close();
                        Messenger.Default.Unregister(this);
                    }
                });

            Messenger.Default.Register<NotificationMessageAction<bool>>(
                this,
                message =>
                {
                    if (message.Notification.Equals(Messages.AddCloudUrl))
                    {
                        var view = new Views.AddCloudUrl();
                        var result = view.ShowDialog();
                        message.Execute(result.GetValueOrDefault());
                    }
                });

            Messenger.Default.Register<NotificationMessageAction<bool>>(
               this,
               message =>
               {
                   if (message.Notification.Equals(Messages.CreateMicrocloudTarget))
                   {
                       var view = new Views.CreateMicrocloudTarget();
                       var result = view.ShowDialog();
                       message.Execute(result.GetValueOrDefault());
                   }
               });
		}
	}
}