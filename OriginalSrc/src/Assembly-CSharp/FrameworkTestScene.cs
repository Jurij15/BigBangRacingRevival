using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020005B7 RID: 1463
public class FrameworkTestScene : IScene, IStatedObject
{
	// Token: 0x06002ABC RID: 10940 RVA: 0x001BAEA8 File Offset: 0x001B92A8
	public FrameworkTestScene(string _sceneName)
	{
		this.m_name = _sceneName;
	}

	// Token: 0x170000ED RID: 237
	// (get) Token: 0x06002ABD RID: 10941 RVA: 0x001BAEB7 File Offset: 0x001B92B7
	// (set) Token: 0x06002ABE RID: 10942 RVA: 0x001BAEBF File Offset: 0x001B92BF
	public string m_name
	{
		get
		{
			return this._name;
		}
		set
		{
			this._name = value;
		}
	}

	// Token: 0x170000EE RID: 238
	// (get) Token: 0x06002ABF RID: 10943 RVA: 0x001BAEC8 File Offset: 0x001B92C8
	// (set) Token: 0x06002AC0 RID: 10944 RVA: 0x001BAED0 File Offset: 0x001B92D0
	public StateMachine m_stateMachine
	{
		get
		{
			return this._stateMachine;
		}
		set
		{
			this._stateMachine = value;
		}
	}

	// Token: 0x170000EF RID: 239
	// (get) Token: 0x06002AC1 RID: 10945 RVA: 0x001BAED9 File Offset: 0x001B92D9
	public bool m_initComplete
	{
		get
		{
			return this._initComplete;
		}
	}

	// Token: 0x06002AC2 RID: 10946 RVA: 0x001BAEE1 File Offset: 0x001B92E1
	public IState GetCurrentState()
	{
		return this.m_stateMachine.GetCurrentState();
	}

	// Token: 0x06002AC3 RID: 10947 RVA: 0x001BAEEE File Offset: 0x001B92EE
	public void Load()
	{
		this.Initialize();
	}

