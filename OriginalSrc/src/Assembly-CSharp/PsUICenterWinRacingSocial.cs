using System;
using UnityEngine;

// Token: 0x020002F3 RID: 755
public class PsUICenterWinRacingSocial : PsUICenterWinRacing
{
	// Token: 0x06001643 RID: 5699 RVA: 0x000E8D45 File Offset: 0x000E7145
	public PsUICenterWinRacingSocial(UIComponent _parent)
		: base(_parent)
	{
	}

	// Token: 0x06001644 RID: 5700 RVA: 0x000E8D50 File Offset: 0x000E7150
	public override void CreateGoButton(UIComponent _parent)
	{
		this.m_nextRace = new PsUIGenericButton(_parent, 0.25f, 0.25f, 0.01f, "Button");
		this.m_nextRace.SetMargins(0.02f, 0.02f, 0.01f, 0.01f, RelativeTo.ScreenHeight);
		this.m_nextRace.SetGreenColors(true);
		this.m_nextRace.SetFittedText(PsStrings.Get(StringID.CONTINUE), 0.04f, 0.3f, RelativeTo.ScreenHeight, true);
		this.m_nextRace.SetHeight(0.12f, RelativeTo.ScreenHeight);
		this.m_nextRace.SetHorizontalAlign(1f);
		if (PsState.m_activeGameLoop.GetPosition() != 1)
		{
			this.m_go = new PsUIAttentionButton(_parent, Vector3.one * 1.15f, 0.25f, 0.25f, 0.01f);
		}
		else
		{
			this.m_go = new PsUIGenericButton(_parent, 0.25f, 0.25f, 0.01f, "Button");
		}
		this.m_go.SetRaceButton(PsState.m_activeMinigame.m_gameStartCount + 1, 0, false);
		this.m_go.SetHeight(0.12f, RelativeTo.ScreenHeight);
		this.m_go.SetHorizontalAlign(1f);
	}

	// Token: 0x06001645 RID: 5701 RVA: 0x000E8E84 File Offset: 0x000E7284
	public override void CreateOpponents()
	{
		this.m_opponentArea = new UICanvas(this, false, string.Empty, null, string.Empty);
		this.m_opponentArea.SetHeight(1f, RelativeTo.ScreenHeight);
		this.m_opponentArea.SetWidth(0.67f, RelativeTo.ScreenHeight);
		this.m_opponentArea.SetAlign(1f, 1f);
		this.m_opponentArea.SetDepthOffset(5f);
		this.m_opponentArea.SetDrawHandler(new UIDrawDelegate(base.OpponentAreaDrawhandler));
		this.m_opponents = new PsUIWinOpponentsSocial(this.m_opponentArea);
		this.m_opponents.SetMargins(0f, 0f, 0.175f, 0f, RelativeTo.ScreenHeight);
		this.m_opponents.SetAlign(1f, 1f);
	}
}
