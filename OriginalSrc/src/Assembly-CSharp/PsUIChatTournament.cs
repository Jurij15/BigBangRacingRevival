using System;
using System.Collections.Generic;
using Server;

// Token: 0x0200028D RID: 653
public class PsUIChatTournament : PsUICenterChat
{
	// Token: 0x060013A1 RID: 5025 RVA: 0x000C4034 File Offset: 0x000C2434
	public PsUIChatTournament(UIComponent _parent)
		: base(_parent)
	{
		this.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.TournamentChatBGDrawHandler));
		if (this.m_commentField != null)
		{
			string text = PsStrings.Get(StringID.TOURNAMENT_CHAT_BUTTON_ROOM);
			text = text.Replace("%1", PsMetagameManager.m_activeTournament.tournament.room.ToString());
			this.m_commentField.SetHintText(text, false);
		}
	}

	// Token: 0x060013A2 RID: 5026 RVA: 0x000C40B4 File Offset: 0x000C24B4
	public override void SendComment()
	{
		Debug.LogError("SendComment");
		CommentData commentData = default(CommentData);
		commentData.playerId = PlayerPrefsX.GetUserId();
		commentData.comment = this.m_input;
		commentData.name = PlayerPrefsX.GetUserName();
		commentData.facebookId = PlayerPrefsX.GetFacebookId();
		commentData.gameCenterId = PlayerPrefsX.GetGameCenterId();
		string input = this.m_input;
		this.SendComment(input);
		this.m_input = string.Empty;
		this.m_commentField.SetText(this.m_input);
		PsUITeamChatComment psUITeamChatComment = new PsUITournamentChatComment(this.m_commentList, commentData);
		psUITeamChatComment.Update();
		this.m_commentList.SetHeight(1f, RelativeTo.ParentHeight);
		this.m_commentList.CalculateReferenceSizes();
		this.m_commentList.UpdateSize();
		this.m_commentList.ArrangeContents();
		this.m_commentList.UpdateDimensions();
		this.m_commentList.UpdateSize();
		this.m_commentList.UpdateAlign();
		this.m_commentList.UpdateChildrenAlign();
		this.m_commentList.ArrangeContents();
		this.m_commentList.m_parent.ArrangeContents();
		base.ScrollContents(true);
	}

	// Token: 0x060013A3 RID: 5027 RVA: 0x000C41D0 File Offset: 0x000C25D0
	protected override void SendComment(string _comment)
	{
		new PsServerQueueFlow(null, delegate
		{
			HttpC httpC = Tournament.SaveComment(_comment, new Action<CommentData[]>(this.GetCommentsOK), new Action<HttpC>(this.SendCommentFailed), null);
			httpC.objectData = _comment;
			EntityManager.AddComponentToEntity(this.m_TC.p_entity, httpC);
		}, new string[] { "Comment" });
	}

	// Token: 0x060013A4 RID: 5028 RVA: 0x000C4214 File Offset: 0x000C2614
	public void SendCommentFailed(HttpC _c)
	{
		Debug.Log("COMMENT FAILED", null);
		ServerManager.ThrowServerErrorException(PsStrings.Get(StringID.CONNECTION_ERROR_HEADER), _c.www, delegate
		{
			HttpC httpC = Tournament.SaveComment((string)_c.objectData, new Action<CommentData[]>(this.GetCommentsOK), new Action<HttpC>(this.SendCommentFailed), null);
			httpC.objectData = _c.objectData;
			EntityManager.AddComponentToEntity(this.m_TC.p_entity, httpC);
			return httpC;
		}, null);
	}

	// Token: 0x060013A5 RID: 5029 RVA: 0x000C4268 File Offset: 0x000C2668
	protected override PsUITeamChatComment GetCommentType(int i)
	{
		PsUITeamChatComment psUITeamChatComment;
		if (string.IsNullOrEmpty(this.m_comments[i].type))
		{
			psUITeamChatComment = new PsUITournamentChatComment(this.m_commentList, this.m_comments[i]);
		}
		else if (this.m_comments[i].type == "JOIN")
		{
			psUITeamChatComment = new PsUITeamChatAnnouncement(this.m_commentList, this.m_comments[i]);
		}
		else if (this.m_comments[i].type == "LEAVE")
		{
			psUITeamChatComment = new PsUITeamChatAnnouncement(this.m_commentList, this.m_comments[i]);
		}
		else if (this.m_comments[i].type == "KICK")
		{
			psUITeamChatComment = new PsUITeamChatAnnouncement(this.m_commentList, this.m_comments[i]);
		}
		else if (this.m_comments[i].type == "MINIGAME_PUBLISH")
		{
			psUITeamChatComment = new PsUITeamChatLevel(this.m_commentList, this.m_comments[i]);
		}
		else if (this.m_comments[i].type == "MEGA_LIKE")
		{
			psUITeamChatComment = new PsUITeamChatLevel(this.m_commentList, this.m_comments[i]);
		}
		else if (this.m_comments[i].type == "SEASON_CHANGE")
		{
			psUITeamChatComment = new PsUITeamChatAnnouncement(this.m_commentList, this.m_comments[i]);
		}
		else
		{
			psUITeamChatComment = null;
		}
		return psUITeamChatComment;
	}

	// Token: 0x060013A6 RID: 5030 RVA: 0x000C4444 File Offset: 0x000C2844
	public void RemoveOldData()
	{
		this.m_commentList.DestroyChildren();
		if (this.m_fetchingComments)
		{
			List<IComponent> componentsByEntity = EntityManager.GetComponentsByEntity(ComponentType.Http, this.m_TC.p_entity);
			foreach (IComponent component in componentsByEntity)
			{
				HttpC httpC = (HttpC)component;
				HttpC httpC2 = httpC;
				HttpS.RemoveComponent(httpC2);
			}
		}
		this.m_fetchingComments = true;
		new PsUILoadingAnimation(this.m_commentList, false).Update();
		this.m_commentList.CalculateReferenceSizes();
		this.m_commentList.UpdateSize();
		this.m_commentList.ArrangeContents();
		this.m_commentList.UpdateDimensions();
		this.m_commentList.UpdateSize();
		this.m_commentList.UpdateAlign();
		this.m_commentList.UpdateChildrenAlign();
		this.m_commentList.ArrangeContents();
		this.m_commentList.m_parent.ArrangeContents();
		this.m_currentIndex = 0;
		this.m_runningIndex = 0;
		this.m_comments = null;
		this.m_oldDataRemoved = true;
	}

	// Token: 0x060013A7 RID: 5031 RVA: 0x000C4568 File Offset: 0x000C2968
	public void ReloadNewData()
	{
		if (this.m_oldDataRemoved)
		{
			this.GetComments();
		}
		this.m_oldDataRemoved = false;
	}

	// Token: 0x060013A8 RID: 5032 RVA: 0x000C4582 File Offset: 0x000C2982
	public override void GetComments()
	{
		this.m_fetchingComments = true;
		new PsServerQueueFlow(null, delegate
		{
			if (this.m_TC.p_entity != null)
			{
				HttpC comments = Tournament.GetComments(new Action<CommentData[]>(this.GetCommentsOK), new Action<HttpC>(this.GetCommentsFAILED), null, 50);
				EntityManager.AddComponentToEntity(this.m_TC.p_entity, comments);
			}
		}, new string[] { "Comment" });
	}

	// Token: 0x060013A9 RID: 5033 RVA: 0x000C45AC File Offset: 0x000C29AC
	protected override void GetCommentsFAILED(HttpC _c)
	{
		Debug.Log("GET COMMENTS FAILED", null);
		ServerManager.ThrowServerErrorException(PsStrings.Get(StringID.CONNECTION_ERROR_HEADER), _c.www, delegate
		{
			HttpC comments = Tournament.GetComments(new Action<CommentData[]>(this.GetCommentsOK), new Action<HttpC>(this.GetCommentsFAILED), null, 50);
			EntityManager.AddComponentToEntity(this.m_TC.p_entity, comments);
			return comments;
		}, null);
	}

	// Token: 0x0400167C RID: 5756
	private bool m_oldDataRemoved;
}
