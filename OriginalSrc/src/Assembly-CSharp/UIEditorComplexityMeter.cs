using System;

// Token: 0x0200038C RID: 908
public class UIEditorComplexityMeter : UIComponent
{
	// Token: 0x06001A2A RID: 6698 RVA: 0x00122B8C File Offset: 0x00120F8C
	public UIEditorComplexityMeter(UIComponent _parent, float _appearLimit = 0.7f)
		: base(_parent, false, "ComplexityMeter", null, null, string.Empty)
	{
		this.m_currentMinigame = LevelManager.m_currentLevel as Minigame;
		this.m_currentValue = 0f;
		this.RemoveDrawHandler();
		UIVerticalList uiverticalList = new UIVerticalList(this, "vlist");
		uiverticalList.RemoveDrawHandler();
		uiverticalList.SetWidth(0.06f, RelativeTo.ScreenShortest);
		UIFittedSprite uifittedSprite = new UIFittedSprite(uiverticalList, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("hud_thermometer_top", null), true, true);
		this.m_fillSprite = new UISprite(uifittedSprite, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("hud_thermometer_fill", null), false);
		this.m_fillSprite.SetHeight(0f, RelativeTo.ParentHeight);
		this.m_fillSprite.SetWidth(0.25f, RelativeTo.ParentWidth);
		this.m_fillSprite.SetVerticalAlign(0f);
		new UIFittedSprite(uiverticalList, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("hud_thermometer_bottom", null), true, true);
	}

	// Token: 0x06001A2B RID: 6699 RVA: 0x00122C9D File Offset: 0x0012109D
	public override void Step()
	{
		this.UpdateMeter((float)this.m_currentMinigame.m_complexity, (float)this.m_currentMinigame.m_maxComplexity);
		base.Step();
	}

	// Token: 0x06001A2C RID: 6700 RVA: 0x00122CC4 File Offset: 0x001210C4
	public void UpdateMeter(float _value, float _maxValue)
	{
		if (this.m_currentValue != _value)
		{
			float positionBetween = ToolBox.getPositionBetween(_value, 0f, _maxValue);
			this.m_fillSprite.SetHeight(positionBetween * 0.9f, RelativeTo.ParentHeight);
			this.m_currentValue = _value;
			this.m_fillSprite.Update();
		}
	}

	// Token: 0x04001C94 RID: 7316
	private UISprite m_fillSprite;

	// Token: 0x04001C95 RID: 7317
	private float m_currentValue;

	// Token: 0x04001C96 RID: 7318
	private Minigame m_currentMinigame;
}
