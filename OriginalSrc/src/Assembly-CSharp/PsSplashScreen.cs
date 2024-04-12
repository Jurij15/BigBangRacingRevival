using System;
using UnityEngine;

// Token: 0x020003A6 RID: 934
public class PsSplashScreen : UICanvas
{
	// Token: 0x06001AB9 RID: 6841 RVA: 0x00129BB0 File Offset: 0x00127FB0
	public PsSplashScreen(UIComponent _parent)
		: base(_parent, false, "SplashScreen", null, string.Empty)
	{
		this.SetWidth(1f, RelativeTo.ScreenWidth);
		this.SetHeight(1f, RelativeTo.ScreenHeight);
		this.SetDrawHandler(new UIDrawDelegate(this.SplashBackgroundDrawhandler));
		EntityManager.RemoveAllTagsFromEntity(this.m_TC.p_entity, false);
	}

	// Token: 0x06001ABA RID: 6842 RVA: 0x00129C0C File Offset: 0x0012800C
	private void SplashBackgroundDrawhandler(UIComponent _c)
	{
		this.m_splashTexture = Resources.Load("PlaySomething/Menu/Textures/splash_universal_tournaments") as Texture2D;
		this.m_splashMaterial = Object.Instantiate<Material>(ResourceManager.GetMaterial(RESOURCE.ProfileMat_Material));
		this.m_splashMaterial.mainTexture = this.m_splashTexture;
		float num = (float)Screen.width / (float)this.m_splashTexture.width;
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] rect = DebugDraw.GetRect((float)this.m_splashTexture.width * num, (float)this.m_splashTexture.height * num, Vector2.zero);
		PrefabS.CreateFlatPrefabComponentsFromVectorArray(_c.m_TC, Vector3.forward * 5f, rect, DebugDraw.HexToUint("ffffff"), DebugDraw.HexToUint("ffffff"), this.m_splashMaterial, _c.m_camera, "Splash", UVRect.Normal());
	}

	// Token: 0x06001ABB RID: 6843 RVA: 0x00129CE6 File Offset: 0x001280E6
	public override void Destroy()
	{
		if (this.m_splashMaterial != null)
		{
			Object.Destroy(this.m_splashMaterial);
			this.m_splashMaterial = null;
		}
		Resources.UnloadAsset(this.m_splashTexture);
		this.m_splashTexture = null;
		base.Destroy();
	}

	// Token: 0x04001D2B RID: 7467
	public Material m_splashMaterial;

	// Token: 0x04001D2C RID: 7468
	public Texture2D m_splashTexture;
}
