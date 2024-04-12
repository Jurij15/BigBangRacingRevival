using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001F7 RID: 503
public class ParentGizmo : BasicGizmo
{
	// Token: 0x06000EDC RID: 3804 RVA: 0x0008C5DC File Offset: 0x0008A9DC
	public ParentGizmo(List<GraphElement> _graphElements)
		: base(_graphElements)
	{
		float num = (float)Screen.height / 768f * 100f;
		float num2 = num * 2f;
		float num3 = 0.6f * num2;
		float num4 = 1f * num2;
		float num5 = 0.05f * num2;
		float num6 = 1f * num;
		float num7 = 0.35f * num2;
		if (this.m_graphElements[0].m_isMoveable)
		{
			this.m_TAC = TouchAreaS.AddCircleArea(this.m_uiTC, "Move", num, CameraS.m_uiCamera, null);
			TouchAreaS.AddTouchEventListener(this.m_TAC, new TouchEventDelegate(this.TouchHandler));
			SpriteC spriteC = SpriteS.AddComponent(this.m_uiTC, PsState.m_uiSheet.m_atlas.GetFrame("hud_gizmo_selection_circle", null), PsState.m_uiSheet);
			SpriteS.SetDimensions(spriteC, num2, num2);
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
			SpriteS.ConvertSpritesToPrefabComponent(transformC4, true);
		}
		if (this.m_graphElements[0].m_isForegroundable)
		{
			float num12 = 32f;
			TransformC transformC5 = TransformS.AddComponent(this.m_uiTC.p_entity, "GizmoFgTC");
			transformC5.forceRotation = true;
			TransformS.ParentComponent(transformC5, this.m_uiTC, new Vector3(num6 * Mathf.Sin(num12 * 0.017453292f), num6 * Mathf.Cos(num12 * 0.017453292f), -10f));
			TouchAreaC touchAreaC5 = TouchAreaS.AddCircleArea(transformC5, "Foreground", num7 * 0.5f, CameraS.m_uiCamera, null);
			TouchAreaS.AddTouchEventListener(touchAreaC5, new TouchEventDelegate(this.TouchHandler));
			SpriteC spriteC6 = SpriteS.AddComponent(transformC5, PsState.m_uiSheet.m_atlas.GetFrame("hud_gizmo_depth", null), PsState.m_uiSheet);
			SpriteS.SetDimensions(spriteC6, num7, num7);
			SpriteS.ConvertSpritesToPrefabComponent(transformC5, true);
		}
		if (this.m_graphElements[0].m_isModifiable)
		{
		}
		if (this.m_graphElements[0].m_isReplaceable)
		{
		}
		if (this.m_graphElements[0].m_isLinkable && this.m_graphElements[0].CanCreateConnection())
		{
			float num13 = 0f;
			TransformC transformC6 = TransformS.AddComponent(this.m_uiTC.p_entity, "GizmoLinkTC");
			transformC6.forceRotation = false;
			TransformS.ParentComponent(transformC6, this.m_uiTC, new Vector3(num6 * Mathf.Sin(num13 * 0.017453292f), num6 * Mathf.Cos(num13 * 0.017453292f), -10f));
			TouchAreaC touchAreaC6 = TouchAreaS.AddCircleArea(transformC6, "Link", num7 * 0.5f, CameraS.m_uiCamera, null);
			TouchAreaS.AddTouchEventListener(touchAreaC6, new TouchEventDelegate(this.TouchHandler));
			SpriteC spriteC7 = SpriteS.AddComponent(transformC6, PsState.m_uiSheet.m_atlas.GetFrame("hud_gizmo_connect", null), PsState.m_uiSheet);
			SpriteS.SetDimensions(spriteC7, num7, num7);
			SpriteS.ConvertSpritesToPrefabComponent(transformC6, true);
		}
	}

	// Token: 0x06000EDD RID: 3805 RVA: 0x0008CC14 File Offset: 0x0008B014
	public void HighlightPossibleConnections(List<GraphElement> _list)
	{
		this.m_linkableGizmos = new Dictionary<GraphElement, LinkableGizmo>();
		for (int i = 0; i < _list.Count; i++)
		{
			Dictionary<GraphElement, LinkableGizmo> linkableGizmos = this.m_linkableGizmos;
			GraphElement graphElement = _list[i];
			List<GraphElement> list = new List<GraphElement>();
			list.Add(_list[i]);
			linkableGizmos[graphElement] = new LinkableGizmo(list);
		}
	}

