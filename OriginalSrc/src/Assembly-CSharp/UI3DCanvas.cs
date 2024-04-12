using System;
using UnityEngine;

// Token: 0x02000584 RID: 1412
public class UI3DCanvas : UICanvas
{
	// Token: 0x0600290F RID: 10511 RVA: 0x001B3D0C File Offset: 0x001B210C
	public UI3DCanvas(UIComponent _parent, string _tag)
		: base(_parent, true, _tag, null, string.Empty)
	{
		this.m_3DEntity = EntityManager.AddEntity(new string[] { "UIComponent", _tag });
		this.m_3DCameraPivot = TransformS.AddComponent(this.m_3DEntity, "UI 3D Camera pivot");
		TransformS.ParentComponent(this.m_3DCameraPivot, this.m_TC, Vector3.zero);
		this.m_3DCamera = CameraS.AddCamera("UI 3D Camera", false, 3);
		this.m_3DCamera.transform.SetParent(this.m_3DCameraPivot.transform);
		this.m_3DCamera.transform.localPosition = Vector3.zero;
		this.m_3DCameraPivot.transform.gameObject.layer = this.m_3DCamera.gameObject.layer;
		this.m_3DContent = TransformS.AddComponent(this.m_3DEntity, "Content");
		TransformS.ParentComponent(this.m_3DContent, this.m_TC, Vector3.zero);
		this.AdjustCamera();
	}

	// Token: 0x06002910 RID: 10512 RVA: 0x001B3E18 File Offset: 0x001B2218
	public virtual PrefabC AddGameObject(GameObject _go, Vector3 _position, Vector3 _rotaion = default(Vector3))
	{
		PrefabC prefabC = PrefabS.AddComponent(this.m_3DContent, _position, _go, _go.name, true, true);
		PrefabS.SetCamera(prefabC, this.m_3DCamera);
		prefabC.p_gameObject.transform.rotation = Quaternion.Euler(_rotaion);
		return prefabC;
	}

	// Token: 0x06002911 RID: 10513 RVA: 0x001B3E60 File Offset: 0x001B2260
	public virtual void AdjustCamera()
	{
		if (this.m_3DCamera != null)
		{
			UIComponent uicomponent = this.m_parent;
			Vector3 vector = Vector3.zero;
			Vector3 vector2 = Vector3.zero;
			float num = 1f * (float)Screen.width;
			float num2 = 0f * (float)Screen.width;
			while (uicomponent != null)
			{
				if (uicomponent is UIScrollableCanvas)
				{
					UIScrollableCanvas uiscrollableCanvas = uicomponent as UIScrollableCanvas;
					vector += uiscrollableCanvas.m_scrollTC.transform.localPosition;
					num = uiscrollableCanvas.m_camera.rect.xMax * (float)Screen.width;
					num2 = uiscrollableCanvas.m_camera.rect.xMin * (float)Screen.width;
					break;
				}
				vector2 += uicomponent.m_TC.transform.localPosition;
				uicomponent = uicomponent.m_parent;
			}
			Vector3 position = this.m_TC.transform.position;
			float num3 = position.x - vector.x - this.m_actualWidth * 0.5f + (float)Screen.width * 0.5f;
			float actualWidth = this.m_actualWidth;
			float num4 = 0f;
			float num5 = 0f;
			if (num3 < num2)
			{
				num4 = num2 - num3;
				num3 = num2;
			}
			if (num3 + actualWidth > num)
			{
				num5 = num3 + actualWidth - num;
			}
			float num6 = position.y + -vector.y - this.m_actualHeight * 0.5f;
			float actualHeight = this.m_actualHeight;
			float y = this.m_3DCamera.transform.localPosition.y;
			this.m_3DCamera.transform.localPosition = new Vector3((-num5 + num4) * 0.31f, y, this.m_3DCameraOffset);
			Rect rect;
			rect..ctor(num3 / (float)Screen.width, num6 / (float)Screen.height + 0.5f, (actualWidth - num4 - num5) / (float)Screen.width, actualHeight / (float)Screen.height);
			if (rect.width < 0f)
			{
				rect.width = 0f;
			}
			if (rect.height < 0f)
			{
				rect.height = 0f;
			}
			rect.x += this.m_cameraAddRect.x;
			rect.y += this.m_cameraAddRect.y;
			rect.width += this.m_cameraAddRect.width;
			rect.height += this.m_cameraAddRect.height;
			this.m_3DCamera.rect = rect;
			if (this.m_TAC != null)
			{
				this.m_TAC.m_clip = true;
				this.m_TAC.m_clipBB.l = rect.xMin * (float)Screen.width;
				this.m_TAC.m_clipBB.r = rect.xMax * (float)Screen.width;
				this.m_TAC.m_clipBB.b = rect.yMin * (float)Screen.height;
				this.m_TAC.m_clipBB.t = rect.yMax * (float)Screen.height;
			}
		}
	}

	// Token: 0x06002912 RID: 10514 RVA: 0x001B4198 File Offset: 0x001B2598
	public override void Update()
	{
		this.CalculateReferenceSizes();
		this.UpdateSize();
		this.UpdateAlign();
		this.UpdateMargins();
		if (this.d_Draw != null)
		{
			this.d_Draw(this);
		}
		this.UpdateChildren();
		this.ArrangeContents();
		if (this.m_parent == null)
		{
			this.UpdateSpecial();
		}
	}

	// Token: 0x06002913 RID: 10515 RVA: 0x001B41F1 File Offset: 0x001B25F1
	public override void UpdateSpecial()
	{
		this.UpdateUniqueCamera();
		base.UpdateSpecial();
	}

	// Token: 0x06002914 RID: 10516 RVA: 0x001B41FF File Offset: 0x001B25FF
	public override void UpdateSize()
	{
		base.UpdateSize();
	}

	// Token: 0x06002915 RID: 10517 RVA: 0x001B4207 File Offset: 0x001B2607
	public override void CalculateReferenceSizes()
	{
		base.CalculateReferenceSizes();
	}

	// Token: 0x06002916 RID: 10518 RVA: 0x001B420F File Offset: 0x001B260F
	public override void UpdateUniqueCamera()
	{
		this.m_3DCamera.transform.localPosition = Vector3.forward * this.m_3DCameraOffset;
		this.AdjustCamera();
	}

	// Token: 0x06002917 RID: 10519 RVA: 0x001B4237 File Offset: 0x001B2637
	public override void Step()
	{
		this.AdjustCamera();
		base.Step();
	}

	// Token: 0x06002918 RID: 10520 RVA: 0x001B4245 File Offset: 0x001B2645
	public override void Destroy()
	{
		base.Destroy();
		CameraS.RemoveCamera(this.m_3DCamera);
		this.m_3DCamera = null;
		EntityManager.RemoveEntity(this.m_3DEntity);
		this.m_3DEntity = null;
	}

	// Token: 0x04002E19 RID: 11801
	private Entity m_3DEntity;

	// Token: 0x04002E1A RID: 11802
	public TransformC m_3DContent;

	// Token: 0x04002E1B RID: 11803
	public Camera m_3DCamera;

	// Token: 0x04002E1C RID: 11804
	public float m_3DCameraOffset = -500f;

	// Token: 0x04002E1D RID: 11805
	public TransformC m_3DCameraPivot;

	// Token: 0x04002E1E RID: 11806
	public Rect m_cameraAddRect;
}
