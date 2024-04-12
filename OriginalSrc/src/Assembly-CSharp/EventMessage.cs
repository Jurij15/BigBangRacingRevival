using System;
using System.Collections.Generic;

// Token: 0x02000404 RID: 1028
public class EventMessage
{
	// Token: 0x06001C82 RID: 7298 RVA: 0x00141544 File Offset: 0x0013F944
	public void CopyContent(EventMessage _fromEvent)
	{
		this.eventName = _fromEvent.eventName;
		this.header = _fromEvent.header;
		this.message = _fromEvent.message;
		this.showAsFloatingNode = _fromEvent.showAsFloatingNode;
		this.showAtLogin = _fromEvent.showAtLogin;
		this.showAtNewsFeed = _fromEvent.showAtNewsFeed;
		this.startTime = _fromEvent.startTime;
		this.endTime = _fromEvent.endTime;
		this.endTimeSeconds = _fromEvent.endTimeSeconds;
		this.eventType = _fromEvent.eventType;
		this.eventData = _fromEvent.eventData;
		this.uris = _fromEvent.uris;
		this.eventStates = _fromEvent.eventStates;
	}

	// Token: 0x04001F72 RID: 8050
	public string eventName;

	// Token: 0x04001F73 RID: 8051
	public int messageId;

	// Token: 0x04001F74 RID: 8052
	public string header;

	// Token: 0x04001F75 RID: 8053
	public string message;

	// Token: 0x04001F76 RID: 8054
	public string label;

	// Token: 0x04001F77 RID: 8055
	public bool showAsFloatingNode;

	// Token: 0x04001F78 RID: 8056
	public bool showAtLogin;

	// Token: 0x04001F79 RID: 8057
	public bool showAtNewsFeed;

	// Token: 0x04001F7A RID: 8058
	public long startTime;

	// Token: 0x04001F7B RID: 8059
	public long endTime;

	// Token: 0x04001F7C RID: 8060
	public double endTimeSeconds;

	// Token: 0x04001F7D RID: 8061
	public string eventType;

	// Token: 0x04001F7E RID: 8062
	public Dictionary<string, object> eventData;

	// Token: 0x04001F7F RID: 8063
	public List<string> uris;

	// Token: 0x04001F80 RID: 8064
	public List<EventState> eventStates;

	// Token: 0x04001F81 RID: 8065
	public int timeLeft;

	// Token: 0x04001F82 RID: 8066
	public int localEndTime;

	// Token: 0x04001F83 RID: 8067
	public int timeToStart;

	// Token: 0x04001F84 RID: 8068
	public int localStartTime;

	// Token: 0x04001F85 RID: 8069
	public int previewInHours = 24;

	// Token: 0x04001F86 RID: 8070
	public LiveOps liveOps;

	// Token: 0x04001F87 RID: 8071
	public EventGiftComponent giftContent;

	// Token: 0x04001F88 RID: 8072
	public TournamentInfo tournament;
}
