using System;

// Token: 0x02000175 RID: 373
public static class PsStrings
{
	// Token: 0x06000C7D RID: 3197 RVA: 0x00076410 File Offset: 0x00074810
	public static string Get(StringID id)
	{
		string text = Translations.translations[PsStrings.m_currentLanguage].GetString((int)id);
		if (string.IsNullOrEmpty(text))
		{
			text = Translations.translations[0].GetString((int)id);
		}
		return text;
	}

	// Token: 0x06000C7E RID: 3198 RVA: 0x00076449 File Offset: 0x00074849
	public static Language GetLanguage()
	{
		return (Language)PsStrings.m_currentLanguage;
	}

	// Token: 0x06000C7F RID: 3199 RVA: 0x00076450 File Offset: 0x00074850
	public static string Get(string stringLabel)
	{
		for (int i = 0; i < StringTable.stringIDs.Length; i++)
		{
			if (StringTable.stringIDs[i].Equals(stringLabel))
			{
				return PsStrings.Get((StringID)i);
			}
		}
		return stringLabel;
	}

	// Token: 0x04000BFC RID: 3068
	public static int m_currentLanguage;
}
