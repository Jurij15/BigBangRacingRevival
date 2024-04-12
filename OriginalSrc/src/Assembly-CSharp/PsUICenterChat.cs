using System;
using UnityEngine;

// Token: 0x0200028C RID: 652
public class PsUICenterChat : UICanvas
{
	// Token: 0x0600138B RID: 5003 RVA: 0x000C2FF8 File Offset: 0x000C13F8
	public PsUICenterChat(UIComponent _parent)
		: base(_parent, false, "Chat", null, string.Empty)
	{
		UICanvas uicanvas = new UICanvas(this, false, string.Empty, null, string.Empty);
		uicanvas.SetMargins(0f, 0f, 0f, 0.08f, RelativeTo.ScreenHeight);
		uicanvas.RemoveDrawHandler();
		this.m_commentArea = new UIScrollableCanvas(uicanvas, "CommentArea");
		this.m_commentArea.SetHeight(1f, RelativeTo.ParentHeight);
		this.m_commentArea.SetAlign(1f, 1f);
		this.m_commentArea.SetMargins(0.08f, 0.08f, 0f, 0f, RelativeTo.ParentWidth);
		this.m_commentArea.RemoveDrawHandler();
		this.m_commentList = new UIVerticalList(this.m_commentArea, "CommentList");
		this.m_commentList.SetWidth(1f, RelativeTo.ParentWidth);
		this.m_commentList.SetVerticalAlign(1f);
		this.m_commentList.SetSpacing(0.015f, RelativeTo.ScreenHeight);
		this.m_commentList.SetMargins(0f, 0f, 0.025f, 0.025f, RelativeTo.ScreenHeight);
		this.m_commentList.RemoveDrawHandler();
		this.m_commentList.SetVerticalAlign(0f);
		this.GetComments();
		new PsUILoadingAnimation(this.m_commentList, false);
		UIHorizontalList uihorizontalList = new UIHorizontalList(this, string.Empty);
		uihorizontalList.SetAlign(1f, 0f);
		uihorizontalList.SetSpacing(-0.01f, RelativeTo.ScreenHeight);
		uihorizontalList.SetDepthOffset(-50f);
		uihorizontalList.RemoveDrawHandler();
		uihorizontalList.SetMargins(0.0125f, 0.0125f, 0f, 0f, RelativeTo.ScreenHeight);
		this.m_commentField = new PsUICommentField(uihorizontalList);
		this.m_commentField.m_textField.SetWidth(1f, RelativeTo.ParentWidth);
		this.m_commentField.m_textField.SetHeight(0.08f, RelativeTo.ScreenHeight);
		this.m_commentField.m_profileImage.Destroy();
		this.m_commentField.SetMargins(0.02f, 0.02f, 0.01f, 0.01f, RelativeTo.ScreenHeight);
		(this.m_commentField.m_textField as UITextbox).SetMinRows(1);
		(this.m_commentField.m_textField as UITextbox).SetMaxRows(1);
		this.m_commentField.m_textField.SetDrawHandler(new UIDrawDelegate(UIDrawHandlers.TextfieldOutlined));
		this.m_commentField.SetMinMaxCharacterCount(0, 100);
		this.m_commentField.SetCallbacks(new Action<string>(this.DoneWriting), null);
	}

	// Token: 0x0600138C RID: 5004 RVA: 0x000C32A2 File Offset: 0x000C16A2
	public void AddChatCommentsLoadedCallback(Action _callback)
	{
		this.m_newCommentsLoaded = (Action)Delegate.Remove(this.m_newCommentsLoaded, _callback);
		this.m_newCommentsLoaded = (Action)Delegate.Combine(this.m_newCommentsLoaded, _callback);
	}

	// Token: 0x0600138D RID: 5005 RVA: 0x000C32D2 File Offset: 0x000C16D2
	public void DoneWriting(string _input)
	{
		this.m_input = _input;
		if (!string.IsNullOrEmpty(this.m_input))
		{
			this.SendComment();
		}
	}

	// Token: 0x0600138E RID: 5006 RVA: 0x000C32F1 File Offset: 0x000C16F1
	public virtual void GetComments()
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

	// Token: 0x0600138F RID: 5007 RVA: 0x000C331C File Offset: 0x000C171C
	public void ReverseComments(CommentData[] _comments)
	{
		for (int i = 0; i < _comments.Length / 2; i++)
		{
			CommentData commentData = _comments[i];
			_comments[i] = _comments[_comments.Length - i - 1];
			_comments[_comments.Length - i - 1] = commentData;
		}
	}

