using System;
using System.Collections.Generic;
using AdMediation;
using Server;

// Token: 0x0200000A RID: 10
public static class PsAdMediation
{
	// Token: 0x0600003E RID: 62 RVA: 0x0000311C File Offset: 0x0000151C
	public static void Initialize()
	{
		Manager.Initialize(null, "96752");
		PsAdMediation.m_initialized = true;
	}

	// Token: 0x0600003F RID: 63 RVA: 0x00003130 File Offset: 0x00001530
	public static void Initialize(List<object> _config)
	{
		Dictionary<string, object> dictionary = new Dictionary<string, object>();
		foreach (object obj in _config)
		{
			Dictionary<string, object> dictionary2 = (Dictionary<string, object>)obj;
			NetworkConfig networkConfig;
			networkConfig.priority = Convert.ToInt32(dictionary2["priority"]);
			networkConfig.enabled = true;
			dictionary.Add((string)dictionary2["networkName"], networkConfig);
		}
		Manager.Initialize(dictionary, "96752");
		PsAdMediation.m_initialized = true;
	}

	// Token: 0x06000040 RID: 64 RVA: 0x000031D8 File Offset: 0x000015D8
	public static void Initialize(Dictionary<string, object> _config)
	{
		Manager.Initialize(_config, "96752");
		PsAdMediation.m_initialized = true;
	}

	// Token: 0x06000041 RID: 65 RVA: 0x000031EC File Offset: 0x000015EC
	public static void ShowAd(string _location = null)
	{
		if (PsAdMediation.m_initialized)
		{
			SoundS.PauseMixer(true);
			Action<AdResult> action = delegate(AdResult _result)
			{
				SoundS.PauseMixer(false);
			};
			PsState.m_adWatched = true;
			Manager.ShowAd(action, _location);
		}
		else
		{
			Debug.Log("Ad mediator is not initialized yet!", null);
		}
	}

	// Token: 0x06000042 RID: 66 RVA: 0x00003248 File Offset: 0x00001648
	public static void ShowAd(Action<AdResult> _callback, string _location = null)
	{
		if (PsAdMediation.m_initialized)
		{
			SoundS.PauseMixer(true);
			if (_callback == null)
			{
				_callback = delegate(AdResult _result)
				{
					SoundS.PauseMixer(false);
				};
			}
			else
			{
				_callback = (Action<AdResult>)Delegate.Combine(_callback, delegate(AdResult _result)
				{
					SoundS.PauseMixer(false);
				});
			}
			PsState.m_adWatched = true;
			string text = Manager.ShowAd(_callback, _location);
			if (!string.IsNullOrEmpty(text))
			{
				PsMetrics.SetAdNetworkName(text);
			}
		}
		else
		{
			Debug.Log("Ad mediator is not initialized yet!", null);
		}
	}

	// Token: 0x06000043 RID: 67 RVA: 0x000032E8 File Offset: 0x000016E8
	public static bool adsAvailable()
	{
		if (PsAdMediation.m_initialized)
		{
			return Manager.adsAvailable();
		}
		Debug.Log("Ad mediator is not initialized yet!", null);
		return false;
	}

	// Token: 0x06000044 RID: 68 RVA: 0x00003308 File Offset: 0x00001708
	private static void GetConfigAndInitialize()
	{
		Ads.GetAdNetworkConfig(new Action<HttpC>(PsAdMediation.GetConfigOkAndInit), new Action<HttpC>(PsAdMediation.GetConfigFailedAndInit), null);
	}

	// Token: 0x06000045 RID: 69 RVA: 0x00003358 File Offset: 0x00001758
	public static void UseServerConfig()
	{
		Ads.GetAdNetworkConfig(new Action<HttpC>(PsAdMediation.GetConfigOk), new Action<HttpC>(PsAdMediation.GetConfigFailed), null);
	}

	// Token: 0x06000046 RID: 70 RVA: 0x000033A8 File Offset: 0x000017A8
	private static void GetConfigOk(HttpC _request)
	{
		Dictionary<string, object> dictionary = ClientTools.ParseServerResponse(_request.www.text);
		Manager.LoadConfig(dictionary);
		PsAdMediation.m_initialized = true;
	}

	// Token: 0x06000047 RID: 71 RVA: 0x000033D4 File Offset: 0x000017D4
	private static void GetConfigOkAndInit(HttpC _request)
	{
		Dictionary<string, object> dictionary = ClientTools.ParseServerResponse(_request.www.text);
		Manager.Initialize(dictionary, "96752");
		PsAdMediation.m_initialized = true;
	}

	// Token: 0x06000048 RID: 72 RVA: 0x00003404 File Offset: 0x00001804
	private static void GetConfigFailedAndInit(HttpC _request)
	{
		Ads.GetAdNetworkConfig(new Action<HttpC>(PsAdMediation.GetConfigOkAndInit), new Action<HttpC>(PsAdMediation.GetConfigFailedAndInit), null);
	}

	// Token: 0x06000049 RID: 73 RVA: 0x00003454 File Offset: 0x00001854
	private static void GetConfigFailed(HttpC _request)
	{
		Ads.GetAdNetworkConfig(new Action<HttpC>(PsAdMediation.GetConfigOk), new Action<HttpC>(PsAdMediation.GetConfigFailed), null);
	}

	// Token: 0x0400001D RID: 29
	private static bool m_initialized;
}
