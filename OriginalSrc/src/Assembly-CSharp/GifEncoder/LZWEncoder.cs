using System;
using System.IO;

namespace GifEncoder
{
	// Token: 0x0200051D RID: 1309
	public class LZWEncoder
	{
		// Token: 0x0600268B RID: 9867 RVA: 0x001A66CC File Offset: 0x001A4ACC
		public LZWEncoder(int width, int height, byte[] pixels, int color_depth)
		{
			this.imgW = width;
			this.imgH = height;
			this.pixAry = pixels;
			this.initCodeSize = Math.Max(2, color_depth);
		}

		// Token: 0x0600268C RID: 9868 RVA: 0x001A6770 File Offset: 0x001A4B70
		private void Add(byte c, Stream outs)
		{
			this.accum[this.a_count++] = c;
			if (this.a_count >= 254)
			{
				this.Flush(outs);
			}
		}

		// Token: 0x0600268D RID: 9869 RVA: 0x001A67AD File Offset: 0x001A4BAD
		private void ClearTable(Stream outs)
		{
			this.ResetCodeTable(this.hsize);
			this.free_ent = this.ClearCode + 2;
			this.clear_flg = true;
			this.Output(this.ClearCode, outs);
		}

		// Token: 0x0600268E RID: 9870 RVA: 0x001A67E0 File Offset: 0x001A4BE0
		private void ResetCodeTable(int hsize)
		{
			for (int i = 0; i < hsize; i++)
			{
				this.htab[i] = -1;
			}
		}

		// Token: 0x0600268F RID: 9871 RVA: 0x001A6808 File Offset: 0x001A4C08
		private void Compress(int init_bits, Stream outs)
		{
			this.g_init_bits = init_bits;
			this.clear_flg = false;
			this.n_bits = this.g_init_bits;
			this.maxcode = this.MaxCode(this.n_bits);
			this.ClearCode = 1 << init_bits - 1;
			this.EOFCode = this.ClearCode + 1;
			this.free_ent = this.ClearCode + 2;
			this.a_count = 0;
			int num = this.NextPixel();
			int num2 = 0;
			for (int i = this.hsize; i < 65536; i *= 2)
			{
				num2++;
			}
			num2 = 8 - num2;
			int num3 = this.hsize;
			this.ResetCodeTable(num3);
			this.Output(this.ClearCode, outs);
			for (;;)
			{
				int num4;
				while ((num4 = this.NextPixel()) != LZWEncoder.EOF)
				{
					int i = (num4 << this.maxbits) + num;
					int num5 = (num4 << num2) ^ num;
					if (this.htab[num5] == i)
					{
						num = this.codetab[num5];
					}
					else
					{
						if (this.htab[num5] >= 0)
						{
							int num6 = num3 - num5;
							if (num5 == 0)
							{
								num6 = 1;
							}
							for (;;)
							{
								if ((num5 -= num6) < 0)
								{
									num5 += num3;
								}
								if (this.htab[num5] == i)
								{
									break;
								}
								if (this.htab[num5] < 0)
								{
									goto IL_13C;
								}
							}
							num = this.codetab[num5];
							continue;
						}
						IL_13C:
						this.Output(num, outs);
						num = num4;
						if (this.free_ent < this.maxmaxcode)
						{
							this.codetab[num5] = this.free_ent++;
							this.htab[num5] = i;
						}
						else
						{
							this.ClearTable(outs);
						}
					}
				}
				break;
			}
			this.Output(num, outs);
			this.Output(this.EOFCode, outs);
		}

		// Token: 0x06002690 RID: 9872 RVA: 0x001A69C4 File Offset: 0x001A4DC4
		public void Encode(Stream os)
		{
			os.WriteByte(Convert.ToByte(this.initCodeSize));
			this.remaining = this.imgW * this.imgH;
			this.curPixel = 0;
			this.Compress(this.initCodeSize + 1, os);
			os.WriteByte(0);
		}

		// Token: 0x06002691 RID: 9873 RVA: 0x001A6A12 File Offset: 0x001A4E12
		private void Flush(Stream outs)
		{
			if (this.a_count > 0)
			{
				outs.WriteByte(Convert.ToByte(this.a_count));
				outs.Write(this.accum, 0, this.a_count);
				this.a_count = 0;
			}
		}

