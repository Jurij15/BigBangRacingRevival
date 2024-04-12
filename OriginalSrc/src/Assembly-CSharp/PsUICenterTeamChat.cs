using System;
using UnityEngine;

// Token: 0x0200036B RID: 875
public class PsUICenterTeamChat : PsUICenterMyTeam
{
	// Token: 0x06001966 RID: 6502 RVA: 0x00115D7C File Offset: 0x0011417C
	public PsUICenterTeamChat(UIComponent _parent)
		: base(_parent)
	{
	}

	// Token: 0x06001967 RID: 6503 RVA: 0x00115D88 File Offset: 0x00114188
	public override void CreateRightSide()
	{
		this.m_commentArea = new UIScrollableCanvas(this, string.Empty);
		this.m_commentArea.SetWidth(0.6f, RelativeTo.ParentWidth);
		this.m_commentArea.SetHeight(0.705f, RelativeTo.ScreenHeight);
		this.m_commentArea.SetAlign(1f, 1f);
		this.m_commentArea.SetMargins(0.0618f, 0.0618f, 0f, 0f, RelativeTo.ScreenWidth);
		this.m_commentArea.RemoveDrawHandler();
		this.m_commentList = new UIVerticalList(this.m_commentArea, string.Empty);
		this.m_commentList.SetWidth(1f, RelativeTo.ParentWidth);
		this.m_commentList.SetVerticalAlign(1f);
		this.m_commentList.SetSpacing(0.015f, RelativeTo.ScreenHeight);
		this.m_commentList.SetMargins(0f, 0f, 0.05f, 0.05f, RelativeTo.ScreenHeight);
		this.m_commentList.RemoveDrawHandler();
		this.m_commentList.SetVerticalAlign(0f);
		if (PsMetagameManager.m_team != null)
		{
			this.GetComments();
		}
		new PsUILoadingAnimation(this.m_commentList, false);
		UIHorizontalList uihorizontalList = new UIHorizontalList(this, string.Empty);
		uihorizontalList.SetAlign(1f, 0f);
		uihorizontalList.SetSpacing(-0.01f, RelativeTo.ScreenHeight);
		uihorizontalList.SetDepthOffset(-5f);
		uihorizontalList.RemoveDrawHandler();
		uihorizontalList.SetMargins(0.0125f, 0.0125f, 0f, 0f, RelativeTo.ScreenHeight);
		this.m_commentField = new PsUICommentField(uihorizontalList);
		this.m_commentField.m_textField.SetWidth(0.55f, RelativeTo.ScreenWidth);
		this.m_commentField.m_textField.SetHeight(0.08f, RelativeTo.ScreenHeight);
		this.m_commentField.m_profileImage.Destroy();
		this.m_commentField.SetMargins(0.02f, 0.02f, 0.01f, 0.01f, RelativeTo.ScreenHeight);
		(this.m_commentField.m_textField as UITextbox).SetMinRows(1);
		(this.m_commentField.m_textField as UITextbox).SetMaxRows(1);
		this.m_commentField.m_textField.SetDrawHandler(new UIDrawDelegate(UIDrawHandlers.TextfieldOutlined));
		this.m_commentField.SetMinMaxCharacterCount(0, 100);
		this.m_commentField.SetCallbacks(new Action<string>(this.DoneWriting), null);
	}

	// Token: 0x06001968 RID: 6504 RVA: 0x00115FE3 File Offset: 0x001143E3
	public void DoneWriting(string _input)
	{
		this.m_input = _input;
		if (!string.IsNullOrEmpty(this.m_input) && PsMetagameManager.m_team != null)
		{
			this.SendComment();
		}
	}

	// Token: 0x06001969 RID: 6505 RVA: 0x0011600C File Offset: 0x0011440C
	public void GetComments()
	{
		this.m_fetchingComments = true;
		new PsServerQueueFlow(null, delegate
		{
			if (this.m_TC.p_entity != null)
			{
				HttpC comments = PsMetagameManager.GetComments(PsMetagameManager.m_team.id, new Action<CommentData[]>(this.GetCommentsOK), new Action<HttpC>(this.GetCommentsFAILED), null);
				EntityManager.AddComponentToEntity(this.m_TC.p_entity, comments);
			}
		}, new string[] { "Comment" });
	}

	// Token: 0x0600196A RID: 6506 RVA: 0x00116038 File Offset: 0x00114438
	public void ReverseComments(CommentData[] _comments)
	{
		for (int i = 0; i < _comments.Length / 2; i++)
		{
			CommentData commentData = _comments[i];
			_comments[i] = _comments[_comments.Length - i - 1];
			_comments[_comments.Length - i - 1] = commentData;
		}
	}

