using System;
using System.IO;
using System.Threading;
using UnityEngine;

// Token: 0x0200056B RID: 1387
public class JPGEncoder
{
	// Token: 0x0600287B RID: 10363 RVA: 0x001AEAB3 File Offset: 0x001ACEB3
	public JPGEncoder(Texture2D texture, float quality)
		: this(texture, quality, string.Empty, false)
	{
	}

	// Token: 0x0600287C RID: 10364 RVA: 0x001AEAC3 File Offset: 0x001ACEC3
	public JPGEncoder(Texture2D texture, float quality, bool blocking)
		: this(texture, quality, string.Empty, blocking)
	{
	}

	// Token: 0x0600287D RID: 10365 RVA: 0x001AEAD3 File Offset: 0x001ACED3
	public JPGEncoder(Texture2D texture, float quality, string path)
		: this(texture, quality, path, false)
	{
	}

	// Token: 0x0600287E RID: 10366 RVA: 0x001AEAE0 File Offset: 0x001ACEE0
	public JPGEncoder(Texture2D texture, float quality, string path, bool blocking)
	{
		this.path = path;
		this.image = new JPGEncoder.BitmapData(texture);
		quality = Mathf.Clamp(quality, 1f, 100f);
		this.sf = ((quality >= 50f) ? ((int)(200f - quality * 2f)) : ((int)(5000f / quality)));
		this.cores = SystemInfo.processorCount;
		Thread thread = new Thread(new ThreadStart(this.DoEncoding));
		thread.Start();
		if (blocking)
		{
			thread.Join();
		}
	}

	// Token: 0x0600287F RID: 10367 RVA: 0x001AECEC File Offset: 0x001AD0EC
	private void InitQuantTables(int sf)
	{
		int[] array = new int[]
		{
			16, 11, 10, 16, 24, 40, 51, 61, 12, 12,
			14, 19, 26, 58, 60, 55, 14, 13, 16, 24,
			40, 57, 69, 56, 14, 17, 22, 29, 51, 87,
			80, 62, 18, 22, 37, 56, 68, 109, 103, 77,
			24, 35, 55, 64, 81, 104, 113, 92, 49, 64,
			78, 87, 103, 121, 120, 101, 72, 92, 95, 98,
			112, 100, 103, 99
		};
		int i;
		for (i = 0; i < 64; i++)
		{
			float num = Mathf.Floor((float)((array[i] * sf + 50) / 100));
			num = Mathf.Clamp(num, 1f, 255f);
			this.YTable[this.ZigZag[i]] = Mathf.RoundToInt(num);
		}
		int[] array2 = new int[]
		{
			17, 18, 24, 47, 99, 99, 99, 99, 18, 21,
			26, 66, 99, 99, 99, 99, 24, 26, 56, 99,
			99, 99, 99, 99, 47, 66, 99, 99, 99, 99,
			99, 99, 99, 99, 99, 99, 99, 99, 99, 99,
			99, 99, 99, 99, 99, 99, 99, 99, 99, 99,
			99, 99, 99, 99, 99, 99, 99, 99, 99, 99,
			99, 99, 99, 99
		};
		for (i = 0; i < 64; i++)
		{
			float num = Mathf.Floor((float)((array2[i] * sf + 50) / 100));
			num = Mathf.Clamp(num, 1f, 255f);
			this.UVTable[this.ZigZag[i]] = (int)num;
		}
		float[] array3 = new float[] { 1f, 1.3870399f, 1.306563f, 1.1758755f, 1f, 0.78569496f, 0.5411961f, 0.27589938f };
		i = 0;
		for (int j = 0; j < 8; j++)
		{
			for (int k = 0; k < 8; k++)
			{
				this.fdtbl_Y[i] = 1f / ((float)this.YTable[this.ZigZag[i]] * array3[j] * array3[k] * 8f);
				this.fdtbl_UV[i] = 1f / ((float)this.UVTable[this.ZigZag[i]] * array3[j] * array3[k] * 8f);
				i++;
			}
		}
	}

