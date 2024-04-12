using System;
using UnityEngine;

// Token: 0x02000184 RID: 388
public class UnsyncAnimation : MonoBehaviour
{
	// Token: 0x06000CAD RID: 3245 RVA: 0x0007AAE9 File Offset: 0x00078EE9
	private void Start()
	{
		base.GetComponent<Animation>()[base.GetComponent<Animation>().clip.name].normalizedTime = Random.Range(0f, 1f);
	}
}
