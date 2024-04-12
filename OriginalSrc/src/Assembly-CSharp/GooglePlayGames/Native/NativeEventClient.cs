using System;
using System.Collections.Generic;
using System.Linq;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.Events;
using GooglePlayGames.Native.PInvoke;
using GooglePlayGames.OurUtils;

namespace GooglePlayGames.Native
{
	// Token: 0x020006BB RID: 1723
	internal class NativeEventClient : IEventsClient
	{
		// Token: 0x0600314D RID: 12621 RVA: 0x001C4784 File Offset: 0x001C2B84
		internal NativeEventClient(EventManager manager)
		{
			this.mEventManager = Misc.CheckNotNull<EventManager>(manager);
		}

		// Token: 0x0600314E RID: 12622 RVA: 0x001C4798 File Offset: 0x001C2B98
		public void FetchAllEvents(DataSource source, Action<ResponseStatus, List<IEvent>> callback)
		{
			Misc.CheckNotNull<Action<ResponseStatus, List<IEvent>>>(callback);
			callback = CallbackUtils.ToOnGameThread<ResponseStatus, List<IEvent>>(callback);
			this.mEventManager.FetchAll(ConversionUtils.AsDataSource(source), delegate(EventManager.FetchAllResponse response)
			{
				ResponseStatus responseStatus = ConversionUtils.ConvertResponseStatus(response.ResponseStatus());
				if (!response.RequestSucceeded())
				{
					callback.Invoke(responseStatus, new List<IEvent>());
				}
				else
				{
					callback.Invoke(responseStatus, Enumerable.ToList<IEvent>(Enumerable.Cast<IEvent>(response.Data())));
				}
			});
		}

		// Token: 0x0600314F RID: 12623 RVA: 0x001C47EC File Offset: 0x001C2BEC
		public void FetchEvent(DataSource source, string eventId, Action<ResponseStatus, IEvent> callback)
		{
			Misc.CheckNotNull<string>(eventId);
			Misc.CheckNotNull<Action<ResponseStatus, IEvent>>(callback);
			this.mEventManager.Fetch(ConversionUtils.AsDataSource(source), eventId, delegate(EventManager.FetchResponse response)
			{
				ResponseStatus responseStatus = ConversionUtils.ConvertResponseStatus(response.ResponseStatus());
				if (!response.RequestSucceeded())
				{
					callback.Invoke(responseStatus, null);
				}
				else
				{
					callback.Invoke(responseStatus, response.Data());
				}
			});
		}

		// Token: 0x06003150 RID: 12624 RVA: 0x001C4837 File Offset: 0x001C2C37
		public void IncrementEvent(string eventId, uint stepsToIncrement)
		{
			Misc.CheckNotNull<string>(eventId);
			this.mEventManager.Increment(eventId, stepsToIncrement);
		}

		// Token: 0x04003278 RID: 12920
		private readonly EventManager mEventManager;
	}
}
