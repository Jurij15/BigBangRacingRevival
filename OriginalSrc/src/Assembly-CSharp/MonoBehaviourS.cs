using System;

// Token: 0x02000504 RID: 1284
public static class MonoBehaviourS
{
	// Token: 0x0600250F RID: 9487 RVA: 0x00199984 File Offset: 0x00197D84
	public static void Initialize()
	{
		MonoBehaviourS.m_components = new DynamicArray<MonoBehaviourC>(100, 0.5f, 0.25f, 0.5f);
	}

	// Token: 0x06002510 RID: 9488 RVA: 0x001999A4 File Offset: 0x00197DA4
	public static MonoBehaviourC AddComponent(Entity _entity, IMonoBehaviour _monoBehaviour)
	{
		MonoBehaviourC monoBehaviourC = MonoBehaviourS.m_components.AddItem();
		monoBehaviourC.m_monoBehaviour = _monoBehaviour;
		monoBehaviourC.m_monoBehaviour.m_component = monoBehaviourC;
		EntityManager.AddComponentToEntity(_entity, monoBehaviourC);
		return monoBehaviourC;
	}

	// Token: 0x06002511 RID: 9489 RVA: 0x001999D7 File Offset: 0x00197DD7
	public static void RemoveComponent(MonoBehaviourC _c)
	{
		if (_c.p_entity == null)
		{
			Debug.LogWarning("Trying to remove component that has already been removed");
			return;
		}
		_c.m_monoBehaviour = null;
		EntityManager.RemoveComponentFromEntity(_c);
		MonoBehaviourS.m_components.RemoveItem(_c);
	}

	// Token: 0x06002512 RID: 9490 RVA: 0x00199A08 File Offset: 0x00197E08
	public static void Update()
	{
		for (int i = MonoBehaviourS.m_components.m_aliveCount - 1; i > -1; i--)
		{
			MonoBehaviourC monoBehaviourC = MonoBehaviourS.m_components.m_array[MonoBehaviourS.m_components.m_aliveIndices[i]];
			if (monoBehaviourC.m_active)
			{
				monoBehaviourC.m_monoBehaviour.TLUpdate();
			}
		}
		MonoBehaviourS.m_components.Update();
	}

	// Token: 0x04002AEC RID: 10988
	public static DynamicArray<MonoBehaviourC> m_components;
}
