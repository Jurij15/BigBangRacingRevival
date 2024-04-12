using System;
using UnityEngine;

// Token: 0x0200010C RID: 268
public class PsGameLoopFreshForced : PsGameLoopFresh
{
	// Token: 0x060006F2 RID: 1778 RVA: 0x0004ECB6 File Offset: 0x0004D0B6
	public PsGameLoopFreshForced(PsMinigameMetaData _data)
		: base(_data)
	{
	}

	// Token: 0x060006F3 RID: 1779 RVA: 0x0004ECBF File Offset: 0x0004D0BF
	public PsGameLoopFreshForced(string _minigameId, PsPlanetPath _path)
		: base(_minigameId, _path)
	{
	}

	// Token: 0x060006F4 RID: 1780 RVA: 0x0004ECC9 File Offset: 0x0004D0C9
	public override int GetVisualLikes()
	{
		return 20 + Random.Range(0, 10);
	}

	// Token: 0x060006F5 RID: 1781 RVA: 0x0004ECD6 File Offset: 0x0004D0D6
	public override int GetVisualDislikes()
	{
		return 2 + Random.Range(0, 2);
	}
}
