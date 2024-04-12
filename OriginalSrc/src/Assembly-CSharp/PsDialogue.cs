using System;
using System.Collections.Generic;

// Token: 0x02000138 RID: 312
public class PsDialogue
{
	// Token: 0x06000965 RID: 2405 RVA: 0x00064CF1 File Offset: 0x000630F1
	public PsDialogue(string _identifier, PsNodeEventTrigger _trigger)
	{
		this.m_identifier = _identifier;
		this.m_trigger = _trigger;
		this.m_steps = new List<PsDialogueStep>();
	}

	// Token: 0x06000966 RID: 2406 RVA: 0x00064D14 File Offset: 0x00063114
	public void AddStep(PsDialogueCharacter _character, PsDialogueCharacterPosition _position, StringID _characterTextID, StringID _proceedTextID)
	{
		PsDialogueStep psDialogueStep = new PsDialogueStep();
		psDialogueStep.m_character = _character;
		psDialogueStep.m_characterPosition = _position;
		psDialogueStep.m_characterTextLocalized = _characterTextID;
		psDialogueStep.m_proceedTextLocalized = _proceedTextID;
		psDialogueStep.m_charactertext = "HI THERE!";
		psDialogueStep.m_proceedText = "Well, Hello!";
		psDialogueStep.m_cancelText = string.Empty;
		this.m_steps.Add(psDialogueStep);
	}

	// Token: 0x040008D9 RID: 2265
	public string m_identifier;

	// Token: 0x040008DA RID: 2266
	public PsNodeEventTrigger m_trigger;

	// Token: 0x040008DB RID: 2267
	public List<PsDialogueStep> m_steps;
}
