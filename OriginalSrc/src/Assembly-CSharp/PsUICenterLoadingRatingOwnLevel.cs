using System;

// Token: 0x020003AC RID: 940
public class PsUICenterLoadingRatingOwnLevel : PsUICenterRatingLoading
{
	// Token: 0x06001AEC RID: 6892 RVA: 0x0012BD3C File Offset: 0x0012A13C
	public PsUICenterLoadingRatingOwnLevel(UIComponent _parent)
		: base(_parent)
	{
		this.m_continuebutton = new PsUIGenericButton(this, 0.25f, 0.25f, 0.01f, "Button");
		this.m_continuebutton.SetMargins(0.02f, 0.02f, 0.01f, 0.01f, RelativeTo.ScreenHeight);
		this.m_continuebutton.SetGreenColors(true);
		this.m_continuebutton.SetText(PsStrings.Get(StringID.CONTINUE), 0.04f, 0.3f, RelativeTo.ScreenHeight, true, RelativeTo.ScreenShortest);
		this.m_continuebutton.SetHeight(0.12f, RelativeTo.ScreenHeight);
		this.m_continuebutton.SetHorizontalAlign(0.9f);
		this.m_continuebutton.SetVerticalAlign(0.1f);
		this.m_continuebutton.SetDepthOffset(-10f);
	}

	// Token: 0x06001AED RID: 6893 RVA: 0x0012BDFC File Offset: 0x0012A1FC
	public override void CreateRatingButtonList(UIComponent _parent)
	{
	}

	// Token: 0x06001AEE RID: 6894 RVA: 0x0012BDFE File Offset: 0x0012A1FE
	protected override void CreateCreatorInfo(UIComponent _parent)
	{
		this.m_creator = new PsUICreatorInfo(_parent, true, false, false, true, true, false);
		this.m_creator.SetHorizontalAlign(0.5f);
	}

	// Token: 0x06001AEF RID: 6895 RVA: 0x0012BE24 File Offset: 0x0012A224
	public override void Step()
	{
		if (this.m_continuebutton != null && this.m_continuebutton.m_hit && !this.m_continuePressed)
		{
			this.m_continuePressed = true;
			TouchAreaS.CancelAllTouches(null);
			TouchAreaS.Disable();
			this.Proceed();
		}
		base.Step();
	}

	// Token: 0x04001D58 RID: 7512
	private new PsUIGenericButton m_continuebutton;

	// Token: 0x04001D59 RID: 7513
	private bool m_continuePressed;
}
