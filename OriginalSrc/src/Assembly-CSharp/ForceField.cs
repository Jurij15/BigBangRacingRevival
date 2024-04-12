using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200005A RID: 90
public class ForceField : Unit
{
	// Token: 0x06000211 RID: 529 RVA: 0x00019C30 File Offset: 0x00018030
	public ForceField(GraphElement _graphElement)
		: base(_graphElement, UnitType.Basic)
	{
		this.m_alienContactCount = 0;
		this.m_enabled = true;
		this.m_depth = 75f;
		GraphNode graphNode = base.m_graphElement as GraphNode;
		graphNode.m_moveChilds = false;
		graphNode.m_nodeType = GraphNodeType.Invisible;
		if (graphNode.m_childElements.Count == 0)
		{
			this.m_node1 = new GraphNode(GraphNodeType.Child, typeof(TouchableAssembledClass), "ForceNode1", _graphElement.m_position + Vector3.right * 50f + Vector3.forward * this.m_depth + Vector3.up * 100f, Vector3.zero, Vector3.one);
			this.m_node2 = new GraphNode(GraphNodeType.Child, typeof(TouchableAssembledClass), "ForceNode2", _graphElement.m_position + Vector3.right * -50f + Vector3.forward * this.m_depth + Vector3.up * 100f, Vector3.zero, Vector3.one);
			this.m_trigger = new GraphNode(GraphNodeType.IndependentChild, typeof(TouchableAssembledClass), "Trigger", _graphElement.m_position + Vector3.forward * this.m_depth, Vector3.zero, Vector3.one);
			graphNode.AddElement(this.m_node1);
			graphNode.AddElement(this.m_node2);
			graphNode.AddElement(this.m_trigger);
		}
		else
		{
			this.m_node1 = graphNode.m_childElements[0] as GraphNode;
			this.m_node2 = graphNode.m_childElements[1] as GraphNode;
			this.m_trigger = graphNode.m_childElements[2] as GraphNode;
			this.m_node1.m_position.z = this.m_depth;
			this.m_node2.m_position.z = this.m_depth;
			this.m_trigger.m_position.z = this.m_depth;
		}
		base.m_graphElement.m_position = (this.m_node1.m_position + this.m_node2.m_position) / 2f;
		this.m_node1.m_minDistanceFromParent = 50f;
		this.m_node1.m_maxDistanceFromParent = 10000f;
		this.m_node2.m_minDistanceFromParent = 50f;
		this.m_node2.m_maxDistanceFromParent = 10000f;
		this.m_trigger.m_minDistanceFromParent = 0f;
		this.m_trigger.m_maxDistanceFromParent = 50000f;
		this.m_node1.m_isRemovable = false;
		this.m_node2.m_isRemovable = false;
		this.m_trigger.m_isRemovable = true;
		this.m_node1.m_isCopyable = false;
		this.m_node2.m_isCopyable = false;
		this.m_trigger.m_isCopyable = true;
		this.m_trigger.m_isFlippable = true;
		this.m_trigger.m_isRotateable = false;
		this.m_trigger.m_isForegroundable = true;
		if (base.m_graphElement.m_storedRotation == Vector3.zero)
		{
			int num = 1;
			while (ForceField.m_reserverdColors.Contains((ForceFieldColor)num))
			{
				num++;
			}
			this.m_color = (ForceFieldColor)num;
			base.m_graphElement.m_storedRotation = new Vector3((float)num, (float)num, (float)num);
		}
		else
		{
			if (base.m_graphElement.m_flipped)
			{
				int num2 = Enum.GetNames(typeof(ForceFieldColor)).Length;
				base.m_graphElement.m_flipped = false;
				if (ForceField.m_reserverdColors.Count < num2)
				{
					int num3 = (int)base.m_graphElement.m_storedRotation.x + 1;
					if (num3 > num2)
					{
						num3 = 1;
					}
					while (ForceField.m_reserverdColors.Contains((ForceFieldColor)num3))
					{
						num3++;
						if (num3 > num2)
						{
							num3 = 1;
						}
					}
					base.m_graphElement.m_storedRotation = new Vector3((float)num3, (float)num3, (float)num3);
				}
			}
			this.m_color = (ForceFieldColor)base.m_graphElement.m_storedRotation.x;
		}
		if (base.m_graphElement.m_inFront)
		{
			this.m_enabled = false;
		}
		ForceField.m_reserverdColors.Add(this.m_color);
		this.m_radius = 50f;
		this.m_mainPrefab = ResourceManager.GetGameObject(RESOURCE.ForceFieldPrefab_GameObject);
		this.m_node1TC = TransformS.AddComponent(this.m_entity, "ForceNode1");
		TransformS.SetGlobalTransform(this.m_node1TC, this.m_node1.m_position + Vector3.forward * this.m_depth, this.m_node1.m_rotation);
		this.m_node2TC = TransformS.AddComponent(this.m_entity, "ForceNode2");
		TransformS.SetGlobalTransform(this.m_node2TC, this.m_node2.m_position + Vector3.forward * this.m_depth, this.m_node2.m_rotation);
		this.m_triggerTC = TransformS.AddComponent(this.m_entity, "Trigger");
		TransformS.SetGlobalTransform(this.m_triggerTC, this.m_trigger.m_position + Vector3.forward * this.m_depth, this.m_trigger.m_rotation);
		TransformC transformC = TransformS.AddComponent(this.m_entity, "Base");
		TransformS.SetGlobalTransform(transformC, this.m_trigger.m_position, Vector3.zero);
		PrefabC prefabC = PrefabS.AddComponent(transformC, Vector3.zero, this.m_mainPrefab);
		this.m_effect = prefabC.p_gameObject.GetComponent<EffectForceField>();
		this.m_effect.Initialize(this.m_color, this.m_enabled);
		this.m_base = prefabC.p_gameObject.transform.Find("ForceFieldButton");
		this.m_base.position = this.m_triggerTC.transform.position + Vector3.forward * this.m_depth * 0.5f;
		this.m_base.rotation = Quaternion.Euler(this.m_trigger.m_rotation);
		this.m_top = prefabC.p_gameObject.transform.Find("ForceFieldStart");
		this.m_top.position = this.m_node1TC.transform.position;
		this.m_bottom = prefabC.p_gameObject.transform.Find("ForceFieldEnd");
		this.m_bottom.position = this.m_node2TC.transform.position;
		TransformS.SetGlobalRotation(this.m_node1TC, this.m_top.transform.rotation.eulerAngles);
		TransformS.SetGlobalRotation(this.m_node2TC, this.m_bottom.transform.rotation.eulerAngles);
		float magnitude = (this.m_node2.m_position - this.m_node1.m_position).magnitude;
		this.m_length = magnitude;
		if (!this.m_minigame.m_editing)
		{
			ucpPolyShape ucpPolyShape = new ucpPolyShape(50f, 50f, Vector2.zero, 257U, 1f, 0.5f, 0.5f, (ucpCollisionType)10, true);
			this.m_triggerCMB = ChipmunkProS.AddStaticBody(this.m_triggerTC, ucpPolyShape, null);
			ChipmunkProS.AddCollisionHandler(this.m_triggerCMB, new CollisionDelegate(this.TriggerCollisionHandler), (ucpCollisionType)10, (ucpCollisionType)3, true, false, true);
			ucpPolyShape ucpPolyShape2 = new ucpPolyShape(magnitude, 2f, Vector2.zero, 257U, 1f, 0.5f, 1f, (ucpCollisionType)4, false);
			this.m_fieldCMB = ChipmunkProS.AddStaticBody(base.m_graphElement.m_TC, ucpPolyShape2, null);
			this.m_fieldCMB.customComponent = this.m_unitC;
			if (!this.m_enabled)
			{
				for (int i = 0; i < this.m_fieldCMB.shapes.Count; i++)
				{
					ChipmunkProWrapper.ucpShapeSetLayers(this.m_fieldCMB.shapes[i].shapePtr, 0U);
				}
			}
		}
		this.m_effect.Update();
	}