	// Token: 0x06002880 RID: 10368 RVA: 0x001AEE58 File Offset: 0x001AD258
	private JPGEncoder.BitString[] ComputeHuffmanTbl(byte[] nrcodes, byte[] std_table)
	{
		int num = 0;
		int num2 = 0;
		JPGEncoder.BitString[] array = new JPGEncoder.BitString[256];
		for (int i = 1; i <= 16; i++)
		{
			for (int j = 1; j <= (int)nrcodes[i]; j++)
			{
				array[(int)std_table[num2]] = default(JPGEncoder.BitString);
				array[(int)std_table[num2]].value = num;
				array[(int)std_table[num2]].length = i;
				num2++;
				num++;
			}
			num *= 2;
		}
		return array;
	}

	// Token: 0x06002881 RID: 10369 RVA: 0x001AEEE4 File Offset: 0x001AD2E4
	private void InitHuffmanTbl()
	{
		this.YDC_HT = this.ComputeHuffmanTbl(this.std_dc_luminance_nrcodes, this.std_dc_luminance_values);
		this.UVDC_HT = this.ComputeHuffmanTbl(this.std_dc_chrominance_nrcodes, this.std_dc_chrominance_values);
		this.YAC_HT = this.ComputeHuffmanTbl(this.std_ac_luminance_nrcodes, this.std_ac_luminance_values);
		this.UVAC_HT = this.ComputeHuffmanTbl(this.std_ac_chrominance_nrcodes, this.std_ac_chrominance_values);
	}

	// Token: 0x06002882 RID: 10370 RVA: 0x001AEF54 File Offset: 0x001AD354
	private void InitCategoryfloat()
	{
		int num = 1;
		int num2 = 2;
		for (int i = 1; i <= 15; i++)
		{
			for (int j = num; j < num2; j++)
			{
				this.category[32767 + j] = i;
				JPGEncoder.BitString bitString = default(JPGEncoder.BitString);
				bitString.length = i;
				bitString.value = j;
				this.bitcode[32767 + j] = bitString;
			}
			for (int j = -(num2 - 1); j <= -num; j++)
			{
				this.category[32767 + j] = i;
				JPGEncoder.BitString bitString = default(JPGEncoder.BitString);
				bitString.length = i;
				bitString.value = num2 - 1 + j;
				this.bitcode[32767 + j] = bitString;
			}
			num <<= 1;
			num2 <<= 1;
		}
	}

	// Token: 0x06002883 RID: 10371 RVA: 0x001AF032 File Offset: 0x001AD432
	public byte[] GetBytes()
	{
		if (!this.isDone)
		{
			Debug.LogError("JPEGEncoder not complete, cannot get bytes!");
			return null;
		}
		return this.byteout.GetAllBytes();
	}

	// Token: 0x06002884 RID: 10372 RVA: 0x001AF058 File Offset: 0x001AD458
	private void WriteBits(JPGEncoder.BitString bs)
	{
		int value = bs.value;
		int i = bs.length - 1;
		while (i >= 0)
		{
			if (((long)value & (long)((ulong)Convert.ToUInt32(1 << i))) != 0L)
			{
				this.bytenew |= Convert.ToUInt32(1 << this.bytepos);
			}
			i--;
			this.bytepos--;
			if (this.bytepos < 0)
			{
				if (this.bytenew == 255U)
				{
					this.WriteByte(byte.MaxValue);
					this.WriteByte(0);
				}
				else
				{
					this.WriteByte((byte)this.bytenew);
				}
				this.bytepos = 7;
				this.bytenew = 0U;
			}
		}
	}

	// Token: 0x06002885 RID: 10373 RVA: 0x001AF116 File Offset: 0x001AD516
	private void WriteByte(byte value)
	{
		this.byteout.WriteByte(value);
	}

	// Token: 0x06002886 RID: 10374 RVA: 0x001AF124 File Offset: 0x001AD524
	private void WriteWord(int value)
	{
		this.WriteByte((byte)((value >> 8) & 255));
		this.WriteByte((byte)(value & 255));
	}

