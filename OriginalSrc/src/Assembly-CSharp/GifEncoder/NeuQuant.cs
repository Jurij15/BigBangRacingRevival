using System;

namespace GifEncoder
{
	// Token: 0x0200051E RID: 1310
	public class NeuQuant
	{
		// Token: 0x06002696 RID: 9878 RVA: 0x001A6C70 File Offset: 0x001A5070
		public NeuQuant(byte[] thepic, int len, int sample)
		{
			this.thepicture = thepic;
			this.lengthcount = len;
			this.samplefac = sample;
			this.network = new int[NeuQuant.PaletteSize][];
			for (int i = 0; i < NeuQuant.PaletteSize; i++)
			{
				this.network[i] = new int[4];
				int[] array = this.network[i];
				array[0] = (array[1] = (array[2] = (i << NeuQuant.netbiasshift + 8) / NeuQuant.PaletteSize));
				this.freq[i] = NeuQuant.intbias / NeuQuant.PaletteSize;
				this.bias[i] = 0;
			}
		}

		// Token: 0x06002697 RID: 9879 RVA: 0x001A6D54 File Offset: 0x001A5154
		public byte[] ColorMap()
		{
			byte[] array = new byte[3 * NeuQuant.PaletteSize];
			int[] array2 = new int[NeuQuant.PaletteSize];
			for (int i = 0; i < NeuQuant.PaletteSize; i++)
			{
				array2[this.network[i][3]] = i;
			}
			int num = 0;
			for (int j = 0; j < NeuQuant.PaletteSize; j++)
			{
				int num2 = array2[j];
				array[num++] = (byte)this.network[num2][0];
				array[num++] = (byte)this.network[num2][1];
				array[num++] = (byte)this.network[num2][2];
			}
			return array;
		}

		// Token: 0x06002698 RID: 9880 RVA: 0x001A6DF8 File Offset: 0x001A51F8
		public void Inxbuild()
		{
			int num = 0;
			int num2 = 0;
			for (int i = 0; i < NeuQuant.PaletteSize; i++)
			{
				int[] array = this.network[i];
				int num3 = i;
				int num4 = array[1];
				int[] array2;
				for (int j = i + 1; j < NeuQuant.PaletteSize; j++)
				{
					array2 = this.network[j];
					if (array2[1] < num4)
					{
						num3 = j;
						num4 = array2[1];
					}
				}
				array2 = this.network[num3];
				if (i != num3)
				{
					int j = array2[0];
					array2[0] = array[0];
					array[0] = j;
					j = array2[1];
					array2[1] = array[1];
					array[1] = j;
					j = array2[2];
					array2[2] = array[2];
					array[2] = j;
					j = array2[3];
					array2[3] = array[3];
					array[3] = j;
				}
				if (num4 != num)
				{
					this.netindex[num] = num2 + i >> 1;
					for (int j = num + 1; j < num4; j++)
					{
						this.netindex[j] = i;
					}
					num = num4;
					num2 = i;
				}
			}
			this.netindex[num] = num2 + NeuQuant.maxnetpos >> 1;
			for (int j = num + 1; j < 256; j++)
			{
				this.netindex[j] = NeuQuant.maxnetpos;
			}
		}

