using System;

// Token: 0x02000206 RID: 518
public class PsServerQueueFlow : Flow
{
	// Token: 0x06000F17 RID: 3863 RVA: 0x0008FD7E File Offset: 0x0008E17E
	public PsServerQueueFlow(Action _proceed, Action _serverCall, string[] _tags)
		: base(_proceed, null, null)
	{
		PsMetagameManager.m_serverQueueItems.Add(new ServerQueueItem(_tags, _proceed, _serverCall));
	}

	// Token: 0x06000F18 RID: 3864 RVA: 0x0008FD9C File Offset: 0x0008E19C
	public PsServerQueueFlow(Action _proceed, Action _serverCall, string[] _tags, string _textBox)
		: base(_proceed, null, null)
	{
		PsUIBasePopup psUIBasePopup = new PsUIBasePopup(typeof(PsUIPopupServerQueue), null, null, null, false, true, InitialPage.Center, false, false, false);
		psUIBasePopup.SetAction("Server", _serverCall);
		psUIBasePopup.SetAction("Proceed", _proceed);
		(psUIBasePopup.m_mainContent as PsUIPopupServerQueue).CreateContent(_tags, _textBox);
		psUIBasePopup.Update();
	}
}
