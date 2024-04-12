using System;
using UnityEngine;

// Token: 0x0200037D RID: 893
public class PsUITeamChatAnnouncement : PsUITeamChatComment
{
	// Token: 0x060019E1 RID: 6625 RVA: 0x0011D8B0 File Offset: 0x0011BCB0
	public PsUITeamChatAnnouncement(UIComponent _parent, CommentData _data)
		: base(_parent, _data)
	{
		this.SetMargins(0f, 0f, 0.0125f, 0.0125f, RelativeTo.ScreenHeight);
	}

	// Token: 0x060019E2 RID: 6626 RVA: 0x0011D8D5 File Offset: 0x0011BCD5
	public override void CreateProfile(bool _left = true)
	{
	}

	// Token: 0x060019E3 RID: 6627 RVA: 0x0011D8D8 File Offset: 0x0011BCD8
	public override void CreateComment(bool _right = true)
	{
		string text = string.Empty;
		if (this.m_comment.type == "JOIN")
		{
			text = PsStrings.Get(StringID.CHAT_NOTIFICATION_JOINED);
			text = text.Replace("%1", this.m_comment.name);
			this.m_bubbleColor = DebugDraw.HexToColor("#73a54a");
		}
		else if (this.m_comment.type == "LEAVE")
		{
			text = PsStrings.Get(StringID.CHAT_NOTIFICATION_LEFT);
			text = text.Replace("%1", this.m_comment.name);
			this.m_bubbleColor = DebugDraw.HexToColor("#b0634e");
		}
		else if (this.m_comment.type == "KICK")
		{
			text = PsStrings.Get(StringID.CHAT_KICK_NOTIFICATION);
			text = text.Replace("%1", this.m_comment.customData.kickedName);
			text = text.Replace("%2", this.m_comment.name);
			text = text.Replace("%kickReason%", this.m_comment.customData.text);
			this.m_bubbleColor = DebugDraw.HexToColor("#b0634e");
		}
		else if (this.m_comment.type == "SEASON_CHANGE")
		{
			text = PsStrings.Get(StringID.CHAT_NOTIFICATION_SEASON_END);
			if (this.m_comment.customData.rewardType == "coin")
			{
				text = PsStrings.Get(StringID.CHAT_NOTIFICATION_SEASON_END_COINS);
			}
			text = text.Replace("%1", this.m_comment.customData.score.ToString());
			text = text.Replace("%2", this.m_comment.customData.reward.ToString());
			this.m_bubbleColor = DebugDraw.HexToColor("#ff8400");
		}
		UITextbox uitextbox = new UITextbox(this, false, string.Empty, text, PsFontManager.GetFont(PsFonts.KGSecondChances), 0.0275f, RelativeTo.ScreenHeight, false, Align.Left, Align.Top, "#ffffff", true, null);
		uitextbox.SetMargins(0.01f, RelativeTo.ScreenHeight);
		uitextbox.SetMinRows(2);
		uitextbox.SetMaxRows(4);
		uitextbox.SetWidth(1f, RelativeTo.ParentWidth);
		uitextbox.SetHorizontalAlign(0f);
		uitextbox.SetDrawHandler(new UIDrawDelegate(base.AnnouncementDrawhandler));
		uitextbox.m_tmc.m_renderer.material.shader = Shader.Find("Framework/ColorFontShader");
	}
}
