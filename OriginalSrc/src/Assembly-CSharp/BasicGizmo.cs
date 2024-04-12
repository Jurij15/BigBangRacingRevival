using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001F0 RID: 496
public class BasicGizmo
{
	// Token: 0x06000EBD RID: 3773 RVA: 0x00088FF0 File Offset: 0x000873F0
	public BasicGizmo(List<GraphElement> _graphElements)
	{
		this.m_graphElements = _graphElements;
		this.m_linkPrefabC = new List<PrefabC>();
		this.m_readyToMove = false;
		this.m_startAngle = 0f;
		this.m_startPositions = new List<Vector3>();
		this.m_startRotations = new List<Vector3>();
		this.m_startScales = new List<Vector3>();
		string[] array = new string[] { "TransformGizmo" };
		this.m_uiTC = EntityManager.AddEntityWithTC(array);
		this.m_uiTC.p_entity.m_persistent = true;
		this.m_uiTC.transform.gameObject.name = "GizmoTC";
		this.m_worldTC = TransformS.AddComponent(this.m_uiTC.p_entity, "GizmoWorldTC");
		int count = this.m_graphElements.Count;
		Vector3 vector = Vector3.zero;
		for (int i = 0; i < count; i++)
		{
			vector += this.m_graphElements[i].m_TC.transform.position;
		}
		vector /= (float)count;
		this.m_selectionOffsets = new Vector3[count];
		for (int j = 0; j < count; j++)
		{
			this.m_selectionOffsets[j] = vector - this.m_graphElements[j].m_TC.transform.position;
		}
		Vector3 vector2 = Vector3.zero;
		if (GizmoManager.GetSelection().Count == 1)
		{
			vector2 = this.m_graphElements[0].m_rotation;
		}
		Vector3 vector3 = CameraS.m_mainCamera.WorldToScreenPoint(vector);
		vector3.x -= (float)Screen.width * 0.5f;
		vector3.y -= (float)Screen.height * 0.5f;
		vector3.z = 0f;
		TransformS.SetGlobalPosition(this.m_worldTC, vector);
		TransformS.SetGlobalRotation(this.m_worldTC, vector2);
		TransformS.SetGlobalPosition(this.m_uiTC, vector3);
		this.m_tacTCDepth = 0f;
		int num = 0;
		for (int k = 0; k < this.m_graphElements.Count; k++)
		{
			this.ParentToGizmo(this.m_graphElements[k]);
			if (this.m_graphElements[k].m_TAC != null)
			{
				this.m_tacTCDepth += this.m_graphElements[k].m_TAC.m_TC.transform.position.z;
				num++;
			}
		}
		if (num > 0)
		{
			this.m_tacTCDepth /= (float)num;
		}
	}

	// Token: 0x06000EBE RID: 3774 RVA: 0x00089298 File Offset: 0x00087698
	public static Vector3 LimitInsideDome(Vector3 _coord)
	{
		if (Main.m_currentGame.m_currentScene.m_name != "EditorScene")
		{
			return _coord;
		}
		Vector3 vector = _coord;
		Vector2 vector2 = (vector - AutoGeometryManager.m_tileCacheOffset) / 16f;
		float num = ToolBox.PointInsideEllipse(vector2, AutoGeometryManager.m_domeCenter, AutoGeometryManager.m_domeDiameter * 0.5f, 0f);
		if (num >= 1f)
		{
			Vector2 vector3 = AutoGeometryManager.m_domeCenter * 16f + AutoGeometryManager.m_tileCacheOffset;
			float num2 = AutoGeometryManager.m_domeDiameter.x / AutoGeometryManager.m_domeDiameter.y;
			Vector2 vector4 = (vector - vector3) * 0.5f;
			vector -= vector4 * (num - 1f);
		}
		vector.y = Mathf.Min((float)LevelManager.m_currentLevel.m_currentLayer.m_layerHeight * 0.5f - 35f, Mathf.Max((float)LevelManager.m_currentLevel.m_currentLayer.m_layerHeight * -0.5f + 35f, vector.y));
		return vector;
	}

