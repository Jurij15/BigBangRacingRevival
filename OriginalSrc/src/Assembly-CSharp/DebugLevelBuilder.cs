using System;
using UnityEngine;

// Token: 0x0200008F RID: 143
public class DebugLevelBuilder : MonoBehaviour
{
	// Token: 0x06000319 RID: 793 RVA: 0x0002FDDB File Offset: 0x0002E1DB
	private void Start()
	{
		this.levelBuilder = this.currentMap.GetComponent("LevelBuilder") as LevelBuilder;
		this.levelBuilder.InitializeBackground(Random.Range(1, 1000), 1f);
	}

	// Token: 0x040003F2 RID: 1010
	public GameObject currentMap;

	// Token: 0x040003F3 RID: 1011
	private LevelBuilder levelBuilder;
}
