using System;
using System.Collections.Generic;

// Token: 0x0200050D RID: 1293
public static class TimerS
{
	// Token: 0x060025AB RID: 9643 RVA: 0x0019FF3D File Offset: 0x0019E33D
	public static void Initialize()
	{
		TimerS.m_components = new DynamicArray<TimerC>(100, 0.5f, 0.25f, 0.5f);
		TimerS.m_removeEntityList = new List<Entity>();
		TimerS.m_removeComponentList = new List<TimerC>();
	}

	// Token: 0x060025AC RID: 9644 RVA: 0x0019FF70 File Offset: 0x0019E370
	public static TimerC AddComponent(Entity _entity, string _name, float _duration, float _delay, bool _destroyEntityOnTimeout, TimerComponentDelegate _timeoutHandler = null)
	{
		TimerC timerC = TimerS.m_components.AddItem();
		timerC.name = _name;
		timerC.duration = (double)_duration;
		timerC.currentTime = (double)(-(double)_delay);
		timerC.timeoutHandler = _timeoutHandler;
		timerC.isDone = false;
		timerC.destroyEntityOnTimeout = _destroyEntityOnTimeout;
		timerC.customComponent = null;
		EntityManager.AddComponentToEntity(_entity, timerC);
		return timerC;
	}

	// Token: 0x060025AD RID: 9645 RVA: 0x0019FFC8 File Offset: 0x0019E3C8
	public static void RemoveComponent(TimerC _c)
	{
		if (_c.p_entity == null)
		{
			Debug.LogWarning("Trying to remove component that has already been removed");
			return;
		}
		_c.timeoutHandler = null;
		EntityManager.RemoveComponentFromEntity(_c);
		if (!TimerS.m_updatingComponents)
		{
			TimerS.m_components.RemoveItem(_c);
		}
		else if (!TimerS.m_removeComponentList.Contains(_c))
		{
			TimerS.m_removeComponentList.Add(_c);
		}
	}

	// Token: 0x060025AE RID: 9646 RVA: 0x001A0030 File Offset: 0x0019E430
	public static void Update()
	{
		TimerS.m_updatingComponents = true;
		for (int i = TimerS.m_components.m_aliveCount - 1; i > -1; i--)
		{
			TimerC timerC = TimerS.m_components.m_array[TimerS.m_components.m_aliveIndices[i]];
			if (timerC.m_active)
			{
				timerC.currentTime += (double)((!timerC.useUnscaledDeltaTime) ? Main.m_gameDeltaTime : Main.m_dt);
				if (timerC.currentTime >= timerC.duration)
				{
					timerC.isDone = true;
					if (timerC.timeoutHandler != null)
					{
						timerC.timeoutHandler(timerC);
					}
					if (timerC.destroyEntityOnTimeout && !TimerS.m_removeEntityList.Contains(timerC.p_entity))
					{
						TimerS.m_removeEntityList.Add(timerC.p_entity);
					}
				}
			}
		}
		TimerS.m_updatingComponents = false;
		while (TimerS.m_removeComponentList.Count > 0)
		{
			int num = TimerS.m_removeComponentList.Count - 1;
			TimerS.m_components.RemoveItem(TimerS.m_removeComponentList[num]);
			TimerS.m_removeComponentList.RemoveAt(num);
		}
		while (TimerS.m_removeEntityList.Count > 0)
		{
			int num2 = TimerS.m_removeEntityList.Count - 1;
			EntityManager.RemoveEntity(TimerS.m_removeEntityList[num2]);
			TimerS.m_removeEntityList.RemoveAt(num2);
		}
		TimerS.m_components.Update();
	}

	// Token: 0x04002B2B RID: 11051
	public static DynamicArray<TimerC> m_components;

	// Token: 0x04002B2C RID: 11052
	private static List<Entity> m_removeEntityList;

	// Token: 0x04002B2D RID: 11053
	private static List<TimerC> m_removeComponentList;

	// Token: 0x04002B2E RID: 11054
	private static bool m_updatingComponents;
}
