using System;
using UnityEngine;

// Token: 0x02000132 RID: 306
public static class PsLoadingScreen
{
	// Token: 0x06000959 RID: 2393 RVA: 0x00064933 File Offset: 0x00062D33
	public static void Create()
	{
		PsLoadingScreen.m_entity = EntityManager.AddEntity("LoadingScreen");
		PsLoadingScreen.m_entity.m_persistent = true;
		PsLoadingScreen.m_tc = TransformS.AddComponent(PsLoadingScreen.m_entity, Vector3.zero);
		PsLoadingScreen.m_active = false;
	}

	// Token: 0x0600095A RID: 2394 RVA: 0x0006496C File Offset: 0x00062D6C
	public static void FadeIn(TweenEventDelegate _fadeInCallback = null)
	{
		TouchAreaS.Disable();
		PsLoadingScreen.m_fadeCallback = _fadeInCallback;
		if (PsLoadingScreen.m_entity == null)
		{
			PsLoadingScreen.Create();
		}
		PsLoadingScreen.m_camera = CameraS.AddCamera("Loading Screen Camera", true, 3);
		PsLoadingScreen.m_bg = new UICanvas(null, false, string.Empty, null, string.Empty);
		PsLoadingScreen.m_bg.SetCamera(PsLoadingScreen.m_camera, true, false);
		PsLoadingScreen.m_bg.SetWidth(1f, RelativeTo.ScreenWidth);
		PsLoadingScreen.m_bg.SetHeight(1f, RelativeTo.ScreenHeight);
		PsLoadingScreen.m_bg.SetDepthOffset(-240f);
		PsLoadingScreen.m_bg.SetDrawHandler(new UIDrawDelegate(PsLoadingScreen.BackgroundDrawhandler));
		PsLoadingScreen.m_bg.Update();
		TweenC tweenC = TweenS.AddTransformTween(PsLoadingScreen.m_bg.m_TC, TweenedProperty.Alpha, TweenStyle.Linear, Vector3.zero, Vector3.one, 0.5f, 0f, true);
		TweenS.SetTweenAlphaProperties(tweenC, false, true, false, null);
		TweenS.AddTweenEndEventListener(tweenC, new TweenEventDelegate(PsLoadingScreen.FadeInCallback));
		PsLoadingScreen.m_active = true;
	}

	// Token: 0x0600095B RID: 2395 RVA: 0x00064A88 File Offset: 0x00062E88
	private static void BackgroundDrawhandler(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] rect = DebugDraw.GetRect(_c.m_actualWidth, _c.m_actualHeight, Vector2.zero);
		PrefabC prefabC = PrefabS.CreateFlatPrefabComponentsFromVectorArray(_c.m_TC, Vector3.zero, rect, DebugDraw.HexToUint("#000000"), DebugDraw.HexToUint("#000000"), ResourceManager.GetMaterial(RESOURCE.MenuPopupBackgroundMat_Material), _c.m_camera, string.Empty, null);
		prefabC.p_gameObject.GetComponent<Renderer>().material.shader = Shader.Find("WOE/Unlit/ColorUnlitTransparentBg");
		prefabC.p_gameObject.GetComponent<Renderer>().material.color = new Color(0f, 0f, 0f, 1f);
	}

	// Token: 0x0600095C RID: 2396 RVA: 0x00064B46 File Offset: 0x00062F46
	private static void FadeInCallback(TweenC _c)
	{
		new PsUILoadingText(PsLoadingScreen.m_bg);
		PsLoadingScreen.m_bg.Update();
		if (PsLoadingScreen.m_fadeCallback != null)
		{
			Debug.Log("FADE IN CALLBACK", null);
			PsLoadingScreen.m_fadeCallback(_c);
		}
	}

	// Token: 0x0600095D RID: 2397 RVA: 0x00064B80 File Offset: 0x00062F80
	public static void FadeOut(TweenEventDelegate _fadeOutCallback = null)
	{
		if (PsLoadingScreen.m_bg != null)
		{
			PsLoadingScreen.m_bg.DestroyChildren();
		}
		PsLoadingScreen.m_fadeCallback = _fadeOutCallback;
		TweenC tweenC = TweenS.AddTransformTween(PsLoadingScreen.m_bg.m_TC, TweenedProperty.Alpha, TweenStyle.Linear, Vector3.one, Vector3.zero, 0.5f, 0f, true);
		TweenS.SetTweenAlphaProperties(tweenC, false, true, false, null);
		TweenS.AddTweenEndEventListener(tweenC, new TweenEventDelegate(PsLoadingScreen.FadeOutCallback));
	}

	// Token: 0x0600095E RID: 2398 RVA: 0x00064BFB File Offset: 0x00062FFB
	private static void FadeOutCallback(TweenC _c)
	{
		PsLoadingScreen.m_active = false;
		TouchAreaS.Enable();
		if (PsLoadingScreen.m_fadeCallback != null)
		{
			PsLoadingScreen.m_fadeCallback(_c);
		}
		PsLoadingScreen.Destroy();
	}

	// Token: 0x0600095F RID: 2399 RVA: 0x00064C24 File Offset: 0x00063024
	public static void Destroy()
	{
		if (PsLoadingScreen.m_bg != null)
		{
			PsLoadingScreen.m_bg.Destroy();
			PsLoadingScreen.m_bg = null;
		}
		CameraS.RemoveCamera(PsLoadingScreen.m_camera);
		if (PsLoadingScreen.m_entity != null)
		{
			EntityManager.RemoveEntity(PsLoadingScreen.m_entity);
			PsLoadingScreen.m_entity = null;
		}
		PsLoadingScreen.m_tc = null;
		PsLoadingScreen.m_camera = null;
		PsLoadingScreen.m_sheet = null;
		PsLoadingScreen.m_fadeCallback = null;
	}

	// Token: 0x040008B3 RID: 2227
	public static UICanvas m_bg;

	// Token: 0x040008B4 RID: 2228
	public static Entity m_entity;

	// Token: 0x040008B5 RID: 2229
	public static TransformC m_tc;

	// Token: 0x040008B6 RID: 2230
	public static Camera m_camera;

	// Token: 0x040008B7 RID: 2231
	public static SpriteSheet m_sheet;

	// Token: 0x040008B8 RID: 2232
	private static TweenEventDelegate m_fadeCallback;

	// Token: 0x040008B9 RID: 2233
	public static bool m_active;
}
