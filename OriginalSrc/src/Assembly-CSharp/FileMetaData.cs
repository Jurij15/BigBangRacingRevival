using System;
using System.Collections.Generic;
using MiniJSON;

// Token: 0x020003FC RID: 1020
public class FileMetaData
{
	// Token: 0x06001C68 RID: 7272 RVA: 0x00140CFA File Offset: 0x0013F0FA
	public FileMetaData()
	{
		this.name = string.Empty;
		this.type = string.Empty;
		this.path = string.Empty;
		this.version = -1;
	}

	// Token: 0x06001C69 RID: 7273 RVA: 0x00140D2A File Offset: 0x0013F12A
	public FileMetaData(string _json)
	{
		this.ParseFromJSON(_json);
	}

	// Token: 0x06001C6A RID: 7274 RVA: 0x00140D39 File Offset: 0x0013F139
	public FileMetaData(Dictionary<string, object> _dict)
	{
		this.ParseFromDictionary(_dict);
	}

	// Token: 0x06001C6B RID: 7275 RVA: 0x00140D48 File Offset: 0x0013F148
	public void ParseFromJSON(string _json)
	{
		this.ParseFromDictionary(Json.Deserialize(_json) as Dictionary<string, object>);
	}

	// Token: 0x06001C6C RID: 7276 RVA: 0x00140D5C File Offset: 0x0013F15C
	public void ParseFromDictionary(Dictionary<string, object> _dict)
	{
		if (_dict.ContainsKey("name"))
		{
			this.name = (string)_dict["name"];
		}
		if (_dict.ContainsKey("type"))
		{
			this.type = (string)_dict["type"];
		}
		if (_dict.ContainsKey("path"))
		{
			this.path = (string)_dict["path"];
		}
		if (_dict.ContainsKey("version"))
		{
			this.version = Convert.ToInt32(_dict["version"]);
		}
	}

	// Token: 0x04001EF8 RID: 7928
	public string name;

	// Token: 0x04001EF9 RID: 7929
	public string type;

	// Token: 0x04001EFA RID: 7930
	public string path;

	// Token: 0x04001EFB RID: 7931
	public int version;
}
