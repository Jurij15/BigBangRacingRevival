using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200057F RID: 1407
public class UnloadUnusedAssets : MonoBehaviour
{
	// Token: 0x06002905 RID: 10501 RVA: 0x001B3048 File Offset: 0x001B1448
	private IEnumerator Start()
	{
		AsyncOperation async = Resources.UnloadUnusedAssets();
		yield return async;
		Debug.Log("Unload assets complete.", null);
		this.isDone = true;
		yield break;
	}

	// Token: 0x04002E10 RID: 11792
	public bool isDone;
}