	// Token: 0x0600196B RID: 6507 RVA: 0x0011609C File Offset: 0x0011449C
	private void GetCommentsOK(CommentData[] _comments)
	{
		this.ReverseComments(_comments);
		Debug.Log("GET COMMENTS SUCCEED", null);
		if (PlayerPrefsX.GetNewComments() > 0)
		{
			PlayerPrefsX.SetNewComments(0);
			((this.GetRoot() as PsUIBasePopup).m_mainContent as PsUITabbedJoinedTeam).DestroyNotification();
		}
		this.m_fetchingComments = false;
		this.m_currentTick = 0;
		this.m_comments = _comments;
		this.m_destroyAndCreate = true;
		if (this.m_currentIndex == 50)
		{
			PsUITeamChatComment psUITeamChatComment = this.m_commentList.m_childs[this.m_runningIndex - 1] as PsUITeamChatComment;
			if (psUITeamChatComment != null)
			{
				int num = 0;
				for (int i = this.m_comments.Length - 1; i >= 0; i--)
				{
					if (psUITeamChatComment.m_comment.playerId == this.m_comments[i].playerId && psUITeamChatComment.m_comment.comment == this.m_comments[i].comment && psUITeamChatComment.m_comment.timestamp == this.m_comments[i].timestamp)
					{
						num = i + 1;
						break;
					}
				}
				this.m_currentIndex = num;
				if (this.m_currentIndex == 0)
				{
					this.m_runningIndex = 0;
				}
			}
		}
	}

	// Token: 0x0600196C RID: 6508 RVA: 0x001161DF File Offset: 0x001145DF
	private void GetCommentsFAILED(HttpC _c)
	{
		Debug.Log("GET COMMENTS FAILED", null);
		ServerManager.ThrowServerErrorException(PsStrings.Get(StringID.CONNECTION_ERROR_HEADER), _c.www, delegate
		{
			HttpC comments = PsMetagameManager.GetComments(PsMetagameManager.m_team.id, new Action<CommentData[]>(this.GetCommentsOK), new Action<HttpC>(this.GetCommentsFAILED), null);
			EntityManager.AddComponentToEntity(this.m_TC.p_entity, comments);
			return comments;
		}, null);
	}

	// Token: 0x0600196D RID: 6509 RVA: 0x0011620E File Offset: 0x0011460E
	public override void GetTeam()
	{
		this.m_sideList.DestroyChildren(1);
		new PsUILoadingAnimation(this.m_sideList, false);
		PsMetagameManager.GetOwnTeam(new Action<TeamData>(this.TeamGetOK), false);
	}

	// Token: 0x0600196E RID: 6510 RVA: 0x0011623C File Offset: 0x0011463C
	public override void TeamGetOK(TeamData _data)
	{
		Debug.Log("GET TEAM SUCCEED", null);
		PsMetagameManager.m_team = _data;
		this.m_sideList.DestroyChildren(1);
		this.m_searched = true;
	}

	// Token: 0x0600196F RID: 6511 RVA: 0x00116264 File Offset: 0x00114664
	public void CommentFAILED(HttpC _c)
	{
		Debug.Log("COMMENT FAILED", null);
		ServerManager.ThrowServerErrorException(PsStrings.Get(StringID.CONNECTION_ERROR_HEADER), _c.www, () => PsServerRequest.ServerComment((PsMetagameManager.CommentInfo)_c.objectData, new Action<CommentData[]>(this.GetCommentsOK), new Action<HttpC>(this.CommentFAILED), null), null);
	}

	// Token: 0x06001970 RID: 6512 RVA: 0x001162B8 File Offset: 0x001146B8
	public void SendComment()
	{
		CommentData commentData = default(CommentData);
		commentData.playerId = PlayerPrefsX.GetUserId();
		commentData.comment = this.m_input;
		commentData.name = PlayerPrefsX.GetUserName();
		commentData.facebookId = PlayerPrefsX.GetFacebookId();
		commentData.gameCenterId = PlayerPrefsX.GetGameCenterId();
		string commentString = this.m_input;
		new PsServerQueueFlow(null, delegate
		{
			HttpC httpC = PsMetagameManager.Comment(PsMetagameManager.m_team.id, commentString, string.Empty, null, new Action<CommentData[]>(this.GetCommentsOK), new Action<HttpC>(this.CommentFAILED));
			EntityManager.AddComponentToEntity(this.m_TC.p_entity, httpC);
		}, new string[] { "Comment" });
		this.m_input = string.Empty;
		this.m_commentField.SetText(this.m_input);
		PsUITeamChatComment psUITeamChatComment = new PsUITeamChatComment(this.m_commentList, commentData);
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
		this.ScrollContents(true);
	}

