using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200002B RID: 43
public class Sensor : Unit
{
	// Token: 0x06000122 RID: 290 RVA: 0x0000CEE0 File Offset: 0x0000B2E0
	public Sensor(GraphElement _graphElement)
		: base(_graphElement, UnitType.Basic)
	{
		if (PsState.m_activeGameLoop != null && !this.m_minigame.m_editing)
		{
			Sensor.<Sensor>c__AnonStorey0 <Sensor>c__AnonStorey = new Sensor.<Sensor>c__AnonStorey0();
			<Sensor>c__AnonStorey.$this = this;
			<Sensor>c__AnonStorey.pauseTutorial = delegate
			{
				if (<Sensor>c__AnonStorey.$this.m_tutorialArrow != null)
				{
					<Sensor>c__AnonStorey.$this.m_tutorialArrow = null;
				}
			};
			<Sensor>c__AnonStorey.continueTutorial = delegate
			{
				if (<Sensor>c__AnonStorey.$this.m_tutorialArrow == null)
				{
					Sensor $this = <Sensor>c__AnonStorey.$this;
					UIComponent button = Controller.GetButton(<Sensor>c__AnonStorey.$this.m_targetButtonName);
					Action pauseTutorial = <Sensor>c__AnonStorey.pauseTutorial;
					$this.m_tutorialArrow = new PsUITutorialArrowUI(button, true, pauseTutorial, 2f, default(Vector3), false);
				}
			};
			<Sensor>c__AnonStorey.groundHit = delegate(Vehicle _v)
			{
				float num = 4f;
				if (_v.m_contactState != ContactState.OnAir || Controller.GetButton(<Sensor>c__AnonStorey.$this.m_targetButtonName) == null || Controller.GetButton(<Sensor>c__AnonStorey.$this.m_targetButtonName).m_TC.p_entity == null)
				{
					if (!<Sensor>c__AnonStorey.$this.m_tutorialFinished)
					{
						<Sensor>c__AnonStorey.$this.m_finishTutorial.Invoke();
					}
				}
				else
				{
					bool isDown = Controller.GetButton(<Sensor>c__AnonStorey.$this.m_targetButtonName).m_isDown;
					float num2 = 0f;
					float num3 = ChipmunkProWrapper.ucpBodyGetAngVel(_v.m_chassisBody.body);
					if (Mathf.Abs(num3) > 0.7f && ((<Sensor>c__AnonStorey.$this.m_flipDirection > 0 && num3 > 0f) || (<Sensor>c__AnonStorey.$this.m_flipDirection < 0 && num3 < 0f)))
					{
						num2 = Mathf.Min(Mathf.Abs(ChipmunkProWrapper.ucpBodyGetAngVel(_v.m_chassisBody.body)) / num, 1f);
						if (isDown)
						{
							num2 = Mathf.Clamp(num2, 0.15f, 1f);
						}
					}
					Main.SetTimeScale(num2);
				}
			};
			<Sensor>c__AnonStorey.landing = delegate(Vehicle _v)
			{
				float num4 = _v.m_chassisBody.TC.transform.rotation.eulerAngles.z;
				if (num4 > 180f)
				{
					num4 -= 360f;
				}
				float num5 = <Sensor>c__AnonStorey.$this.m_landingAngle - num4;
				if (num5 > 0f)
				{
					<Sensor>c__AnonStorey.$this.m_flipDirection = 1;
					<Sensor>c__AnonStorey.$this.m_targetButtonName = "LeftButton1";
				}
				else
				{
					<Sensor>c__AnonStorey.$this.m_flipDirection = -1;
					<Sensor>c__AnonStorey.$this.m_targetButtonName = "LeftButton2";
				}
				if (Mathf.Abs(num5) < <Sensor>c__AnonStorey.$this.m_angleMaxVariance * 1.5f)
				{
					float num6 = 1f;
					if (Mathf.Abs(num5) > 10f)
					{
						num6 = Mathf.Lerp(1f, 0.4f, Mathf.Abs(num5) / (<Sensor>c__AnonStorey.$this.m_angleMaxVariance * 1.5f));
					}
					Main.SetTimeScale(num6);
				}
				if (Mathf.Abs(num5) > <Sensor>c__AnonStorey.$this.m_angleMaxVariance)
				{
					if (!Controller.GetButton(<Sensor>c__AnonStorey.$this.m_targetButtonName).m_isDown)
					{
						<Sensor>c__AnonStorey.continueTutorial.Invoke();
					}
				}
				else if (<Sensor>c__AnonStorey.$this.m_tutorialArrow != null)
				{
					<Sensor>c__AnonStorey.$this.m_tutorialArrow.Destroy();
					<Sensor>c__AnonStorey.$this.m_tutorialArrow.m_callBack.Invoke();
				}
			};
			<Sensor>c__AnonStorey.flip = delegate(string[] _params, int _direction)
			{
				if (_direction > 0)
				{
					<Sensor>c__AnonStorey.$this.m_targetButtonName = "LeftButton1";
				}
				else
				{
					<Sensor>c__AnonStorey.$this.m_targetButtonName = "LeftButton2";
				}
				<Sensor>c__AnonStorey.$this.m_playerVehicle = PsState.m_activeMinigame.m_playerUnit as Vehicle;
				if (!<Sensor>c__AnonStorey.$this.m_tutorialFinished && <Sensor>c__AnonStorey.$this.m_finishTutorial != null)
				{
					<Sensor>c__AnonStorey.$this.m_finishTutorial.Invoke();
				}
				<Sensor>c__AnonStorey.$this.m_tutorialFinished = false;
				Action<Vehicle> forceButtonPress = delegate(Vehicle _v)
				{
					if (!<Sensor>c__AnonStorey.m_tutorialFinished && !Controller.GetButton(<Sensor>c__AnonStorey.m_targetButtonName).m_isDown)
					{
						<Sensor>c__AnonStorey.continueTutorial.Invoke();
					}
				};
				<Sensor>c__AnonStorey.$this.m_finishTutorial = delegate
				{
					if (<Sensor>c__AnonStorey.m_tutorialArrow != null)
					{
						<Sensor>c__AnonStorey.m_tutorialArrow.Destroy();
						<Sensor>c__AnonStorey.m_tutorialArrow = null;
					}
					<Sensor>c__AnonStorey.m_tutorialFinished = true;
					<Sensor>c__AnonStorey.m_finishTutorial = null;
					<Sensor>c__AnonStorey.m_playerVehicle.RemoveAllVehicleListeners();
					<Sensor>c__AnonStorey.m_playerVehicle.d_boostTriggeredDelegate = null;
					PsState.m_activeMinigame.RemoveTimeScale();
				};
				<Sensor>c__AnonStorey.$this.m_playerVehicle.AddVehicleListener(<Sensor>c__AnonStorey.groundHit);
				<Sensor>c__AnonStorey.$this.m_playerVehicle.AddVehicleListener(forceButtonPress);
				<Sensor>c__AnonStorey.$this.m_playerVehicle.d_boostTriggeredDelegate = delegate
				{
					if (!<Sensor>c__AnonStorey.m_tutorialFinished)
					{
						<Sensor>c__AnonStorey.SetLandingAngleParameters(_params);
						<Sensor>c__AnonStorey.m_playerVehicle.RemoveVehicleListener(forceButtonPress);
						<Sensor>c__AnonStorey.m_playerVehicle.AddVehicleListener(<Sensor>c__AnonStorey.landing);
					}
				};
				int dir = _direction;
				<Sensor>c__AnonStorey.$this.m_tutorialArrow = new PsUITutorialArrowUI(Controller.GetButton(<Sensor>c__AnonStorey.$this.m_targetButtonName), true, delegate
				{
					<Sensor>c__AnonStorey.pauseTutorial.Invoke();
					<Sensor>c__AnonStorey.m_flipDirection = dir;
				}, 2f, default(Vector3), false);
				Main.SetTimeScale(0f);
			};
			Sensor.AddListener("backflip", delegate(string[] _params)
			{
				<Sensor>c__AnonStorey.flip.Invoke(_params, 1);
			});
			Sensor.AddListener("frontflip", delegate(string[] _params)
			{
				<Sensor>c__AnonStorey.flip.Invoke(_params, -1);
			});
		}
		base.m_graphElement.m_name = "Sensor";
		base.m_graphElement.m_isCopyable = true;
		base.m_graphElement.m_isRemovable = true;
		base.m_graphElement.m_isFlippable = true;
		base.m_graphElement.m_isRotateable = false;
		this.m_tc = TransformS.AddComponent(this.m_entity, _graphElement.m_name);
		TransformS.SetTransform(this.m_tc, _graphElement.m_position + new Vector3(0f, 0f, 50f), _graphElement.m_rotation);
		this.m_shape = new ucpCircleShape(45f, Vector2.zero, 17895696U, 0f, 0f, 0f, (ucpCollisionType)10, true);
		this.m_body = ChipmunkProS.AddStaticBody(this.m_tc, this.m_shape, null);
		if (!this.m_minigame.m_editing)
		{
			ChipmunkProS.AddCollisionHandler(this.m_body, new CollisionDelegate(this.CollisionHandler), (ucpCollisionType)10, (ucpCollisionType)3, true, false, false);
		}
		else
		{
			Vector2[] circle = DebugDraw.GetCircle(45f, 32, Vector2.zero);
			PrefabS.CreatePathPrefabComponentFromVectorArray(this.m_tc, Vector3.zero, circle, 4f, Color.yellow, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line8Mat_Material), CameraS.m_mainCamera, Position.Center, true);
		}
		this.CreateEditorTouchArea(45f, 45f, null, default(Vector2));
	}

	// Token: 0x06000123 RID: 291 RVA: 0x0000D0F8 File Offset: 0x0000B4F8
	private void SetLandingAngleParameters(string[] _params)
	{
		this.m_landingAngle = 0f;
		this.m_angleMaxVariance = 30f;
		if (_params.Length > 1)
		{
			float.TryParse(_params[1], ref this.m_landingAngle);
		}
		if (_params.Length > 2)
		{
			float.TryParse(_params[2], ref this.m_angleMaxVariance);
		}
		if (Mathf.Abs(this.m_landingAngle) > 360f)
		{
			this.m_landingAngle = 0f;
		}
		else if (Mathf.Abs(this.m_landingAngle) > 180f)
		{
			if (this.m_landingAngle > 0f)
			{
				this.m_landingAngle -= 360f;
			}
			else
			{
				this.m_landingAngle += 360f;
			}
		}
		this.m_angleMaxVariance = Mathf.Abs(this.m_angleMaxVariance);
	}

	// Token: 0x06000124 RID: 292 RVA: 0x0000D1CF File Offset: 0x0000B5CF
	public override void CreateEditorTouchArea(float _width, float _height, TransformC _parentTC = null, Vector2 _offset = default(Vector2))
	{
		if (this.m_minigame.m_editing)
		{
			this.CreateGraphElementTouchArea(_width, _parentTC);
		}
	}

	// Token: 0x06000125 RID: 293 RVA: 0x0000D1EC File Offset: 0x0000B5EC
	protected void CollisionHandler(ucpCollisionPair _pair, ucpCollisionPhase _phase)
	{
		string text = (base.m_graphElement as LevelTextNode).m_text;
		string[] array = text.Split(new char[] { ':' }, 1);
		text = array[0];
		if (array.Length > 0 && Sensor.m_listeners != null && Sensor.m_listeners.ContainsKey(text))
		{
			Sensor.m_listeners[text].Invoke(array);
			Sensor.m_listeners.Remove(text);
		}
	}

	// Token: 0x06000126 RID: 294 RVA: 0x0000D264 File Offset: 0x0000B664
	public override void Update()
	{
		if (this.m_minigame.m_editing)
		{
			if (base.m_graphElement.m_flipped)
			{
				base.m_graphElement.m_flipped = false;
				this.m_sensorText = (base.m_graphElement as LevelTextNode).m_text;
				this.m_textModel = new TextModel((base.m_graphElement as LevelTextNode).m_text, null);
				PsUICenterTutorialSignTextInput psUICenterTutorialSignTextInput = new PsUICenterTutorialSignTextInput(this.m_textModel);
				psUICenterTutorialSignTextInput.Update();
			}
			if (this.m_textModel != null)
			{
				if (this.m_textModel.m_done && this.m_sensorText != this.m_textModel.m_text)
				{
					this.m_sensorText = this.m_textModel.m_text;
					(base.m_graphElement as LevelTextNode).m_text = this.m_textModel.m_text;
					this.m_textModel = null;
				}
				else if (this.m_textModel.m_cancelled)
				{
					this.m_textModel = null;
				}
			}
		}
	}

	// Token: 0x06000127 RID: 295 RVA: 0x0000D366 File Offset: 0x0000B766
	public static void AddListener(string _sensorName, Action<string[]> _callBack)
	{
		if (Sensor.m_listeners == null)
		{
			Sensor.m_listeners = new Dictionary<string, Action<string[]>>();
		}
		if (!Sensor.m_listeners.ContainsKey(_sensorName))
		{
			Sensor.m_listeners.Add(_sensorName, _callBack);
		}
	}

	// Token: 0x040000C7 RID: 199
	private const int RADIUS = 45;

	// Token: 0x040000C8 RID: 200
	private ChipmunkBodyC m_body;

	// Token: 0x040000C9 RID: 201
	private ucpShape m_shape;

	// Token: 0x040000CA RID: 202
	private TransformC m_tc;

	// Token: 0x040000CB RID: 203
	private string m_sensorText;

	// Token: 0x040000CC RID: 204
	private TextModel m_textModel;

	// Token: 0x040000CD RID: 205
	private PsUITutorialArrowUI m_tutorialArrow;

	// Token: 0x040000CE RID: 206
	private bool m_tutorialFinished;

	// Token: 0x040000CF RID: 207
	private Action m_finishTutorial;

	// Token: 0x040000D0 RID: 208
	private Vehicle m_playerVehicle;

	// Token: 0x040000D1 RID: 209
	private float m_landingAngle;

	// Token: 0x040000D2 RID: 210
	private float m_angleMaxVariance;

	// Token: 0x040000D3 RID: 211
	private string m_targetButtonName;

	// Token: 0x040000D4 RID: 212
	private int m_flipDirection;

	// Token: 0x040000D5 RID: 213
	public static Dictionary<string, Action<string[]>> m_listeners;
}
