using System;

// Token: 0x020003BE RID: 958
public class Notification : PsUIPopup
{
	// Token: 0x06001B54 RID: 6996 RVA: 0x00131368 File Offset: 0x0012F768
	public Notification()
		: base(null, null, false, "Popup")
	{
		this.m_duration = 1.0;
		NotificationManager.AddNotification(this);
	}

	// Token: 0x06001B55 RID: 6997 RVA: 0x0013138D File Offset: 0x0012F78D
	public virtual void Start()
	{
		this.m_started = Main.m_gameTimeSinceAppStarted;
	}

	// Token: 0x06001B56 RID: 6998 RVA: 0x0013139A File Offset: 0x0012F79A
	public virtual void End()
	{
		NotificationManager.RemoveNotification(this);
	}

	// Token: 0x04001DB4 RID: 7604
	public double m_started;

	// Token: 0x04001DB5 RID: 7605
	public double m_duration;
}
