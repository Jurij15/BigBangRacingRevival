using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200020E RID: 526
public class GenericSignalSequence
{
	// Token: 0x06000F46 RID: 3910 RVA: 0x00091043 File Offset: 0x0008F443
	public GenericSignalSequence(Array _signalSequenceOrder, string _material, int _channelCount, string _dialogIdentifier, Action _endSequenceCallback = null)
	{
		this.m_channelCount = _channelCount;
		this.m_endSequenceCallBack = _endSequenceCallback;
		this.m_tvIndex = 1;
		this.m_dialogIndex = 0;
		this.m_material = _material;
		this.m_dialogIdentifier = _dialogIdentifier;
		this.CreateSequenceOrder(_signalSequenceOrder);
	}

	// Token: 0x06000F47 RID: 3911 RVA: 0x0009107E File Offset: 0x0008F47E
	public GenericSignalSequence(string _material, int _channelCount, string _dialogIdentifier, Action _endSequenceCallback = null)
		: this(new int[] { 0, 1, 2, 3, 6 }, _material, _channelCount, _dialogIdentifier, _endSequenceCallback)
	{
	}

	// Token: 0x06000F48 RID: 3912 RVA: 0x0009109C File Offset: 0x0008F49C
	public GenericSignalSequence(string _material, string _dialogIdentifier, Action _endSequenceCallback = null)
		: this(new int[] { 1, 2, 3, 6 }, _material, 1, _dialogIdentifier, _endSequenceCallback)
	{
	}

