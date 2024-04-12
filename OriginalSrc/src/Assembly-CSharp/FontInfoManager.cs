using System;
using System.Collections.Generic;

// Token: 0x020004BD RID: 1213
public static class FontInfoManager
{
	// Token: 0x06002297 RID: 8855 RVA: 0x00190460 File Offset: 0x0018E860
	public static void AddInfo(string _fontPathName, int _characterSize, float _lineSpacing)
	{
		if (FontInfoManager.m_infos == null)
		{
			FontInfoManager.m_infos = new Dictionary<string, FontInfo>();
		}
		if (!FontInfoManager.m_infos.ContainsKey(_fontPathName))
		{
			FontInfo fontInfo = default(FontInfo);
			fontInfo.lineSpacing = _lineSpacing;
			fontInfo.characterSize = _characterSize;
			FontInfoManager.m_infos.Add(_fontPathName, fontInfo);
		}
	}

	// Token: 0x06002298 RID: 8856 RVA: 0x001904B8 File Offset: 0x0018E8B8
	public static FontInfo GetFontInfo(string _fontPathName)
	{
		if (FontInfoManager.m_infos != null && FontInfoManager.m_infos.ContainsKey(_fontPathName))
		{
			return FontInfoManager.m_infos[_fontPathName];
		}
		return new FontInfo
		{
			lineSpacing = 1f,
			characterSize = 10
		};
	}

	// Token: 0x040028B2 RID: 10418
	private static Dictionary<string, FontInfo> m_infos;
}