	// Token: 0x06002887 RID: 10375 RVA: 0x001AF144 File Offset: 0x001AD544
	private float[] FDCTQuant(float[] data, float[] fdtbl)
	{
		int num = 0;
		for (int i = 0; i < 8; i++)
		{
			float num2 = data[num] + data[num + 7];
			float num3 = data[num] - data[num + 7];
			float num4 = data[num + 1] + data[num + 6];
			float num5 = data[num + 1] - data[num + 6];
			float num6 = data[num + 2] + data[num + 5];
			float num7 = data[num + 2] - data[num + 5];
			float num8 = data[num + 3] + data[num + 4];
			float num9 = data[num + 3] - data[num + 4];
			float num10 = num2 + num8;
			float num11 = num2 - num8;
			float num12 = num4 + num6;
			float num13 = num4 - num6;
			data[num] = num10 + num12;
			data[num + 4] = num10 - num12;
			float num14 = (num13 + num11) * 0.70710677f;
			data[num + 2] = num11 + num14;
			data[num + 6] = num11 - num14;
			num10 = num9 + num7;
			num12 = num7 + num5;
			num13 = num5 + num3;
			float num15 = (num10 - num13) * 0.38268343f;
			float num16 = 0.5411961f * num10 + num15;
			float num17 = 1.306563f * num13 + num15;
			float num18 = num12 * 0.70710677f;
			float num19 = num3 + num18;
			float num20 = num3 - num18;
			data[num + 5] = num20 + num16;
			data[num + 3] = num20 - num16;
			data[num + 1] = num19 + num17;
			data[num + 7] = num19 - num17;
			num += 8;
		}
		num = 0;
		for (int i = 0; i < 8; i++)
		{
			float num2 = data[num] + data[num + 56];
			float num3 = data[num] - data[num + 56];
			float num4 = data[num + 8] + data[num + 48];
			float num5 = data[num + 8] - data[num + 48];
			float num6 = data[num + 16] + data[num + 40];
			float num7 = data[num + 16] - data[num + 40];
			float num8 = data[num + 24] + data[num + 32];
			float num9 = data[num + 24] - data[num + 32];
			float num10 = num2 + num8;
			float num11 = num2 - num8;
			float num12 = num4 + num6;
			float num13 = num4 - num6;
			data[num] = num10 + num12;
			data[num + 32] = num10 - num12;
			float num14 = (num13 + num11) * 0.70710677f;
			data[num + 16] = num11 + num14;
			data[num + 48] = num11 - num14;
			num10 = num9 + num7;
			num12 = num7 + num5;
			num13 = num5 + num3;
			float num15 = (num10 - num13) * 0.38268343f;
			float num16 = 0.5411961f * num10 + num15;
			float num17 = 1.306563f * num13 + num15;
			float num18 = num12 * 0.70710677f;
			float num19 = num3 + num18;
			float num20 = num3 - num18;
			data[num + 40] = num20 + num16;
			data[num + 24] = num20 - num16;
			data[num + 8] = num19 + num17;
			data[num + 56] = num19 - num17;
			num++;
		}
		for (int i = 0; i < 64; i++)
		{
			data[i] = Mathf.Round(data[i] * fdtbl[i]);
		}
		return data;
	}

	// Token: 0x06002888 RID: 10376 RVA: 0x001AF444 File Offset: 0x001AD844
	private void WriteAPP0()
	{
		this.WriteWord(65504);
		this.WriteWord(16);
		this.WriteByte(74);
		this.WriteByte(70);
		this.WriteByte(73);
		this.WriteByte(70);
		this.WriteByte(0);
		this.WriteByte(1);
		this.WriteByte(1);
		this.WriteByte(0);
		this.WriteWord(1);
		this.WriteWord(1);
		this.WriteByte(0);
		this.WriteByte(0);
	}

	// Token: 0x06002889 RID: 10377 RVA: 0x001AF4BC File Offset: 0x001AD8BC
	private void WriteSOF0(int width, int height)
	{
		this.WriteWord(65472);
		this.WriteWord(17);
		this.WriteByte(8);
		this.WriteWord(height);
		this.WriteWord(width);
		this.WriteByte(3);
		this.WriteByte(1);
		this.WriteByte(17);
		this.WriteByte(0);
		this.WriteByte(2);
		this.WriteByte(17);
		this.WriteByte(1);
		this.WriteByte(3);
		this.WriteByte(17);
		this.WriteByte(1);
	}

