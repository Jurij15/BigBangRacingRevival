using System;
using UnityEngine;

// Token: 0x020002E1 RID: 737
public class PsUITopStartRacing : UICanvas
{
	// Token: 0x060015C1 RID: 5569 RVA: 0x000E21C0 File Offset: 0x000E05C0
	public PsUITopStartRacing(UIComponent _parent)
		: base(_parent, false, "TopContent", null, string.Empty)
	{
		this.SetHeight(0.06f, RelativeTo.ScreenHeight);
		this.SetWidth(1f, RelativeTo.ScreenWidth);
		this.SetAlign(0.5f, 1f);
		this.SetDrawHandler(new UIDrawDelegate(this.TopDrawhandler));
		UIHorizontalList uihorizontalList = new UIHorizontalList(this, "UpperLeft");
		uihorizontalList.SetMargins(0.025f, RelativeTo.ScreenShortest);
		uihorizontalList.SetSpacing(0.025f, RelativeTo.ScreenShortest);
		uihorizontalList.SetAlign(0f, 1f);
		uihorizontalList.RemoveDrawHandler();
		this.m_exitButton = new PsUIGenericButton(uihorizontalList, 0.25f, 0.25f, 0.005f, "Button");
		this.m_exitButton.SetSound("/UI/ExitLevel");
		this.m_exitButton.SetIcon("hud_icon_menu_exit", 0.06f, RelativeTo.ScreenShortest, "#FFFFFF", default(cpBB));
		this.m_exitButton.SetOrangeColors(true);
		this.m_exitButton.SetDepthOffset(-5f);
		if (PsState.m_levelLeaderboardsEnabled)
		{
			this.m_leaderboardButton = new PsUIGenericButton(uihorizontalList, 0.25f, 0.25f, 0.005f, "Button");
			this.m_leaderboardButton.SetIcon("menu_icon_leaderboards", 0.06f, RelativeTo.ScreenShortest, "#FFFFFF", default(cpBB));
			this.m_leaderboardButton.SetDepthOffset(-10f);
		}
	}

	// Token: 0x060015C2 RID: 5570 RVA: 0x000E2324 File Offset: 0x000E0724
	public void TopDrawhandler(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] rect = DebugDraw.GetRect(_c.m_actualWidth, _c.m_actualHeight, Vector2.zero, true);
		Color black = Color.black;
		Color black2 = Color.black;
		Color black3 = Color.black;
		black3.a = 0.5f;
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * -1f, rect, (float)Screen.height * 0.0075f, black, black2, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line4Mat_Material), _c.m_camera, Position.Center, true);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * 1f, rect, (float)Screen.height * 0.015f, black3, black3, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line4Mat_Material), _c.m_camera, Position.Center, true);
		GGData ggdata = new GGData(rect);
		PrefabS.CreateFlatPrefabComponentsFromPolygon(_c.m_TC, Vector3.zero, ggdata, black, black2, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera);
	}

	// Token: 0x060015C3 RID: 5571 RVA: 0x000E241D File Offset: 0x000E081D
	public void RemoveButton()
	{
		if (this.m_exitButton != null)
		{
			this.m_exitButton.Destroy();
		}
		this.m_exitButton = null;
	}

	// Token: 0x060015C4 RID: 5572 RVA: 0x000E243C File Offset: 0x000E083C
	public void Skip()
	{
		(this.GetRoot() as PsUIBasePopup).CallAction("Skip");
	}

	// Token: 0x060015C5 RID: 5573 RVA: 0x000E2454 File Offset: 0x000E0854
	public void CancelSkip()
	{
	}

	// Token: 0x060015C6 RID: 5574 RVA: 0x000E2458 File Offset: 0x000E0858
	public override void Step()
	{
		if (this.m_exitButton != null && this.m_exitButton.m_TC.p_entity != null && this.m_exitButton.m_TC.p_entity.m_active && (this.m_exitButton.m_hit || Main.AndroidBackButtonPressed((this.GetRoot() as PsUIBasePopup).m_guid)))
		{
			(this.GetRoot() as PsUIBasePopup).CallAction("Exit");
		}
		if (this.m_leaderboardButton != null && this.m_leaderboardButton.m_hit)
		{
			PsUIBasePopup popup = new PsUIBasePopup(typeof(PsUICenterLeaderboardLevel), null, null, null, true, true, InitialPage.Center, false, false, false);
			popup.SetAction("Exit", delegate
			{
				popup.Destroy();
			});
		}
		base.Step();
	}

	// Token: 0x0400186C RID: 6252
	private PsUIGenericButton m_exitButton;

	// Token: 0x0400186D RID: 6253
	private PsUIGenericButton m_leaderboardButton;

	// Token: 0x0400186E RID: 6254
	private UIVerticalList m_ghost;
}
