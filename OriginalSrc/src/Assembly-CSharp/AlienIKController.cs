using System;
using UnityEngine;

// Token: 0x020001B7 RID: 439
public class AlienIKController : MonoBehaviour
{
	// Token: 0x06000D8F RID: 3471 RVA: 0x0007ED36 File Offset: 0x0007D136
	private void Awake()
	{
		AlienIKController.instance = this;
	}

	// Token: 0x06000D90 RID: 3472 RVA: 0x0007ED40 File Offset: 0x0007D140
	private void OnAnimatorIK()
	{
		if (this.ikGoalHandLeft == null || this.ikGoalHandRight == null)
		{
			Debug.Log("IK goals set!", null);
			this.ikGoalHandLeft = GameObject.Find("IKGoalHandLeft").transform;
			this.ikGoalHandRight = GameObject.Find("IKGoalHandRight").transform;
		}
		if (this.ikGoalHandLeft != null && this.ikGoalHandRight != null)
		{
			this.animator.SetIKPositionWeight(2, this.ikWeight);
			this.animator.SetIKPosition(2, this.ikGoalHandLeft.position);
			this.animator.SetIKRotationWeight(2, this.ikWeight);
			this.animator.SetIKRotation(2, this.ikGoalHandLeft.rotation);
			this.animator.SetIKPositionWeight(3, this.ikWeight);
			this.animator.SetIKPosition(3, this.ikGoalHandRight.position);
			this.animator.SetIKRotationWeight(3, this.ikWeight);
			this.animator.SetIKRotation(3, this.ikGoalHandRight.rotation);
		}
	}

	// Token: 0x06000D91 RID: 3473 RVA: 0x0007EE6A File Offset: 0x0007D26A
	private void OnDestroy()
	{
		AlienIKController.instance = null;
	}

	// Token: 0x04001003 RID: 4099
	public static AlienIKController instance;

	// Token: 0x04001004 RID: 4100
	public Animator animator;

	// Token: 0x04001005 RID: 4101
	public Transform ikGoalHandLeft;

	// Token: 0x04001006 RID: 4102
	public Transform ikGoalHandRight;

	// Token: 0x04001007 RID: 4103
	public Motorcycle motorcycle;

	// Token: 0x04001008 RID: 4104
	public float maxHeight = 1f;

	// Token: 0x04001009 RID: 4105
	public float minHeight = -1f;

	// Token: 0x0400100A RID: 4106
	public float ikWeight = 1f;
}
