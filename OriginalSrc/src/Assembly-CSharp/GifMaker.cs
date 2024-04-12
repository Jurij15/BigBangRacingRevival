using System;
using System.Collections;
using System.IO;
using System.Threading;
using GifEncoder;
using UnityEngine;

// Token: 0x02000520 RID: 1312
public class GifMaker : MonoBehaviour
{
	// Token: 0x170000C5 RID: 197
	// (get) Token: 0x060026A5 RID: 9893 RVA: 0x001A7793 File Offset: 0x001A5B93
	public static bool IsRecording
	{
		get
		{
			return GifMaker.Instance._recording;
		}
	}

	// Token: 0x170000C6 RID: 198
	// (get) Token: 0x060026A6 RID: 9894 RVA: 0x001A779F File Offset: 0x001A5B9F
	public static bool IsPaused
	{
		get
		{
			return GifMaker.Instance._paused;
		}
	}

	// Token: 0x170000C7 RID: 199
	// (get) Token: 0x060026A7 RID: 9895 RVA: 0x001A77AB File Offset: 0x001A5BAB
	public static int GetGifProgress
	{
		get
		{
			return GifMaker.Instance._gifProgress;
		}
	}

	// Token: 0x170000C8 RID: 200
	// (get) Token: 0x060026A8 RID: 9896 RVA: 0x001A77B7 File Offset: 0x001A5BB7
	// (set) Token: 0x060026A9 RID: 9897 RVA: 0x001A77BE File Offset: 0x001A5BBE
	public static GifMaker Instance { get; private set; }

	// Token: 0x060026AA RID: 9898 RVA: 0x001A77C6 File Offset: 0x001A5BC6
	private void Awake()
	{
		GifMaker.Instance = this;
	}

	// Token: 0x060026AB RID: 9899 RVA: 0x001A77D0 File Offset: 0x001A5BD0
	private void Update()
	{
		if (this._recording && !this._paused)
		{
			if (this._time >= this._lastTime + this.RecordOptions.FrameInterval)
			{
				base.StartCoroutine(this.CaptureFrame());
				this._lastTime = this._time;
			}
			this._time += 0.016666668f * (float)Main.m_ticksPerFrame;
		}
	}

	// Token: 0x060026AC RID: 9900 RVA: 0x001A7844 File Offset: 0x001A5C44
	public void CaptureFrameTest()
	{
		this.gifTest.m_gifTexture = this._renderTextures[this._nextFrame];
		this._nextFrame = ToolBox.getRolledValue(this._nextFrame + 1, 0, this.RecordOptions.MaxFramesToKeep - 1);
		this._textureCount = ToolBox.limitBetween(this._textureCount + 1, 0, this.RecordOptions.MaxFramesToKeep);
	}

	// Token: 0x060026AD RID: 9901 RVA: 0x001A78A9 File Offset: 0x001A5CA9
	public static void StartRecord()
	{
		GifMaker.Instance.StartRecordThis();
	}

	// Token: 0x060026AE RID: 9902 RVA: 0x001A78B5 File Offset: 0x001A5CB5
	public static void StartRecord(GifMaker.GifRecordOptions gifRecordOptionses)
	{
		GifMaker.Instance.RecordOptions = gifRecordOptionses;
		GifMaker.Instance._paused = false;
		GifMaker.Instance.StartRecordThis();
	}

	// Token: 0x060026AF RID: 9903 RVA: 0x001A78D7 File Offset: 0x001A5CD7
	public static void StopRecord()
	{
		GifMaker.Instance.StopRecordThis();
	}

	// Token: 0x060026B0 RID: 9904 RVA: 0x001A78E3 File Offset: 0x001A5CE3
	public static void PauseRecord(bool _pause)
	{
		GifMaker.Instance._paused = _pause;
	}

	// Token: 0x060026B1 RID: 9905 RVA: 0x001A78F0 File Offset: 0x001A5CF0
	public static void SaveGif()
	{
		GifMaker.Instance.SaveGifThis();
	}

	// Token: 0x060026B2 RID: 9906 RVA: 0x001A78FC File Offset: 0x001A5CFC
	public static void StopEncode()
	{
		GifMaker.Instance.StopEncodeThis();
	}

