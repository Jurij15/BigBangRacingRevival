using BBRRevival.ControlPanel.Model;
using BBRRevival.ControlPanel.Model.Messages;
using BBRRevival.Services.Events;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BBRRevival.ControlPanel.Pages.ViewModels
{
    public partial class RequestsViewModel : ObservableRecipient
    {
        public ObservableCollection<RequestModel> Requests { get; set; } = new();

        public RequestsViewModel()
        {
            this.IsActive = true;

            List<NewRequestReceivedEventArgs> messages = null;
            try
            {
                messages = Messenger.Send(new RequestCacheMessage());
            }
            catch (Exception ex)
            {
                //probably nothing cached
            }

            if (messages is not null)
            {
                foreach (var item in messages)
                {
                    Requests.Add(new RequestModel(item));
                }
            }
        }

        protected override void OnActivated()
        {
            Messenger.Register<RequestsViewModel, NewRequestMessage>(this, OnNewRequestMessageReceived);

            base.OnActivated();
        }

        private void OnNewRequestMessageReceived(object s, NewRequestMessage e)
        {
            App.Current.Dispatcher.Invoke(() => {
                Requests.Add(new RequestModel(e.args));
            });
        }
    }
}