	// Token: 0x0600288A RID: 10378 RVA: 0x001AF53C File Offset: 0x001AD93C
	private void WriteDQT()
	{
		this.WriteWord(65499);
		this.WriteWord(132);
		this.WriteByte(0);
		for (int i = 0; i < 64; i++)
		{
			this.WriteByte((byte)this.YTable[i]);
		}
		this.WriteByte(1);
		for (int i = 0; i < 64; i++)
		{
			this.WriteByte((byte)this.UVTable[i]);
		}
	}

	// Token: 0x0600288B RID: 10379 RVA: 0x001AF5B4 File Offset: 0x001AD9B4
	private void WriteDHT()
	{
		this.WriteWord(65476);
		this.WriteWord(418);
		this.WriteByte(0);
		for (int i = 0; i < 16; i++)
		{
			this.WriteByte(this.std_dc_luminance_nrcodes[i + 1]);
		}
		for (int i = 0; i <= 11; i++)
		{
			this.WriteByte(this.std_dc_luminance_values[i]);
		}
		this.WriteByte(16);
		for (int i = 0; i < 16; i++)
		{
			this.WriteByte(this.std_ac_luminance_nrcodes[i + 1]);
		}
		for (int i = 0; i <= 161; i++)
		{
			this.WriteByte(this.std_ac_luminance_values[i]);
		}
		this.WriteByte(1);
		for (int i = 0; i < 16; i++)
		{
			this.WriteByte(this.std_dc_chrominance_nrcodes[i + 1]);
		}
		for (int i = 0; i <= 11; i++)
		{
			this.WriteByte(this.std_dc_chrominance_values[i]);
		}
		this.WriteByte(17);
		for (int i = 0; i < 16; i++)
		{
			this.WriteByte(this.std_ac_chrominance_nrcodes[i + 1]);
		}
		for (int i = 0; i <= 161; i++)
		{
			this.WriteByte(this.std_ac_chrominance_values[i]);
		}
	}

	// Token: 0x0600288C RID: 10380 RVA: 0x001AF70B File Offset: 0x001ADB0B
	private void WriteDRI()
	{
		this.WriteWord(65501);
		this.WriteWord(4);
		this.WriteWord(40);
	}

	// Token: 0x0600288D RID: 10381 RVA: 0x001AF728 File Offset: 0x001ADB28
	private void WriteSOS()
	{
		this.WriteWord(65498);
		this.WriteWord(12);
		this.WriteByte(3);
		this.WriteByte(1);
		this.WriteByte(0);
		this.WriteByte(2);
		this.WriteByte(17);
		this.WriteByte(3);
		this.WriteByte(17);
		this.WriteByte(0);
		this.WriteByte(63);
		this.WriteByte(0);
	}

	// Token: 0x0600288E RID: 10382 RVA: 0x001AF794 File Offset: 0x001ADB94
	private float ProcessDU(float[] CDU, float[] fdtbl, float DC, JPGEncoder.BitString[] HTDC, JPGEncoder.BitString[] HTAC)
	{
		JPGEncoder.BitString bitString = HTAC[0];
		JPGEncoder.BitString bitString2 = HTAC[240];
		float[] array = this.FDCTQuant(CDU, fdtbl);
		for (int i = 0; i < 64; i++)
		{
			this.DU[this.ZigZag[i]] = (int)array[i];
		}
		int num = (int)((float)this.DU[0] - DC);
		DC = (float)this.DU[0];
		if (num == 0)
		{
			this.WriteBits(HTDC[0]);
		}
		else
		{
			this.WriteBits(HTDC[this.category[32767 + num]]);
			this.WriteBits(this.bitcode[32767 + num]);
		}
		int num2 = 63;
		while (num2 > 0 && this.DU[num2] == 0)
		{
			num2--;
		}
		if (num2 == 0)
		{
			this.WriteBits(bitString);
			return DC;
		}
		for (int i = 1; i <= num2; i++)
		{
			int num3 = i;
			while (this.DU[i] == 0 && i <= num2)
			{
				i++;
			}
			int num4 = i - num3;
			if (num4 >= 16)
			{
				for (int j = 1; j <= num4 / 16; j++)
				{
					this.WriteBits(bitString2);
				}
				num4 &= 15;
			}
			this.WriteBits(HTAC[num4 * 16 + this.category[32767 + this.DU[i]]]);
			this.WriteBits(this.bitcode[32767 + this.DU[i]]);
		}
		if (num2 != 63)
		{
			this.WriteBits(bitString);
		}
		return DC;
	}

