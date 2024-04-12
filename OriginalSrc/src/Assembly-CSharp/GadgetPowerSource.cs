using System;
using UnityEngine;

// Token: 0x020000AE RID: 174
public abstract class GadgetPowerSource : Unit
{
	// Token: 0x06000395 RID: 917 RVA: 0x0001AC1C File Offset: 0x0001901C
	public GadgetPowerSource(GraphElement _graphElement)
		: base(_graphElement, UnitType.Basic)
	{
		this.m_powerSource = _graphElement as LevelPowerLink;
		this.m_powerSource.m_colorIndex = (int)this.m_powerSource.m_storedRotation.x;
		this.m_powerSource.m_inputLimit = 0;
		this.m_triggerOnStart = this.m_powerSource.m_inFront;
		if (this.m_powerSource.m_flipped)
		{
			this.m_powerSource.ChangeColor();
			this.m_powerSource.m_flipped = false;
		}
		this.m_activeColor = this.m_powerSource.GetPowerColor();
		this.m_disabledColor = this.m_powerSource.GetDisabledColor();
		this.CreateObject(this.m_powerSource);
		if (this.m_powerSource.m_inFront)
		{
			this.TurnOn(false);
		}
		else
		{
			this.TurnOff(false);
		}
		this.m_powerSource.m_isForegroundable = true;
	}

	// Token: 0x06000396 RID: 918 RVA: 0x0001ACFA File Offset: 0x000190FA
	protected void SetOutputOffset(Vector3 _offset)
	{
		if (this.m_powerSource != null)
		{
			this.m_powerSource.m_outputOffset = _offset;
		}
	}

	// Token: 0x06000397 RID: 919
	protected abstract void CreateObject(LevelPowerLink _powerSource);

	// Token: 0x06000398 RID: 920 RVA: 0x0001AD13 File Offset: 0x00019113
	protected virtual void TurnOn(bool _anim)
	{
		this.m_powerSource.UpdateColors(true);
	}

	// Token: 0x06000399 RID: 921 RVA: 0x0001AD21 File Offset: 0x00019121
	protected virtual void TurnOff(bool _anim)
	{
		this.m_powerSource.UpdateColors(false);
	}

	// Token: 0x0600039A RID: 922 RVA: 0x0001AD2F File Offset: 0x0001912F
	public override void SyncPositionToGraphElementPosition()
	{
		base.SyncPositionToGraphElementPosition();
		if (this.m_powerSource != null)
		{
			this.m_powerSource.UpdateLinkPositions();
		}
	}

	// Token: 0x0600039B RID: 923 RVA: 0x0001AD4D File Offset: 0x0001914D
	protected virtual void Trigger()
	{
		this.m_powerOn = !this.m_powerOn;
		if (!this.m_powerOn)
		{
			this.TurnOff(true);
		}
		else
		{
			this.TurnOn(true);
		}
		this.m_powerSource.Trigger();
	}

	// Token: 0x0600039C RID: 924 RVA: 0x0001AD88 File Offset: 0x00019188
	public override void Update()
	{
		if (!this.m_minigame.m_editing)
		{
			if (this.m_triggerOnStart)
			{
				this.m_powerSource.m_powerOn = (this.m_powerOn = true);
				this.m_powerSource.Trigger();
				this.TurnOn(false);
				this.m_triggerOnStart = false;
			}
		}
	}

	// Token: 0x0400049E RID: 1182
	protected LevelPowerLink m_powerSource;

	// Token: 0x0400049F RID: 1183
	protected bool m_powerOn;

	// Token: 0x040004A0 RID: 1184
	protected Color m_activeColor;

	// Token: 0x040004A1 RID: 1185
	protected Color m_disabledColor;

	// Token: 0x040004A2 RID: 1186
	public bool m_triggerOnStart;
}
