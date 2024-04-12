using System;

// Token: 0x020004CA RID: 1226
public class MonoBehaviourC : BasicComponent
{
	// Token: 0x060022D1 RID: 8913 RVA: 0x00191058 File Offset: 0x0018F458
	public MonoBehaviourC()
		: base(ComponentType.MonoBehaviour)
	{
		MonoBehaviourC.m_componentCount++;
	}

	// Token: 0x060022D2 RID: 8914 RVA: 0x0019106E File Offset: 0x0018F46E
	public override void Reset()
	{
		base.Reset();
		this.m_monoBehaviour = null;
	}

	// Token: 0x060022D3 RID: 8915 RVA: 0x00191080 File Offset: 0x0018F480
	~MonoBehaviourC()
	{
		MonoBehaviourC.m_componentCount--;
	}

	// Token: 0x04002914 RID: 10516
	public static int m_componentCount;

	// Token: 0x04002915 RID: 10517
	public IMonoBehaviour m_monoBehaviour;
}
