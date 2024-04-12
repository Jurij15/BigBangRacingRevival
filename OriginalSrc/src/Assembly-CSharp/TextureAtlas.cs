using System;
using System.Collections;
using System.Collections.Generic;
using MiniJSON;
using UnityEngine;

// Token: 0x02000558 RID: 1368
public class TextureAtlas
{
	// Token: 0x060027ED RID: 10221 RVA: 0x001AABCC File Offset: 0x001A8FCC
	public TextureAtlas(TextAsset _JSON)
	{
		string text = _JSON.text;
		Dictionary<string, object> dictionary = Json.Deserialize(text) as Dictionary<string, object>;
		List<object> list = dictionary["frames"] as List<object>;
		for (int i = 0; i < list.Count; i++)
		{
			TextureAtlas.AtlasFrame atlasFrame = default(TextureAtlas.AtlasFrame);
			Dictionary<string, object> dictionary2 = list[i] as Dictionary<string, object>;
			string text2 = dictionary2["filename"] as string;
			text2 = text2.Substring(0, text2.Length - 4);
			atlasFrame.name = text2;
			Dictionary<string, object> dictionary3 = dictionary2["frame"] as Dictionary<string, object>;
			atlasFrame.rect = new Rect((float)((long)dictionary3["x"]), (float)((long)dictionary3["y"]), (float)((long)dictionary3["w"]), (float)((long)dictionary3["h"]));
			atlasFrame.rotated = (bool)dictionary2["rotated"];
			atlasFrame.trimmed = (bool)dictionary2["trimmed"];
			this.m_frames.Add(text2, atlasFrame);
		}
	}

	// Token: 0x060027EE RID: 10222 RVA: 0x001AAD18 File Offset: 0x001A9118
	public Frame GetFrame(string _frameName, string _fallBack = null)
	{
		if (!string.IsNullOrEmpty(_frameName) && this.m_frames.ContainsKey(_frameName))
		{
			TextureAtlas.AtlasFrame atlasFrame = (TextureAtlas.AtlasFrame)this.m_frames[_frameName];
			return new Frame(atlasFrame.rect.x, atlasFrame.rect.y, atlasFrame.rect.width, atlasFrame.rect.height);
		}
		if (!string.IsNullOrEmpty(_fallBack) && this.m_frames.ContainsKey(_fallBack))
		{
			TextureAtlas.AtlasFrame atlasFrame2 = (TextureAtlas.AtlasFrame)this.m_frames[_fallBack];
			return new Frame(atlasFrame2.rect.x, atlasFrame2.rect.y, atlasFrame2.rect.width, atlasFrame2.rect.height);
		}
		Debug.LogWarning("Requested frame (" + _frameName + ") missing! Defaulting to _unknown");
		TextureAtlas.AtlasFrame atlasFrame3 = (TextureAtlas.AtlasFrame)this.m_frames["_unknown"];
		return new Frame(atlasFrame3.rect.x, atlasFrame3.rect.y, atlasFrame3.rect.width, atlasFrame3.rect.height);
	}

	// Token: 0x060027EF RID: 10223 RVA: 0x001AAE58 File Offset: 0x001A9258
	public string[] GetFrameNames(string _contains = "")
	{
		List<string> list = new List<string>();
		IDictionaryEnumerator enumerator = this.m_frames.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				if (_contains == string.Empty || dictionaryEntry.Key.ToString().Contains(_contains))
				{
					list.Add(dictionaryEntry.Key.ToString());
				}
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
		return list.ToArray();
	}

	// Token: 0x04002D70 RID: 11632
	private Hashtable m_frames = new Hashtable();

	// Token: 0x02000559 RID: 1369
	private struct AtlasFrame
	{
		// Token: 0x04002D71 RID: 11633
		public string name;

		// Token: 0x04002D72 RID: 11634
		public bool rotated;

		// Token: 0x04002D73 RID: 11635
		public bool trimmed;

		// Token: 0x04002D74 RID: 11636
		public Rect rect;
	}
}