	// Token: 0x060026B3 RID: 9907 RVA: 0x001A7908 File Offset: 0x001A5D08
	public static void SaveGif(GifMaker.GifEncodeOptions gifEncodeOptionses, Material overlayMaterial)
	{
		GifMaker.OverlayMat = overlayMaterial;
		GifMaker.Instance.EncodeOptions = gifEncodeOptionses;
		GifMaker.Instance.SaveGifThis();
	}

	// Token: 0x060026B4 RID: 9908 RVA: 0x001A7925 File Offset: 0x001A5D25
	public static void FreeMem()
	{
		GifMaker.Instance.StopRecordThis();
		GifMaker.Instance.FreeMemThis();
	}

	// Token: 0x060026B5 RID: 9909 RVA: 0x001A793B File Offset: 0x001A5D3B
	public static Texture[] GetFrames()
	{
		return GifMaker.Instance.GetFramesThis();
	}

	// Token: 0x060026B6 RID: 9910 RVA: 0x001A7948 File Offset: 0x001A5D48
	private void StartRecordThis()
	{
		this._time = 0f;
		this._gifProgress = 0;
		if (this._recording)
		{
			return;
		}
		this._finalTextures = null;
		this.gifTest = this.RecordOptions.TargetCameras[0].GetComponent<GifCamera>();
		if (this._renderTextures == null)
		{
			this._renderTextures = new RenderTexture[this.RecordOptions.MaxFramesToKeep];
			RenderTextureFormat renderTextureFormat = 4;
			if (SystemInfo.graphicsDeviceType == 16)
			{
				renderTextureFormat = 7;
			}
			for (int i = 0; i < this._renderTextures.Length; i++)
			{
				this._renderTextures[i] = new RenderTexture(this.RecordOptions.FrameRectWidth, this.RecordOptions.FrameRectHeight, 24, renderTextureFormat);
			}
			Debug.Log("GifMaker: Render textures initialized", null);
		}
		this._textureCount = 0;
		this._nextFrame = 0;
		this._lastTime = 0f;
		this._recording = true;
	}

	// Token: 0x060026B7 RID: 9911 RVA: 0x001A7A2D File Offset: 0x001A5E2D
	private void StopRecordThis()
	{
		this._recording = false;
	}

	// Token: 0x060026B8 RID: 9912 RVA: 0x001A7A38 File Offset: 0x001A5E38
	private void SaveGifThis()
	{
		if (this._textureCount == 0 || this._recording)
		{
			return;
		}
		this._finalTextures = new Color32[this._textureCount][];
		RenderTexture active = RenderTexture.active;
		RenderTexture.active = this._renderTextures[0];
		Texture2D texture2D = new Texture2D(RenderTexture.active.width, RenderTexture.active.height, 3, false);
		GL.PushMatrix();
		GL.LoadPixelMatrix(0f, 256f, 256f, 0f);
		for (int i = 0; i < this._textureCount; i++)
		{
			int num;
			if (this._textureCount < this.RecordOptions.MaxFramesToKeep)
			{
				num = i;
			}
			else
			{
				num = ToolBox.getRolledValue(this._nextFrame + i, 0, this.RecordOptions.MaxFramesToKeep - 1);
			}
			RenderTexture.active = this._renderTextures[num];
			Graphics.DrawTexture(new Rect(0f, 0f, 256f, 256f), GifMaker.OverlayMat.mainTexture, GifMaker.OverlayMat);
			texture2D.ReadPixels(new Rect(0f, 0f, (float)texture2D.width, (float)texture2D.height), 0, 0, false);
			this._finalTextures[i] = texture2D.GetPixels32();
		}
		GL.PopMatrix();
		RenderTexture.active = active;
		Object.DestroyImmediate(texture2D);
		this._encodeThread = new Thread(new ThreadStart(this.EncodeThread));
		this._encodeThread.Start();
	}

