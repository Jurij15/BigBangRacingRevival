using System;
using CodeStage.AntiCheat.Detectors;
using UnityEngine;

namespace CodeStage.AntiCheat.ObscuredTypes
{
	// Token: 0x0200048F RID: 1167
	[Serializable]
	public struct ObscuredQuaternion
	{
		// Token: 0x060020C9 RID: 8393 RVA: 0x00186C8B File Offset: 0x0018508B
		private ObscuredQuaternion(ObscuredQuaternion.RawEncryptedQuaternion value)
		{
			this.currentCryptoKey = ObscuredQuaternion.cryptoKey;
			this.hiddenValue = value;
			this.fakeValue = ObscuredQuaternion.initialFakeValue;
			this.inited = true;
		}

		// Token: 0x060020CA RID: 8394 RVA: 0x00186CB1 File Offset: 0x001850B1
		public static void SetNewCryptoKey(int newKey)
		{
			ObscuredQuaternion.cryptoKey = newKey;
		}

		// Token: 0x060020CB RID: 8395 RVA: 0x00186CB9 File Offset: 0x001850B9
		public static ObscuredQuaternion.RawEncryptedQuaternion Encrypt(Quaternion value)
		{
			return ObscuredQuaternion.Encrypt(value, 0);
		}

		// Token: 0x060020CC RID: 8396 RVA: 0x00186CC4 File Offset: 0x001850C4
		public static ObscuredQuaternion.RawEncryptedQuaternion Encrypt(Quaternion value, int key)
		{
			if (key == 0)
			{
				key = ObscuredQuaternion.cryptoKey;
			}
			ObscuredQuaternion.RawEncryptedQuaternion rawEncryptedQuaternion;
			rawEncryptedQuaternion.x = ObscuredFloat.Encrypt(value.x, key);
			rawEncryptedQuaternion.y = ObscuredFloat.Encrypt(value.y, key);
			rawEncryptedQuaternion.z = ObscuredFloat.Encrypt(value.z, key);
			rawEncryptedQuaternion.w = ObscuredFloat.Encrypt(value.w, key);
			return rawEncryptedQuaternion;
		}

		// Token: 0x060020CD RID: 8397 RVA: 0x00186D2F File Offset: 0x0018512F
		public static Quaternion Decrypt(ObscuredQuaternion.RawEncryptedQuaternion value)
		{
			return ObscuredQuaternion.Decrypt(value, 0);
		}

		// Token: 0x060020CE RID: 8398 RVA: 0x00186D38 File Offset: 0x00185138
		public static Quaternion Decrypt(ObscuredQuaternion.RawEncryptedQuaternion value, int key)
		{
			if (key == 0)
			{
				key = ObscuredQuaternion.cryptoKey;
			}
			Quaternion quaternion;
			quaternion.x = ObscuredFloat.Decrypt(value.x, key);
			quaternion.y = ObscuredFloat.Decrypt(value.y, key);
			quaternion.z = ObscuredFloat.Decrypt(value.z, key);
			quaternion.w = ObscuredFloat.Decrypt(value.w, key);
			return quaternion;
		}

		// Token: 0x060020CF RID: 8399 RVA: 0x00186DA3 File Offset: 0x001851A3
		public void ApplyNewCryptoKey()
		{
			if (this.currentCryptoKey != ObscuredQuaternion.cryptoKey)
			{
				this.hiddenValue = ObscuredQuaternion.Encrypt(this.InternalDecrypt(), ObscuredQuaternion.cryptoKey);
				this.currentCryptoKey = ObscuredQuaternion.cryptoKey;
			}
		}

		// Token: 0x060020D0 RID: 8400 RVA: 0x00186DD8 File Offset: 0x001851D8
		public void RandomizeCryptoKey()
		{
			Quaternion quaternion = this.InternalDecrypt();
			this.currentCryptoKey = Random.seed;
			this.hiddenValue = ObscuredQuaternion.Encrypt(quaternion, this.currentCryptoKey);
		}

		// Token: 0x060020D1 RID: 8401 RVA: 0x00186E09 File Offset: 0x00185209
		public ObscuredQuaternion.RawEncryptedQuaternion GetEncrypted()
		{
			this.ApplyNewCryptoKey();
			return this.hiddenValue;
		}

		// Token: 0x060020D2 RID: 8402 RVA: 0x00186E17 File Offset: 0x00185217
		public void SetEncrypted(ObscuredQuaternion.RawEncryptedQuaternion encrypted)
		{
			this.inited = true;
			this.hiddenValue = encrypted;
			if (ObscuredCheatingDetector.IsRunning)
			{
				this.fakeValue = this.InternalDecrypt();
			}
		}

