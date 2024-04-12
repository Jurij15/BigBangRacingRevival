using System;

// Token: 0x020002EA RID: 746
public class PsUICenterLoseAdventureBattle : PsUICenterStartAdventureBattle
{
	// Token: 0x06001616 RID: 5654 RVA: 0x000E6BC1 File Offset: 0x000E4FC1
	public PsUICenterLoseAdventureBattle(UIComponent _parent)
		: base(_parent)
	{
	}

	// Token: 0x06001617 RID: 5655 RVA: 0x000E6BCC File Offset: 0x000E4FCC
	public override void CreateCenterContent()
	{
		PsUIPowerFuels psUIPowerFuels = new PsUIPowerFuels(this, new Action(base.UpdatePowerFuelCc));
		psUIPowerFuels.SetVerticalAlign(0f);
		this.m_skullRiderCharacter = new PsUISkullRiderCharacter(this.m_TC.p_entity);
		if (!PsState.m_activeMinigame.m_playerReachedGoal)
		{
			this.m_skullRiderCharacter.Taunt();
		}
		else
		{
			this.m_skullRiderCharacter.Talk();
		}
	}

	// Token: 0x06001618 RID: 5656 RVA: 0x000E6C37 File Offset: 0x000E5037
	public override void Destroy()
	{
		if (this.m_skullRiderCharacter != null)
		{
			this.m_skullRiderCharacter.Destroy();
			this.m_skullRiderCharacter = null;
		}
		base.Destroy();
	}

	// Token: 0x040018DF RID: 6367
	public PsUISkullRiderCharacter m_skullRiderCharacter;
}