	// Token: 0x06000212 RID: 530 RVA: 0x0001A448 File Offset: 0x00018848
	public override void Update()
	{
		base.Update();
		if (this.m_minigame.m_editing)
		{
			TransformS.SetGlobalTransform(this.m_node1TC, this.m_node1.m_position + Vector3.forward * this.m_depth, this.m_top.rotation.eulerAngles);
			TransformS.SetGlobalTransform(this.m_node2TC, this.m_node2.m_position + Vector3.forward * this.m_depth, this.m_bottom.rotation.eulerAngles);
			TransformS.SetGlobalTransform(this.m_triggerTC, this.m_trigger.m_position + Vector3.forward * this.m_depth, this.m_trigger.m_rotation);
			this.m_top.position = this.m_node1TC.transform.position;
			this.m_bottom.position = this.m_node2TC.transform.position;
			this.m_base.position = this.m_triggerTC.transform.position + Vector3.forward * this.m_depth * 0.5f;
			this.m_base.rotation = Quaternion.Euler(this.m_trigger.m_rotation);
			Vector2 vector = this.m_node1.m_position - this.m_node2.m_position;
			float magnitude = vector.magnitude;
			if (magnitude != this.m_length)
			{
				this.m_length = magnitude;
				float num = Mathf.Atan2(vector.y, vector.x) * 57.29578f;
				base.m_graphElement.m_position = this.m_node2.m_position + vector * 0.5f;
				base.m_graphElement.m_rotation = Vector3.forward * num;
				TransformS.SetGlobalTransform(base.m_graphElement.m_TC, base.m_graphElement.m_position, base.m_graphElement.m_rotation);
			}
		}
	}

