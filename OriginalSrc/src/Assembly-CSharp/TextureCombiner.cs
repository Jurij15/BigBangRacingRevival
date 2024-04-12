using System;
using UnityEngine;

// Token: 0x020001D2 RID: 466
public static class TextureCombiner
{
	// Token: 0x06000E0A RID: 3594 RVA: 0x00083480 File Offset: 0x00081880
	public static Texture2D CombineVehicleTextures(Texture2D baseDiffuse, Texture2D baseMask, Texture2D patternMask, Color baseColor, Color patternColor)
	{
		Texture2D texture2D = new Texture2D(baseDiffuse.width, baseDiffuse.height);
		Color[] pixels = baseDiffuse.GetPixels();
		Color[] pixels2 = baseMask.GetPixels();
		Color[] pixels3 = patternMask.GetPixels();
		for (int i = 0; i < pixels.GetLength(0); i++)
		{
			Color color = pixels[i];
			Color color2 = pixels2[i];
			Color color3 = pixels3[i];
			Color color4 = Color.Lerp(baseColor, patternColor, color3.r);
			Color color5 = Color.Lerp(color, color * color4, color2.r);
			pixels[i] = color5;
		}
		texture2D.SetPixels(pixels);
		texture2D.Apply();
		texture2D.Compress(false);
		return texture2D;
	}

	// Token: 0x06000E0B RID: 3595 RVA: 0x0008354B File Offset: 0x0008194B
	public static void CombineCharacterTextures()
	{
	}
}
