using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000081 RID: 129
public class Teleport : TeleportBase
{
	// Token: 0x0600028B RID: 651 RVA: 0x00020FAC File Offset: 0x0001F3AC
	public Teleport(GraphElement _graphElement)
		: base(_graphElement, 50f, "TeleportPrefab", 60f)
	{
		this.m_length = 0f;
		if (Teleport.m_colors == null)
		{
			this.InitializeColors();
		}
		this.UpdateIndices();
		bool flag = this.ColorExists(base.m_graphElement.m_storedRotation);
		if (base.m_graphElement.m_storedRotation == Vector3.zero || !flag)
		{
			this.m_index = 0;
			while (Teleport.m_reservedIndices.Contains(this.m_index))
			{
				this.m_index++;
			}
			base.m_graphElement.m_storedRotation = new Vector3((float)Teleport.m_colors[this.m_index].r, (float)Teleport.m_colors[this.m_index].g, (float)Teleport.m_colors[this.m_index].b);
		}
		else
		{
			this.m_index = this.GetIndex(base.m_graphElement.m_storedRotation);
		}
		Teleport.m_reservedIndices.Add(this.m_index);
		Teleport.m_teleportObjects.Add(this);
		this.AddProperties(this.m_portal1.p_gameObject, this.m_portal2.p_gameObject);
		this.CreateConnection();
		this.SetCameras();
		this.m_initialRenderTimer = 2;
	}

	// Token: 0x0600028C RID: 652 RVA: 0x00021128 File Offset: 0x0001F528
	public void SetCameras()
	{
		GameObject gameObject = new GameObject("TeleportCamera1");
		GameObject gameObject2 = new GameObject("TeleportCamera2");
		gameObject.transform.parent = this.m_portal1.p_gameObject.transform;
		gameObject2.transform.parent = this.m_portal2.p_gameObject.transform;
		gameObject.transform.localPosition = new Vector3(0f, 0f, -750f - this.m_depth);
		gameObject2.transform.localPosition = new Vector3(0f, 0f, -750f - this.m_depth);
		this.m_camera1 = gameObject.AddComponent<Camera>();
		this.m_camera2 = gameObject2.AddComponent<Camera>();
		int num = 768;
		this.m_camera1.cullingMask = num;
		this.m_camera2.cullingMask = num;
		this.m_camera1.enabled = false;
		this.m_camera2.enabled = false;
		this.m_renderTexture1 = new RenderTexture(256, 256, 24, 4);
		this.m_renderTexture2 = new RenderTexture(256, 256, 24, 4);
		this.m_camera1.targetTexture = this.m_renderTexture1;
		this.m_camera1.gameObject.layer = CameraS.m_mainCameraLayer;
		this.m_camera1.fieldOfView = CameraS.m_mainCameraFov;
		this.m_camera1.farClipPlane = CameraS.m_mainCameraFarClip;
		this.m_camera1.nearClipPlane = CameraS.m_mainCameraNearClip;
		this.m_camera1.fieldOfView = CameraS.m_mainCameraFov;
		this.m_camera1.depth = -5f;
		this.m_camera1.backgroundColor = Color.black;
		this.m_camera2.targetTexture = this.m_renderTexture2;
		this.m_camera2.gameObject.layer = CameraS.m_mainCameraLayer;
		this.m_camera2.fieldOfView = CameraS.m_mainCameraFov;
		this.m_camera2.farClipPlane = CameraS.m_mainCameraFarClip;
		this.m_camera2.nearClipPlane = CameraS.m_mainCameraNearClip;
		this.m_camera2.fieldOfView = CameraS.m_mainCameraFov;
		this.m_camera2.depth = -5f;
		this.m_camera2.backgroundColor = Color.black;
		this.m_camera1.Render();
		this.m_camera2.Render();
		this.m_materials[0].mainTexture = this.m_camera2.targetTexture;
		this.m_materials[1].mainTexture = this.m_camera1.targetTexture;
		this.m_camera1Ticks = Teleport.m_reservedIndices.IndexOf(this.m_index) * 4;
		this.m_camera2Ticks = this.m_camera1Ticks + 2;
	}

