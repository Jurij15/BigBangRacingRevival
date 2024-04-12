using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000092 RID: 146
[Serializable]
public class BasicAssembledClass : IAssembledClass
{
	// Token: 0x0600031C RID: 796 RVA: 0x00008AB6 File Offset: 0x00006EB6
	public BasicAssembledClass(GraphElement _graphElement)
	{
		this.m_assembledEntities = new List<Entity>();
		this.m_graphElement = _graphElement;
		this.m_minigame = LevelManager.m_currentLevel as Minigame;
	}

	// Token: 0x17000007 RID: 7
	// (get) Token: 0x0600031D RID: 797 RVA: 0x00008AE0 File Offset: 0x00006EE0
	// (set) Token: 0x0600031E RID: 798 RVA: 0x00008AE8 File Offset: 0x00006EE8
	public GraphElement m_graphElement
	{
		get
		{
			return this._graphElement;
		}
		set
		{
			this._graphElement = value;
		}
	}

	// Token: 0x17000008 RID: 8
	// (get) Token: 0x0600031F RID: 799 RVA: 0x00008AF1 File Offset: 0x00006EF1
	// (set) Token: 0x06000320 RID: 800 RVA: 0x00008AF9 File Offset: 0x00006EF9
	public List<Entity> m_assembledEntities
	{
		get
		{
			return this._assembledEntities;
		}
		set
		{
			this._assembledEntities = value;
		}
	}

	// Token: 0x06000321 RID: 801 RVA: 0x00008B04 File Offset: 0x00006F04
	public virtual void CreateGraphElementTouchArea(Mesh _collisionMesh, TransformC _parentTC = null)
	{
		if (this.m_minigame.m_editing)
		{
			if (this.m_graphElement.m_TAC != null)
			{
				Debug.LogError("Trying to add multiple touch areas to GraphElement.");
				return;
			}
			this.m_graphElement.m_isRect = true;
			this.m_graphElement.m_width = _collisionMesh.bounds.extents.x;
			this.m_graphElement.m_width = _collisionMesh.bounds.extents.y;
			this.m_graphElement.m_TAC = TouchAreaS.AddMeshArea((_parentTC != null) ? _parentTC : this.m_graphElement.m_TC, "Select", _collisionMesh, CameraS.m_mainCamera, this.m_graphElement.m_TC);
			TouchAreaS.AddTouchEventListener(this.m_graphElement.m_TAC, new TouchEventDelegate(this.SelectionTouchHandler));
		}
	}

	// Token: 0x06000322 RID: 802 RVA: 0x00008BE4 File Offset: 0x00006FE4
	public virtual void CreateGraphElementTouchArea(float _graphElementRadius, TransformC _parentTC = null)
	{
		if (this.m_minigame.m_editing)
		{
			if (this.m_graphElement.m_TAC != null)
			{
				Debug.LogError("Trying to add multiple touch areas to GraphElement.");
				return;
			}
			this.m_graphElement.m_isRect = false;
			this.m_graphElement.m_radius = _graphElementRadius;
			this.m_graphElement.m_TAC = TouchAreaS.AddCircleArea((_parentTC != null) ? _parentTC : this.m_graphElement.m_TC, "Select", _graphElementRadius, CameraS.m_mainCamera, this.m_graphElement.m_TC);
			TouchAreaS.AddTouchEventListener(this.m_graphElement.m_TAC, new TouchEventDelegate(this.SelectionTouchHandler));
		}
	}

	// Token: 0x06000323 RID: 803 RVA: 0x00008C90 File Offset: 0x00007090
	public virtual void CreateGraphElementTouchArea(float _graphElementWidth, float _graphElementHeight, TransformC _parentTC = null, Vector2 _offset = default(Vector2))
	{
		if (this.m_minigame.m_editing)
		{
			if (this.m_graphElement.m_TAC != null)
			{
				Debug.LogError("Trying to add multiple touch areas to GraphElement.");
				return;
			}
			this.m_graphElement.m_isRect = true;
			this.m_graphElement.m_width = _graphElementWidth;
			this.m_graphElement.m_height = _graphElementHeight;
			this.m_graphElement.m_TAC = TouchAreaS.AddRectArea((_parentTC != null) ? _parentTC : this.m_graphElement.m_TC, "Select", _graphElementWidth, _graphElementHeight, CameraS.m_mainCamera, this.m_graphElement.m_TC, _offset);
			TouchAreaS.AddTouchEventListener(this.m_graphElement.m_TAC, new TouchEventDelegate(this.SelectionTouchHandler));
		}
	}

	// Token: 0x06000324 RID: 804 RVA: 0x00008D48 File Offset: 0x00007148
	public void RemoveAssembledEntity(Entity _e)
	{
		if (this.m_assembledEntities.Contains(_e))
		{
			EntityManager.RemoveEntity(_e);
			this.m_assembledEntities.Remove(_e);
		}
	}

	// Token: 0x06000325 RID: 805 RVA: 0x00008D70 File Offset: 0x00007170
	public virtual void Destroy()
	{
		while (this.m_assembledEntities.Count > 0)
		{
			int num = this.m_assembledEntities.Count - 1;
			if (this.m_assembledEntities[num].m_index > -1)
			{
				EntityManager.RemoveEntity(this.m_assembledEntities[num]);
			}
			this.m_assembledEntities.RemoveAt(num);
		}
		this.m_graphElement.m_assembledClasses.Remove(this);
	}

	// Token: 0x06000326 RID: 806 RVA: 0x00008DE8 File Offset: 0x000071E8
	public virtual void SyncPositionToGraphElementPosition()
	{
		for (int i = 0; i < this.m_assembledEntities.Count; i++)
		{
			List<IComponent> componentsByEntity = EntityManager.GetComponentsByEntity(ComponentType.ChipmunkBody, this.m_assembledEntities[i]);
			for (int j = 0; j < componentsByEntity.Count; j++)
			{
				ChipmunkBodyC chipmunkBodyC = componentsByEntity[j] as ChipmunkBodyC;
				ChipmunkProWrapper.ucpBodySetPos(chipmunkBodyC.body, chipmunkBodyC.TC.transform.position);
			}
		}
	}

	// Token: 0x06000327 RID: 807 RVA: 0x00008E68 File Offset: 0x00007268
	protected void SelectionTouchHandler(TouchAreaC _touchArea, TouchAreaPhase _touchPhase, bool _touchIsSecondary, int _touchCount, TLTouch[] _touches)
	{
		if (_touchCount == 1 && _touchPhase == TouchAreaPhase.ReleaseIn && !_touchIsSecondary)
		{
			List<GraphElement> selection = GizmoManager.GetSelection();
			if (this.m_graphElement.m_selected)
			{
				GizmoManager.RemoveFromSelection(this.m_graphElement);
			}
			else
			{
				GizmoManager.AddToSelection(this.m_graphElement);
			}
			new SelectUndoAction(selection, GizmoManager.GetSelection());
			GizmoManager.Update();
		}
	}

	// Token: 0x04000403 RID: 1027
	private GraphElement _graphElement;

	// Token: 0x04000404 RID: 1028
	private List<Entity> _assembledEntities;

	// Token: 0x04000405 RID: 1029
	public Minigame m_minigame;
}
