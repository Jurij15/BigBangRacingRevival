using System;
using CodeStage.AntiCheat.Detectors;
using UnityEngine;

namespace CodeStage.AntiCheat.ObscuredTypes
{
	// Token: 0x02000499 RID: 1177
	[Serializable]
	public struct ObscuredVector3
	{
		// Token: 0x0600217D RID: 8573 RVA: 0x00188A66 File Offset: 0x00186E66
		private ObscuredVector3(ObscuredVector3.RawEncryptedVector3 encrypted)
		{
			this.currentCryptoKey = ObscuredVector3.cryptoKey;
			this.hiddenValue = encrypted;
			this.fakeValue = ObscuredVector3.initialFakeValue;
			this.inited = true;
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x0600217E RID: 8574 RVA: 0x00188A8C File Offset: 0x00186E8C
		// (set) Token: 0x0600217F RID: 8575 RVA: 0x00188B01 File Offset: 0x00186F01
		public float x
		{
			get
			{
				float num = this.InternalDecryptField(this.hiddenValue.x);
				if (ObscuredCheatingDetector.IsRunning && !this.fakeValue.Equals(ObscuredVector3.initialFakeValue) && Math.Abs(num - this.fakeValue.x) > ObscuredCheatingDetector.Instance.vector3Epsilon)
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

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06002180 RID: 8576 RVA: 0x00188B2C File Offset: 0x00186F2C
		// (set) Token: 0x06002181 RID: 8577 RVA: 0x00188BA1 File Offset: 0x00186FA1
		public float y
		{
			get
			{
				float num = this.InternalDecryptField(this.hiddenValue.y);
				if (ObscuredCheatingDetector.IsRunning && !this.fakeValue.Equals(ObscuredVector3.initialFakeValue) && Math.Abs(num - this.fakeValue.y) > ObscuredCheatingDetector.Instance.vector3Epsilon)
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

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06002182 RID: 8578 RVA: 0x00188BCC File Offset: 0x00186FCC
		// (set) Token: 0x06002183 RID: 8579 RVA: 0x00188C41 File Offset: 0x00187041
		public float z
		{
			get
			{
				float num = this.InternalDecryptField(this.hiddenValue.z);
				if (ObscuredCheatingDetector.IsRunning && !this.fakeValue.Equals(ObscuredVector3.initialFakeValue) && Math.Abs(num - this.fakeValue.z) > ObscuredCheatingDetector.Instance.vector3Epsilon)
				{
					ObscuredCheatingDetector.Instance.OnCheatingDetected();
				}
				return num;
			}
			set
			{
				this.hiddenValue.z = this.InternalEncryptField(value);
				if (ObscuredCheatingDetector.IsRunning)
				{
					this.fakeValue.z = value;
				}
			}
		}

		// Token: 0x17000097 RID: 151
		public float this[int index]
		{
			get
			{
				switch (index)
				{
				case 0:
					return this.x;
				case 1:
					return this.y;
				case 2:
					return this.z;
				default:
					throw new IndexOutOfRangeException("Invalid ObscuredVector3 index!");
				}
			}
			set
			{
				switch (index)
				{
				case 0:
					this.x = value;
					break;
				case 1:
					this.y = value;
					break;
				case 2:
					this.z = value;
					break;
				default:
					throw new IndexOutOfRangeException("Invalid ObscuredVector3 index!");
				}
			}
		}

		// Token: 0x06002186 RID: 8582 RVA: 0x00188CF7 File Offset: 0x001870F7
		public static void SetNewCryptoKey(int newKey)
		{
			ObscuredVector3.cryptoKey = newKey;
		}

		// Token: 0x06002187 RID: 8583 RVA: 0x00188CFF File Offset: 0x001870FF
		public static ObscuredVector3.RawEncryptedVector3 Encrypt(Vector3 value)
		{
			return ObscuredVector3.Encrypt(value, 0);
		}

		// Token: 0x06002188 RID: 8584 RVA: 0x00188D08 File Offset: 0x00187108
		public static ObscuredVector3.RawEncryptedVector3 Encrypt(Vector3 value, int key)
		{
			if (key == 0)
			{
				key = ObscuredVector3.cryptoKey;
			}
			ObscuredVector3.RawEncryptedVector3 rawEncryptedVector;
			rawEncryptedVector.x = ObscuredFloat.Encrypt(value.x, key);
			rawEncryptedVector.y = ObscuredFloat.Encrypt(value.y, key);
			rawEncryptedVector.z = ObscuredFloat.Encrypt(value.z, key);
			return rawEncryptedVector;
		}

		// Token: 0x06002189 RID: 8585 RVA: 0x00188D5F File Offset: 0x0018715F
		public static Vector3 Decrypt(ObscuredVector3.RawEncryptedVector3 value)
		{
			return ObscuredVector3.Decrypt(value, 0);
		}

		// Token: 0x0600218A RID: 8586 RVA: 0x00188D68 File Offset: 0x00187168
		public static Vector3 Decrypt(ObscuredVector3.RawEncryptedVector3 value, int key)
		{
			if (key == 0)
			{
				key = ObscuredVector3.cryptoKey;
			}
			Vector3 vector;
			vector.x = ObscuredFloat.Decrypt(value.x, key);
			vector.y = ObscuredFloat.Decrypt(value.y, key);
			vector.z = ObscuredFloat.Decrypt(value.z, key);
			return vector;
		}

		// Token: 0x0600218B RID: 8587 RVA: 0x00188DBF File Offset: 0x001871BF
		public void ApplyNewCryptoKey()
		{
			if (this.currentCryptoKey != ObscuredVector3.cryptoKey)
			{
				this.hiddenValue = ObscuredVector3.Encrypt(this.InternalDecrypt(), ObscuredVector3.cryptoKey);
				this.currentCryptoKey = ObscuredVector3.cryptoKey;
			}
		}

		// Token: 0x0600218C RID: 8588 RVA: 0x00188DF4 File Offset: 0x001871F4
		public void RandomizeCryptoKey()
		{
			Vector3 vector = this.InternalDecrypt();
			this.currentCryptoKey = Random.seed;
			this.hiddenValue = ObscuredVector3.Encrypt(vector, this.currentCryptoKey);
		}

		// Token: 0x0600218D RID: 8589 RVA: 0x00188E25 File Offset: 0x00187225
		public ObscuredVector3.RawEncryptedVector3 GetEncrypted()
		{
			this.ApplyNewCryptoKey();
			return this.hiddenValue;
		}

		// Token: 0x0600218E RID: 8590 RVA: 0x00188E33 File Offset: 0x00187233
		public void SetEncrypted(ObscuredVector3.RawEncryptedVector3 encrypted)
		{
			this.inited = true;
			this.hiddenValue = encrypted;
			if (ObscuredCheatingDetector.IsRunning)
			{
				this.fakeValue = this.InternalDecrypt();
			}
		}

		// Token: 0x0600218F RID: 8591 RVA: 0x00188E5C File Offset: 0x0018725C
		private Vector3 InternalDecrypt()
		{
			if (!this.inited)
			{
				this.currentCryptoKey = ObscuredVector3.cryptoKey;
				this.hiddenValue = ObscuredVector3.Encrypt(ObscuredVector3.initialFakeValue, ObscuredVector3.cryptoKey);
				this.fakeValue = ObscuredVector3.initialFakeValue;
				this.inited = true;
			}
			Vector3 vector;
			vector.x = ObscuredFloat.Decrypt(this.hiddenValue.x, this.currentCryptoKey);
			vector.y = ObscuredFloat.Decrypt(this.hiddenValue.y, this.currentCryptoKey);
			vector.z = ObscuredFloat.Decrypt(this.hiddenValue.z, this.currentCryptoKey);
			if (ObscuredCheatingDetector.IsRunning && !this.fakeValue.Equals(Vector3.zero) && !this.CompareVectorsWithTolerance(vector, this.fakeValue))
			{
				ObscuredCheatingDetector.Instance.OnCheatingDetected();
			}
			return vector;
		}

		// Token: 0x06002190 RID: 8592 RVA: 0x00188F44 File Offset: 0x00187344
		private bool CompareVectorsWithTolerance(Vector3 vector1, Vector3 vector2)
		{
			float vector3Epsilon = ObscuredCheatingDetector.Instance.vector3Epsilon;
			return Math.Abs(vector1.x - vector2.x) < vector3Epsilon && Math.Abs(vector1.y - vector2.y) < vector3Epsilon && Math.Abs(vector1.z - vector2.z) < vector3Epsilon;
		}

		// Token: 0x06002191 RID: 8593 RVA: 0x00188FAC File Offset: 0x001873AC
		private float InternalDecryptField(int encrypted)
		{
			int num = ObscuredVector3.cryptoKey;
			if (this.currentCryptoKey != ObscuredVector3.cryptoKey)
			{
				num = this.currentCryptoKey;
			}
			return ObscuredFloat.Decrypt(encrypted, num);
		}

		// Token: 0x06002192 RID: 8594 RVA: 0x00188FE0 File Offset: 0x001873E0
		private int InternalEncryptField(float encrypted)
		{
			return ObscuredFloat.Encrypt(encrypted, ObscuredVector3.cryptoKey);
		}

		// Token: 0x06002193 RID: 8595 RVA: 0x00188FFC File Offset: 0x001873FC
		public static implicit operator ObscuredVector3(Vector3 value)
		{
			ObscuredVector3 obscuredVector = new ObscuredVector3(ObscuredVector3.Encrypt(value, ObscuredVector3.cryptoKey));
			if (ObscuredCheatingDetector.IsRunning)
			{
				obscuredVector.fakeValue = value;
			}
			return obscuredVector;
		}

		// Token: 0x06002194 RID: 8596 RVA: 0x0018902E File Offset: 0x0018742E
		public static implicit operator Vector3(ObscuredVector3 value)
		{
			return value.InternalDecrypt();
		}

		// Token: 0x06002195 RID: 8597 RVA: 0x00189037 File Offset: 0x00187437
		public static ObscuredVector3 operator +(ObscuredVector3 a, ObscuredVector3 b)
		{
			return a.InternalDecrypt() + b.InternalDecrypt();
		}

		// Token: 0x06002196 RID: 8598 RVA: 0x00189051 File Offset: 0x00187451
		public static ObscuredVector3 operator +(Vector3 a, ObscuredVector3 b)
		{
			return a + b.InternalDecrypt();
		}

		// Token: 0x06002197 RID: 8599 RVA: 0x00189065 File Offset: 0x00187465
		public static ObscuredVector3 operator +(ObscuredVector3 a, Vector3 b)
		{
			return a.InternalDecrypt() + b;
		}

		// Token: 0x06002198 RID: 8600 RVA: 0x00189079 File Offset: 0x00187479
		public static ObscuredVector3 operator -(ObscuredVector3 a, ObscuredVector3 b)
		{
			return a.InternalDecrypt() - b.InternalDecrypt();
		}

		// Token: 0x06002199 RID: 8601 RVA: 0x00189093 File Offset: 0x00187493
		public static ObscuredVector3 operator -(Vector3 a, ObscuredVector3 b)
		{
			return a - b.InternalDecrypt();
		}

		// Token: 0x0600219A RID: 8602 RVA: 0x001890A7 File Offset: 0x001874A7
		public static ObscuredVector3 operator -(ObscuredVector3 a, Vector3 b)
		{
			return a.InternalDecrypt() - b;
		}

		// Token: 0x0600219B RID: 8603 RVA: 0x001890BB File Offset: 0x001874BB
		public static ObscuredVector3 operator -(ObscuredVector3 a)
		{
			return -a.InternalDecrypt();
		}

		// Token: 0x0600219C RID: 8604 RVA: 0x001890CE File Offset: 0x001874CE
		public static ObscuredVector3 operator *(ObscuredVector3 a, float d)
		{
			return a.InternalDecrypt() * d;
		}

		// Token: 0x0600219D RID: 8605 RVA: 0x001890E2 File Offset: 0x001874E2
		public static ObscuredVector3 operator *(float d, ObscuredVector3 a)
		{
			return d * a.InternalDecrypt();
		}

		// Token: 0x0600219E RID: 8606 RVA: 0x001890F6 File Offset: 0x001874F6
		public static ObscuredVector3 operator /(ObscuredVector3 a, float d)
		{
			return a.InternalDecrypt() / d;
		}

		// Token: 0x0600219F RID: 8607 RVA: 0x0018910A File Offset: 0x0018750A
		public static bool operator ==(ObscuredVector3 lhs, ObscuredVector3 rhs)
		{
			return lhs.InternalDecrypt() == rhs.InternalDecrypt();
		}

		// Token: 0x060021A0 RID: 8608 RVA: 0x0018911F File Offset: 0x0018751F
		public static bool operator ==(Vector3 lhs, ObscuredVector3 rhs)
		{
			return lhs == rhs.InternalDecrypt();
		}

		// Token: 0x060021A1 RID: 8609 RVA: 0x0018912E File Offset: 0x0018752E
		public static bool operator ==(ObscuredVector3 lhs, Vector3 rhs)
		{
			return lhs.InternalDecrypt() == rhs;
		}

		// Token: 0x060021A2 RID: 8610 RVA: 0x0018913D File Offset: 0x0018753D
		public static bool operator !=(ObscuredVector3 lhs, ObscuredVector3 rhs)
		{
			return lhs.InternalDecrypt() != rhs.InternalDecrypt();
		}

		// Token: 0x060021A3 RID: 8611 RVA: 0x00189152 File Offset: 0x00187552
		public static bool operator !=(Vector3 lhs, ObscuredVector3 rhs)
		{
			return lhs != rhs.InternalDecrypt();
		}

		// Token: 0x060021A4 RID: 8612 RVA: 0x00189161 File Offset: 0x00187561
		public static bool operator !=(ObscuredVector3 lhs, Vector3 rhs)
		{
			return lhs.InternalDecrypt() != rhs;
		}

		// Token: 0x060021A5 RID: 8613 RVA: 0x00189170 File Offset: 0x00187570
		public override bool Equals(object other)
		{
			return this.InternalDecrypt().Equals(other);
		}

		// Token: 0x060021A6 RID: 8614 RVA: 0x00189194 File Offset: 0x00187594
		public override int GetHashCode()
		{
			return this.InternalDecrypt().GetHashCode();
		}

		// Token: 0x060021A7 RID: 8615 RVA: 0x001891B8 File Offset: 0x001875B8
		public override string ToString()
		{
			return this.InternalDecrypt().ToString();
		}

		// Token: 0x060021A8 RID: 8616 RVA: 0x001891DC File Offset: 0x001875DC
		public string ToString(string format)
		{
			return this.InternalDecrypt().ToString(format);
		}

		// Token: 0x040027DF RID: 10207
		private static int cryptoKey = 120207;

		// Token: 0x040027E0 RID: 10208
		private static readonly Vector3 initialFakeValue = Vector3.zero;

		// Token: 0x040027E1 RID: 10209
		[SerializeField]
		private int currentCryptoKey;

		// Token: 0x040027E2 RID: 10210
		[SerializeField]
		private ObscuredVector3.RawEncryptedVector3 hiddenValue;

		// Token: 0x040027E3 RID: 10211
		[SerializeField]
		private Vector3 fakeValue;

		// Token: 0x040027E4 RID: 10212
		[SerializeField]
		private bool inited;

		// Token: 0x0200049A RID: 1178
		[Serializable]
		public struct RawEncryptedVector3
		{
			// Token: 0x040027E5 RID: 10213
			public int x;

			// Token: 0x040027E6 RID: 10214
			public int y;

			// Token: 0x040027E7 RID: 10215
			public int z;
		}
	}
}
