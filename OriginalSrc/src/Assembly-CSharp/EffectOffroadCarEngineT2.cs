using System;
using UnityEngine;

// Token: 0x020001D7 RID: 471
public class EffectOffroadCarEngineT2 : MonoBehaviour
{
	// Token: 0x06000E18 RID: 3608 RVA: 0x0008383E File Offset: 0x00081C3E
	private void Start()
	{
		if (PsState.m_activeMinigame != null)
		{
			this.vehicle = PsState.m_activeMinigame.m_playerUnit as OffroadCar;
		}
	}

	// Token: 0x06000E19 RID: 3609 RVA: 0x00083860 File Offset: 0x00081C60
	private void Update()
	{
		if (PsState.m_activeMinigame != null && this.flapperAnimator != null)
		{
			if ((Controller.GetButtonState("Throttle") == ControllerButtonState.ON && !this.flapperTriggered) || (Controller.GetButtonState("Reverse") == ControllerButtonState.ON && !this.flapperTriggered))
			{
				this.flapperAnimator.SetTrigger("Open");
				this.flapperAnimator.ResetTrigger("Close");
				this.flapperTriggered = true;
			}
			else if (Controller.GetButtonState("Throttle") == ControllerButtonState.OFF && Controller.GetButtonState("Reverse") == ControllerButtonState.OFF && this.flapperTriggered)
			{
				this.flapperAnimator.SetTrigger("Close");
				this.flapperAnimator.ResetTrigger("Open");
				this.flapperTriggered = false;
			}
		}
	}

	// Token: 0x04001116 RID: 4374
	[HideInInspector]
	public OffroadCar vehicle;

	// Token: 0x04001117 RID: 4375
	public Animator flapperAnimator;

	// Token: 0x04001118 RID: 4376
	private bool flapperTriggered;
}
