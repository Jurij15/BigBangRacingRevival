using System;
using System.Collections;
using FMOD.Studio;

// Token: 0x020004CD RID: 1229
public class SoundC : BasicComponent
{
	// Token: 0x060022DB RID: 8923 RVA: 0x001911B0 File Offset: 0x0018F5B0
	public SoundC()
		: base(ComponentType.Sound)
	{
		SoundC.m_componentCount++;
	}

	// Token: 0x060022DC RID: 8924 RVA: 0x001911C8 File Offset: 0x0018F5C8
	public override void Reset()
	{
		base.Reset();
		this.isPlaying = false;
		this.isPaused = false;
		this.forceAtListenerPosition = false;
		this.eventInstance = null;
		this.parameterInstances = null;
		this.p_TC = null;
		this.name = null;
		this.isCombineSound = false;
		this.combineSoundKey = string.Empty;
	}

	// Token: 0x060022DD RID: 8925 RVA: 0x00191220 File Offset: 0x0018F620
	~SoundC()
	{
		SoundC.m_componentCount--;
	}

	// Token: 0x04002923 RID: 10531
	public static int m_componentCount;

	// Token: 0x04002924 RID: 10532
	public bool isCombineSound;

	// Token: 0x04002925 RID: 10533
	public string combineSoundKey;

	// Token: 0x04002926 RID: 10534
	public TransformC p_TC;

	// Token: 0x04002927 RID: 10535
	public string name;

	// Token: 0x04002928 RID: 10536
	public bool isPlaying;

	// Token: 0x04002929 RID: 10537
	public bool isPaused;

	// Token: 0x0400292A RID: 10538
	public bool forceAtListenerPosition;

	// Token: 0x0400292B RID: 10539
	public EventInstance eventInstance;

	// Token: 0x0400292C RID: 10540
	public Hashtable parameterInstances;
}
