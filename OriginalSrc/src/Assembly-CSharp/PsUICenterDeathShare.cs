using System;
using System.IO;
using DeepLink;
using Server;
using UnityEngine;

// Token: 0x020002AE RID: 686
public class PsUICenterDeathShare : UICanvas
{
	// Token: 0x0600149B RID: 5275 RVA: 0x000D37F0 File Offset: 0x000D1BF0
	public PsUICenterDeathShare(UIComponent _parent)
		: base(_parent, true, string.Empty, null, string.Empty)
	{
		this.SetWidth(1f, RelativeTo.ScreenWidth);
		this.SetHeight(1f, RelativeTo.ScreenHeight);
		this.RemoveDrawHandler();
		string text = "<color=#d1f0d8>WASTED!</color>";
		switch (PsState.m_lastDeathReason)
		{
		case DeathReason.EJECT:
			text = "<color=#d1f0d8>" + PsStrings.Get(StringID.DEATH_SCREEN_REASON_EJECT) + "</color>";
			break;
		case DeathReason.EXPLODED:
			text = "<color=#d1f0d8>" + PsStrings.Get(StringID.DEATH_SCREEN_REASON_EXPLOSION) + "</color>";
			break;
		case DeathReason.ELECTRIFIED:
			text = "<color=#d1f0d8>" + PsStrings.Get(StringID.DEATH_SCREEN_REASON_ELECTRIFIED) + "</color>";
			break;
		case DeathReason.NECKSNAP:
			text = "<color=#d1f0d8>" + PsStrings.Get(StringID.DEATH_SCREEN_REASON_NECKSNAP) + "</color>";
			break;
		case DeathReason.BLACK_HOLE:
			text = "<color=#d1f0d8>" + PsStrings.Get(StringID.DEATH_SCREEN_REASON_BLACK_HOLE) + "</color>";
			break;
		}
		UITextbox uitextbox = new UITextbox(this, false, string.Empty, text, PsFontManager.GetFont(PsFonts.KGSecondChances), 0.1f, RelativeTo.ScreenHeight, false, Align.Center, Align.Top, null, true, null);
		uitextbox.SetWidth(0.75f, RelativeTo.ScreenWidth);
		uitextbox.SetVerticalAlign(0.95f);
		TransformS.SetRotation(uitextbox.m_TC, new Vector3(0f, 0f, 4f));
		TweenC tweenC = TweenS.AddTransformTween(uitextbox.m_TC, TweenedProperty.Scale, TweenStyle.QuadInOut, new Vector3(1.05f, 1.05f, 1f), 0.6f, 0f, false);
		TweenS.SetAdditionalTweenProperties(tweenC, -1, true, TweenStyle.QuadInOut);
	}

