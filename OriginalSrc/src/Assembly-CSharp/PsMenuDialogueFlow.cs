using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000204 RID: 516
public class PsMenuDialogueFlow : PsDialogueFlow
{
	// Token: 0x06000F0B RID: 3851 RVA: 0x0008ED04 File Offset: 0x0008D104
	public PsMenuDialogueFlow(PsGameLoop _gameLoop, PsNodeEventTrigger _dialogueTrigger, float _delay = 0f, Action _proceed = null, bool _hideShowUI = true)
		: base(_gameLoop, _dialogueTrigger, _delay, _proceed)
	{
		TouchAreaS.Disable();
		this.m_dialogueTrigger = _dialogueTrigger;
		this.m_hideShowUI = _hideShowUI;
		this.m_prodeedAction = _proceed;
		PsPlanetNode node = _gameLoop.m_node;
		bool flag = node == null || _gameLoop.m_nodeId == node.m_loop.m_nodeId || (_gameLoop.m_nodeId == node.m_loop.m_nodeId - 1 && !node.m_loop.m_activated);
		if (_gameLoop.m_dialogues != null && _gameLoop.m_dialogues.ContainsKey(_dialogueTrigger.ToString()) && flag)
		{
			string text = (string)_gameLoop.m_dialogues[_dialogueTrigger.ToString()];
			if (text == "unlock_motorcycle_planet")
			{
				if (_delay > 0f)
				{
					TimerS.AddComponent(PsMetagameManager.m_utilityEntity, "dialogueStartDelay", _delay, 0f, false, new TimerComponentDelegate(this.ProceedDelayHandler));
				}
				else
				{
					base.Proceed();
				}
				return;
			}
			if (text == "unlock_offroadcar_racing")
			{
				if (PlayerPrefsX.GetOffroadRacing())
				{
					if (_delay > 0f)
					{
						TimerS.AddComponent(PsMetagameManager.m_utilityEntity, "dialogueStartDelay", _delay, 0f, false, new TimerComponentDelegate(this.ProceedDelayHandler));
					}
					else
					{
						base.Proceed();
					}
					return;
				}
				this.m_prodeedAction = null;
				PlayerPrefsX.SetOffroadRacing(true);
			}
			else if (text == "unlock_freshandfree")
			{
				if (PlayerPrefsX.GetFreshAndFree())
				{
					base.Proceed();
					return;
				}
				PsFloaters.CreateFreshAndFreePlanetForced();
			}
			Debug.LogWarning(_dialogueTrigger);
			if (_dialogueTrigger == PsNodeEventTrigger.LoopClaimed)
			{
				bool flag2 = false;
				List<UIComponent> allUIComponents = PsMainMenuState.GetAllUIComponents();
				PsMainMenuState.HideUI(flag2, false, null, false, allUIComponents);
				this.m_prodeedAction = (Action)Delegate.Combine(this.m_prodeedAction, delegate
				{
					PsMainMenuState.ShowUI(true, PsMainMenuState.GetAllUIComponents());
				});
			}
			this.m_dialogue = PsMetagameData.GetDialogueByIdentifier(text);
			this.m_dialogueIndex = 0;
			this.m_canvas = new UIScrollableCanvas(null, string.Empty);
			this.m_canvas.m_TAC.d_TouchEventDelegate = new TouchEventDelegate(this.BubbleTouchHandler);
			this.m_canvas.RemoveDrawHandler();
			this.m_canvas.Update();
			this.m_camera = CameraS.AddCamera("DialogueCamera", true, 3);
			TimerS.AddComponent(PsMetagameManager.m_utilityEntity, "dialogueStartDelay", _delay, 0f, false, new TimerComponentDelegate(this.StartDelayHandler));
		}
		else if (_delay > 0f)
		{
			TimerS.AddComponent(PsMetagameManager.m_utilityEntity, "dialogueStartDelay", _delay, 0f, false, new TimerComponentDelegate(this.ProceedDelayHandler));
		}
		else
		{
			base.Proceed();
		}
	}

	// Token: 0x06000F0C RID: 3852 RVA: 0x0008EFD8 File Offset: 0x0008D3D8
	public PsMenuDialogueFlow(PsDialogue _dialogue, float _delay = 0f, Action _proceed = null, bool _hideShowUI = true, bool _dontShowUIOnProceed = false)
		: base(null, PsNodeEventTrigger.Manual, _delay, _proceed)
	{
		this.m_dontShowUIOnProceed = _dontShowUIOnProceed;
		this.m_hideShowUI = _hideShowUI;
		this.m_prodeedAction = _proceed;
		this.m_dialogue = _dialogue;
		this.m_dialogueIndex = 0;
		this.m_canvas = new UIScrollableCanvas(null, string.Empty);
		this.m_canvas.m_TAC.d_TouchEventDelegate = new TouchEventDelegate(this.BubbleTouchHandler);
		this.m_canvas.RemoveDrawHandler();
		this.m_canvas.Update();
		this.m_camera = CameraS.AddCamera("DialogueCamera", true, 3);
		TimerS.AddComponent(PsMetagameManager.m_utilityEntity, "dialogueStartDelay", _delay, 0f, false, new TimerComponentDelegate(this.StartDelayHandler));
	}

