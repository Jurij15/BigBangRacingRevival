using System;
using UnityEngine;

// Token: 0x020002B9 RID: 697
public class PsUITopPause : UICanvas
{
	// Token: 0x060014BE RID: 5310 RVA: 0x000D8950 File Offset: 0x000D6D50
	public PsUITopPause(UIComponent _parent)
		: base(_parent, false, "TopContent", null, string.Empty)
	{
		this.RemoveDrawHandler();
		this.CreateLeftArea();
		UIHorizontalList uihorizontalList = new UIHorizontalList(this, "UpperRight");
		uihorizontalList.SetMargins(0.025f, RelativeTo.ScreenShortest);
		uihorizontalList.SetSpacing(0.025f, RelativeTo.ScreenShortest);
		uihorizontalList.SetAlign(1f, 1f);
		uihorizontalList.RemoveDrawHandler();
		this.m_restartButton = new PsUIGenericButton(uihorizontalList, 0.25f, 0.25f, 0.005f, "Button");
		this.m_restartButton.SetIcon("hud_icon_restart", 0.06f, RelativeTo.ScreenShortest, "#FFFFFF", default(cpBB));
		this.m_restartButton.SetOrangeColors(true);
	}

	// Token: 0x060014BF RID: 5311 RVA: 0x000D8A08 File Offset: 0x000D6E08
	public virtual void CreateLeftArea()
	{
		this.m_leftArea = new UIHorizontalList(this, "UpperLeft");
		this.m_leftArea.SetMargins(0.025f, RelativeTo.ScreenShortest);
		this.m_leftArea.SetSpacing(0.025f, RelativeTo.ScreenShortest);
		this.m_leftArea.SetAlign(0f, 1f);
		this.m_leftArea.RemoveDrawHandler();
		this.m_exitButton = new PsUIGenericButton(this.m_leftArea, 0.25f, 0.25f, 0.005f, "Button");
		this.m_exitButton.SetIcon("hud_icon_menu_exit", 0.06f, RelativeTo.ScreenShortest, "#FFFFFF", default(cpBB));
		this.m_exitButton.SetSound("/UI/ExitLevel");
		this.m_exitButton.SetOrangeColors(true);
		if (PsState.m_levelLeaderboardsEnabled && (PsState.m_activeGameLoop.m_gameMode is PsGameModeRacing || PsState.m_activeGameLoop.m_gameMode is PsGameModeRace))
		{
			this.m_leaderboardButton = new PsUIGenericButton(this.m_leftArea, 0.25f, 0.25f, 0.005f, "Button");
			this.m_leaderboardButton.SetIcon("menu_icon_leaderboards", 0.06f, RelativeTo.ScreenShortest, "#FFFFFF", default(cpBB));
			this.m_leaderboardButton.SetDepthOffset(-10f);
		}
	}

	// Token: 0x060014C0 RID: 5312 RVA: 0x000D8B58 File Offset: 0x000D6F58
	protected void SkipLevel()
	{
		(this.GetRoot() as PsUIBasePopup).CallAction("Skip");
	}

	// Token: 0x060014C1 RID: 5313 RVA: 0x000D8B70 File Offset: 0x000D6F70
	public virtual void CreateMiddleArea()
	{
	}

	// Token: 0x060014C2 RID: 5314 RVA: 0x000D8B74 File Offset: 0x000D6F74
	public void DescriptionDrawHandler(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] array = new Vector2[]
		{
			new Vector2(_c.m_actualWidth * -0.5f, _c.m_actualHeight * 0.5f),
			new Vector2(_c.m_actualWidth * -0.5f, _c.m_actualHeight * -0.5f),
			new Vector2(_c.m_actualWidth * 0.5f, _c.m_actualHeight * -0.5f),
			new Vector2(_c.m_actualWidth * 0.5f, _c.m_actualHeight * 0.5f),
			new Vector2(_c.m_actualWidth * -0.4f, _c.m_actualHeight * 0.5f),
			new Vector2(_c.m_actualWidth * -0.45f, _c.m_actualHeight * 0.5f + 0.025f * (float)Screen.height),
			new Vector2(_c.m_actualWidth * -0.45f, _c.m_actualHeight * 0.5f)
		};
		PrefabS.CreateFlatPrefabComponentsFromVectorArray(_c.m_TC, Vector3.zero, array, DebugDraw.HexToUint("fffec6"), DebugDraw.HexToUint("fffec6"), ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera, string.Empty, null);
		Vector2[] array2 = new Vector2[]
		{
			new Vector2(_c.m_actualWidth * -0.5f, _c.m_actualHeight * 0.5f),
			new Vector2(_c.m_actualWidth * -0.5f, _c.m_actualHeight * -0.5f),
			new Vector2(_c.m_actualWidth * 0.5f, _c.m_actualHeight * -0.5f),
			new Vector2(_c.m_actualWidth * 0.5f, _c.m_actualHeight * 0.5f)
		};
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward + Vector3.down + Vector3.right * 0.5f, array2, 0.004f * (float)Screen.height, DebugDraw.GetColor(0f, 0f, 0f, 110f), ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera, Position.Center, true);
	}

	// Token: 0x060014C3 RID: 5315 RVA: 0x000D8E14 File Offset: 0x000D7214
	public override void Step()
	{
		if (this.m_exitButton.m_TC.p_entity.m_active && this.m_exitButton.m_hit)
		{
			(this.GetRoot() as PsUIBasePopup).CallAction("Exit");
			TouchAreaS.CancelAllTouches(null);
		}
		else if (this.m_restartButton.m_hit)
		{
			(this.GetRoot() as PsUIBasePopup).CallAction("Restart");
			TouchAreaS.CancelAllTouches(null);
		}
		else if (this.m_leaderboardButton != null && this.m_leaderboardButton.m_hit)
		{
			PsUIBasePopup popup = new PsUIBasePopup(typeof(PsUICenterLeaderboardLevel), null, null, null, true, true, InitialPage.Center, false, false, false);
			popup.SetAction("Exit", delegate
			{
				popup.Destroy();
			});
			TouchAreaS.CancelAllTouches(null);
		}
		base.Step();
	}

	// Token: 0x0400178A RID: 6026
	private PsUIGenericButton m_exitButton;

	// Token: 0x0400178B RID: 6027
	private PsUIGenericButton m_leaderboardButton;

	// Token: 0x0400178C RID: 6028
	private PsUIGenericButton m_restartButton;

	// Token: 0x0400178D RID: 6029
	protected UIHorizontalList m_leftArea;
}
