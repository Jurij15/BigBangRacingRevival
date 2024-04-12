using System;
using UnityEngine;

// Token: 0x020000AD RID: 173
public abstract class Gadget : Unit
{
	// Token: 0x06000391 RID: 913 RVA: 0x00015B50 File Offset: 0x00013F50
	public Gadget(GraphElement _graphElement)
		: base(_graphElement, UnitType.Basic)
	{
		if (base.m_graphElement.GetType() == typeof(LevelGadget))
		{
			this.m_levelGadget = base.m_graphElement as LevelGadget;
			this.m_levelGadget.m_name = this.ToString();
			this.m_levelGadget.m_inputLimit = 1;
			if (this.m_levelGadget.m_inputs.Count > 0)
			{
				this.m_levelGadget.AddPowerStateChangeListener(new Action<bool>(this.PowerStateChange));
				this.m_powerOn = (this.m_initialPowerOn = this.m_levelGadget.m_powerOn);
			}
			else
			{
				this.m_levelGadget.m_powerOn = (this.m_initialPowerOn = (this.m_powerOn = true));
			}
		}
	}

	// Token: 0x06000392 RID: 914 RVA: 0x00015C2B File Offset: 0x0001402B
	protected void SetInputOffset(Vector3 _offset)
	{
		if (this.m_levelGadget != null)
		{
			this.m_levelGadget.m_inputOffset = _offset;
		}
	}

	// Token: 0x06000393 RID: 915 RVA: 0x00015C44 File Offset: 0x00014044
	public virtual void PowerStateChange(bool _state)
	{
		if (this.m_powerOn != _state)
		{
			this.m_powerOn = _state;
		}
	}

	// Token: 0x06000394 RID: 916 RVA: 0x00015C59 File Offset: 0x00014059
	public override void SyncPositionToGraphElementPosition()
	{
		base.SyncPositionToGraphElementPosition();
		if (this.m_levelGadget != null)
		{
			this.m_levelGadget.UpdateInputPositions();
		}
	}

	// Token: 0x0400049B RID: 1179
	protected LevelGadget m_levelGadget;

	// Token: 0x0400049C RID: 1180
	protected bool m_powerOn = true;

	// Token: 0x0400049D RID: 1181
	protected bool m_initialPowerOn = true;
}
