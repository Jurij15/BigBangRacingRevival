using System;
using System.Collections.Generic;

namespace GooglePlayGames.BasicApi.Events
{
	// Token: 0x020005D0 RID: 1488
	public interface IEventsClient
	{
		// Token: 0x06002B3D RID: 11069
		void FetchAllEvents(DataSource source, Action<ResponseStatus, List<IEvent>> callback);

		// Token: 0x06002B3E RID: 11070
		void FetchEvent(DataSource source, string eventId, Action<ResponseStatus, IEvent> callback);

		// Token: 0x06002B3F RID: 11071
		void IncrementEvent(string eventId, uint stepsToIncrement);
	}
}