	// Token: 0x0600028D RID: 653 RVA: 0x000213CC File Offset: 0x0001F7CC
	public bool ColorExists(Vector3 _storedColor)
	{
		bool flag = false;
		for (int i = 0; i < Teleport.m_colors.Count; i++)
		{
			Vector3 vector;
			vector..ctor((float)Teleport.m_colors[i].r, (float)Teleport.m_colors[i].g, (float)Teleport.m_colors[i].b);
			if (_storedColor == vector)
			{
				flag = true;
				break;
			}
		}
		return flag;
	}

	// Token: 0x0600028E RID: 654 RVA: 0x00021450 File Offset: 0x0001F850
	public int GetIndex(Vector3 _storedColor)
	{
		int num = 0;
		for (int i = 0; i < Teleport.m_colors.Count; i++)
		{
			Vector3 vector;
			vector..ctor((float)Teleport.m_colors[i].r, (float)Teleport.m_colors[i].g, (float)Teleport.m_colors[i].b);
			if (_storedColor == vector)
			{
				num = i;
				break;
			}
		}
		return num;
	}

	// Token: 0x0600028F RID: 655 RVA: 0x000214D4 File Offset: 0x0001F8D4
	public void InitializeColors()
	{
		Teleport.m_colors = new List<Color32>();
		Teleport.m_colors.Add(new Color32(byte.MaxValue, 90, 0, 240));
		Teleport.m_colors.Add(new Color32(30, 90, 190, 240));
		Teleport.m_colors.Add(new Color32(50, 110, 10, 240));
		Teleport.m_colors.Add(new Color32(130, 60, 45, 240));
		Teleport.m_colors.Add(new Color32(110, 50, 110, 240));
		Teleport.m_backgroundColors = new List<Color32>();
		Teleport.m_backgroundColors.Add(new Color32(246, 232, 121, 149));
		Teleport.m_backgroundColors.Add(new Color32(180, 180, 180, 149));
		Teleport.m_backgroundColors.Add(new Color32(180, 180, 180, 149));
		Teleport.m_backgroundColors.Add(new Color32(180, 180, 180, 149));
		Teleport.m_backgroundColors.Add(new Color32(180, 180, 180, 149));
		Teleport.m_spiralColors = new List<Color32>();
		Teleport.m_spiralColors.Add(new Color32(byte.MaxValue, 203, 92, 22));
		Teleport.m_spiralColors.Add(new Color32(180, 180, 180, 22));
		Teleport.m_spiralColors.Add(new Color32(180, 180, 180, 22));
		Teleport.m_spiralColors.Add(new Color32(180, 180, 180, 22));
		Teleport.m_spiralColors.Add(new Color32(180, 180, 180, 22));
	}

	// Token: 0x06000290 RID: 656 RVA: 0x000216D4 File Offset: 0x0001FAD4
	public void UpdateIndices()
	{
		if (Teleport.m_reservedIndices == null)
		{
			Teleport.m_reservedIndices = new List<int>();
		}
		else
		{
			Teleport.m_reservedIndices.Clear();
		}
		for (int i = 0; i < PsS.m_units.m_aliveCount; i++)
		{
			UnitC unitC = PsS.m_units.m_array[PsS.m_units.m_aliveIndices[i]];
			Teleport teleport = unitC.m_unit as Teleport;
			if (teleport != null && teleport != this)
			{
				Teleport.m_reservedIndices.Add(teleport.m_index);
			}
		}
	}