	// Token: 0x06000EBF RID: 3775 RVA: 0x000893C4 File Offset: 0x000877C4
	public void DragCameraAtBorders()
	{
		float num = (float)Screen.height * 0.2f;
		Vector3 position = PsState.m_editorCamTarget.targetTC.transform.position;
		bool flag = false;
		Vector2 vector = this.m_uiTC.transform.position;
		if (vector.x < (float)Screen.width * -0.5f + (num + PsState.m_editorCameraExtraBorder.l) || vector.x > (float)Screen.width * 0.5f - (num + PsState.m_editorCameraExtraBorder.r))
		{
			if (vector.x < 0f)
			{
				position.x -= (num + PsState.m_editorCameraExtraBorder.l - (vector.x + (float)Screen.width * 0.5f)) * 0.25f;
			}
			else
			{
				position.x += (num + PsState.m_editorCameraExtraBorder.r - ((float)Screen.width * 0.5f - vector.x)) * 0.25f;
			}
			position.x = Mathf.Min((float)LevelManager.m_currentLevel.m_currentLayer.m_layerWidth * 0.5f, Mathf.Max((float)LevelManager.m_currentLevel.m_currentLayer.m_layerWidth * -0.5f, position.x));
			flag = true;
		}
		if (vector.y < (float)Screen.height * -0.5f + (num + PsState.m_editorCameraExtraBorder.b) || vector.y > (float)Screen.height * 0.5f - (num + PsState.m_editorCameraExtraBorder.t))
		{
			if (vector.y < 0f)
			{
				position.y -= (num + PsState.m_editorCameraExtraBorder.b - (vector.y + (float)Screen.height * 0.5f)) * 0.25f;
			}
			else
			{
				position.y += (num + PsState.m_editorCameraExtraBorder.t - ((float)Screen.height * 0.5f - vector.y)) * 0.25f;
			}
			position.y = Mathf.Min((float)LevelManager.m_currentLevel.m_currentLayer.m_layerHeight * 0.5f, Mathf.Max((float)LevelManager.m_currentLevel.m_currentLayer.m_layerHeight * -0.5f, position.y));
			flag = true;
		}
		if (flag)
		{
			PsState.m_editorCamTarget.targetTC.transform.position = position;
		}
		PsState.m_editorCameraPos = position;
	}

	// Token: 0x06000EC0 RID: 3776 RVA: 0x0008964C File Offset: 0x00087A4C
	public virtual void TouchHandler(TouchAreaC _touchArea, TouchAreaPhase _touchPhase, bool _touchIsSecondary, int _touchCount, TLTouch[] _touches)
	{
		if (_touchIsSecondary)
		{
			return;
		}
		if (_touchCount == 1 && _touchArea.m_name == "Move")
		{
			if (_touchPhase == TouchAreaPhase.Began || _touchPhase == TouchAreaPhase.DragStart)
			{
				if (!this.m_readyToMove)
				{
					Vector3 vector = _touches[0].m_currentPosition;
					vector.x -= (float)Screen.width * 0.5f;
					vector.y -= (float)Screen.height * 0.5f;
					vector.z = -10f;
					this.m_touchOffset = this.m_uiTC.transform.position - vector;
					this.m_readyToMove = true;
					this.m_startPositions = new List<Vector3>();
					for (int i = 0; i < this.m_graphElements.Count; i++)
					{
						this.m_startPositions.Add(this.m_graphElements[i].m_position);
					}
				}
			}
			else if (this.m_readyToMove && (_touchPhase == TouchAreaPhase.MoveIn || _touchPhase == TouchAreaPhase.MoveOut || _touchPhase == TouchAreaPhase.StationaryIn || _touchPhase == TouchAreaPhase.StationaryOut))
			{
				Vector3 vector2 = _touches[0].m_currentPosition + this.m_touchOffset;
				vector2.x -= (float)Screen.width * 0.5f;
				vector2.y -= (float)Screen.height * 0.5f;
				vector2.z = 0f;
				this.DragCameraAtBorders();
				Vector3 vector3 = vector2 - this.m_uiTC.transform.position;
				Vector3 vector4 = vector3 * 0.382f;
				TransformS.GlobalMove(this.m_uiTC, vector4);
				Vector3 vector5 = TouchAreaS.GetTouchWorldPos(CameraS.m_mainCamera, this.m_uiTC.transform.position + new Vector3((float)Screen.width * 0.5f, (float)Screen.height * 0.5f, 0f), -this.m_tacTCDepth);
				vector5 = BasicGizmo.LimitInsideDome(vector5);
				TransformS.SetGlobalPosition(this.m_worldTC, vector5);
				for (int j = 0; j < this.m_graphElements.Count; j++)
				{
					GraphElement graphElement = this.m_graphElements[j];
					Vector3 vector6 = vector5 + this.m_selectionOffsets[j];
					TransformS.SetGlobalPosition(graphElement.m_TC, vector6);
					graphElement.m_position = vector6;
					for (int k = 0; k < graphElement.m_assembledClasses.Count; k++)
					{
						graphElement.m_assembledClasses[k].SyncPositionToGraphElementPosition();
					}
				}
			}
			else if (_touchPhase != TouchAreaPhase.DragEnd)
			{
				if (_touchPhase == TouchAreaPhase.ReleaseIn || _touchPhase == TouchAreaPhase.ReleaseOut)
				{
					LevelManager.m_currentLevel.ItemChanged();
					new MoveUndoAction(this.m_graphElements, this.m_startPositions);
					this.m_readyToMove = false;
				}
			}
		}
	}

