using System;
using System.Runtime.Serialization;

// Token: 0x020004E4 RID: 1252
public interface IComponent : IPoolable, ISerializable
{
	// Token: 0x170000B3 RID: 179
	// (get) Token: 0x06002332 RID: 9010
	// (set) Token: 0x06002333 RID: 9011
	bool m_active { get; set; }

	// Token: 0x170000B4 RID: 180
	// (get) Token: 0x06002334 RID: 9012
	// (set) Token: 0x06002335 RID: 9013
	bool m_wasActive { get; set; }

	// Token: 0x170000B5 RID: 181
	// (get) Token: 0x06002336 RID: 9014
	// (set) Token: 0x06002337 RID: 9015
	int m_identifier { get; set; }

	// Token: 0x170000B6 RID: 182
	// (get) Token: 0x06002338 RID: 9016
	// (set) Token: 0x06002339 RID: 9017
	Entity p_entity { get; set; }

	// Token: 0x170000B7 RID: 183
	// (get) Token: 0x0600233A RID: 9018
	// (set) Token: 0x0600233B RID: 9019
	ComponentType m_componentType { get; set; }
}