	// Token: 0x06001971 RID: 6513 RVA: 0x001163F4 File Offset: 0x001147F4
	private void CreateBatch()
	{
		if (this.m_destroyAndCreate)
		{
			this.m_commentList.DestroyChildren(this.m_runningIndex);
			this.m_destroyAndCreate = false;
		}
		int num = Mathf.Min(this.m_currentIndex + 10, this.m_comments.Length);
		for (int i = this.m_currentIndex; i < num; i++)
		{
			this.m_runningIndex++;
			PsUITeamChatComment psUITeamChatComment = null;
			if (string.IsNullOrEmpty(this.m_comments[i].type))
			{
				psUITeamChatComment = new PsUITeamChatComment(this.m_commentList, this.m_comments[i]);
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
			psUITeamChatComment.Update();
		}
		float actualHeight = this.m_commentList.m_actualHeight;
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
		this.m_currentIndex = num;
		float num2 = this.m_commentList.m_TC.transform.position.y - this.m_commentList.m_actualHeight * 0.5f;
		float num3 = this.m_commentArea.m_scrollTC.transform.position.y - this.m_commentArea.m_actualHeight * 0.5f;
		float num4 = num2 - num3;
		if (actualHeight > 0f && num4 * -1f >= (float)Screen.height / 5f)
		{
			Vector3 vector = this.m_commentArea.m_scrollTC.transform.position + new Vector3(0f, this.m_commentList.m_actualHeight - actualHeight, 0f);
			this.m_commentArea.m_scrollTC.transform.position = vector;
		}
		else
		{
			this.ScrollContents(true);
		}
	}

	// Token: 0x06001972 RID: 6514 RVA: 0x001167AC File Offset: 0x00114BAC
	public void ScrollContents(bool _always)
	{
		float num = this.m_commentList.m_TC.transform.position.y - this.m_commentList.m_actualHeight * 0.5f;
		float num2 = this.m_commentArea.m_scrollTC.transform.position.y - this.m_commentArea.m_actualHeight * 0.5f;
		float num3 = num - num2;
		if (_always || num3 * -1f < (float)(Screen.height / 5))
		{
			Vector2 vector = this.m_commentArea.m_scrollTC.transform.position + new Vector2(0f, num3);
			this.m_commentArea.ScrollToPosition(vector, null);
		}
	}

	// Token: 0x06001973 RID: 6515 RVA: 0x0011687C File Offset: 0x00114C7C
	public override void UpdateLogic()
	{
		if (!this.m_fetchingComments)
		{
			this.m_currentTick++;
			if (this.m_currentTick >= 600)
			{
				this.GetComments();
			}
		}
		if (this.m_searched && !this.m_sideCreated)
		{
			base.CreateSideInfo();
			this.m_sideList.Update();
		}
		if (this.m_comments != null && (this.m_currentIndex < this.m_comments.Length || (this.m_comments.Length == 0 && this.m_currentIndex == 0 && this.m_destroyAndCreate)) && this.m_TC.p_entity.m_active)
		{
			this.CreateBatch();
		}
		if (this.m_leave.m_hit)
		{
			this.m_popup = new PsUIBasePopup(typeof(PsUICenterLeaveTeam), null, null, null, true, true, InitialPage.Center, false, false, false);
			this.m_popup.SetAction("Exit", delegate
			{
				this.m_popup.Destroy();
				this.m_popup = null;
			});
			this.m_popup.SetAction("Proceed", delegate
			{
				base.LeaveTeam();
			});
			TweenS.AddTransformTween(this.m_popup.m_mainContent.m_parent.m_TC, TweenedProperty.Scale, TweenStyle.ElasticOut, Vector3.one * 0.75f, Vector3.one, 0.75f, 0f, true);
		}
		else if (this.m_settings != null && this.m_settings.m_hit)
		{
			PsUIBasePopup popup = new PsUIBasePopup(typeof(PsUICenterTeamSettings), null, null, null, true, true, InitialPage.Center, false, false, false);
			popup.SetAction("Exit", delegate
			{
				popup.Destroy();
			});
			popup.SetAction("Save", delegate
			{
				popup.Destroy();
				this.UpdateTeamInfo();
			});
		}
	}

	// Token: 0x04001BF3 RID: 7155
	private CommentData[] m_comments;

	// Token: 0x04001BF4 RID: 7156
	private UIScrollableCanvas m_commentArea;

	// Token: 0x04001BF5 RID: 7157
	private UIVerticalList m_commentList;

	// Token: 0x04001BF6 RID: 7158
	private bool m_destroyAndCreate;

	// Token: 0x04001BF7 RID: 7159
	private PsUICommentField m_commentField;

	// Token: 0x04001BF8 RID: 7160
	private string m_input;

	// Token: 0x04001BF9 RID: 7161
	private const int REFRESH_TICKS = 600;

	// Token: 0x04001BFA RID: 7162
	private int m_currentTick;

	// Token: 0x04001BFB RID: 7163
	private bool m_fetchingComments;

	// Token: 0x04001BFC RID: 7164
	private int m_runningIndex;
}
