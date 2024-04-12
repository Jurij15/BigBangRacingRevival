using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000298 RID: 664
public class PsUI3DCanvasGacha : UI3DRenderTextureCanvas
{
	// Token: 0x06001401 RID: 5121 RVA: 0x000CA1A0 File Offset: 0x000C85A0
	public PsUI3DCanvasGacha(UIComponent _parent, string _tag, Vector3 _offset = default(Vector3))
		: base(_parent, _tag, null, false)
	{
		this.m_spinnerOffset = _offset;
		this.m_3DCamera.fieldOfView = 22f;
		this.m_3DCamera.nearClipPlane = 1f;
		this.m_3DCamera.farClipPlane = 10f;
	}

	// Token: 0x06001402 RID: 5122 RVA: 0x000CA1FC File Offset: 0x000C85FC
	public virtual PrefabC AddSpinnerBaseGo(int _count, float _distanceBetweenPrizes, Vector3 _prizeLookAtAngle)
	{
		this.m_prizeLookAtAngle = _prizeLookAtAngle;
		this.m_angleBetweenPrizes = 360f / (float)_count;
		this.SetCircleSize(_distanceBetweenPrizes);
		float num = 4.63f;
		this.m_3DCameraOffset = -this.m_radius - num;
		this.m_prefab = this.AddEmptyGameObject(Vector3.zero, Vector3.zero);
		this.m_prefab.p_gameObject.transform.localPosition = this.m_spinnerOffset;
		return this.m_prefab;
	}

	// Token: 0x06001403 RID: 5123 RVA: 0x000CA274 File Offset: 0x000C8674
	public PrefabC AddEmptyGameObject(Vector3 _position, Vector3 _rotation = default(Vector3))
	{
		PrefabC prefabC = PrefabS.AddComponent(this.m_3DContent, _position);
		PrefabS.SetCamera(prefabC, this.m_3DCamera);
		prefabC.p_gameObject.transform.rotation = Quaternion.Euler(_rotation);
		return prefabC;
	}

	// Token: 0x06001404 RID: 5124 RVA: 0x000CA2B1 File Offset: 0x000C86B1
	protected void SetPrizeCount(int _count)
	{
		this.m_prizeCount = _count;
	}

	// Token: 0x06001405 RID: 5125 RVA: 0x000CA2BA File Offset: 0x000C86BA
	protected void SetTargetAngle(int _targetIndex)
	{
		if (this.m_rotationDirection > 0)
		{
			_targetIndex = this.m_prizeCount - _targetIndex;
		}
		this.m_yTargetAngle = (float)_targetIndex * (this.m_angleBetweenPrizes * (float)this.m_rotationDirection);
	}

	// Token: 0x06001406 RID: 5126 RVA: 0x000CA2E9 File Offset: 0x000C86E9
	private void SetCircleSize(float _distanceBetweenPrizes)
	{
		this.m_radius = _distanceBetweenPrizes / (0.017453292f * this.m_angleBetweenPrizes);
	}

	// Token: 0x06001407 RID: 5127 RVA: 0x000CA2FF File Offset: 0x000C86FF
	protected void SetSpinnerCamera()
	{
		PrefabS.SetCamera(this.m_prefab.p_gameObject, this.m_3DCamera);
	}

	// Token: 0x06001408 RID: 5128 RVA: 0x000CA317 File Offset: 0x000C8717
	protected void SetTargetAngle()
	{
		this.m_ySpinnerTargetAngle = this.m_yRotateOffset + (float)this.m_prizeIndex * -this.m_angleBetweenPrizes;
	}

	// Token: 0x06001409 RID: 5129 RVA: 0x000CA338 File Offset: 0x000C8738
	protected Vector3 GetSpinnerPrizePosition(Vector3 _center, float _angle)
	{
		float radius = this.m_radius;
		Vector3 vector;
		vector.x = _center.x + radius * Mathf.Sin(_angle * 0.017453292f) * -1f;
		vector.y = _center.y;
		vector.z = _center.z + radius * Mathf.Cos(_angle * 0.017453292f) * -1f;
		return vector;
	}

	// Token: 0x0600140A RID: 5130 RVA: 0x000CA3A4 File Offset: 0x000C87A4
	public void StartAnimation(Action _winCallBack)
	{
		this.m_winCallBack = _winCallBack;
		this.m_yTargetAngle += (float)(1080 * this.m_rotationDirection);
		this.m_tween = TweenS.AddTween(this.m_TC.p_entity, TweenStyle.QuadOut, 0f, 1f, 5f, 0f);
		this.m_startTween = TweenS.AddTween(this.m_TC.p_entity, TweenStyle.ExpoIn, 0f, 1f, 0.3f, 0f);
		this.m_spinAnimationJustStarted = true;
		Debug.Log("StartSpin", null);
		SoundS.PlaySingleShotWithParameter("/Metagame/SpinWheel", Vector3.zero, "SpinStep", 0f, 1f);
	}

	// Token: 0x0600140B RID: 5131 RVA: 0x000CA458 File Offset: 0x000C8858
	public float GetNeedleValue()
	{
		int num = (int)(this.m_currentRotation / this.m_angleBetweenPrizes);
		float num2 = this.m_currentRotation - this.m_angleBetweenPrizes * (float)num;
		num2 = num2 / this.m_angleBetweenPrizes - 0.5f;
		if (num2 < 0f)
		{
			num2 = 1f + num2;
		}
		return num2;
	}

	// Token: 0x0600140C RID: 5132 RVA: 0x000CA4A8 File Offset: 0x000C88A8
	protected virtual void WinEffect()
	{
		if (this.m_winCallBack != null)
		{
			this.m_winCallBack.Invoke();
		}
		if (this.m_prizeGO != null)
		{
		}
	}