	// Token: 0x06001390 RID: 5008 RVA: 0x000C3380 File Offset: 0x000C1780
	protected virtual void GetCommentsOK(CommentData[] _comments)
	{
		this.ReverseComments(_comments);
		Debug.Log("GET COMMENTS SUCCEED", null);
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
		this.m_newCommentsLoaded.Invoke();
	}

	// Token: 0x06001391 RID: 5009 RVA: 0x000C34A3 File Offset: 0x000C18A3
	public long GetLastMessageTimeStamp()
	{
		if (this.m_comments != null && this.m_comments.Length > 0)
		{
			return this.m_comments[this.m_comments.Length - 1].timestamp;
		}
		return 0L;
	}

	// Token: 0x06001392 RID: 5010 RVA: 0x000C34DB File Offset: 0x000C18DB
	protected virtual void GetCommentsFAILED(HttpC _c)
	{
		Debug.Log("GET COMMENTS FAILED", null);
		ServerManager.ThrowServerErrorException(PsStrings.Get(StringID.CONNECTION_ERROR_HEADER), _c.www, delegate
		{
			HttpC comments = PsMetagameManager.GetComments(PsMetagameManager.m_team.id, new Action<CommentData[]>(this.GetCommentsOK), new Action<HttpC>(this.GetCommentsFAILED), null);
			EntityManager.AddComponentToEntity(this.m_TC.p_entity, comments);
			return comments;
		}, null);
	}

	// Token: 0x06001393 RID: 5011 RVA: 0x000C350C File Offset: 0x000C190C
	public void CommentFAILED(HttpC _c)
	{
		Debug.Log("COMMENT FAILED", null);
		ServerManager.ThrowServerErrorException(PsStrings.Get(StringID.CONNECTION_ERROR_HEADER), _c.www, () => PsServerRequest.ServerComment((PsMetagameManager.CommentInfo)_c.objectData, new Action<CommentData[]>(this.GetCommentsOK), new Action<HttpC>(this.CommentFAILED), null), null);
	}

	// Token: 0x06001394 RID: 5012 RVA: 0x000C3560 File Offset: 0x000C1960
	public virtual void SendComment()
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

	// Token: 0x06001395 RID: 5013 RVA: 0x000C367C File Offset: 0x000C1A7C
	protected virtual void SendComment(string _comment)
	{
		new PsServerQueueFlow(null, delegate
		{
			HttpC httpC = PsMetagameManager.Comment(PsMetagameManager.m_team.id, _comment, string.Empty, null, new Action<CommentData[]>(this.GetCommentsOK), new Action<HttpC>(this.CommentFAILED));
			EntityManager.AddComponentToEntity(this.m_TC.p_entity, httpC);
		}, new string[] { "Comment" });
	}

