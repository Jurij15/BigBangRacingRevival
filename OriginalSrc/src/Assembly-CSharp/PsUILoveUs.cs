using System;
using UnityEngine;

// Token: 0x02000312 RID: 786
public class PsUILoveUs : UIVerticalList
{
	// Token: 0x0600173A RID: 5946 RVA: 0x000FA478 File Offset: 0x000F8878
	public PsUILoveUs(UIComponent _parent)
		: base(_parent, string.Empty)
	{
		this.SetWidth(1f, RelativeTo.ParentWidth);
		this.SetSpacing(0.03f, RelativeTo.ScreenHeight);
		this.SetMargins(0f, 0f, 0f, 0.015f, RelativeTo.ScreenHeight);
		this.RemoveDrawHandler();
		UICanvas uicanvas = new UICanvas(this, false, string.Empty, null, string.Empty);
		uicanvas.SetHeight(0.04f, RelativeTo.ScreenHeight);
		uicanvas.SetWidth(1f, RelativeTo.ParentWidth);
		uicanvas.SetVerticalAlign(1f);
		uicanvas.RemoveDrawHandler();
		UIFittedText uifittedText = new UIFittedText(uicanvas, false, string.Empty, PsStrings.Get(StringID.SETTINGS_LOVEUS), PsFontManager.GetFont(PsFonts.KGSecondChances), true, "ffffff", null);
		uifittedText.SetMargins(0.05f, 0f, 0f, 0f, RelativeTo.ScreenHeight);
		UICanvas uicanvas2 = new UICanvas(uifittedText, false, string.Empty, null, string.Empty);
		uicanvas2.SetSize(0.04f, 0.04f, RelativeTo.ScreenHeight);
		uicanvas2.SetHorizontalAlign(0f);
		uicanvas2.SetMargins(-0.05f, 0.05f, 0f, 0f, RelativeTo.ScreenHeight);
		uicanvas2.RemoveDrawHandler();
		UIFittedSprite uifittedSprite = new UIFittedSprite(uicanvas2, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_icon_heart", null), true, true);
		uifittedSprite.SetHeight(1f, RelativeTo.ParentHeight);
		uifittedSprite.SetOverrideShader(Shader.Find("WOE/Unlit/ColorUnlitTransparent"));
		uifittedSprite.SetColor(DebugDraw.HexToColor("#ee483c"));
		UIHorizontalList uihorizontalList = new UIHorizontalList(this, string.Empty);
		uihorizontalList.SetHeight(0.068f, RelativeTo.ScreenHeight);
		uihorizontalList.SetVerticalAlign(0f);
		uihorizontalList.SetSpacing(0.02f, RelativeTo.ScreenHeight);
		uihorizontalList.SetMargins(0.012f, 0.012f, 0.018f, 0.018f, RelativeTo.ScreenHeight);
		uihorizontalList.SetDrawHandler(new UIDrawDelegate(this.LikeAreaDrawhandler));
		this.m_likeFB = new PsUIGenericButton(uihorizontalList, 0.25f, 0.25f, 0.005f, "Button");
		this.m_likeFB.SetIcon("menu_icon_facebook", 0.045f, RelativeTo.ScreenShortest, "#FFFFFF", default(cpBB));
		this.m_likeFB.SetLovelyRedColors();
		this.m_likeIG = new PsUIGenericButton(uihorizontalList, 0.25f, 0.25f, 0.005f, "Button");
		this.m_likeIG.SetIcon("menu_icon_instagram", 0.045f, RelativeTo.ScreenShortest, "#FFFFFF", default(cpBB));
		this.m_likeIG.SetLovelyRedColors();
		this.m_forum = new PsUIGenericButton(uihorizontalList, 0.25f, 0.25f, 0.005f, "Button");
		this.m_forum.SetIconOnly("menu_icon_discord", 0.055f, RelativeTo.ScreenShortest, "#FFFFFF", default(cpBB));
		cpBB cpBB = new cpBB(0f, -0.007f, 0f, -0.007f);
		this.m_forum.SetLovelyRedColors();
	}

	// Token: 0x0600173B RID: 5947 RVA: 0x000FA75C File Offset: 0x000F8B5C
	private void LikeAreaDrawhandler(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] roundedRect = DebugDraw.GetRoundedRect(_c.m_actualWidth, _c.m_actualHeight * 1.5f, 0.0075f * (float)Screen.height, 8, Vector2.zero);
		GGData ggdata = new GGData(roundedRect);
		Color color = DebugDraw.HexToColor("#4176a9");
		PrefabS.CreateFlatPrefabComponentsFromPolygon(_c.m_TC, Vector3.forward * 2f, ggdata, color, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward, roundedRect, 0.0085f * (float)Screen.height, color, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line8Mat_Material), _c.m_camera, Position.Center, true);
	}

	// Token: 0x0600173C RID: 5948 RVA: 0x000FA814 File Offset: 0x000F8C14
	public override void Step()
	{
		if (this.m_likeFB != null && this.m_likeFB.m_hit)
		{
			PsMetrics.FacebookPageOpened();
			if (this.CheckPackageAppIsPresent("com.facebook.katana"))
			{
				Application.OpenURL("fb://page/1650097745233577");
			}
			else
			{
				Application.OpenURL("https://www.facebook.com/1650097745233577");
			}
		}
		else if (this.m_likeIG != null && this.m_likeIG.m_hit)
		{
			PsMetrics.InstagramPageOpened();
			Application.OpenURL("https://www.instagram.com/_u/bigbangracinggame");
		}
		else if (this.m_forum != null && this.m_forum.m_hit)
		{
			Application.OpenURL("https://discord.gg/m4UAAEb");
			PsMetrics.ForumPageOpened();
		}
		base.Step();
	}

	// Token: 0x0600173D RID: 5949 RVA: 0x000FA8D0 File Offset: 0x000F8CD0
	private bool CheckPackageAppIsPresent(string package)
	{
		AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject @static = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
		AndroidJavaObject androidJavaObject = @static.Call<AndroidJavaObject>("getPackageManager", new object[0]);
		AndroidJavaObject androidJavaObject2 = androidJavaObject.Call<AndroidJavaObject>("getInstalledPackages", new object[] { 0 });
		int num = androidJavaObject2.Call<int>("size", new object[0]);
		for (int i = 0; i < num; i++)
		{
			AndroidJavaObject androidJavaObject3 = androidJavaObject2.Call<AndroidJavaObject>("get", new object[] { i });
			string text = androidJavaObject3.Get<string>("packageName");
			if (text.CompareTo(package) == 0)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x040019FA RID: 6650
	private PsUIGenericButton m_likeFB;

	// Token: 0x040019FB RID: 6651
	private PsUIGenericButton m_likeIG;

	// Token: 0x040019FC RID: 6652
	private PsUIGenericButton m_forum;
}
