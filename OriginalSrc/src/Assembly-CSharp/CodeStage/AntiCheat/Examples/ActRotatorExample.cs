using System;
using UnityEngine;

namespace CodeStage.AntiCheat.Examples
{
	// Token: 0x02000475 RID: 1141
	[AddComponentMenu("")]
	public class ActRotatorExample : MonoBehaviour
	{
		// Token: 0x06001F08 RID: 7944 RVA: 0x0017D97E File Offset: 0x0017BD7E
		private void Update()
		{
			base.transform.Rotate(0f, this.speed * Time.deltaTime, 0f);
		}

		// Token: 0x040026A2 RID: 9890
		[Range(1f, 100f)]
		public float speed = 5f;
	}
}