		// Token: 0x06002699 RID: 9881 RVA: 0x001A6F34 File Offset: 0x001A5334
		public void Learn()
		{
			if (this.lengthcount < NeuQuant.minpicturebytes)
			{
				this.samplefac = 1;
			}
			this.alphadec = 30 + (this.samplefac - 1) / 3;
			byte[] array = this.thepicture;
			int num = 0;
			int num2 = this.lengthcount;
			int num3 = this.lengthcount / (3 * this.samplefac);
			int num4 = num3 / NeuQuant.ncycles;
			int num5 = NeuQuant.initalpha;
			int num6 = NeuQuant.initradius;
			int num7 = num6 >> NeuQuant.radiusbiasshift;
			if (num7 <= 1)
			{
				num7 = 0;
			}
			int i;
			for (i = 0; i < num7; i++)
			{
				this.radpower[i] = num5 * ((num7 * num7 - i * i) * NeuQuant.radbias / (num7 * num7));
			}
			int num8;
			if (this.lengthcount < NeuQuant.minpicturebytes)
			{
				num8 = 3;
			}
			else if (this.lengthcount % NeuQuant.prime1 != 0)
			{
				num8 = 3 * NeuQuant.prime1;
			}
			else if (this.lengthcount % NeuQuant.prime2 != 0)
			{
				num8 = 3 * NeuQuant.prime2;
			}
			else if (this.lengthcount % NeuQuant.prime3 != 0)
			{
				num8 = 3 * NeuQuant.prime3;
			}
			else
			{
				num8 = 3 * NeuQuant.prime4;
			}
			i = 0;
			while (i < num3)
			{
				int num9 = (int)(array[num] & byte.MaxValue) << NeuQuant.netbiasshift;
				int num10 = (int)(array[num + 1] & byte.MaxValue) << NeuQuant.netbiasshift;
				int num11 = (int)(array[num + 2] & byte.MaxValue) << NeuQuant.netbiasshift;
				int j = this.Contest(num9, num10, num11);
				this.Altersingle(num5, j, num9, num10, num11);
				if (num7 != 0)
				{
					this.Alterneigh(num7, j, num9, num10, num11);
				}
				num += num8;
				if (num >= num2)
				{
					num -= this.lengthcount;
				}
				i++;
				if (num4 == 0)
				{
					num4 = 1;
				}
				if (i % num4 == 0)
				{
					num5 -= num5 / this.alphadec;
					num6 -= num6 / NeuQuant.radiusdec;
					num7 = num6 >> NeuQuant.radiusbiasshift;
					if (num7 <= 1)
					{
						num7 = 0;
					}
					for (j = 0; j < num7; j++)
					{
						this.radpower[j] = num5 * ((num7 * num7 - j * j) * NeuQuant.radbias / (num7 * num7));
					}
				}
			}
		}

		// Token: 0x0600269A RID: 9882 RVA: 0x001A7188 File Offset: 0x001A5588
		public int Map(int b, int g, int r)
		{
			int num = 1000;
			int num2 = -1;
			int num3 = this.netindex[g];
			int num4 = num3 - 1;
			while (num3 < NeuQuant.PaletteSize || num4 >= 0)
			{
				if (num3 < NeuQuant.PaletteSize)
				{
					int[] array = this.network[num3];
					int num5 = array[1] - g;
					if (num5 >= num)
					{
						num3 = NeuQuant.PaletteSize;
					}
					else
					{
						num3++;
						if (num5 < 0)
						{
							num5 = -num5;
						}
						int num6 = array[0] - b;
						if (num6 < 0)
						{
							num6 = -num6;
						}
						num5 += num6;
						if (num5 < num)
						{
							num6 = array[2] - r;
							if (num6 < 0)
							{
								num6 = -num6;
							}
							num5 += num6;
							if (num5 < num)
							{
								num = num5;
								num2 = array[3];
							}
						}
					}
				}
				if (num4 >= 0)
				{
					int[] array = this.network[num4];
					int num5 = g - array[1];
					if (num5 >= num)
					{
						num4 = -1;
					}
					else
					{
						num4--;
						if (num5 < 0)
						{
							num5 = -num5;
						}
						int num6 = array[0] - b;
						if (num6 < 0)
						{
							num6 = -num6;
						}
						num5 += num6;
						if (num5 < num)
						{
							num6 = array[2] - r;
							if (num6 < 0)
							{
								num6 = -num6;
							}
							num5 += num6;
							if (num5 < num)
							{
								num = num5;
								num2 = array[3];
							}
						}
					}
				}
			}
			return num2;
		}

