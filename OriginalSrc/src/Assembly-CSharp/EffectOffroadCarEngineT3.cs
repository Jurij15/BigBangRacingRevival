using System;
using UnityEngine;

// Token: 0x020001D8 RID: 472
public class EffectOffroadCarEngineT3 : MonoBehaviour
{
	// Token: 0x06000E1B RID: 3611 RVA: 0x0008394D File Offset: 0x00081D4D
	private void Start()
	{
		if (PsState.m_activeMinigame != null)
		{
			this.vehicle = PsState.m_activeMinigame.m_playerUnit as OffroadCar;
		}
	}

	// Token: 0x06000E1C RID: 3612 RVA: 0x00083970 File Offset: 0x00081D70
	private void Update()
	{
		if (PsState.m_activeMinigame != null && this.beltRotator != null)
		{
			if (this.vehicle != null)
			{
				this.beltRotator.Rotate(new Vector3(0f, 0f, this.vehicle.m_currentRPM / 20f));
			}
			else
			{
				this.beltRotator.Rotate(new Vector3(0f, 0f, this.defaultTurnRate));
			}
		}
	}

	// Token: 0x04001119 RID: 4377
	[HideInInspector]
	public OffroadCar vehicle;

	// Token: 0x0400111A RID: 4378
	public Transform beltRotator;

	// Token: 0x0400111B RID: 4379
	public float defaultTurnRate = 25f;
}
