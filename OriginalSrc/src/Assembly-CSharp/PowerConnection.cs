using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

// Token: 0x020000C5 RID: 197
[Serializable]
public class PowerConnection : GraphConnection, ISerializable, IDeserializationCallback
{
	// Token: 0x060003D8 RID: 984 RVA: 0x000376B2 File Offset: 0x00035AB2
	public PowerConnection(GraphElement _start, GraphElement _end)
		: base("PowerConnection")
	{
		this.m_disabledAtStart = false;
		_start.AddOutput(this);
		_end.AddInput(this);
	}

	// Token: 0x060003D9 RID: 985 RVA: 0x000376DF File Offset: 0x00035ADF
	public PowerConnection(SerializationInfo info, StreamingContext ctxt)
		: base(info, ctxt)
	{
	}

	// Token: 0x060003DA RID: 986 RVA: 0x000376F4 File Offset: 0x00035AF4
	public override void SetPropertyDefaults()
	{
		base.SetPropertyDefaults();
		this.m_isRemovable = true;
		this.m_isMoveable = false;
		this.m_isCopyable = false;
	}

	// Token: 0x060003DB RID: 987 RVA: 0x00037711 File Offset: 0x00035B11
	public override void Assemble()
	{
		base.Assemble();
		this.Draw(false);
	}

	// Token: 0x060003DC RID: 988 RVA: 0x00037720 File Offset: 0x00035B20
	public override void Trigger()
	{
		this.m_powerOn = !this.m_powerOn;
		if (this.m_visualscript != null)
		{
			this.m_visualscript.ChangeColor((!this.m_powerOn) ? this.m_disabledColor : this.m_activeColor);
		}
		base.Trigger();
	}

	// Token: 0x060003DD RID: 989 RVA: 0x0003777C File Offset: 0x00035B7C
	public void UpdateColor()
	{
		this.m_activeColor = (this.m_start as LevelPowerLink).GetPowerColor();
		this.m_disabledColor = (this.m_start as LevelPowerLink).GetDisabledColor();
		Color activeColor = (this.m_start as LevelPowerLink).GetActiveColor();
		if (this.m_visualscript != null)
		{
			this.m_visualscript.ChangeColor(activeColor);
		}
	}

	// Token: 0x060003DE RID: 990 RVA: 0x000377E4 File Offset: 0x00035BE4
	public void CreateTouchArea()
	{
		float num = (float)Screen.height / 768f * 50f;
		Vector3 centerPosition = this.m_visualscript.GetCenterPosition();
		this.m_touchTC = TransformS.AddComponent(this.m_TC.p_entity);
		TransformS.ParentComponent(this.m_touchTC, this.m_TC);
		TransformS.SetPosition(this.m_touchTC, centerPosition - this.m_TC.transform.position);
		SpriteC spriteC = SpriteS.AddComponent(this.m_touchTC, PsState.m_uiSheet.m_atlas.GetFrame("hud_gizmo_selection_circle", null), PsState.m_uiSheet);
		SpriteS.SetDimensions(spriteC, num, num);
		SpriteS.ConvertSpritesToPrefabComponent(this.m_touchTC, CameraS.m_mainCamera, true, Shader.Find("Framework/FontShader"));
		this.m_TAC = TouchAreaS.AddCircleArea(this.m_touchTC, "Select", num, CameraS.m_mainCamera, this.m_touchTC);
		TouchAreaS.AddTouchEventListener(this.m_TAC, new TouchEventDelegate(this.SelectionTouchHandler));
	}

	// Token: 0x060003DF RID: 991 RVA: 0x000378DB File Offset: 0x00035CDB
	private void SetTouchAreaPosition()
	{
		if (this.m_touchTC != null)
		{
			TransformS.SetPosition(this.m_touchTC, this.m_visualscript.GetCenterPosition() - this.m_TC.transform.position);
		}
	}

	// Token: 0x060003E0 RID: 992 RVA: 0x00037914 File Offset: 0x00035D14
	public void UpdateVisualPresentationPosition()
	{
		if (this.m_startTransform != null && this.m_endTransform != null)
		{
			Vector3 startPosition = this.GetStartPosition();
			Vector3 vector = this.GetEndPosition() - startPosition;
			Vector3 vector2 = startPosition + vector * 0.5f;
			Vector3 vector3 = vector * 0.5f - vector.normalized;
			Vector3 vector4 = vector3 - vector.normalized;
			TransformS.SetGlobalPosition(this.m_TC, vector2);
			this.m_startTransform.localPosition = -vector3 + Vector3.forward * 70f;
			this.m_endTransform.localPosition = vector3 + Vector3.forward * 70f;
			this.SetTouchAreaPosition();
		}
		else
		{
			Debug.LogError("something was null");
		}
	}

	// Token: 0x060003E1 RID: 993 RVA: 0x000379F8 File Offset: 0x00035DF8
	private void Draw(bool _highlight)
	{
		this.m_isHighlighted = _highlight;
		if (this.m_end == null || this.m_start == null)
		{
			return;
		}
		Vector3 startPosition = this.GetStartPosition();
		Vector3 vector = this.GetEndPosition() - startPosition;
		Vector3 vector2 = startPosition + vector * 0.5f;
		Vector3 vector3 = vector * 0.5f - vector.normalized;
		Vector3 vector4 = vector3 - vector.normalized;
		TransformS.SetGlobalPosition(this.m_TC, vector2);
		if (this.m_prefab == null)
		{
			this.CreateVisualPresentation(-vector3, vector3);
		}
	}

