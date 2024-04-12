using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200014F RID: 335
public class BoneModifier : MonoBehaviour
{
	// Token: 0x06000BA3 RID: 2979 RVA: 0x00073A78 File Offset: 0x00071E78
	public BoneModifier.BoneModifierData AddChipmunkModifier(ChipmunkBodyC _cmb, Transform _bone, float _weight = 1f, Transform _parent = null)
	{
		BoneModifier.BoneModifierData boneModifierData = new BoneModifier.BoneModifierData();
		boneModifierData.m_cmb = _cmb;
		boneModifierData.m_bone = _bone;
		boneModifierData.m_weight = _weight;
		boneModifierData.m_parent = _parent;
		boneModifierData.m_angleOffset = 0f;
		boneModifierData.m_posOffset = Vector3.zero;
		this.m_bones.Add(boneModifierData);
		return boneModifierData;
	}

	// Token: 0x06000BA4 RID: 2980 RVA: 0x00073ACC File Offset: 0x00071ECC
	public BoneModifier.BoneModifierData AddCustomModifier(Transform _bone, float _weight = 1f, Transform _parent = null)
	{
		BoneModifier.BoneModifierData boneModifierData = new BoneModifier.BoneModifierData();
		boneModifierData.m_cmb = null;
		boneModifierData.m_bone = _bone;
		boneModifierData.m_weight = _weight;
		boneModifierData.m_parent = _parent;
		boneModifierData.m_angleOffset = 0f;
		boneModifierData.m_posOffset = Vector3.zero;
		this.m_bones.Add(boneModifierData);
		return boneModifierData;
	}

	// Token: 0x06000BA5 RID: 2981 RVA: 0x00073B20 File Offset: 0x00071F20
	private void LateUpdate()
	{
		foreach (BoneModifier.BoneModifierData boneModifierData in this.m_bones)
		{
			this.m_globalWeight = Mathf.Clamp(this.m_globalWeight, 0f, 1f);
			if (boneModifierData.m_bone != null)
			{
				float num;
				if (boneModifierData.m_cmb != null)
				{
					num = ToolBox.getRolledValue(ChipmunkProWrapper.ucpBodyGetAngle(boneModifierData.m_cmb.body) * 57.29578f, 0f, 360f);
					if (boneModifierData.m_parent != null)
					{
						num = Mathf.DeltaAngle(num, boneModifierData.m_parent.transform.localRotation.eulerAngles.z);
					}
					num *= boneModifierData.m_weight * this.m_globalWeight;
				}
				else
				{
					num = boneModifierData.m_angleOffset * boneModifierData.m_weight * this.m_globalWeight;
					Vector3 vector = boneModifierData.m_posOffset * boneModifierData.m_weight * this.m_globalWeight;
					boneModifierData.m_bone.localPosition = boneModifierData.m_bone.localPosition + vector;
				}
				boneModifierData.m_bone.localRotation = Quaternion.Euler(boneModifierData.m_bone.localRotation.eulerAngles + new Vector3(num, 0f, 0f));
			}
			else
			{
				Debug.LogError("Something went wrong!");
			}
		}
	}

	// Token: 0x06000BA6 RID: 2982 RVA: 0x00073CCC File Offset: 0x000720CC
	private void OnDestroy()
	{
		this.m_bones.Clear();
	}

	// Token: 0x04000A48 RID: 2632
	public float m_globalWeight = 1f;

	// Token: 0x04000A49 RID: 2633
	public List<BoneModifier.BoneModifierData> m_bones = new List<BoneModifier.BoneModifierData>();

	// Token: 0x02000150 RID: 336
	public class BoneModifierData
	{
		// Token: 0x04000A4A RID: 2634
		public float m_angleOffset;

		// Token: 0x04000A4B RID: 2635
		public Vector3 m_posOffset;

		// Token: 0x04000A4C RID: 2636
		public ChipmunkBodyC m_cmb;

		// Token: 0x04000A4D RID: 2637
		public Transform m_bone;

		// Token: 0x04000A4E RID: 2638
		public Transform m_parent;

		// Token: 0x04000A4F RID: 2639
		public float m_weight;
	}
}
