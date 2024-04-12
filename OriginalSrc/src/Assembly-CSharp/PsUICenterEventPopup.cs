using System;

// Token: 0x02000384 RID: 900
public class PsUICenterEventPopup : UICanvas
{
	// Token: 0x06001A02 RID: 6658 RVA: 0x0011F9F0 File Offset: 0x0011DDF0
	public PsUICenterEventPopup(UIComponent _parent)
		: base(_parent, false, string.Empty, null, string.Empty)
	{
		this.RemoveDrawHandler();
		this.m_chatParent = new UIComponent(this, false, "chatParent", null, null, string.Empty);
		this.m_chatParent.SetWidth(0.4f, RelativeTo.ScreenWidth);
		this.m_chatParent.SetHeight(1f, RelativeTo.ParentHeight);
		this.m_chatParent.SetAlign(0f, 0f);
		this.m_chatParent.RemoveDrawHandler();
		this.m_chat = new PsUIChatGlobal(this.m_chatParent);
		this.m_chat.SetWidth(1f, RelativeTo.ParentWidth);
		this.m_chat.SetHeight(1f, RelativeTo.ParentHeight);
		this.m_chat.SetMargins(0f, 0f, 0f, 0f, RelativeTo.ScreenWidth);
		this.m_chat.SetAlign(0f, 0f);
		this.m_chat.m_commentArea.SetVerticalAlign(0.25f);
		this.m_rightCanvas = new UICanvas(this, true, string.Empty, null, string.Empty);
		this.m_rightCanvas.SetWidth(0.6f, RelativeTo.ScreenWidth);
		this.m_rightCanvas.SetMargins(0f, 0f, 0.1f, 0f, RelativeTo.OwnHeight);
		this.m_rightCanvas.SetHorizontalAlign(1f);
		this.m_rightCanvas.RemoveDrawHandler();
		if (PsMetagameManager.m_doubleValueGoodOrBadEvent != null)
		{
			if (PsMetagameManager.m_activeTournament != null && (PsMetagameManager.m_activeTournament.timeLeft > 0 || (PsMetagameManager.m_activeTournament.tournament != null && PsMetagameManager.m_activeTournament.tournament.joined && !PsMetagameManager.m_activeTournament.tournament.claimed)))
			{
				this.m_event = PsMetagameManager.m_activeTournament;
				PsUIBaseEventPopup psUIBaseEventPopup = new PsUITournamentEventPopup(this.m_rightCanvas, string.Empty, this.m_event);
			}
			else
			{
				this.m_event = PsMetagameManager.m_doubleValueGoodOrBadEvent;
				PsUIBaseEventPopup psUIBaseEventPopup = new PsUIGemEventPopup(this.m_rightCanvas, string.Empty, this.m_event);
			}
		}
		else
		{
			this.m_event = PsMetagameManager.m_activeTournament;
			PsUIBaseEventPopup psUIBaseEventPopup = new PsUITournamentEventPopup(this.m_rightCanvas, string.Empty, this.m_event);
		}
	}

	// Token: 0x06001A03 RID: 6659 RVA: 0x0011FC1E File Offset: 0x0011E01E
	public override void Step()
	{
		if (this.m_chat != null)
		{
			this.m_chat.UpdateLogic();
		}
		base.Step();
	}

	// Token: 0x04001C5D RID: 7261
	private UIComponent m_chatParent;

	// Token: 0x04001C5E RID: 7262
	private PsUIChatGlobal m_chat;

	// Token: 0x04001C5F RID: 7263
	private UICanvas m_rightCanvas;

	// Token: 0x04001C60 RID: 7264
	private EventMessage m_event;
}
