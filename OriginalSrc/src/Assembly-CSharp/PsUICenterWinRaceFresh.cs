using System;
using UnityEngine;

// Token: 0x020002F0 RID: 752
public class PsUICenterWinRaceFresh : PsUICenterWinRace
{
	// Token: 0x06001632 RID: 5682 RVA: 0x000E84E4 File Offset: 0x000E68E4
	public PsUICenterWinRaceFresh(UIComponent _parent)
		: base(_parent)
	{
	}

	// Token: 0x06001633 RID: 5683 RVA: 0x000E84ED File Offset: 0x000E68ED
	public override void CreateLikeButtons(UIComponent _parent)
	{
	}

	// Token: 0x06001634 RID: 5684 RVA: 0x000E84F0 File Offset: 0x000E68F0
	public override void Preset()
	{
		PsMetagameManager.ShowResources(this.m_camera, false, false, true, false, 0.15f, false, false, false);
		this.m_topFrameName = "hud_big_diamond_top";
		this.m_bottomFrameName = "hud_big_diamond_bottom";
		this.m_spacing = -0.07f;
		this.m_centerVAlign = 1f;
	}

	// Token: 0x06001635 RID: 5685 RVA: 0x000E8540 File Offset: 0x000E6940
	public override void CreateReward(UIComponent _parent, int _score)
	{
		UIVerticalList uiverticalList = new UIVerticalList(_parent, "ASDASD");
		uiverticalList.SetVerticalAlign(0.485f);
		uiverticalList.RemoveDrawHandler();
		if (PsState.m_activeGameLoop.m_rewardOld < _score)
		{
			UIHorizontalList uihorizontalList = new UIHorizontalList(uiverticalList, "WEWE");
			uihorizontalList.SetHeight(0.175f, RelativeTo.ParentHeight);
			uihorizontalList.RemoveDrawHandler();
			if (PsState.m_activeGameLoop.m_currentRunScore < _score)
			{
				UIText uitext = new UIText(uihorizontalList, false, string.Empty, "+1", PsFontManager.GetFont(PsFonts.HurmeSemiBold), 0.8f, RelativeTo.ParentHeight, "#ffe025", null);
				UIFittedSprite uifittedSprite = new UIFittedSprite(uihorizontalList, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_resources_diamond_icon", null), true, true);
				uifittedSprite.SetHeight(1f, RelativeTo.ParentHeight);
			}
			if (PsState.m_activeGameLoop.m_currentRunScore >= _score)
			{
				this.m_flyers.Add(uihorizontalList);
			}
		}
		if (PsState.m_activeGameLoop.m_currentRunScore < _score)
		{
			UIVerticalList uiverticalList2 = new UIVerticalList(uiverticalList, string.Empty);
			uiverticalList2.RemoveDrawHandler();
			PsGameModeRace psGameModeRace = PsState.m_activeGameLoop.m_gameMode as PsGameModeRace;
			float num = ((_score != 1) ? ((_score != 2) ? psGameModeRace.m_threeMedalTime : psGameModeRace.m_twoMedalTime) : psGameModeRace.m_oneMedalTime);
			UIText uitext2 = new UIText(uiverticalList2, false, string.Empty, (HighScores.TimeScoreToTime(PsState.m_activeGameLoop.m_timeScoreCurrent) - num).ToString("F3"), PsFontManager.GetFont(PsFonts.KGSecondChances), 0.07875f, RelativeTo.ParentHeight, "#52a7ad", null);
			UIText uitext3 = new UIText(uiverticalList2, false, string.Empty, "faster", PsFontManager.GetFont(PsFonts.KGSecondChances), 0.07875f, RelativeTo.ParentHeight, "#52a7ad", null);
		}
	}

	// Token: 0x06001636 RID: 5686 RVA: 0x000E86E8 File Offset: 0x000E6AE8
	public override void CreateFriendRaceButton()
	{
	}

	// Token: 0x06001637 RID: 5687 RVA: 0x000E86EC File Offset: 0x000E6AEC
	public override void FlyerTimerEvent()
	{
		PsMetagameManager.m_menuResourceView.CreateFlyingResources(1, this.m_camera.WorldToScreenPoint(this.m_flyers[0].m_TC.transform.position) - new Vector3((float)Screen.width, (float)Screen.height, 0f) * 0.5f, ResourceType.Diamonds, 0f, null, null, null, null, default(Vector2));
		this.m_flyers.RemoveAt(0);
	}

	// Token: 0x04001906 RID: 6406
	private int m_realReward;
}
