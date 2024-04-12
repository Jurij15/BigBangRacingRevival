using System;
using System.Threading;
using UnityEngine;

// Token: 0x02000579 RID: 1401
public class TextureScale
{
	// Token: 0x060028CA RID: 10442 RVA: 0x001B1330 File Offset: 0x001AF730
	public static void Point(Texture2D tex, int newWidth, int newHeight)
	{
		TextureScale.ThreadedScale(tex, newWidth, newHeight, false);
	}

	// Token: 0x060028CB RID: 10443 RVA: 0x001B133B File Offset: 0x001AF73B
	public static void Bilinear(Texture2D tex, int newWidth, int newHeight)
	{
		TextureScale.ThreadedScale(tex, newWidth, newHeight, true);
	}

	// Token: 0x060028CC RID: 10444 RVA: 0x001B1348 File Offset: 0x001AF748
	private static void ThreadedScale(Texture2D tex, int newWidth, int newHeight, bool useBilinear)
	{
		TextureScale.texColors = tex.GetPixels();
		TextureScale.newColors = new Color[newWidth * newHeight];
		if (useBilinear)
		{
			TextureScale.ratioX = 1f / ((float)newWidth / (float)(tex.width - 1));
			TextureScale.ratioY = 1f / ((float)newHeight / (float)(tex.height - 1));
		}
		else
		{
			TextureScale.ratioX = (float)tex.width / (float)newWidth;
			TextureScale.ratioY = (float)tex.height / (float)newHeight;
		}
		TextureScale.w = tex.width;
		TextureScale.w2 = newWidth;
		int num = Mathf.Min(SystemInfo.processorCount, newHeight);
		int num2 = newHeight / num;
		TextureScale.finishCount = 0;
		if (TextureScale.mutex == null)
		{
			TextureScale.mutex = new Mutex(false);
		}
		if (num > 1)
		{
			int i;
			TextureScale.ThreadData threadData;
			for (i = 0; i < num - 1; i++)
			{
				threadData = new TextureScale.ThreadData(num2 * i, num2 * (i + 1));
				ParameterizedThreadStart parameterizedThreadStart = ((!useBilinear) ? new ParameterizedThreadStart(TextureScale.PointScale) : new ParameterizedThreadStart(TextureScale.BilinearScale));
				Thread thread = new Thread(parameterizedThreadStart);
				thread.Start(threadData);
			}
			threadData = new TextureScale.ThreadData(num2 * i, newHeight);
			if (useBilinear)
			{
				TextureScale.BilinearScale(threadData);
			}
			else
			{
				TextureScale.PointScale(threadData);
			}
			while (TextureScale.finishCount < num)
			{
				Thread.Sleep(1);
			}
		}
		else
		{
			TextureScale.ThreadData threadData2 = new TextureScale.ThreadData(0, newHeight);
			if (useBilinear)
			{
				TextureScale.BilinearScale(threadData2);
			}
			else
			{
				TextureScale.PointScale(threadData2);
			}
		}
		tex.Resize(newWidth, newHeight);
		tex.SetPixels(TextureScale.newColors);
		tex.Apply();
	}

	// Token: 0x060028CD RID: 10445 RVA: 0x001B14DC File Offset: 0x001AF8DC
	public static void BilinearScale(object obj)
	{
		TextureScale.ThreadData threadData = (TextureScale.ThreadData)obj;
		for (int i = threadData.start; i < threadData.end; i++)
		{
			int num = (int)Mathf.Floor((float)i * TextureScale.ratioY);
			int num2 = num * TextureScale.w;
			int num3 = (num + 1) * TextureScale.w;
			int num4 = i * TextureScale.w2;
			for (int j = 0; j < TextureScale.w2; j++)
			{
				int num5 = (int)Mathf.Floor((float)j * TextureScale.ratioX);
				float num6 = (float)j * TextureScale.ratioX - (float)num5;
				TextureScale.newColors[num4 + j] = TextureScale.ColorLerpUnclamped(TextureScale.ColorLerpUnclamped(TextureScale.texColors[num2 + num5], TextureScale.texColors[num2 + num5 + 1], num6), TextureScale.ColorLerpUnclamped(TextureScale.texColors[num3 + num5], TextureScale.texColors[num3 + num5 + 1], num6), (float)i * TextureScale.ratioY - (float)num);
			}
		}
		TextureScale.mutex.WaitOne();
		TextureScale.finishCount++;
		TextureScale.mutex.ReleaseMutex();
	}

	// Token: 0x060028CE RID: 10446 RVA: 0x001B1618 File Offset: 0x001AFA18
	public static void PointScale(object obj)
	{
		TextureScale.ThreadData threadData = (TextureScale.ThreadData)obj;
		for (int i = threadData.start; i < threadData.end; i++)
		{
			int num = (int)(TextureScale.ratioY * (float)i) * TextureScale.w;
			int num2 = i * TextureScale.w2;
			for (int j = 0; j < TextureScale.w2; j++)
			{
				TextureScale.newColors[num2 + j] = TextureScale.texColors[(int)((float)num + TextureScale.ratioX * (float)j)];
			}
		}
		TextureScale.mutex.WaitOne();
		TextureScale.finishCount++;
		TextureScale.mutex.ReleaseMutex();
	}

	// Token: 0x060028CF RID: 10447 RVA: 0x001B16CC File Offset: 0x001AFACC
	private static Color ColorLerpUnclamped(Color c1, Color c2, float value)
	{
		return new Color(c1.r + (c2.r - c1.r) * value, c1.g + (c2.g - c1.g) * value, c1.b + (c2.b - c1.b) * value, c1.a + (c2.a - c1.a) * value);
	}

	// Token: 0x04002DFC RID: 11772
	private static Color[] texColors;

	// Token: 0x04002DFD RID: 11773
	private static Color[] newColors;

	// Token: 0x04002DFE RID: 11774
	private static int w;

	// Token: 0x04002DFF RID: 11775
	private static float ratioX;

	// Token: 0x04002E00 RID: 11776
	private static float ratioY;

	// Token: 0x04002E01 RID: 11777
	private static int w2;

	// Token: 0x04002E02 RID: 11778
	private static int finishCount;

	// Token: 0x04002E03 RID: 11779
	private static Mutex mutex;

	// Token: 0x0200057A RID: 1402
	public class ThreadData
	{
		// Token: 0x060028D0 RID: 10448 RVA: 0x001B1742 File Offset: 0x001AFB42
		public ThreadData(int s, int e)
		{
			this.start = s;
			this.end = e;
		}

		// Token: 0x04002E04 RID: 11780
		public int start;

		// Token: 0x04002E05 RID: 11781
		public int end;
	}
}
