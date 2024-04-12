using System;
using System.Collections.Generic;

// Token: 0x0200053E RID: 1342
public interface IAssembledClass
{
	// Token: 0x170000CD RID: 205
	// (get) Token: 0x06002780 RID: 10112
	// (set) Token: 0x06002781 RID: 10113
	GraphElement m_graphElement { get; set; }

	// Token: 0x170000CE RID: 206
	// (get) Token: 0x06002782 RID: 10114
	// (set) Token: 0x06002783 RID: 10115
	List<Entity> m_assembledEntities { get; set; }

	// Token: 0x06002784 RID: 10116
	void SyncPositionToGraphElementPosition();

	// Token: 0x06002785 RID: 10117
	void Destroy();
}
