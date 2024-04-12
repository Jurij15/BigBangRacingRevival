using System;

// Token: 0x0200016F RID: 367
public static class PsFontManager
{
	// Token: 0x06000C59 RID: 3161 RVA: 0x00075383 File Offset: 0x00073783
	public static string GetFont(PsFonts _font)
	{
		if (PlayerPrefsX.GetLanguage() == Language.Ru)
		{
			return "OpenSans_Font";
		}
		return _font.ToString() + "_Font";
	}
}
