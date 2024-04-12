using System;
using UnityEngine;

// Token: 0x020002F6 RID: 758
public class PsUITopWinRacing : UICanvas
{
	// Token: 0x0600164C RID: 5708 RVA: 0x000E93C8 File Offset: 0x000E77C8
	public PsUITopWinRacing(UIComponent _parent)
		: base(_parent, false, "TopContent", null, string.Empty)
	{
		this.SetHeight(0.1f, RelativeTo.ScreenHeight);
		this.SetWidth(1f, RelativeTo.ScreenWidth);
		this.SetAlign(0.5f, 1f);
		this.SetDrawHandler(new UIDrawDelegate(this.TopDrawhandler));
		this.m_leftArea = new UIHorizontalList(this, "UpperLeft");
		this.m_leftArea.SetMargins(0.025f, RelativeTo.ScreenShortest);
		this.m_leftArea.SetSpacing(0.025f, RelativeTo.ScreenShortest);
		this.m_leftArea.SetAlign(0f, 1f);
		this.m_leftArea.RemoveDrawHandler();
		if (PsState.m_activeGameLoop is PsGameLoopRacing && PsState.m_activeGameLoop.GetPosition() != 1)
		{
			this.m_exitButton = new PsUIGenericButton(this.m_leftArea, 0.25f, 0.25f, 0.005f, "Button");
			this.m_exitButton.SetSound("/UI/ExitLevel");
			this.m_exitButton.SetIcon("hud_icon_menu_exit", 0.06f, RelativeTo.ScreenShortest, "#FFFFFF", default(cpBB));
			this.m_exitButton.SetOrangeColors(true);
			this.m_exitButton.SetDepthOffset(-5f);
		}
		if (PsState.m_levelLeaderboardsEnabled)
		{
			this.m_leaderboardButton = new PsUIGenericButton(this.m_leftArea, 0.25f, 0.25f, 0.005f, "Button");
			this.m_leaderboardButton.SetIcon("menu_icon_leaderboards", 0.06f, RelativeTo.ScreenShortest, "#FFFFFF", default(cpBB));
			this.m_leaderboardButton.SetDepthOffset(-10f);
		}
	}

	// Token: 0x0600164D RID: 5709 RVA: 0x000E956C File Offset: 0x000E796C
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

	// Token: 0x0600164E RID: 5710 RVA: 0x000E9654 File Offset: 0x000E7A54
	public string GetPosText()
	{
		int position = PsState.m_activeGameLoop.GetPosition();
		if (position == 1)
		{
			return "1st";
		}
		if (position == 2)
		{
			return "2nd";
		}
		if (position == 3)
		{
			return "3rd";
		}
		if (position == 4)
		{
			return "4th";
		}
		return "last";
	}

	// Token: 0x0600164F RID: 5711 RVA: 0x000E96A8 File Offset: 0x000E7AA8
	public void TopDrawhandler(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] rect = DebugDraw.GetRect(_c.m_actualWidth, 0.06f * (float)Screen.height, new Vector2(0f, 0.02f * (float)Screen.height), true);
		Color black = Color.black;
		Color black2 = Color.black;
		Color black3 = Color.black;
		black3.a = 0.5f;
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * -1f, rect, (float)Screen.height * 0.0075f, black, black2, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line4Mat_Material), _c.m_camera, Position.Center, true);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * 1f, rect, (float)Screen.height * 0.015f, black3, black3, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line4Mat_Material), _c.m_camera, Position.Center, true);
		GGData ggdata = new GGData(rect);
		PrefabS.CreateFlatPrefabComponentsFromPolygon(_c.m_TC, Vector3.zero, ggdata, black, black2, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera);
	}

	// Token: 0x0400190D RID: 6413
	private PsUIGenericButton m_exitButton;

	// Token: 0x0400190E RID: 6414
	private PsUIGenericButton m_leaderboardButton;

	// Token: 0x0400190F RID: 6415
	protected UIHorizontalList m_leftArea;
}
