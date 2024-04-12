using System;
using UnityEngine;

// Token: 0x0200041E RID: 1054
public static class GhostManager
{
	// Token: 0x06001D58 RID: 7512 RVA: 0x0014F4A8 File Offset: 0x0014D8A8
	public static void Initialize()
	{
		if (GhostManager.m_ghostSpriteSheet == null)
		{
			GhostManager.m_ghostSpriteSheet = SpriteS.AddSpriteSheet(CameraS.m_mainCamera, ResourceManager.GetMaterial(RESOURCE.IngameAtlasMat_Material), ResourceManager.GetTextAsset(RESOURCE.UiAtlas_TextAsset), 1f);
			GhostManager.m_ghostSpriteSheet.m_material.shader = Shader.Find("WOE/Unlit/ColorUnlitTransparentOverlay");
		}
		else
		{
			Debug.LogError("Not destroyed properly!");
		}
	}

	// Token: 0x06001D59 RID: 7513 RVA: 0x0014F50F File Offset: 0x0014D90F
	public static void Free()
	{
		if (GhostManager.m_ghostSpriteSheet != null)
		{
			SpriteS.RemoveSpriteSheet(GhostManager.m_ghostSpriteSheet);
			GhostManager.m_ghostSpriteSheet = null;
		}
	}

	// Token: 0x04002031 RID: 8241
	public static SpriteSheet m_ghostSpriteSheet;
}