		// Token: 0x0600269B RID: 9883 RVA: 0x001A72BD File Offset: 0x001A56BD
		public byte[] Process()
		{
			this.Learn();
			this.Unbiasnet();
			this.Inxbuild();
			return this.ColorMap();
		}

		// Token: 0x0600269C RID: 9884 RVA: 0x001A72D8 File Offset: 0x001A56D8
		public void Unbiasnet()
		{
			for (int i = 0; i < NeuQuant.PaletteSize; i++)
			{
				this.network[i][0] >>= NeuQuant.netbiasshift;
				this.network[i][1] >>= NeuQuant.netbiasshift;
				this.network[i][2] >>= NeuQuant.netbiasshift;
				this.network[i][3] = i;
			}
		}

		// Token: 0x0600269D RID: 9885 RVA: 0x001A7354 File Offset: 0x001A5754
		protected void Alterneigh(int rad, int i, int b, int g, int r)
		{
			int num = i - rad;
			if (num < -1)
			{
				num = -1;
			}
			int num2 = i + rad;
			if (num2 > NeuQuant.PaletteSize)
			{
				num2 = NeuQuant.PaletteSize;
			}
			int num3 = i + 1;
			int num4 = i - 1;
			int num5 = 1;
			while (num3 < num2 || num4 > num)
			{
				int num6 = this.radpower[num5++];
				if (num3 < num2)
				{
					int[] array = this.network[num3++];
					try
					{
						array[0] -= num6 * (array[0] - b) / NeuQuant.alpharadbias;
						array[1] -= num6 * (array[1] - g) / NeuQuant.alpharadbias;
						array[2] -= num6 * (array[2] - r) / NeuQuant.alpharadbias;
					}
					catch (Exception)
					{
					}
				}
				if (num4 > num)
				{
					int[] array = this.network[num4--];
					try
					{
						array[0] -= num6 * (array[0] - b) / NeuQuant.alpharadbias;
						array[1] -= num6 * (array[1] - g) / NeuQuant.alpharadbias;
						array[2] -= num6 * (array[2] - r) / NeuQuant.alpharadbias;
					}
					catch (Exception)
					{
					}
				}
			}
		}

		// Token: 0x0600269E RID: 9886 RVA: 0x001A74B4 File Offset: 0x001A58B4
		protected void Altersingle(int alpha, int i, int b, int g, int r)
		{
			int[] array = this.network[i];
			array[0] -= alpha * (array[0] - b) / NeuQuant.initalpha;
			array[1] -= alpha * (array[1] - g) / NeuQuant.initalpha;
			array[2] -= alpha * (array[2] - r) / NeuQuant.initalpha;
		}

		// Token: 0x0600269F RID: 9887 RVA: 0x001A7514 File Offset: 0x001A5914
		protected int Contest(int b, int g, int r)
		{
			int num = int.MaxValue;
			int num2 = num;
			int num3 = -1;
			int num4 = num3;
			for (int i = 0; i < NeuQuant.PaletteSize; i++)
			{
				int[] array = this.network[i];
				int num5 = array[0] - b;
				if (num5 < 0)
				{
					num5 = -num5;
				}
				int num6 = array[1] - g;
				if (num6 < 0)
				{
					num6 = -num6;
				}
				num5 += num6;
				num6 = array[2] - r;
				if (num6 < 0)
				{
					num6 = -num6;
				}
				num5 += num6;
				if (num5 < num)
				{
					num = num5;
					num3 = i;
				}
				int num7 = num5 - (this.bias[i] >> NeuQuant.intbiasshift - NeuQuant.netbiasshift);
				if (num7 < num2)
				{
					num2 = num7;
					num4 = i;
				}
				int num8 = this.freq[i] >> NeuQuant.betashift;
				this.freq[i] -= num8;
				this.bias[i] += num8 << NeuQuant.gammashift;
			}
			this.freq[num3] += NeuQuant.beta;
			this.bias[num3] -= NeuQuant.betagamma;
			return num4;
		}

