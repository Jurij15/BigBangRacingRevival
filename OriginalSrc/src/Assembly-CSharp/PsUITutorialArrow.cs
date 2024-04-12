using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000261 RID: 609
public class PsUITutorialArrow : PsUITutorialArrowBase
{
	// Token: 0x0600124C RID: 4684 RVA: 0x000B4AE8 File Offset: 0x000B2EE8
	public PsUITutorialArrow(string _entityTag, string _tcName, int _id = 0)
	{
		TouchAreaS.Enable();
		if (PsMetagameManager.m_tutorialArrow != null)
		{
			PsMetagameManager.m_tutorialArrow.Destroy();
		}
		PsMetagameManager.m_tutorialArrow = this;
		this.m_id = _id;
		this.m_entityName = _entityTag;
		this.m_target = _tcName;
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
		if (this.hasTarget)
		{
			base.DrawCanvas();
			this.SetCamera();
		}
	}

	// Token: 0x0600124D RID: 4685 RVA: 0x000B4BF0 File Offset: 0x000B2FF0
	public new virtual void GetTarget()
	{
		Entity entity = null;
		List<Entity> entitiesByTag = EntityManager.GetEntitiesByTag(this.m_entityName);
		for (int i = 0; i < entitiesByTag.Count; i++)
		{
			TouchAreaC touchAreaC = null;
			for (int j = 0; j < entitiesByTag[i].m_components.Count; j++)
			{
				if (entitiesByTag[i].m_components[j].m_componentType == ComponentType.TouchArea)
				{
					touchAreaC = entitiesByTag[i].m_components[j] as TouchAreaC;
				}
				if (entitiesByTag[i].m_components[j].m_componentType == ComponentType.Transform && (entitiesByTag[i].m_components[j] as TransformC).transform.name == this.m_target)
				{
					this.trans = entitiesByTag[i].m_components[j] as TransformC;
					entity = entitiesByTag[i];
					this.m_transfound = true;
				}
			}
			if (this.m_transfound)
			{
				if (touchAreaC == null)
				{
					Debug.LogError("Tutorial target " + this.m_entityName + " toucharea not found!");
				}
				this.m_touchArea = touchAreaC;
				break;
			}
		}
		if (!this.m_transfound)
		{
			Debug.LogWarning("Tutorial not found: " + this.m_target);
			this.m_destroy = true;
		}
		else
		{
			Debug.LogWarning("Tutorial gameobject found: " + this.m_target);
			this.m_entityTarget = entity;
			base.GetTargetSprites(this.m_entityTarget);
			this.m_goTarget = this.trans.transform.gameObject;
			this.hasTarget = true;
		}
	}

	// Token: 0x0600124E RID: 4686 RVA: 0x000B4DA8 File Offset: 0x000B31A8
	public override void Step()
	{
		if (this.m_destroy)
		{
			this.Destroy();
			return;
		}
		if (!this.hasTarget)
		{
			this.GetTarget();
			if (this.hasTarget)
			{
				this.SetCamera();
				base.DrawCanvas();
			}
			else
			{
				this.m_destroy = true;
			}
		}
		if (this.hasTarget)
		{
			if (this.m_goTarget == null || this.m_goTarget.name != this.m_target)
			{
				this.hasTarget = false;
				TouchAreaS.RemoveTouchCameraFilter();
				this.m_canvas.Destroy();
			}
			else if (this.m_3Dcamera != null)
			{
				Vector3 vector = this.m_3Dcamera.WorldToViewportPoint(this.m_goTarget.transform.position);
				this.offset.SetAlign(vector.x, vector.y);
				this.offset.UpdateAlign();
				if (vector.x >= 0f && vector.x <= 1f && vector.y >= 0f && vector.y <= 1f)
				{
					TouchAreaS.SetTouchCameraFilter(this.m_3Dcamera);
				}
				else
				{
					TouchAreaS.RemoveTouchCameraFilter();
				}
			}
		}
	}

	// Token: 0x0600124F RID: 4687 RVA: 0x000B4EF8 File Offset: 0x000B32F8
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
		if (this.m_transfound)
		{
			PrefabS.SetCamera(this.m_goTarget, this.m_originalCamera);
			if (this.m_spriteList != null && this.m_spriteList.Count > 0)
			{
				this.m_spriteList[0].p_spriteSheet.SetCamera(this.m_originalCamera);
			}
			CameraS.m_cameras.Remove(this.m_3Dcamera);
			Object.DestroyImmediate(this.m_3Dcamera.gameObject);
			TouchAreaS.RemoveTouchCameraFilter();
			TouchAreaS.RemoveTouchEventListener(this.m_touchArea, new TouchEventDelegate(this.TouchHandler));
			TouchAreaS.SetCamera(this.m_touchArea, this.m_originalCamera);
		}
	}

	// Token: 0x04001577 RID: 5495
	public string m_target;

	// Token: 0x04001578 RID: 5496
	public string m_entityName;

	// Token: 0x04001579 RID: 5497
	public int m_id;

	// Token: 0x0400157A RID: 5498
	protected Entity m_entityTarget;

	// Token: 0x0400157B RID: 5499
	protected TransformC trans;

	// Token: 0x0400157C RID: 5500
	protected bool m_transfound;

	// Token: 0x0400157D RID: 5501
	protected bool hasTarget;
}
