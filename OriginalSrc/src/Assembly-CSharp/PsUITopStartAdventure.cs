using System;
using UnityEngine;

// Token: 0x020002DD RID: 733
public class PsUITopStartAdventure : UICanvas
{
	// Token: 0x060015B1 RID: 5553 RVA: 0x000E1548 File Offset: 0x000DF948
	public PsUITopStartAdventure(UIComponent _parent)
		: base(_parent, false, "TopContent", null, string.Empty)
	{
		this.SetHeight(0.1f, RelativeTo.ScreenHeight);
		this.SetWidth(1f, RelativeTo.ScreenWidth);
		this.SetAlign(0.5f, 1f);
		this.SetDrawHandler(new UIDrawDelegate(this.TopDrawhandler));
		UIHorizontalList uihorizontalList = new UIHorizontalList(this, "UpperLeft");
		uihorizontalList.SetMargins(0.025f, RelativeTo.ScreenShortest);
		uihorizontalList.SetSpacing(0.025f, RelativeTo.ScreenShortest);
		uihorizontalList.SetAlign(0f, 1f);
		uihorizontalList.RemoveDrawHandler();
		this.m_exitButton = new PsUIGenericButton(uihorizontalList, 0.25f, 0.25f, 0.005f, "Button");
		this.m_exitButton.SetIcon("hud_icon_menu_exit", 0.06f, RelativeTo.ScreenShortest, "#FFFFFF", default(cpBB));
		this.m_exitButton.SetSound("/UI/ExitLevel");
		this.m_exitButton.SetOrangeColors(true);
		this.m_exitButton.SetDepthOffset(-5f);
		if (PsState.m_activeGameLoop.m_path != null && PsState.m_activeGameLoop.m_path.GetPathType() == PsPlanetPathType.main && PsState.m_activeGameLoop.m_nodeId == PsState.m_activeGameLoop.m_path.m_currentNodeId && PsState.m_activeGameLoop.m_scoreBest == 0 && PsState.m_activeMinigame.m_gameStartCount > 0 && PsState.m_activeGameLoop.m_timeScoreBest == 2147483647)
		{
			this.m_skipButton = new PsUIGenericButton(uihorizontalList, 0.25f, 0.25f, 0.005f, "Button");
			this.m_skipButton.SetBlueColors(true);
			this.m_skipButton.SetText(PsStrings.Get(StringID.SKIP_LEVEL), 0.04f, 0f, RelativeTo.ScreenHeight, true, RelativeTo.ScreenShortest);
			this.m_skipButton.SetSkipPrice(PsMetagameManager.m_skipPrice, 0.035f);
		}
	}

	// Token: 0x060015B2 RID: 5554 RVA: 0x000E1728 File Offset: 0x000DFB28
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

	// Token: 0x060015B3 RID: 5555 RVA: 0x000E1838 File Offset: 0x000DFC38
	public void Skip()
	{
		(this.GetRoot() as PsUIBasePopup).CallAction("Skip");
	}

	// Token: 0x060015B4 RID: 5556 RVA: 0x000E1850 File Offset: 0x000DFC50
	public void CancelSkip()
	{
	}

	// Token: 0x060015B5 RID: 5557 RVA: 0x000E1854 File Offset: 0x000DFC54
	public override void Step()
	{
		if (this.m_exitButton != null && this.m_exitButton.m_TC.p_entity != null && this.m_exitButton.m_TC.p_entity.m_active && (this.m_exitButton.m_hit || Main.AndroidBackButtonPressed((this.GetRoot() as PsUIBasePopup).m_guid)))
		{
			(this.GetRoot() as PsUIBasePopup).CallAction("Exit");
		}
		else if (this.m_skipButton != null && this.m_skipButton.m_hit)
		{
			new PsSkipLevelFlow(new Action(this.Skip), new Action(this.CancelSkip), PsMetagameManager.m_skipPrice);
		}
		base.Step();
	}

	// Token: 0x04001864 RID: 6244
	private PsUIGenericButton m_exitButton;

	// Token: 0x04001865 RID: 6245
	private PsUIGenericButton m_skipButton;

	// Token: 0x04001866 RID: 6246
	private UIVerticalList m_ghost;
}
