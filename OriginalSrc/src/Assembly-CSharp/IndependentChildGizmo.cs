using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001F3 RID: 499
public class IndependentChildGizmo : BasicGizmo
{
	// Token: 0x06000ED4 RID: 3796 RVA: 0x0008AABC File Offset: 0x00088EBC
	public IndependentChildGizmo(List<GraphElement> _graphElements)
		: base(_graphElements)
	{
		float num = (float)Screen.height / 768f * 100f;
		float num2 = num * 2f;
		float num3 = 0.6f * num2;
		float num4 = 1f * num2;
		float num5 = 0.05f * num2;
		float num6 = 1f * num;
		float num7 = 0.35f * num2;
		this.m_minDistanceFromParent = (this.m_graphElements[0] as GraphNode).m_minDistanceFromParent;
		this.m_maxDistanceFromParent = (this.m_graphElements[0] as GraphNode).m_maxDistanceFromParent;
		if (this.m_graphElements[0].m_isMoveable)
		{
			this.m_TAC = TouchAreaS.AddCircleArea(this.m_uiTC, "Move", num, CameraS.m_uiCamera, null);
			TouchAreaS.AddTouchEventListener(this.m_TAC, new TouchEventDelegate(this.TouchHandler));
			SpriteC spriteC = SpriteS.AddComponent(this.m_uiTC, PsState.m_uiSheet.m_atlas.GetFrame("hud_gizmo_selection_circle", null), PsState.m_uiSheet);
			SpriteS.SetDimensions(spriteC, num2, num2);
			SpriteS.SetOffset(spriteC, new Vector3(0f, 0f, 50f), 0f);
			SpriteS.ConvertSpritesToPrefabComponent(this.m_uiTC, true);
		}
		if (this.m_graphElements[0].m_isRotateable)
		{
			float num8 = 135f;
			TransformC transformC = TransformS.AddComponent(this.m_uiTC.p_entity, "GizmoRotateTC");
			TransformS.ParentComponent(transformC, this.m_uiTC, new Vector3(num6 * Mathf.Sin(num8 * 0.017453292f), num6 * Mathf.Cos(num8 * 0.017453292f), -10f));
			TouchAreaC touchAreaC = TouchAreaS.AddCircleArea(transformC, "RotateZ", num7 * 0.5f, CameraS.m_uiCamera, null);
			TouchAreaS.AddTouchEventListener(touchAreaC, new TouchEventDelegate(this.TouchHandler));
			SpriteC spriteC2 = SpriteS.AddComponent(transformC, PsState.m_uiSheet.m_atlas.GetFrame("hud_gizmo_rotate", null), PsState.m_uiSheet);
			SpriteS.SetDimensions(spriteC2, num7, num7);
			SpriteS.SetOffset(spriteC2, new Vector3(0f, 0f, 50f), 0f);
			SpriteS.ConvertSpritesToPrefabComponent(transformC, true);
		}
		if (this.m_graphElements[0].m_isCopyable)
		{
			float num9 = -135f;
			TransformC transformC2 = TransformS.AddComponent(this.m_uiTC.p_entity, "GizmoCopyTC");
			transformC2.forceRotation = true;
			TransformS.ParentComponent(transformC2, this.m_uiTC, new Vector3(num6 * Mathf.Sin(num9 * 0.017453292f), num6 * Mathf.Cos(num9 * 0.017453292f), -10f));
			TouchAreaC touchAreaC2 = TouchAreaS.AddCircleArea(transformC2, "Copy", num7 * 0.5f, CameraS.m_uiCamera, null);
			TouchAreaS.AddTouchEventListener(touchAreaC2, new TouchEventDelegate(this.TouchHandler));
			SpriteC spriteC3 = SpriteS.AddComponent(transformC2, PsState.m_uiSheet.m_atlas.GetFrame("hud_gizmo_copy", null), PsState.m_uiSheet);
			SpriteS.SetDimensions(spriteC3, num7, num7);
			SpriteS.SetOffset(spriteC3, new Vector3(0f, 0f, 50f), 0f);
			SpriteS.ConvertSpritesToPrefabComponent(transformC2, true);
		}
		if (this.m_graphElements[0].m_isRemovable)
		{
			float num10 = -45f;
			TransformC transformC3 = TransformS.AddComponent(this.m_uiTC.p_entity, "GizmoRemoveTC");
			transformC3.forceRotation = true;
			TransformS.ParentComponent(transformC3, this.m_uiTC, new Vector3(num6 * Mathf.Sin(num10 * 0.017453292f), num6 * Mathf.Cos(num10 * 0.017453292f), -10f));
			TouchAreaC touchAreaC3 = TouchAreaS.AddCircleArea(transformC3, "Remove", num7 * 0.5f, CameraS.m_uiCamera, null);
			TouchAreaS.AddTouchEventListener(touchAreaC3, new TouchEventDelegate(this.TouchHandler));
			SpriteC spriteC4 = SpriteS.AddComponent(transformC3, PsState.m_uiSheet.m_atlas.GetFrame("hud_gizmo_put_back", null), PsState.m_uiSheet);
			SpriteS.SetDimensions(spriteC4, num7, num7);
			SpriteS.SetOffset(spriteC4, new Vector3(0f, 0f, 50f), 0f);
			SpriteS.ConvertSpritesToPrefabComponent(transformC3, true);
		}
		if (this.m_graphElements[0].m_isFlippable)
		{
			float num11 = 70f;
			TransformC transformC4 = TransformS.AddComponent(this.m_uiTC.p_entity, "GizmoFlipTC");
			transformC4.forceRotation = true;
			TransformS.ParentComponent(transformC4, this.m_uiTC, new Vector3(num6 * Mathf.Sin(num11 * 0.017453292f), num6 * Mathf.Cos(num11 * 0.017453292f), -10f));
			TouchAreaC touchAreaC4 = TouchAreaS.AddCircleArea(transformC4, "Flip", num7 * 0.5f, CameraS.m_uiCamera, null);
			TouchAreaS.AddTouchEventListener(touchAreaC4, new TouchEventDelegate(this.TouchHandler));
			SpriteC spriteC5 = SpriteS.AddComponent(transformC4, PsState.m_uiSheet.m_atlas.GetFrame("hud_gizmo_flip", null), PsState.m_uiSheet);
			SpriteS.SetDimensions(spriteC5, num7, num7);
			SpriteS.SetOffset(spriteC5, new Vector3(0f, 0f, 50f), 0f);
			SpriteS.ConvertSpritesToPrefabComponent(transformC4, true);
		}
		if (this.m_graphElements[0].m_isForegroundable)
		{
			float num12 = 15f;
			TransformC transformC5 = TransformS.AddComponent(this.m_uiTC.p_entity, "GizmoFgTC");
			transformC5.forceRotation = true;
			TransformS.ParentComponent(transformC5, this.m_uiTC, new Vector3(num6 * Mathf.Sin(num12 * 0.017453292f), num6 * Mathf.Cos(num12 * 0.017453292f), -10f));
			TouchAreaC touchAreaC5 = TouchAreaS.AddCircleArea(transformC5, "Foreground", num7 * 0.5f, CameraS.m_uiCamera, null);
			TouchAreaS.AddTouchEventListener(touchAreaC5, new TouchEventDelegate(this.TouchHandler));
			SpriteC spriteC6 = SpriteS.AddComponent(transformC5, PsState.m_uiSheet.m_atlas.GetFrame("hud_gizmo_depth", null), PsState.m_uiSheet);
			SpriteS.SetDimensions(spriteC6, num7, num7);
			SpriteS.SetOffset(spriteC6, new Vector3(0f, 0f, 50f), 0f);
			SpriteS.ConvertSpritesToPrefabComponent(transformC5, true);
		}
		if (this.m_graphElements[0].m_isModifiable)
		{
		}
		if (this.m_graphElements[0].m_isReplaceable)
		{
		}
	}

