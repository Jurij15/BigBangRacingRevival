using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001F1 RID: 497
public class ChildGizmo : BasicGizmo
{
	// Token: 0x06000EC7 RID: 3783 RVA: 0x00089E98 File Offset: 0x00088298
	public ChildGizmo(List<GraphElement> _graphElements)
		: base(_graphElements)
	{
		this.m_minDistanceFromParent = (this.m_graphElements[0] as GraphNode).m_minDistanceFromParent;
		this.m_maxDistanceFromParent = (this.m_graphElements[0] as GraphNode).m_maxDistanceFromParent;
		float num = (float)Screen.height / 768f * 50f;
		float num2 = num * 2f;
		if (this.m_graphElements[0].m_isMoveable)
		{
			TransformC transformC = TransformS.AddComponent(this.m_uiTC.p_entity, this.m_uiTC.transform.position + Vector3.forward * -10f);
			TransformS.ParentComponent(transformC, this.m_uiTC);
			this.m_TAC = TouchAreaS.AddCircleArea(transformC, "Move", num, CameraS.m_uiCamera, null);
			TouchAreaS.AddTouchEventListener(this.m_TAC, new TouchEventDelegate(this.TouchHandler));
			SpriteC spriteC = SpriteS.AddComponent(this.m_uiTC, PsState.m_uiSheet.m_atlas.GetFrame("hud_gizmo_selection_circle", null), PsState.m_uiSheet);
			SpriteS.SetDimensions(spriteC, num2, num2);
			SpriteS.SetOffset(spriteC, new Vector3(0f, 0f, 50f), 0f);
			SpriteS.ConvertSpritesToPrefabComponent(this.m_uiTC, true);
		}
	}

	// Token: 0x06000EC8 RID: 3784 RVA: 0x00089FE0 File Offset: 0x000883E0
	public override void TouchHandler(TouchAreaC _touchArea, TouchAreaPhase _touchPhase, bool _touchIsSecondary, int _touchCount, TLTouch[] _touches)
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
				base.DragCameraAtBorders();
				Vector3 vector3 = vector2 - this.m_uiTC.transform.position;
				Vector3 vector4 = vector3 * 0.382f;
				TransformS.GlobalMove(this.m_uiTC, vector4);
				Vector3 vector5 = TouchAreaS.GetTouchWorldPos(CameraS.m_mainCamera, this.m_uiTC.transform.position + new Vector3((float)Screen.width * 0.5f, (float)Screen.height * 0.5f, 0f), -this.m_tacTCDepth);
				if (this.m_minDistanceFromParent != 0f || this.m_maxDistanceFromParent != 0f)
				{
					Vector2 vector6 = vector5 - this.m_graphElements[0].m_parentElement.m_TC.transform.position;
					if (vector6.sqrMagnitude < this.m_minDistanceFromParent * this.m_minDistanceFromParent)
					{
						vector6 = vector6.normalized * this.m_minDistanceFromParent;
						vector5 = this.m_graphElements[0].m_parentElement.m_TC.transform.position + vector6;
					}
					else if (vector6.sqrMagnitude > this.m_maxDistanceFromParent * this.m_maxDistanceFromParent)
					{
						vector6 = vector6.normalized * this.m_maxDistanceFromParent;
						vector5 = this.m_graphElements[0].m_parentElement.m_TC.transform.position + vector6;
					}
				}
				vector5 = BasicGizmo.LimitInsideDome(vector5);
				vector5.z = this.m_worldTC.transform.position.z;
				TransformS.SetGlobalPosition(this.m_worldTC, vector5);
				for (int j = 0; j < this.m_graphElements.Count; j++)
				{
					GraphElement graphElement = this.m_graphElements[j];
					Vector3 vector7 = vector5 + this.m_selectionOffsets[j];
					TransformS.SetGlobalPosition(graphElement.m_TC, vector7);
					graphElement.m_position = vector7;
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
					this.m_readyToMove = false;
					LevelManager.m_currentLevel.ItemChanged();
					new MoveUndoAction(this.m_graphElements, this.m_startPositions);
				}
			}
		}
	}

	// Token: 0x040011B7 RID: 4535
	private float m_minDistanceFromParent;

	// Token: 0x040011B8 RID: 4536
	private float m_maxDistanceFromParent;
}
