using System;
using UnityEngine;

// Token: 0x02000205 RID: 517
public class PsMinigameDialogueFlow : PsDialogueFlow
{
	// Token: 0x06000F11 RID: 3857 RVA: 0x0008F814 File Offset: 0x0008DC14
	public PsMinigameDialogueFlow(PsGameLoop _gameLoop, PsNodeEventTrigger _dialogueTrigger, float _delay, Action<string> _proceed, Action<string> _cancel)
		: base(_gameLoop, _dialogueTrigger, _delay, null)
	{
		PsMinigameDialogueFlow $this = this;
		this.m_cancelAction = delegate
		{
			_cancel.Invoke($this.GetDialogueIdentifier());
		};
		this.m_prodeedAction = delegate
		{
			_proceed.Invoke($this.GetDialogueIdentifier());
		};
		this.m_gameLoop = _gameLoop;
		this.m_dialogueTrigger = _dialogueTrigger;
		this.HandleTriggers(_delay);
	}

	// Token: 0x06000F12 RID: 3858 RVA: 0x0008F881 File Offset: 0x0008DC81
	public PsMinigameDialogueFlow(PsGameLoop _gameLoop, PsNodeEventTrigger _dialogueTrigger, float _delay = 0f, Action _proceed = null, Action _cancel = null)
		: base(_gameLoop, _dialogueTrigger, _delay, _proceed)
	{
		this.m_gameLoop = _gameLoop;
		this.m_cancelAction = _cancel;
		this.m_prodeedAction = _proceed;
		this.m_dialogueTrigger = _dialogueTrigger;
		this.HandleTriggers(_delay);
	}

	// Token: 0x06000F13 RID: 3859 RVA: 0x0008F8B4 File Offset: 0x0008DCB4
	public PsMinigameDialogueFlow(PsDialogue _dialogue, float _delay = 0f, Action _proceed = null, Action _cancel = null)
		: base(null, PsNodeEventTrigger.Manual, _delay, _proceed)
	{
		this.m_cancelAction = _cancel;
		this.m_prodeedAction = _proceed;
		this.m_dialogue = _dialogue;
		this.m_dialogueIndex = 0;
		TimerS.AddComponent(PsMetagameManager.m_utilityEntity, "dialogueStartDelay", _delay, 0f, false, new TimerComponentDelegate(this.StartDelayHandler));
		this.m_canvas = new UIScrollableCanvas(null, string.Empty);
		this.m_canvas.m_TAC.d_TouchEventDelegate = new TouchEventDelegate(this.BubbleTouchHandler);
		this.m_canvas.RemoveDrawHandler();
		this.m_canvas.Update();
		this.m_camera = CameraS.AddCamera("DialogueCamera", true, 3);
		this.m_crew = new PsUICrewMemberBase(this.m_camera, string.Empty);
	}

	// Token: 0x06000F14 RID: 3860 RVA: 0x0008F978 File Offset: 0x0008DD78
	public string GetDialogueIdentifier()
	{
		return (this.m_dialogue == null) ? string.Empty : this.m_dialogue.m_identifier;
	}

	// Token: 0x06000F15 RID: 3861 RVA: 0x0008F99C File Offset: 0x0008DD9C
	public void HandleTriggers(float _delay)
	{
		if (this.m_dialogueTrigger == PsNodeEventTrigger.MinigameWin && PsState.m_activeMinigame.m_playerReachedGoalCount > 1)
		{
			base.Proceed();
			return;
		}
		if (this.m_dialogueTrigger == PsNodeEventTrigger.MinigameLose && this.m_gameLoop.m_dialogues != null && this.m_gameLoop.m_dialogues.ContainsKey(PsNodeEventTrigger.MinigameLoseFirstTime.ToString()) && PsState.m_activeMinigame.m_gameDeathCount == 1)
		{
			this.m_dialogueTrigger = PsNodeEventTrigger.MinigameLoseFirstTime;
		}
		if (this.m_gameLoop.m_dialogues != null && this.m_gameLoop.m_dialogues.ContainsKey(this.m_dialogueTrigger.ToString()))
		{
			Debug.LogWarning(this.m_dialogueTrigger);
			this.m_dialogue = PsMetagameData.GetDialogueByIdentifier((string)this.m_gameLoop.m_dialogues[this.m_dialogueTrigger.ToString()]);
			this.m_dialogueIndex = 0;
			TimerS.AddComponent(PsMetagameManager.m_utilityEntity, "dialogueStartDelay", _delay, 0f, false, new TimerComponentDelegate(this.StartDelayHandler));
			this.m_canvas = new UIScrollableCanvas(null, string.Empty);
			this.m_canvas.m_TAC.d_TouchEventDelegate = new TouchEventDelegate(this.BubbleTouchHandler);
			this.m_canvas.RemoveDrawHandler();
			this.m_canvas.Update();
			this.m_camera = CameraS.AddCamera("DialogueCamera", true, 3);
			this.m_crew = new PsUICrewMemberBase(this.m_camera, string.Empty);
		}
		else
		{
			base.Proceed();
		}
	}

