using System;
using Server;
using UnityEngine;

// Token: 0x0200030D RID: 781
public class PsUIFriendLevelList : UICanvas
{
	// Token: 0x06001718 RID: 5912 RVA: 0x000F8A84 File Offset: 0x000F6E84
	public PsUIFriendLevelList(UIComponent _parent)
		: base(_parent, false, string.Empty, null, string.Empty)
	{
		PsUITabbedCreate.m_selectedTab = 2;
		(this.m_parent as UIScrollableCanvas).SetScrollPositionToChild(this);
		(this.m_parent as UIScrollableCanvas).m_maxScrollInertialY = 0f;
		(this.m_parent as UIScrollableCanvas).SetScrollPosition(0f, 0f);
		this.SetWidth(1f, RelativeTo.ParentWidth);
		this.SetHeight(1f, RelativeTo.ParentHeight);
		this.SetMargins(0f, 0f, 0f, 0f, RelativeTo.ScreenHeight);
		this.SetVerticalAlign(1f);
		this.RemoveDrawHandler();
		UIText uitext = new UIText(null, false, string.Empty, PsStrings.Get(StringID.SOCIAL_LATEST_LEVELS), PsFontManager.GetFont(PsFonts.KGSecondChances), 0.04f, RelativeTo.ScreenHeight, "ffffff", "313131");
		uitext.SetShadowShift(new Vector2(0.5f, -0.2f), 0.125f);
		uitext.SetVerticalAlign(1f);
		uitext.SetMargins(0f, 0f, -0.04f, 0.04f, RelativeTo.ScreenHeight);
		this.m_levelArea = new PsUIProfileLevelArea(this, 4, new cpBB(0.12f, 0.1f, 0.12f, 0.085f), RelativeTo.ScreenWidth, -1, null);
		this.m_levelArea.SetHeight(1f, RelativeTo.ParentHeight);
		this.m_levelArea.SetWidth(1f, RelativeTo.ParentWidth);
		this.m_levelArea.AddTitleComponent(uitext);
		this.m_levelArea.PopulateContent(null, typeof(PsGameLoopSocial), "User has no levels", 0.02f, false, false, false);
		this.m_levelArea.SetVerticalAlign(0f);
		this.LoadContent();
	}

	// Token: 0x06001719 RID: 5913 RVA: 0x000F8C28 File Offset: 0x000F7028
	public void LoadContent()
	{
		HttpC followeeMinigames = MiniGame.GetFolloweeMinigames(new Action<PsMinigameMetaData[]>(this.DataSUCCEED), new Action<HttpC>(this.DataFAILED), 40, null);
		EntityManager.AddComponentToEntity(this.m_TC.p_entity, followeeMinigames);
	}

	// Token: 0x0600171A RID: 5914 RVA: 0x000F8C67 File Offset: 0x000F7067
	private void DataSUCCEED(PsMinigameMetaData[] _data)
	{
		this.m_levelArea.PopulateContent(_data, typeof(PsGameLoopSocial), PsStrings.Get(StringID.SOCIAL_NO_LEVELS_FOUND), 0.03f, false, true, false);
		this.m_parent.Update();
	}

	// Token: 0x0600171B RID: 5915 RVA: 0x000F8C9C File Offset: 0x000F709C
	private void DataFAILED(HttpC _c)
	{
		Debug.Log("GET FOLLOWEE MINIGAMES FAILED", null);
		string networkError = ServerErrors.GetNetworkError(_c.www.error);
		ServerManager.ThrowServerErrorException(PsStrings.Get(StringID.CONNECTION_ERROR_HEADER), networkError, delegate
		{
			HttpC followeeMinigames = MiniGame.GetFolloweeMinigames(new Action<PsMinigameMetaData[]>(this.DataSUCCEED), new Action<HttpC>(this.DataFAILED), 50, null);
			EntityManager.AddComponentToEntity(this.m_TC.p_entity, followeeMinigames);
			return followeeMinigames;
		}, null, StringID.TRY_AGAIN_SERVER);
	}

	// Token: 0x0600171C RID: 5916 RVA: 0x000F8CE7 File Offset: 0x000F70E7
	public override void Step()
	{
		base.Step();
	}

	// Token: 0x0600171D RID: 5917 RVA: 0x000F8CEF File Offset: 0x000F70EF
	public override void Destroy()
	{
		(this.m_parent as UIScrollableCanvas).m_maxScrollInertialY = 50f / (1024f / (float)Screen.width);
		base.Destroy();
	}

	// Token: 0x040019E3 RID: 6627
	private PsUIProfileLevelArea m_levelArea;
}
