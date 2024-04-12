using System;
using Server;

// Token: 0x02000241 RID: 577
public class PsUIComments : UIVerticalList
{
	// Token: 0x0600117F RID: 4479 RVA: 0x000A9F8E File Offset: 0x000A838E
	public PsUIComments(UIComponent _parent, bool _touchable, string _tag, string _gameId, bool _loadImmediately = true)
		: base(_parent, _tag)
	{
		this.SetSpacing(0.025f, RelativeTo.ScreenShortest);
		this.RemoveDrawHandler();
		this.RemoveTouchAreas();
		this.m_gameId = _gameId;
		this.m_fetchingData = false;
		if (_loadImmediately)
		{
			this.LoadContent();
		}
	}

	// Token: 0x06001180 RID: 4480 RVA: 0x000A9FCC File Offset: 0x000A83CC
	public void LoadContent()
	{
		if (!this.m_fetchingData)
		{
			HttpC httpC = Comment.Get(this.m_gameId, new Action<CommentData[]>(this.DataRequestSUCCEED), new Action<HttpC>(this.DataRequestFAILED), null, 50);
			EntityManager.AddComponentToEntity(this.m_TC.p_entity, httpC);
			this.m_fetchingData = true;
		}
	}

	// Token: 0x06001181 RID: 4481 RVA: 0x000AA023 File Offset: 0x000A8423
	private void DataRequestSUCCEED(CommentData[] _commentData)
	{
		if (!this.m_hidden)
		{
			this.HandleData(_commentData);
		}
		this.m_fetchingData = false;
	}

	// Token: 0x06001182 RID: 4482 RVA: 0x000AA03E File Offset: 0x000A843E
	private void DataRequestFAILED(HttpC _request)
	{
		this.m_fetchingData = false;
		Debug.Log("Screenshot FAILED", null);
	}

	// Token: 0x06001183 RID: 4483 RVA: 0x000AA054 File Offset: 0x000A8454
	private void HandleData(CommentData[] _commentData)
	{
		for (int i = 0; i < _commentData.Length; i++)
		{
			this.CreateComment(_commentData[i]);
		}
		this.m_parent.m_parent.Update();
	}

	// Token: 0x06001184 RID: 4484 RVA: 0x000AA098 File Offset: 0x000A8498
	private void CreateComment(CommentData _commentData)
	{
		UIScaleToContentCanvas uiscaleToContentCanvas = new UIScaleToContentCanvas(this, "Row", true, true);
		uiscaleToContentCanvas.SetWidth(0.75f, RelativeTo.ParentWidth);
		uiscaleToContentCanvas.SetHeight(1f, RelativeTo.ScreenHeight);
		uiscaleToContentCanvas.RemoveTouchAreas();
		uiscaleToContentCanvas.RemoveDrawHandler();
		string text = "<color=#ffffff>";
		if (PlayerPrefsX.GetUserId().Equals(_commentData.playerId))
		{
			text = "<color=#80ff33>";
		}
		UIVerticalList uiverticalList = new UIVerticalList(uiscaleToContentCanvas, string.Empty);
		uiverticalList.SetAlign(0f, 1f);
		uiverticalList.SetMargins(0f, 0f, 0.035f, 0f, RelativeTo.ScreenHeight);
		uiverticalList.RemoveDrawHandler();
		uiverticalList.RemoveTouchAreas();
		PsUIProfileImage psUIProfileImage = new PsUIProfileImage(uiverticalList, false, "profile", _commentData.facebookId, _commentData.gameCenterId, -1, true, false, false, 0.1f, 0.06f, "fff9e6", null, false, true);
		psUIProfileImage.SetSize(0.075f, 0.075f, RelativeTo.ScreenShortest);
		uiverticalList = new UIVerticalList(uiscaleToContentCanvas, string.Empty);
		uiverticalList.SetAlign(1f, 1f);
		uiverticalList.SetMargins(0.1f, 0f, 0f, 0f, RelativeTo.ScreenShortest);
		uiverticalList.RemoveTouchAreas();
		UIText uitext = new UIText(uiverticalList, false, "User", text + PlayerPrefsX.GetUserName() + "</color>", PsFontManager.GetFont(PsFonts.HurmeSemiBold), 0.03f, RelativeTo.ScreenHeight, null, null);
		uitext.SetHorizontalAlign(0f);
		uitext.SetDrawHandler(new UIDrawDelegate(UIDrawHandlers.DebugRect));
		UITextbox uitextbox = new UITextbox(uiverticalList, false, "Comment", _commentData.comment, PsFontManager.GetFont(PsFonts.HurmeRegular), 0.035f, RelativeTo.ScreenHeight, false, Align.Left, Align.Top, null, true, null);
		uitextbox.SetWidth(1f, RelativeTo.ParentWidth);
		uitextbox.SetMargins(0.025f, 0.025f, 0.025f, 0.025f, RelativeTo.ScreenShortest);
		uitextbox.SetHorizontalAlign(0f);
		uitextbox.SetDrawHandler(new UIDrawDelegate(UIDrawHandlers.DebugRect));
	}

	// Token: 0x06001185 RID: 4485 RVA: 0x000AA297 File Offset: 0x000A8697
	public void SendComment(string _comment)
	{
	}

	// Token: 0x06001186 RID: 4486 RVA: 0x000AA299 File Offset: 0x000A8699
	private void DataSendSUCCEED(HttpC _request)
	{
		Debug.Log("Comment send SUCCEED", null);
	}

	// Token: 0x06001187 RID: 4487 RVA: 0x000AA2A6 File Offset: 0x000A86A6
	private void DataSendFAILED(HttpC _request)
	{
		Debug.Log("Comment send FAILED", null);
	}

	// Token: 0x06001188 RID: 4488 RVA: 0x000AA2B3 File Offset: 0x000A86B3
	public override void Destroy()
	{
		base.Destroy();
	}

	// Token: 0x0400147B RID: 5243
	public string m_gameId;

	// Token: 0x0400147C RID: 5244
	public bool m_fetchingData;
}
