using System;
using UnityEngine;

// Token: 0x0200005C RID: 92
public class MovingPlatform : MovingPlatformBase
{
	// Token: 0x0600021A RID: 538 RVA: 0x0001A988 File Offset: 0x00018D88
	public MovingPlatform(GraphElement _graphElement)
		: base(_graphElement, "MovingPlatformPrefab")
	{
		base.SetInputOffset(new Vector3(0f, 0f, 60f));
		base.m_graphElement.m_isRotateable = true;
		this.m_stopTicks = 35f;
		this.m_powerOffPosition = this.m_startPos;
	}

	// Token: 0x0600021B RID: 539 RVA: 0x0001A9DE File Offset: 0x00018DDE
	public override void PowerStateChange(bool _state)
	{
		base.PowerStateChange(_state);
		if (!_state)
		{
			this.m_powerOffPosition = ChipmunkProWrapper.ucpBodyGetPos(this.m_cmb.body);
		}
	}

	// Token: 0x0600021C RID: 540 RVA: 0x0001AA04 File Offset: 0x00018E04
	public override void Update()
	{
		base.Update();
		if (!this.m_minigame.m_editing && this.m_minigame.m_gameStarted)
		{
			if (!this.m_powerOn)
			{
				ChipmunkProWrapper.ucpBodySetPos(this.m_cmb.body, this.m_powerOffPosition);
				return;
			}
			if (this.m_stopTicks > 0f)
			{
				this.m_stopTicks -= 1f * Main.m_timeScale;
			}
			if (this.m_ghostEntity != null)
			{
				base.DestroyEndGhost();
			}
			if (this.m_currentTime == -1f && this.m_stopTicks <= 0f)
			{
				this.m_startPos = this.m_cmb.TC.transform.position;
				this.m_currentTime = 0f;
			}
			else if (this.m_currentValue == this.m_moveMagnitude * this.m_rotMultiplier)
			{
				this.m_currentTime = -1f;
				this.m_rotMultiplier *= -1f;
				this.m_stopTicks = 35f;
			}
			if (this.m_currentTime != -1f)
			{
				this.m_currentTime += Main.m_gameDeltaTime;
				if (this.m_currentTime >= this.m_duration)
				{
					this.m_currentTime = this.m_duration;
				}
				this.m_currentValue = TweenS.tween(TweenStyle.QuadInOut, this.m_currentTime, this.m_duration, 0f, this.m_moveMagnitude * this.m_rotMultiplier);
			}
			float magnitude = ChipmunkProWrapper.ucpBodyGetVel(this.m_cmb.body).magnitude;
			ChipmunkProWrapper.ucpBodySetPos(this.m_cmb.body, this.m_startPos + this.m_moveDir * this.m_currentValue);
			TransformS.SetGlobalRotation(this.m_gearTC, this.m_gearTC.transform.rotation.eulerAngles + new Vector3(0f, 0f, magnitude * Main.m_gameDeltaTime) * this.m_rotMultiplier);
		}
	}

	// Token: 0x04000214 RID: 532
	private float m_stopTicks;

	// Token: 0x04000215 RID: 533
	private Vector2 m_powerOffPosition;
}
