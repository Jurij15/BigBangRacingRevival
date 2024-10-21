using BBRRevival.ControlPanel.Model.Messages;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBRRevival.ControlPanel.Pages.ViewModels
{
    public partial class LogViewModel : ObservableRecipient
    {
        public ObservableCollection<string> LogMessages { get; } = new();

        public LogViewModel()
        {
            this.IsActive = true;

            List<string> messages = null;

            try
            {
                messages = Messenger.Send(new LogCacheMessage());
            }
            catch (Exception ex)
            {
                //probably no cached messages
            }

            if (messages is not null)
            {
                foreach (var item in messages)
                {
                    LogMessages.Add(item);
                }
            }
        }

        protected override void OnActivated()
        {
            Messenger.Register<LogViewModel, NewLogMessage>(this, OnNewMessageReceived);

            base.OnActivated();
        }

        private void OnNewMessageReceived(object s, NewLogMessage e)
        {
            App.Current.Dispatcher.Invoke(() => {
                LogMessages.Add(e.Message);
            });
        }
    }
}
