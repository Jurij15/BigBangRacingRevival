using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Facebook.Unity;

// Token: 0x02000528 RID: 1320
public static class Metrics
{
	// Token: 0x060026EB RID: 9963 RVA: 0x001A8A55 File Offset: 0x001A6E55
	public static void Initialize(string _flurryIosApiKey, string _flurryAndroidApiKey, string _adjustToken)
	{
		Metrics.DEBUG = false;
		Metrics.UID = Metrics.GetMetricsGUID();
	}

	// Token: 0x060026EC RID: 9964 RVA: 0x001A8A67 File Offset: 0x001A6E67
	public static void Initialize(string _adjustToken)
	{
		Metrics.DEBUG = false;
		Metrics.UID = Metrics.GetMetricsGUID();
	}

	// Token: 0x060026ED RID: 9965 RVA: 0x001A8A79 File Offset: 0x001A6E79
	public static void SendEvent(string _eventName, Dictionary<string, string> _parameters, bool _sendToOmniata = true)
	{
		if (Metrics.DEBUG)
		{
			Debug.Log("send metric event: " + _eventName, null);
		}
	}

	// Token: 0x060026EE RID: 9966 RVA: 0x001A8A98 File Offset: 0x001A6E98
	private static double CurrencyStringToDouble(string _currencyString)
	{
		double num = 0.0;
		try
		{
			string text = "0";
			if (!string.IsNullOrEmpty(_currencyString))
			{
				text = new string(Enumerable.ToArray<char>(Enumerable.Where<char>(_currencyString, (char c) => char.IsDigit(c) || c == ',' || c == '.')));
				if (Enumerable.Contains<char>(text, ','))
				{
					text = text.Replace(',', '.');
				}
				while (text.EndsWith("."))
				{
					text = text.Substring(0, text.Length - 1);
				}
				while (text.StartsWith("."))
				{
					text = text.Substring(1);
				}
				int num2 = text.LastIndexOf('.');
				if (num2 > 0)
				{
					int num3 = text.IndexOf('.', 0, num2);
					while (num2 > 0 && num3 > 0 && num3 != num2)
					{
						text = text.Remove(num3, 1);
						num2 = text.LastIndexOf('.');
						num3 = text.IndexOf('.', 0, num2);
					}
				}
			}
			if (!string.IsNullOrEmpty(text))
			{
				num = double.Parse(text, 167, CultureInfo.InvariantCulture);
			}
		}
		catch (Exception ex)
		{
			Debug.LogError("Parse error: " + ex);
		}
		return num;
	}

	// Token: 0x060026EF RID: 9967 RVA: 0x001A8BF0 File Offset: 0x001A6FF0
	public static void SendRevenueEvent(string _total, string _currency, string _transactionId)
	{
		if (_transactionId == null)
		{
			_transactionId = "sandbox";
		}
		double num = Metrics.CurrencyStringToDouble(_total);
		Dictionary<string, object> dictionary = new Dictionary<string, object>();
		dictionary.Add("transactionId", _transactionId);
		FB.LogPurchase((float)num, _currency, dictionary);
	}

	// Token: 0x060026F0 RID: 9968 RVA: 0x001A8C2C File Offset: 0x001A702C
	public static string GetMetricsGUID()
	{
		string text = PlayerPrefsX.GetMetricsGUID();
		if (string.IsNullOrEmpty(text))
		{
			text = Guid.NewGuid().ToString();
			PlayerPrefsX.SetFirstLogin(true);
			PlayerPrefsX.SetMetricsGUID(text);
		}
		return text;
	}

	// Token: 0x04002C64 RID: 11364
	private static bool DEBUG = true;

	// Token: 0x04002C65 RID: 11365
	private const string ORG = "traplight";

	// Token: 0x04002C66 RID: 11366
	private static string UID;

	// Token: 0x04002C67 RID: 11367
	private static int m_repeatEventDebugCounter = 1;

	// Token: 0x04002C68 RID: 11368
	private static string m_repeatEventDebugName = string.Empty;
}
