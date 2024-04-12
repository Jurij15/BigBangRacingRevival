using System;
using System.Collections.Generic;
using System.Globalization;

namespace AdMediation
{
	// Token: 0x0200049F RID: 1183
	public static class Manager
	{
		// Token: 0x060021D6 RID: 8662 RVA: 0x001899C4 File Offset: 0x00187DC4
		public static void Initialize(Dictionary<string, object> _config, string _id)
		{
			if (Manager.m_adNetworks == null)
			{
				Manager.m_adNetworks = new List<AdNetwork>();
			}
			else
			{
				Manager.m_adNetworks.Clear();
			}
			AdNetwork adNetwork = new AdNetwork("UnityAds", 1, delegate
			{
				UnityAdsManager.Initialize(_id, false);
			}, new Func<bool>(UnityAdsManager.isReadyToShowAd), delegate(string zone, Action<AdResult> callback)
			{
				UnityAdsManager.ShowAd(callback, zone);
			}, true);
			Manager.m_adNetworks.Add(adNetwork);
			Manager.LoadConfig(_config);
		}

		// Token: 0x060021D7 RID: 8663 RVA: 0x00189A68 File Offset: 0x00187E68
		public static bool adsAvailable()
		{
			if (Manager.GetVideoAdCount() > 0 || Manager.GetVideoAdTimeOutSeconds() < 0)
			{
				foreach (AdNetwork adNetwork in Manager.m_adNetworks)
				{
					if (adNetwork.adsAvailable.Invoke())
					{
						return true;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x060021D8 RID: 8664 RVA: 0x00189AEC File Offset: 0x00187EEC
		public static void LoadConfig(Dictionary<string, object> _config)
		{
			if (_config != null)
			{
				foreach (AdNetwork adNetwork in Manager.m_adNetworks)
				{
					object obj = null;
					if (_config.TryGetValue(adNetwork.networkName, ref obj))
					{
						adNetwork.SetConfig((NetworkConfig)obj);
					}
				}
			}
			Manager.InitializeAdNetworks();
		}

		// Token: 0x060021D9 RID: 8665 RVA: 0x00189B6C File Offset: 0x00187F6C
		private static void InitializeAdNetworks()
		{
			if (Manager.m_adNetworks != null)
			{
				foreach (AdNetwork adNetwork in Manager.m_adNetworks)
				{
					Debug.Log("Initialize " + adNetwork.networkName, null);
					adNetwork.initialize.Invoke();
				}
			}
		}

		// Token: 0x060021DA RID: 8666 RVA: 0x00189BEC File Offset: 0x00187FEC
		public static string ShowAd(Action<AdResult> _callback = null, string _zone = null)
		{
			string text = null;
			bool flag = false;
			Manager.m_adNetworks.Sort((AdNetwork n1, AdNetwork n2) => n1.priority.CompareTo(n2.priority));
			foreach (AdNetwork adNetwork in Manager.m_adNetworks)
			{
				if (adNetwork.enabled)
				{
					Debug.Log("Checking for ads in " + adNetwork.networkName, null);
					if (adNetwork.adsAvailable.Invoke())
					{
						text = adNetwork.networkName;
						flag = true;
						adNetwork.showAd.Invoke(_zone, _callback);
						Manager.VideoAdShown();
						break;
					}
				}
			}
			if (!flag)
			{
				_callback.Invoke(AdResult.Failed);
			}
			return text;
		}

		// Token: 0x060021DB RID: 8667 RVA: 0x00189CCC File Offset: 0x001880CC
		public static void ShowAd(Action<AdResult> _callback, string _zone, string _adNetwork)
		{
			Manager.m_adNetworks.Find((AdNetwork n) => n.networkName == _adNetwork).showAd.Invoke(_zone, _callback);
		}

		// Token: 0x060021DC RID: 8668 RVA: 0x00189D08 File Offset: 0x00188108
		private static int GetVideoAdCount()
		{
			int num = PlayerPrefsX.GetVideoAdCount();
			if (num == -1)
			{
				num = PlayerPrefsX.GetVideoAdStartCount();
				Manager.SetVideoAdCount(num);
			}
			return num;
		}

		// Token: 0x060021DD RID: 8669 RVA: 0x00189D2F File Offset: 0x0018812F
		private static void SetVideoAdCount(int _count)
		{
			PlayerPrefsX.SetVideoAdCount(_count);
		}

		// Token: 0x060021DE RID: 8670 RVA: 0x00189D38 File Offset: 0x00188138
		private static void VideoAdShown()
		{
			int videoAdCount = Manager.GetVideoAdCount();
			if (Manager.GetVideoAdTimeOutSeconds() < 0)
			{
				Manager.SetVideoAdCount(PlayerPrefsX.GetVideoAdStartCount() - 1);
			}
			else if (videoAdCount > 0)
			{
				Manager.SetVideoAdCount(videoAdCount - 1);
			}
			Manager.SetVideoAdTimeOut();
		}

		// Token: 0x060021DF RID: 8671 RVA: 0x00189D7C File Offset: 0x0018817C
		private static void SetVideoAdTimeOut()
		{
			int videoAdStartTimeOut = PlayerPrefsX.GetVideoAdStartTimeOut();
			PlayerPrefsX.SetVideoAdTimeOut(DateTime.Now.AddSeconds((double)videoAdStartTimeOut).ToString("O"));
		}

		// Token: 0x060021E0 RID: 8672 RVA: 0x00189DB0 File Offset: 0x001881B0
		private static int GetVideoAdTimeOutSeconds()
		{
			string videoAdTimeOut = PlayerPrefsX.GetVideoAdTimeOut();
			int num;
			if (videoAdTimeOut != null)
			{
				num = Convert.ToInt32(DateTime.ParseExact(videoAdTimeOut, "O", CultureInfo.InvariantCulture).Subtract(DateTime.Now).TotalSeconds);
			}
			else
			{
				num = PlayerPrefsX.GetVideoAdStartTimeOut();
				Manager.SetVideoAdTimeOut();
			}
			return num;
		}

		// Token: 0x040027FD RID: 10237
		private static List<AdNetwork> m_adNetworks;
	}
}