	// Token: 0x0600288F RID: 10383 RVA: 0x001AF96C File Offset: 0x001ADD6C
	private void RGB2YUV(JPGEncoder.BitmapData image, int xpos, int ypos)
	{
		int num = 0;
		for (int i = 0; i < 8; i++)
		{
			for (int j = 0; j < 8; j++)
			{
				Color32 pixelColor = image.GetPixelColor(xpos + j, image.height - (ypos + i));
				this.YDU[num] = 0.299f * (float)pixelColor.r + 0.587f * (float)pixelColor.g + 0.114f * (float)pixelColor.b - 128f;
				this.UDU[num] = -0.16874f * (float)pixelColor.r + -0.33126f * (float)pixelColor.g + 0.5f * (float)pixelColor.b;
				this.VDU[num] = 0.5f * (float)pixelColor.r + -0.41869f * (float)pixelColor.g + -0.08131f * (float)pixelColor.b;
				num++;
			}
		}
	}

	// Token: 0x06002890 RID: 10384 RVA: 0x001AFA5C File Offset: 0x001ADE5C
	private void DoEncoding()
	{
		this.isDone = false;
		this.InitHuffmanTbl();
		this.InitCategoryfloat();
		this.InitQuantTables(this.sf);
		this.Encode();
		if (!string.IsNullOrEmpty(this.path))
		{
			File.WriteAllBytes(this.path, this.GetBytes());
		}
		this.isDone = true;
	}

	// Token: 0x06002891 RID: 10385 RVA: 0x001AFAB8 File Offset: 0x001ADEB8
	private void Encode()
	{
		this.byteout = new JPGEncoder.ByteArray();
		this.bytenew = 0U;
		this.bytepos = 7;
		this.WriteWord(65496);
		this.WriteAPP0();
		this.WriteDQT();
		this.WriteSOF0(this.image.width, this.image.height);
		this.WriteDHT();
		this.WriteDRI();
		this.WriteSOS();
		float num = 0f;
		float num2 = 0f;
		float num3 = 0f;
		this.bytenew = 0U;
		this.bytepos = 7;
		int num4 = 0;
		int num5 = 0;
		int num6 = 0;
		for (int i = 0; i < this.image.height; i += 8)
		{
			for (int j = 0; j < this.image.width; j += 8)
			{
				this.RGB2YUV(this.image, j, i);
				num = this.ProcessDU(this.YDU, this.fdtbl_Y, num, this.YDC_HT, this.YAC_HT);
				num2 = this.ProcessDU(this.UDU, this.fdtbl_UV, num2, this.UVDC_HT, this.UVAC_HT);
				num3 = this.ProcessDU(this.VDU, this.fdtbl_UV, num3, this.UVDC_HT, this.UVAC_HT);
				num5++;
				if (num5 == 40)
				{
					num5 = 0;
					this.WriteWord(65488 + num6);
					num6 = (num6 + 1) % 8;
					this.bytenew = 0U;
					this.bytepos = 7;
					num = 0f;
					num2 = 0f;
					num3 = 0f;
					num4++;
				}
				if (this.cores == 1)
				{
					Thread.Sleep(0);
				}
			}
		}
		if (this.bytepos >= 0)
		{
			this.WriteBits(new JPGEncoder.BitString
			{
				length = this.bytepos + 1,
				value = (1 << this.bytepos + 1) - 1
			});
		}
		this.WriteWord(65497);
		this.isDone = true;
	}

	// Token: 0x04002D9B RID: 11675
	private int[] ZigZag = new int[]
	{
		0, 1, 5, 6, 14, 15, 27, 28, 2, 4,
		7, 13, 16, 26, 29, 42, 3, 8, 12, 17,
		25, 30, 41, 43, 9, 11, 18, 24, 31, 40,
		44, 53, 10, 19, 23, 32, 39, 45, 52, 54,
		20, 22, 33, 38, 46, 51, 55, 60, 21, 34,
		37, 47, 50, 56, 59, 61, 35, 36, 48, 49,
		57, 58, 62, 63
	};

	// Token: 0x04002D9C RID: 11676
	private int[] YTable = new int[64];

	// Token: 0x04002D9D RID: 11677
	private int[] UVTable = new int[64];