		// Token: 0x060020D3 RID: 8403 RVA: 0x00186E40 File Offset: 0x00185240
		private Quaternion InternalDecrypt()
		{
			if (!this.inited)
			{
				this.currentCryptoKey = ObscuredQuaternion.cryptoKey;
				this.hiddenValue = ObscuredQuaternion.Encrypt(ObscuredQuaternion.initialFakeValue);
				this.fakeValue = ObscuredQuaternion.initialFakeValue;
				this.inited = true;
			}
			Quaternion quaternion;
			quaternion.x = ObscuredFloat.Decrypt(this.hiddenValue.x, this.currentCryptoKey);
			quaternion.y = ObscuredFloat.Decrypt(this.hiddenValue.y, this.currentCryptoKey);
			quaternion.z = ObscuredFloat.Decrypt(this.hiddenValue.z, this.currentCryptoKey);
			quaternion.w = ObscuredFloat.Decrypt(this.hiddenValue.w, this.currentCryptoKey);
			if (ObscuredCheatingDetector.IsRunning && !this.fakeValue.Equals(ObscuredQuaternion.initialFakeValue) && !this.CompareQuaternionsWithTolerance(quaternion, this.fakeValue))
			{
				ObscuredCheatingDetector.Instance.OnCheatingDetected();
			}
			return quaternion;
		}

		// Token: 0x060020D4 RID: 8404 RVA: 0x00186F40 File Offset: 0x00185340
		private bool CompareQuaternionsWithTolerance(Quaternion q1, Quaternion q2)
		{
			float quaternionEpsilon = ObscuredCheatingDetector.Instance.quaternionEpsilon;
			return Math.Abs(q1.x - q2.x) < quaternionEpsilon && Math.Abs(q1.y - q2.y) < quaternionEpsilon && Math.Abs(q1.z - q2.z) < quaternionEpsilon && Math.Abs(q1.w - q2.w) < quaternionEpsilon;
		}

		// Token: 0x060020D5 RID: 8405 RVA: 0x00186FC0 File Offset: 0x001853C0
		public static implicit operator ObscuredQuaternion(Quaternion value)
		{
			ObscuredQuaternion obscuredQuaternion = new ObscuredQuaternion(ObscuredQuaternion.Encrypt(value));
			if (ObscuredCheatingDetector.IsRunning)
			{
				obscuredQuaternion.fakeValue = value;
			}
			return obscuredQuaternion;
		}

		// Token: 0x060020D6 RID: 8406 RVA: 0x00186FED File Offset: 0x001853ED
		public static implicit operator Quaternion(ObscuredQuaternion value)
		{
			return value.InternalDecrypt();
		}

		// Token: 0x060020D7 RID: 8407 RVA: 0x00186FF8 File Offset: 0x001853F8
		public override int GetHashCode()
		{
			return this.InternalDecrypt().GetHashCode();
		}

		// Token: 0x060020D8 RID: 8408 RVA: 0x0018701C File Offset: 0x0018541C
		public override string ToString()
		{
			return this.InternalDecrypt().ToString();
		}

		// Token: 0x060020D9 RID: 8409 RVA: 0x00187040 File Offset: 0x00185440
		public string ToString(string format)
		{
			return this.InternalDecrypt().ToString(format);
		}

		// Token: 0x040027AF RID: 10159
		private static int cryptoKey = 120205;

		// Token: 0x040027B0 RID: 10160
		private static readonly Quaternion initialFakeValue = Quaternion.identity;

		// Token: 0x040027B1 RID: 10161
		[SerializeField]
		private int currentCryptoKey;

		// Token: 0x040027B2 RID: 10162
		[SerializeField]
		private ObscuredQuaternion.RawEncryptedQuaternion hiddenValue;

		// Token: 0x040027B3 RID: 10163
		[SerializeField]
		private Quaternion fakeValue;

		// Token: 0x040027B4 RID: 10164
		[SerializeField]
		private bool inited;

		// Token: 0x02000490 RID: 1168
		[Serializable]
		public struct RawEncryptedQuaternion
		{
			// Token: 0x040027B5 RID: 10165
			public int x;

			// Token: 0x040027B6 RID: 10166
			public int y;

			// Token: 0x040027B7 RID: 10167
			public int z;

			// Token: 0x040027B8 RID: 10168
			public int w;
		}
	}
}
