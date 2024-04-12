using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

// Token: 0x020004C9 RID: 1225
public class HttpC : BasicComponent
{
	// Token: 0x060022C5 RID: 8901 RVA: 0x00190D74 File Offset: 0x0018F174
	public HttpC()
		: base(ComponentType.Http)
	{
		HttpC.m_componentCount++;
	}

	// Token: 0x14000003 RID: 3
	// (add) Token: 0x060022C6 RID: 8902 RVA: 0x00190D8C File Offset: 0x0018F18C
	// (remove) Token: 0x060022C7 RID: 8903 RVA: 0x00190DC4 File Offset: 0x0018F1C4
	[field: DebuggerBrowsable(0)]
	public event Action<HttpC> requestComplete;

	// Token: 0x14000004 RID: 4
	// (add) Token: 0x060022C8 RID: 8904 RVA: 0x00190DFC File Offset: 0x0018F1FC
	// (remove) Token: 0x060022C9 RID: 8905 RVA: 0x00190E34 File Offset: 0x0018F234
	[field: DebuggerBrowsable(0)]
	public event Action<HttpC> requestFailed;

	// Token: 0x060022CA RID: 8906 RVA: 0x00190E6C File Offset: 0x0018F26C
	public override void Reset()
	{
		base.Reset();
		this.www = null;
		this.url = null;
		this.tag = null;
		this.queueTags = null;
		this.requestComplete = null;
		this.requestFailed = null;
		this.disposeWhenDestroyed = true;
		this.canRemove = false;
		this.ignoreInQueue = false;
		this.timeOut = 0.0;
		this.retryCount = 3;
		this.objectData = null;
		this.executed = false;
		this.debugKeyCode = 0;
		this.wwwUrl = null;
		this.wwwData = null;
		this.wwwHeaders = null;
	}

	// Token: 0x060022CB RID: 8907 RVA: 0x00190F00 File Offset: 0x0018F300
	public void FireRequest()
	{
		if (this.wwwHeaders == null)
		{
			this.www = new WWW(this.wwwUrl, this.wwwData);
		}
		else
		{
			this.www = new WWW(this.wwwUrl, this.wwwData, this.wwwHeaders);
		}
	}

	// Token: 0x060022CC RID: 8908 RVA: 0x00190F51 File Offset: 0x0018F351
	public void SetDebug(KeyCode _executeKeyCode)
	{
		this.debugKeyCode = _executeKeyCode;
		this.destroyAfterDone = false;
	}

	// Token: 0x060022CD RID: 8909 RVA: 0x00190F61 File Offset: 0x0018F361
	public void RequestComplete()
	{
		if (this.requestComplete != null && !this.executed)
		{
			this.requestComplete.Invoke(this);
		}
		this.executed = true;
		this.canRemove = true;
	}

	// Token: 0x060022CE RID: 8910 RVA: 0x00190F94 File Offset: 0x0018F394
	public void RequestFailed()
	{
		if (this.retryCount > 0)
		{
			this.retryCount--;
			this.timeOut = Main.m_gameTimeSinceAppStarted + HttpS.m_localTimeOut;
			this.FireRequest();
		}
		else
		{
			if (this.requestFailed != null && !this.executed)
			{
				this.requestFailed.Invoke(this);
			}
			this.executed = true;
			this.canRemove = true;
		}
	}

	// Token: 0x060022CF RID: 8911 RVA: 0x00191007 File Offset: 0x0018F407
	public override void Destroy()
	{
		base.Destroy();
		this.requestComplete = null;
		this.requestFailed = null;
	}

	// Token: 0x060022D0 RID: 8912 RVA: 0x00191020 File Offset: 0x0018F420
	~HttpC()
	{
		HttpC.m_componentCount--;
	}

	// Token: 0x04002901 RID: 10497
	public static int m_componentCount;

	// Token: 0x04002904 RID: 10500
	public WWW www;

	// Token: 0x04002905 RID: 10501
	public string url;

	// Token: 0x04002906 RID: 10502
	public string tag;

	// Token: 0x04002907 RID: 10503
	public string[] queueTags;

	// Token: 0x04002908 RID: 10504
	public bool destroyAfterDone;

	// Token: 0x04002909 RID: 10505
	public bool canRemove;

	// Token: 0x0400290A RID: 10506
	public bool disposeWhenDestroyed;

	// Token: 0x0400290B RID: 10507
	public bool executed;

	// Token: 0x0400290C RID: 10508
	public bool ignoreInQueue;

	// Token: 0x0400290D RID: 10509
	public double timeOut;

	// Token: 0x0400290E RID: 10510
	public object objectData;

	// Token: 0x0400290F RID: 10511
	public int retryCount;

	// Token: 0x04002910 RID: 10512
	public KeyCode debugKeyCode;

	// Token: 0x04002911 RID: 10513
	public string wwwUrl;

	// Token: 0x04002912 RID: 10514
	public byte[] wwwData;

	// Token: 0x04002913 RID: 10515
	public Dictionary<string, string> wwwHeaders;
}
