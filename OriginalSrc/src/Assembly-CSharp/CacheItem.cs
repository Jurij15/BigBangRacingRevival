using System;

// Token: 0x020004B2 RID: 1202
public class CacheItem<T>
{
	// Token: 0x06002251 RID: 8785 RVA: 0x0018EDEC File Offset: 0x0018D1EC
	public CacheItem(string _key, T _content, float _expireDurationMins = -1f)
	{
		this.m_key = _key;
		this.m_content = _content;
		this.m_expireDurationMins = (double)_expireDurationMins;
		this.m_expires = this.m_expireDurationMins > 0.0;
		if (this.m_expires)
		{
			this.m_expirationTime = Main.m_gameTimeSinceAppStarted + this.m_expireDurationMins * 60.0;
		}
	}

	// Token: 0x06002252 RID: 8786 RVA: 0x0018EE5D File Offset: 0x0018D25D
	public T GetContent()
	{
		if (this.m_expires)
		{
			this.m_expirationTime = Main.m_gameTimeSinceAppStarted + this.m_expireDurationMins * 60.0;
		}
		return this.m_content;
	}

	// Token: 0x06002253 RID: 8787 RVA: 0x0018EE8C File Offset: 0x0018D28C
	public void SetContent(T _content)
	{
		if (this.m_expires)
		{
			this.m_expirationTime = Main.m_gameTimeSinceAppStarted + this.m_expireDurationMins * 60.0;
		}
		this.m_content = _content;
	}

	// Token: 0x06002254 RID: 8788 RVA: 0x0018EEBC File Offset: 0x0018D2BC
	public bool IsExpired()
	{
		return this.m_expires && Main.m_gameTimeSinceAppStarted > this.m_expirationTime;
	}

	// Token: 0x06002255 RID: 8789 RVA: 0x0018EEE2 File Offset: 0x0018D2E2
	public void Refresh()
	{
		if (this.m_expires)
		{
			this.m_expirationTime = Main.m_gameTimeSinceAppStarted + this.m_expireDurationMins * 60.0;
		}
	}

	// Token: 0x04002885 RID: 10373
	public string m_key;

	// Token: 0x04002886 RID: 10374
	public T m_content;

	// Token: 0x04002887 RID: 10375
	private bool m_expires;

	// Token: 0x04002888 RID: 10376
	private double m_expireDurationMins;

	// Token: 0x04002889 RID: 10377
	private double m_expirationTime;
}
