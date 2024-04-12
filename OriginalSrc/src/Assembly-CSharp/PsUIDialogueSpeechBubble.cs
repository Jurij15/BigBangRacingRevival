using System;

// Token: 0x02000246 RID: 582
public abstract class PsUIDialogueSpeechBubble : UIVerticalList
{
	// Token: 0x060011AD RID: 4525 RVA: 0x000AA89C File Offset: 0x000A8C9C
	public PsUIDialogueSpeechBubble()
		: base(null, string.Empty)
	{
	}

	// Token: 0x060011AE RID: 4526 RVA: 0x000AA8AA File Offset: 0x000A8CAA
	public virtual void Proceed()
	{
		Debug.LogError("Override");
	}

	// Token: 0x060011AF RID: 4527 RVA: 0x000AA8B6 File Offset: 0x000A8CB6
	public virtual void ProceedCancel()
	{
		Debug.LogError("Override");
	}
}
