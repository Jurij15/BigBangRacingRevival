using System;
using Server;

// Token: 0x0200028E RID: 654
public class PsUIChatGlobal : PsUICenterChat
{
	// Token: 0x060013AC RID: 5036 RVA: 0x000C4754 File Offset: 0x000C2B54
	public PsUIChatGlobal(UIComponent _parent)
		: base(_parent)
	{
		this.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.TournamentChatBGDrawHandler));
		this.m_commentField.SetHintText(PsStrings.Get(StringID.TOURNAMENT_CHAT_BUTTON_GLOBAL), false);
	}

	// Token: 0x060013AD RID: 5037 RVA: 0x000C47A4 File Offset: 0x000C2BA4
	protected override void SendComment(string _comment)
	{
		new PsServerQueueFlow(null, delegate
		{
			HttpC httpC = Chat.SaveComment(_comment, new Action<CommentData[]>(this.GetCommentsOK), new Action<HttpC>(this.SendCommentFailed), null);
			httpC.objectData = _comment;
			EntityManager.AddComponentToEntity(this.m_TC.p_entity, httpC);
		}, new string[] { "Comment" });
	}

	// Token: 0x060013AE RID: 5038 RVA: 0x000C47E8 File Offset: 0x000C2BE8
	public void SendCommentFailed(HttpC _c)
	{
		Debug.Log("COMMENT FAILED", null);
		ServerManager.ThrowServerErrorException(PsStrings.Get(StringID.CONNECTION_ERROR_HEADER), _c.www, delegate
		{
			HttpC httpC = Chat.SaveComment((string)_c.objectData, new Action<CommentData[]>(this.GetCommentsOK), new Action<HttpC>(this.SendCommentFailed), null);
			httpC.objectData = _c.objectData;
			EntityManager.AddComponentToEntity(this.m_TC.p_entity, httpC);
			return httpC;
		}, null);
	}

	// Token: 0x060013AF RID: 5039 RVA: 0x000C483B File Offset: 0x000C2C3B
	public override void GetComments()
	{
		this.m_fetchingComments = true;
		new PsServerQueueFlow(null, delegate
		{
			if (this.m_TC.p_entity != null)
			{
				HttpC comments = Chat.GetComments(new Action<CommentData[]>(this.GetCommentsOK), new Action<HttpC>(this.GetCommentsFAILED), null, 50);
				EntityManager.AddComponentToEntity(this.m_TC.p_entity, comments);
			}
		}, new string[] { "Comment" });
	}

	// Token: 0x060013B0 RID: 5040 RVA: 0x000C4865 File Offset: 0x000C2C65
	protected override void GetCommentsFAILED(HttpC _c)
	{
		Debug.Log("GET COMMENTS FAILED", null);
		ServerManager.ThrowServerErrorException(PsStrings.Get(StringID.CONNECTION_ERROR_HEADER), _c.www, delegate
		{
			HttpC comments = Chat.GetComments(new Action<CommentData[]>(this.GetCommentsOK), new Action<HttpC>(this.GetCommentsFAILED), null, 50);
			EntityManager.AddComponentToEntity(this.m_TC.p_entity, comments);
			return comments;
		}, null);
	}
}
