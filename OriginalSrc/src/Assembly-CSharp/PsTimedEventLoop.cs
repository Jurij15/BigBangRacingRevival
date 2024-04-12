using System;
using System.Globalization;

// Token: 0x02000106 RID: 262
public class PsTimedEventLoop : PsGameLoop
{
	// Token: 0x06000624 RID: 1572 RVA: 0x00049C90 File Offset: 0x00048090
	public PsTimedEventLoop(PsMinigameContext _context, string _timedEventId, string _minigameId, PsPlanetPath _path, int _id, int _levelNumber, int _score, int _timeleft, int _duration = -1, string _endTime = null, bool _eventOver = false, bool _domeDestroyed = false)
		: base(_context, _minigameId, _path, -1, _levelNumber, _score, true, null)
	{
		this.m_timedEventId = _timedEventId;
		this.m_eventTimeLeft = _timeleft;
		this.m_eventDuration = _duration;
		this.m_eventOver = _eventOver;
		this.m_domeDestroyed = _domeDestroyed;
		if (_endTime != null)
		{
			this.m_endTime = DateTime.ParseExact(_endTime, "O", CultureInfo.InvariantCulture);
		}
		else
		{
			this.m_endTime = DateTime.Now.AddSeconds((double)this.m_eventTimeLeft);
		}
	}

	// Token: 0x06000625 RID: 1573 RVA: 0x00049D12 File Offset: 0x00048112
	public virtual void SetEventState()
	{
		if (!this.m_eventOver)
		{
			this.m_eventOver = this.IsEventOver();
		}
		this.m_timeRunOut = this.IsEventOver();
	}

	// Token: 0x06000626 RID: 1574 RVA: 0x00049D38 File Offset: 0x00048138
	public void UpdateInfo(int _timeLeftSeconds)
	{
		this.m_eventTimeLeft = _timeLeftSeconds;
		this.m_endTime = DateTime.Now.AddSeconds((double)this.m_eventTimeLeft);
	}

	// Token: 0x06000627 RID: 1575 RVA: 0x00049D66 File Offset: 0x00048166
	public bool IsEventOver()
	{
		return this.SecondsLeft() < 0;
	}

	// Token: 0x06000628 RID: 1576 RVA: 0x00049D74 File Offset: 0x00048174
	public int SecondsLeft()
	{
		DateTime now = DateTime.Now;
		return Convert.ToInt32(this.m_endTime.Subtract(now).TotalSeconds);
	}

	// Token: 0x06000629 RID: 1577 RVA: 0x00049DA0 File Offset: 0x000481A0
	public virtual string GetBannerString()
	{
		if (this.m_eventOver && !this.m_timeRunOut)
		{
			return "Completed";
		}
		if (this.m_timeRunOut)
		{
			return "Expired";
		}
		return PsMetagameManager.GetTimeStringFromSeconds(this.SecondsLeft());
	}

	// Token: 0x0600062A RID: 1578 RVA: 0x00049DDA File Offset: 0x000481DA
	public virtual void HandleEventOver()
	{
		if (!this.m_domeDestroyed)
		{
			this.m_domeDestroyed = true;
			(this.m_node as PsFloatingPlanetNode).DestroyNode();
			PsPlanetManager.m_timedEvents = null;
		}
	}

	// Token: 0x04000759 RID: 1881
	public PsTimedEventType m_type;

	// Token: 0x0400075A RID: 1882
	public double m_eventLastStartTime;

	// Token: 0x0400075B RID: 1883
	public int m_eventTimeLeft;

	// Token: 0x0400075C RID: 1884
	public int m_eventDuration;

	// Token: 0x0400075D RID: 1885
	public DateTime m_endTime;

	// Token: 0x0400075E RID: 1886
	public bool m_eventOver;

	// Token: 0x0400075F RID: 1887
	public bool m_domeDestroyed;

	// Token: 0x04000760 RID: 1888
	public bool m_timeRunOut;

	// Token: 0x04000761 RID: 1889
	public string m_timedEventId;
}