	// Token: 0x06000ED5 RID: 3797 RVA: 0x0008B0FC File Offset: 0x000894FC
	public override void TouchHandler(TouchAreaC _touchArea, TouchAreaPhase _touchPhase, bool _touchIsSecondary, int _touchCount, TLTouch[] _touches)
	{
		if (_touchIsSecondary)
		{
			return;
		}
		if (_touchCount == 1)
		{
			if (_touchArea.m_name == "Copy")
			{
				if (_touchPhase == TouchAreaPhase.ReleaseIn)
				{
					List<GraphElement> originals = new List<GraphElement>();
					List<GraphElement> copies = new List<GraphElement>();
					List<GraphElement> selection = GizmoManager.GetParents(this.m_graphElements);
					string text = (selection[0] as GraphNode).m_assembleClassType.ToString();
					if ((selection[0] as GraphNode).m_assembleClassType == null || PsMetagameData.CanPlaceUnit(text))
					{
						if (PsState.UsingEditorResources())
						{
							Action action = delegate
							{
								originals.Add(selection[0]);
								GraphElement graphElement4 = selection[0].DeepCopy();
								graphElement4.m_id = LevelManager.GetUniqueId();
								Vector3 vector13 = Vector3.right + Vector3.down;
								Vector3 vector14 = vector13 * Random.Range(0f, 0.2f);
								graphElement4.m_position += vector13 * 15f + vector14;
								GraphNode graphNode2 = graphElement4 as GraphNode;
								if (graphNode2.m_childElements.Count != 0)
								{
									for (int num13 = 0; num13 < graphNode2.m_childElements.Count; num13++)
									{
										graphNode2.m_childElements[num13].m_position += vector13 * 15f + vector14;
									}
								}
								selection[0].m_parentElement.AddElement(graphElement4);
								graphElement4.Assemble();
								graphElement4.Copied();
								copies.Add(graphElement4);
								LevelManager.m_currentLevel.ItemChanged();
								new CopyUndoAction(originals.ToArray(), copies.ToArray());
								GizmoManager.ClearSelection();
								GizmoManager.AddToSelection(copies);
								GizmoManager.Update();
								SoundS.PlaySingleShot("/UI/ItemCreate", Vector3.zero, 1f);
								EditorScene.CumulateReservedResources(selection[0].m_name, -1);
							};
							if (PsMetagameManager.m_playerStats.GetEditorResourceCount(text) + EditorScene.GetReservedResourceCount(text) > 0)
							{
								action.Invoke();
							}
							else
							{
								PsEditorItem psEditorItem = null;
								for (int i = 0; i < PsMetagameData.m_units.Count; i++)
								{
									for (int j = 0; j < PsMetagameData.m_units[i].m_items.Count; j++)
									{
										if (PsMetagameData.m_units[i].m_items[j].m_identifier == text)
										{
											psEditorItem = PsMetagameData.m_units[i].m_items[j] as PsEditorItem;
										}
									}
								}
								EditorBaseState editorBaseState = Main.m_currentGame.m_currentScene.m_stateMachine.GetCurrentState() as EditorBaseState;
								if (editorBaseState != null && psEditorItem != null)
								{
									editorBaseState.ShowEditorItemPurchasePopup(psEditorItem, action);
								}
							}
						}
						else
						{
							for (int k = 0; k < selection.Count; k++)
							{
								originals.Add(selection[k]);
								GraphElement graphElement = selection[k].DeepCopy();
								graphElement.m_id = LevelManager.GetUniqueId();
								Vector3 vector = Vector3.right + Vector3.down;
								Vector3 vector2 = vector * Random.Range(0f, 0.2f);
								graphElement.m_position += vector * 15f + vector2;
								GraphNode graphNode = graphElement as GraphNode;
								if (graphNode.m_childElements.Count != 0)
								{
									for (int l = 0; l < graphNode.m_childElements.Count; l++)
									{
										graphNode.m_childElements[l].m_position += vector * 15f + vector2;
									}
								}
								selection[k].m_parentElement.AddElement(graphElement);
								graphElement.Assemble();
								graphElement.Copied();
								copies.Add(graphElement);
							}
							LevelManager.m_currentLevel.ItemChanged();
							new CopyUndoAction(originals.ToArray(), copies.ToArray());
							GizmoManager.ClearSelection();
							GizmoManager.AddToSelection(copies);
							GizmoManager.Update();
							SoundS.PlaySingleShot("/UI/ItemCreate", Vector3.zero, 1f);
						}
					}
				}
			}
			else if (_touchArea.m_name == "Remove")
			{
				if (_touchPhase == TouchAreaPhase.ReleaseIn)
				{
					List<GraphElement> list = new List<GraphElement>();
					List<GraphElement> selection2 = GizmoManager.GetSelection();
					while (selection2.Count > 0)
					{
						int num = selection2.Count - 1;
						if (PsState.UsingEditorResources())
						{
							EditorScene.CumulateReservedResources(selection2[num].m_name, 1);
						}
						selection2[num].Removed();
						list.Add(selection2[num].DeepCopy());
						selection2[num].Dispose();
						selection2.RemoveAt(num);
					}
					GizmoManager.ClearSelection();
					GizmoManager.Update();
					LevelManager.m_currentLevel.ItemChanged();
					new RemoveUndoAction(list.ToArray());
					SoundS.PlaySingleShot("/UI/ItemDelete", Vector3.zero, 1f);
				}
			}
			else if (_touchArea.m_name == "Flip")
			{
				if (_touchPhase == TouchAreaPhase.ReleaseIn)
				{
					List<GraphElement> list2 = new List<GraphElement>();
					List<GraphElement> parents = GizmoManager.GetParents(this.m_graphElements);
					for (int m = 0; m < parents.Count; m++)
					{
						parents[m].Flipped();
						parents[m].m_flipped = !parents[m].m_flipped;
						parents[m].Reset();
						list2.Add(parents[m]);
					}
					GizmoManager.Update();
					LevelManager.m_currentLevel.ItemChanged();
					new FlipUndoAction(list2.ToArray());
					SoundS.PlaySingleShot("/UI/ItemFlip", Vector3.zero, 1f);
				}
			}
			else if (_touchArea.m_name == "Foreground")
			{
				if (_touchPhase == TouchAreaPhase.ReleaseIn)
				{
					List<GraphElement> list3 = new List<GraphElement>();
					List<GraphElement> parents2 = GizmoManager.GetParents(this.m_graphElements);
					for (int n = 0; n < parents2.Count; n++)
					{
						parents2[n].Foregrounded();
						parents2[n].m_inFront = !parents2[n].m_inFront;
						parents2[n].Reset();
						list3.Add(parents2[n]);
					}
					GizmoManager.Update();
					LevelManager.m_currentLevel.ItemChanged();
					new FlipUndoAction(list3.ToArray());
					SoundS.PlaySingleShot("/UI/ItemFlip", Vector3.zero, 1f);
				}
			}
			else if (_touchArea.m_name == "Move")
			{
				if (_touchPhase == TouchAreaPhase.Began || _touchPhase == TouchAreaPhase.DragStart)
				{
					if (!this.m_readyToMove)
					{
						Vector3 vector3 = _touches[0].m_currentPosition;
						vector3.x -= (float)Screen.width * 0.5f;
						vector3.y -= (float)Screen.height * 0.5f;
						vector3.z = -10f;
						this.m_touchOffset = this.m_uiTC.transform.position - vector3;
						this.m_readyToMove = true;
						this.m_startPositions = new List<Vector3>();
						for (int num2 = 0; num2 < this.m_graphElements.Count; num2++)
						{
							this.m_startPositions.Add(this.m_graphElements[num2].m_position);
						}
					}
				}
				else if (this.m_readyToMove && (_touchPhase == TouchAreaPhase.MoveIn || _touchPhase == TouchAreaPhase.MoveOut || _touchPhase == TouchAreaPhase.StationaryIn || _touchPhase == TouchAreaPhase.StationaryOut))
				{
					Vector3 vector4 = _touches[0].m_currentPosition + this.m_touchOffset;
					vector4.x -= (float)Screen.width * 0.5f;
					vector4.y -= (float)Screen.height * 0.5f;
					vector4.z = 0f;
					base.DragCameraAtBorders();
					Vector3 vector5 = vector4 - this.m_uiTC.transform.position;
					Vector3 vector6 = vector5 * 0.382f;
					TransformS.GlobalMove(this.m_uiTC, vector6);
					Vector3 vector7 = TouchAreaS.GetTouchWorldPos(CameraS.m_mainCamera, this.m_uiTC.transform.position + new Vector3((float)Screen.width * 0.5f, (float)Screen.height * 0.5f, 0f), -this.m_tacTCDepth);
					if (this.m_minDistanceFromParent != 0f || this.m_maxDistanceFromParent != 0f)
					{
						Vector2 vector8 = vector7 - this.m_graphElements[0].m_parentElement.m_TC.transform.position;
						if (vector8.sqrMagnitude < this.m_minDistanceFromParent * this.m_minDistanceFromParent)
						{
							vector8 = vector8.normalized * this.m_minDistanceFromParent;
							vector7 = this.m_graphElements[0].m_parentElement.m_TC.transform.position + vector8;
						}
						else if (vector8.sqrMagnitude > this.m_maxDistanceFromParent * this.m_maxDistanceFromParent)
						{
							vector8 = vector8.normalized * this.m_maxDistanceFromParent;
							vector7 = this.m_graphElements[0].m_parentElement.m_TC.transform.position + vector8;
						}
					}
					vector7 = BasicGizmo.LimitInsideDome(vector7);
					vector7.z = this.m_worldTC.transform.position.z;
					TransformS.SetGlobalPosition(this.m_worldTC, vector7);
					for (int num3 = 0; num3 < this.m_graphElements.Count; num3++)
					{
						GraphElement graphElement2 = this.m_graphElements[num3];
						Vector3 vector9 = vector7 + this.m_selectionOffsets[num3];
						TransformS.SetGlobalPosition(graphElement2.m_TC, vector9);
						graphElement2.m_position = vector9;
						for (int num4 = 0; num4 < graphElement2.m_assembledClasses.Count; num4++)
						{
							graphElement2.m_assembledClasses[num4].SyncPositionToGraphElementPosition();
						}
					}
				}
				else if (_touchPhase == TouchAreaPhase.DragEnd)
				{
					new MoveUndoAction(this.m_graphElements, this.m_startPositions);
				}
				else if (_touchPhase == TouchAreaPhase.ReleaseIn || _touchPhase == TouchAreaPhase.ReleaseOut)
				{
					LevelManager.m_currentLevel.ItemChanged();
					this.m_readyToMove = false;
				}
			}
			else if (_touchArea.m_name == "RotateZ")
			{
				Vector3 touchWorldPos = TouchAreaS.GetTouchWorldPos(CameraS.m_mainCamera, this.m_uiTC.transform.position + new Vector3((float)Screen.width * 0.5f, (float)Screen.height * 0.5f, 0f), 0f);
				Vector3 vector10 = _touches[0].m_currentPosition;
				Vector3 touchWorldPos2 = TouchAreaS.GetTouchWorldPos(CameraS.m_mainCamera, vector10, 0f);
				if (_touchPhase == TouchAreaPhase.Began)
				{
					this.m_startRotations = new List<Vector3>();
					for (int num5 = 0; num5 < this.m_graphElements.Count; num5++)
					{
						this.m_startRotations.Add(this.m_graphElements[num5].m_rotation);
					}
					Vector2 vector11 = touchWorldPos2 - touchWorldPos;
					float num6 = Mathf.Atan2(vector11.y, vector11.x) * 57.29578f;
					this.m_startAngle = num6;
				}
				else if (_touchPhase == TouchAreaPhase.MoveIn || _touchPhase == TouchAreaPhase.MoveOut || _touchPhase == TouchAreaPhase.StationaryIn || _touchPhase == TouchAreaPhase.StationaryOut)
				{
					Vector2 vector12 = touchWorldPos2 - touchWorldPos;
					float num7 = Mathf.Atan2(vector12.y, vector12.x) * 57.29578f;
					for (int num8 = 0; num8 < this.m_graphElements.Count; num8++)
					{
						GraphElement graphElement3 = this.m_graphElements[num8];
						graphElement3.m_rotation = Vector3.forward * (num7 - this.m_startAngle) + this.m_startRotations[num8];
						TransformS.SetGlobalRotation(graphElement3.m_TC, graphElement3.m_rotation);
						for (int num9 = 0; num9 < graphElement3.m_assembledClasses.Count; num9++)
						{
							IAssembledClass assembledClass = graphElement3.m_assembledClasses[num9];
							for (int num10 = 0; num10 < assembledClass.m_assembledEntities.Count; num10++)
							{
								List<IComponent> componentsByEntity = EntityManager.GetComponentsByEntity(ComponentType.ChipmunkBody, assembledClass.m_assembledEntities[num10]);
								for (int num11 = 0; num11 < componentsByEntity.Count; num11++)
								{
									ChipmunkBodyC chipmunkBodyC = componentsByEntity[num11] as ChipmunkBodyC;
									ChipmunkProWrapper.ucpBodySetAngle(chipmunkBodyC.body, ToolBox.getCappedAngle(graphElement3.m_rotation.z) * 0.017453292f);
								}
								List<IComponent> componentsByEntity2 = EntityManager.GetComponentsByEntity(ComponentType.Transform, assembledClass.m_assembledEntities[num10]);
								for (int num12 = 0; num12 < componentsByEntity2.Count; num12++)
								{
									TransformC transformC = componentsByEntity2[num12] as TransformC;
									TransformS.SetGlobalRotation(transformC, graphElement3.m_rotation);
								}
							}
						}
					}
				}
				else if (_touchPhase == TouchAreaPhase.ReleaseIn || _touchPhase == TouchAreaPhase.ReleaseOut)
				{
					LevelManager.m_currentLevel.ItemChanged();
					new RotateUndoAction(this.m_graphElements, this.m_startRotations);
				}
			}
		}
	}

	// Token: 0x040011C0 RID: 4544
	private float m_minDistanceFromParent;

	// Token: 0x040011C1 RID: 4545
	private float m_maxDistanceFromParent;
}
