using System;
using AdMediation;
using UnityEngine.Advertisements;

// Token: 0x02000529 RID: 1321
public static class UnityAdsManager
{
	// Token: 0x060026F3 RID: 9971 RVA: 0x001A8CA0 File Offset: 0x001A70A0
	public static void Initialize(string _gameId, bool _testMode)
	{
		if (Advertisement.isSupported)
		{
			Advertisement.Initialize(_gameId, _testMode);
		}
		else
		{
			Debug.Log("Platform not supported for UnityAds", null);
		}
	}

	// Token: 0x060026F4 RID: 9972 RVA: 0x001A8CC4 File Offset: 0x001A70C4
	public static bool isReadyToShowAd()
	{
		if (!Advertisement.isSupported)
		{
			return false;
		}
		bool flag = Advertisement.IsReady();
		Debug.Log("UnityAds ready to show an ad: " + flag, null);
		return flag;
	}

	// Token: 0x060026F5 RID: 9973 RVA: 0x001A8CFC File Offset: 0x001A70FC
	public static void ShowAd(string _zoneId = null)
	{
		ShowOptions showOptions = new ShowOptions();
		showOptions.resultCallback = delegate(ShowResult result)
		{
			Debug.Log(result.ToString(), null);
		};
		Advertisement.Show(_zoneId, showOptions);
	}

	// Token: 0x060026F6 RID: 9974 RVA: 0x001A8D3C File Offset: 0x001A713C
	public static void ShowAd(Action<AdResult> _callback, string _zoneId = null)
	{
		Advertisement.Show(_zoneId, new ShowOptions
		{
			resultCallback = delegate(ShowResult result)
			{
				_callback.Invoke((AdResult)Enum.Parse(typeof(AdResult), result.ToString()));
			}
		});
	}
}
