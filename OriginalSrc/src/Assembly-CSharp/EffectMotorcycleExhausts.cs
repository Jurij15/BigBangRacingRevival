using System;
using UnityEngine;

// Token: 0x020001D4 RID: 468
public class EffectMotorcycleExhausts : MonoBehaviour
{
	// Token: 0x06000E0E RID: 3598 RVA: 0x00083560 File Offset: 0x00081960
	private void Start()
	{
		if (PsState.m_activeMinigame != null)
		{
			this.vehicle = PsState.m_activeMinigame.m_playerUnit as Motorcycle;
		}
	}

	// Token: 0x06000E0F RID: 3599 RVA: 0x00083584 File Offset: 0x00081984
	private void RearExhaust()
	{
		int num = Random.Range(0, this.exhausts.Length);
		if (this.exhausts.Length > 0)
		{
			foreach (ParticleSystem particleSystem in this.exhausts[num].singleEffects)
			{
				particleSystem.Play();
			}
			if (this.exhausts[num].playPuffAudio)
			{
				SoundS.PlaySingleShot("/Ingame/Vehicles/EnginePopping_Dirtbike", Vector2.zero, 1f);
			}
			else
			{
				SoundS.PlaySingleShot("/Ingame/Vehicles/EngineBackfire_Dirtbike", Vector2.zero, 1f);
			}
		}
	}

	// Token: 0x06000E10 RID: 3600 RVA: 0x00083624 File Offset: 0x00081A24
	private void Update()
	{
		if (PsState.m_activeMinigame != null)
		{
			if (Controller.GetButtonState("Throttle") == ControllerButtonState.OFF && Controller.GetButtonState("Reverse") == ControllerButtonState.OFF)
			{
				this.gasTriggered = false;
			}
			if ((Controller.GetButtonState("Throttle") == ControllerButtonState.ON && !this.gasTriggered) || (Controller.GetButtonState("Reverse") == ControllerButtonState.ON && !this.gasTriggered))
			{
				float value = Random.value;
				if (value < this.exhaustProcRate)
				{
					this.RearExhaust();
				}
				this.gasTriggered = true;
			}
			else if (Controller.GetButtonState("Throttle") == ControllerButtonState.ON || Controller.GetButtonState("Reverse") == ControllerButtonState.ON)
			{
				if (this.exhaustTimer <= 0f)
				{
					this.exhaustTimer = Random.Range(1f, 4f);
					if (this.exhausts != null)
					{
						this.RearExhaust();
					}
				}
				else
				{
					this.exhaustTimer -= Main.m_gameDeltaTime;
				}
			}
		}
	}

	// Token: 0x0400110C RID: 4364
	[HideInInspector]
	public Motorcycle vehicle;

	// Token: 0x0400110D RID: 4365
	public EffectExhaustGroup[] exhausts;

	// Token: 0x0400110E RID: 4366
	public float exhaustProcRate = 0.5f;

	// Token: 0x0400110F RID: 4367
	private bool gasTriggered;

	// Token: 0x04001110 RID: 4368
	private float exhaustTimer;
}
