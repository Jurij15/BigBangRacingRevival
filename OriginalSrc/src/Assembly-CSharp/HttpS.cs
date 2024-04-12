using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

// Token: 0x02000503 RID: 1283
public static class HttpS
{
	// Token: 0x06002502 RID: 9474 RVA: 0x0019923F File Offset: 0x0019763F
	public static void Initialize()
	{
		HttpS.m_maxStreams = 10;
		HttpS.m_components = new DynamicArray<HttpC>(100, 0.5f, 0.25f, 0.5f);
		HttpS.m_removeComponentList = new List<HttpC>();
		HttpS.m_requestList = new List<HttpC>();
	}

	// Token: 0x06002503 RID: 9475 RVA: 0x00199277 File Offset: 0x00197677
	public static HttpC AddGetComponent(string _url, string _tag, string[] _queueTags = null, bool _destroyAfterDone = true, Hashtable _headers = null)
	{
		return HttpS.AddComponent(null, _url, _tag, null, _headers, _queueTags, _destroyAfterDone, true);
	}

	// Token: 0x06002504 RID: 9476 RVA: 0x00199287 File Offset: 0x00197687
	public static HttpC AddPostComponent(string _url, string _tag, Hashtable _postHeader = null, string[] _queueTags = null, bool _destroyAfterDone = true)
	{
		if (_postHeader == null)
		{
			_postHeader = HttpS.defaultHeaders("application/octet-stream");
		}
		return HttpS.AddComponent(null, _url, _tag, new byte[1], _postHeader, _queueTags, _destroyAfterDone, true);
	}

	// Token: 0x06002505 RID: 9477 RVA: 0x001992AE File Offset: 0x001976AE
	public static HttpC AddPostComponent(string _url, string _tag, string _JSON, Hashtable _postHeader = null, string[] _queueTags = null, bool _destroyAfterDone = true)
	{
		if (_postHeader == null)
		{
			_postHeader = HttpS.defaultHeaders("application/json");
		}
		return HttpS.AddComponent(null, _url, _tag, Encoding.UTF8.GetBytes(_JSON), _postHeader, _queueTags, _destroyAfterDone, true);
	}

	// Token: 0x06002506 RID: 9478 RVA: 0x001992DB File Offset: 0x001976DB
	public static HttpC AddPostComponent(string _url, string _tag, byte[] _postData = null, Hashtable _postHeader = null, string[] _queueTags = null, bool _destroyAfterDone = true, bool _disposeWhenDestroyed = true)
	{
		if (_postHeader == null)
		{
			_postHeader = HttpS.defaultHeaders("application/octet-stream");
		}
		return HttpS.AddComponent(null, _url, _tag, _postData, _postHeader, _queueTags, _destroyAfterDone, _disposeWhenDestroyed);
	}

	// Token: 0x06002507 RID: 9479 RVA: 0x00199300 File Offset: 0x00197700
	private static HttpC AddComponent(Entity _entity, string _url, string _tag, byte[] _postData = null, Hashtable _postHeader = null, string[] _queueTags = null, bool _destroyAfterDone = true, bool _disposeWhenDestroyed = true)
	{
		HttpC httpC = HttpS.m_components.AddItem();
		httpC.tag = _tag;
		httpC.queueTags = _queueTags;
		httpC.url = _url;
		httpC.destroyAfterDone = _destroyAfterDone;
		httpC.disposeWhenDestroyed = _disposeWhenDestroyed;
		httpC.timeOut = Main.m_gameTimeSinceAppStarted + HttpS.m_localTimeOut;
		httpC.wwwUrl = _url;
		httpC.wwwData = _postData;
		if (_postHeader != null)
		{
			httpC.wwwHeaders = new Dictionary<string, string>();
			IEnumerator enumerator = _postHeader.Keys.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					string text = (string)obj;
					httpC.wwwHeaders.Add(text, (string)_postHeader[text]);
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
		}
		else
		{
			httpC.wwwHeaders = null;
		}
		HttpS.m_requestList.Add(httpC);
		if (_entity != null)
		{
			EntityManager.AddComponentToEntity(_entity, httpC);
		}
		else
		{
			httpC.m_active = true;
			httpC.m_wasActive = true;
		}
		return httpC;
	}

	// Token: 0x06002508 RID: 9480 RVA: 0x00199414 File Offset: 0x00197814
	public static void SetInActive(HttpC _C)
	{
		HttpS.m_maxStreams++;
	}

	// Token: 0x06002509 RID: 9481 RVA: 0x00199424 File Offset: 0x00197824
	public static void RemoveComponent(HttpC _c)
	{
		if (_c.www != null)
		{
			HttpS.m_aliveWWWStreams--;
			if (HttpS.m_maxStreams > 10)
			{
				HttpS.m_maxStreams--;
			}
			_c.www = null;
		}
		if (HttpS.m_requestList.Contains(_c))
		{
			HttpS.m_requestList.Remove(_c);
		}
		EntityManager.RemoveComponentFromEntity(_c);
		HttpS.m_components.RemoveItem(_c);
	}