	// Token: 0x06000EC1 RID: 3777 RVA: 0x00089945 File Offset: 0x00087D45
	public void Destroy()
	{
		this.UnparentFromGizmo();
		if (this.m_uiTC != null)
		{
			EntityManager.RemoveEntity(this.m_uiTC.p_entity, true, true);
		}
		this.m_uiTC = null;
	}

	// Token: 0x06000EC2 RID: 3778 RVA: 0x00089974 File Offset: 0x00087D74
	public void MoveGizmo(Vector3 _worldPos)
	{
		TransformS.SetGlobalPosition(this.m_worldTC, _worldPos);
		for (int i = 0; i < this.m_graphElements.Count; i++)
		{
			GraphElement graphElement = this.m_graphElements[i];
			Vector3 vector = _worldPos + this.m_selectionOffsets[i];
			TransformS.SetGlobalPosition(graphElement.m_TC, vector);
			graphElement.m_position = vector;
			for (int j = 0; j < graphElement.m_assembledClasses.Count; j++)
			{
				IAssembledClass assembledClass = graphElement.m_assembledClasses[j];
				assembledClass.SyncPositionToGraphElementPosition();
			}
		}
	}

	// Token: 0x06000EC3 RID: 3779 RVA: 0x00089A14 File Offset: 0x00087E14
	public void UpdatePosition()
	{
		if (this.m_graphElements.Count > 0)
		{
			int count = this.m_graphElements.Count;
			Vector3 vector = Vector3.zero;
			Vector3 vector2 = Vector3.zero;
			int num = 0;
			for (int i = 0; i < count; i++)
			{
				vector += this.m_graphElements[i].m_TC.transform.position;
				if (this.m_graphElements[i].m_TAC != null)
				{
					vector2 += this.m_graphElements[i].m_TAC.m_TC.transform.position;
					num++;
				}
			}
			vector /= (float)count;
			if (num > 0)
			{
				vector2 /= (float)num;
			}
			this.m_selectionOffsets = new Vector3[count];
			for (int j = 0; j < count; j++)
			{
				this.m_selectionOffsets[j] = this.m_graphElements[j].m_TC.transform.position - vector;
			}
			Vector3 vector3 = Vector3.zero;
			if (this.m_graphElements.Count == 1)
			{
				vector3 = this.m_graphElements[0].m_rotation;
			}
			Vector3 vector4 = CameraS.m_mainCamera.WorldToScreenPoint(vector2);
			vector4.x -= (float)Screen.width * 0.5f;
			vector4.y -= (float)Screen.height * 0.5f;
			vector4.z = 50f;
			TransformS.SetGlobalPosition(this.m_uiTC, vector4);
			this.SetGizmoRotation(vector3);
			TransformS.SetGlobalPosition(this.m_worldTC, vector);
			for (int k = 0; k < this.m_graphElements.Count; k++)
			{
				GraphElement graphElement = this.m_graphElements[k];
				Vector3 vector5 = vector + this.m_selectionOffsets[k];
				TransformS.SetGlobalPosition(graphElement.m_TC, vector5);
				graphElement.m_position = vector5;
				for (int l = 0; l < graphElement.m_assembledClasses.Count; l++)
				{
					IAssembledClass assembledClass = graphElement.m_assembledClasses[l];
					assembledClass.SyncPositionToGraphElementPosition();
				}
			}
		}
	}