	// Token: 0x06000EDE RID: 3806 RVA: 0x0008CC6E File Offset: 0x0008B06E
	private void AnimateChosenLinkGizmo(GraphElement _target)
	{
		if (this.m_linkableGizmos.ContainsKey(_target))
		{
			this.m_linkableGizmos[_target].SetHighlight();
		}
		else
		{
			Debug.LogError("Didnt have");
		}
	}

	// Token: 0x06000EDF RID: 3807 RVA: 0x0008CCA4 File Offset: 0x0008B0A4
	public override void TouchHandler(TouchAreaC _touchArea, TouchAreaPhase _touchPhase, bool _touchIsSecondary, int _touchCount, TLTouch[] _touches)
	{
		if (_touchIsSecondary || this.m_uiTC == null)
		{
			return;
		}
		if (_touchCount == 1)
		{
			if (_touchArea.m_name == "Link")
			{
				if (_touchPhase == TouchAreaPhase.Began || _touchPhase == TouchAreaPhase.DragStart)
				{
					if (!this.m_readyToLink)
					{
						Vector3 vector = _touches[0].m_currentPosition;
						vector.x -= (float)Screen.width * 0.5f;
						vector.y -= (float)Screen.height * 0.5f;
						vector.z = -10f;
						this.m_touchOffset = this.m_uiTC.transform.position - vector;
						this.m_readyToLink = true;
						this.m_startPositions = new List<Vector3>();
						for (int i = 0; i < this.m_graphElements.Count; i++)
						{
							this.m_startPositions.Add(this.m_graphElements[i].m_position);
						}
						EntityManager.SetVisibilityOfEntity(this.m_uiTC.p_entity, false);
						List<GraphElement> list = LevelManager.m_currentLevel.m_currentLayer.m_childElements;
						list = this.m_graphElements[0].FilterSuitables(list).FindAll((GraphElement c) => c.CanConnect(this.m_graphElements[0]));
						this.HighlightPossibleConnections(list);
					}
				}
				else if (this.m_readyToLink && (_touchPhase == TouchAreaPhase.MoveIn || _touchPhase == TouchAreaPhase.MoveOut || _touchPhase == TouchAreaPhase.StationaryIn || _touchPhase == TouchAreaPhase.StationaryOut))
				{
					TLTouch tltouch = _touches[0];
					Vector3 vector2 = tltouch.m_currentPosition + this.m_touchOffset;
					vector2.x -= (float)Screen.width * 0.5f;
					vector2.y -= (float)Screen.height * 0.5f;
					vector2.z = -10f;
					base.DragCameraAtBorders();
					Vector3 vector3 = vector2 - this.m_uiTC.transform.position;
					Vector3 vector4 = vector3 * 0.382f;
					Vector3 vector5 = TouchAreaS.GetTouchWorldPos(CameraS.m_mainCamera, _touches[0].m_currentPosition, 0f);
					List<GraphElement> list2 = LevelManager.m_currentLevel.m_currentLayer.m_childElements;
					list2 = this.m_graphElements[0].FilterSuitables(list2).FindAll((GraphElement c) => c.CanConnect(this.m_graphElements[0]));
					for (int j = 0; j < list2.Count; j++)
					{
						bool flag = false;
						for (int k = 0; k < this.m_graphElements.Count; k++)
						{
							if (list2[j].m_elementType != GraphElementType.Node || list2[j] == this.m_graphElements[k])
							{
								flag = true;
								break;
							}
						}
						if (!flag)
						{
							if (list2[j].m_TC != null)
							{
								float num = Math.Max(-(CameraS.m_mainCamera.transform.position.z + 500f), 1000f) / 10f;
								float sqrMagnitude = (vector5 - list2[j].m_TC.transform.position).sqrMagnitude;
								if (sqrMagnitude < num * num)
								{
									if (this.m_linkTarget == null)
									{
										this.m_linkTarget = list2[j];
									}
									else if (sqrMagnitude < (vector5 - this.m_linkTarget.m_TC.transform.position).sqrMagnitude)
									{
										this.m_linkTarget.Select(false);
										this.m_linkTarget = list2[j];
									}
								}
								else if (list2[j] == this.m_linkTarget)
								{
									list2[j].Select(false);
									this.m_linkTarget = null;
								}
							}
						}
					}
					if (this.m_linkTarget != null)
					{
						this.AnimateChosenLinkGizmo(this.m_linkTarget);
						this.m_linkTarget.Select(true);
						vector5 = this.m_linkTarget.GetInputPosition();
					}
					Vector3[] array = new Vector3[]
					{
						this.m_graphElements[0].GetOutputPosition(),
						vector5
					};
					if (this.m_connectionArrow != null)
					{
						this.m_connectionArrow.SetEndPosition(array[1]);
					}
					else
					{
						this.m_connectionArrow = this.m_graphElements[0].GetVisualConnection(array[0], array[1]);
					}
				}
				else if (_touchPhase != TouchAreaPhase.DragEnd)
				{
					if (_touchPhase == TouchAreaPhase.ReleaseIn || _touchPhase == TouchAreaPhase.ReleaseOut)
					{
						if (this.m_linkTarget != null && this.m_linkTarget.CanConnect(this.m_graphElements[0]))
						{
							for (int l = 0; l < this.m_graphElements.Count; l++)
							{
								GraphConnection graphConnection = this.m_graphElements[l].CreateConnection(this.m_linkTarget);
								LevelManager.m_currentLevel.m_currentLayer.AddElement(graphConnection);
								graphConnection.Assemble();
								this.m_graphElements[l].Linked();
							}
							this.m_linkTarget.Select(false);
							this.m_linkTarget.Linked();
							this.m_linkTarget = null;
							LevelManager.m_currentLevel.ItemChanged();
						}
						this.m_readyToLink = false;
						GizmoManager.ClearSelection();
						GizmoManager.Update();
						this.m_connectionArrow.Destroy();
						this.m_connectionArrow = null;
					}
				}
			}
			if (_touchArea.m_name == "Copy")
			{
				if (_touchPhase == TouchAreaPhase.ReleaseIn)
				{
					List<GraphElement> originals = new List<GraphElement>();
					List<GraphElement> copies = new List<GraphElement>();
					List<GraphElement> selection = GizmoManager.GetParents(this.m_graphElements);
					string text = string.Empty;
					Type assembleClassType = (selection[0] as GraphNode).m_assembleClassType;
					if (assembleClassType != null)
					{
						text = assembleClassType.ToString();
					}
					if (assembleClassType == null || PsMetagameData.CanPlaceUnit(text))
					{
						if (PsState.UsingEditorResources())
						{
							Action action = delegate
							{
								originals.Add(selection[0]);
								GraphElement graphElement3 = selection[0].DeepCopy();
								graphElement3.m_id = LevelManager.GetUniqueId();
								Vector3 vector18 = Vector3.right + Vector3.down;
								Vector3 vector19 = vector18 * Random.Range(0f, 0.2f);
								graphElement3.m_position += vector18 * 15f + vector19;
								GraphNode graphNode3 = graphElement3 as GraphNode;
								if (graphNode3.m_childElements.Count != 0)
								{
									for (int num18 = 0; num18 < graphNode3.m_childElements.Count; num18++)
									{
										graphNode3.m_childElements[num18].m_position += vector18 * 15f + vector19;
									}
								}
								selection[0].m_parentElement.AddElement(graphElement3);
								graphElement3.Assemble();
								graphElement3.Copied();
								copies.Add(graphElement3);
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
								for (int m = 0; m < PsMetagameData.m_units.Count; m++)
								{
									for (int n = 0; n < PsMetagameData.m_units[m].m_items.Count; n++)
									{
										if (PsMetagameData.m_units[m].m_items[n].m_identifier == text)
										{
											psEditorItem = PsMetagameData.m_units[m].m_items[n] as PsEditorItem;
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
							for (int num2 = 0; num2 < selection.Count; num2++)
							{
								originals.Add(selection[num2]);
								GraphElement graphElement = selection[num2].DeepCopy();
								graphElement.m_id = LevelManager.GetUniqueId();
								Vector3 vector6 = Vector3.right + Vector3.down;
								Vector3 vector7 = vector6 * Random.Range(0f, 0.2f);
								graphElement.m_position += vector6 * 15f + vector7;
								GraphNode graphNode = graphElement as GraphNode;
								if (graphNode.m_childElements.Count != 0)
								{
									for (int num3 = 0; num3 < graphNode.m_childElements.Count; num3++)
									{
										graphNode.m_childElements[num3].m_position += vector6 * 15f + vector7;
									}
								}
								selection[num2].m_parentElement.AddElement(graphElement);
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
					List<GraphElement> list3 = new List<GraphElement>();
					List<GraphElement> selection2 = GizmoManager.GetSelection();
					while (selection2.Count > 0)
					{
						int num4 = selection2.Count - 1;
						if (PsState.UsingEditorResources())
						{
							EditorScene.CumulateReservedResources(selection2[num4].m_name, 1);
						}
						selection2[num4].Removed();
						list3.Add(selection2[num4].DeepCopy());
						selection2[num4].Dispose();
						selection2.RemoveAt(num4);
					}
					GizmoManager.ClearSelection();
					GizmoManager.Update();
					LevelManager.m_currentLevel.ItemChanged();
					new RemoveUndoAction(list3.ToArray());
					SoundS.PlaySingleShot("/UI/ItemDelete", Vector3.zero, 1f);
				}
			}
			else if (_touchArea.m_name == "Flip")
			{
				if (_touchPhase == TouchAreaPhase.ReleaseIn)
				{
					List<GraphElement> list4 = new List<GraphElement>();
					List<GraphElement> parents = GizmoManager.GetParents(this.m_graphElements);
					for (int num5 = 0; num5 < parents.Count; num5++)
					{
						parents[num5].Flipped();
						parents[num5].m_flipped = !parents[num5].m_flipped;
						parents[num5].Reset();
						list4.Add(parents[num5]);
					}
					GizmoManager.Update();
					LevelManager.m_currentLevel.ItemChanged();
					new FlipUndoAction(list4.ToArray());
					SoundS.PlaySingleShot("/UI/ItemFlip", Vector3.zero, 1f);
				}
			}
			else if (_touchArea.m_name == "Foreground")
			{
				if (_touchPhase == TouchAreaPhase.ReleaseIn)
				{
					List<GraphElement> list5 = new List<GraphElement>();
					List<GraphElement> parents2 = GizmoManager.GetParents(this.m_graphElements);
					for (int num6 = 0; num6 < parents2.Count; num6++)
					{
						parents2[num6].Foregrounded();
						parents2[num6].m_inFront = !parents2[num6].m_inFront;
						parents2[num6].Reset();
						list5.Add(parents2[num6]);
					}
					GizmoManager.Update();
					LevelManager.m_currentLevel.ItemChanged();
					new FlipUndoAction(list5.ToArray());
					SoundS.PlaySingleShot("/UI/ItemFlip", Vector3.zero, 1f);
				}
			}
			else if (_touchArea.m_name == "Move")
			{
				if (_touchPhase == TouchAreaPhase.Began || _touchPhase == TouchAreaPhase.DragStart)
				{
					if (!this.m_readyToMove)
					{
						Vector3 vector8 = _touches[0].m_currentPosition;
						vector8.x -= (float)Screen.width * 0.5f;
						vector8.y -= (float)Screen.height * 0.5f;
						vector8.z = -10f;
						this.m_touchOffset = this.m_uiTC.transform.position - vector8;
						this.m_readyToMove = true;
						this.m_startPositions = new List<Vector3>();
						for (int num7 = 0; num7 < this.m_graphElements.Count; num7++)
						{
							this.m_startPositions.Add(this.m_graphElements[num7].m_position);
						}
					}
				}
				else if (this.m_readyToMove && (_touchPhase == TouchAreaPhase.MoveIn || _touchPhase == TouchAreaPhase.MoveOut || _touchPhase == TouchAreaPhase.StationaryIn || _touchPhase == TouchAreaPhase.StationaryOut))
				{
					Vector3 vector9 = _touches[0].m_currentPosition + this.m_touchOffset;
					vector9.x -= (float)Screen.width * 0.5f;
					vector9.y -= (float)Screen.height * 0.5f;
					vector9.z = -10f;
					base.DragCameraAtBorders();
					Vector3 vector10 = vector9 - this.m_uiTC.transform.position;
					Vector3 vector11 = vector10 * 0.382f;
					TransformS.GlobalMove(this.m_uiTC, vector11);
					Vector3 vector12 = TouchAreaS.GetTouchWorldPos(CameraS.m_mainCamera, this.m_uiTC.transform.position + new Vector3((float)Screen.width * 0.5f, (float)Screen.height * 0.5f, 0f), -this.m_tacTCDepth);
					vector12 = BasicGizmo.LimitInsideDome(vector12);
					Vector3 vector13 = this.m_worldTC.transform.position - vector12;
					base.MoveGizmo(vector12);
					for (int num8 = 0; num8 < GizmoManager.m_childGizmos.Count; num8++)
					{
						bool flag2 = true;
						for (int num9 = 0; num9 < GizmoManager.m_childGizmos[num8].m_graphElements.Count; num9++)
						{
							GraphNode graphNode2 = GizmoManager.m_childGizmos[num8].m_graphElements[num9] as GraphNode;
							if (!graphNode2.m_parentElement.m_moveChilds)
							{
								flag2 = false;
							}
						}
						if (flag2)
						{
							Vector3 vector14 = GizmoManager.m_childGizmos[num8].m_worldTC.transform.position - vector13;
							GizmoManager.m_childGizmos[num8].MoveGizmo(vector14);
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
			else if (_touchArea.m_name == "RotateZ")
			{
				Vector3 touchWorldPos = TouchAreaS.GetTouchWorldPos(CameraS.m_mainCamera, this.m_uiTC.transform.position + new Vector3((float)Screen.width * 0.5f, (float)Screen.height * 0.5f, 0f), 0f);
				Vector3 vector15 = _touches[0].m_currentPosition;
				Vector3 touchWorldPos2 = TouchAreaS.GetTouchWorldPos(CameraS.m_mainCamera, vector15, 0f);
				if (_touchPhase == TouchAreaPhase.Began)
				{
					this.m_startRotations = new List<Vector3>();
					for (int num10 = 0; num10 < this.m_graphElements.Count; num10++)
					{
						this.m_startRotations.Add(this.m_graphElements[num10].m_rotation);
					}
					Vector2 vector16 = touchWorldPos2 - touchWorldPos;
					float num11 = Mathf.Atan2(vector16.y, vector16.x) * 57.29578f;
					this.m_startAngle = num11;
				}
				else if ((_touchPhase == TouchAreaPhase.MoveIn || _touchPhase == TouchAreaPhase.MoveOut || _touchPhase == TouchAreaPhase.StationaryIn || _touchPhase == TouchAreaPhase.StationaryOut) && this.m_graphElements.Count == this.m_startRotations.Count)
				{
					Vector2 vector17 = touchWorldPos2 - touchWorldPos;
					float num12 = Mathf.Atan2(vector17.y, vector17.x) * 57.29578f;
					for (int num13 = 0; num13 < this.m_graphElements.Count; num13++)
					{
						GraphElement graphElement2 = this.m_graphElements[num13];
						graphElement2.m_rotation = Vector3.forward * (num12 - this.m_startAngle) + this.m_startRotations[num13];
						TransformS.SetGlobalRotation(graphElement2.m_TC, graphElement2.m_rotation);
						graphElement2.Rotated();
						for (int num14 = 0; num14 < graphElement2.m_assembledClasses.Count; num14++)
						{
							IAssembledClass assembledClass = graphElement2.m_assembledClasses[num14];
							for (int num15 = 0; num15 < assembledClass.m_assembledEntities.Count; num15++)
							{
								List<IComponent> componentsByEntity = EntityManager.GetComponentsByEntity(ComponentType.ChipmunkBody, assembledClass.m_assembledEntities[num15]);
								for (int num16 = 0; num16 < componentsByEntity.Count; num16++)
								{
									ChipmunkBodyC chipmunkBodyC = componentsByEntity[num16] as ChipmunkBodyC;
									ChipmunkProWrapper.ucpBodySetAngle(chipmunkBodyC.body, ToolBox.getCappedAngle(graphElement2.m_rotation.z) * 0.017453292f);
								}
								List<IComponent> componentsByEntity2 = EntityManager.GetComponentsByEntity(ComponentType.Transform, assembledClass.m_assembledEntities[num15]);
								for (int num17 = 0; num17 < componentsByEntity2.Count; num17++)
								{
									TransformC transformC = componentsByEntity2[num17] as TransformC;
									TransformS.SetGlobalRotation(transformC, graphElement2.m_rotation);
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

	// Token: 0x040011C4 RID: 4548
	private Dictionary<GraphElement, LinkableGizmo> m_linkableGizmos;

	// Token: 0x040011C5 RID: 4549
	private GraphConnectionVisual m_connectionArrow;
}
