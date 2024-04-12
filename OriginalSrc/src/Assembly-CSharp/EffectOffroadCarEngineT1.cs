using System;
using UnityEngine;

// Token: 0x020001D6 RID: 470
public class EffectOffroadCarEngineT1 : MonoBehaviour
{
	// Token: 0x06000E15 RID: 3605 RVA: 0x000837A1 File Offset: 0x00081BA1
	private void Start()
	{
		if (PsState.m_activeMinigame != null)
		{
			this.vehicle = PsState.m_activeMinigame.m_playerUnit as OffroadCar;
		}
	}

	// Token: 0x06000E16 RID: 3606 RVA: 0x000837C4 File Offset: 0x00081BC4
	private void Update()
	{
		if (PsState.m_activeMinigame != null)
		{
			if (this.vehicle != null)
			{
				base.transform.Rotate(new Vector3(0f, 0f, this.vehicle.m_currentRPM / 20f));
			}
			else
			{
				base.transform.Rotate(new Vector3(0f, 0f, this.defaultTurnRate));
			}
		}
	}

	// Token: 0x04001114 RID: 4372
	[HideInInspector]
	public OffroadCar vehicle;

	// Token: 0x04001115 RID: 4373
	public float defaultTurnRate = 25f;
}
