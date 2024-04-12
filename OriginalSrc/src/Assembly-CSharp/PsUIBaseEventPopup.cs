using System;
using UnityEngine;

// Token: 0x02000292 RID: 658
public abstract class PsUIBaseEventPopup : UICanvas
{
	// Token: 0x060013C4 RID: 5060 RVA: 0x000C5F78 File Offset: 0x000C4378
	public PsUIBaseEventPopup(UIComponent _parent, string _tag, EventMessage _event)
		: base(_parent, false, _tag, null, string.Empty)
	{
		this.m_event = _event;
		if (this.m_event != null)
		{
			this.m_duration = PsMetagameManager.GetHoursFromSeconds((long)(this.m_event.localEndTime - this.m_event.localStartTime), 0);
			if (this.m_event.timeToStart >= 0)
			{
				this.m_timeLeft = this.m_event.timeToStart;
			}
			else
			{
				this.m_hasStarted = true;
				this.m_timeLeft = this.m_event.timeLeft;
			}
			if (this.m_event.timeLeft <= 0)
			{
				this.m_hasEnded = true;
			}
		}
		else
		{
			this.m_timeLeft = 0;
			this.m_hasStarted = true;
			this.m_hasEnded = true;
			this.m_duration = 0f;
		}
		this.RemoveDrawHandler();
		this.m_topColor = Color.white;
		this.m_bottomColor = Color.white;
		this.m_mainContainer = new UICanvas(this, false, string.Empty, null, string.Empty);
		this.m_mainContainer.SetVerticalAlign(1f);
		this.m_mainContainer.SetSize(0.93f, 0.8f, RelativeTo.ParentHeight);
		this.m_mainContainer.SetDrawHandler(new UIDrawDelegate(this.EventDrawhandler));
		this.m_header = new UICanvas(this.m_mainContainer, false, string.Empty, null, string.Empty);
		this.m_header.SetHeight(0.25f, RelativeTo.ParentHeight);
		this.m_header.SetVerticalAlign(1f);
		this.m_header.RemoveDrawHandler();
		this.SetRightHeaderContent();
		this.m_playButtonParent = new UICanvas(this.m_mainContainer, false, string.Empty, null, string.Empty);
		this.m_playButtonParent.SetHeight(0.12f, RelativeTo.ScreenHeight);
		this.m_playButtonParent.SetVerticalAlign(0f);
		this.m_playButtonParent.SetMargins(0f, 0f, 0.5f, -0.5f, RelativeTo.OwnHeight);
		this.m_playButtonParent.RemoveDrawHandler();
		this.SetRightButton();
		this.m_mainContent = new UICanvas(this.m_mainContainer, false, string.Empty, null, string.Empty);
		this.m_mainContent.SetVerticalAlign(0f);
		this.m_mainContent.SetHeight(0.75f, RelativeTo.ParentHeight);
		this.m_mainContent.SetMargins(0.1f, RelativeTo.OwnHeight);
		this.m_mainContent.RemoveDrawHandler();
		this.SetAboutContainer();
		this.m_bottomContainer = new UICanvas(this.m_mainContent, false, string.Empty, null, string.Empty);
		this.m_bottomContainer.SetVerticalAlign(0f);
		this.m_bottomContainer.SetHeight(0.5f, RelativeTo.ParentHeight);
		this.m_bottomContainer.SetDrawHandler(new UIDrawDelegate(this.EventDrawhandler));
		this.SetRightBottomContent();
		this.SetRightFooter();
	}

	// Token: 0x060013C5 RID: 5061 RVA: 0x000C623A File Offset: 0x000C463A
	public void SetTopColor(Color _color)
	{
		this.m_topColor = _color;
	}

	// Token: 0x060013C6 RID: 5062 RVA: 0x000C6243 File Offset: 0x000C4643
	public void SetBottomColor(Color _color)
	{
		this.m_bottomColor = _color;
	}

	// Token: 0x060013C7 RID: 5063 RVA: 0x000C624C File Offset: 0x000C464C
	public void SetColor(Color _topColor, Color _bottomColor)
	{
		this.SetTopColor(_topColor);
		this.SetBottomColor(_bottomColor);
	}

	// Token: 0x060013C8 RID: 5064 RVA: 0x000C625C File Offset: 0x000C465C
	public void SetAboutText(string _text)
	{
		this.m_aboutText.SetText(_text);
	}

	// Token: 0x060013C9 RID: 5065 RVA: 0x000C626A File Offset: 0x000C466A
	protected virtual void SetRightHeaderContent()
	{
	}

