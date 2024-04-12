using System;
using System.Collections.Generic;
using Prime31;
using Server;
using UnityEngine;

// Token: 0x02000416 RID: 1046
public class ErrorHandler
{
	// Token: 0x06001D16 RID: 7446 RVA: 0x0014C378 File Offset: 0x0014A778
	public ErrorHandler(Action<HttpC> _okCallback, Action _errorCallback = null)
	{
		this.m_okCallback = _okCallback;
		this.m_errorCallback = _errorCallback;
		if (this.m_errorCallback == null)
		{
			this.m_errorCallback = this.m_restartCallback;
		}
	}

	// Token: 0x06001D17 RID: 7447 RVA: 0x0014C3D3 File Offset: 0x0014A7D3
	public bool ContentIsTypeOf(Dictionary<string, string> _headers, string _contentType)
	{
		return _headers.ContainsValue(_contentType);
	}

	// Token: 0x06001D18 RID: 7448 RVA: 0x0014C3DC File Offset: 0x0014A7DC
	public void CheckForErrors(HttpC _c)
	{
		Debug.Log("Checking For Errors: " + _c.www.url, null);
		Dictionary<string, string> responseHeaders = _c.www.responseHeaders;
		if (responseHeaders.ContainsKey("PLAY_STATUS"))
		{
			if (responseHeaders["PLAY_STATUS"].Equals("OK"))
			{
				if (this.CheckHash(_c) && this.m_okCallback != null)
				{
					this.m_okCallback.Invoke(_c);
				}
			}
			else if (responseHeaders["PLAY_STATUS"].Equals("ERROR"))
			{
				string text = responseHeaders["PLAY_ERROR"];
				if (text != null)
				{
					if (ErrorHandler.<>f__switch$map5 == null)
					{
						Dictionary<string, int> dictionary = new Dictionary<string, int>(9);
						dictionary.Add("GAME_NOT_FOUND", 0);
						dictionary.Add("TEAM_NOT_FOUND_ERROR", 1);
						dictionary.Add("VALIDATION_ERROR", 2);
						dictionary.Add("SOCIAL_ERROR", 3);
						dictionary.Add("PURCHASE_VERIFICATION_ERROR", 4);
						dictionary.Add("SESSION_ERROR", 5);
						dictionary.Add("FATAL_ERROR", 6);
						dictionary.Add("UNKNOWN", 7);
						dictionary.Add("NOT_FOUND", 8);
						ErrorHandler.<>f__switch$map5 = dictionary;
					}
					int num;
					if (ErrorHandler.<>f__switch$map5.TryGetValue(text, ref num))
					{
						switch (num)
						{
						case 0:
							ServerManager.m_dontShowLoginPopup = true;
							Debug.LogError("SERVER ERROR: " + responseHeaders["PLAY_ERROR"]);
							Debug.LogError(responseHeaders["PLAY_ERROR"] + ": " + _c.www.text);
							ServerManager.ThrowServerErrorException(PsStrings.Get(StringID.ERROR_LEVEL_NOT_FOUND), PsStrings.Get(StringID.ERROR_LEVEL_NOT_FOUND), null, this.m_errorCallback, StringID.OK);
							break;
						case 1:
							ServerManager.m_dontShowLoginPopup = true;
							Debug.LogError("SERVER ERROR: " + responseHeaders["PLAY_ERROR"]);
							Debug.LogError(responseHeaders["PLAY_ERROR"] + ": " + _c.www.text);
							ServerManager.ThrowServerErrorException(PsStrings.Get(StringID.TEAM_NOT_FOUND_HEADER), PsStrings.Get(StringID.TEAM_NOT_FOUND_TEXT), null, this.m_errorCallback, StringID.OK);
							break;
						case 2:
							Debug.LogError("SERVER ERROR: " + responseHeaders["PLAY_ERROR"]);
							Debug.LogError(responseHeaders["PLAY_ERROR"] + ": " + _c.www.text);
							ServerManager.ThrowServerErrorException(PsStrings.Get(StringID.CONNECTION_ERROR_HEADER), PsStrings.Get(StringID.NETWORK_ERROR), null, this.m_errorCallback, StringID.TRY_AGAIN_SERVER);
							break;
						case 3:
							Debug.LogError("SERVER ERROR: " + responseHeaders["PLAY_ERROR"]);
							Debug.LogError(responseHeaders["PLAY_ERROR"] + ": " + _c.www.text);
							GameCenterManager.disableGcLogin = true;
							ServerManager.ThrowServerErrorException(PsStrings.Get(StringID.CONNECTION_ERROR_HEADER), _c.www.text, null, this.m_errorCallback, StringID.TRY_AGAIN_SERVER);
							break;
						case 4:
						{
							Debug.LogError("SERVER ERROR: " + responseHeaders["PLAY_ERROR"]);
							Debug.LogError(responseHeaders["PLAY_ERROR"] + ": " + _c.www.text);
							GooglePurchase googlePurchase = _c.objectData as GooglePurchase;
							if (googlePurchase != null)
							{
								GoogleIAB.consumeProduct(googlePurchase.productId);
							}
							PsState.m_inIapFlow = false;
							ServerManager.ThrowServerErrorException(PsStrings.Get(StringID.CONNECTION_ERROR_HEADER), _c.www.text, null, this.m_errorCallback, StringID.TRY_AGAIN_SERVER);
							break;
						}
						case 5:
							Debug.LogWarning("SERVER ERROR: " + responseHeaders["PLAY_ERROR"]);
							Debug.LogWarning(responseHeaders["PLAY_ERROR"] + ": " + _c.www.text);
							ServerManager.ThrowServerErrorException(PsStrings.Get(StringID.SESSION_EXPIRED), PsStrings.Get(StringID.SESSION_EXPIRED_TEXT), null, new Action(LoginFlow.Restart), StringID.TRY_AGAIN_SERVER);
							break;
						case 6:
							Debug.LogError("SERVER ERROR: " + responseHeaders["PLAY_ERROR"]);
							Debug.LogError(responseHeaders["PLAY_ERROR"] + ": " + _c.www.text);
							ServerManager.ThrowServerErrorException(PsStrings.Get(StringID.CONNECTION_ERROR_HEADER), PsStrings.Get(StringID.NETWORK_ERROR), null, this.m_errorCallback, StringID.TRY_AGAIN_SERVER);
							break;
						case 7:
							Debug.LogError("SERVER ERROR: " + responseHeaders["PLAY_ERROR"]);
							Debug.LogError(responseHeaders["PLAY_ERROR"] + ": " + _c.www.text);
							HttpS.ClearAllRequests(null);
							ServerManager.ThrowServerErrorException(PsStrings.Get(StringID.CONNECTION_ERROR_HEADER), PsStrings.Get(StringID.NETWORK_ERROR), null, this.m_restartCallback, StringID.TRY_AGAIN_SERVER);
							break;
						case 8:
							Debug.LogWarning("SERVER ERROR: " + responseHeaders["PLAY_ERROR"]);
							Debug.LogWarning(responseHeaders["PLAY_ERROR"] + ": " + _c.www.text);
							this.m_errorCallback.Invoke();
							break;
						}
					}
				}
			}
		}
		else
		{
			this.ThrowError(_c, "ERROR: RESPONSE DIDN'T HAVE STATUS HEADER");
		}
	}

