using System;
using UnityEngine;

// Token: 0x02000260 RID: 608
public class PsUITutorialNodeArrow : PsUITutorialArrowBase
{
	// Token: 0x06001247 RID: 4679 RVA: 0x000B4818 File Offset: 0x000B2C18
	public PsUITutorialNodeArrow(PsPlanetNode _node)
	{
		this.m_node = _node;
		TouchAreaS.Enable();
		if (PsMetagameManager.m_tutorialArrow != null)
		{
			PsMetagameManager.m_tutorialArrow.Destroy();
		}
		PsMetagameManager.m_tutorialArrow = this;
		int num = 11;
		GameObject gameObject = new GameObject("3D Tutorial Camera");
		this.m_3Dcamera = gameObject.AddComponent<Camera>();
		CameraS.m_cameras.Add(this.m_3Dcamera);
		this.m_originalCamera = GameObject.Find("PlanetCamera").GetComponent<Camera>();
		this.m_3Dcamera.CopyFrom(this.m_originalCamera);
		this.m_3Dcamera.clearFlags = 3;
		this.m_3Dcamera.depth = 15f;
		this.m_3Dcamera.gameObject.layer = num;
		this.m_3Dcamera.cullingMask = 0;
		this.m_3Dcamera.cullingMask |= 1 << num;
		this.GetTarget();
		base.DrawCanvas();
		base.GetTargetSprites(this.m_node.m_tc.p_entity);
		this.SetCamera();
	}

	// Token: 0x06001248 RID: 4680 RVA: 0x000B491D File Offset: 0x000B2D1D
	protected override void GetTarget()
	{
		this.m_goTarget = this.m_node.m_prefab.p_gameObject;
		this.m_touchArea = this.m_node.m_tac;
	}

	// Token: 0x06001249 RID: 4681 RVA: 0x000B4946 File Offset: 0x000B2D46
	protected override void SetCamera()
	{
		base.SetCamera();
		if (this.m_node.m_highlightTC != null)
		{
			PrefabS.SetCamera(this.m_node.m_highlightTC.transform.gameObject, this.m_3Dcamera);
		}
	}

	// Token: 0x0600124A RID: 4682 RVA: 0x000B4980 File Offset: 0x000B2D80
	public override void Step()
	{
		if (this.m_destroy)
		{
			this.Destroy();
			return;
		}
		Vector3 vector = this.m_3Dcamera.WorldToViewportPoint(this.m_goTarget.transform.position);
		this.offset.SetAlign(vector.x, vector.y);
		this.offset.UpdateAlign();
	}

	// Token: 0x0600124B RID: 4683 RVA: 0x000B49E0 File Offset: 0x000B2DE0
	public override void Destroy()
	{
		if (PsMetagameManager.m_tutorialArrow == this)
		{
			PsMetagameManager.m_tutorialArrow = null;
		}
		if (this.m_canvas != null)
		{
			if (this.m_fadeOutSpriteSheet != null)
			{
				SpriteS.RemoveSpriteSheet(this.m_fadeOutSpriteSheet);
			}
			this.m_fadeOutSpriteSheet = null;
			this.m_canvas.Destroy();
		}
		PrefabS.SetCamera(this.m_goTarget, this.m_originalCamera);
		PrefabS.SetCamera(this.m_node.m_highlightTC.transform.gameObject, this.m_originalCamera);
		if (this.m_spriteList.Count > 0)
		{
			this.m_spriteList[0].p_spriteSheet.SetCamera(this.m_originalCamera);
		}
		CameraS.m_cameras.Remove(this.m_3Dcamera);
		Object.DestroyImmediate(this.m_3Dcamera.gameObject);
		TouchAreaS.RemoveTouchCameraFilter();
		TouchAreaS.RemoveTouchEventListener(this.m_node.m_tac, new TouchEventDelegate(this.TouchHandler));
		TouchAreaS.SetCamera(this.m_node.m_tac, this.m_originalCamera);
	}

	// Token: 0x04001576 RID: 5494
	private PsPlanetNode m_node;
}
