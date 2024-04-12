using System;
using UnityEngine;

// Token: 0x020002DA RID: 730
public class PsUIRaceProfileFresh : PsUIRaceProfile
{
	// Token: 0x060015A8 RID: 5544 RVA: 0x000E11DC File Offset: 0x000DF5DC
	public PsUIRaceProfileFresh(UIComponent _parent, RacerProfile _profile, bool _winScreen, bool _margin, int _position)
		: base(_parent, _profile, _winScreen, _margin, _position)
	{
	}

	// Token: 0x060015A9 RID: 5545 RVA: 0x000E11EB File Offset: 0x000DF5EB
	protected override void CreateTrophies(UIComponent _parent)
	{
	}

	// Token: 0x060015AA RID: 5546 RVA: 0x000E11F0 File Offset: 0x000DF5F0
	public override void CreatePrize(UIComponent _parent)
	{
		string text = "menu_resources_shard_icon";
		if (!string.IsNullOrEmpty(text) && ((this.m_profile.won && !this.m_profile.wonAtCreate) || !this.m_profile.won) && this.m_profile.playerId != PlayerPrefsX.GetUserId())
		{
			UICanvas uicanvas = new UICanvas(_parent, false, string.Empty, null, string.Empty);
			uicanvas.SetSize(0.085f, 0.085f, RelativeTo.ScreenHeight);
			uicanvas.RemoveDrawHandler();
			this.m_winIcon = new UIHorizontalList(uicanvas, "diamond list");
			this.m_winIcon.RemoveDrawHandler();
			this.m_winIcon.SetHeight(0.065f, RelativeTo.ScreenHeight);
			(this.m_winIcon as UIHorizontalList).SetSpacing(-0.02f, RelativeTo.ScreenHeight);
			for (int i = 0; i < this.m_profile.amount; i++)
			{
				UIFittedSprite uifittedSprite = new UIFittedSprite(this.m_winIcon, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame(text, null), true, true);
				this.m_winIcon.SetHeight(0.065f, RelativeTo.ScreenHeight);
				uifittedSprite.SetDepthOffset(-5f + (float)i);
			}
		}
		else
		{
			UICanvas uicanvas2 = new UICanvas(_parent, false, string.Empty, null, string.Empty);
			uicanvas2.SetSize(0.085f, 0.085f, RelativeTo.ScreenHeight);
			uicanvas2.RemoveDrawHandler();
		}
	}

	// Token: 0x060015AB RID: 5547 RVA: 0x000E135C File Offset: 0x000DF75C
	public void RewardAlphaTween()
	{
		for (int i = 0; i < this.m_winIcon.m_childs.Count; i++)
		{
			TweenC tweenC = TweenS.AddTransformTween(this.m_winIcon.m_childs[i].m_TC, TweenedProperty.Alpha, TweenStyle.ExpoIn, Vector3.zero, 0.35f, 0f, true);
			TweenS.SetTweenAlphaProperties(tweenC, false, true, false, Shader.Find("WOE/Unlit/ColorUnlitTransparent"));
		}
	}
}
