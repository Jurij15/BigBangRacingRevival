using System;
using UnityEngine;

// Token: 0x020004C0 RID: 1216
public class ParticleRandomSeed : MonoBehaviour
{
	// Token: 0x060022A3 RID: 8867 RVA: 0x00190A3C File Offset: 0x0018EE3C
	private void Awake()
	{
		base.gameObject.GetComponent<ParticleSystem>().randomSeed = (uint)Random.Range(-99999, 99999);
		Debug.LogError("Gameobject: " + base.gameObject.ToString() + ", name: " + base.gameObject.name.ToString());
	}

	// Token: 0x060022A4 RID: 8868 RVA: 0x00190A97 File Offset: 0x0018EE97
	private void Update()
	{
	}
}