	// Token: 0x06000F0D RID: 3853 RVA: 0x0008F098 File Offset: 0x0008D498
	private void CreateCrewButtonDelegate(TweenC _c)
	{
		TouchAreaS.Enable();
		this.m_crew.Talk();
		this.m_crew.LookAtCamera();
		Vector3 vector;
		vector..ctor(5f, 20f, 0f);
		if (this.m_crew.m_character.m_character == PsDialogueCharacter.Architect)
		{
			vector..ctor(-5f, 20f, 0f);
		}
		Vector3 vector2 = this.m_crew.m_character.m_TC.transform.position + vector;
		Action action = null;
		if (this.m_dialogue.m_steps.Count - 1 == this.m_dialogueIndex)
		{
			action = (Action)Delegate.Combine(action, new Action(this.DialogueProceedAction));
		}
		else
		{
			action = new Action(this.DialogueProceedAction);
		}
		PsDialogueStep psDialogueStep = this.m_dialogue.m_steps[this.m_dialogueIndex];
		string text = ((psDialogueStep.m_characterTextLocalized == StringID.EMPTY) ? psDialogueStep.m_charactertext : PsStrings.Get(psDialogueStep.m_characterTextLocalized));
		string text2 = ((psDialogueStep.m_proceedTextLocalized == StringID.EMPTY) ? psDialogueStep.m_proceedText : PsStrings.Get(psDialogueStep.m_proceedTextLocalized));
		this.m_bubble = new PsUIFreeDialogueSpeechBubble(vector2, text, text2, psDialogueStep.m_cancelText, action, new Action(base.Proceed), this.m_camera);
		this.m_dialogueIndex++;
	}

	// Token: 0x06000F0E RID: 3854 RVA: 0x0008F211 File Offset: 0x0008D611
	private void DestroyScientiestButtonDelegate(TweenC _c)
	{
		if (this.m_crew != null)
		{
			this.m_crew.Destroy();
			this.m_crew = null;
		}
		base.Proceed();
	}