	// Token: 0x060013CA RID: 5066 RVA: 0x000C626C File Offset: 0x000C466C
	protected virtual void SetAboutContainer()
	{
		this.m_aboutText = new UITextbox(this.m_mainContent, false, string.Empty, string.Empty, PsFontManager.GetFont(PsFonts.HurmeBold), 0.03f, RelativeTo.ScreenHeight, true, Align.Center, Align.Top, null, true, null);
		this.m_aboutText.SetHeight(0.5f, RelativeTo.ParentHeight);
		this.m_aboutText.SetWidth(0.9f, RelativeTo.ParentWidth);
		this.m_aboutText.SetVerticalAlign(1f);
	}

	// Token: 0x060013CB RID: 5067 RVA: 0x000C62D9 File Offset: 0x000C46D9
	protected virtual void SetRightButton()
	{
	}

	// Token: 0x060013CC RID: 5068 RVA: 0x000C62DB File Offset: 0x000C46DB
	protected virtual void SetRightBottomContent()
	{
	}

	// Token: 0x060013CD RID: 5069 RVA: 0x000C62DD File Offset: 0x000C46DD
	protected virtual void SetRightFooter()
	{
	}

	// Token: 0x060013CE RID: 5070 RVA: 0x000C62DF File Offset: 0x000C46DF
	protected virtual void UpdateRightContent()
	{
		this.SetRightHeaderContent();
		this.SetRightBottomContent();
		this.SetRightButton();
		this.SetRightFooter();
		this.SetAboutContainer();
		this.Update();
	}

	// Token: 0x060013CF RID: 5071
	protected abstract void PlayButtonPressed();

	// Token: 0x060013D0 RID: 5072
	protected abstract void UpdateEventInfo();

	// Token: 0x060013D1 RID: 5073 RVA: 0x000C6308 File Offset: 0x000C4708
	public override void Step()
	{
		if (this.m_playButton != null && this.m_playButton.m_hit)
		{
			if (PlayerPrefsX.GetNameChanged())
			{
				this.PlayButtonPressed();
			}
			else
			{
				PsUserNameInputState psUserNameInputState = new PsUserNameInputState();
				psUserNameInputState.m_lastState = Main.m_currentGame.m_currentScene.GetCurrentState();
				psUserNameInputState.m_continueAction = new Action(this.PlayButtonPressed);
				PsMenuScene.m_lastIState = psUserNameInputState;
				PsMenuScene.m_lastState = null;
				Main.m_currentGame.m_currentScene.m_stateMachine.ChangeState(psUserNameInputState);
			}
		}
		if (this.m_event != null)
		{
			this.UpdateEventInfo();
		}
		base.Step();
	}

	// Token: 0x060013D2 RID: 5074 RVA: 0x000C63AC File Offset: 0x000C47AC
	protected virtual void EventDrawhandler(UIComponent _c)
	{
		_c.m_TC.transform.localScale = Vector3.one;
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, false);
		Vector2[] roundedRect = DebugDraw.GetRoundedRect(_c.m_actualWidth, _c.m_actualHeight, _c.m_actualHeight * 0.02f, 15, Vector2.zero);
		Color black = Color.black;
		black.a = 0.35f;
		GGData ggdata = new GGData(roundedRect);
		PrefabS.CreateFlatPrefabComponentsFromPolygon(_c.m_TC, Vector3.forward, ggdata, black, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.zero, roundedRect, (float)Screen.height * 0.009f, this.m_bottomColor, this.m_topColor, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line8Mat_Material), _c.m_camera, Position.Center, true);
	}

	// Token: 0x04001689 RID: 5769
	protected UIFittedText m_timeleftText;

	// Token: 0x0400168A RID: 5770
	protected UICanvas m_mainContainer;

	// Token: 0x0400168B RID: 5771
	protected UICanvas m_header;

	// Token: 0x0400168C RID: 5772
	protected UICanvas m_mainContent;

	// Token: 0x0400168D RID: 5773
	protected UICanvas m_bottomContainer;

	// Token: 0x0400168E RID: 5774
	protected UICanvas m_playButtonParent;

	// Token: 0x0400168F RID: 5775
	protected UITextbox m_aboutText;

	// Token: 0x04001690 RID: 5776
	protected PsUIGenericButton m_playButton;

	// Token: 0x04001691 RID: 5777
	protected EventMessage m_event;

	// Token: 0x04001692 RID: 5778
	protected int m_timeLeft;

	// Token: 0x04001693 RID: 5779
	protected bool m_hasStarted;

	// Token: 0x04001694 RID: 5780
	protected bool m_hasEnded;

	// Token: 0x04001695 RID: 5781
	public Color m_topColor;

	// Token: 0x04001696 RID: 5782
	public Color m_bottomColor;

	// Token: 0x04001697 RID: 5783
	protected float m_duration;
}
