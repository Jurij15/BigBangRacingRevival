using System;
using UnityEngine;

// Token: 0x020003DE RID: 990
public class UIDrawHandlers
{
	// Token: 0x06001C2A RID: 7210 RVA: 0x0013DCC0 File Offset: 0x0013C0C0
	public static void EditorPopupBackground(UIComponent _c)
	{
		float num = _c.m_actualWidth;
		float num2 = _c.m_actualHeight;
		UIScrollableCanvas uiscrollableCanvas = _c as UIScrollableCanvas;
		if (uiscrollableCanvas != null)
		{
			num = Mathf.Max(num, uiscrollableCanvas.m_contentWidth);
			num2 = Mathf.Max(num2, uiscrollableCanvas.m_contentHeight);
			num *= 2f;
			num2 *= 1.5f;
		}
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] rect = DebugDraw.GetRect(num, num2 * 0.5f, new Vector2(0f, num2 * 0.25f), false);
		Vector2[] rect2 = DebugDraw.GetRect(num, num2 * 0.5f, new Vector2(0f, -num2 * 0.25f), false);
		uint num3 = DebugDraw.ColorToUInt(DebugDraw.GetColor(26f, 6f, 45f, 120f));
		uint num4 = DebugDraw.ColorToUInt(DebugDraw.GetColor(26f, 6f, 45f, 20f));
		uint num5 = DebugDraw.ColorToUInt(DebugDraw.GetColor(26f, 6f, 45f, 120f));
		Camera camera = CameraS.m_uiCamera;
		if (_c.m_camera != null)
		{
			camera = _c.m_camera;
		}
		else if (_c.m_parent != null)
		{
			camera = _c.m_parent.m_camera;
		}
		PrefabS.CreateFlatPrefabComponentsFromVectorArray(_c.m_TC, Vector3.forward * (_c.m_TC.transform.position.z + 10f), rect, num4, num5, ResourceManager.GetMaterial(RESOURCE.MenuPopupBackgroundMat_Material), camera, string.Empty, null);
		PrefabS.CreateFlatPrefabComponentsFromVectorArray(_c.m_TC, Vector3.forward * (_c.m_TC.transform.position.z + 10f), rect2, num3, num4, ResourceManager.GetMaterial(RESOURCE.MenuPopupBackgroundMat_Material), camera, string.Empty, null);
	}

	// Token: 0x06001C2B RID: 7211 RVA: 0x0013DE9C File Offset: 0x0013C29C
	public static void MenuPopupBackground(UIComponent _c)
	{
		float num = _c.m_actualWidth;
		float num2 = _c.m_actualHeight;
		UIScrollableCanvas uiscrollableCanvas = _c as UIScrollableCanvas;
		if (uiscrollableCanvas != null)
		{
			num = Mathf.Max(num, uiscrollableCanvas.m_contentWidth);
			num2 = Mathf.Max(num2, uiscrollableCanvas.m_contentHeight);
			num *= 1.5f;
			num2 *= 1.5f;
		}
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] rect = DebugDraw.GetRect(num, num2 * 0.5f, new Vector2(0f, num2 * 0.25f), false);
		Vector2[] rect2 = DebugDraw.GetRect(num, num2 * 0.5f, new Vector2(0f, -num2 * 0.25f), false);
		uint num3 = DebugDraw.ColorToUInt(DebugDraw.GetColor(0f, 0f, 0f, 200f));
		uint num4 = DebugDraw.ColorToUInt(DebugDraw.GetColor(0f, 0f, 0f, 200f));
		uint num5 = DebugDraw.ColorToUInt(DebugDraw.GetColor(0f, 0f, 0f, 200f));
		Camera camera = CameraS.m_uiCamera;
		if (_c.m_camera != null)
		{
			camera = _c.m_camera;
		}
		else if (_c.m_parent != null)
		{
			camera = _c.m_parent.m_camera;
		}
		PrefabS.CreateFlatPrefabComponentsFromVectorArray(_c.m_TC, Vector3.forward * (_c.m_TC.transform.position.z + 10f), rect, num4, num5, ResourceManager.GetMaterial(RESOURCE.MenuPopupBackgroundMat_Material), camera, string.Empty, null);
		PrefabS.CreateFlatPrefabComponentsFromVectorArray(_c.m_TC, Vector3.forward * (_c.m_TC.transform.position.z + 10f), rect2, num3, num4, ResourceManager.GetMaterial(RESOURCE.MenuPopupBackgroundMat_Material), camera, string.Empty, null);
	}

	// Token: 0x06001C2C RID: 7212 RVA: 0x0013E078 File Offset: 0x0013C478
	public static void EditorPopupContentArea(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] array = new Vector2[]
		{
			new Vector2(_c.m_actualWidth * -0.5f, _c.m_actualHeight * 0.5f),
			new Vector2(_c.m_actualWidth * -0.191f, _c.m_actualHeight * 0.5f),
			new Vector2(_c.m_actualWidth * 0.5f, _c.m_actualHeight * 0.5f),
			new Vector2(_c.m_actualWidth * 0.5f, _c.m_actualHeight * 0.191f),
			new Vector2(_c.m_actualWidth * 0.5f, _c.m_actualHeight * -0.5f),
			new Vector2(_c.m_actualWidth * 0.191f, _c.m_actualHeight * -0.5f),
			new Vector2(_c.m_actualWidth * -0.5f, _c.m_actualHeight * -0.5f),
			new Vector2(_c.m_actualWidth * -0.5f, _c.m_actualHeight * -0.191f)
		};
		DebugDraw.AddRandom(array, _c.m_actualHeight * 0.025f);
		Color color = DebugDraw.GetColor(0f, 60f, 109f);
		uint num = DebugDraw.ColorToUInt(color);
		Camera camera = CameraS.m_uiCamera;
		if (_c.m_parent != null)
		{
			camera = _c.m_parent.m_camera;
		}
		PrefabS.CreateFlatPrefabComponentsFromVectorArray(_c.m_TC, Vector3.zero, array, num, num, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), camera, string.Empty, null);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * -0.5f, array, 4f, color, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line4Mat_Material), camera, Position.Center, true);
		Color color2 = DebugDraw.GetColor(0f, 0f, 0f, 100f);
		uint num2 = DebugDraw.ColorToUInt(color2);
		PrefabS.CreateFlatPrefabComponentsFromVectorArray(_c.m_TC, new Vector3(0.01f * (float)Screen.height, -0.01f * (float)Screen.height, 5f), array, num2, num2, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), camera, string.Empty, null);
	}

	// Token: 0x06001C2D RID: 7213 RVA: 0x0013E2EC File Offset: 0x0013C6EC
	public static void ScrollableList(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] rect = DebugDraw.GetRect(_c.m_actualWidth * 0.95f, _c.m_actualHeight, Vector2.zero, false);
		Color color = DebugDraw.GetColor(0f, 24f, 71f);
		uint num = DebugDraw.ColorToUInt(color);
		Camera camera = CameraS.m_uiCamera;
		if (_c.m_parent != null)
		{
			camera = _c.m_parent.m_camera;
		}
		PrefabS.CreateFlatPrefabComponentsFromVectorArray(_c.m_TC, Vector3.zero, rect, num, num, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), camera, string.Empty, null);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * -0.5f, rect, 4f, color, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line4Mat_Material), camera, Position.Center, true);
	}

	// Token: 0x06001C2E RID: 7214 RVA: 0x0013E3B8 File Offset: 0x0013C7B8
	public static void LeftArrow(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] array = new Vector2[]
		{
			new Vector2(_c.m_actualWidth * -0.35f, _c.m_actualHeight * 0.5f),
			new Vector2(_c.m_actualWidth * 0.5f, _c.m_actualHeight * 0.5f),
			new Vector2(_c.m_actualWidth * 0.5f, _c.m_actualHeight * -0.5f),
			new Vector2(_c.m_actualWidth * -0.35f, _c.m_actualHeight * -0.5f),
			new Vector2(_c.m_actualWidth * -0.5f, _c.m_actualHeight * 0f)
		};
		DebugDraw.AddRandom(array, _c.m_actualHeight * 0.05f);
		Vector2[] array2 = new Vector2[]
		{
			new Vector2(_c.m_actualWidth * -0.325f, _c.m_actualHeight * 0.3f),
			new Vector2(_c.m_actualWidth * -0.325f, _c.m_actualHeight * -0.3f),
			new Vector2(_c.m_actualWidth * -0.44f, _c.m_actualHeight * 0f)
		};
		Color color = DebugDraw.GetColor(0f, 60f, 109f);
		uint num = DebugDraw.ColorToUInt(color);
		Color color2 = DebugDraw.GetColor(183f, 241f, 10f);
		uint num2 = DebugDraw.ColorToUInt(color2);
		Camera camera = CameraS.m_uiCamera;
		if (_c.m_parent != null)
		{
			camera = _c.m_parent.m_camera;
		}
		PrefabS.CreateFlatPrefabComponentsFromVectorArray(_c.m_TC, Vector3.zero, array, num, num, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), camera, string.Empty, null);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * -0.5f, array, 4f, color, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line4Mat_Material), camera, Position.Center, true);
		PrefabS.CreateFlatPrefabComponentsFromVectorArray(_c.m_TC, Vector3.forward * -1f, array2, num2, num2, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), camera, string.Empty, null);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * -1.5f, array2, 4f, color2, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line4Mat_Material), camera, Position.Center, true);
	}

	// Token: 0x06001C2F RID: 7215 RVA: 0x0013E654 File Offset: 0x0013CA54
	public static void RightArrow(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] array = new Vector2[]
		{
			new Vector2(_c.m_actualWidth * -0.5f, _c.m_actualHeight * 0.5f),
			new Vector2(_c.m_actualWidth * 0.35f, _c.m_actualHeight * 0.5f),
			new Vector2(_c.m_actualWidth * 0.5f, _c.m_actualHeight * 0f),
			new Vector2(_c.m_actualWidth * 0.35f, _c.m_actualHeight * -0.5f),
			new Vector2(_c.m_actualWidth * -0.5f, _c.m_actualHeight * -0.5f)
		};
		DebugDraw.AddRandom(array, _c.m_actualHeight * 0.05f);
		Vector2[] array2 = new Vector2[]
		{
			new Vector2(_c.m_actualWidth * 0.325f, _c.m_actualHeight * 0.3f),
			new Vector2(_c.m_actualWidth * 0.325f, _c.m_actualHeight * -0.3f),
			new Vector2(_c.m_actualWidth * 0.44f, _c.m_actualHeight * 0f)
		};
		Color color = DebugDraw.GetColor(0f, 60f, 109f);
		uint num = DebugDraw.ColorToUInt(color);
		Color color2 = DebugDraw.GetColor(183f, 241f, 10f);
		uint num2 = DebugDraw.ColorToUInt(color2);
		Camera camera = CameraS.m_uiCamera;
		if (_c.m_parent != null)
		{
			camera = _c.m_parent.m_camera;
		}
		PrefabS.CreateFlatPrefabComponentsFromVectorArray(_c.m_TC, Vector3.zero, array, num, num, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), camera, string.Empty, null);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * -0.5f, array, 4f, color, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line4Mat_Material), camera, Position.Center, true);
		PrefabS.CreateFlatPrefabComponentsFromVectorArray(_c.m_TC, Vector3.forward * -1f, array2, num2, num2, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), camera, string.Empty, null);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * -1.5f, array2, 4f, color2, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line4Mat_Material), camera, Position.Center, true);
	}

	// Token: 0x06001C30 RID: 7216 RVA: 0x0013E8F0 File Offset: 0x0013CCF0
	public static void Textfield(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] rect = DebugDraw.GetRect(_c.m_actualWidth, _c.m_actualHeight, Vector2.zero, false);
		Color color = DebugDraw.GetColor(255f, 255f, 255f);
		uint num = DebugDraw.ColorToUInt(color);
		Camera camera = CameraS.m_uiCamera;
		if (_c.m_parent != null)
		{
			camera = _c.m_parent.m_camera;
		}
		PrefabS.CreateFlatPrefabComponentsFromVectorArray(_c.m_TC, Vector3.zero, rect, num, num, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), camera, string.Empty, null);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * -0.5f, rect, 4f, color, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line4Mat_Material), camera, Position.Center, true);
	}

	// Token: 0x06001C31 RID: 7217 RVA: 0x0013E9B4 File Offset: 0x0013CDB4
	public static void TextfieldDark(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] rect = DebugDraw.GetRect(_c.m_actualWidth, _c.m_actualHeight, Vector2.zero, false);
		Color color = DebugDraw.HexToColor("#22618a");
		uint num = DebugDraw.ColorToUInt(color);
		Camera camera = CameraS.m_uiCamera;
		if (_c.m_parent != null)
		{
			camera = _c.m_parent.m_camera;
		}
		PrefabS.CreateFlatPrefabComponentsFromVectorArray(_c.m_TC, Vector3.zero, rect, num, num, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), camera, string.Empty, null);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * -0.5f, rect, 4f, color, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line4Mat_Material), camera, Position.Center, true);
	}

	// Token: 0x06001C32 RID: 7218 RVA: 0x0013EA70 File Offset: 0x0013CE70
	public static void TextfieldOutlined(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] rect = DebugDraw.GetRect(_c.m_actualWidth, _c.m_actualHeight, Vector2.zero, false);
		Color color = DebugDraw.HexToColor("#01101B");
		Color color2 = DebugDraw.HexToColor("#11273E");
		uint num = DebugDraw.ColorToUInt(color);
		uint num2 = DebugDraw.ColorToUInt(color2);
		Camera camera = CameraS.m_uiCamera;
		if (_c.m_parent != null)
		{
			camera = _c.m_parent.m_camera;
		}
		PrefabS.CreateFlatPrefabComponentsFromVectorArray(_c.m_TC, Vector3.zero, rect, num2, num, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), camera, string.Empty, null);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * -0.5f, rect, 4f, Color.white, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line4Mat_Material), camera, Position.Center, true);
	}

	// Token: 0x06001C33 RID: 7219 RVA: 0x0013EB48 File Offset: 0x0013CF48
	public static void DescriptionField(UIComponent _c)
	{
		float num = 0.026f;
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] roundedRect = DebugDraw.GetRoundedRect(_c.m_actualWidth, _c.m_actualHeight, (float)Screen.height * 0.008f, 8, Vector2.zero);
		Vector2[] array = new Vector2[roundedRect.Length + 2];
		Array.Copy(roundedRect, 0, array, 0, 16);
		array[16] = new Vector2(roundedRect[16].x, roundedRect[16].y - (float)Screen.height * num);
		array[17] = new Vector2(roundedRect[16].x - (float)Screen.height * num, roundedRect[16].y - (float)Screen.height * (num / 2f));
		Array.Copy(roundedRect, 16, array, 18, roundedRect.Length - 16);
		Color color = DebugDraw.HexToColor("#fffec6");
		uint num2 = DebugDraw.ColorToUInt(color);
		Camera camera = CameraS.m_uiCamera;
		if (_c.m_parent != null)
		{
			camera = _c.m_parent.m_camera;
		}
		PrefabS.CreateFlatPrefabComponentsFromVectorArray(_c.m_TC, Vector3.zero, array, num2, num2, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), camera, string.Empty, null);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * -0.5f, array, 4f, color, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line4Mat_Material), camera, Position.Center, true);
	}

	// Token: 0x06001C34 RID: 7220 RVA: 0x0013ECBC File Offset: 0x0013D0BC
	public static void InfoArea(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] roundedRect = DebugDraw.GetRoundedRect(_c.m_actualWidth, _c.m_actualHeight, (float)Screen.height * 0.01f, 6, Vector2.zero);
		Color color;
		color..ctor(0f, 0f, 0f, 0.5f);
		uint num = DebugDraw.ColorToUInt(color);
		color.a = 0f;
		uint num2 = DebugDraw.ColorToUInt(color);
		Camera camera = CameraS.m_uiCamera;
		if (_c.m_parent != null)
		{
			camera = _c.m_parent.m_camera;
		}
		PrefabS.CreateFlatPrefabComponentsFromVectorArray(_c.m_TC, Vector3.zero, roundedRect, num2, num, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), camera, string.Empty, null);
	}

	// Token: 0x06001C35 RID: 7221 RVA: 0x0013ED78 File Offset: 0x0013D178
	public static void PositiveButton(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] rect = DebugDraw.GetRect(_c.m_actualWidth, _c.m_actualHeight, Vector2.zero, false);
		DebugDraw.AddRandom(rect, _c.m_actualHeight * 0.05f);
		Color color = DebugDraw.GetColor(98f, 216f, 15f);
		uint num = DebugDraw.ColorToUInt(color);
		Camera camera = CameraS.m_uiCamera;
		if (_c.m_parent != null)
		{
			camera = _c.m_parent.m_camera;
		}
		PrefabS.CreateFlatPrefabComponentsFromVectorArray(_c.m_TC, Vector3.zero, rect, num, num, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), camera, string.Empty, null);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * -0.5f, rect, 4f, color, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line4Mat_Material), camera, Position.Center, true);
		Color color2 = DebugDraw.GetColor(0f, 0f, 0f, 100f);
		uint num2 = DebugDraw.ColorToUInt(color2);
		PrefabS.CreateFlatPrefabComponentsFromVectorArray(_c.m_TC, new Vector3(0.01f * (float)Screen.height, -0.01f * (float)Screen.height), rect, num2, num2, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), camera, string.Empty, null);
	}

	// Token: 0x06001C36 RID: 7222 RVA: 0x0013EEB0 File Offset: 0x0013D2B0
	public static void NegativeButton(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] rect = DebugDraw.GetRect(_c.m_actualWidth, _c.m_actualHeight, Vector2.zero, false);
		DebugDraw.AddRandom(rect, _c.m_actualHeight * 0.05f);
		Color color = DebugDraw.GetColor(234f, 70f, 49f);
		uint num = DebugDraw.ColorToUInt(color);
		Camera camera = CameraS.m_uiCamera;
		if (_c.m_parent != null)
		{
			camera = _c.m_parent.m_camera;
		}
		PrefabS.CreateFlatPrefabComponentsFromVectorArray(_c.m_TC, Vector3.zero, rect, num, num, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), camera, string.Empty, null);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * -0.5f, rect, 4f, color, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line4Mat_Material), camera, Position.Center, true);
		Color color2 = DebugDraw.GetColor(0f, 0f, 0f, 100f);
		uint num2 = DebugDraw.ColorToUInt(color2);
		PrefabS.CreateFlatPrefabComponentsFromVectorArray(_c.m_TC, new Vector3(0.01f * (float)Screen.height, -0.01f * (float)Screen.height), rect, num2, num2, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), camera, string.Empty, null);
	}

	// Token: 0x06001C37 RID: 7223 RVA: 0x0013EFE8 File Offset: 0x0013D3E8
	public static void NeutralButton(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] rect = DebugDraw.GetRect(_c.m_actualWidth, _c.m_actualHeight, Vector2.zero, false);
		DebugDraw.AddRandom(rect, _c.m_actualHeight * 0.05f);
		Color color = DebugDraw.GetColor(220f, 220f, 220f);
		uint num = DebugDraw.ColorToUInt(color);
		Camera camera = CameraS.m_uiCamera;
		if (_c.m_parent != null)
		{
			camera = _c.m_parent.m_camera;
		}
		PrefabS.CreateFlatPrefabComponentsFromVectorArray(_c.m_TC, Vector3.zero, rect, num, num, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), camera, string.Empty, null);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * -0.5f, rect, 4f, color, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line4Mat_Material), camera, Position.Center, true);
		Color color2 = DebugDraw.GetColor(0f, 0f, 0f, 100f);
		uint num2 = DebugDraw.ColorToUInt(color2);
		PrefabS.CreateFlatPrefabComponentsFromVectorArray(_c.m_TC, new Vector3(0.01f * (float)Screen.height, -0.01f * (float)Screen.height), rect, num2, num2, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), camera, string.Empty, null);
	}

	// Token: 0x06001C38 RID: 7224 RVA: 0x0013F120 File Offset: 0x0013D520
	public static void FacebookButton(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] rect = DebugDraw.GetRect(_c.m_actualWidth, _c.m_actualHeight, Vector2.zero, false);
		DebugDraw.AddRandom(rect, _c.m_actualHeight * 0.05f);
		Color color = DebugDraw.GetColor(59f, 87f, 157f);
		uint num = DebugDraw.ColorToUInt(color);
		Camera camera = CameraS.m_uiCamera;
		if (_c.m_parent != null)
		{
			camera = _c.m_parent.m_camera;
		}
		PrefabS.CreateFlatPrefabComponentsFromVectorArray(_c.m_TC, Vector3.zero, rect, num, num, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), camera, string.Empty, null);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * -0.5f, rect, 4f, color, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line4Mat_Material), camera, Position.Center, true);
		Color color2 = DebugDraw.GetColor(0f, 0f, 0f, 100f);
		uint num2 = DebugDraw.ColorToUInt(color2);
		PrefabS.CreateFlatPrefabComponentsFromVectorArray(_c.m_TC, new Vector3(0.01f * (float)Screen.height, -0.01f * (float)Screen.height, 1f), rect, num2, num2, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), camera, string.Empty, null);
	}

	// Token: 0x06001C39 RID: 7225 RVA: 0x0013F260 File Offset: 0x0013D660
	public static void SelectorCancelButton(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] array = new Vector2[]
		{
			new Vector2(_c.m_actualWidth * -0.5f + _c.m_actualHeight * 0.318f * 0.5f, _c.m_actualHeight * 0.5f),
			new Vector2(_c.m_actualWidth * 0.5f + _c.m_actualHeight * 0.318f * 0.5f, _c.m_actualHeight * 0.5f),
			new Vector2(_c.m_actualWidth * 0.5f - _c.m_actualHeight * 0.318f * 0.5f, 0f),
			new Vector2(_c.m_actualWidth * 0.5f + _c.m_actualHeight * 0.318f * 0.5f, _c.m_actualHeight * -0.5f),
			new Vector2(_c.m_actualWidth * -0.5f + _c.m_actualHeight * 0.318f * 0.5f, _c.m_actualHeight * -0.5f),
			new Vector2(_c.m_actualWidth * -0.5f - _c.m_actualHeight * 0.318f * 0.5f, 0f)
		};
		Color color = DebugDraw.GetColor(234f, 70f, 49f);
		uint num = DebugDraw.ColorToUInt(color);
		Camera camera = CameraS.m_uiCamera;
		if (_c.m_parent != null)
		{
			camera = _c.m_parent.m_camera;
		}
		Vector3 vector = Vector3.zero;
		if (_c.m_highlight)
		{
			vector = Vector3.up * -0.02f * (float)Screen.height;
			(_c as UITextButton).m_tmc.m_go.transform.localPosition = vector;
		}
		else
		{
			(_c as UITextButton).m_tmc.m_go.transform.localPosition = Vector3.zero;
		}
		PrefabS.CreateFlatPrefabComponentsFromVectorArray(_c.m_TC, Vector3.zero + vector, array, num, num, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), camera, string.Empty, null);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * 1f + vector, array, 4f, color, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line4Mat_Material), camera, Position.Center, true);
		if (!_c.m_highlight)
		{
			Color color2 = DebugDraw.GetColor(43f, 60f, 56f);
			uint num2 = DebugDraw.ColorToUInt(color2);
			PrefabS.CreateFlatPrefabComponentsFromVectorArray(_c.m_TC, new Vector3(0f, -0.02f * (float)Screen.height, 5f), array, num2, num2, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), camera, string.Empty, null);
			PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, new Vector3(0f, -0.02f * (float)Screen.height, 6f), array, 4f, color2, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line4Mat_Material), camera, Position.Center, true);
		}
	}

	// Token: 0x06001C3A RID: 7226 RVA: 0x0013F58C File Offset: 0x0013D98C
	public static void SelectorCategoryButton(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] array = new Vector2[]
		{
			new Vector2(_c.m_actualWidth * -0.5f + _c.m_actualHeight * 0.318f * 0.5f, _c.m_actualHeight * 0.5f),
			new Vector2(_c.m_actualWidth * 0.5f + _c.m_actualHeight * 0.318f * 0.5f, _c.m_actualHeight * 0.5f),
			new Vector2(_c.m_actualWidth * 0.5f - _c.m_actualHeight * 0.318f * 0.5f, 0f),
			new Vector2(_c.m_actualWidth * 0.5f + _c.m_actualHeight * 0.318f * 0.5f, _c.m_actualHeight * -0.5f),
			new Vector2(_c.m_actualWidth * -0.5f + _c.m_actualHeight * 0.318f * 0.5f, _c.m_actualHeight * -0.5f),
			new Vector2(_c.m_actualWidth * -0.5f - _c.m_actualHeight * 0.318f * 0.5f, 0f)
		};
		Color color = DebugDraw.GetColor(0f, 112f, 178f);
		uint num = DebugDraw.ColorToUInt(color);
		Camera camera = CameraS.m_uiCamera;
		if (_c.m_parent != null)
		{
			camera = _c.m_parent.m_camera;
		}
		Vector3 vector = Vector3.zero;
		if (_c.m_highlight)
		{
			vector = Vector3.up * -0.02f * (float)Screen.height;
			(_c as UITextButton).m_tmc.m_go.transform.localPosition = vector;
		}
		else
		{
			(_c as UITextButton).m_tmc.m_go.transform.localPosition = Vector3.zero;
		}
		PrefabS.CreateFlatPrefabComponentsFromVectorArray(_c.m_TC, Vector3.zero + vector, array, num, num, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), camera, string.Empty, null);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * 1f + vector, array, 4f, color, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line4Mat_Material), camera, Position.Center, true);
		if (!_c.m_highlight)
		{
			Color color2 = DebugDraw.GetColor(43f, 60f, 56f);
			uint num2 = DebugDraw.ColorToUInt(color2);
			PrefabS.CreateFlatPrefabComponentsFromVectorArray(_c.m_TC, new Vector3(0f, -0.02f * (float)Screen.height, 5f), array, num2, num2, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), camera, string.Empty, null);
			PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, new Vector3(0f, -0.02f * (float)Screen.height, 6f), array, 4f, color2, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line4Mat_Material), camera, Position.Center, true);
		}
	}

	// Token: 0x06001C3B RID: 7227 RVA: 0x0013F8B8 File Offset: 0x0013DCB8
	public static void SelectedCategoryButton(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] array = new Vector2[]
		{
			new Vector2(_c.m_actualWidth * -0.5f + _c.m_actualHeight * 0.318f * 0.5f, _c.m_actualHeight * 0.5f),
			new Vector2(_c.m_actualWidth * 0.5f - _c.m_actualHeight * 0.318f * 0.5f, _c.m_actualHeight * 0.5f),
			new Vector2(_c.m_actualWidth * 0.5f + _c.m_actualHeight * 0.318f * 0.5f, 0f),
			new Vector2(_c.m_actualWidth * 0.5f - _c.m_actualHeight * 0.318f * 0.5f, _c.m_actualHeight * -0.5f),
			new Vector2(_c.m_actualWidth * -0.5f + _c.m_actualHeight * 0.318f * 0.5f, _c.m_actualHeight * -0.5f),
			new Vector2(_c.m_actualWidth * -0.5f - _c.m_actualHeight * 0.318f * 0.5f, 0f)
		};
		Color color = DebugDraw.GetColor(0f, 112f, 178f);
		uint num = DebugDraw.ColorToUInt(color);
		Camera camera = CameraS.m_uiCamera;
		if (_c.m_parent != null)
		{
			camera = _c.m_parent.m_camera;
		}
		PrefabS.CreateFlatPrefabComponentsFromVectorArray(_c.m_TC, new Vector3(0f, 0f, 5f), array, num, num, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), camera, string.Empty, null);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, new Vector3(0f, 0f, 6f), array, 4f, color, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line4Mat_Material), camera, Position.Center, true);
	}

	// Token: 0x06001C3C RID: 7228 RVA: 0x0013FAD8 File Offset: 0x0013DED8
	public static void EditorFileMenuContentArea(UIComponent _c)
	{
		SpriteC spriteC = EntityManager.GetComponentByIdentifier(_c.m_TC.p_entity, 1) as SpriteC;
		if (spriteC == null)
		{
			spriteC = SpriteS.AddComponent(_c.m_TC, PsState.m_uiSheet.m_atlas.GetFrame("hud_dropdown_background", null), PsState.m_uiSheet);
			SpriteS.SetDimensions(spriteC, _c.m_actualWidth, _c.m_actualHeight);
			spriteC.m_identifier = 1;
		}
		else
		{
			SpriteS.SetDimensions(spriteC, _c.m_actualWidth, _c.m_actualHeight);
		}
	}

	// Token: 0x06001C3D RID: 7229 RVA: 0x0013FB58 File Offset: 0x0013DF58
	public static void DebugRect(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		DebugDraw.CreateBox(_c.m_camera, _c.m_TC, Vector2.zero, _c.m_actualWidth, _c.m_actualHeight, false);
		DebugDraw.CreateBox(_c.m_camera, _c.m_TC, new Vector2(_c.m_actualMargins.l - _c.m_actualMargins.r, _c.m_actualMargins.b - _c.m_actualMargins.t) * 0.5f, _c.m_actualWidth - _c.m_actualMargins.l - _c.m_actualMargins.r, _c.m_actualHeight - _c.m_actualMargins.b - _c.m_actualMargins.t, false);
		if (_c.m_highlight)
		{
			SpriteS.SetColorByTransformComponent(_c.m_TC, Color.green, false, false);
		}
		else if (_c.m_highlightSecondary)
		{
			SpriteS.SetColorByTransformComponent(_c.m_TC, Color.yellow, false, false);
		}
		else if (_c.m_hit)
		{
			SpriteS.SetColorByTransformComponent(_c.m_TC, Color.cyan, false, false);
		}
		else
		{
			SpriteS.SetColorByTransformComponent(_c.m_TC, Color.white, false, false);
		}
		Camera camera = CameraS.m_uiCamera;
		if (_c.m_parent != null)
		{
			camera = _c.m_parent.m_camera;
		}
	}

	// Token: 0x06001C3E RID: 7230 RVA: 0x0013FCC4 File Offset: 0x0013E0C4
	public static void ButtonGreen(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] rect = DebugDraw.GetRect(_c.m_actualWidth, _c.m_actualHeight, Vector2.zero, false);
		DebugDraw.AddRandom(rect, _c.m_actualHeight * 0.05f);
		Color color = DebugDraw.GetColor(93f, 213f, 14f);
		uint num = DebugDraw.ColorToUInt(color);
		Camera camera = CameraS.m_uiCamera;
		if (_c.m_parent != null)
		{
			camera = _c.m_parent.m_camera;
		}
		PrefabS.CreateFlatPrefabComponentsFromVectorArray(_c.m_TC, Vector3.zero, rect, num, num, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), camera, string.Empty, null);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * -0.5f, rect, 4f, color, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line4Mat_Material), camera, Position.Center, true);
		Color color2 = DebugDraw.GetColor(0f, 0f, 0f, 100f);
		uint num2 = DebugDraw.ColorToUInt(color2);
		PrefabS.CreateFlatPrefabComponentsFromVectorArray(_c.m_TC, new Vector3(0.01f * (float)Screen.height, -0.01f * (float)Screen.height, 1f), rect, num2, num2, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), camera, string.Empty, null);
	}

	// Token: 0x06001C3F RID: 7231 RVA: 0x0013FE04 File Offset: 0x0013E204
	public static void ButtonYellow(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] rect = DebugDraw.GetRect(_c.m_actualWidth, _c.m_actualHeight, Vector2.zero, false);
		DebugDraw.AddRandom(rect, _c.m_actualHeight * 0.05f);
		Color color = DebugDraw.GetColor(237f, 169f, 0f);
		uint num = DebugDraw.ColorToUInt(color);
		Camera camera = CameraS.m_uiCamera;
		if (_c.m_parent != null)
		{
			camera = _c.m_parent.m_camera;
		}
		PrefabS.CreateFlatPrefabComponentsFromVectorArray(_c.m_TC, Vector3.zero, rect, num, num, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), camera, string.Empty, null);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * -0.5f, rect, 4f, color, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line4Mat_Material), camera, Position.Center, true);
		Color color2 = DebugDraw.GetColor(0f, 0f, 0f, 100f);
		uint num2 = DebugDraw.ColorToUInt(color2);
		PrefabS.CreateFlatPrefabComponentsFromVectorArray(_c.m_TC, new Vector3(0.01f * (float)Screen.height, -0.01f * (float)Screen.height, 1f), rect, num2, num2, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), camera, string.Empty, null);
	}

	// Token: 0x06001C40 RID: 7232 RVA: 0x0013FF44 File Offset: 0x0013E344
	public static void ButtonRed(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] rect = DebugDraw.GetRect(_c.m_actualWidth, _c.m_actualHeight, Vector2.zero, false);
		DebugDraw.AddRandom(rect, _c.m_actualHeight * 0.05f);
		Color color = DebugDraw.GetColor(224f, 62f, 18f);
		uint num = DebugDraw.ColorToUInt(color);
		Camera camera = CameraS.m_uiCamera;
		if (_c.m_parent != null)
		{
			camera = _c.m_parent.m_camera;
		}
		PrefabS.CreateFlatPrefabComponentsFromVectorArray(_c.m_TC, Vector3.zero, rect, num, num, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), camera, string.Empty, null);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * -0.5f, rect, 4f, color, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line4Mat_Material), camera, Position.Center, true);
		Color color2 = DebugDraw.GetColor(0f, 0f, 0f, 100f);
		uint num2 = DebugDraw.ColorToUInt(color2);
		PrefabS.CreateFlatPrefabComponentsFromVectorArray(_c.m_TC, new Vector3(0.01f * (float)Screen.height, -0.01f * (float)Screen.height, 1f), rect, num2, num2, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), camera, string.Empty, null);
	}

	// Token: 0x06001C41 RID: 7233 RVA: 0x00140084 File Offset: 0x0013E484
	public static void ButtonBlack(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] rect = DebugDraw.GetRect(_c.m_actualWidth, _c.m_actualHeight, Vector2.zero, false);
		DebugDraw.AddRandom(rect, _c.m_actualHeight * 0.05f);
		Color color = DebugDraw.HexToColor("#22618a") * Color.gray;
		uint num = DebugDraw.ColorToUInt(color);
		Camera camera = CameraS.m_uiCamera;
		if (_c.m_parent != null)
		{
			camera = _c.m_parent.m_camera;
		}
		PrefabS.CreateFlatPrefabComponentsFromVectorArray(_c.m_TC, Vector3.zero, rect, num, num, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), camera, string.Empty, null);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * -0.5f, rect, 4f, color, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line4Mat_Material), camera, Position.Center, true);
		Color color2 = DebugDraw.GetColor(0f, 0f, 0f, 100f);
		uint num2 = DebugDraw.ColorToUInt(color2);
		PrefabS.CreateFlatPrefabComponentsFromVectorArray(_c.m_TC, new Vector3(0.01f * (float)Screen.height, -0.01f * (float)Screen.height, 1f), rect, num2, num2, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), camera, string.Empty, null);
	}

	// Token: 0x06001C42 RID: 7234 RVA: 0x001401C4 File Offset: 0x0013E5C4
	public static void ButtonBlue(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] rect = DebugDraw.GetRect(_c.m_actualWidth, _c.m_actualHeight, Vector2.zero, false);
		DebugDraw.AddRandom(rect, _c.m_actualHeight * 0.05f);
		Color color = DebugDraw.GetColor(57f, 201f, 255f);
		uint num = DebugDraw.ColorToUInt(color);
		Camera camera = CameraS.m_uiCamera;
		if (_c.m_parent != null)
		{
			camera = _c.m_parent.m_camera;
		}
		PrefabS.CreateFlatPrefabComponentsFromVectorArray(_c.m_TC, Vector3.zero, rect, num, num, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), camera, string.Empty, null);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * -0.5f, rect, 4f, color, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line4Mat_Material), camera, Position.Center, true);
		Color color2 = DebugDraw.GetColor(0f, 0f, 0f, 100f);
		uint num2 = DebugDraw.ColorToUInt(color2);
		PrefabS.CreateFlatPrefabComponentsFromVectorArray(_c.m_TC, new Vector3(0.01f * (float)Screen.height, -0.01f * (float)Screen.height, 1f), rect, num2, num2, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), camera, string.Empty, null);
	}
}
