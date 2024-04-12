using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

// Token: 0x02000019 RID: 25
[Serializable]
public class LevelPowerConnectable : GraphNode
{
	// Token: 0x060000C3 RID: 195 RVA: 0x000073C3 File Offset: 0x000057C3
	public LevelPowerConnectable(GraphNodeType _nodeType, Type _assembleClassType, string _name, Vector3 _pos, Vector3 _rot, Vector3 _sca)
		: base(_nodeType, _assembleClassType, _name, _pos, _rot, _sca)
	{
	}

	// Token: 0x060000C4 RID: 196 RVA: 0x000073EA File Offset: 0x000057EA
	public LevelPowerConnectable(SerializationInfo info, StreamingContext ctxt)
		: base(info, ctxt)
	{
		this.SetPropertyDefaults();
	}

	// Token: 0x060000C5 RID: 197 RVA: 0x00007410 File Offset: 0x00005810
	public override void GetObjectData(SerializationInfo info, StreamingContext ctxt)
	{
		base.GetObjectData(info, ctxt);
	}

	// Token: 0x060000C6 RID: 198 RVA: 0x0000741C File Offset: 0x0000581C
	public void UpdateLinkPositions()
	{
		for (int i = 0; i < this.m_inputs.Count; i++)
		{
			PowerConnection powerConnection = this.m_inputs[i] as PowerConnection;
			powerConnection.UpdateVisualPresentationPosition();
		}
		for (int j = 0; j < this.m_outputs.Count; j++)
		{
			PowerConnection powerConnection2 = this.m_outputs[j] as PowerConnection;
			powerConnection2.UpdateVisualPresentationPosition();
		}
	}

	// Token: 0x060000C7 RID: 199 RVA: 0x00007491 File Offset: 0x00005891
	public override GraphConnection CreateConnection(GraphElement _target)
	{
		return new PowerConnection(this, _target);
	}

	// Token: 0x060000C8 RID: 200 RVA: 0x0000749A File Offset: 0x0000589A
	public override List<GraphElement> FilterSuitables(List<GraphElement> _list)
	{
		return _list.FindAll((GraphElement c) => c.GetType() == typeof(LevelPowerLink) || c.GetType() == typeof(LevelGadget));
	}

	// Token: 0x060000C9 RID: 201 RVA: 0x000074BF File Offset: 0x000058BF
	public override bool CanConnect(GraphElement _host)
	{
		return this.m_inputs.Count < this.m_inputLimit;
	}

	// Token: 0x060000CA RID: 202 RVA: 0x000074D4 File Offset: 0x000058D4
	public override Vector3 GetInputPosition()
	{
		return base.GetOutputPosition() + Quaternion.AngleAxis(this.m_rotation.z, Vector3.forward) * this.m_inputOffset;
	}

	// Token: 0x060000CB RID: 203 RVA: 0x00007501 File Offset: 0x00005901
	public override Vector3 GetOutputPosition()
	{
		return base.GetOutputPosition() + Quaternion.AngleAxis(this.m_rotation.z, Vector3.forward) * this.m_outputOffset;
	}

	// Token: 0x060000CC RID: 204 RVA: 0x0000752E File Offset: 0x0000592E
	public override void Select(bool _select)
	{
		base.Select(_select);
	}

	// Token: 0x060000CD RID: 205 RVA: 0x00007537 File Offset: 0x00005937
	public override GraphConnectionVisual GetVisualConnection(Vector3 _start, Vector3 _end)
	{
		return new PowerLineGraphVisual(this.m_TC, _start, _end);
	}

	// Token: 0x04000096 RID: 150
	public bool m_powerOn;

	// Token: 0x04000097 RID: 151
	public int m_inputLimit;

	// Token: 0x04000098 RID: 152
	public int m_outputLimit;

	// Token: 0x04000099 RID: 153
	public Vector3 m_outputOffset = Vector3.zero;

	// Token: 0x0400009A RID: 154
	public Vector3 m_inputOffset = Vector3.zero;
}