	// Token: 0x0600140D RID: 5133 RVA: 0x000CA4D4 File Offset: 0x000C88D4
	public override void Step()
	{
		if (this.m_tween != null)
		{
			this.m_currentRotation = this.m_yTargetAngle * this.m_tween.currentValue.x * this.m_startTween.currentValue.x;
			Quaternion quaternion = Quaternion.Euler(new Vector3(0f, this.m_currentRotation, 0f));
			this.m_prefab.p_gameObject.transform.rotation = quaternion;
			if (this.m_tween.hasFinished)
			{
				Debug.Log("Roulette finished", null);
				SoundS.PlaySingleShotWithParameter("/Metagame/SpinWheel", Vector3.zero, "SpinStep", (float)((!this.m_isBigWin) ? 2 : 3), 1f);
				TweenS.RemoveComponent(this.m_tween);
				TweenS.RemoveComponent(this.m_startTween);
				this.m_tween = null;
				this.m_startTween = null;
				this.WinEffect();
			}
			else if (Math.Abs(-this.m_currentRotation - this.m_soundAngle) > this.m_angleBetweenPrizes)
			{
				this.m_soundAngle = -this.m_currentRotation;
				this.m_hitCenter = true;
			}
			else
			{
				this.m_hitCenter = false;
			}
		}
		for (int i = 0; i < this.m_prefab.p_gameObject.transform.childCount; i++)
		{
			Transform child = this.m_prefab.p_gameObject.transform.GetChild(i);
			child.rotation = Quaternion.Euler(this.m_prizeLookAtAngle);
		}
		base.Step();
	}

	// Token: 0x0600140E RID: 5134 RVA: 0x000CA654 File Offset: 0x000C8A54
	public void AddSpinnerTargetGOs<T>(int _count, T m_prize, List<T> _list, float _distanceBetweenTargets, Vector3 _prizeLookAtAngle, params string[] _parameters)
	{
		PrefabC prefabC = this.AddSpinnerBaseGo(_count, _distanceBetweenTargets, _prizeLookAtAngle);
		this.m_isBigWin = this.IsBigWin<T>(m_prize);
		for (int i = 0; i < _list.Count; i++)
		{
			GameObject oneGO = this.GetOneGO<T>(_list[i]);
			oneGO.transform.parent = prefabC.p_gameObject.transform;
			float num = this.m_angleBetweenPrizes * (float)i;
			oneGO.transform.localPosition = this.GetSpinnerPrizePosition(Vector3.zero, num);
			if (this.IsSame<T>(_list[i], m_prize) && this.m_prizeIndex == -1)
			{
				this.m_prizeIndex = i;
				this.SetPrizeGameObject(oneGO);
			}
		}
		this.SetPrizeCount(_count);
		this.SetTargetAngle(this.m_prizeIndex);
		this.SetSpinnerCamera();
	}

	// Token: 0x0600140F RID: 5135 RVA: 0x000CA71C File Offset: 0x000C8B1C
	public virtual void SetPrizeGameObject(GameObject _go)
	{
		this.m_prizeGO = _go;
	}

	// Token: 0x06001410 RID: 5136 RVA: 0x000CA725 File Offset: 0x000C8B25
	public virtual bool IsSame<T>(T _first, T _second)
	{
		Debug.LogError("OVERRIDE");
		return false;
	}

	// Token: 0x06001411 RID: 5137 RVA: 0x000CA732 File Offset: 0x000C8B32
	public virtual GameObject GetOneGO<T>(T _item)
	{
		Debug.LogError("OVERRIDE");
		return null;
	}

	// Token: 0x06001412 RID: 5138 RVA: 0x000CA73F File Offset: 0x000C8B3F
	public virtual bool IsBigWin<T>(T _item)
	{
		Debug.LogError("OVERRIDE");
		return false;
	}

	// Token: 0x040016B4 RID: 5812
	private PrefabC m_prefab;

	// Token: 0x040016B5 RID: 5813
	private Vector3 m_prizeLookAtAngle;

	// Token: 0x040016B6 RID: 5814
	private float m_ySpinnerTargetAngle;

	// Token: 0x040016B7 RID: 5815
	private float m_yRotateOffset;

	// Token: 0x040016B8 RID: 5816
	private float m_yTargetAngle;

	// Token: 0x040016B9 RID: 5817
	public float m_radius;

	// Token: 0x040016BA RID: 5818
	public float m_angleBetweenPrizes;

	// Token: 0x040016BB RID: 5819
	public int m_prizeCount;

	// Token: 0x040016BC RID: 5820
	public int m_prizeIndex = -1;

	// Token: 0x040016BD RID: 5821
	protected GameObject m_prizeGO;

	// Token: 0x040016BE RID: 5822
	public TweenC m_tween;

	// Token: 0x040016BF RID: 5823
	public TweenC m_startTween;

	// Token: 0x040016C0 RID: 5824
	protected Action m_winCallBack;

	// Token: 0x040016C1 RID: 5825
	public int m_rotationDirection = 1;

	// Token: 0x040016C2 RID: 5826
	private float m_currentRotation;

	// Token: 0x040016C3 RID: 5827
	private Vector3 m_spinnerOffset;

	// Token: 0x040016C4 RID: 5828
	protected bool m_isBigWin;

	// Token: 0x040016C5 RID: 5829
	public bool m_spinAnimationJustStarted;

	// Token: 0x040016C6 RID: 5830
	private float m_soundAngle;

	// Token: 0x040016C7 RID: 5831
	public bool m_hitCenter;
}