	// Token: 0x0600250A RID: 9482 RVA: 0x00199494 File Offset: 0x00197894
	public static bool QueueTagsExist(string[] _tags)
	{
		if (HttpS.m_gatesFreeze || ((_tags == null || _tags.Length == 0) && HttpS.m_components.m_aliveCount > 0))
		{
			bool flag = false;
			for (int i = HttpS.m_components.m_array.Length - 1; i > -1; i--)
			{
				HttpC httpC = HttpS.m_components.m_array[i];
				if (httpC != null && httpC.m_active && !httpC.ignoreInQueue)
				{
					flag = true;
					break;
				}
			}
			return flag;
		}
		if ((_tags == null || _tags.Length == 0) && HttpS.m_components.m_aliveCount == 0)
		{
			return false;
		}
		for (int j = HttpS.m_components.m_array.Length - 1; j > -1; j--)
		{
			HttpC httpC2 = HttpS.m_components.m_array[j];
			if (httpC2 != null && httpC2.m_active && httpC2.queueTags != null && !httpC2.ignoreInQueue)
			{
				for (int k = 0; k < httpC2.queueTags.Length; k++)
				{
					for (int l = 0; l < _tags.Length; l++)
					{
						if (httpC2.queueTags[k].IndexOf(_tags[l]) >= 0)
						{
							return true;
						}
					}
				}
			}
		}
		return false;
	}

	// Token: 0x0600250B RID: 9483 RVA: 0x001995E8 File Offset: 0x001979E8
	public static void Update()
	{
		double gameTimeSinceAppStarted = Main.m_gameTimeSinceAppStarted;
		if (HttpS.m_aliveWWWStreams < HttpS.m_maxStreams)
		{
			int num = Mathf.Min(1, HttpS.m_requestList.Count);
			for (int i = 0; i < num; i++)
			{
				HttpS.m_requestList[i].FireRequest();
				HttpS.m_aliveWWWStreams++;
			}
			for (int j = 0; j < num; j++)
			{
				HttpS.m_requestList.Remove(HttpS.m_requestList[j]);
				j--;
				num--;
			}
		}
		for (int k = HttpS.m_components.m_aliveCount - 1; k > -1; k--)
		{
			if (HttpS.m_components.m_aliveCount <= 0)
			{
				break;
			}
			HttpC httpC = HttpS.m_components.m_array[HttpS.m_components.m_aliveIndices[k]];
			if (httpC.m_active && httpC.www != null)
			{
				if (httpC.www.isDone)
				{
					if (!string.IsNullOrEmpty(httpC.www.error) && !httpC.executed)
					{
						Debug.LogWarning("WWWRequest error " + httpC.www.error + " from " + httpC.url);
						httpC.RequestFailed();
					}
					else if (httpC.debugKeyCode != null)
					{
						if (Input.GetKey(httpC.debugKeyCode))
						{
							httpC.RequestComplete();
							httpC.destroyAfterDone = true;
						}
					}
					else
					{
						httpC.RequestComplete();
					}
					if (httpC.destroyAfterDone && httpC.canRemove && !HttpS.m_gatesFreeze && HttpS.m_components.m_aliveCount > 0)
					{
						HttpS.m_removeComponentList.Add(httpC);
					}
				}
				else if (httpC.www.progress <= 0f)
				{
					if (gameTimeSinceAppStarted > httpC.timeOut)
					{
						if (!httpC.executed)
						{
							Debug.LogWarning("WWWRequest locally timed out from " + httpC.url);
						}
						httpC.RequestFailed();
						if (httpC.destroyAfterDone && httpC.canRemove && !HttpS.m_gatesFreeze)
						{
							HttpS.m_removeComponentList.Add(httpC);
						}
					}
				}
			}
		}
		while (HttpS.m_removeComponentList.Count > 0)
		{
			int num2 = HttpS.m_removeComponentList.Count - 1;
			HttpS.RemoveComponent(HttpS.m_removeComponentList[num2]);
			HttpS.m_removeComponentList.RemoveAt(num2);
		}
		HttpS.m_components.Update();
	}

	// Token: 0x0600250C RID: 9484 RVA: 0x0019988C File Offset: 0x00197C8C
	public static void ClearAllRequests(HttpC _exception = null)
	{
		for (int i = HttpS.m_components.m_aliveCount - 1; i > -1; i--)
		{
			HttpC httpC = HttpS.m_components.m_array[HttpS.m_components.m_aliveIndices[i]];
			if (_exception == null || httpC != _exception)
			{
				if (httpC.m_active && httpC.www != null)
				{
					HttpS.m_removeComponentList.Add(httpC);
				}
			}
		}
		while (HttpS.m_removeComponentList.Count > 0)
		{
			int num = HttpS.m_removeComponentList.Count - 1;
			HttpS.RemoveComponent(HttpS.m_removeComponentList[num]);
			HttpS.m_removeComponentList.RemoveAt(num);
		}
		HttpS.m_requestList.Clear();
		HttpS.m_components.Update();
	}

	// Token: 0x0600250D RID: 9485 RVA: 0x00199954 File Offset: 0x00197D54
	private static Hashtable defaultHeaders(string _contentType)
	{
		Hashtable hashtable = new Hashtable();
		hashtable.Add("Content-Type", _contentType);
		return hashtable;
	}

	// Token: 0x04002AE4 RID: 10980
	public static double m_localTimeOut = 20.0;

	// Token: 0x04002AE5 RID: 10981
	public static DynamicArray<HttpC> m_components;

	// Token: 0x04002AE6 RID: 10982
	private static List<HttpC> m_removeComponentList;

	// Token: 0x04002AE7 RID: 10983
	private static List<HttpC> m_requestList;

	// Token: 0x04002AE8 RID: 10984
	private static int m_aliveWWWStreams;

	// Token: 0x04002AE9 RID: 10985
	private const int MAX_STREAMS = 10;

	// Token: 0x04002AEA RID: 10986
	private static int m_maxStreams;

	// Token: 0x04002AEB RID: 10987
	public static bool m_gatesFreeze;
}
