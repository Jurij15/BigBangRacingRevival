using System;

// Token: 0x02000235 RID: 565
public class PsUIRacingChestSlotPopup : PsUIChestSlotPopup
{
	// Token: 0x060010EF RID: 4335 RVA: 0x000A2C4E File Offset: 0x000A104E
	public PsUIRacingChestSlotPopup(UIComponent _parent)
		: base(_parent)
	{
	}

	// Token: 0x060010F0 RID: 4336 RVA: 0x000A2C57 File Offset: 0x000A1057
	public override void Initialize()
	{
		base.Initialize();
		this.m_headerText = PsStrings.Get(StringID.TREASURE_REMINDER_RACING_TITLE);
		this.m_contentText = PsStrings.Get(StringID.TREASURE_REMINDER_RACING_TEXT);
		this.m_playButtonText = PsStrings.Get(StringID.RACE_START);
	}

	// Token: 0x060010F1 RID: 4337 RVA: 0x000A2C8C File Offset: 0x000A108C
	public override void Destroy()
	{
		base.Destroy();
		if (PsMainMenuState.m_racingGacha != null)
		{
			TouchAreaS.SetCamera(PsMainMenuState.m_racingGacha.m_TAC, CameraS.m_uiCamera);
		}
	}
}