	// Token: 0x06000F0F RID: 3855 RVA: 0x0008F238 File Offset: 0x0008D638
	protected override void DialogueProceedAction()
	{
		this.m_bubble = null;
		if (this.m_dialogue == null || this.m_dialogue.m_steps == null)
		{
			Debug.LogError("dialogue error: " + this.m_dialogueTrigger);
			base.Proceed();
			return;
		}
		if (this.m_dialogue.m_steps.Count <= this.m_dialogueIndex)
		{
			if (this.m_crew != null)
			{
				Vector3 vector = -Vector3.right * (float)Screen.height * 0.35f;
				if (this.m_crew.m_character.m_character == PsDialogueCharacter.Architect)
				{
					vector = Vector3.right * (float)Screen.height * 0.35f;
				}
				TweenC tweenC = TweenS.AddTransformTween(this.m_crew.m_character.m_TC, TweenedProperty.Position, TweenStyle.ExpoInOut, this.m_crewOriginalPos + vector, 1f, 0f, true);
				TweenS.AddTweenEndEventListener(tweenC, new TweenEventDelegate(this.DestroyScientiestButtonDelegate));
				if (this.m_hideShowUI && !this.m_dontShowUIOnProceed)
				{
					PsMainMenuState.ShowUI(true, PsMainMenuState.GetAllUIComponents());
				}
			}
			else
			{
				base.Proceed();
			}
			return;
		}
		Action action = null;
		PsDialogueStep psDialogueStep = this.m_dialogue.m_steps[this.m_dialogueIndex];
		Vector3 vector2 = Vector3.zero;
		if (this.m_crew == null && this.m_hideShowUI)
		{
			bool flag = true;
			bool flag2 = true;
			List<UIComponent> allUIComponents = PsMainMenuState.GetAllUIComponents();
			PsMainMenuState.HideUI(flag, flag2, null, false, allUIComponents);
		}
		if (psDialogueStep.m_character == PsDialogueCharacter.Scientist)
		{
			if (this.m_crew == null)
			{
				this.m_crew = new PsUICrewMemberBase(this.m_camera, string.Empty);
				this.m_crew.SetDepthOffset(-20f);
				this.m_crew.CreateCharacter(PsDialogueCharacter.Scientist, PsDialogueCharacterPosition.Left, 1f);
				this.m_crewOriginalPos = this.m_crew.m_character.m_TC.transform.localPosition;
				TweenC tweenC2 = TweenS.AddTransformTween(this.m_crew.m_character.m_TC, TweenedProperty.Position, TweenStyle.ExpoInOut, this.m_crewOriginalPos - Vector3.right * (float)Screen.height * 0.35f, this.m_crewOriginalPos, 1f, 0f, true);
				TweenS.AddTweenEndEventListener(tweenC2, new TweenEventDelegate(this.CreateCrewButtonDelegate));
				return;
			}
			vector2 = this.m_crew.m_character.m_TC.transform.position + new Vector3(5f, 20f, 0f);
			this.m_crew.Talk();
			this.m_crew.LookAtCamera();
		}
		else if (psDialogueStep.m_character == PsDialogueCharacter.Mechanic)
		{
			if (this.m_crew == null)
			{
				this.m_crew = new PsUICrewMemberBase(this.m_camera, string.Empty);
				this.m_crew.SetDepthOffset(-20f);
				this.m_crew.CreateCharacter(PsDialogueCharacter.Mechanic, PsDialogueCharacterPosition.Left, 1f);
				this.m_crewOriginalPos = this.m_crew.m_character.m_TC.transform.localPosition;
				TweenC tweenC3 = TweenS.AddTransformTween(this.m_crew.m_character.m_TC, TweenedProperty.Position, TweenStyle.ExpoInOut, this.m_crewOriginalPos - Vector3.right * (float)Screen.height * 0.35f, this.m_crewOriginalPos, 1f, 0f, true);
				TweenS.AddTweenEndEventListener(tweenC3, new TweenEventDelegate(this.CreateCrewButtonDelegate));
				return;
			}
			vector2 = this.m_crew.m_character.m_TC.transform.position + new Vector3(5f, 20f, 0f);
			this.m_crew.Talk();
			this.m_crew.LookAtCamera();
		}
		else if (psDialogueStep.m_character == PsDialogueCharacter.Architect)
		{
			if (this.m_crew == null)
			{
				this.m_crew = new PsUICrewMemberBase(this.m_camera, string.Empty);
				this.m_crew.SetDepthOffset(-20f);
				this.m_crew.CreateCharacter(PsDialogueCharacter.Architect, PsDialogueCharacterPosition.Right, 1f);
				this.m_crewOriginalPos = this.m_crew.m_character.m_TC.transform.localPosition;
				TweenC tweenC4 = TweenS.AddTransformTween(this.m_crew.m_character.m_TC, TweenedProperty.Position, TweenStyle.ExpoInOut, this.m_crewOriginalPos + Vector3.right * (float)Screen.height * 0.35f, this.m_crewOriginalPos, 1f, 0f, true);
				TweenS.AddTweenEndEventListener(tweenC4, new TweenEventDelegate(this.CreateCrewButtonDelegate));
				return;
			}
			vector2 = this.m_crew.m_character.m_TC.transform.position + new Vector3(-5f, 20f, 0f);
			this.m_crew.Talk();
			this.m_crew.LookAtCamera();
		}
		if (this.m_dialogue.m_steps.Count - 1 == this.m_dialogueIndex)
		{
			action = (Action)Delegate.Combine(action, new Action(this.DialogueProceedAction));
		}
		else
		{
			action = new Action(this.DialogueProceedAction);
		}
		string text = ((psDialogueStep.m_characterTextLocalized == StringID.EMPTY) ? psDialogueStep.m_charactertext : PsStrings.Get(psDialogueStep.m_characterTextLocalized));
		string text2 = ((psDialogueStep.m_proceedTextLocalized == StringID.EMPTY) ? psDialogueStep.m_proceedText : PsStrings.Get(psDialogueStep.m_proceedTextLocalized));
		this.m_bubble = new PsUIFreeDialogueSpeechBubble(vector2, text, text2, psDialogueStep.m_cancelText, action, new Action(base.Proceed), null);
		this.m_bubble.SetCamera(this.m_camera, true, false);
		this.m_dialogueIndex++;
	}

	// Token: 0x040011E7 RID: 4583
	private bool m_hideShowUI = true;

	// Token: 0x040011E8 RID: 4584
	private PsNodeEventTrigger m_dialogueTrigger;

	// Token: 0x040011E9 RID: 4585
	private bool m_dontShowUIOnProceed;

	// Token: 0x040011EA RID: 4586
	public Vector3 m_crewOriginalPos;
}
