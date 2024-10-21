using BBRRevival.ControlPanel.Model.Messages;
using BBRRevival.ControlPanel.Services;
using CommunityToolkit.Mvvm.Messaging;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BBRRevival.ControlPanel
{
    public class CustomFormatProvider : IFormatProvider, ICustomFormatter
    {
        // Returns the format for a specific type
        public object GetFormat(Type formatType)
        {
            if (formatType == typeof(ICustomFormatter))
            {
                return this;
            }
            return null;
        }

        public string Format(string format, object arg, IFormatProvider formatProvider)
        {
            if (arg is LogEvent logEvent)
            {
                // Create a custom format for the log message
                return $"[{logEvent.Timestamp:yyyy-MM-dd HH:mm:ss}] [{logEvent.Level}] {logEvent.RenderMessage()}";
            }

            // Default formatting for other types
            return arg?.ToString() ?? string.Empty;
        }
    }


    public class CustomLogSink : ILogEventSink
    {
        public event Action<LogEvent> LogEventReceived;

        private List<string> _logs { get; set; } = new();

        private readonly IFormatProvider _formatProvider;

        public CustomLogSink(IFormatProvider formatProvider)
        {
            //Register the log cache message
            WeakReferenceMessenger.Default.Register<CustomLogSink, LogCacheMessage>(this, (r, m) =>
            {
                m.Reply(_logs);
            });

            _formatProvider = formatProvider;
        }

        public void Emit(LogEvent logEvent)
        {
            LogEventReceived?.Invoke(logEvent);

            HandleLogEvent(logEvent);
        }

        private void HandleLogEvent(LogEvent logEvent)
        {
            var message = string.Format(_formatProvider, "{0}", logEvent);

            _logs.Add(message);
            WeakReferenceMessenger.Default.Send(new NewLogMessage() { Message = message });
        }


    }
}