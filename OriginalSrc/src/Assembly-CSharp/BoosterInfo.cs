using System;
using System.Collections;
using CodeStage.AntiCheat.ObscuredTypes;

// Token: 0x020000F2 RID: 242
public class BoosterInfo
{
	// Token: 0x0600054E RID: 1358 RVA: 0x00046019 File Offset: 0x00044419
	public BoosterInfo(Type _vehicleType, string _serverIdentifier, int _count)
	{
		this.m_vehicleType = _vehicleType;
		this.m_serverIdentifier = _serverIdentifier;
		this.m_count = _count;
		this.m_reloadTimeSeconds = PsMetagameManager.GetSecondsFromTimeString(PsMetagameManager.GetKeyReloadTime());
	}

	// Token: 0x0600054F RID: 1359 RVA: 0x0004604C File Offset: 0x0004444C
	public void UseOne(Hashtable _data)
	{
		Debug.Log("E_Test UseOne", null);
		int num = 1;
		if (this.m_count > 0)
		{
			this.m_reloadTimeLeftPrevious = -1;
			this.m_reloadTimeSeconds = PsMetagameManager.GetSecondsFromTimeString(PsMetagameManager.GetKeyReloadTime());
			this.m_reloadTimeLeft = this.m_reloadTimeSeconds;
			this.m_lastKeyUsedTime = Main.m_EPOCHSeconds;
			this.Cumulate(-num);
			Hashtable hashtable = new Hashtable();
			Debug.Log("-------------------reloadtimeleft: " + this.m_reloadTimeLeft, null);
			hashtable.Add(this.m_serverIdentifier + "BoosterRefresh", this.m_reloadTimeLeft);
			_data.Add("Resources", hashtable);
			Debug.Log(this.m_vehicleType.ToString() + " booster USED ######################## left: " + this.m_count, null);
		}
	}

	// Token: 0x06000550 RID: 1360 RVA: 0x00046121 File Offset: 0x00044521
	public int GetMaxCount()
	{
		return 6 + (int)PsUpgradeManager.GetCurrentEfficiency(this.m_vehicleType, PsUpgradeManager.UpgradeType.NITRO_BOOST_COUNT);
	}

	// Token: 0x06000551 RID: 1361 RVA: 0x00046134 File Offset: 0x00044534
	public void Update(ref Hashtable _hashtable)
	{
		if (this.m_count < this.GetMaxCount())
		{
			this.m_reloadTimeUpdated = false;
			this.m_reloadTimeLeft = (int)Math.Ceiling(this.m_lastKeyUsedTime + (double)this.m_reloadTimeSeconds - Main.m_EPOCHSeconds);
			if (this.m_reloadTimeLeft != this.m_reloadTimeLeftPrevious)
			{
				if (this.m_reloadTimeLeft <= 0)
				{
					this.Reload(ref _hashtable);
				}
				this.m_reloadTimeUpdated = true;
				this.m_reloadTimeLeftPrevious = this.m_reloadTimeLeft;
			}
		}
	}

	// Token: 0x06000552 RID: 1362 RVA: 0x000461B8 File Offset: 0x000445B8
	public void Reload(ref Hashtable _hashtable)
	{
		Debug.Log("E_Test Reload", null);
		int num = 1;
		if (_hashtable == null)
		{
			_hashtable = new Hashtable();
		}
		this.Cumulate(num);
		if (this.m_count < this.GetMaxCount())
		{
			Hashtable hashtable;
			if (_hashtable.ContainsKey("Resources"))
			{
				hashtable = (Hashtable)_hashtable["Resources"];
			}
			else
			{
				hashtable = new Hashtable();
				_hashtable.Add("Resources", hashtable);
			}
			this.m_reloadTimeSeconds = PsMetagameManager.GetSecondsFromTimeString(PsMetagameManager.GetKeyReloadTime());
			this.m_reloadTimeLeftPrevious = -1;
			this.m_reloadTimeLeft = this.m_reloadTimeSeconds;
			this.m_lastKeyUsedTime = Main.m_EPOCHSeconds;
			hashtable.Add(this.m_serverIdentifier + "BoosterRefresh", this.m_reloadTimeLeft);
		}
		Debug.Log(string.Concat(new object[] { "Booster RELOADED: ", this.m_serverIdentifier, ": count: ", this.m_count }), null);
	}

	// Token: 0x06000553 RID: 1363 RVA: 0x000462C1 File Offset: 0x000446C1
	public void Cumulate(int _amount)
	{
		this.m_count = Math.Min(this.m_count + _amount, this.GetMaxCount());
	}

	// Token: 0x06000554 RID: 1364 RVA: 0x000462E6 File Offset: 0x000446E6
	public void Refill(int _amount)
	{
		this.m_count = Math.Min(this.m_count + _amount, this.GetMaxCount());
	}

	// Token: 0x040006D4 RID: 1748
	public Type m_vehicleType;

	// Token: 0x040006D5 RID: 1749
	public string m_serverIdentifier;

	// Token: 0x040006D6 RID: 1750
	public ObscuredInt m_count;

	// Token: 0x040006D7 RID: 1751
	public bool m_reloadTimeUpdated;

	// Token: 0x040006D8 RID: 1752
	public double m_lastKeyUsedTime;

	// Token: 0x040006D9 RID: 1753
	public int m_reloadTimeSeconds;

	// Token: 0x040006DA RID: 1754
	public int m_reloadTimeLeft;

	// Token: 0x040006DB RID: 1755
	public int m_reloadTimeLeftPrevious;
}
