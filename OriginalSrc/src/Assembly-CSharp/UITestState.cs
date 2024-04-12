using System;

// Token: 0x020005BF RID: 1471
public class UITestState : BasicState
{
	// Token: 0x06002AEF RID: 10991 RVA: 0x001BC8D8 File Offset: 0x001BACD8
	public override void Enter(IStatedObject _parent)
	{
		this.m_model = new UIModel(this, null);
		UIManager.m_canvas = new UICanvas(null, false, "BaseCanvas", null, string.Empty);
		UIManager.m_canvas.SetMargins(0.01f, RelativeTo.ScreenHeight);
		UIPanel uipanel = new UIPanel(UIManager.m_canvas, "PropertyPanel", new UIComponent(null, false, string.Empty, null, null, string.Empty), new UIComponent(null, false, string.Empty, null, null, string.Empty));
		uipanel.SetHeight(1f, RelativeTo.ParentHeight);
		uipanel.SetWidth(0.382f, RelativeTo.ParentHeight);
		uipanel.SetHorizontalAlign(1f);
		Debug.Log("propertyPanel " + uipanel.m_TC.m_index, null);
		Debug.Log("propertyPanelContainer " + uipanel.m_container.m_TC.m_index, null);
		this.m_symbolSheet = SpriteS.AddSpriteSheet(uipanel.m_camera, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SymbolMat_Material), 1f);
		UIHorizontalList uihorizontalList = new UIHorizontalList(uipanel, "HorizontalArea");
		Debug.Log("horizontal " + uihorizontalList.m_TC.m_index, null);
		uihorizontalList.SetHorizontalAlign(0f);
		uihorizontalList.SetVerticalAlign(1f);
		UIVerticalList uiverticalList = new UIVerticalList(uihorizontalList, "VerticalArea");
		uiverticalList.SetVerticalAlign(1f);
		uiverticalList.SetMargins(0f, 0f, 0.04f, 0.04f, RelativeTo.ParentShortest);
		uiverticalList.SetSpacing(0.04f, RelativeTo.ParentShortest);
		UIManager.m_canvas.Update();
	}

	// Token: 0x06002AF0 RID: 10992 RVA: 0x001BCA61 File Offset: 0x001BAE61
	public override void Execute()
	{
	}

	// Token: 0x06002AF1 RID: 10993 RVA: 0x001BCA63 File Offset: 0x001BAE63
	public override void Exit()
	{
		UIManager.DestroyUI();
		SpriteS.RemoveSpriteSheet(this.m_symbolSheet);
		this.m_symbolSheet = null;
		UIManager.m_canvas = null;
	}

	// Token: 0x04002FFE RID: 12286
	public static int m_healthType = 1;

	// Token: 0x04002FFF RID: 12287
	public static string[] m_healthLabels = new string[] { "No Health", "Hearths", "Health Bar" };

	// Token: 0x04003000 RID: 12288
	public static string[] m_healthShortLabels = new string[] { "None", "Hearths", "Bar" };

	// Token: 0x04003001 RID: 12289
	public static Frame[] m_healthFrameLabels = new Frame[]
	{
		new Frame(0f, 0f, 64f, 64f),
		new Frame(64f, 0f, 64f, 64f),
		new Frame(128f, 0f, 64f, 64f)
	};

	// Token: 0x04003002 RID: 12290
	public static int m_healthAmount = 3;

	// Token: 0x04003003 RID: 12291
	public static float m_speed = 0.5f;

	// Token: 0x04003004 RID: 12292
	public static int m_controller = 0;

	// Token: 0x04003005 RID: 12293
	public static string[] m_controllerLabels = new string[] { "Player 1", "Player 2", "Player3", "Player4", "Computer", "Artificial Intelligence" };

	// Token: 0x04003006 RID: 12294
	public static string[] m_controllerLabelsShort = new string[] { "P1", "P2", "P3", "P4", "CPU", "AI" };

	// Token: 0x04003007 RID: 12295
	public static bool m_CPU_hostile = true;

	// Token: 0x04003008 RID: 12296
	public static string[] m_CPU_hostileLabels = new string[] { "No", "Yes" };

	// Token: 0x04003009 RID: 12297
	public static float m_CPU_skill = 0.35f;

	// Token: 0x0400300A RID: 12298
	public static int m_PLAYER_controlMethod = 0;

	// Token: 0x0400300B RID: 12299
	public static string[] m_controlMethodLabels = new string[] { "Tilt", "1-Button", "2-Button", "Joystick & 1-Button", "Joystick & 2-Button" };

	// Token: 0x0400300C RID: 12300
	public static string[] m_controlMethodLabelsShort = new string[] { "Tilt", "1B", "2B", "J+1B", "J+2B" };

	// Token: 0x0400300D RID: 12301
	public UIModel m_model;

	// Token: 0x0400300E RID: 12302
	private SpriteSheet m_symbolSheet;
}
