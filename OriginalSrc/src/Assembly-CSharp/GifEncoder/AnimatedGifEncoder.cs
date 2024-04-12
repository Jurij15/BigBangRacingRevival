using System;
using System.IO;
using UnityEngine;

namespace GifEncoder
{
	// Token: 0x0200051C RID: 1308
	public class AnimatedGifEncoder
	{
		// Token: 0x0600267B RID: 9851 RVA: 0x001A6010 File Offset: 0x001A4410
		public AnimatedGifEncoder(string filePath)
		{
			this.fs = new FileStream(filePath, 4, 2, 0);
			this.WriteString("GIF89a");
		}

		// Token: 0x0600267C RID: 9852 RVA: 0x001A605C File Offset: 0x001A445C
		public AnimatedGifEncoder(Stream _stream)
		{
			this.fs = _stream;
			this.WriteString("GIF89a");
		}

		// Token: 0x0600267D RID: 9853 RVA: 0x001A6095 File Offset: 0x001A4495
		public void SetDelay(int ms)
		{
			this.delay = (int)Math.Round((double)((float)ms / 10f));
		}

		// Token: 0x0600267E RID: 9854 RVA: 0x001A60AC File Offset: 0x001A44AC
		public void AddFrame(Color32[] _pixels, int _width, int _height)
		{
			if (!this.sizeSet)
			{
				this.width = _width;
				this.height = _height;
				this.sizeSet = true;
			}
			this.currentFramePixels = _pixels;
			this.GetImagePixels();
			this.AnalyzePixels();
			if (this.firstFrame)
			{
				this.WriteLSD();
				this.WritePalette();
				if (this.repeat >= 0)
				{
					this.WriteNetscapeExt();
				}
			}
			this.WriteGraphicCtrlExt();
			this.WriteImageDesc();
			if (!this.firstFrame)
			{
				this.WritePalette();
			}
			this.WritePixels();
			this.firstFrame = false;
		}

		// Token: 0x0600267F RID: 9855 RVA: 0x001A613F File Offset: 0x001A453F
		public void Finish()
		{
			this.fs.WriteByte(59);
			this.fs.Flush();
			this.fs.Close();
		}

		// Token: 0x06002680 RID: 9856 RVA: 0x001A6164 File Offset: 0x001A4564
		public void Cancel()
		{
			FileStream fileStream = this.fs as FileStream;
			if (fileStream != null)
			{
				string name = fileStream.Name;
				File.Delete(name);
			}
			this.Finish();
		}

		// Token: 0x06002681 RID: 9857 RVA: 0x001A6198 File Offset: 0x001A4598
		private void AnalyzePixels()
		{
			if (this.firstFrame)
			{
				this.visiblePixels = new byte[this.pixels.Length];
			}
			this.nq = new NeuQuant(this.pixels, this.pixels.Length, this.sample);
			this.colorTab = this.nq.Process();
			int num = this.pixels.Length / 3;
			this.indexedPixels = new byte[num];
			for (int i = 0; i < num; i++)
			{
				int num2 = i * 3;
				int num3 = num2 + 1;
				int num4 = num3 + 1;
				bool flag = this.firstFrame || Math.Abs((int)(this.pixels[num2] - this.visiblePixels[num2])) > 3 || Math.Abs((int)(this.pixels[num3] - this.visiblePixels[num3])) > 3 || Math.Abs((int)(this.pixels[num4] - this.visiblePixels[num4])) > 3;
				int num5;
				if (flag)
				{
					this.visiblePixels[num2] = this.pixels[num2];
					this.visiblePixels[num3] = this.pixels[num3];
					this.visiblePixels[num4] = this.pixels[num4];
					num5 = this.nq.Map((int)this.pixels[num2], (int)this.pixels[num3], (int)this.pixels[num4]);
				}
				else
				{
					num5 = NeuQuant.PaletteSize;
				}
				this.usedEntry[num5] = true;
				this.indexedPixels[i] = (byte)num5;
			}
			this.colorDepth = (int)Math.Log((double)(NeuQuant.PaletteSize + 1), 2.0);
			this.palSize = this.colorDepth - 1;
		}

		// Token: 0x06002682 RID: 9858 RVA: 0x001A6338 File Offset: 0x001A4738
		private void GetImagePixels()
		{
			this.pixels = new byte[3 * this.currentFramePixels.Length];
			for (int i = 0; i < this.height; i++)
			{
				for (int j = 0; j < this.width; j++)
				{
					Color32 color = this.currentFramePixels[(this.height - 1 - i) * this.width + j];
					this.pixels[i * this.width * 3 + j * 3] = color.r;
					this.pixels[i * this.width * 3 + j * 3 + 1] = color.g;
					this.pixels[i * this.width * 3 + j * 3 + 2] = color.b;
				}
			}
		}

