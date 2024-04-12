using System;
using System.Collections.Generic;
using Server;
using UnityEngine;

// Token: 0x020003B1 RID: 945
public class PsUIPreloaderCenter : UICanvas
{
	// Token: 0x06001B15 RID: 6933 RVA: 0x0012F1A4 File Offset: 0x0012D5A4
	public PsUIPreloaderCenter(UIComponent _parent)
		: base(_parent, false, "PreloaderCenter", null, string.Empty)
	{
		PsUIPreloaderCenter $this = this;
		PsBackgroundDownloader.Initialize();
		if (this.m_loaderEntity == null)
		{
			this.m_loaderEntity = EntityManager.AddEntity();
		}
		this.m_fadeDone = false;
		this.m_downloadFinished = false;
		this.m_fadeAlpha = 1f;
		this.m_progress = 0f;
		ServerManager.m_dontShowLoginPopup = true;
		this.m_fadeMaterial = new Material(Shader.Find("WOE/Unlit/ColorUnlitTransparent"));
		this.SetHeight(1f, RelativeTo.ScreenHeight);
		this.SetWidth(1f, RelativeTo.ScreenWidth);
		this.SetDrawHandler(new UIDrawDelegate(this.BackgroundDrawhandler));
		this.SetMargins(0.025f, RelativeTo.ScreenHeight);
		this.m_loadingBar = new UICanvas(this, false, "loadingBar", null, string.Empty);
		this.m_loadingBar.SetWidth(0.5f, RelativeTo.ScreenWidth);
		this.m_loadingBar.SetHeight(0.06f, RelativeTo.ScreenHeight);
		this.m_loadingBar.SetVerticalAlign(0f);
		this.m_loadingBar.SetDrawHandler(new UIDrawDelegate(this.LoadingBarDrawhandler));
		this.m_text = new UIText(this.m_loadingBar, false, string.Empty, PsStrings.Get(StringID.UPDATE_CHECK), PsFontManager.GetFont(PsFonts.KGSecondChances), 0.035f, RelativeTo.ScreenHeight, "#ffffff", null);
		this.m_text.SetVerticalAlign(0.6f);
		string clientVersion = Uri.EscapeUriString(Main.CLIENT_VERSION().ToString() + " " + Application.platform);
		Preload.CheckClientVersion(clientVersion, new Action<HttpC>(this.VersionCheckOk), delegate(HttpC c)
		{
			$this.VersionCheckFail(c, clientVersion);
		}, null);
		PsMetrics.PreloadStart();
	}

	// Token: 0x06001B16 RID: 6934 RVA: 0x0012F374 File Offset: 0x0012D774
	private void VersionCheckOk(HttpC _c)
	{
		Dictionary<string, object> dictionary = ClientTools.ParseServerResponse(_c.www.text);
		string text = "0";
		if (dictionary.ContainsKey("version"))
		{
			text = (string)dictionary["version"];
		}
		if (string.Compare(text, "upToDate") == 0)
		{
			this.m_clientIsUpToDate = true;
		}
		else
		{
			string text2 = "Please update your client.";
			if (dictionary.ContainsKey("versionMessage"))
			{
				text2 = (string)dictionary["versionMessage"];
			}
			Debug.LogError(string.Concat(new object[]
			{
				"ClientVersion: ",
				Main.CLIENT_VERSION(),
				", ServerVersion: ",
				text
			}));
			Debug.Log("Client has old version!", null);
			ServerManager.ThrowOldVersionMessage(text2);
			this.m_clientIsUpToDate = false;
		}
	}

	// Token: 0x06001B17 RID: 6935 RVA: 0x0012F448 File Offset: 0x0012D848
	private void VersionCheckFail(HttpC _c, string _version)
	{
		HttpC httpC = Preload.CheckClientVersion(_version, new Action<HttpC>(this.VersionCheckOk), delegate(HttpC cb)
		{
			this.VersionCheckFail(cb, _version);
		}, null);
	}

	// Token: 0x06001B18 RID: 6936 RVA: 0x0012F48E File Offset: 0x0012D88E
	public void ChangeScene()
	{
		PsMetrics.PreloadFinished(this.m_downloadFinished);
		Debug.Log(Main.m_currentGame.m_currentScene.GetType(), null);
		(Main.m_currentGame.m_currentScene as PreloadingScene).NextScene();
	}

	// Token: 0x06001B19 RID: 6937 RVA: 0x0012F4C4 File Offset: 0x0012D8C4
	public override void Step()
	{
		if (!this.m_fadeDone)
		{
			this.m_fadeOutTicks--;
			if (this.m_fadeOutTicks <= 0)
			{
				this.m_fadeOutTicks = 0;
			}
			this.m_fadeAlpha = (float)this.m_fadeOutTicks / 30f;
			this.d_Draw(this);
			if (this.m_fadeOutTicks <= 0)
			{
				this.m_fadeDone = true;
			}
		}
		else
		{
			if (this.m_download != null && !this.m_downloadFinished)
			{
				if (this.m_download.www == null && this.m_downloadFinished)
				{
					this.m_progress = 1f;
				}
				else if (this.m_download.www != null)
				{
					this.m_progress = this.m_download.www.progress;
				}
				if (this.m_progress < 0.004f || this.m_progress < this.m_fakeProgress)
				{
					if (this.m_fakeProgress < 0.04f)
					{
						this.m_fakeProgress += 1E-05f;
					}
					this.m_progress = this.m_fakeProgress;
				}
				this.m_loadingBar.d_Draw(this.m_loadingBar);
			}
			if (PsBackgroundDownloader.m_checksDone && this.m_clientIsUpToDate)
			{
				this.ChangeScene();
			}
		}
		base.Step();
	}

