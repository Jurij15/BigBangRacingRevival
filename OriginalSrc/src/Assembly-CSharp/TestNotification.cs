using System;
using UnityEngine;

// Token: 0x020003BF RID: 959
public class TestNotification : Notification
{
	// Token: 0x06001B57 RID: 6999 RVA: 0x001313A2 File Offset: 0x0012F7A2
	public TestNotification(string _text)
	{
		this.m_text = _text;
	}

	// Token: 0x06001B58 RID: 7000 RVA: 0x001313B4 File Offset: 0x0012F7B4
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
		this.m_camera.depth = 2f;
		this.m_canvas = new UICanvas(this, true, "Notification", null, string.Empty);
		this.m_canvas.SetSize(0.5f, 0.1f, RelativeTo.ScreenHeight);
		this.m_canvas.SetDrawHandler(new UIDrawDelegate(PsUIPopup.PopupDrawHandler));
		new UIFittedText(this.m_canvas, false, string.Empty, this.m_text, PsFontManager.GetFont(PsFonts.HurmeRegular), true, null, null);
		this.m_tween = TweenS.AddTransformTween(this.m_canvas.m_TC, TweenedProperty.Position, TweenStyle.CubicInOut, this.m_canvas.m_TC.transform.localPosition - new Vector3(0f, 0.1f * (float)Screen.height, 0f), this.m_canvas.m_TC.transform.localPosition, 0.5f, 0f, false);
		this.Update();
	}

	// Token: 0x06001B59 RID: 7001 RVA: 0x00131510 File Offset: 0x0012F910
	public override void End()
	{
		this.Destroy();
		base.End();
	}

	// Token: 0x06001B5A RID: 7002 RVA: 0x00131520 File Offset: 0x0012F920
	public override void Step()
	{
		if (this.m_started != 0.0 && this.m_tween != null && this.m_tween.hasFinished && this.m_started + this.m_duration - Main.m_gameTimeSinceAppStarted <= 0.5 && !this.m_turned)
		{
			this.m_turned = true;
			TweenS.RemoveComponent(this.m_tween);
			this.m_tween = TweenS.AddTransformTween(this.m_canvas.m_TC, TweenedProperty.Position, TweenStyle.CubicInOut, this.m_canvas.m_TC.transform.localPosition, this.m_canvas.m_TC.transform.localPosition - new Vector3(0f, 0.1f * (float)Screen.height, 0f), 0.5f, 0f, false);
		}
		base.Step();
	}

	// Token: 0x06001B5B RID: 7003 RVA: 0x0013160D File Offset: 0x0012FA0D
	public override void Destroy()
	{
		if (this.m_tween != null)
		{
			TweenS.RemoveComponent(this.m_tween);
			this.m_tween = null;
		}
		base.Destroy();
	}

	// Token: 0x04001DB6 RID: 7606
	private string m_text;

	// Token: 0x04001DB7 RID: 7607
	private UICanvas m_canvas;

	// Token: 0x04001DB8 RID: 7608
	private TweenC m_tween;

	// Token: 0x04001DB9 RID: 7609
	private bool m_turned;
}
