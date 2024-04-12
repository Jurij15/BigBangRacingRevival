using System;
using UnityEngine;

// Token: 0x02000082 RID: 130
public class TriggeredMovingPlatform : MovingPlatformBase
{
	// Token: 0x0600029D RID: 669 RVA: 0x00022338 File Offset: 0x00020738
	public TriggeredMovingPlatform(GraphElement _graphElement)
		: base(_graphElement, "TriggeredMovingPlatformPrefab")
	{
		if (!this.m_minigame.m_editing)
		{
			ChipmunkProS.AddCollisionHandler(this.m_cmb, new CollisionDelegate(this.PlatformCollisionHandler), (ucpCollisionType)4, (ucpCollisionType)3, true, true, true);
		}
		this.m_childNode.m_maxDistanceFromParent = 2000f;
		PrefabC prefabC = PrefabS.AddComponent(this.m_platformTC, new Vector3(0f, 0f, -55f) + base.GetZBufferBias(), this.m_mainPrefab.transform.Find("ActiveLight").gameObject);
		this.m_light = prefabC.p_gameObject;
		this.m_lightMaterial = this.m_light.GetComponent<Renderer>().material;
		this.m_inActiveColor = new Color(0.105882354f, 0.1254902f, 0.101960786f);
		this.m_activeColor = new Color(0.3137255f, 1f, 0.023529412f);
		this.m_lightMaterial.color = this.m_inActiveColor;
		this.m_ticks = 0f;
		base.m_graphElement.m_isRotateable = true;
		this.m_active = (this.m_move = (this.m_set = (this.m_setActive = false)));
		this.m_moveBeginPos = this.m_startPos;
	}

	// Token: 0x0600029E RID: 670 RVA: 0x0002247E File Offset: 0x0002087E
	public override void Destroy()
	{
		Object.Destroy(this.m_lightMaterial);
		base.Destroy();
	}

	// Token: 0x0600029F RID: 671 RVA: 0x00022494 File Offset: 0x00020894
	public override void Update()
	{
		base.Update();
		if (!this.m_minigame.m_editing && this.m_minigame.m_gameStarted)
		{
			if (this.m_ghostEntity != null)
			{
				base.DestroyEndGhost();
			}
			if (this.m_setActive)
			{
				this.m_ticks += 1f * Main.m_timeScale;
				if (this.m_ticks >= 15f)
				{
					if (!this.m_active)
					{
						this.m_set = false;
					}
					this.m_active = true;
					this.m_ticks = 15f;
					this.m_setActive = false;
				}
			}
			else if (!this.m_setActive && !this.m_move)
			{
				this.m_ticks -= 1f * Main.m_timeScale;
				if (this.m_ticks <= 0f)
				{
					this.m_active = false;
					this.m_ticks = 0f;
				}
			}
			if (this.m_active && !this.m_move && !this.m_set)
			{
				if ((ChipmunkProWrapper.ucpBodyGetPos(this.m_cmb.body) - this.m_childNode.m_position).sqrMagnitude < (ChipmunkProWrapper.ucpBodyGetPos(this.m_cmb.body) - this.m_startPos).sqrMagnitude)
				{
					this.m_moveDir = this.m_startPos - this.m_childNode.m_position;
					this.m_moveBeginPos = this.m_childNode.m_position;
					this.m_rotMultiplier = 1f;
				}
				else
				{
					this.m_moveDir = this.m_childNode.m_position - this.m_startPos;
					this.m_moveBeginPos = this.m_startPos;
					this.m_rotMultiplier = -1f;
				}
				this.m_moveDir = this.m_moveDir.normalized;
				this.m_currentTime = 0f;
				this.m_currentValue = 0f;
				this.m_move = true;
				this.m_set = true;
				SoundS.PlaySingleShot("/InGame/Units/ElevatorActivated", this.m_cmb.TC.transform.position, 1f);
			}
			if (this.m_move)
			{
				this.m_currentTime += Main.m_gameDeltaTime;
				if (this.m_currentTime >= this.m_duration)
				{
					this.m_currentTime = this.m_duration;
				}
				this.m_currentValue = TweenS.tween(TweenStyle.QuadInOut, this.m_currentTime, this.m_duration, 0f, this.m_moveMagnitude);
			}
			ChipmunkProWrapper.ucpBodySetPos(this.m_cmb.body, this.m_moveBeginPos + this.m_moveDir * this.m_currentValue);
			TransformS.SetGlobalRotation(this.m_gearTC, this.m_gearTC.transform.rotation.eulerAngles + new Vector3(0f, 0f, ChipmunkProWrapper.ucpBodyGetVel(this.m_cmb.body).magnitude * Main.m_gameDeltaTime) * this.m_rotMultiplier);
			if (this.m_currentTime == this.m_duration)
			{
				this.m_move = false;
			}
			if (this.m_active || this.m_setActive || this.m_move)
			{
				this.TurnLight(true);
			}
			else
			{
				this.TurnLight(false);
			}
		}
	}

	// Token: 0x060002A0 RID: 672 RVA: 0x0002280E File Offset: 0x00020C0E
	public void TurnLight(bool _on)
	{
		this.m_lightMaterial.color = ((!_on) ? this.m_inActiveColor : this.m_activeColor);
	}

	// Token: 0x060002A1 RID: 673 RVA: 0x00022834 File Offset: 0x00020C34
	private void PlatformCollisionHandler(ucpCollisionPair _pair, ucpCollisionPhase _phase)
	{
		ChipmunkBodyC chipmunkBodyC = ChipmunkProS.m_bodies.m_array[_pair.ucpComponentIndexB];
		UnitC unitC = chipmunkBodyC.customComponent as UnitC;
		if (unitC == null || unitC.m_unit == null)
		{
			return;
		}
		if (_phase == ucpCollisionPhase.Begin || _phase == ucpCollisionPhase.Persist)
		{
			this.m_setActive = true;
		}
	}

	// Token: 0x040002DC RID: 732
	private bool m_active;

	// Token: 0x040002DD RID: 733
	private bool m_move;

	// Token: 0x040002DE RID: 734
	private bool m_set;

	// Token: 0x040002DF RID: 735
	private bool m_setActive;

	// Token: 0x040002E0 RID: 736
	private GameObject m_light;

	// Token: 0x040002E1 RID: 737
	private Color m_inActiveColor;

	// Token: 0x040002E2 RID: 738
	private Color m_activeColor;

	// Token: 0x040002E3 RID: 739
	private Vector2 m_moveBeginPos;

	// Token: 0x040002E4 RID: 740
	private Material m_lightMaterial;

	// Token: 0x040002E5 RID: 741
	private float m_ticks;
}
