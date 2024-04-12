using System;
using System.Collections;

// Token: 0x0200045F RID: 1119
public class WOEURL : URL
{
	// Token: 0x06001ECF RID: 7887 RVA: 0x0015A74C File Offset: 0x00158B4C
	public WOEURL(string _command)
	{
		if (!string.IsNullOrEmpty(_command))
		{
			this.host = "https://woeprod.traplightgames.com";
			this.command = _command;
		}
	}

	// Token: 0x06001ED0 RID: 7888 RVA: 0x0015A771 File Offset: 0x00158B71
	public HttpC AddGetComponent(string _tag, string[] _queueTags = null, bool _destroyAfterDone = true)
	{
		return HttpS.AddGetComponent(base.ConstructURL(), _tag, _queueTags, _destroyAfterDone, this.ConstructHeaders());
	}

	// Token: 0x06001ED1 RID: 7889 RVA: 0x0015A788 File Offset: 0x00158B88
	public HttpC AddPostComponent(string _tag)
	{
		Hashtable hashtable = this.ConstructHeaders();
		return HttpS.AddPostComponent(base.ConstructURL(), _tag, hashtable, null, true);
	}

	// Token: 0x06001ED2 RID: 7890 RVA: 0x0015A7AB File Offset: 0x00158BAB
	public HttpC AddPostComponent(string _tag, string _JSON, Hashtable _postHeader = null, string[] _queueTags = null, bool _destroyAfterDone = true)
	{
		if (_postHeader != null)
		{
			_postHeader = this.AddGameHeaders(_postHeader, _JSON);
		}
		else
		{
			_postHeader = this.ConstructHeaders(_JSON);
		}
		return HttpS.AddPostComponent(base.ConstructURL(), _tag, _JSON, _postHeader, _queueTags, _destroyAfterDone);
	}

	// Token: 0x06001ED3 RID: 7891 RVA: 0x0015A7E0 File Offset: 0x00158BE0
	public HttpC AddPostComponent(string _tag, byte[] _data, Hashtable _postHeader = null, string[] _queueTags = null, bool _destroyAfterDone = true)
	{
		string text = base.ConstructURL();
		if (_postHeader == null)
		{
			_postHeader = this.ConstructHeaders(_data);
		}
		else
		{
			_postHeader = this.AddGameHeaders(_postHeader, _data);
			_postHeader.Add("Content-Type", "application/octet-stream");
		}
		return HttpS.AddPostComponent(text, _tag, _data, _postHeader, _queueTags, _destroyAfterDone, true);
	}

	// Token: 0x06001ED4 RID: 7892 RVA: 0x0015A830 File Offset: 0x00158C30
	private Hashtable ConstructHeaders()
	{
		Hashtable hashtable = new Hashtable();
		return this.AddGameHeaders(hashtable);
	}

	// Token: 0x06001ED5 RID: 7893 RVA: 0x0015A84C File Offset: 0x00158C4C
	private Hashtable ConstructHeaders(byte[] _data)
	{
		Hashtable hashtable = new Hashtable();
		hashtable.Add("Content-Type", "application/octet-stream");
		return this.AddGameHeaders(hashtable, _data);
	}

	// Token: 0x06001ED6 RID: 7894 RVA: 0x0015A878 File Offset: 0x00158C78
	private Hashtable ConstructHeaders(string _json)
	{
		Hashtable hashtable = new Hashtable();
		hashtable.Add("Content-Type", "application/json");
		return this.AddGameHeaders(hashtable, _json);
	}

	// Token: 0x06001ED7 RID: 7895 RVA: 0x0015A8A3 File Offset: 0x00158CA3
	private Hashtable AddGameHeaders(Hashtable _headers)
	{
		_headers.Add("PLAY_HASH", ClientTools.GenerateHash(base.ConstructURI()));
		return this.AddSessionHeaders(_headers);
	}

	// Token: 0x06001ED8 RID: 7896 RVA: 0x0015A8C2 File Offset: 0x00158CC2
	private Hashtable AddGameHeaders(Hashtable _headers, byte[] _data)
	{
		_headers.Add("PLAY_HASH", ClientTools.GenerateHash(base.ConstructURI(), _data));
		return this.AddSessionHeaders(_headers);
	}

	// Token: 0x06001ED9 RID: 7897 RVA: 0x0015A8E2 File Offset: 0x00158CE2
	private Hashtable AddGameHeaders(Hashtable _headers, string _json)
	{
		_headers.Add("PLAY_HASH", ClientTools.GenerateHash(base.ConstructURI() + _json));
		return this.AddSessionHeaders(_headers);
	}

	// Token: 0x06001EDA RID: 7898 RVA: 0x0015A907 File Offset: 0x00158D07
	private Hashtable AddSessionHeaders(Hashtable _headers)
	{
		_headers.Add("SESSION_ID", PsState.m_sessionId);
		if (PlayerPrefsX.GetUserId() != null)
		{
			_headers.Add("PLAYER_ID", PlayerPrefsX.GetUserId());
		}
		return _headers;
	}
}
