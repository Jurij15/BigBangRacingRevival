using System;
using UnityEngine;

// Token: 0x02000152 RID: 338
public class RagdollJointBootstrap : MonoBehaviour
{
	// Token: 0x06000BAC RID: 2988 RVA: 0x00073D60 File Offset: 0x00072160
	private void LateUpdate()
	{
		float num = ChipmunkProWrapper.ucpBodyGetAngle(this.m_ragdollNode.m_cmb.body) * 57.29578f;
		if (this.m_ragdollNode.m_parent != null)
		{
			num -= ChipmunkProWrapper.ucpBodyGetAngle(this.m_ragdollNode.m_parent.m_cmb.body) * 57.29578f;
		}
		float num2 = 1f;
		float num3 = 90f;
		if (this.m_ragdollNode.m_flipped)
		{
			num2 = -1f;
			num3 = 270f;
		}
		if (this.m_mainTC != null)
		{
			Quaternion quaternion = Quaternion.Euler(new Vector3(-num * num2, num3, 0f));
			this.m_mainTC.transform.position = this.m_ragdollNode.m_cmb.TC.transform.position;
			this.m_mainTC.transform.localRotation = quaternion;
			this.m_ragdollNode.m_transform.localPosition = new Vector3(0f, -this.m_ragdollNode.m_centeroid.y, -this.m_ragdollNode.m_centeroid.x * num2);
			this.m_ragdollNode.m_transform.localRotation = Quaternion.identity;
		}
		else
		{
			Quaternion quaternion2 = Quaternion.Euler(this.m_ragdollNode.m_originalRot.eulerAngles + new Vector3(-num * num2, 0f, 0f));
			this.m_ragdollNode.m_transform.localRotation = quaternion2;
		}
	}

	// Token: 0x04000A50 RID: 2640
	public RagdollNode m_ragdollNode;

	// Token: 0x04000A51 RID: 2641
	public TransformC m_mainTC;
}