	// Token: 0x04002D9E RID: 11678
	private float[] fdtbl_Y = new float[64];

	// Token: 0x04002D9F RID: 11679
	private float[] fdtbl_UV = new float[64];

	// Token: 0x04002DA0 RID: 11680
	private JPGEncoder.BitString[] YDC_HT;

	// Token: 0x04002DA1 RID: 11681
	private JPGEncoder.BitString[] UVDC_HT;

	// Token: 0x04002DA2 RID: 11682
	private JPGEncoder.BitString[] YAC_HT;

	// Token: 0x04002DA3 RID: 11683
	private JPGEncoder.BitString[] UVAC_HT;

	// Token: 0x04002DA4 RID: 11684
	private byte[] std_dc_luminance_nrcodes = new byte[]
	{
		0, 0, 1, 5, 1, 1, 1, 1, 1, 1,
		0, 0, 0, 0, 0, 0, 0
	};

	// Token: 0x04002DA5 RID: 11685
	private byte[] std_dc_luminance_values = new byte[]
	{
		0, 1, 2, 3, 4, 5, 6, 7, 8, 9,
		10, 11
	};

	// Token: 0x04002DA6 RID: 11686
	private byte[] std_ac_luminance_nrcodes = new byte[]
	{
		0, 0, 2, 1, 3, 3, 2, 4, 3, 5,
		5, 4, 4, 0, 0, 1, 125
	};

	// Token: 0x04002DA7 RID: 11687
	private byte[] std_ac_luminance_values = new byte[]
	{
		1, 2, 3, 0, 4, 17, 5, 18, 33, 49,
		65, 6, 19, 81, 97, 7, 34, 113, 20, 50,
		129, 145, 161, 8, 35, 66, 177, 193, 21, 82,
		209, 240, 36, 51, 98, 114, 130, 9, 10, 22,
		23, 24, 25, 26, 37, 38, 39, 40, 41, 42,
		52, 53, 54, 55, 56, 57, 58, 67, 68, 69,
		70, 71, 72, 73, 74, 83, 84, 85, 86, 87,
		88, 89, 90, 99, 100, 101, 102, 103, 104, 105,
		106, 115, 116, 117, 118, 119, 120, 121, 122, 131,
		132, 133, 134, 135, 136, 137, 138, 146, 147, 148,
		149, 150, 151, 152, 153, 154, 162, 163, 164, 165,
		166, 167, 168, 169, 170, 178, 179, 180, 181, 182,
		183, 184, 185, 186, 194, 195, 196, 197, 198, 199,
		200, 201, 202, 210, 211, 212, 213, 214, 215, 216,
		217, 218, 225, 226, 227, 228, 229, 230, 231, 232,
		233, 234, 241, 242, 243, 244, 245, 246, 247, 248,
		249, 250
	};

	// Token: 0x04002DA8 RID: 11688
	private byte[] std_dc_chrominance_nrcodes = new byte[]
	{
		0, 0, 3, 1, 1, 1, 1, 1, 1, 1,
		1, 1, 0, 0, 0, 0, 0
	};

	// Token: 0x04002DA9 RID: 11689
	private byte[] std_dc_chrominance_values = new byte[]
	{
		0, 1, 2, 3, 4, 5, 6, 7, 8, 9,
		10, 11
	};

	// Token: 0x04002DAA RID: 11690
	private byte[] std_ac_chrominance_nrcodes = new byte[]
	{
		0, 0, 2, 1, 2, 4, 4, 3, 4, 7,
		5, 4, 4, 0, 1, 2, 119
	};

