using System;
using CodeStage.AntiCheat.Detectors;
using UnityEngine;

namespace CodeStage.AntiCheat.ObscuredTypes
{
	// Token: 0x02000497 RID: 1175
	[Serializable]
	public struct ObscuredVector2
	{
		// Token: 0x06002162 RID: 8546 RVA: 0x00188519 File Offset: 0x00186919
		private ObscuredVector2(ObscuredVector2.RawEncryptedVector2 value)
		{
			this.currentCryptoKey = ObscuredVector2.cryptoKey;
			this.hiddenValue = value;
			this.fakeValue = ObscuredVector2.initialFakeValue;
			this.inited = true;
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06002163 RID: 8547 RVA: 0x00188540 File Offset: 0x00186940
		// (set) Token: 0x06002164 RID: 8548 RVA: 0x001885B5 File Offset: 0x001869B5
		public float x
		{
			get
			{
				float num = this.InternalDecryptField(this.hiddenValue.x);
				if (ObscuredCheatingDetector.IsRunning && !this.fakeValue.Equals(ObscuredVector2.initialFakeValue) && Math.Abs(num - this.fakeValue.x) > ObscuredCheatingDetector.Instance.vector2Epsilon)
				{
					ObscuredCheatingDetector.Instance.OnCheatingDetected();
				}
				return num;
			}
			set
			{
				this.hiddenValue.x = this.InternalEncryptField(value);
				if (ObscuredCheatingDetector.IsRunning)
				{
					this.fakeValue.x = value;
				}
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06002165 RID: 8549 RVA: 0x001885E0 File Offset: 0x001869E0
		// (set) Token: 0x06002166 RID: 8550 RVA: 0x00188655 File Offset: 0x00186A55
		public float y
		{
			get
			{
				float num = this.InternalDecryptField(this.hiddenValue.y);
				if (ObscuredCheatingDetector.IsRunning && !this.fakeValue.Equals(ObscuredVector2.initialFakeValue) && Math.Abs(num - this.fakeValue.y) > ObscuredCheatingDetector.Instance.vector2Epsilon)
				{
					ObscuredCheatingDetector.Instance.OnCheatingDetected();
				}
				return num;
			}
			set
			{
				this.hiddenValue.y = this.InternalEncryptField(value);
				if (ObscuredCheatingDetector.IsRunning)
				{
					this.fakeValue.y = value;
				}
			}
		}

		// Token: 0x17000093 RID: 147
		public float this[int index]
		{
			get
			{
				if (index == 0)
				{
					return this.x;
				}
				if (index != 1)
				{
					throw new IndexOutOfRangeException("Invalid ObscuredVector2 index!");
				}
				return this.y;
			}
			set
			{
				if (index != 0)
				{
					if (index != 1)
					{
						throw new IndexOutOfRangeException("Invalid ObscuredVector2 index!");
					}
					this.y = value;
				}
				else
				{
					this.x = value;
				}
			}
		}

		// Token: 0x06002169 RID: 8553 RVA: 0x001886E2 File Offset: 0x00186AE2
		public static void SetNewCryptoKey(int newKey)
		{
			ObscuredVector2.cryptoKey = newKey;
		}

		// Token: 0x0600216A RID: 8554 RVA: 0x001886EA File Offset: 0x00186AEA
		public static ObscuredVector2.RawEncryptedVector2 Encrypt(Vector2 value)
		{
			return ObscuredVector2.Encrypt(value, 0);
		}

		// Token: 0x0600216B RID: 8555 RVA: 0x001886F4 File Offset: 0x00186AF4
		public static ObscuredVector2.RawEncryptedVector2 Encrypt(Vector2 value, int key)
		{
			if (key == 0)
			{
				key = ObscuredVector2.cryptoKey;
			}
			ObscuredVector2.RawEncryptedVector2 rawEncryptedVector;
			rawEncryptedVector.x = ObscuredFloat.Encrypt(value.x, key);
			rawEncryptedVector.y = ObscuredFloat.Encrypt(value.y, key);
			return rawEncryptedVector;
		}

		// Token: 0x0600216C RID: 8556 RVA: 0x00188737 File Offset: 0x00186B37
		public static Vector2 Decrypt(ObscuredVector2.RawEncryptedVector2 value)
		{
			return ObscuredVector2.Decrypt(value, 0);
		}

		// Token: 0x0600216D RID: 8557 RVA: 0x00188740 File Offset: 0x00186B40
		public static Vector2 Decrypt(ObscuredVector2.RawEncryptedVector2 value, int key)
		{
			if (key == 0)
			{
				key = ObscuredVector2.cryptoKey;
			}
			Vector2 vector;
			vector.x = ObscuredFloat.Decrypt(value.x, key);
			vector.y = ObscuredFloat.Decrypt(value.y, key);
			return vector;
		}

		// Token: 0x0600216E RID: 8558 RVA: 0x00188783 File Offset: 0x00186B83
		public void ApplyNewCryptoKey()
		{
			if (this.currentCryptoKey != ObscuredVector2.cryptoKey)
			{
				this.hiddenValue = ObscuredVector2.Encrypt(this.InternalDecrypt(), ObscuredVector2.cryptoKey);
				this.currentCryptoKey = ObscuredVector2.cryptoKey;
			}
		}

		// Token: 0x0600216F RID: 8559 RVA: 0x001887B8 File Offset: 0x00186BB8
		public void RandomizeCryptoKey()
		{
			Vector2 vector = this.InternalDecrypt();
			this.currentCryptoKey = Random.seed;
			this.hiddenValue = ObscuredVector2.Encrypt(vector, this.currentCryptoKey);
		}

		// Token: 0x06002170 RID: 8560 RVA: 0x001887E9 File Offset: 0x00186BE9
		public ObscuredVector2.RawEncryptedVector2 GetEncrypted()
		{
			this.ApplyNewCryptoKey();
			return this.hiddenValue;
		}

		// Token: 0x06002171 RID: 8561 RVA: 0x001887F7 File Offset: 0x00186BF7
		public void SetEncrypted(ObscuredVector2.RawEncryptedVector2 encrypted)
		{
			this.inited = true;
			this.hiddenValue = encrypted;
			if (ObscuredCheatingDetector.IsRunning)
			{
				this.fakeValue = this.InternalDecrypt();
			}
		}

		// Token: 0x06002172 RID: 8562 RVA: 0x00188820 File Offset: 0x00186C20
		private Vector2 InternalDecrypt()
		{
			if (!this.inited)
			{
				this.currentCryptoKey = ObscuredVector2.cryptoKey;
				this.hiddenValue = ObscuredVector2.Encrypt(ObscuredVector2.initialFakeValue);
				this.fakeValue = ObscuredVector2.initialFakeValue;
				this.inited = true;
			}
			Vector2 vector;
			vector.x = ObscuredFloat.Decrypt(this.hiddenValue.x, this.currentCryptoKey);
			vector.y = ObscuredFloat.Decrypt(this.hiddenValue.y, this.currentCryptoKey);
			if (ObscuredCheatingDetector.IsRunning && !this.fakeValue.Equals(ObscuredVector2.initialFakeValue) && !this.CompareVectorsWithTolerance(vector, this.fakeValue))
			{
				ObscuredCheatingDetector.Instance.OnCheatingDetected();
			}
			return vector;
		}

		// Token: 0x06002173 RID: 8563 RVA: 0x001888E8 File Offset: 0x00186CE8
		private bool CompareVectorsWithTolerance(Vector2 vector1, Vector2 vector2)
		{
			float vector2Epsilon = ObscuredCheatingDetector.Instance.vector2Epsilon;
			return Math.Abs(vector1.x - vector2.x) < vector2Epsilon && Math.Abs(vector1.y - vector2.y) < vector2Epsilon;
		}

		// Token: 0x06002174 RID: 8564 RVA: 0x00188934 File Offset: 0x00186D34
		private float InternalDecryptField(int encrypted)
		{
			int num = ObscuredVector2.cryptoKey;
			if (this.currentCryptoKey != ObscuredVector2.cryptoKey)
			{
				num = this.currentCryptoKey;
			}
			return ObscuredFloat.Decrypt(encrypted, num);
		}

		// Token: 0x06002175 RID: 8565 RVA: 0x00188968 File Offset: 0x00186D68
		private int InternalEncryptField(float encrypted)
		{
			return ObscuredFloat.Encrypt(encrypted, ObscuredVector2.cryptoKey);
		}

		// Token: 0x06002176 RID: 8566 RVA: 0x00188984 File Offset: 0x00186D84
		public static implicit operator ObscuredVector2(Vector2 value)
		{
			ObscuredVector2 obscuredVector = new ObscuredVector2(ObscuredVector2.Encrypt(value));
			if (ObscuredCheatingDetector.IsRunning)
			{
				obscuredVector.fakeValue = value;
			}
			return obscuredVector;
		}

		// Token: 0x06002177 RID: 8567 RVA: 0x001889B1 File Offset: 0x00186DB1
		public static implicit operator Vector2(ObscuredVector2 value)
		{
			return value.InternalDecrypt();
		}

		// Token: 0x06002178 RID: 8568 RVA: 0x001889BC File Offset: 0x00186DBC
		public static implicit operator Vector3(ObscuredVector2 value)
		{
			Vector2 vector = value.InternalDecrypt();
			return new Vector3(vector.x, vector.y, 0f);
		}

		// Token: 0x06002179 RID: 8569 RVA: 0x001889EC File Offset: 0x00186DEC
		public override int GetHashCode()
		{
			return this.InternalDecrypt().GetHashCode();
		}

		// Token: 0x0600217A RID: 8570 RVA: 0x00188A10 File Offset: 0x00186E10
		public override string ToString()
		{
			return this.InternalDecrypt().ToString();
		}

		// Token: 0x0600217B RID: 8571 RVA: 0x00188A34 File Offset: 0x00186E34
		public string ToString(string format)
		{
			return this.InternalDecrypt().ToString(format);
		}

		// Token: 0x040027D7 RID: 10199
		private static int cryptoKey = 120206;

		// Token: 0x040027D8 RID: 10200
		private static readonly Vector2 initialFakeValue = Vector2.zero;

		// Token: 0x040027D9 RID: 10201
		[SerializeField]
		private int currentCryptoKey;

		// Token: 0x040027DA RID: 10202
		[SerializeField]
		private ObscuredVector2.RawEncryptedVector2 hiddenValue;

		// Token: 0x040027DB RID: 10203
		[SerializeField]
		private Vector2 fakeValue;

		// Token: 0x040027DC RID: 10204
		[SerializeField]
		private bool inited;

		// Token: 0x02000498 RID: 1176
		[Serializable]
		public struct RawEncryptedVector2
		{
			// Token: 0x040027DD RID: 10205
			public int x;

			// Token: 0x040027DE RID: 10206
			public int y;
		}
	}
}