	// Token: 0x060026B9 RID: 9913 RVA: 0x001A7BAC File Offset: 0x001A5FAC
	private void EncodeThread()
	{
		Debug.Log("Encode GIF started...", null);
		AnimatedGifEncoder animatedGifEncoder;
		if (this.EncodeOptions.OutputPath != null)
		{
			animatedGifEncoder = new AnimatedGifEncoder(this.EncodeOptions.OutputPath);
		}
		else
		{
			animatedGifEncoder = new AnimatedGifEncoder(this.EncodeOptions.Stream);
		}
		animatedGifEncoder.SetDelay((int)(this.EncodeOptions.Delay * 1000f));
		for (int i = 0; i < this._finalTextures.Length; i++)
		{
			Debug.Log("Encodding frame " + i, null);
			animatedGifEncoder.AddFrame(this._finalTextures[i], this.RecordOptions.FrameRectWidth, this.RecordOptions.FrameRectHeight);
			this._finalTextures[i] = null;
			this._gifProgress = i;
		}
		this._finalTextures = null;
		animatedGifEncoder.Finish();
		if (this.EncodeOptions.OnSaveComplete != null)
		{
			this.EncodeOptions.OnSaveComplete();
		}
	}

	// Token: 0x060026BA RID: 9914 RVA: 0x001A7CA3 File Offset: 0x001A60A3
	private void StopEncodeThis()
	{
		if (this._encodeThread != null)
		{
			if (this._encodeThread.IsAlive)
			{
				this._encodeThread.Abort();
				this._finalTextures = null;
			}
			this._encodeThread = null;
		}
	}

	// Token: 0x060026BB RID: 9915 RVA: 0x001A7CDC File Offset: 0x001A60DC
	private IEnumerator CaptureFrame()
	{
		if (this.RecordOptions.OnStartFrameCapture != null)
		{
			this.RecordOptions.OnStartFrameCapture();
		}
		for (int i = 0; i < this.RecordOptions.TargetCameras.Length; i++)
		{
			if (this.RecordOptions.TargetCameras != null)
			{
				this.RecordOptions.TargetCameras[i].targetTexture = this._renderTextures[this._nextFrame];
				this.RecordOptions.TargetCameras[i].Render();
			}
		}
		this._nextFrame = ToolBox.getRolledValue(this._nextFrame + 1, 0, this.RecordOptions.MaxFramesToKeep - 1);
		this._textureCount = ToolBox.limitBetween(this._textureCount + 1, 0, this.RecordOptions.MaxFramesToKeep);
		for (int j = 0; j < this.RecordOptions.TargetCameras.Length; j++)
		{
			if (this.RecordOptions.TargetCameras != null)
			{
				this.RecordOptions.TargetCameras[j].targetTexture = null;
			}
		}
		if (this.RecordOptions.OnEndFrameCapture != null)
		{
			this.RecordOptions.OnEndFrameCapture();
		}
		yield return true;
		yield break;
	}

	// Token: 0x060026BC RID: 9916 RVA: 0x001A7CF7 File Offset: 0x001A60F7
	public static void CaptureFrame(RenderTexture _src)
	{
	}

	// Token: 0x060026BD RID: 9917 RVA: 0x001A7CF9 File Offset: 0x001A60F9
	public static RenderTexture GetCurrentTex()
	{
		return GifMaker.Instance._renderTextures[GifMaker.Instance._nextFrame];
	}

	// Token: 0x060026BE RID: 9918 RVA: 0x001A7D10 File Offset: 0x001A6110
	private Texture[] GetFramesThis()
	{
		Texture[] array = new Texture[this._textureCount];
		for (int i = 0; i < this._textureCount; i++)
		{
			int num;
			if (this._textureCount < this.RecordOptions.MaxFramesToKeep)
			{
				num = i;
			}
			else
			{
				num = ToolBox.getRolledValue(this._nextFrame + i, 0, this.RecordOptions.MaxFramesToKeep - 1);
			}
			array[i] = this._renderTextures[num];
		}
		return array;
	}

	// Token: 0x060026BF RID: 9919 RVA: 0x001A7D85 File Offset: 0x001A6185
	private void FreeMemThis()
	{
		this.DestroyRenderTextures();
	}

	// Token: 0x060026C0 RID: 9920 RVA: 0x001A7D90 File Offset: 0x001A6190
	private void DestroyRenderTextures()
	{
		if (this._renderTextures != null)
		{
			foreach (RenderTexture renderTexture in this._renderTextures)
			{
				Object.DestroyImmediate(renderTexture);
			}
			this._renderTextures = null;
		}
	}