		// Token: 0x06002692 RID: 9874 RVA: 0x001A6A4B File Offset: 0x001A4E4B
		private int MaxCode(int n_bits)
		{
			return (1 << n_bits) - 1;
		}

		// Token: 0x06002693 RID: 9875 RVA: 0x001A6A58 File Offset: 0x001A4E58
		private int NextPixel()
		{
			if (this.remaining == 0)
			{
				return LZWEncoder.EOF;
			}
			this.remaining--;
			int num = this.curPixel + 1;
			if (num < this.pixAry.GetUpperBound(0))
			{
				byte b = this.pixAry[this.curPixel++];
				return (int)(b & byte.MaxValue);
			}
			return 255;
		}

		// Token: 0x06002694 RID: 9876 RVA: 0x001A6AC8 File Offset: 0x001A4EC8
		private void Output(int code, Stream outs)
		{
			this.cur_accum &= this.masks[this.cur_bits];
			if (this.cur_bits > 0)
			{
				this.cur_accum |= code << this.cur_bits;
			}
			else
			{
				this.cur_accum = code;
			}
			this.cur_bits += this.n_bits;
			while (this.cur_bits >= 8)
			{
				this.Add((byte)(this.cur_accum & 255), outs);
				this.cur_accum >>= 8;
				this.cur_bits -= 8;
			}
			if (this.free_ent > this.maxcode || this.clear_flg)
			{
				if (this.clear_flg)
				{
					this.maxcode = this.MaxCode(this.n_bits = this.g_init_bits);
					this.clear_flg = false;
				}
				else
				{
					this.n_bits++;
					if (this.n_bits == this.maxbits)
					{
						this.maxcode = this.maxmaxcode;
					}
					else
					{
						this.maxcode = this.MaxCode(this.n_bits);
					}
				}
			}
			if (code == this.EOFCode)
			{
				while (this.cur_bits > 0)
				{
					this.Add((byte)(this.cur_accum & 255), outs);
					this.cur_accum >>= 8;
					this.cur_bits -= 8;
				}
				this.Flush(outs);
			}
		}

		// Token: 0x04002BF2 RID: 11250
		private static readonly int EOF = -1;

		// Token: 0x04002BF3 RID: 11251
		private int imgW;

		// Token: 0x04002BF4 RID: 11252
		private int imgH;

		// Token: 0x04002BF5 RID: 11253
		private byte[] pixAry;

		// Token: 0x04002BF6 RID: 11254
		private int initCodeSize;

		// Token: 0x04002BF7 RID: 11255
		private int remaining;

		// Token: 0x04002BF8 RID: 11256
		private int curPixel;

		// Token: 0x04002BF9 RID: 11257
		private static readonly int BITS = 12;

		// Token: 0x04002BFA RID: 11258
		private static readonly int HSIZE = 5003;

		// Token: 0x04002BFB RID: 11259
		private int n_bits;

		// Token: 0x04002BFC RID: 11260
		private int maxbits = LZWEncoder.BITS;

		// Token: 0x04002BFD RID: 11261
		private int maxcode;

		// Token: 0x04002BFE RID: 11262
		private int maxmaxcode = 1 << LZWEncoder.BITS;

		// Token: 0x04002BFF RID: 11263
		private int[] htab = new int[LZWEncoder.HSIZE];

		// Token: 0x04002C00 RID: 11264
		private int[] codetab = new int[LZWEncoder.HSIZE];

		// Token: 0x04002C01 RID: 11265
		private int hsize = LZWEncoder.HSIZE;

		// Token: 0x04002C02 RID: 11266
		private int free_ent;

		// Token: 0x04002C03 RID: 11267
		private bool clear_flg;

		// Token: 0x04002C04 RID: 11268
		private int g_init_bits;

		// Token: 0x04002C05 RID: 11269
		private int ClearCode;

		// Token: 0x04002C06 RID: 11270
		private int EOFCode;

		// Token: 0x04002C07 RID: 11271
		private int cur_accum;

		// Token: 0x04002C08 RID: 11272
		private int cur_bits;

		// Token: 0x04002C09 RID: 11273
		private int[] masks = new int[]
		{
			0, 1, 3, 7, 15, 31, 63, 127, 255, 511,
			1023, 2047, 4095, 8191, 16383, 32767, 65535
		};

		// Token: 0x04002C0A RID: 11274
		private int a_count;

		// Token: 0x04002C0B RID: 11275
		private byte[] accum = new byte[256];
	}
}
