using System;

// Token: 0x020002E9 RID: 745
public class PsUICenterWinAdventureBattle : PsUICenterWinAdventure
{
	// Token: 0x0600160F RID: 5647 RVA: 0x000E6AB7 File Offset: 0x000E4EB7
	public PsUICenterWinAdventureBattle(UIComponent _parent)
		: base(_parent)
	{
	}

	// Token: 0x06001610 RID: 5648 RVA: 0x000E6AC0 File Offset: 0x000E4EC0
	public override void CreateCenterContent()
	{
		this.m_skullRiderCharacter = new PsUISkullRiderCharacter(this.m_TC.p_entity);
		this.m_skullRiderCharacter.Exit(new Action(this.CreateReward));
	}

	// Token: 0x06001611 RID: 5649 RVA: 0x000E6AF0 File Offset: 0x000E4EF0
	public void CreateReward()
	{
		PsGachaManager.m_lastOpenedGacha = GachaType.BOSS;
		PsGachaManager.m_lastGachaRewards = PsGachaManager.OpenGacha(new PsGacha(GachaType.BOSS), -1, true);
		PsMetrics.ChestOpened("bossWon");
		PsUIBasePopup popup = new PsUIBasePopup(typeof(PsUICenterOpenGacha), null, null, null, true, true, InitialPage.Center, false, false, false);
		popup.SetAction("Exit", delegate
		{
			popup.Destroy();
			(this.GetRoot() as PsUIBasePopup).CallAction("Continue");
		});
	}

	// Token: 0x06001612 RID: 5650 RVA: 0x000E6B66 File Offset: 0x000E4F66
	public override void CreateLowerLeftContent()
	{
	}

	// Token: 0x06001613 RID: 5651 RVA: 0x000E6B68 File Offset: 0x000E4F68
	public override void CreateLowerRightContent()
	{
	}

	// Token: 0x06001614 RID: 5652 RVA: 0x000E6B6A File Offset: 0x000E4F6A
	public override void CreateLowerCenterContent()
	{
	}

	// Token: 0x06001615 RID: 5653 RVA: 0x000E6B6C File Offset: 0x000E4F6C
	public override void Destroy()
	{
		if (this.m_skullRiderCharacter != null)
		{
			this.m_skullRiderCharacter.Destroy();
			this.m_skullRiderCharacter = null;
		}
		base.Destroy();
	}

	// Token: 0x040018DE RID: 6366
	public PsUISkullRiderCharacter m_skullRiderCharacter;
}