	// Token: 0x06001D19 RID: 7449 RVA: 0x0014C94C File Offset: 0x0014AD4C
	public bool CheckHash(HttpC _c)
	{
		Dictionary<string, string> responseHeaders = _c.www.responseHeaders;
		string uri = this.GetUri(_c.www.url);
		if (responseHeaders.ContainsKey("PLAY_HASH"))
		{
			if (this.ContentIsTypeOf(responseHeaders, "application/json"))
			{
				if (!ClientTools.GenerateHash(uri + _c.www.text).Equals(responseHeaders["PLAY_HASH"]))
				{
					this.ThrowError(_c, "ERROR: JSON HASH MISMATCH");
					return false;
				}
			}
			else
			{
				if (!this.ContentIsTypeOf(responseHeaders, "application/octet-stream"))
				{
					this.ThrowError(_c, "ERROR: RESPONSE DIDN'T HAVE CORRECT CONTENT TYPE");
					return false;
				}
				if (!ClientTools.GenerateHash(uri, _c.www.bytes).Equals(responseHeaders["PLAY_HASH"]))
				{
					this.ThrowError(_c, "ERROR: DATA HASH MISMATCH");
					return false;
				}
			}
			return true;
		}
		this.ThrowError(_c, "ERROR: RESPONSE DIDN'T HAVE HASH");
		return false;
	}

	// Token: 0x06001D1A RID: 7450 RVA: 0x0014CA44 File Offset: 0x0014AE44
	public string GetUri(string url)
	{
		int num = 0;
		char c = '/';
		for (int i = 0; i < url.Length; i++)
		{
			if (url.get_Chars(i) == c)
			{
				num++;
				if (num == 3)
				{
					return url.Substring(i);
				}
			}
		}
		return string.Empty;
	}

	// Token: 0x06001D1B RID: 7451 RVA: 0x0014CA94 File Offset: 0x0014AE94
	private void ThrowError(HttpC _c, string _errorMessage)
	{
		Debug.LogError(_c.www.error);
		Debug.LogError(_errorMessage + ", CHECK REQUEST " + _c.www.url);
		ServerManager.ThrowServerErrorException(PsStrings.Get(StringID.CONNECTION_ERROR_HEADER), PsStrings.Get(StringID.NETWORK_ERROR), null, this.m_restartCallback, StringID.TRY_AGAIN_SERVER);
	}

	// Token: 0x04001FDE RID: 8158
	private Action<HttpC> m_okCallback;

	// Token: 0x04001FDF RID: 8159
	private Action m_errorCallback;

	// Token: 0x04001FE0 RID: 8160
	private Action m_restartCallback = delegate
	{
		Main.m_currentGame.m_sceneManager.ChangeScene(new StartupScene("StartupScene"), new FadeLoadingScene(Color.black, true, 0.25f));
	};
}
