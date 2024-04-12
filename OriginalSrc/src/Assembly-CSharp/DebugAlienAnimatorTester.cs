using System;
using UnityEngine;

// Token: 0x020001B9 RID: 441
public class DebugAlienAnimatorTester : MonoBehaviour
{
	// Token: 0x06000DA0 RID: 3488 RVA: 0x0007FD73 File Offset: 0x0007E173
	private void Start()
	{
		this.alienAnimator = base.GetComponent<Animator>();
	}

	// Token: 0x06000DA1 RID: 3489 RVA: 0x0007FD84 File Offset: 0x0007E184
	private void Update()
	{
		if (Input.GetKey(276))
		{
			this.alienAnimator.SetInteger("LeanDir", -1);
		}
		else if (Input.GetKey(275))
		{
			this.alienAnimator.SetInteger("LeanDir", 1);
		}
		else
		{
			this.alienAnimator.SetInteger("LeanDir", 0);
		}
		if (Input.GetKey(273))
		{
			this.alienAnimator.SetInteger("DriveDir", 1);
		}
		else if (Input.GetKey(274))
		{
			this.alienAnimator.SetInteger("DriveDir", -1);
		}
		else
		{
			this.alienAnimator.SetInteger("DriveDir", 0);
		}
	}

	// Token: 0x04001024 RID: 4132
	private Animator alienAnimator;
}
