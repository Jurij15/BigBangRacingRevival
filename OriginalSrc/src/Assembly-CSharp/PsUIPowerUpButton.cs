using System;

// Token: 0x0200021E RID: 542
public class PsUIPowerUpButton : UICanvas
{
	// Token: 0x06000FA9 RID: 4009 RVA: 0x00093DDC File Offset: 0x000921DC
	public PsUIPowerUpButton(UIComponent _parent)
		: base(_parent, true, "powerup", null, string.Empty)
	{
		this.m_TAC.m_allowSecondary = true;
		this.m_TAC.m_allowPrimary = false;
		this.m_TAC.m_cancelOtherTouches = false;
		this.RemoveDrawHandler();
		PsPowerUp powerUp = (PsState.m_activeMinigame.m_playerUnit as Vehicle).m_powerUp;
		this.SetUI(powerUp, true);
	}

	// Token: 0x06000FAA RID: 4010 RVA: 0x00093E50 File Offset: 0x00092250
	public void SetUI(PsPowerUp _powerup, bool _update = true)
	{
		if (_powerup != null)
		{
			if (this.m_sprite != null)
			{
				this.m_sprite.Destroy();
				this.m_sprite = null;
			}
			this.m_sprite = new UIFittedSprite(this, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame(this.m_baseImage, null), true, true);
			string name = _powerup.GetName();
			UIFittedText uifittedText = new UIFittedText(this.m_sprite, false, string.Empty, name, PsFontManager.GetFont(PsFonts.HurmeSemiBold), true, "#000000", null);
		}
		else if (this.m_sprite != null)
		{
			this.m_sprite.Destroy();
			this.m_sprite = null;
		}
		if (_update)
		{
			this.Update();
		}
	}

	// Token: 0x06000FAB RID: 4011 RVA: 0x00093F04 File Offset: 0x00092304
	public override void Step()
	{
		if (this.m_hit)
		{
		}
		base.Step();
	}

	// Token: 0x04001253 RID: 4691
	private PsPowerUp m_powerup;

	// Token: 0x04001254 RID: 4692
	private UIFittedSprite m_sprite;

	// Token: 0x04001255 RID: 4693
	private string m_baseImage = "menu_cp_image_signal";
}