		// Token: 0x04002C0C RID: 11276
		public static readonly int PaletteSize = 255;

		// Token: 0x04002C0D RID: 11277
		protected static readonly int prime1 = 499;

		// Token: 0x04002C0E RID: 11278
		protected static readonly int prime2 = 491;

		// Token: 0x04002C0F RID: 11279
		protected static readonly int prime3 = 487;

		// Token: 0x04002C10 RID: 11280
		protected static readonly int prime4 = 503;

		// Token: 0x04002C11 RID: 11281
		protected static readonly int minpicturebytes = 3 * NeuQuant.prime4;

		// Token: 0x04002C12 RID: 11282
		protected static readonly int maxnetpos = NeuQuant.PaletteSize - 1;

		// Token: 0x04002C13 RID: 11283
		protected static readonly int netbiasshift = 4;

		// Token: 0x04002C14 RID: 11284
		protected static readonly int ncycles = 100;

		// Token: 0x04002C15 RID: 11285
		protected static readonly int intbiasshift = 16;

		// Token: 0x04002C16 RID: 11286
		protected static readonly int intbias = 1 << NeuQuant.intbiasshift;

		// Token: 0x04002C17 RID: 11287
		protected static readonly int gammashift = 10;

		// Token: 0x04002C18 RID: 11288
		protected static readonly int gamma = 1 << NeuQuant.gammashift;

		// Token: 0x04002C19 RID: 11289
		protected static readonly int betashift = 10;

		// Token: 0x04002C1A RID: 11290
		protected static readonly int beta = NeuQuant.intbias >> NeuQuant.betashift;

		// Token: 0x04002C1B RID: 11291
		protected static readonly int betagamma = NeuQuant.intbias << NeuQuant.gammashift - NeuQuant.betashift;

		// Token: 0x04002C1C RID: 11292
		protected static readonly int initrad = NeuQuant.PaletteSize >> 3;

		// Token: 0x04002C1D RID: 11293
		protected static readonly int radiusbiasshift = 6;

		// Token: 0x04002C1E RID: 11294
		protected static readonly int radiusbias = 1 << NeuQuant.radiusbiasshift;

		// Token: 0x04002C1F RID: 11295
		protected static readonly int initradius = NeuQuant.initrad * NeuQuant.radiusbias;

		// Token: 0x04002C20 RID: 11296
		protected static readonly int radiusdec = 30;

		// Token: 0x04002C21 RID: 11297
		protected static readonly int alphabiasshift = 10;

		// Token: 0x04002C22 RID: 11298
		protected static readonly int initalpha = 1 << NeuQuant.alphabiasshift;

		// Token: 0x04002C23 RID: 11299
		protected int alphadec;

		// Token: 0x04002C24 RID: 11300
		protected static readonly int radbiasshift = 8;

		// Token: 0x04002C25 RID: 11301
		protected static readonly int radbias = 1 << NeuQuant.radbiasshift;

		// Token: 0x04002C26 RID: 11302
		protected static readonly int alpharadbshift = NeuQuant.alphabiasshift + NeuQuant.radbiasshift;

		// Token: 0x04002C27 RID: 11303
		protected static readonly int alpharadbias = 1 << NeuQuant.alpharadbshift;

		// Token: 0x04002C28 RID: 11304
		protected byte[] thepicture;

		// Token: 0x04002C29 RID: 11305
		protected int lengthcount;

		// Token: 0x04002C2A RID: 11306
		protected int samplefac;

		// Token: 0x04002C2B RID: 11307
		protected int[][] network;

		// Token: 0x04002C2C RID: 11308
		protected int[] netindex = new int[256];

		// Token: 0x04002C2D RID: 11309
		protected int[] bias = new int[NeuQuant.PaletteSize];

		// Token: 0x04002C2E RID: 11310
		protected int[] freq = new int[NeuQuant.PaletteSize];

		// Token: 0x04002C2F RID: 11311
		protected int[] radpower = new int[NeuQuant.initrad];
	}
}