	// Token: 0x060003E2 RID: 994 RVA: 0x00037A94 File Offset: 0x00035E94
	private Vector3 GetEndPosition()
	{
		return (this.m_end as LevelPowerConnectable).GetInputPosition();
	}

	// Token: 0x060003E3 RID: 995 RVA: 0x00037AA6 File Offset: 0x00035EA6
	private Vector3 GetStartPosition()
	{
		return (this.m_start as LevelPowerConnectable).GetOutputPosition();
	}

	// Token: 0x060003E4 RID: 996 RVA: 0x00037AB8 File Offset: 0x00035EB8
	public virtual void CreateVisualPresentation(Vector3 _startPos, Vector3 _endPos)
	{
		if (this.m_start == null || this.m_end == null)
		{
			return;
		}
		GameObject gameObject = ResourceManager.GetGameObject(RESOURCE.PowerLinePrefab_GameObject);
		this.m_prefab = PrefabS.AddComponent(this.m_TC, Vector3.zero, gameObject);
		this.m_visualscript = this.m_prefab.p_gameObject.GetComponent<VisualsPowerLine>();
		this.UpdateColor();
		this.m_startTransform = this.m_prefab.p_gameObject.transform.Find("Start");
		this.m_startTransform.localPosition = _startPos + Vector3.forward * 70f;
		this.m_endTransform = this.m_prefab.p_gameObject.transform.Find("End");
		this.m_endTransform.localPosition = _endPos + Vector3.forward * 70f;
		this.m_visualscript.UpdateLine();
		if (PsState.m_activeMinigame != null && PsState.m_activeMinigame.m_editing)
		{
			this.CreateTouchArea();
		}
	}

	// Token: 0x060003E5 RID: 997 RVA: 0x00037BC4 File Offset: 0x00035FC4
	public override void Update()
	{
		if (this.m_disabled)
		{
			return;
		}
		base.Update();
		bool flag = false;
		if (this.m_start != null && this.m_start.m_selected)
		{
			flag = true;
		}
		else if (this.m_end != null && this.m_end.m_selected)
		{
			flag = true;
		}
		if (this.m_selected || flag)
		{
			this.Draw(true);
		}
		else if (!flag && this.m_isHighlighted)
		{
			this.Draw(false);
		}
	}

	// Token: 0x060003E6 RID: 998 RVA: 0x00037C59 File Offset: 0x00036059
	public override void Select(bool _select)
	{
		base.Select(_select);
		if (!_select)
		{
			this.Draw(false);
		}
	}

	// Token: 0x060003E7 RID: 999 RVA: 0x00037C6F File Offset: 0x0003606F
	public override void Clear(bool _isReset)
	{
		this.m_powerOn = false;
		if (this.m_prefab != null)
		{
			this.m_prefab = null;
		}
		base.Clear(_isReset);
	}

	// Token: 0x060003E8 RID: 1000 RVA: 0x00037C94 File Offset: 0x00036094
	protected void SelectionTouchHandler(TouchAreaC _touchArea, TouchAreaPhase _touchPhase, bool _touchIsSecondary, int _touchCount, TLTouch[] _touches)
	{
		if (_touchCount == 1 && _touchPhase == TouchAreaPhase.ReleaseIn && !_touchIsSecondary)
		{
			List<GraphElement> selection = GizmoManager.GetSelection();
			if (this.m_selected)
			{
				GizmoManager.RemoveFromSelection(this);
			}
			else
			{
				GizmoManager.AddToSelection(this);
			}
			new SelectUndoAction(selection, GizmoManager.GetSelection());
			GizmoManager.Update();
		}
	}

	// Token: 0x060003E9 RID: 1001 RVA: 0x00037CEC File Offset: 0x000360EC
	public new PowerConnection DeepCopy()
	{
		PowerConnection powerConnection;
		using (MemoryStream memoryStream = new MemoryStream())
		{
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			binaryFormatter.Serialize(memoryStream, this);
			memoryStream.Position = 0L;
			powerConnection = (PowerConnection)binaryFormatter.Deserialize(memoryStream);
		}
		return powerConnection;
	}

	// Token: 0x060003EA RID: 1002 RVA: 0x00037D48 File Offset: 0x00036148
	public override void GetObjectData(SerializationInfo info, StreamingContext ctxt)
	{
		base.GetObjectData(info, ctxt);
	}

	// Token: 0x04000502 RID: 1282
	private GraphNode m_startNode;

	// Token: 0x04000503 RID: 1283
	private Transform m_startTransform;

	// Token: 0x04000504 RID: 1284
	private GraphNode m_endNode;

	// Token: 0x04000505 RID: 1285
	private Transform m_endTransform;

	// Token: 0x04000506 RID: 1286
	private PrefabC m_prefab;

	// Token: 0x04000507 RID: 1287
	private VisualsPowerLine m_visualscript;

	// Token: 0x04000508 RID: 1288
	private bool m_powerOn;

	// Token: 0x04000509 RID: 1289
	private Color m_activeColor;

	// Token: 0x0400050A RID: 1290
	private Color m_disabledColor = Color.grey;

	// Token: 0x0400050B RID: 1291
	private TransformC m_touchTC;

	// Token: 0x0400050C RID: 1292
	private bool m_isHighlighted;
}