	// Token: 0x06002AC4 RID: 10948 RVA: 0x001BAEF8 File Offset: 0x001B92F8
	public void Initialize()
	{
		FrameworkTestScene.m_currentStateId = 0;
		FrameworkTestScene.m_states = new List<IState>();
		FrameworkTestScene.m_states.Add(FrameworkTestScene.state_touchAreaTest);
		FrameworkTestScene.m_states.Add(FrameworkTestScene.state_uiTest);
		FrameworkTestScene.m_states.Add(FrameworkTestScene.state_ServerTest);
		FrameworkTestScene.m_states.Add(FrameworkTestScene.state_chipmunkTest);
		FrameworkTestScene.m_states.Add(FrameworkTestScene.state_massCreationTest);
		FrameworkTestScene.m_states.Add(FrameworkTestScene.state_spriteSortingTest);
		FrameworkTestScene.m_spriteSheet = SpriteS.AddSpriteSheet(Camera.main, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), 1f);
		this.m_stateMachine = new StateMachine();
		this.m_stateMachine.ChangeState(FrameworkTestScene.m_states[FrameworkTestScene.m_currentStateId]);
		Debug.LogInfo("Framework testscene loaded");
		this._initComplete = true;
	}

	// Token: 0x06002AC5 RID: 10949 RVA: 0x001BAFC3 File Offset: 0x001B93C3
	public void Reset()
	{
		this.Destroy();
		this.Load();
	}

	// Token: 0x06002AC6 RID: 10950 RVA: 0x001BAFD4 File Offset: 0x001B93D4
	public void Update()
	{
		this.mainTicker++;
		if (Input.GetKeyDown(32))
		{
			FrameworkTestScene.m_currentStateId = ToolBox.getRolledValue(FrameworkTestScene.m_currentStateId + 1, 0, FrameworkTestScene.m_states.Count - 1);
			this.m_stateMachine.ChangeState(FrameworkTestScene.m_states[FrameworkTestScene.m_currentStateId]);
		}
		this.m_stateMachine.Update();
		if (this.mainTicker % 10 == 0)
		{
			string text = string.Empty;
			string text2 = text;
			text = string.Concat(new object[]
			{
				text2,
				"Entities: ",
				EntityManager.m_entities.m_aliveCount,
				" (",
				EntityManager.m_entities.m_currentLength,
				")\n"
			});
			text2 = text;
			text = string.Concat(new object[]
			{
				text2,
				"Transforms: ",
				TransformS.m_components.m_aliveCount,
				" (",
				TransformS.m_components.m_currentLength,
				")\n"
			});
			text2 = text;
			text = string.Concat(new object[]
			{
				text2,
				"Chipmunk: ",
				ChipmunkProS.m_bodies.m_aliveCount,
				" (",
				ChipmunkProS.m_bodies.m_currentLength,
				")\n"
			});
			text2 = text;
			text = string.Concat(new object[]
			{
				text2,
				"Prefabs: ",
				PrefabS.m_components.m_aliveCount,
				" (",
				PrefabS.m_components.m_currentLength,
				")\n"
			});
			text2 = text;
			text = string.Concat(new object[]
			{
				text2,
				"Tweens: ",
				TweenS.m_components.m_aliveCount,
				" (",
				TweenS.m_components.m_currentLength,
				")\n"
			});
			text2 = text;
			text = string.Concat(new object[]
			{
				text2,
				"CameraTargets: ",
				CameraS.m_cameraTargetComponents.m_aliveCount,
				" (",
				CameraS.m_cameraTargetComponents.m_currentLength,
				")\n"
			});
			text2 = text;
			text = string.Concat(new object[]
			{
				text2,
				"UI Components: ",
				UIComponent.m_instanceCount,
				"\n"
			});
			text2 = text;
			text = string.Concat(new object[]
			{
				text2,
				"Sprite instances (",
				SpriteC.m_componentCount,
				")\n"
			});
			for (int i = 0; i < SpriteS.m_sheets.m_aliveCount; i++)
			{
				SpriteSheet spriteSheet = SpriteS.m_sheets.m_array[SpriteS.m_sheets.m_aliveIndices[i]];
				text2 = text;
				text = string.Concat(new object[]
				{
					text2,
					"   ",
					spriteSheet.m_index,
					": ",
					spriteSheet.m_components.m_aliveCount,
					"\n"
				});
			}
		}
	}

	// Token: 0x06002AC7 RID: 10951 RVA: 0x001BB302 File Offset: 0x001B9702
	public void Destroy()
	{
		this.m_stateMachine.Destroy();
		EntityManager.RemoveAllEntities();
	}

	// Token: 0x06002AC8 RID: 10952 RVA: 0x001BB314 File Offset: 0x001B9714
	~FrameworkTestScene()
	{
		Debug.Log(this + ": Memory Freed", null);
	}

	// Token: 0x04002FE6 RID: 12262
	private string _name;

	// Token: 0x04002FE7 RID: 12263
	private StateMachine _stateMachine;

	// Token: 0x04002FE8 RID: 12264
	private bool _initComplete;

	// Token: 0x04002FE9 RID: 12265
	public static SpriteSheet m_spriteSheet;

	// Token: 0x04002FEA RID: 12266
	public static IState state_ServerTest = new ServerTestState();

	// Token: 0x04002FEB RID: 12267
	public static IState state_AutoGeometryTest = new AutoGeometryTestState();

	// Token: 0x04002FEC RID: 12268
	public static IState state_uiTest = new UITestState();

	// Token: 0x04002FED RID: 12269
	public static IState state_touchAreaTest = new TouchAreaTestState();

	// Token: 0x04002FEE RID: 12270
	public static IState state_massCreationTest = new MassCreateTestState();

	// Token: 0x04002FEF RID: 12271
	public static IState state_spriteSortingTest = new SpriteSortingTestState();

	// Token: 0x04002FF0 RID: 12272
	public static IState state_cameraTest = new CameraTestState();

	// Token: 0x04002FF1 RID: 12273
	public static IState state_chipmunkTest = new ChipmunkTestState();

	// Token: 0x04002FF2 RID: 12274
	public static List<IState> m_states;

	// Token: 0x04002FF3 RID: 12275
	public static int m_currentStateId;

	// Token: 0x04002FF4 RID: 12276
	private int mainTicker;
}