	// Token: 0x06000F49 RID: 3913 RVA: 0x000910BC File Offset: 0x0008F4BC
	private void CreateSequenceOrder(Array order)
	{
		this.m_sequenceQueue = new Queue<Action>();
		IEnumerator enumerator = order.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				switch ((int)enumerator.Current)
				{
				case 0:
					this.m_sequenceQueue.Enqueue(new Action(this.ChannelNoise));
					continue;
				case 1:
					this.m_sequenceQueue.Enqueue(new Action(this.ChannelRoll));
					continue;
				case 2:
					this.m_sequenceQueue.Enqueue(new Action(this.ChannelStabile));
					continue;
				case 3:
					this.m_sequenceQueue.Enqueue(new Action(this.ChannelDialogue));
					continue;
				case 4:
					this.m_sequenceQueue.Enqueue(new Action(this.ChannelSignalLevel));
					continue;
				case 5:
					this.m_sequenceQueue.Enqueue(new Action(this.ChannelPlaySignalLevel));
					continue;
				}
				this.m_sequenceQueue.Enqueue(new Action(this.ChannelEndSignal));
			}
		}
		finally
		{
			IDisposable disposable;
			if ((disposable = enumerator as IDisposable) != null)
			{
				disposable.Dispose();
			}
		}
	}

	// Token: 0x06000F4A RID: 3914 RVA: 0x00091218 File Offset: 0x0008F618
	public void StartSequence()
	{
		if (this.m_channelCount > 0)
		{
			GameSceneEffectManager.CreateTVChannels(this.m_material, this.m_channelCount);
		}
		else
		{
			GameSceneEffectManager.CreateTVChannels("TVChannelFlicksMat_Material", 4);
		}
		GameSceneEffectManager.m_dialogue = PsMetagameData.GetDialogueByIdentifier(this.m_dialogIdentifier);
		GameSceneEffectManager.m_crew = new PsUICrewMemberBase(CameraS.m_uiCamera, string.Empty);
		GameSceneEffectManager.m_bubble = null;
		PsDialogueCharacterPosition characterPosition = GameSceneEffectManager.m_dialogue.m_steps[0].m_characterPosition;
		if (characterPosition == PsDialogueCharacterPosition.Left)
		{
			this.m_bubbleHandlePosition = SpeechBubbleHandlePosition.Left;
			this.m_bubbleHandleType = SpeechBubbleHandleType.SmallToLeft;
		}
		else
		{
			this.m_bubbleHandlePosition = SpeechBubbleHandlePosition.Right;
			this.m_bubbleHandleType = SpeechBubbleHandleType.SmallToRight;
		}
		GameSceneEffectManager.m_crew.CreateCharacter(GameSceneEffectManager.m_dialogue.m_steps[0].m_character, characterPosition, 1f);
		this.NextChannelAction();
		GameSceneEffectManager.StartMusic("/Cutscenes/Television", true);
	}

	// Token: 0x06000F4B RID: 3915 RVA: 0x000912F0 File Offset: 0x0008F6F0
	private void NextChannelAction(TimerC _c)
	{
		if (_c != null)
		{
			TimerS.RemoveComponent(_c);
		}
		if (this.m_sequenceQueue.Count > 0)
		{
			Action action = this.m_sequenceQueue.Dequeue();
			action.Invoke();
		}
		else
		{
			Debug.LogError("No more next actions for the SignalSequence");
		}
	}

	// Token: 0x06000F4C RID: 3916 RVA: 0x0009133B File Offset: 0x0008F73B
	private void NextChannelAction()
	{
		if (GameSceneEffectManager.m_bubble != null)
		{
			GameSceneEffectManager.m_bubble = null;
		}
		this.NextChannelAction(null);
	}

	// Token: 0x06000F4D RID: 3917 RVA: 0x00091354 File Offset: 0x0008F754
	private void ChannelNoise()
	{
		GameSceneEffectManager.StartMusic("/Cutscenes/Television", true);
		GameSceneEffectManager.SetChannel(0);
		TimerS.AddComponent(GameSceneEffectManager.m_utilityEntity, string.Empty, 0.4f, 0f, false, new TimerComponentDelegate(this.NextChannelAction));
	}

	// Token: 0x06000F4E RID: 3918 RVA: 0x00091390 File Offset: 0x0008F790
	private void ChannelRoll()
	{
		GameSceneEffectManager.SetChannel(this.m_tvIndex);
		GameSceneEffectManager.SetChannelRoll(true);
		this.m_tvIndex++;
		TimerS.AddComponent(GameSceneEffectManager.m_utilityEntity, string.Empty, 1f, 0f, false, new TimerComponentDelegate(this.NextChannelAction));
	}

	// Token: 0x06000F4F RID: 3919 RVA: 0x000913E3 File Offset: 0x0008F7E3
	private void ChannelStabile()
	{
		GameSceneEffectManager.SetChannelRoll(false);
		TimerS.AddComponent(GameSceneEffectManager.m_utilityEntity, string.Empty, 2f, 0f, false, new TimerComponentDelegate(this.NextChannelAction));
	}

	// Token: 0x06000F50 RID: 3920 RVA: 0x00091414 File Offset: 0x0008F814
	private void ChannelDialogue()
	{
		PsDialogueStep psDialogueStep = GameSceneEffectManager.m_dialogue.m_steps[this.m_dialogIndex];
		Action action = new Action(this.NextChannelAction);
		if (GameSceneEffectManager.m_crew != null)
		{
			GameSceneEffectManager.m_crew.Talk();
			GameSceneEffectManager.m_crew.LookAtCamera();
			if (GameSceneEffectManager.m_dialogue.m_steps.Count - 1 == this.m_dialogIndex)
			{
				action = (Action)Delegate.Combine(action, new Action(GameSceneEffectManager.m_crew.LookAwayFromCamera));
			}
		}
		this.m_dialogIndex++;
		GameSceneEffectManager.m_bubble = new PsUIFixedDialogueSpeechBubble(new Vector2((float)Screen.width * -0.2f, (float)Screen.height * -0.1f), psDialogueStep.m_charactertext, psDialogueStep.m_proceedText, psDialogueStep.m_cancelText, action, null, this.m_bubbleHandlePosition, this.m_bubbleHandleType, null);
	}

	// Token: 0x06000F51 RID: 3921 RVA: 0x000914F0 File Offset: 0x0008F8F0
	private void ChannelSignalLevel()
	{
		GameSceneEffectManager.SetChannel(-1);
		GameSceneEffectManager.SetChannelRoll(true);
		TimerS.AddComponent(GameSceneEffectManager.m_utilityEntity, string.Empty, 1f, 0f, false, new TimerComponentDelegate(this.NextChannelAction));
	}

	// Token: 0x06000F52 RID: 3922 RVA: 0x00091525 File Offset: 0x0008F925
	private void ChannelPlaySignalLevel()
	{
		GameSceneEffectManager.StartMusic("/Cutscenes/RacingAudience_AmbienceLoop", false);
		GameSceneEffectManager.SetEffects(false);
		GameSceneEffectManager.DestroyCrew();
		this.m_endSequenceCallBack.Invoke();
	}

	// Token: 0x06000F53 RID: 3923 RVA: 0x00091548 File Offset: 0x0008F948
	private void ChannelEndSignal()
	{
		if (this.m_endSequenceCallBack != null)
		{
			this.m_endSequenceCallBack.Invoke();
		}
		else
		{
			PsState.m_activeGameLoop.ExitMinigame();
		}
		GameSceneEffectManager.DestroyCrew();
	}

	// Token: 0x0400122A RID: 4650
	private int m_tvIndex;

	// Token: 0x0400122B RID: 4651
	private int m_dialogIndex;

	// Token: 0x0400122C RID: 4652
	private int m_channelCount;

	// Token: 0x0400122D RID: 4653
	private Action m_endSequenceCallBack;

	// Token: 0x0400122E RID: 4654
	private string m_material;

	// Token: 0x0400122F RID: 4655
	private string m_dialogIdentifier;

	// Token: 0x04001230 RID: 4656
	private SpeechBubbleHandlePosition m_bubbleHandlePosition;

	// Token: 0x04001231 RID: 4657
	private SpeechBubbleHandleType m_bubbleHandleType;

	// Token: 0x04001232 RID: 4658
	private Queue<Action> m_sequenceQueue;
}
