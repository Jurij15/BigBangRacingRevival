using System;
using System.Runtime.Serialization;
using UnityEngine;

// Token: 0x02000015 RID: 21
[Serializable]
public class LevelGadget : LevelPowerConnectable
{
	// Token: 0x060000A3 RID: 163 RVA: 0x00007572 File Offset: 0x00005972
	public LevelGadget(GraphNodeType _nodeType, Type _assembleClassType, string _name, Vector3 _pos, Vector3 _rot, Vector3 _sca)
		: base(_nodeType, _assembleClassType, _name, _pos, _rot, _sca)
	{
	}

	// Token: 0x060000A4 RID: 164 RVA: 0x000075A6 File Offset: 0x000059A6
	public LevelGadget(SerializationInfo info, StreamingContext ctxt)
		: base(info, ctxt)
	{
		this.SetPropertyDefaults();
	}

	// Token: 0x060000A5 RID: 165 RVA: 0x000075D9 File Offset: 0x000059D9
	public override void GetObjectData(SerializationInfo info, StreamingContext ctxt)
	{
		base.GetObjectData(info, ctxt);
	}

	// Token: 0x060000A6 RID: 166 RVA: 0x000075E4 File Offset: 0x000059E4
	public void UpdateInputPositions()
	{
		for (int i = 0; i < this.m_inputs.Count; i++)
		{
			PowerConnection powerConnection = this.m_inputs[i] as PowerConnection;
			powerConnection.UpdateVisualPresentationPosition();
		}
	}

	// Token: 0x060000A7 RID: 167 RVA: 0x00007625 File Offset: 0x00005A25
	public override void Trigger()
	{
		this.m_powerOn = !this.m_powerOn;
		this.didChange.Invoke(this.m_powerOn);
	}

	// Token: 0x060000A8 RID: 168 RVA: 0x00007647 File Offset: 0x00005A47
	public override void Reset()
	{
		base.Reset();
		this.m_powerOn = false;
		this.didChange = delegate
		{
		};
	}

	// Token: 0x060000A9 RID: 169 RVA: 0x00007679 File Offset: 0x00005A79
	public override void Clear(bool _isReset)
	{
		base.Clear(_isReset);
		this.m_powerOn = false;
		this.didChange = delegate
		{
		};
	}

	// Token: 0x060000AA RID: 170 RVA: 0x000076AC File Offset: 0x00005AAC
	public override void Dispose()
	{
		this.didChange = delegate
		{
		};
		this.m_powerOn = false;
		base.Dispose();
	}

	// Token: 0x060000AB RID: 171 RVA: 0x000076DE File Offset: 0x00005ADE
	public void AddPowerStateChangeListener(Action<bool> _action)
	{
		this.didChange = (Action<bool>)Delegate.Combine(this.didChange, _action);
	}

	// Token: 0x060000AC RID: 172 RVA: 0x000076F7 File Offset: 0x00005AF7
	public bool HasPower()
	{
		return this.m_powerOn;
	}

	// Token: 0x0400008A RID: 138
	public new bool m_powerOn;

	// Token: 0x0400008B RID: 139
	public Action<bool> didChange = delegate
	{
	};
}
