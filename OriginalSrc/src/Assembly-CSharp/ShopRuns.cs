using System;
using System.Collections.Generic;

// Token: 0x02000340 RID: 832
public class ShopRuns
{
	// Token: 0x06001867 RID: 6247 RVA: 0x00108EC8 File Offset: 0x001072C8
	public static void ParseServerConfig(Dictionary<string, object> _serverResponse)
	{
		if (_serverResponse.ContainsKey("triesForAd"))
		{
			ShopRuns.m_triesForAd = Convert.ToInt32(_serverResponse["triesForAd"]);
		}
		else
		{
			Debug.LogError("Didnt contain triesForAd");
		}
		if (_serverResponse.ContainsKey("triesForGems"))
		{
			ShopRuns.m_triesForGems = Convert.ToInt32(_serverResponse["triesForGems"]);
		}
		else
		{
			Debug.LogError("Didnt contain triesForGems");
		}
		if (_serverResponse.ContainsKey("triesGemPrice"))
		{
			ShopRuns.m_gemPrice = Convert.ToInt32(_serverResponse["triesGemPrice"]);
		}
		else
		{
			Debug.LogError("Didnt contain triesGemPrice");
		}
	}

	// Token: 0x06001868 RID: 6248 RVA: 0x00108F71 File Offset: 0x00107371
	public static int GetGemPrice()
	{
		return ShopRuns.m_gemPrice;
	}

	// Token: 0x06001869 RID: 6249 RVA: 0x00108F78 File Offset: 0x00107378
	public static int GetTriesForGems()
	{
		return ShopRuns.m_triesForGems;
	}

	// Token: 0x0600186A RID: 6250 RVA: 0x00108F7F File Offset: 0x0010737F
	public static int GetTriesForAd()
	{
		return ShopRuns.m_triesForAd;
	}

	// Token: 0x04001B1B RID: 6939
	private static int m_gemPrice = 15;

	// Token: 0x04001B1C RID: 6940
	private static int m_triesForGems = 5;

	// Token: 0x04001B1D RID: 6941
	private static int m_triesForAd = 3;
}