	// Token: 0x04002C30 RID: 11312
	public GifMaker.GifRecordOptions RecordOptions;

	// Token: 0x04002C31 RID: 11313
	public GifMaker.GifEncodeOptions EncodeOptions;

	// Token: 0x04002C33 RID: 11315
	public static byte[] GifFixedPalette;

	// Token: 0x04002C34 RID: 11316
	public static Material OverlayMat;

	// Token: 0x04002C35 RID: 11317
	private static int renderCounter;

	// Token: 0x04002C36 RID: 11318
	private bool _recording;

	// Token: 0x04002C37 RID: 11319
	private bool _paused;

	// Token: 0x04002C38 RID: 11320
	private RenderTexture[] _renderTextures;

	// Token: 0x04002C39 RID: 11321
	private Color32[][] _finalTextures;

	// Token: 0x04002C3A RID: 11322
	private int _textureCount;

	// Token: 0x04002C3B RID: 11323
	private int _nextFrame;

	// Token: 0x04002C3C RID: 11324
	private float _lastTime;

	// Token: 0x04002C3D RID: 11325
	private Thread _encodeThread;

	// Token: 0x04002C3E RID: 11326
	private int _gifProgress;

	// Token: 0x04002C3F RID: 11327
	private float _time;

	// Token: 0x04002C40 RID: 11328
	private GifCamera gifTest;

	// Token: 0x02000521 RID: 1313
	// (Invoke) Token: 0x060026C3 RID: 9923
	public delegate void StartFrameCapture();

	// Token: 0x02000522 RID: 1314
	// (Invoke) Token: 0x060026C7 RID: 9927
	public delegate void EndFrameCapture();

	// Token: 0x02000523 RID: 1315
	// (Invoke) Token: 0x060026CB RID: 9931
	public delegate void SaveComplete();

	// Token: 0x02000524 RID: 1316
	[Serializable]
	public struct GifRecordOptions
	{
		// Token: 0x060026CE RID: 9934 RVA: 0x001A7DD6 File Offset: 0x001A61D6
		public GifRecordOptions(Camera[] targetCameras, float frameInterval, Rect frameRect, int frameRectWidth, int frameRectHeight, GifMaker.StartFrameCapture onStartFrameCapture, GifMaker.EndFrameCapture onEndFrameCapture, int maxFrames)
		{
			this.TargetCameras = targetCameras;
			this.FrameInterval = frameInterval;
			this.FrameRect = frameRect;
			this.FrameRectWidth = frameRectWidth;
			this.FrameRectHeight = frameRectHeight;
			this.OnStartFrameCapture = onStartFrameCapture;
			this.OnEndFrameCapture = onEndFrameCapture;
			this.MaxFramesToKeep = maxFrames;
		}

		// Token: 0x060026CF RID: 9935 RVA: 0x001A7E18 File Offset: 0x001A6218
		public GifRecordOptions(Camera targetCamera, float frameInterval, Rect frameRect, int frameRectWidth, int frameRectHeight, GifMaker.StartFrameCapture onStartFrameCapture, GifMaker.EndFrameCapture onEndFrameCapture, int maxFrames)
		{
			this.TargetCameras = new Camera[1];
			this.TargetCameras[0] = targetCamera;
			this.FrameInterval = frameInterval;
			this.FrameRect = frameRect;
			this.FrameRectWidth = frameRectWidth;
			this.FrameRectHeight = frameRectHeight;
			this.OnStartFrameCapture = onStartFrameCapture;
			this.OnEndFrameCapture = onEndFrameCapture;
			this.MaxFramesToKeep = maxFrames;
		}

		// Token: 0x060026D0 RID: 9936 RVA: 0x001A7E70 File Offset: 0x001A6270
		public GifRecordOptions(Camera[] targetCameras, float frameInterval, Rect frameRect, int frameRectWidth, int frameRectHeight, int maxFrames)
		{
			this.TargetCameras = targetCameras;
			this.FrameInterval = frameInterval;
			this.FrameRect = frameRect;
			this.FrameRectWidth = frameRectWidth;
			this.FrameRectHeight = frameRectHeight;
			this.OnStartFrameCapture = null;
			this.OnEndFrameCapture = null;
			this.MaxFramesToKeep = maxFrames;
		}

