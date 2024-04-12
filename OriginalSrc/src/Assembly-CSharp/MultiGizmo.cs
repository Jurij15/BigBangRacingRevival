using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001F6 RID: 502
public class MultiGizmo : BasicGizmo
{
	// Token: 0x06000EDA RID: 3802 RVA: 0x0008C05C File Offset: 0x0008A45C
	public MultiGizmo(List<GraphElement> _graphElements)
		: base(_graphElements)
	{
		float num = (float)Screen.height / 768f * 100f;
		float num2 = num * 2f;
		float num3 = 1f * num;
		float num4 = 0.35f * num2;
		if (this.m_graphElements[0].m_isMoveable)
		{
			this.m_TAC = TouchAreaS.AddCircleArea(this.m_uiTC, "Move", num, CameraS.m_uiCamera, null);
			TouchAreaS.AddTouchEventListener(this.m_TAC, new TouchEventDelegate(this.TouchHandler));
			SpriteC spriteC = SpriteS.AddComponent(this.m_uiTC, PsState.m_uiSheet.m_atlas.GetFrame("hud_gizmo_selection_circle", null), PsState.m_uiSheet);
			SpriteS.SetDimensions(spriteC, num2, num2);
			SpriteS.SetOffset(spriteC, new Vector3(0f, 0f, 50f), 0f);
			SpriteS.ConvertSpritesToPrefabComponent(this.m_uiTC, true);
		}
		if (this.m_graphElements[0].m_isRemovable && !PsState.UsingEditorResources())
		{
			float num5 = -45f;
			TransformC transformC = TransformS.AddComponent(this.m_uiTC.p_entity, "GizmoRemoveTC");
			transformC.forceRotation = true;
			TransformS.ParentComponent(transformC, this.m_uiTC, new Vector3(num3 * Mathf.Sin(num5 * 0.017453292f), num3 * Mathf.Cos(num5 * 0.017453292f), -10f));
			TouchAreaC touchAreaC = TouchAreaS.AddCircleArea(transformC, "Remove", num4 * 0.5f, CameraS.m_uiCamera, null);
			TouchAreaS.AddTouchEventListener(touchAreaC, new TouchEventDelegate(this.TouchHandler));
			SpriteC spriteC2 = SpriteS.AddComponent(transformC, PsState.m_uiSheet.m_atlas.GetFrame("hud_gizmo_put_back", null), PsState.m_uiSheet);
			SpriteS.SetDimensions(spriteC2, num4, num4);
			SpriteS.ConvertSpritesToPrefabComponent(transformC, true);
		}
	}

	// Token: 0x06000EDB RID: 3803 RVA: 0x0008C224 File Offset: 0x0008A624
	public override void TouchHandler(TouchAreaC _touchArea, TouchAreaPhase _touchPhase, bool _touchIsSecondary, int _touchCount, TLTouch[] _touches)
	{
		if (_touchIsSecondary)
		{
			return;
		}
		if (_touchCount == 1)
		{
			if (_touchArea.m_name == "Move")
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
					vector2.z = -10f;
					base.DragCameraAtBorders();
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
							IAssembledClass assembledClass = graphElement.m_assembledClasses[k];
							assembledClass.SyncPositionToGraphElementPosition();
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
			else if (_touchArea.m_name == "Remove" && _touchPhase == TouchAreaPhase.ReleaseIn)
			{
				List<GraphElement> list = new List<GraphElement>();
				List<GraphElement> selection = GizmoManager.GetSelection();
				while (selection.Count > 0)
				{
					int num = selection.Count - 1;
					selection[num].Removed();
					list.Add(selection[num].DeepCopy());
					selection[num].Dispose();
					selection.RemoveAt(num);
				}
				GizmoManager.ClearSelection();
				GizmoManager.Update();
				LevelManager.m_currentLevel.ItemChanged();
				new RemoveUndoAction(list.ToArray());
				SoundS.PlaySingleShot("/UI/ItemDelete", Vector3.zero, 1f);
			}
		}
	}
}
