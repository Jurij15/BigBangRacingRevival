using System;
using System.Collections.Generic;
using System.IO;
using InAppPurchases;
using MiniJSON;
using Prime31;
using Server;

// Token: 0x02000089 RID: 137
public static class PsIAPManager
{
	// Token: 0x17000005 RID: 5
	// (get) Token: 0x060002F5 RID: 757 RVA: 0x0002F40B File Offset: 0x0002D80B
	public static bool ProductsReceived
	{
		get
		{
			return Manager.ProductsReceived;
		}
	}

	// Token: 0x17000006 RID: 6
	// (get) Token: 0x060002F6 RID: 758 RVA: 0x0002F412 File Offset: 0x0002D812
	public static bool HavePendingPurcheses
	{
		get
		{
			return Manager.IapProvider.HavePendingPurchases();
		}
	}

	// Token: 0x060002F7 RID: 759 RVA: 0x0002F41E File Offset: 0x0002D81E
	public static void Initialize()
	{
		PsIAPManager.GetConfigAndInitialize();
	}

	// Token: 0x060002F8 RID: 760 RVA: 0x0002F425 File Offset: 0x0002D825
	public static void Initialize(Dictionary<string, object> _config)
	{
		Manager.Initialize(_config);
		PsIAPManager.m_initialized = true;
	}

	// Token: 0x060002F9 RID: 761 RVA: 0x0002F434 File Offset: 0x0002D834
	public static ServerProduct GetServerProductById(string _identifier)
	{
		return Manager.Products.Find((ServerProduct p) => p.identifier == _identifier);
	}

	// Token: 0x060002FA RID: 762 RVA: 0x0002F464 File Offset: 0x0002D864
	public static IAPProduct GetIAPProductById(string _identifier)
	{
		return Manager.AvailableItems.Find((IAPProduct p) => p.productId == _identifier);
	}

	// Token: 0x060002FB RID: 763 RVA: 0x0002F494 File Offset: 0x0002D894
	public static List<ServerProduct> GetProducts()
	{
		return Manager.Products;
	}

	// Token: 0x060002FC RID: 764 RVA: 0x0002F49B File Offset: 0x0002D89B
	public static bool CanMakePayments()
	{
		return Manager.CanMakePayments();
	}

	// Token: 0x060002FD RID: 765 RVA: 0x0002F4A2 File Offset: 0x0002D8A2
	public static void CheckItemAvailability()
	{
		if (Manager.AvailableItems != null && Manager.AvailableItems.Count <= 0)
		{
			Manager.ReloadItems();
		}
	}

	// Token: 0x060002FE RID: 766 RVA: 0x0002F4C4 File Offset: 0x0002D8C4
	private static void GetConfigAndInitialize()
	{
		InAppPurchase.GetConfig(new Action<HttpC>(PsIAPManager.GetConfigOkAndInit), new Action<HttpC>(PsIAPManager.GetConfigFailedAndInit), null);
	}

	// Token: 0x060002FF RID: 767 RVA: 0x0002F514 File Offset: 0x0002D914
	private static void GetConfigOkAndInit(HttpC _request)
	{
		Dictionary<string, object> dictionary = ClientTools.ParseServerResponse(_request.www.text);
		Debug.Log(dictionary, null);
		PsIAPManager.Initialize(dictionary);
	}

	// Token: 0x06000300 RID: 768 RVA: 0x0002F540 File Offset: 0x0002D940
	private static void GetConfigFailedAndInit(HttpC _request)
	{
		InAppPurchase.GetConfig(new Action<HttpC>(PsIAPManager.GetConfigOkAndInit), new Action<HttpC>(PsIAPManager.GetConfigFailedAndInit), null);
	}

	// Token: 0x06000301 RID: 769 RVA: 0x0002F590 File Offset: 0x0002D990
	public static void StoreBackup(string _string, string _transactionId)
	{
		string text = ClientTools.GenerateHash(_string);
		Dictionary<string, object> dictionary = new Dictionary<string, object>();
		dictionary.Add("n", _string);
		dictionary.Add("h", text);
		string text2 = Json.Serialize(dictionary);
		string text3 = Main.GetPersistentDataPath();
		text3 = text3 + "/" + _transactionId;
		try
		{
			if (File.Exists(text3))
			{
				File.Delete(text3);
			}
			StreamWriter streamWriter = File.CreateText(text3);
			streamWriter.WriteLine(text2);
			streamWriter.Close();
			Debug.Log("Writing string worked: " + text2, null);
		}
		catch (Exception ex)
		{
			Debug.Log("Writing string did not work: " + text2 + ", exception: " + ex.Message, null);
		}
	}

