using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200002F RID: 47
public class RagdollNode
{
	// Token: 0x0600012B RID: 299 RVA: 0x0000DC58 File Offset: 0x0000C058
	public RagdollNode(Transform _transform)
	{
		this.m_transform = _transform;
		this.m_name = _transform.name;
		this.m_childs = new List<RagdollNode>();
		this.m_ragdollJointBootstrap = this.m_transform.gameObject.AddComponent<RagdollJointBootstrap>();
		this.m_ragdollJointBootstrap.m_ragdollNode = this;
	}

	// Token: 0x0600012C RID: 300 RVA: 0x0000DCAC File Offset: 0x0000C0AC
	public RagdollNode FindChild(string _name)
	{
		for (int i = 0; i < this.m_childs.Count; i++)
		{
			if (this.m_childs[i].m_name == _name)
			{
				return this.m_childs[i];
			}
			this.m_childs[i].FindChild(_name);
		}
		return null;
	}

	// Token: 0x040000DC RID: 220
	public string m_name;

	// Token: 0x040000DD RID: 221
	public RagdollNode m_parent;

	// Token: 0x040000DE RID: 222
	public List<RagdollNode> m_childs;

	// Token: 0x040000DF RID: 223
	public Transform m_transform;

	// Token: 0x040000E0 RID: 224
	public Quaternion m_originalRot;

	// Token: 0x040000E1 RID: 225
	public Vector3 m_centeroid;

	// Token: 0x040000E2 RID: 226
	public Vector3 m_offset;

	// Token: 0x040000E3 RID: 227
	public bool m_flipped;

	// Token: 0x040000E4 RID: 228
	public TransformC m_tc;

	// Token: 0x040000E5 RID: 229
	public ChipmunkBodyC m_cmb;

	// Token: 0x040000E6 RID: 230
	public IntPtr m_pivotJoint;

	// Token: 0x040000E7 RID: 231
	public IntPtr m_rotaryLimitJoint;

	// Token: 0x040000E8 RID: 232
	public RagdollJointBootstrap m_ragdollJointBootstrap;
}
