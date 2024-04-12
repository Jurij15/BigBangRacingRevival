using System;
using System.Collections;

// Token: 0x02000149 RID: 329
public static class PsMetagamePlayerData
{
	// Token: 0x06000B0C RID: 2828 RVA: 0x0006EFA0 File Offset: 0x0006D3A0
	public static void UpdatePlayerData(Hashtable _newData)
	{
		if (_newData != null)
		{
			IEnumerator enumerator = _newData.Keys.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					string text = (string)obj;
					if (PsMetagamePlayerData.m_playerData.ContainsKey(text))
					{
						PsMetagamePlayerData.m_playerData[text] = _newData[text];
					}
					else
					{
						PsMetagamePlayerData.m_playerData.Add(text, _newData[text]);
					}
				}
			}
			finally
			{
				IDisposable disposable;
				if ((disposable = enumerator as IDisposable) != null)
				{
					disposable.Dispose();
				}
			}
			PsMetagamePlayerData.m_playerDataUpdated = true;
		}
	}

	// Token: 0x04000A1E RID: 2590
	public static Hashtable m_playerData = new Hashtable();

	// Token: 0x04000A1F RID: 2591
	public static bool m_playerDataUpdated = true;
}
