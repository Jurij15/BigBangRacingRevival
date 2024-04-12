using System;
using System.Runtime.Serialization;
using UnityEngine;

// Token: 0x0200001A RID: 26
[Serializable]
public class LevelPowerLink : LevelPowerConnectable
{
	// Token: 0x060000CF RID: 207 RVA: 0x00007E7C File Offset: 0x0000627C
	public LevelPowerLink(GraphNodeType _nodeType, Type _assembleClassType, string _name, Vector3 _pos, Vector3 _rot, Vector3 _sca)
		: base(_nodeType, _assembleClassType, _name, _pos, _rot, _sca)
	{
		this.m_colorIndex = (int)this.m_storedRotation.x;
	}

	// Token: 0x060000D0 RID: 208 RVA: 0x00007E9F File Offset: 0x0000629F
	public LevelPowerLink(SerializationInfo info, StreamingContext ctxt)
		: base(info, ctxt)
	{
		this.SetPropertyDefaults();
	}

	// Token: 0x060000D1 RID: 209 RVA: 0x00007EAF File Offset: 0x000062AF
	public void ChangeColor()
	{
		this.m_colorIndex = (this.m_colorIndex + 1) % LevelPowerLink.enabledColors.Length;
		this.m_storedRotation.x = (float)this.m_colorIndex;
		this.UpdateColors(this.m_powerOn);
	}

	// Token: 0x060000D2 RID: 210 RVA: 0x00007EE8 File Offset: 0x000062E8
	public void UpdateColors(bool _powerOn)
	{
		this.m_powerOn = _powerOn;
		for (int i = 0; i < this.m_outputs.Count; i++)
		{
			(this.m_outputs[i] as PowerConnection).UpdateColor();
		}
	}

	// Token: 0x060000D3 RID: 211 RVA: 0x00007F2E File Offset: 0x0000632E
	public Color GetActiveColor()
	{
		return (!this.m_powerOn) ? this.GetDisabledColor() : this.GetPowerColor();
	}

	// Token: 0x060000D4 RID: 212 RVA: 0x00007F4C File Offset: 0x0000634C
	public Color GetPowerColor()
	{
		return this.GetColor(LevelPowerLink.enabledColors);
	}

	// Token: 0x060000D5 RID: 213 RVA: 0x00007F59 File Offset: 0x00006359
	public Color GetDisabledColor()
	{
		return this.GetColor(LevelPowerLink.disabledColors);
	}

	// Token: 0x060000D6 RID: 214 RVA: 0x00007F66 File Offset: 0x00006366
	private Color GetColor(Color[] _colors)
	{
		if (this.m_colorIndex < _colors.Length)
		{
			return _colors[this.m_colorIndex];
		}
		return _colors[this.m_colorIndex % _colors.Length];
	}

	// Token: 0x060000D7 RID: 215 RVA: 0x00007F9D File Offset: 0x0000639D
	public override void GetObjectData(SerializationInfo info, StreamingContext ctxt)
	{
		base.GetObjectData(info, ctxt);
	}

	// Token: 0x060000D8 RID: 216 RVA: 0x00007FA8 File Offset: 0x000063A8
	public new void UpdateLinkPositions()
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

	// Token: 0x060000D9 RID: 217 RVA: 0x00008020 File Offset: 0x00006420
	public override void Trigger()
	{
		for (int i = 0; i < this.m_outputs.Count; i++)
		{
			this.m_outputs[i].Trigger();
		}
	}

	// Token: 0x060000DA RID: 218 RVA: 0x0000805A File Offset: 0x0000645A
	public override void SetPropertyDefaults()
	{
		base.SetPropertyDefaults();
		this.m_isFlippable = true;
		this.m_isRemovable = true;
		this.m_isCopyable = true;
		this.m_isRotateable = true;
		this.m_isLinkable = true;
	}

	// Token: 0x060000DB RID: 219 RVA: 0x00008085 File Offset: 0x00006485
	public override bool CanCreateConnection()
	{
		return this.m_outputLimit > this.m_outputs.Count;
	}

	// Token: 0x0400009C RID: 156
	public static Color[] enabledColors = new Color[]
	{
		new Color(1f, 0.678f, 0.678f),
		new Color(0.271f, 0.729f, 1f),
		new Color(0.318f, 1f, 0.322f),
		new Color(0.984f, 1f, 0f),
		new Color(1f, 0.643f, 0.192f)
	};

	// Token: 0x0400009D RID: 157
	public static Color[] disabledColors = new Color[]
	{
		Color.grey,
		Color.grey,
		Color.grey,
		Color.grey,
		Color.grey
	};

	// Token: 0x0400009E RID: 158
	public new bool m_powerOn;

	// Token: 0x0400009F RID: 159
	public int m_colorIndex;
}
