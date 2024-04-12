using System;
using UnityEngine;

// Token: 0x020002F1 RID: 753
public class PsUICenterWinRacing : PsUICenterStartRacing
{
	// Token: 0x06001638 RID: 5688 RVA: 0x000E8773 File Offset: 0x000E6B73
	public PsUICenterWinRacing(UIComponent _parent)
		: base(_parent)
	{
		if (this.m_showRentStatsButtonArea != null)
		{
			this.m_showRentStatsButtonArea.Destroy();
		}
	}

	// Token: 0x06001639 RID: 5689 RVA: 0x000E87A0 File Offset: 0x000E6BA0
	private void FarRightHandler(TweenC _c)
	{
		TweenC tweenC = TweenS.AddTransformTween(_c.p_TC, TweenedProperty.Rotation, TweenStyle.QuadOut, new Vector3(0f, 0f, 360f), 0.4f, 0f, true);
		TweenS.AddTransformTween(_c.p_TC, TweenedProperty.Scale, TweenStyle.QuadOut, new Vector3(1.1f, 1.1f, 1.1f), Vector3.one, 0.4f, 0f, true);
		TweenS.AddTweenEndEventListener(tweenC, new TweenEventDelegate(this.MiddleToLeft));
	}

	// Token: 0x0600163A RID: 5690 RVA: 0x000E8820 File Offset: 0x000E6C20
	private void MiddleToLeft(TweenC _c)
	{
		TweenC tweenC = TweenS.AddTransformTween(_c.p_TC, TweenedProperty.Rotation, TweenStyle.QuadIn, new Vector3(0f, 0f, 1.5f), 0.4f, 0f, true);
		TweenS.AddTransformTween(_c.p_TC, TweenedProperty.Scale, TweenStyle.QuadIn, Vector3.one, new Vector3(1.1f, 1.1f, 1.1f), 0.4f, 0f, true);
		TweenS.AddTweenEndEventListener(tweenC, new TweenEventDelegate(this.FarLeftHandler));
	}

	// Token: 0x0600163B RID: 5691 RVA: 0x000E88A0 File Offset: 0x000E6CA0
	private void FarLeftHandler(TweenC _c)
	{
		TweenC tweenC = TweenS.AddTransformTween(_c.p_TC, TweenedProperty.Rotation, TweenStyle.QuadOut, new Vector3(0f, 0f, 0f), 0.4f, 0f, true);
		TweenS.AddTransformTween(_c.p_TC, TweenedProperty.Scale, TweenStyle.QuadOut, new Vector3(1.1f, 1.1f, 1.1f), Vector3.one, 0.4f, 0f, true);
		TweenS.AddTweenEndEventListener(tweenC, new TweenEventDelegate(this.MiddleToRight));
	}

	// Token: 0x0600163C RID: 5692 RVA: 0x000E8920 File Offset: 0x000E6D20
	private void MiddleToRight(TweenC _c)
	{
		TweenC tweenC = TweenS.AddTransformTween(_c.p_TC, TweenedProperty.Rotation, TweenStyle.QuadIn, new Vector3(0f, 0f, -1.5f), 0.4f, 0f, true);
		TweenS.AddTransformTween(_c.p_TC, TweenedProperty.Scale, TweenStyle.QuadIn, Vector3.one, new Vector3(1.1f, 1.1f, 1.1f), 0.4f, 0f, true);
		TweenS.AddTweenEndEventListener(tweenC, new TweenEventDelegate(this.FarRightHandler));
	}

	// Token: 0x0600163D RID: 5693 RVA: 0x000E89A0 File Offset: 0x000E6DA0
	public new void BottomDrawhandler(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] rect = DebugDraw.GetRect(_c.m_actualWidth, _c.m_actualHeight, Vector2.zero, true);
		Color color = DebugDraw.HexToColor("##083f7c");
		Color color2 = DebugDraw.HexToColor("##004b9f");
		Color black = Color.black;
		black.a = 0.5f;
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * -1f, rect, (float)Screen.height * 0.0075f, color, color2, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line4Mat_Material), _c.m_camera, Position.Center, true);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * 1f, rect, (float)Screen.height * 0.015f, black, black, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line4Mat_Material), _c.m_camera, Position.Center, true);
		GGData ggdata = new GGData(rect);
		PrefabS.CreateFlatPrefabComponentsFromPolygon(_c.m_TC, Vector3.zero, ggdata, color, color2, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera);
	}

	// Token: 0x0600163E RID: 5694 RVA: 0x000E8AA4 File Offset: 0x000E6EA4
	public override void Step()
	{
		if (GameScene.m_lowPerformance && !PlayerPrefsX.GetLowEndPrompt() && this.m_lowEndPrompt == null && !this.m_lowEndShown)
		{
			this.m_lowEndPrompt = new PsUIBasePopup(typeof(PsUICenterLowPerformancePrompt), null, null, null, false, true, InitialPage.Center, true, false, false);
			this.m_lowEndPrompt.SetAction("Exit", delegate
			{
				PlayerPrefsX.SetLowEndPrompt(true);
				this.m_lowEndPrompt.Destroy();
				this.m_lowEndPrompt = null;
			});
			this.m_lowEndShown = true;
		}
		base.Step();
	}

	// Token: 0x04001907 RID: 6407
	private string m_opponentName = string.Empty;
}