	// Token: 0x06000302 RID: 770 RVA: 0x0002F650 File Offset: 0x0002DA50
	public static string GetBackup(string _id)
	{
		string text = string.Empty;
		string text2 = Main.GetPersistentDataPath();
		text2 = text2 + "/" + _id;
		try
		{
			if (!string.IsNullOrEmpty(text2) && File.Exists(text2))
			{
				StreamReader streamReader = File.OpenText(text2);
				string text3 = string.Empty;
				for (string text4 = streamReader.ReadLine(); text4 != null; text4 = streamReader.ReadLine())
				{
					text3 += text4;
				}
				streamReader.Close();
				text = text3;
				Dictionary<string, object> dictionary = Json.Deserialize(text) as Dictionary<string, object>;
				if (dictionary.ContainsKey("n") && dictionary.ContainsKey("h"))
				{
					string text5 = (string)dictionary["n"];
					string text6 = (string)dictionary["h"];
					if (ClientTools.GenerateHash(text5) == text6)
					{
						return text5;
					}
				}
			}
			else
			{
				Debug.Log("File did not exist or path was empty", null);
			}
		}
		catch (Exception ex)
		{
			Debug.Log("reading did not work: " + ex.Message, null);
		}
		return text;
	}

	// Token: 0x06000303 RID: 771 RVA: 0x0002F780 File Offset: 0x0002DB80
	public static void RemoveBackup(string _id)
	{
		string text = Main.GetPersistentDataPath();
		text = text + "/" + _id;
		if (!string.IsNullOrEmpty(text) && File.Exists(text))
		{
			File.Delete(text);
		}
	}

	// Token: 0x06000304 RID: 772 RVA: 0x0002F7BC File Offset: 0x0002DBBC
	public static void RetryPendingPurchases(Action<string, bool, string, string> _callback)
	{
		if (PsIAPManager.HavePendingPurcheses)
		{
			PsState.m_inIapFlow = true;
			Manager.CompletePendingPurchases(_callback);
		}
	}

	// Token: 0x06000305 RID: 773 RVA: 0x0002F7D4 File Offset: 0x0002DBD4
	public static void PurchaseConsumable(string _id, Action<string, bool, string, string> _callback = null)
	{
		PsState.m_inIapFlow = true;
		PsIAPManager.GetNonce(_id, _callback, false);
	}

	// Token: 0x06000306 RID: 774 RVA: 0x0002F7E4 File Offset: 0x0002DBE4
	public static void PurchaseNonConsumable(string _id, Action<string, bool, string, string> _callback = null)
	{
		PsState.m_inIapFlow = true;
		PsIAPManager.GetNonce(_id, _callback, true);
	}

	// Token: 0x06000307 RID: 775 RVA: 0x0002F7F4 File Offset: 0x0002DBF4
	private static void GetNonce(string _id, Action<string, bool, string, string> _callback, bool _noConsume = false)
	{
		PsMetagameManager.IAPDebug("IAPDebug_GetNonce", 3, new KeyValuePair<string, object>[0]);
		InAppPurchase.GetNonce(_id, delegate(HttpC c)
		{
			PsIAPManager.GetNonceSuccess(c, _id, _callback, _noConsume);
		}, delegate(HttpC c)
		{
			PsIAPManager.GetNonceFailed(c, _id, _callback);
		}, null);
	}

	// Token: 0x06000308 RID: 776 RVA: 0x0002F854 File Offset: 0x0002DC54
	private static void GetNonceSuccess(HttpC _c, string _id, Action<string, bool, string, string> _callback, bool _noConsume)
	{
		PsMetagameManager.IAPDebug("IAPDebug_GetNonceSuccess", 3, new KeyValuePair<string, object>[0]);
		Dictionary<string, object> dictionary = ClientTools.ParseServerResponse(_c.www.text);
		if (dictionary.ContainsKey("nonce"))
		{
			if (_noConsume)
			{
				Manager.PurchaseNonConsumable(_id, dictionary["nonce"] as string, _callback);
			}
			else
			{
				Manager.PurchaseConsumable(_id, dictionary["nonce"] as string, _callback);
			}
		}
		else
		{
			_callback.Invoke(_id, false, "Unable to connect to server.", null);
		}
	}

	// Token: 0x06000309 RID: 777 RVA: 0x0002F8DF File Offset: 0x0002DCDF
	private static void GetNonceFailed(HttpC _c, string _id, Action<string, bool, string, string> _callback)
	{
		PsMetagameManager.IAPDebug("IAPDebug_GetNonceFailed", 2, new KeyValuePair<string, object>[0]);
		_callback.Invoke(_id, false, "Unable to connect to server.", null);
	}

