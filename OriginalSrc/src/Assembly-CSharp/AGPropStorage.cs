using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200017D RID: 381
public class AGPropStorage : MonoBehaviour
{
	// Token: 0x06000C9D RID: 3229 RVA: 0x0007A720 File Offset: 0x00078B20
	public void Initialize()
	{
		for (int i = 0; i < this.m_propLibrary.Length; i++)
		{
			AGPropSettings component = this.m_propLibrary[i].gameObject.GetComponent<AGPropSettings>();
			if (component != null)
			{
				for (int j = 0; j < component.m_propability; j++)
				{
					this.m_propSettingsLookup.Add(component);
				}
			}
		}
	}

	// Token: 0x04000DA5 RID: 3493
	public float m_density = 0.5f;

	// Token: 0x04000DA6 RID: 3494
	public GameObject[] m_propLibrary;

	// Token: 0x04000DA7 RID: 3495
	[HideInInspector]
	public List<AGPropSettings> m_propSettingsLookup = new List<AGPropSettings>();
}
