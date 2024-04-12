using System;

// Token: 0x02000380 RID: 896
public class PsUITeamChatLevel : PsUITeamChatComment
{
	// Token: 0x060019F6 RID: 6646 RVA: 0x0011E548 File Offset: 0x0011C948
	public PsUITeamChatLevel(UIComponent _parent, CommentData _data)
		: base(_parent, _data)
	{
		this.SetMargins(0f, 0f, 0.0125f, 0.0125f, RelativeTo.ScreenHeight);
		this.m_loop = new PsGameLoopSocial(_data.customData.gameId, null, null, -1, -1, 0, false);
		this.m_loop.m_returnToChat = true;
	}

	// Token: 0x060019F7 RID: 6647 RVA: 0x0011E5A1 File Offset: 0x0011C9A1
	public override void CreateProfile(bool _left = true)
	{
	}

	// Token: 0x060019F8 RID: 6648 RVA: 0x0011E5A4 File Offset: 0x0011C9A4
	public override void CreateComment(bool _right = true)
	{
		string text = string.Empty;
		if (this.m_comment.type == "MINIGAME_PUBLISH")
		{
			text = PsStrings.Get(StringID.CHAT_CREATED_NOTIFICATION);
			text = text.Replace("%1", this.m_comment.name);
			if (this.m_comment.customData.gameMode == "Race")
			{
				text = text.Replace("%2", PsStrings.Get(StringID.CHAT_CREATED_RACING));
			}
			else
			{
				text = text.Replace("%2", PsStrings.Get(StringID.CHAT_CREATED_ADVENTURE));
			}
			this.m_bubbleColor = DebugDraw.HexToColor("#5b8bb2");
		}
		else if (this.m_comment.type == "MEGA_LIKE")
		{
			text = PsStrings.Get(StringID.CHAT_MEGA_LIKED);
			text = text.Replace("%1", this.m_comment.name);
			text = text.Replace("%2", this.m_comment.customData.text);
			this.m_bubbleColor = DebugDraw.HexToColor("#5b8bb2");
		}
		UITextbox uitextbox = new UITextbox(this, false, string.Empty, text, PsFontManager.GetFont(PsFonts.KGSecondChances), 0.0275f, RelativeTo.ScreenHeight, false, Align.Left, Align.Top, "#ffffff", true, null);
		uitextbox.SetMargins(0.01f, RelativeTo.ScreenHeight);
		uitextbox.SetMinRows(2);
		uitextbox.SetMaxRows(4);
		uitextbox.SetWidth(0.7f, RelativeTo.ParentWidth);
		uitextbox.SetHorizontalAlign(0f);
		uitextbox.SetDrawHandler(new UIDrawDelegate(base.AnnouncementDrawhandler));
		this.m_play = new PsUIGenericButton(this, 0.25f, 0.25f, 0.005f, "Button");
		this.m_play.SetGreenColors(true);
		this.m_play.SetMargins(0f, 0f, 0.01f, 0.01f, RelativeTo.ScreenHeight);
		this.m_play.SetFittedText(PsStrings.Get(StringID.PLAY), 0.05f, 0.25f, RelativeTo.ParentWidth, true);
		this.m_play.SetHeight(0.08f, RelativeTo.ScreenHeight);
	}

	// Token: 0x060019F9 RID: 6649 RVA: 0x0011E7A4 File Offset: 0x0011CBA4
	public override void Step()
	{
		if (this.m_play.m_hit)
		{
			TouchAreaS.Disable();
			TouchAreaS.CancelAllTouches(null);
			this.m_loop.StartLoop();
		}
		base.Step();
	}

	// Token: 0x04001C4D RID: 7245
	private PsUIGenericButton m_play;

	// Token: 0x04001C4E RID: 7246
	private PsGameLoopSocial m_loop;
}
