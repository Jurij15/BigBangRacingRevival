using System;
using UnityEngine;

// Token: 0x020003C0 RID: 960
public class PsAchievementNotification : Notification
{
	// Token: 0x06001B5C RID: 7004 RVA: 0x00131632 File Offset: 0x0012FA32
	public PsAchievementNotification(string _name, string _description, string _icon)
	{
		this.m_icon = _icon;
		this.m_name = _name;
		this.m_description = _description;
	}

	// Token: 0x06001B5D RID: 7005 RVA: 0x00131650 File Offset: 0x0012FA50
	public override void Start()
	{
		base.Start();
		this.m_duration = 2.0;
		this.m_turned = false;
		this.SetSize(0.5f, 0.1f, RelativeTo.ScreenHeight);
		this.RemoveDrawHandler();
		this.SetMargins(0f, RelativeTo.ScreenHeight);
		this.SetAlign(0.5f, 0f);
		this.RemoveTouchAreas();
		this.m_canvas = new UICanvas(this, true, "Notification", null, string.Empty);
		this.m_canvas.SetSize(0.5f, 0.1f, RelativeTo.ScreenHeight);
		this.m_canvas.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.LeagueBackgroundActive));
		this.CreateUI(this.m_canvas);
		this.m_tween = TweenS.AddTransformTween(this.m_canvas.m_TC, TweenedProperty.Position, TweenStyle.CubicInOut, this.m_canvas.m_TC.transform.localPosition - new Vector3(0f, 0.1f * (float)Screen.height, 0f), this.m_canvas.m_TC.transform.localPosition, 0.5f, 0f, false);
		this.m_tween.useUnscaledDeltaTime = true;
		this.Update();
	}

	// Token: 0x06001B5E RID: 7006 RVA: 0x00131794 File Offset: 0x0012FB94
	public void CreateUI(UIComponent _parent)
	{
		UIText uitext = new UIText(_parent, false, string.Empty, "Achievement completed!", PsFontManager.GetFont(PsFonts.HurmeBold), 0.02f, RelativeTo.ScreenHeight, null, null);
		uitext.SetVerticalAlign(1f);
		UICanvas uicanvas = new UICanvas(_parent, false, string.Empty, null, string.Empty);
		uicanvas.SetWidth(0.8f, RelativeTo.ParentWidth);
		uicanvas.SetHeight(0.7f, RelativeTo.ParentHeight);
		uicanvas.SetVerticalAlign(0f);
		UIFittedText uifittedText = new UIFittedText(uicanvas, false, string.Empty, this.m_name, PsFontManager.GetFont(PsFonts.HurmeBold), true, null, null);
		UIFittedSprite uifittedSprite = new UIFittedSprite(_parent, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame(this.m_icon, null), true, true);
		uifittedSprite.SetHeight(1f, RelativeTo.ParentHeight);
		uifittedSprite.SetHorizontalAlign(1f);
	}

	// Token: 0x06001B5F RID: 7007 RVA: 0x0013185F File Offset: 0x0012FC5F
	public override void End()
	{
		this.Destroy();
		base.End();
	}

	// Token: 0x06001B60 RID: 7008 RVA: 0x00131870 File Offset: 0x0012FC70
	public override void Step()
	{
		if (this.m_started != 0.0 && this.m_tween != null && this.m_tween.hasFinished && this.m_started + this.m_duration - Main.m_gameTimeSinceAppStarted <= 0.5 && !this.m_turned)
		{
			this.m_turned = true;
			TweenS.RemoveComponent(this.m_tween);
			this.m_tween = TweenS.AddTransformTween(this.m_canvas.m_TC, TweenedProperty.Position, TweenStyle.CubicInOut, this.m_canvas.m_TC.transform.localPosition, this.m_canvas.m_TC.transform.localPosition - new Vector3(0f, 0.1f * (float)Screen.height, 0f), 0.5f, 0f, false);
			this.m_tween.useUnscaledDeltaTime = true;
		}
		base.Step();
	}

	// Token: 0x06001B61 RID: 7009 RVA: 0x00131969 File Offset: 0x0012FD69
	public override void Destroy()
	{
		if (this.m_tween != null)
		{
			TweenS.RemoveComponent(this.m_tween);
			this.m_tween = null;
		}
		base.Destroy();
	}

	// Token: 0x04001DBB RID: 7611
	private string m_name;

	// Token: 0x04001DBC RID: 7612
	private string m_description;

	// Token: 0x04001DBD RID: 7613
	private string m_icon;

	// Token: 0x04001DBE RID: 7614
	private UICanvas m_canvas;

	// Token: 0x04001DBF RID: 7615
	private TweenC m_tween;

	// Token: 0x04001DC0 RID: 7616
	private bool m_turned;
}
