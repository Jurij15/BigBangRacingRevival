using System;
using System.Collections.Generic;
using FMOD.Studio;

// Token: 0x02000507 RID: 1287
public class CombineSound
{
	// Token: 0x04002AF2 RID: 10994
	public EventInstance eventInstance;

	// Token: 0x04002AF3 RID: 10995
	public List<SoundC> components;

	// Token: 0x04002AF4 RID: 10996
	public bool isPlaying;

	// Token: 0x04002AF5 RID: 10997
	public bool isPaused;

	// Token: 0x04002AF6 RID: 10998
	public bool isOutOfRange;

	// Token: 0x04002AF7 RID: 10999
	public float volume;

	// Token: 0x04002AF8 RID: 11000
	public float minDist;

	// Token: 0x04002AF9 RID: 11001
	public float maxDist;

	// Token: 0x04002AFA RID: 11002
	public SoundC nearestSoundC;
}
