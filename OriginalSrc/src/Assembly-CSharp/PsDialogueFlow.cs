using System;
using UnityEngine;

// Token: 0x02000200 RID: 512
public class PsDialogueFlow
{
	// Token: 0x06000EFC RID: 3836 RVA: 0x0008E89E File Offset: 0x0008CC9E
	public PsDialogueFlow(PsGameLoop _gameLoop, PsNodeEventTrigger _dialogueTrigger, float _delay = 0f, Action _proceed = null)
	{
		PsState.m_dialogues.Add(this);
	}

	// Token: 0x06000EFD RID: 3837 RVA: 0x0008E8B1 File Offset: 0x0008CCB1
	protected virtual void StartDelayHandler(TimerC _c)
	{
		TimerS.RemoveComponent(_c);
		this.DialogueProceedAction();
	}

	// Token: 0x06000EFE RID: 3838 RVA: 0x0008E8BF File Offset: 0x0008CCBF
	protected virtual void ProceedDelayHandler(TimerC _c)
	{
		TimerS.RemoveComponent(_c);
		this.Proceed();
	}

	// Token: 0x06000EFF RID: 3839 RVA: 0x0008E8CD File Offset: 0x0008CCCD
	protected virtual void DialogueProceedAction()
	{
		Debug.LogWarning("OVERWRITE THIS");
	}

	// Token: 0x06000F00 RID: 3840 RVA: 0x0008E8DC File Offset: 0x0008CCDC
	public virtual void BubbleTouchHandler(TouchAreaC _touchArea, TouchAreaPhase _touchPhase, bool _touchIsSecondary, int _touchCount, TLTouch[] _touches)
	{
		if (_touchCount == 1 && (_touchPhase == TouchAreaPhase.ReleaseIn || (!_touchArea.m_allowPrimary && _touchPhase == TouchAreaPhase.RollIn)) && !_touches[0].m_dragged && this.m_bubble != null)
		{
			this.m_bubble.Proceed();
		}
	}

	// Token: 0x06000F01 RID: 3841 RVA: 0x0008E92E File Offset: 0x0008CD2E
	protected void Cancel()
	{
		this.Destroy();
		TouchAreaS.Enable();
		if (this.m_cancelAction != null)
		{
			this.m_cancelAction.Invoke();
		}
	}

	// Token: 0x06000F02 RID: 3842 RVA: 0x0008E951 File Offset: 0x0008CD51
	protected void Proceed()
	{
		this.Destroy();
		TouchAreaS.Enable();
		if (this.m_prodeedAction != null)
		{
			this.m_prodeedAction.Invoke();
		}
	}

	// Token: 0x06000F03 RID: 3843 RVA: 0x0008E974 File Offset: 0x0008CD74
	public void Destroy()
	{
		if (this.m_bubble != null)
		{
			this.m_bubble.Destroy();
			this.m_bubble = null;
		}
		if (this.m_crew != null)
		{
			this.m_crew.Destroy();
			this.m_crew = null;
		}
		if (this.m_camera != null)
		{
			CameraS.RemoveCamera(this.m_camera);
			this.m_camera = null;
		}
		if (this.m_canvas != null)
		{
			this.m_canvas.Destroy();
			this.m_canvas = null;
		}
		PsState.m_dialogues.Remove(this);
	}

	// Token: 0x040011D7 RID: 4567
	protected int m_dialogueIndex;

	// Token: 0x040011D8 RID: 4568
	protected PsDialogue m_dialogue;

	// Token: 0x040011D9 RID: 4569
	protected Action m_prodeedAction;

	// Token: 0x040011DA RID: 4570
	protected Action m_cancelAction;

	// Token: 0x040011DB RID: 4571
	protected UIScrollableCanvas m_canvas;

	// Token: 0x040011DC RID: 4572
	protected PsUICrewMemberBase m_crew;

	// Token: 0x040011DD RID: 4573
	public Camera m_camera;

	// Token: 0x040011DE RID: 4574
	protected PsUIDialogueSpeechBubble m_bubble;
}
