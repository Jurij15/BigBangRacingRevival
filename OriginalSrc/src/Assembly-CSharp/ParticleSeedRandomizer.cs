using System;
using UnityEngine;

// Token: 0x02000151 RID: 337
public class ParticleSeedRandomizer : MonoBehaviour
{
	// Token: 0x06000BA9 RID: 2985 RVA: 0x00073CEC File Offset: 0x000720EC
	private void Awake()
	{
		if (base.gameObject)
		{
			int num = Random.Range(0, 99999);
			base.gameObject.GetComponent<ParticleSystem>().randomSeed = (uint)num;
			Debug.LogError("Gameobject: " + base.gameObject.ToString() + ", name: " + base.gameObject.name.ToString());
		}
	}

	// Token: 0x06000BAA RID: 2986 RVA: 0x00073D55 File Offset: 0x00072155
	private void Update()
	{
	}
}
