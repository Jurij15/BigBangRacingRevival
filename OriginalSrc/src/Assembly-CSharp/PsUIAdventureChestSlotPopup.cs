using System;

// Token: 0x02000234 RID: 564
public class PsUIAdventureChestSlotPopup : PsUIChestSlotPopup
{
	// Token: 0x060010EC RID: 4332 RVA: 0x000A2BEA File Offset: 0x000A0FEA
	public PsUIAdventureChestSlotPopup(UIComponent _parent)
		: base(_parent)
	{
	}

	// Token: 0x060010ED RID: 4333 RVA: 0x000A2BF3 File Offset: 0x000A0FF3
	public override void Initialize()
	{
		base.Initialize();
		this.m_headerText = PsStrings.Get(StringID.TREASURE_REMINDER_ADVENTURE_TITLE);
		this.m_contentText = PsStrings.Get(StringID.TREASURE_REMINDER_ADVENTURE_TEXT);
		this.m_playButtonText = PsStrings.Get(StringID.PLAY);
	}

	// Token: 0x060010EE RID: 4334 RVA: 0x000A2C28 File Offset: 0x000A1028
	public override void Destroy()
	{
		base.Destroy();
		if (PsMainMenuState.m_adventureGacha != null)
		{
			TouchAreaS.SetCamera(PsMainMenuState.m_adventureGacha.m_TAC, CameraS.m_uiCamera);
		}
	}
}