	// Token: 0x06000F16 RID: 3862 RVA: 0x0008FB3C File Offset: 0x0008DF3C
	protected override void DialogueProceedAction()
	{
		this.m_bubble = null;
		if (this.m_dialogue == null || this.m_dialogue.m_steps == null || this.m_dialogue.m_steps.Count <= this.m_dialogueIndex)
		{
			Debug.LogWarning("dialogue error: " + this.m_dialogueTrigger);
			base.Proceed();
			return;
		}
		PsDialogueStep psDialogueStep = this.m_dialogue.m_steps[this.m_dialogueIndex];
		SpeechBubbleHandlePosition speechBubbleHandlePosition = SpeechBubbleHandlePosition.Left;
		if (psDialogueStep.m_character == PsDialogueCharacter.Mechanic)
		{
			this.m_crew.CreateCharacter(PsDialogueCharacter.Mechanic, PsDialogueCharacterPosition.Left, 1f);
		}
		else if (psDialogueStep.m_character == PsDialogueCharacter.Scientist)
		{
			this.m_crew.CreateCharacter(PsDialogueCharacter.Scientist, PsDialogueCharacterPosition.Left, 1f);
		}
		else if (psDialogueStep.m_character == PsDialogueCharacter.Architect)
		{
			this.m_crew.CreateCharacter(PsDialogueCharacter.Architect, PsDialogueCharacterPosition.Right, 1f);
			speechBubbleHandlePosition = SpeechBubbleHandlePosition.Right;
		}
		this.m_crew.Talk();
		this.m_crew.LookAtCamera();
		Action action = new Action(this.DialogueProceedAction);
		if (this.m_dialogue.m_steps.Count - 1 == this.m_dialogueIndex)
		{
			action = (Action)Delegate.Combine(action, new Action(this.m_crew.LookAwayFromCamera));
		}
		string text = ((psDialogueStep.m_characterTextLocalized == StringID.EMPTY) ? psDialogueStep.m_charactertext : PsStrings.Get(psDialogueStep.m_characterTextLocalized));
		string text2 = ((psDialogueStep.m_proceedTextLocalized == StringID.EMPTY) ? psDialogueStep.m_proceedText : PsStrings.Get(psDialogueStep.m_proceedTextLocalized));
		Vector2 vector = new Vector2((float)Screen.width * -0.2f, (float)Screen.height * -0.1f);
		string text3 = text;
		string text4 = text2;
		string cancelText = psDialogueStep.m_cancelText;
		Action action2 = action;
		Action action3 = new Action(base.Cancel);
		SpeechBubbleHandlePosition speechBubbleHandlePosition2 = speechBubbleHandlePosition;
		Camera camera = this.m_camera;
		this.m_bubble = new PsUIFixedDialogueSpeechBubble(vector, text3, text4, cancelText, action2, action3, speechBubbleHandlePosition2, SpeechBubbleHandleType.SmallToLeft, camera);
		this.m_dialogueIndex++;
	}

	// Token: 0x040011EC RID: 4588
	private PsNodeEventTrigger m_dialogueTrigger;

	// Token: 0x040011ED RID: 4589
	private PsGameLoop m_gameLoop;
}