	// Token: 0x04002DAB RID: 11691
	private byte[] std_ac_chrominance_values = new byte[]
	{
		0, 1, 2, 3, 17, 4, 5, 33, 49, 6,
		18, 65, 81, 7, 97, 113, 19, 34, 50, 129,
		8, 20, 66, 145, 161, 177, 193, 9, 35, 51,
		82, 240, 21, 98, 114, 209, 10, 22, 36, 52,
		225, 37, 241, 23, 24, 25, 26, 38, 39, 40,
		41, 42, 53, 54, 55, 56, 57, 58, 67, 68,
		69, 70, 71, 72, 73, 74, 83, 84, 85, 86,
		87, 88, 89, 90, 99, 100, 101, 102, 103, 104,
		105, 106, 115, 116, 117, 118, 119, 120, 121, 122,
		130, 131, 132, 133, 134, 135, 136, 137, 138, 146,
		147, 148, 149, 150, 151, 152, 153, 154, 162, 163,
		164, 165, 166, 167, 168, 169, 170, 178, 179, 180,
		181, 182, 183, 184, 185, 186, 194, 195, 196, 197,
		198, 199, 200, 201, 202, 210, 211, 212, 213, 214,
		215, 216, 217, 218, 226, 227, 228, 229, 230, 231,
		232, 233, 234, 242, 243, 244, 245, 246, 247, 248,
		249, 250
	};

	// Token: 0x04002DAC RID: 11692
	private JPGEncoder.BitString[] bitcode = new JPGEncoder.BitString[65535];

	// Token: 0x04002DAD RID: 11693
	private int[] category = new int[65535];

	// Token: 0x04002DAE RID: 11694
	private uint bytenew;

	// Token: 0x04002DAF RID: 11695
	private int bytepos = 7;

	// Token: 0x04002DB0 RID: 11696
	private JPGEncoder.ByteArray byteout = new JPGEncoder.ByteArray();

	// Token: 0x04002DB1 RID: 11697
	private const int resetInterval = 40;

	// Token: 0x04002DB2 RID: 11698
	private int[] DU = new int[64];

	// Token: 0x04002DB3 RID: 11699
	private float[] YDU = new float[64];

	// Token: 0x04002DB4 RID: 11700
	private float[] UDU = new float[64];

	// Token: 0x04002DB5 RID: 11701
	private float[] VDU = new float[64];

	// Token: 0x04002DB6 RID: 11702
	public bool isDone;

	// Token: 0x04002DB7 RID: 11703
	private JPGEncoder.BitmapData image;

	// Token: 0x04002DB8 RID: 11704
	private int sf;

	// Token: 0x04002DB9 RID: 11705
	private string path;

	// Token: 0x04002DBA RID: 11706
	private int cores;

	// Token: 0x0200056C RID: 1388
	private class ByteArray
	{
		// Token: 0x06002892 RID: 10386 RVA: 0x001AFCAF File Offset: 0x001AE0AF
		public ByteArray()
		{
			this.stream = new MemoryStream();
			this.writer = new BinaryWriter(this.stream);
		}

		// Token: 0x06002893 RID: 10387 RVA: 0x001AFCD3 File Offset: 0x001AE0D3
		public void WriteByte(byte value)
		{
			this.writer.Write(value);
		}

		// Token: 0x06002894 RID: 10388 RVA: 0x001AFCE4 File Offset: 0x001AE0E4
		public byte[] GetAllBytes()
		{
			byte[] array = new byte[this.stream.Length];
			this.stream.Position = 0L;
			this.stream.Read(array, 0, array.Length);
			return array;
		}

		// Token: 0x04002DBB RID: 11707
		private MemoryStream stream;

		// Token: 0x04002DBC RID: 11708
		private BinaryWriter writer;
	}

	// Token: 0x0200056D RID: 1389
	private struct BitString
	{
		// Token: 0x04002DBD RID: 11709
		public int length;

		// Token: 0x04002DBE RID: 11710
		public int value;
	}

	// Token: 0x0200056E RID: 1390
	private class BitmapData
	{
		// Token: 0x06002895 RID: 10389 RVA: 0x001AFD22 File Offset: 0x001AE122
		public BitmapData(Texture2D texture)
		{
			this.height = texture.height;
			this.width = texture.width;
			this.pixels = texture.GetPixels32();
		}

		// Token: 0x06002896 RID: 10390 RVA: 0x001AFD4E File Offset: 0x001AE14E
		public Color32 GetPixelColor(int x, int y)
		{
			x = Mathf.Clamp(x, 0, this.width - 1);
			y = Mathf.Clamp(y, 0, this.height - 1);
			return this.pixels[y * this.width + x];
		}

		// Token: 0x04002DBF RID: 11711
		public int height;

		// Token: 0x04002DC0 RID: 11712
		public int width;

		// Token: 0x04002DC1 RID: 11713
		private Color32[] pixels;
	}
}