	// Token: 0x06001396 RID: 5014 RVA: 0x000C36C0 File Offset: 0x000C1AC0
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
			PsUITeamChatComment commentType = this.GetCommentType(i);
			commentType.Update();
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
			this.ShowNewCommentsNotification();
		}
		else
		{
			this.ScrollContents(true);
		}
	}

	// Token: 0x06001397 RID: 5015 RVA: 0x000C38C0 File Offset: 0x000C1CC0
	private void ShowNewCommentsNotification()
	{
		if (this.m_notification != null)
		{
			this.m_notification.Destroy();
		}
		this.m_notification = new UIHorizontalList(this.m_commentArea, string.Empty);
		this.m_notification.CreateTouchAreas();
		this.m_notification.SetHeight(0.035f, RelativeTo.ScreenHeight);
		this.m_notification.SetVerticalAlign(0.01f);
		this.m_notification.SetDepthOffset(-30f);
		this.m_notification.SetMargins(0.4f, 0.4f, 0.25f, 0.25f, RelativeTo.OwnHeight);
		this.m_notification.SetDrawHandler(new UIDrawDelegate(this.NotificationDrawhandler));
		this.m_commentArea.FreezeToCamera(this.m_notification);
		UIText uitext = new UIText(this.m_notification, false, string.Empty, PsStrings.Get(StringID.TOUR_CHAT_NEW_MESSAGES), PsFontManager.GetFont(PsFonts.HurmeSemiBold), 0.9f, RelativeTo.ParentHeight, null, null);
		this.m_commentArea.UpdateFrozenChildren();
	}

	// Token: 0x06001398 RID: 5016 RVA: 0x000C39B4 File Offset: 0x000C1DB4
	private void HideNewCommentsNotification()
	{
		if (this.m_notification != null)
		{
			this.m_notification.Destroy();
			this.m_notification = null;
		}
	}

	// Token: 0x06001399 RID: 5017 RVA: 0x000C39D4 File Offset: 0x000C1DD4
	protected virtual void NotificationDrawhandler(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		float num = 0.015f * (float)Screen.height;
		Vector2[] roundedRect = DebugDraw.GetRoundedRect(_c.m_actualWidth, _c.m_actualHeight, num, 6, Vector2.zero);
		GGData ggdata = new GGData(roundedRect);
		Color color = DebugDraw.HexToColor("#F20B0B");
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward, roundedRect, 0.005f * (float)Screen.height, color, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line8Mat_Material), _c.m_camera, Position.Center, true);
		PrefabS.CreateFlatPrefabComponentsFromPolygon(_c.m_TC, Vector3.forward * 2f, ggdata, color, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera);
	}

	// Token: 0x0600139A RID: 5018 RVA: 0x000C3A88 File Offset: 0x000C1E88
	protected virtual PsUITeamChatComment GetCommentType(int i)
	{
		PsUITeamChatComment psUITeamChatComment;
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
		else
		{
			psUITeamChatComment = null;
		}
		return psUITeamChatComment;
	}

	// Token: 0x0600139B RID: 5019 RVA: 0x000C3C64 File Offset: 0x000C2064
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

	// Token: 0x0600139C RID: 5020 RVA: 0x000C3D34 File Offset: 0x000C2134
	public virtual void UpdateLogic()
	{
		if (!this.m_fetchingComments)
		{
			this.m_currentTick++;
			if (this.m_currentTick >= 600)
			{
				this.GetComments();
			}
		}
		if (this.m_comments != null && (this.m_currentIndex < this.m_comments.Length || (this.m_comments.Length == 0 && this.m_currentIndex == 0 && this.m_destroyAndCreate)) && this.m_TC.p_entity.m_active)
		{
			this.CreateBatch();
		}
	}

	// Token: 0x0600139D RID: 5021 RVA: 0x000C3DCC File Offset: 0x000C21CC
	public override void Step()
	{
		if (Input.GetKeyDown(107) && Input.GetKey(304))
		{
			this.ShowNewCommentsNotification();
		}
		if (this.m_notification != null && !this.m_notification.m_hidden)
		{
			float num = this.m_commentList.m_TC.transform.position.y - this.m_commentList.m_actualHeight * 0.5f;
			float num2 = this.m_commentArea.m_scrollTC.transform.position.y - this.m_commentArea.m_actualHeight * 0.5f;
			float num3 = num - num2;
			if (num3 * -1f < (float)Screen.height * 0.15f)
			{
				this.HideNewCommentsNotification();
			}
		}
		if (this.m_notification != null && this.m_notification.m_hit)
		{
			this.HideNewCommentsNotification();
			this.ScrollContents(true);
		}
		base.Step();
	}

	// Token: 0x0400166C RID: 5740
	protected CommentData[] m_comments;

	// Token: 0x0400166D RID: 5741
	public UIScrollableCanvas m_commentArea;

	// Token: 0x0400166E RID: 5742
	protected UIVerticalList m_commentList;

	// Token: 0x0400166F RID: 5743
	private bool m_destroyAndCreate;

	// Token: 0x04001670 RID: 5744
	protected PsUICommentField m_commentField;

	// Token: 0x04001671 RID: 5745
	protected string m_input;

	// Token: 0x04001672 RID: 5746
	private const int REFRESH_TICKS = 600;

	// Token: 0x04001673 RID: 5747
	private int m_currentTick;

	// Token: 0x04001674 RID: 5748
	protected bool m_fetchingComments;

	// Token: 0x04001675 RID: 5749
	protected int m_runningIndex;

	// Token: 0x04001676 RID: 5750
	protected int m_currentIndex;

	// Token: 0x04001677 RID: 5751
	private UICanvas m_notificationParent;

	// Token: 0x04001678 RID: 5752
	private UIHorizontalList m_notification;

	// Token: 0x04001679 RID: 5753
	public Action m_newCommentsLoaded = delegate
	{
		Debug.Log("New Chat comments loaded", null);
	};
}