	// Token: 0x06000213 RID: 531 RVA: 0x0001A668 File Offset: 0x00018A68
	public void InteractWithIntersectingObjects(bool _kill)
	{
		if (_kill)
		{
			ucpPointQueryInfo[] array = new ucpPointQueryInfo[100];
			ChipmunkProWrapper.ucpSpaceShapeQuery(this.m_fieldCMB.shapes[0].shapePtr, array, 100);
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i].shape == IntPtr.Zero)
				{
					break;
				}
				IntPtr intPtr = ChipmunkProWrapper.ucpShapeGetBody(array[i].shape);
				int j = ChipmunkProS.m_bodies.m_array.Length - 1;
				while (j >= 0)
				{
					if (intPtr == ChipmunkProS.m_bodies.m_array[j].body && ChipmunkProWrapper.ucpBodyGetType(ChipmunkProS.m_bodies.m_array[j].body) == ucpBodyType.DYNAMIC)
					{
						ChipmunkBodyC chipmunkBodyC = ChipmunkProS.m_bodies.m_array[j];
						UnitC unitC = chipmunkBodyC.customComponent as UnitC;
						if (unitC == null || unitC.m_unit == null || (unitC.m_unit != null && unitC.m_unit is Goal))
						{
							break;
						}
						unitC.m_unit.EmergencyKill();
						break;
					}
					else
					{
						j--;
					}
				}
			}
		}
		else
		{
			for (int k = 0; k < this.m_contacts.Count; k++)
			{
				ChipmunkProWrapper.ucpBodyActivate(this.m_contacts[k].m_cmb.body);
			}
		}
	}

	// Token: 0x06000214 RID: 532 RVA: 0x0001A7E4 File Offset: 0x00018BE4
	public virtual void TriggerCollisionHandler(ucpCollisionPair _pair, ucpCollisionPhase _phase)
	{
		ChipmunkBodyC chipmunkBodyC = ChipmunkProS.m_bodies.m_array[_pair.ucpComponentIndexB];
		UnitC unitC = chipmunkBodyC.customComponent as UnitC;
		if (unitC == null || unitC.m_unit == null)
		{
			return;
		}
		if (_phase == ucpCollisionPhase.Begin)
		{
			if (this.m_alienContactCount == 0 && this.m_cooldownOver < Main.m_resettingGameTime)
			{
				this.m_cooldownOver = Main.m_resettingGameTime + 0.5f;
				this.m_enabled = !this.m_enabled;
				this.m_effect.ToggleForceField(this.m_enabled);
				for (int i = 0; i < this.m_fieldCMB.shapes.Count; i++)
				{
					ChipmunkProWrapper.ucpShapeSetLayers(this.m_fieldCMB.shapes[i].shapePtr, (!this.m_enabled) ? 0U : this.m_fieldCMB.shapes[i].layerMask);
				}
				this.InteractWithIntersectingObjects(this.m_enabled);
				Vector2 vector = ChipmunkProWrapper.ucpBodyGetPos(this.m_triggerCMB.body);
				if (this.m_enabled)
				{
					SoundS.PlaySingleShot("/Ingame/Units/ForceFieldOn", vector, 1f);
				}
				else
				{
					SoundS.PlaySingleShot("/Ingame/Units/ForceFieldOff", vector, 1f);
				}
			}
			this.m_alienContactCount++;
		}
		else if (_phase == ucpCollisionPhase.Separate)
		{
			this.m_alienContactCount--;
		}
	}

	// Token: 0x06000215 RID: 533 RVA: 0x0001A956 File Offset: 0x00018D56
	public override void Destroy()
	{
		ForceField.m_reserverdColors.Remove(this.m_color);
		base.Destroy();
	}

	// Token: 0x040001FF RID: 511
	protected GameObject m_mainPrefab;

	// Token: 0x04000200 RID: 512
	protected GraphNode m_node1;

	// Token: 0x04000201 RID: 513
	protected GraphNode m_node2;

	// Token: 0x04000202 RID: 514
	protected GraphNode m_trigger;

	// Token: 0x04000203 RID: 515
	protected TransformC m_node1TC;

	// Token: 0x04000204 RID: 516
	protected TransformC m_node2TC;

	// Token: 0x04000205 RID: 517
	protected TransformC m_triggerTC;

	// Token: 0x04000206 RID: 518
	protected Transform m_base;

	// Token: 0x04000207 RID: 519
	protected Transform m_top;

	// Token: 0x04000208 RID: 520
	protected Transform m_bottom;

	// Token: 0x04000209 RID: 521
	protected float m_radius;

	// Token: 0x0400020A RID: 522
	protected float m_depth;

	// Token: 0x0400020B RID: 523
	protected ChipmunkBodyC m_triggerCMB;

	// Token: 0x0400020C RID: 524
	protected ChipmunkBodyC m_fieldCMB;

	// Token: 0x0400020D RID: 525
	protected float m_length;

	// Token: 0x0400020E RID: 526
	protected bool m_enabled;

	// Token: 0x0400020F RID: 527
	private EffectForceField m_effect;

	// Token: 0x04000210 RID: 528
	private static List<ForceFieldColor> m_reserverdColors = new List<ForceFieldColor>();

	// Token: 0x04000211 RID: 529
	private ForceFieldColor m_color;

	// Token: 0x04000212 RID: 530
	private int m_alienContactCount;

	// Token: 0x04000213 RID: 531
	private float m_cooldownOver;
}