	// Token: 0x0600030A RID: 778 RVA: 0x0002F900 File Offset: 0x0002DD00
	public static void GetNewNonce(string _id, Action<string, bool, string, string> _callback, Action<string> _nonceCallback)
	{
		InAppPurchase.GetNonce(_id, delegate(HttpC c)
		{
			PsIAPManager.GetNewNonceSuccess(c, _id, _callback, _nonceCallback);
		}, delegate(HttpC c)
		{
			PsIAPManager.GetNewNonceFailed(c, _id, _callback);
		}, null);
	}

	// Token: 0x0600030B RID: 779 RVA: 0x0002F950 File Offset: 0x0002DD50
	private static void GetNewNonceSuccess(HttpC _c, string _id, Action<string, bool, string, string> _callback, Action<string> _nonceCallback)
	{
		Dictionary<string, object> dictionary = ClientTools.ParseServerResponse(_c.www.text);
		if (dictionary.ContainsKey("nonce"))
		{
			_nonceCallback.Invoke(dictionary["nonce"] as string);
		}
		else
		{
			_callback.Invoke(_id, false, "Unable to connect to server.", null);
		}
	}

	// Token: 0x0600030C RID: 780 RVA: 0x0002F9A7 File Offset: 0x0002DDA7
	private static void GetNewNonceFailed(HttpC _c, string _id, Action<string, bool, string, string> _callback)
	{
		_callback.Invoke(_id, false, "Unable to connect to server.", null);
	}

	// Token: 0x0600030D RID: 781 RVA: 0x0002F9B8 File Offset: 0x0002DDB8
	public static PsIAPManager.ResourceBundle ParseResourceBundle(Dictionary<string, object> _resourceBundleData)
	{
		PsIAPManager.ResourceBundle resourceBundle = default(PsIAPManager.ResourceBundle);
		if (_resourceBundleData != null)
		{
			if (_resourceBundleData.ContainsKey("coins"))
			{
				resourceBundle.coins = Convert.ToInt32(_resourceBundleData["coins"]);
			}
			if (_resourceBundleData.ContainsKey("gems"))
			{
				resourceBundle.gems = Convert.ToInt32(_resourceBundleData["gems"]);
			}
			if (_resourceBundleData.ContainsKey("trails"))
			{
				resourceBundle.trails = ClientTools.ParseList<string>(_resourceBundleData["trails"] as List<object>);
			}
			if (_resourceBundleData.ContainsKey("hats"))
			{
				resourceBundle.hats = ClientTools.ParseList<string>(_resourceBundleData["hats"] as List<object>);
			}
			if (_resourceBundleData.ContainsKey("chests"))
			{
				List<string> list = ClientTools.ParseList<string>(_resourceBundleData["chests"] as List<object>);
				List<GachaType> list2 = new List<GachaType>();
				if (list != null)
				{
					for (int i = 0; i < list.Count; i++)
					{
						list2.Add((GachaType)Enum.Parse(typeof(GachaType), list[i], true));
					}
				}
				resourceBundle.chests = list2;
			}
		}
		return resourceBundle;
	}

	// Token: 0x040003DB RID: 987
	private static bool m_initialized;

	// Token: 0x040003DC RID: 988
	public static List<PsIAPManager.UnfinishedPurchase> unfinishedPurchases;

	// Token: 0x0200008A RID: 138
	public class UnfinishedPurchase
	{
		// Token: 0x06000310 RID: 784 RVA: 0x0002FAF8 File Offset: 0x0002DEF8
		public void fromDictionary(Dictionary<string, object> _dict)
		{
			if (_dict.ContainsKey("productId"))
			{
				this.productId = _dict["productId"] as string;
			}
			if (_dict.ContainsKey("nonce"))
			{
				this.nonce = _dict["nonce"] as string;
			}
		}

		// Token: 0x040003E1 RID: 993
		public string productId;

		// Token: 0x040003E2 RID: 994
		public string nonce;
	}

	// Token: 0x0200008B RID: 139
	public struct ResourceBundle
	{
		// Token: 0x040003E3 RID: 995
		public int coins;

		// Token: 0x040003E4 RID: 996
		public int gems;

		// Token: 0x040003E5 RID: 997
		public List<string> trails;

		// Token: 0x040003E6 RID: 998
		public List<string> hats;

		// Token: 0x040003E7 RID: 999
		public List<GachaType> chests;
	}
}