	// Token: 0x06000EC4 RID: 3780 RVA: 0x00089C68 File Offset: 0x00088068
	protected virtual void SetGizmoRotation(Vector3 _uiRot)
	{
		TransformS.SetGlobalRotation(this.m_uiTC, _uiRot);
	}

	// Token: 0x06000EC5 RID: 3781 RVA: 0x00089C78 File Offset: 0x00088078
	public void ParentToGizmo(GraphElement _element)
	{
		for (int i = 0; i < _element.m_assembledClasses.Count; i++)
		{
			IAssembledClass assembledClass = _element.m_assembledClasses[i];
			for (int j = 0; j < assembledClass.m_assembledEntities.Count; j++)
			{
				List<IComponent> componentsByEntity = EntityManager.GetComponentsByEntity(ComponentType.ChipmunkBody, assembledClass.m_assembledEntities[j]);
				for (int k = 0; k < componentsByEntity.Count; k++)
				{
					ChipmunkBodyC chipmunkBodyC = componentsByEntity[k] as ChipmunkBodyC;
					chipmunkBodyC.m_active = false;
				}
				List<IComponent> componentsByEntity2 = EntityManager.GetComponentsByEntity(ComponentType.Transform, assembledClass.m_assembledEntities[j]);
				for (int l = 0; l < componentsByEntity2.Count; l++)
				{
					TransformC transformC = componentsByEntity2[l] as TransformC;
					if (transformC.parent == null)
					{
						TransformS.ParentComponent(transformC, this.m_worldTC);
					}
				}
			}
		}
	}

	// Token: 0x06000EC6 RID: 3782 RVA: 0x00089D6C File Offset: 0x0008816C
	public void UnparentFromGizmo()
	{
		for (int i = 0; i < this.m_graphElements.Count; i++)
		{
			GraphElement graphElement = this.m_graphElements[i];
			for (int j = 0; j < graphElement.m_assembledClasses.Count; j++)
			{
				IAssembledClass assembledClass = graphElement.m_assembledClasses[j];
				for (int k = 0; k < assembledClass.m_assembledEntities.Count; k++)
				{
					List<IComponent> componentsByEntity = EntityManager.GetComponentsByEntity(ComponentType.ChipmunkBody, assembledClass.m_assembledEntities[k]);
					for (int l = 0; l < componentsByEntity.Count; l++)
					{
						ChipmunkBodyC chipmunkBodyC = componentsByEntity[l] as ChipmunkBodyC;
						chipmunkBodyC.m_active = chipmunkBodyC.m_wasActive;
					}
					List<IComponent> componentsByEntity2 = EntityManager.GetComponentsByEntity(ComponentType.Transform, assembledClass.m_assembledEntities[k]);
					for (int m = 0; m < componentsByEntity2.Count; m++)
					{
						TransformC transformC = componentsByEntity2[m] as TransformC;
						if (transformC.parent == this.m_worldTC)
						{
							TransformS.UnparentComponent(transformC, true);
						}
					}
				}
			}
		}
	}

	// Token: 0x040011A8 RID: 4520
	public List<GraphElement> m_graphElements;

	// Token: 0x040011A9 RID: 4521
	public Vector3[] m_selectionOffsets;

	// Token: 0x040011AA RID: 4522
	public TransformC m_uiTC;

	// Token: 0x040011AB RID: 4523
	public TransformC m_worldTC;

	// Token: 0x040011AC RID: 4524
	public TouchAreaC m_TAC;

	// Token: 0x040011AD RID: 4525
	public bool m_readyToMove;

	// Token: 0x040011AE RID: 4526
	public bool m_readyToLink;

	// Token: 0x040011AF RID: 4527
	public List<PrefabC> m_linkPrefabC;

	// Token: 0x040011B0 RID: 4528
	public GraphElement m_linkTarget;

	// Token: 0x040011B1 RID: 4529
	public Vector3 m_touchOffset;

	// Token: 0x040011B2 RID: 4530
	public List<Vector3> m_startPositions;

	// Token: 0x040011B3 RID: 4531
	public List<Vector3> m_startRotations;

	// Token: 0x040011B4 RID: 4532
	public List<Vector3> m_startScales;

	// Token: 0x040011B5 RID: 4533
	public float m_startAngle;

	// Token: 0x040011B6 RID: 4534
	public float m_tacTCDepth;
}