		// Token: 0x06002683 RID: 9859 RVA: 0x001A6404 File Offset: 0x001A4804
		private void WriteGraphicCtrlExt()
		{
			this.fs.WriteByte(33);
			this.fs.WriteByte(249);
			this.fs.WriteByte(4);
			if (this.firstFrame)
			{
				this.fs.WriteByte(0);
			}
			else
			{
				this.fs.WriteByte(5);
			}
			this.WriteShort(this.delay);
			this.fs.WriteByte((byte)NeuQuant.PaletteSize);
			this.fs.WriteByte(0);
		}

		// Token: 0x06002684 RID: 9860 RVA: 0x001A648C File Offset: 0x001A488C
		private void WriteImageDesc()
		{
			this.fs.WriteByte(44);
			this.WriteShort(0);
			this.WriteShort(0);
			this.WriteShort(this.width);
			this.WriteShort(this.height);
			if (this.firstFrame)
			{
				this.fs.WriteByte(0);
			}
			else
			{
				this.fs.WriteByte(Convert.ToByte(135));
			}
		}

		// Token: 0x06002685 RID: 9861 RVA: 0x001A6500 File Offset: 0x001A4900
		private void WriteLSD()
		{
			this.WriteShort(this.width);
			this.WriteShort(this.height);
			this.fs.WriteByte(Convert.ToByte(240 | this.palSize));
			this.fs.WriteByte(0);
			this.fs.WriteByte(0);
		}

		// Token: 0x06002686 RID: 9862 RVA: 0x001A655C File Offset: 0x001A495C
		private void WriteNetscapeExt()
		{
			this.fs.WriteByte(33);
			this.fs.WriteByte(byte.MaxValue);
			this.fs.WriteByte(11);
			this.WriteString("NETSCAPE2.0");
			this.fs.WriteByte(3);
			this.fs.WriteByte(1);
			this.WriteShort(this.repeat);
			this.fs.WriteByte(0);
		}

		// Token: 0x06002687 RID: 9863 RVA: 0x001A65D0 File Offset: 0x001A49D0
		private void WritePalette()
		{
			this.fs.Write(this.colorTab, 0, this.colorTab.Length);
			int num = 3 * (NeuQuant.PaletteSize + 1) - this.colorTab.Length;
			for (int i = 0; i < num; i++)
			{
				this.fs.WriteByte(0);
			}
		}

		// Token: 0x06002688 RID: 9864 RVA: 0x001A6628 File Offset: 0x001A4A28
		private void WritePixels()
		{
			LZWEncoder lzwencoder = new LZWEncoder(this.width, this.height, this.indexedPixels, this.colorDepth);
			lzwencoder.Encode(this.fs);
		}

		// Token: 0x06002689 RID: 9865 RVA: 0x001A665F File Offset: 0x001A4A5F
		private void WriteShort(int value)
		{
			this.fs.WriteByte(Convert.ToByte(value & 255));
			this.fs.WriteByte(Convert.ToByte((value >> 8) & 255));
		}

		// Token: 0x0600268A RID: 9866 RVA: 0x001A6694 File Offset: 0x001A4A94
		private void WriteString(string s)
		{
			char[] array = s.ToCharArray();
			for (int i = 0; i < array.Length; i++)
			{
				this.fs.WriteByte((byte)array[i]);
			}
		}

		// Token: 0x04002BE1 RID: 11233
		protected int width;

		// Token: 0x04002BE2 RID: 11234
		protected int height;

		// Token: 0x04002BE3 RID: 11235
		protected int repeat;

		// Token: 0x04002BE4 RID: 11236
		protected int delay;

		// Token: 0x04002BE5 RID: 11237
		protected Stream fs;

		// Token: 0x04002BE6 RID: 11238
		protected Color32[] currentFramePixels;

		// Token: 0x04002BE7 RID: 11239
		protected byte[] pixels;

		// Token: 0x04002BE8 RID: 11240
		protected byte[] visiblePixels;

		// Token: 0x04002BE9 RID: 11241
		protected byte[] indexedPixels;

		// Token: 0x04002BEA RID: 11242
		protected int colorDepth;

		// Token: 0x04002BEB RID: 11243
		protected byte[] colorTab;

		// Token: 0x04002BEC RID: 11244
		protected bool[] usedEntry = new bool[256];

		// Token: 0x04002BED RID: 11245
		protected int palSize;

		// Token: 0x04002BEE RID: 11246
		protected bool firstFrame = true;

		// Token: 0x04002BEF RID: 11247
		protected bool sizeSet;

		// Token: 0x04002BF0 RID: 11248
		protected int sample = 10;

		// Token: 0x04002BF1 RID: 11249
		protected NeuQuant nq;
	}
}
