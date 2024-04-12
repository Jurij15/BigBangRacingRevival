using System;

// Token: 0x02000443 RID: 1091
public class ServerQueueItem
{
	// Token: 0x06001E28 RID: 7720 RVA: 0x0015678D File Offset: 0x00154B8D
	public ServerQueueItem(string[] _tags, Action _proceed, Action _serverCall)
	{
		this.m_proceed = _proceed;
		this.m_serverCall = _serverCall;
		this.m_tags = _tags;
	}

	// Token: 0x06001E29 RID: 7721 RVA: 0x001567AC File Offset: 0x00154BAC
	public void Update()
	{
		if (!HttpS.QueueTagsExist(this.m_tags))
		{
			if (this.m_serverCall != null)
			{
				this.m_serverCall.Invoke();
			}
			if (this.m_proceed != null)
			{
				this.m_proceed.Invoke();
			}
			this.done = true;
		}
	}

	// Token: 0x06001E2A RID: 7722 RVA: 0x001567FC File Offset: 0x00154BFC
	~ServerQueueItem()
	{
	}

	// Token: 0x0400217A RID: 8570
	private Action m_serverCall;

	// Token: 0x0400217B RID: 8571
	private Action m_proceed;

	// Token: 0x0400217C RID: 8572
	private string[] m_tags;

	// Token: 0x0400217D RID: 8573
	public bool done;
}