		// Token: 0x060026D1 RID: 9937 RVA: 0x001A7EB0 File Offset: 0x001A62B0
		public GifRecordOptions(Camera targetCamera, float frameInterval, Rect frameRect, int frameRectWidth, int frameRectHeight, int maxFrames)
		{
			this.TargetCameras = new Camera[1];
			this.TargetCameras[0] = targetCamera;
			this.FrameInterval = frameInterval;
			this.FrameRect = frameRect;
			this.FrameRectWidth = frameRectWidth;
			this.FrameRectHeight = frameRectHeight;
			this.OnStartFrameCapture = null;
			this.OnEndFrameCapture = null;
			this.MaxFramesToKeep = maxFrames;
		}

		// Token: 0x04002C41 RID: 11329
		public Camera[] TargetCameras;

		// Token: 0x04002C42 RID: 11330
		public float FrameInterval;

		// Token: 0x04002C43 RID: 11331
		public Rect FrameRect;

		// Token: 0x04002C44 RID: 11332
		public int FrameRectWidth;

		// Token: 0x04002C45 RID: 11333
		public int FrameRectHeight;

		// Token: 0x04002C46 RID: 11334
		public GifMaker.StartFrameCapture OnStartFrameCapture;

		// Token: 0x04002C47 RID: 11335
		public GifMaker.EndFrameCapture OnEndFrameCapture;

		// Token: 0x04002C48 RID: 11336
		public int MaxFramesToKeep;
	}

	// Token: 0x02000525 RID: 1317
	[Serializable]
	public struct GifEncodeOptions
	{
		// Token: 0x060026D2 RID: 9938 RVA: 0x001A7F08 File Offset: 0x001A6308
		public GifEncodeOptions(Stream outputStream, int repeatCount, float quality, float delay, GifMaker.SaveComplete onSaveComplete)
		{
			this.OutputPath = null;
			this.Stream = outputStream;
			if (repeatCount < 0)
			{
				repeatCount = 0;
			}
			this.RepeatCount = repeatCount;
			if (quality < 0.05f)
			{
				quality = 0.05f;
			}
			if (quality > 1f)
			{
				quality = 1f;
			}
			this.Quality = quality;
			this.Delay = delay;
			this.OnSaveComplete = onSaveComplete;
		}

		// Token: 0x060026D3 RID: 9939 RVA: 0x001A7F70 File Offset: 0x001A6370
		public GifEncodeOptions(string outputPath, int repeatCount, float quality, float delay, GifMaker.SaveComplete onSaveComplete)
		{
			this.OutputPath = outputPath;
			if (repeatCount < 0)
			{
				repeatCount = 0;
			}
			this.RepeatCount = repeatCount;
			if (quality < 0.05f)
			{
				quality = 0.05f;
			}
			if (quality > 1f)
			{
				quality = 1f;
			}
			this.Quality = quality;
			this.Delay = delay;
			this.OnSaveComplete = onSaveComplete;
			this.Stream = null;
		}

		// Token: 0x060026D4 RID: 9940 RVA: 0x001A7FD8 File Offset: 0x001A63D8
		public GifEncodeOptions(string outputPath, int repeatCount, float quality, float delay)
		{
			this.OutputPath = outputPath;
			if (repeatCount < 0)
			{
				repeatCount = 0;
			}
			this.RepeatCount = repeatCount;
			if (quality < 0.05f)
			{
				quality = 0.05f;
			}
			if (quality > 1f)
			{
				quality = 1f;
			}
			this.Quality = quality;
			this.Delay = delay;
			this.OnSaveComplete = null;
			this.Stream = null;
		}

		// Token: 0x04002C49 RID: 11337
		public string OutputPath;

		// Token: 0x04002C4A RID: 11338
		public Stream Stream;

		// Token: 0x04002C4B RID: 11339
		public int RepeatCount;

		// Token: 0x04002C4C RID: 11340
		public float Quality;

		// Token: 0x04002C4D RID: 11341
		public float Delay;

		// Token: 0x04002C4E RID: 11342
		public GifMaker.SaveComplete OnSaveComplete;
	}
}
