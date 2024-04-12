using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

// Token: 0x020000B3 RID: 179
[Serializable]
public class GhostNode : ISerializable
{
	// Token: 0x0600039F RID: 927 RVA: 0x00035C2A File Offset: 0x0003402A
	public GhostNode(string _name, Transform _transform)
	{
		this.m_name = _name;
		this.m_posX = new List<int>();
		this.m_posY = new List<int>();
		this.m_rot = new List<int>();
		this.m_transform = _transform;
	}

	// Token: 0x060003A0 RID: 928 RVA: 0x00035C64 File Offset: 0x00034064
	public GhostNode(SerializationInfo info, StreamingContext ctxt)
	{
		this.m_transform = null;
		this.m_name = (string)info.GetValue("name", typeof(string));
		this.m_posX = new List<int>((int[])info.GetValue("x", typeof(int[])));
		this.m_posY = new List<int>((int[])info.GetValue("y", typeof(int[])));
		this.m_rot = new List<int>((int[])info.GetValue("rot", typeof(int[])));
	}

	// Token: 0x060003A1 RID: 929 RVA: 0x00035D10 File Offset: 0x00034110
	public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
	{
		info.AddValue("name", this.m_name);
		info.AddValue("x", this.m_posX.ToArray());
		info.AddValue("y", this.m_posY.ToArray());
		info.AddValue("rot", this.m_rot.ToArray());
	}

	// Token: 0x060003A2 RID: 930 RVA: 0x00035D70 File Offset: 0x00034170
	public void AddKeyFrame()
	{
		this.m_posX.Add((int)(this.m_transform.position.x * 100f));
		this.m_posY.Add((int)(this.m_transform.position.y * 100f));
		this.m_rot.Add((int)(this.m_transform.rotation.eulerAngles.z * 100f));
	}

	// Token: 0x060003A3 RID: 931 RVA: 0x00035DF4 File Offset: 0x000341F4
	public Vector2 GetKeyFramePos(int _frame)
	{
		if (this.m_posX == null || this.m_posY == null || this.m_posX.Count == 0 || this.m_posY.Count == 0)
		{
			return Vector2.zero;
		}
		return new Vector2((float)this.m_posX[_frame] / 100f, (float)this.m_posY[_frame] / 100f);
	}

	// Token: 0x060003A4 RID: 932 RVA: 0x00035E68 File Offset: 0x00034268
	public float GetKeyFrameRotation(int _frame)
	{
		if (this.m_rot == null || this.m_rot.Count == 0)
		{
			return 0f;
		}
		return (float)this.m_rot[_frame] / 100f;
	}

	// Token: 0x040004BC RID: 1212
	public string m_name;

	// Token: 0x040004BD RID: 1213
	public Transform m_transform;

	// Token: 0x040004BE RID: 1214
	public List<int> m_posX;

	// Token: 0x040004BF RID: 1215
	public List<int> m_posY;

	// Token: 0x040004C0 RID: 1216
	public List<int> m_rot;
}
