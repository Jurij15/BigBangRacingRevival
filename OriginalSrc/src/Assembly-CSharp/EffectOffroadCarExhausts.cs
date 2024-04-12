using System;
using UnityEngine;

// Token: 0x020001D9 RID: 473
public class EffectOffroadCarExhausts : MonoBehaviour
{
	// Token: 0x06000E1E RID: 3614 RVA: 0x00083A06 File Offset: 0x00081E06
	private void Start()
	{
		if (PsState.m_activeMinigame != null)
		{
			this.vehicle = PsState.m_activeMinigame.m_playerUnit as OffroadCar;
		}
	}

	// Token: 0x06000E1F RID: 3615 RVA: 0x00083A28 File Offset: 0x00081E28
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
				SoundS.PlaySingleShot("/Ingame/Vehicles/EnginePopping_Car", Vector2.zero, 1f);
			}
			else
			{
				SoundS.PlaySingleShot("/Ingame/Vehicles/EngineBackfire_Car", Vector2.zero, 1f);
			}
		}
	}

	// Token: 0x06000E20 RID: 3616 RVA: 0x00083AC8 File Offset: 0x00081EC8
	private void SideExhaust()
	{
		int num = Random.Range(0, this.sideExhausts.Length);
		if (this.sideExhausts.Length > 0)
		{
			foreach (ParticleSystem particleSystem in this.sideExhausts[num].singleEffects)
			{
				particleSystem.Play();
			}
		}
	}

	// Token: 0x06000E21 RID: 3617 RVA: 0x00083B20 File Offset: 0x00081F20
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
					this.SideExhaust();
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
					if (this.sideExhausts != null)
					{
						this.SideExhaust();
					}
				}
				else
				{
					this.exhaustTimer -= Main.m_gameDeltaTime;
				}
			}
		}
	}

	// Token: 0x0400111C RID: 4380
	[HideInInspector]
	public OffroadCar vehicle;

	// Token: 0x0400111D RID: 4381
	public EffectExhaustGroup[] exhausts;

	// Token: 0x0400111E RID: 4382
	public EffectExhaustGroup[] sideExhausts;

	// Token: 0x0400111F RID: 4383
	public float exhaustProcRate = 0.5f;

	// Token: 0x04001120 RID: 4384
	private bool gasTriggered;

	// Token: 0x04001121 RID: 4385
	private float exhaustTimer;
}
