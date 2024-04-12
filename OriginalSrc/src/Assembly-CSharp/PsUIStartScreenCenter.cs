using System;
using UnityEngine;

// Token: 0x020003B2 RID: 946
public class PsUIStartScreenCenter : UICanvas
{
	// Token: 0x06001B1E RID: 6942 RVA: 0x0012F988 File Offset: 0x0012DD88
	public PsUIStartScreenCenter(UIComponent _parent)
		: base(_parent, false, string.Empty, null, string.Empty)
	{
		float num = 0.0225f * (float)Screen.height;
		this.m_fadeMaterial = new Material(Shader.Find("WOE/Unlit/ColorUnlitTransparent"));
		this.m_changeState = false;
		this.SetWidth(1f, RelativeTo.ScreenWidth);
		this.SetHeight(1f, RelativeTo.ScreenHeight);
		this.RemoveDrawHandler();
		Vector2[] rect = DebugDraw.GetRect((float)Screen.width, (float)Screen.height, Vector2.zero);
		Color black = Color.black;
		uint num2 = DebugDraw.ColorToUInt(black);
		this.m_bg = PrefabS.CreateFlatPrefabComponentsFromVectorArray(this.m_TC, Vector3.forward * 1f, rect, num2, num2, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), this.m_camera, string.Empty, null);
		black.a = 0f;
		num2 = DebugDraw.ColorToUInt(black);
		this.m_fadeObj = PrefabS.CreateFlatPrefabComponentsFromVectorArray(this.m_TC, Vector3.forward * -1f, rect, num2, num2, this.m_fadeMaterial, this.m_camera, string.Empty, null);
		this.m_fadeObj.p_gameObject.GetComponent<Renderer>().material.color = black;
		this.m_logo = PrefabS.AddComponent(this.m_TC, Vector3.zero, Resources.Load("PlaySomething/FX/Prefabs/TraplightLogo") as GameObject);
		Vector3 vector;
		vector..ctor(90f, 180f, 0f);
		this.m_logo.p_gameObject.transform.localRotation = Quaternion.Euler(vector);
		PrefabS.SetCamera(this.m_logo, this.m_camera);
		this.m_logo.p_gameObject.transform.localScale = new Vector3(num, num, num);
	}

	// Token: 0x06001B1F RID: 6943 RVA: 0x0012FB48 File Offset: 0x0012DF48
	public override void Step()
	{
		if (this.m_changeState)
		{
			Main.m_currentGame.m_currentScene.m_stateMachine.ChangeState(new PsUIBaseState(typeof(PsUIPreloaderCenter), null, null, null, false, InitialPage.Center));
		}
		this.m_waitTicks--;
		if (this.m_waitTicks <= 0)
		{
			this.m_waitTicks = 0;
			Color black = Color.black;
			black.a = (float)(30 - this.m_fade) / 30f;
			this.m_fadeObj.p_gameObject.GetComponent<Renderer>().material.color = black;
			this.m_fade--;
			if (this.m_fade <= 0)
			{
				this.m_changeState = true;
			}
		}
		base.Step();
	}

	// Token: 0x06001B20 RID: 6944 RVA: 0x0012FC08 File Offset: 0x0012E008
	public override void Destroy()
	{
		if (this.m_fadeMaterial != null)
		{
			Object.Destroy(this.m_fadeMaterial);
		}
		this.m_fadeMaterial = null;
		base.Destroy();
	}

	// Token: 0x04001D96 RID: 7574
	private PrefabC m_logo;

	// Token: 0x04001D97 RID: 7575
	private int m_waitTicks = 210;

	// Token: 0x04001D98 RID: 7576
	private int m_fade = 30;

	// Token: 0x04001D99 RID: 7577
	private PrefabC m_bg;

	// Token: 0x04001D9A RID: 7578
	private PrefabC m_fadeObj;

	// Token: 0x04001D9B RID: 7579
	private Material m_fadeMaterial;

	// Token: 0x04001D9C RID: 7580
	private bool m_changeState;
}
