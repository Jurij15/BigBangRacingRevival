using System;
using UnityEngine;

// Token: 0x020001A2 RID: 418
public class AnimationEventHelper : MonoBehaviour
{
	// Token: 0x06000D20 RID: 3360 RVA: 0x0007DAD4 File Offset: 0x0007BED4
	private void Start()
	{
		this.bossScript = base.transform.parent.GetComponent<EffectCheckpointBoss>();
	}

	// Token: 0x06000D21 RID: 3361 RVA: 0x0007DAEC File Offset: 0x0007BEEC
	private void CallExternalScript()
	{
		this.bossScript.ExplodeSkull();
	}

	// Token: 0x04000E74 RID: 3700
	public EffectCheckpointBoss bossScript;
}