	// Token: 0x06001B1A RID: 6938 RVA: 0x0012F624 File Offset: 0x0012DA24
	public void BackgroundDrawhandler(UIComponent _c)
	{
		for (int i = 0; i < this.m_prefabs.Count; i++)
		{
			PrefabS.RemoveComponent(this.m_prefabs[i], true);
		}
		this.m_prefabs.Clear();
		Vector2[] rect = DebugDraw.GetRect(_c.m_actualWidth, _c.m_actualHeight, Vector2.zero);
		Color black = Color.black;
		black.a = this.m_fadeAlpha;
		uint num = DebugDraw.ColorToUInt(black);
		this.m_prefabs.Add(PrefabS.CreateFlatPrefabComponentsFromVectorArray(_c.m_TC, Vector3.forward * -10f, rect, num, num, this.m_fadeMaterial, _c.m_camera, string.Empty, null));
		for (int j = 0; j < this.m_prefabs.Count; j++)
		{
			this.m_prefabs[j].p_gameObject.GetComponent<Renderer>().material.color = black;
		}
	}

	// Token: 0x06001B1B RID: 6939 RVA: 0x0012F718 File Offset: 0x0012DB18
	public void LoadingBarDrawhandler(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] roundedRect = DebugDraw.GetRoundedRect(_c.m_actualWidth, _c.m_actualHeight, _c.m_actualHeight / 3f, 8, Vector2.zero);
		Color color = DebugDraw.HexToColor("#06698f");
		uint num = DebugDraw.ColorToUInt(color);
		PrefabS.CreateFlatPrefabComponentsFromVectorArray(_c.m_TC, Vector3.forward * -1f, roundedRect, num, num, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera, string.Empty, null);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * -0.5f, roundedRect, 4f, color, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line4Mat_Material), _c.m_camera, Position.Center, true);
		float num2 = _c.m_actualHeight / 2f;
		num = DebugDraw.HexToUint("#2fe217");
		Vector2[] rect = DebugDraw.GetRect((_c.m_actualWidth - num2) * this.m_progress, _c.m_actualHeight - num2 / 2f, -Vector2.right * (1f - this.m_progress) * 0.5f * (_c.m_actualWidth - num2));
		PrefabS.CreateFlatPrefabComponentsFromVectorArray(_c.m_TC, Vector3.forward * -2f, rect, num, num, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera, string.Empty, null);
	}

	// Token: 0x06001B1C RID: 6940 RVA: 0x0012F878 File Offset: 0x0012DC78
	public void GradientDrawhandler(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] rect = DebugDraw.GetRect(_c.m_actualWidth, _c.m_actualHeight, Vector3.zero);
		PrefabS.CreateFlatPrefabComponentsFromVectorArray(_c.m_TC, Vector3.forward, rect, DebugDraw.ColorToUInt(new Color(0f, 0f, 0f, 0.6f)), DebugDraw.ColorToUInt(new Color(0f, 0f, 0f, 0f)), ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera, string.Empty, null);
	}

	// Token: 0x06001B1D RID: 6941 RVA: 0x0012F916 File Offset: 0x0012DD16
	public override void Destroy()
	{
		if (this.m_loaderEntity != null)
		{
			EntityManager.RemoveEntity(this.m_loaderEntity);
		}
		this.m_loaderEntity = null;
		Object.Destroy(this.m_fadeMaterial);
		this.m_fadeMaterial = null;
		base.Destroy();
	}

	// Token: 0x04001D88 RID: 7560
	private Entity m_loaderEntity;

	// Token: 0x04001D89 RID: 7561
	private UICanvas m_loadingBar;

	// Token: 0x04001D8A RID: 7562
	private List<PrefabC> m_prefabs = new List<PrefabC>();

	// Token: 0x04001D8B RID: 7563
	private int m_fadeOutTicks = 30;

	// Token: 0x04001D8C RID: 7564
	private bool m_fadeDone;

	// Token: 0x04001D8D RID: 7565
	private float m_fadeAlpha;

	// Token: 0x04001D8E RID: 7566
	private Material m_fadeMaterial;

	// Token: 0x04001D8F RID: 7567
	private UIVerticalList m_content;

	// Token: 0x04001D90 RID: 7568
	private HttpC m_download;

	// Token: 0x04001D91 RID: 7569
	private bool m_downloadFinished;

	// Token: 0x04001D92 RID: 7570
	private float m_progress;

	// Token: 0x04001D93 RID: 7571
	private bool m_clientIsUpToDate;

	// Token: 0x04001D94 RID: 7572
	private UIText m_text;

	// Token: 0x04001D95 RID: 7573
	private float m_fakeProgress;
}