	// Token: 0x06000291 RID: 657 RVA: 0x00021760 File Offset: 0x0001FB60
	public Vector2[] GetSegmentChainPoints(Vector2 _start, Vector2 _end, float _segmentLength)
	{
		Vector2 vector = _end - _start;
		float magnitude = vector.magnitude;
		int num = Mathf.CeilToInt(magnitude / _segmentLength);
		float num2 = magnitude / (float)num;
		Vector2[] array = new Vector2[num + 1];
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = _start + vector.normalized * num2 * (float)i - this.m_node1.m_position;
		}
		return array;
	}

	// Token: 0x06000292 RID: 658 RVA: 0x000217F0 File Offset: 0x0001FBF0
	public void MapUVs(GGData _data, float _x)
	{
		float num = 0f;
		for (int i = 0; i < _data.m_vertices.Count; i++)
		{
			_data.m_vertices[i].uv = new Vector2(_x, num);
			num += 0.1f;
		}
	}

	// Token: 0x06000293 RID: 659 RVA: 0x00021840 File Offset: 0x0001FC40
	public void CreateConnection()
	{
		Vector3 normalized = (this.m_node2.m_position - this.m_node1.m_position).normalized;
		Vector3 vector = this.m_node1.m_position + normalized * this.m_radius;
		Vector3 vector2 = this.m_node2.m_position - normalized * this.m_radius;
		if ((vector2 - vector).magnitude - this.m_length > 0.1f)
		{
			Vector2[] segmentChainPoints = this.GetSegmentChainPoints(vector, vector2, 10f);
			if (segmentChainPoints.Length > 3)
			{
				GGData ggdata = new GGData(segmentChainPoints);
				ggdata.Expand(5f);
				GGData ggdata2 = ggdata.Copy();
				ggdata2.Expand(-10f);
				this.MapUVs(ggdata2, 0f);
				this.MapUVs(ggdata, 0.5f);
				if (this.m_linePc != null)
				{
					PrefabS.RemoveComponent(this.m_linePc, true);
				}
				Mesh mesh = GeometryGenerator.GenerateBeltMesh(0f, ggdata, ggdata2, false);
				this.m_linePc = PrefabS.CreatePrefabFromMesh(this.m_node1TC, mesh, CameraS.m_mainCamera, this.m_materials[this.m_materials.Count - 1], true, true, false);
			}
			else if (this.m_linePc != null)
			{
				PrefabS.RemoveComponent(this.m_linePc, true);
				this.m_linePc = null;
			}
		}
		this.m_materials[this.m_materials.Count - 1].SetColor("_TintColor", Teleport.m_spiralColors[this.m_index]);
	}

	// Token: 0x06000294 RID: 660 RVA: 0x000219E8 File Offset: 0x0001FDE8
	public void AddProperties(GameObject _portal1, GameObject _portal2)
	{
		this.m_materials.Add(_portal1.transform.Find("TeleportBackground").GetComponent<Renderer>().material);
		this.m_materials.Add(_portal2.transform.Find("TeleportBackground").GetComponent<Renderer>().material);
		this.m_materials.Add(_portal1.transform.Find("TeleportColor").GetComponent<Renderer>().material);
		this.m_materials.Add(_portal1.transform.Find("TeleportOutline").GetComponent<Renderer>().material);
		this.m_materials.Add(ResourceManager.GetMaterial(RESOURCE.TeleportLine_Material));
		_portal2.transform.Find("TeleportColor").GetComponent<Renderer>().sharedMaterial = this.m_materials[2];
		_portal2.transform.Find("TeleportOutline").GetComponent<Renderer>().sharedMaterial = this.m_materials[3];
		this.m_materials[2].color = Teleport.m_colors[this.m_index];
		this.m_materials[3].SetColor("_TintColor", Teleport.m_spiralColors[this.m_index]);
		_portal1.transform.Find("TeleportBackground").localRotation = Quaternion.Euler(new Vector3(0f, 0f, -15f));
		_portal2.transform.Find("TeleportBackground").localRotation = Quaternion.Euler(new Vector3(0f, 0f, -15f));
		this.m_transforms.Add(_portal1.transform.Find("TeleportOutline"));
		this.m_transforms.Add(_portal2.transform.Find("TeleportOutline"));
	}

	// Token: 0x06000295 RID: 661 RVA: 0x00021BD0 File Offset: 0x0001FFD0
	public override void Destroy()
	{
		if (this.m_linePc != null)
		{
			this.m_linePc = null;
		}
		for (int i = 0; i < this.m_materials.Count - 1; i++)
		{
			Object.Destroy(this.m_materials[i]);
		}
		Object.Destroy(this.m_renderTexture1);
		Object.Destroy(this.m_renderTexture2);
		Object.Destroy(this.m_camera1);
		Object.Destroy(this.m_camera2);
		Teleport.m_reservedIndices.Remove(this.m_index);
		Teleport.m_teleportObjects.Remove(this);
		base.Destroy();
	}

	// Token: 0x06000296 RID: 662 RVA: 0x00021C70 File Offset: 0x00020070
	public void UpdateMaterials()
	{
		for (int i = 2; i < this.m_materials.Count; i++)
		{
			if (i < this.m_materials.Count - 1 || (i >= this.m_materials.Count - 1 && this.m_index == Teleport.m_reservedIndices[0]))
			{
				this.m_materials[i].mainTextureOffset += Vector2.up * 0.01f;
				if (this.m_materials[i].mainTextureOffset.y > 1f)
				{
					this.m_materials[i].mainTextureOffset = new Vector2(this.m_materials[i].mainTextureOffset.x, 0f);
				}
			}
		}
	}

	// Token: 0x06000297 RID: 663 RVA: 0x00021D58 File Offset: 0x00020158
	public override void Update()
	{
		base.Update();
		this.m_portal1Visible = this.IsVisible(this, 1);
		this.m_portal2Visible = this.IsVisible(this, 2);
		if (this.m_portal1Visible || this.m_portal2Visible)
		{
			float num = 1f;
			for (int i = 0; i < this.m_transforms.Count; i++)
			{
				if (i % 2 == 0 && i != 0)
				{
					num += 0.5f;
				}
				this.m_transforms[i].rotation *= Quaternion.Euler(-Vector3.forward * 1f * num);
			}
			this.UpdateMaterials();
		}
		if (this.m_minigame.m_editing && base.m_graphElement.m_selected)
		{
			this.CreateConnection();
		}
		else if (this.m_linePc != null)
		{
			PrefabS.RemoveComponent(this.m_linePc, true);
			this.m_linePc = null;
		}
		if (PsState.m_performanceClass == PerformanceClass.FAST)
		{
			this.GetCameraFPS();
			this.RenderCams();
		}
		else
		{
			if (this.m_initialRenderTimer > 0)
			{
				this.m_initialRenderTimer--;
			}
			if (this.m_initialRenderTimer == 0 || Teleport.m_forceRefresh)
			{
				this.m_camera1.Render();
				this.m_camera2.Render();
				this.m_initialRenderTimer = -1;
				Teleport.m_forceRefresh = false;
			}
		}
	}

	// Token: 0x06000298 RID: 664 RVA: 0x00021ED0 File Offset: 0x000202D0
	public void RenderCams()
	{
		if (Teleport.CAMERA_FPS > 0)
		{
			this.m_camera1Ticks++;
			this.m_camera2Ticks++;
			if (this.m_camera1Ticks >= 60 / Teleport.CAMERA_FPS)
			{
				if (this.m_portal2Visible)
				{
					this.m_camera1Ticks = 0;
					this.m_camera1.Render();
				}
				else
				{
					this.m_camera1Ticks = 60;
				}
			}
			if (this.m_camera2Ticks >= 60 / Teleport.CAMERA_FPS)
			{
				if (this.m_portal1Visible)
				{
					this.m_camera2Ticks = 0;
					this.m_camera2.Render();
				}
				else
				{
					this.m_camera2Ticks = 60;
				}
			}
		}
	}

	// Token: 0x06000299 RID: 665 RVA: 0x00021F80 File Offset: 0x00020380
	public bool IsVisible(Teleport _teleport, int _portalNumber)
	{
		int[] array = this.m_renderIndices1;
		if (_portalNumber == 2)
		{
			array = this.m_renderIndices2;
		}
		for (int i = 0; i < array.Length; i++)
		{
			if (_teleport.m_renderers[array[i]].isVisible)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600029A RID: 666 RVA: 0x00021FD4 File Offset: 0x000203D4
	public void GetCameraFPS()
	{
		int num = 0;
		for (int i = 0; i < Teleport.m_teleportObjects.Count; i++)
		{
			if (Teleport.m_teleportObjects[i] != null)
			{
				if (Teleport.m_teleportObjects[i].m_portal1Visible)
				{
					num++;
				}
				if (Teleport.m_teleportObjects[i].m_portal2Visible)
				{
					num++;
				}
			}
		}
		if (num > 0)
		{
			Teleport.CAMERA_FPS = 30 / num;
		}
		else
		{
			Teleport.CAMERA_FPS = 0;
		}
	}

	// Token: 0x0600029B RID: 667 RVA: 0x0002205C File Offset: 0x0002045C
	public override void TeleportCollisionHandler(ucpCollisionPair _pair, ucpCollisionPhase _phase)
	{
		ChipmunkBodyC chipmunkBodyC = ChipmunkProS.m_bodies.m_array[_pair.ucpComponentIndexB];
		UnitC unitC = chipmunkBodyC.customComponent as UnitC;
		ucpBodyType ucpBodyType = ChipmunkProWrapper.ucpBodyGetType(chipmunkBodyC.body);
		if (unitC == null || unitC.m_unit == null || this.m_node1.m_TC == null || this.m_node2.m_TC == null || ucpBodyType != ucpBodyType.DYNAMIC)
		{
			return;
		}
		if (_phase == ucpCollisionPhase.Begin && !unitC.m_unit.m_teleported)
		{
			if (_pair.shapeA == this.m_node1Shape)
			{
				unitC.m_unit.SetAsTeleporting(15, this.m_node1.m_TC.transform, this.m_node2.m_TC.transform, true, false, false, true);
			}
			else if (_pair.shapeA == this.m_node2Shape)
			{
				unitC.m_unit.SetAsTeleporting(15, this.m_node2.m_TC.transform, this.m_node1.m_TC.transform, true, false, false, true);
			}
		}
		else if (_phase == ucpCollisionPhase.Separate && unitC.m_unit.m_teleported)
		{
			ucpPointQueryInfo[] array = new ucpPointQueryInfo[20];
			bool flag = false;
			if ((_pair.shapeA == this.m_node1Shape && unitC.m_unit.m_teleportStartPos == this.m_node2.m_TC.transform.position) || (_pair.shapeA == this.m_node2Shape && unitC.m_unit.m_teleportStartPos == this.m_node1.m_TC.transform.position))
			{
				flag = true;
			}
			if (flag)
			{
				bool flag2 = true;
				ChipmunkProWrapper.ucpSpaceShapeQuery(_pair.shapeA, array, 20);
				List<IComponent> componentsByEntity = EntityManager.GetComponentsByEntity(ComponentType.ChipmunkBody, unitC.p_entity);
				for (int i = 0; i < componentsByEntity.Count; i++)
				{
					bool flag3 = false;
					ChipmunkBodyC chipmunkBodyC2 = componentsByEntity[i] as ChipmunkBodyC;
					for (int j = 0; j < array.Length; j++)
					{
						if (array[j].shape == IntPtr.Zero)
						{
							break;
						}
						IntPtr intPtr = ChipmunkProWrapper.ucpShapeGetBody(array[j].shape);
						if (intPtr == IntPtr.Zero)
						{
							break;
						}
						if (chipmunkBodyC2.body == intPtr)
						{
							flag3 = true;
							break;
						}
					}
					if (flag3)
					{
						flag2 = false;
						break;
					}
				}
				if (flag2)
				{
					unitC.m_unit.m_teleported = false;
				}
			}
		}
	}

	// Token: 0x040002C7 RID: 711
	public static bool m_forceRefresh = false;

	// Token: 0x040002C8 RID: 712
	private List<Material> m_materials = new List<Material>();

	// Token: 0x040002C9 RID: 713
	public int m_index;

	// Token: 0x040002CA RID: 714
	private static List<Color32> m_colors;

	// Token: 0x040002CB RID: 715
	private static List<Color32> m_spiralColors;

	// Token: 0x040002CC RID: 716
	private static List<Color32> m_backgroundColors;

	// Token: 0x040002CD RID: 717
	private static List<int> m_reservedIndices = new List<int>();

	// Token: 0x040002CE RID: 718
	private static List<Teleport> m_teleportObjects = new List<Teleport>();

	// Token: 0x040002CF RID: 719
	protected List<Transform> m_transforms = new List<Transform>();

	// Token: 0x040002D0 RID: 720
	private float m_length;

	// Token: 0x040002D1 RID: 721
	private PrefabC m_linePc;

	// Token: 0x040002D2 RID: 722
	private Camera m_camera1;

	// Token: 0x040002D3 RID: 723
	private Camera m_camera2;

	// Token: 0x040002D4 RID: 724
	private RenderTexture m_renderTexture1;

	// Token: 0x040002D5 RID: 725
	private RenderTexture m_renderTexture2;

	// Token: 0x040002D6 RID: 726
	private static int CAMERA_FPS;

	// Token: 0x040002D7 RID: 727
	private int m_camera1Ticks;

	// Token: 0x040002D8 RID: 728
	private int m_camera2Ticks;

	// Token: 0x040002D9 RID: 729
	private bool m_portal1Visible;

	// Token: 0x040002DA RID: 730
	private bool m_portal2Visible;

	// Token: 0x040002DB RID: 731
	private int m_initialRenderTimer;
}