	// Token: 0x0600149C RID: 5276 RVA: 0x000D398C File Offset: 0x000D1D8C
	public override void Step()
	{
		if (this.m_gifDisplay != null)
		{
			TransformS.SetRotation(this.m_gifDisplay.m_TC, new Vector3(0f, 0f, 0f));
		}
		if (this.m_gifDisplay == null)
		{
			UIHorizontalList uihorizontalList = new UIHorizontalList(this, "hlist");
			uihorizontalList.SetVerticalAlign(0f);
			uihorizontalList.SetHorizontalAlign(0f);
			uihorizontalList.SetHeight(0.5f, RelativeTo.ScreenHeight);
			uihorizontalList.SetMargins(0.05f, RelativeTo.ScreenHeight);
			uihorizontalList.RemoveDrawHandler();
			this.m_gifDisplay = new PsUIGIFDisplay(uihorizontalList, null, this.gifFrameDelay, true, "gifDisplay", 0.005f);
			this.m_gifDisplay.SetWidth(0.425f, RelativeTo.ScreenHeight);
			this.m_gifDisplay.SetHorizontalAlign(0f);
			TweenS.AddTransformTween(this.m_gifDisplay.m_TC, TweenedProperty.Scale, TweenStyle.ElasticOut, Vector3.zero, Vector3.one, 0.8f, 0.5f, true);
			this.Update();
			TransformS.SetScale(this.m_gifDisplay.m_TC, 0f);
		}
		if (!GifMaker.IsRecording && this.m_gifDisplay.m_frames == null)
		{
			this.m_gifDisplay.SetFrames(GifMaker.GetFrames());
		}
		if (this.m_gifDisplay != null && !this.m_processing)
		{
			if (this.m_gifDisplay.m_hit && this.m_gifDisplay.m_frames != null)
			{
				if (this.m_stream == null)
				{
					this.m_stream = new MemoryStream();
					GifMaker.GifEncodeOptions gifEncodeOptions = new GifMaker.GifEncodeOptions(this.m_stream, 0, 0.5f, this.gifFrameDelay, new GifMaker.SaveComplete(this.GIFEncodeComplete));
					GifMaker.SaveGif(gifEncodeOptions, ResourceManager.GetMaterial(RESOURCE.Gif_Overlay_Mat_Material));
					this.m_gifDisplay.SetToProcessState();
					this.m_gifDisplay.SetProgressText(PsStrings.Get(StringID.DEATH_SCREEN_GIF_ENCODE));
					this.m_processing = true;
					Debug.Log("Started GIF Encode", null);
				}
				else if (this.m_gifMsg != null)
				{
					PsMetrics.GifShared(true);
					this.shareUrl();
				}
			}
			if (this.m_gifEncodeComplete)
			{
				this.m_gifDisplay.SetToProcessState();
				this.m_gifDisplay.SetProgressText(PsStrings.Get(StringID.DEATH_SCREEN_GIF_SEND));
				this.m_httpc = Gif.SaveWithLevel(PsState.m_activeGameLoop.m_minigameId, this.m_stream.ToArray(), new Action<string>(this.gifSendOk), new Action<HttpC>(this.gifSendFail), null);
				EntityManager.AddComponentToEntity(this.m_TC.p_entity, this.m_httpc);
				this.m_processing = true;
				this.m_gifEncodeComplete = false;
			}
		}
		if (this.m_processing)
		{
			if (this.m_httpc != null && this.m_httpc.www != null)
			{
				this.m_gifDisplay.UpdateProgress((int)(this.m_httpc.www.uploadProgress * 100f), 100);
			}
			else if (!this.m_gifEncodeComplete)
			{
				this.m_gifDisplay.UpdateProgress(GifMaker.GetGifProgress + 1, -1);
			}
		}
		if (this.m_gifDisplay != null)
		{
			TransformS.SetRotation(this.m_gifDisplay.m_TC, new Vector3(0f, 0f, 3f));
		}
		if (this.m_hit || Input.GetKeyDown(303))
		{
			GifMaker.StopEncode();
			(this.GetRoot() as PsUIBasePopup).CallAction("Exit");
		}
		base.Step();
	}

	// Token: 0x0600149D RID: 5277 RVA: 0x000D3CEE File Offset: 0x000D20EE
	private void GIFEncodeComplete()
	{
		this.m_gifEncodeComplete = true;
		this.m_processing = false;
		this.m_stream.Close();
		Debug.Log("GIF Encode Complete!", null);
	}

	// Token: 0x0600149E RID: 5278 RVA: 0x000D3D14 File Offset: 0x000D2114
	private void gifSendOk(string _url)
	{
		Debug.Log("GIF Url: " + _url, null);
		this.m_gifMsg = _url;
		this.m_gifMsg = this.m_gifMsg.Insert(0, "#bigbangracing\n");
		Debug.Log("Full msg: " + this.m_gifMsg, null);
		this.m_gifDisplay.SetToFinishedState();
		this.m_httpc = null;
		this.m_processing = false;
		PsMetrics.GifShared(false);
		this.shareUrl();
	}

	// Token: 0x0600149F RID: 5279 RVA: 0x000D3D8B File Offset: 0x000D218B
	private void gifSendFail(HttpC _c)
	{
		Debug.LogError("Gif upload failed.");
		this.m_processing = false;
		this.m_gifDisplay.SetToFailedState();
		this.m_gifMsg = null;
		this.m_httpc = null;
	}

	// Token: 0x060014A0 RID: 5280 RVA: 0x000D3DB7 File Offset: 0x000D21B7
	private void shareUrl()
	{
		Share.ShareTextOnPlatform(this.m_gifMsg);
	}

	// Token: 0x060014A1 RID: 5281 RVA: 0x000D3DC4 File Offset: 0x000D21C4
	public override void Destroy()
	{
		if (this.m_stream != null)
		{
			this.m_stream.Close();
		}
		base.Destroy();
		this.m_gifDisplay = null;
		this.m_httpc = null;
		this.m_stream = null;
	}

	// Token: 0x0400177A RID: 6010
	public PsUIGIFDisplay m_gifDisplay;

	// Token: 0x0400177B RID: 6011
	public MemoryStream m_stream;

	// Token: 0x0400177C RID: 6012
	private bool m_gifEncodeComplete;

	// Token: 0x0400177D RID: 6013
	private bool m_processing;

	// Token: 0x0400177E RID: 6014
	private string m_gifMsg;

	// Token: 0x0400177F RID: 6015
	private HttpC m_httpc;

	// Token: 0x04001780 RID: 6016
	private float gifFrameDelay = 0.06666667f;
}
