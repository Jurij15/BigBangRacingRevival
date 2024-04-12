using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000239 RID: 569
public class PsUICharacterButton : UICanvas
{
	// Token: 0x06001102 RID: 4354 RVA: 0x000A71FC File Offset: 0x000A55FC
	public PsUICharacterButton(UIComponent _parent, PsDialogueCharacter _character)
		: base(_parent, true, "PsUICharacterButton", null, string.Empty)
	{
		this.RemoveDrawHandler();
		base.SetWidth(0.8f, RelativeTo.OwnHeight);
		this.m_prefabTC = TransformS.AddComponent(this.m_TC.p_entity);
		TransformS.ParentComponent(this.m_prefabTC, this.m_TC);
		GameObject gameObject = null;
		if (_character != PsDialogueCharacter.Scientist)
		{
			if (_character != PsDialogueCharacter.Mechanic)
			{
				if (_character == PsDialogueCharacter.Architect)
				{
					gameObject = ResourceManager.GetGameObject(RESOURCE.MenuCharBuildButtonPrefab_GameObject);
				}
			}
			else
			{
				gameObject = ResourceManager.GetGameObject(RESOURCE.MenuCharMechButtonPrefab_GameObject);
			}
		}
		else
		{
			gameObject = ResourceManager.GetGameObject(RESOURCE.MenuCharProfButtonPrefab_GameObject);
		}
		PrefabC prefabC = PrefabS.AddComponent(this.m_prefabTC, Vector3.zero, gameObject);
		PrefabS.SetCamera(prefabC, this.m_camera);
		this.m_animationScript = prefabC.p_gameObject.GetComponent<MenuCharacter>();
		this.m_renderers = prefabC.p_gameObject.GetComponentsInChildren<Renderer>(true);
		TransformS.SetRotation(this.m_prefabTC, Vector3.up * 180f);
	}

	// Token: 0x06001103 RID: 4355 RVA: 0x000A730E File Offset: 0x000A570E
	public override void Update()
	{
		base.Update();
		TransformS.SetScale(this.m_prefabTC, Vector3.one * (this.m_actualHeight / (float)Screen.height) * ((float)Screen.height * 0.0012f));
	}

	// Token: 0x06001104 RID: 4356 RVA: 0x000A7349 File Offset: 0x000A5749
	public new void SetWidth(float _width, RelativeTo _relativeTo)
	{
		Debug.LogWarning("Do not use PsUICharacterButton.SetWidth() method!");
	}

	// Token: 0x06001105 RID: 4357 RVA: 0x000A7355 File Offset: 0x000A5755
	public new void SetSize(float _width, float _height, RelativeTo _relativeTo)
	{
		Debug.LogWarning("Do not use PsUICharacterButton.SetSize() method!");
	}

	// Token: 0x06001106 RID: 4358 RVA: 0x000A7364 File Offset: 0x000A5764
	protected override void OnTouchRollIn(TLTouch _touch, bool _secondary)
	{
		base.OnTouchRollIn(_touch, _secondary);
		if (this.m_scaleOnTouch)
		{
			if (this.m_touchScaleTween != null)
			{
				TweenS.RemoveComponent(this.m_touchScaleTween);
				this.m_touchScaleTween = null;
			}
			this.m_touchScaleTween = TweenS.AddTransformTween(this.m_TC, TweenedProperty.Scale, TweenStyle.BackOut, new Vector3(1.05f, 1.05f, 1.05f), 0.1f, 0f, false);
			this.Poke();
		}
	}

	// Token: 0x06001107 RID: 4359 RVA: 0x000A73DC File Offset: 0x000A57DC
	protected override void OnTouchBegan(TLTouch _touch)
	{
		base.OnTouchBegan(_touch);
		if (this.m_scaleOnTouch)
		{
			if (this.m_touchScaleTween != null)
			{
				TweenS.RemoveComponent(this.m_touchScaleTween);
				this.m_touchScaleTween = null;
			}
			this.m_touchScaleTween = TweenS.AddTransformTween(this.m_TC, TweenedProperty.Scale, TweenStyle.BackOut, new Vector3(1.05f, 1.05f, 1.05f), 0.1f, 0f, false);
			this.Poke();
		}
	}

	// Token: 0x06001108 RID: 4360 RVA: 0x000A7454 File Offset: 0x000A5854
	protected override void OnTouchRollOut(TLTouch _touch, bool _secondary)
	{
		base.OnTouchRollOut(_touch, _secondary);
		if (this.m_scaleOnTouch)
		{
			if (this.m_touchScaleTween != null)
			{
				TweenS.RemoveComponent(this.m_touchScaleTween);
				this.m_touchScaleTween = null;
			}
			this.m_touchScaleTween = TweenS.AddTransformTween(this.m_TC, TweenedProperty.Scale, TweenStyle.BackOut, new Vector3(1f, 1f, 1f), 0.1f, 0f, false);
		}
	}

	// Token: 0x06001109 RID: 4361 RVA: 0x000A74C4 File Offset: 0x000A58C4
	protected override void OnTouchRelease(TLTouch _touch, bool _inside)
	{
		base.OnTouchRelease(_touch, _inside);
		if (this.m_scaleOnTouch)
		{
			if (this.m_touchScaleTween != null)
			{
				TweenS.RemoveComponent(this.m_touchScaleTween);
				this.m_touchScaleTween = null;
			}
			this.m_touchScaleTween = TweenS.AddTransformTween(this.m_TC, TweenedProperty.Scale, TweenStyle.BackOut, new Vector3(1f, 1f, 1f), 0.1f, 0f, false);
			SoundS.PlaySingleShot("/UI/ButtonNormal", Vector3.zero, 1f);
		}
	}

	// Token: 0x0600110A RID: 4362 RVA: 0x000A7548 File Offset: 0x000A5948
	public void Activate()
	{
		this.m_animationScript.Activate();
	}

	// Token: 0x0600110B RID: 4363 RVA: 0x000A7555 File Offset: 0x000A5955
	public void Deactivate()
	{
		this.m_animationScript.Deactivate();
	}

	// Token: 0x0600110C RID: 4364 RVA: 0x000A7562 File Offset: 0x000A5962
	public void Talk()
	{
		if (this.m_animationScript != null)
		{
			this.m_animationScript.Talk();
		}
	}

	// Token: 0x0600110D RID: 4365 RVA: 0x000A7580 File Offset: 0x000A5980
	public void Poke()
	{
		if (this.m_animationScript != null)
		{
			this.m_animationScript.Poke();
		}
	}

	// Token: 0x0600110E RID: 4366 RVA: 0x000A759E File Offset: 0x000A599E
	public void LookAtCamera()
	{
		if (this.m_animationScript != null)
		{
			this.m_animationScript.LookAtCamera();
		}
	}

	// Token: 0x0600110F RID: 4367 RVA: 0x000A75BC File Offset: 0x000A59BC
	public void LookAwayFromCamera()
	{
		if (this.m_animationScript != null)
		{
			this.m_animationScript.LookAwayFromCamera();
		}
	}

	// Token: 0x06001110 RID: 4368 RVA: 0x000A75DC File Offset: 0x000A59DC
	public override void Destroy()
	{
		for (int i = this.m_removeMats.Count - 1; i >= 0; i--)
		{
			Object.Destroy(this.m_removeMats[i]);
		}
		this.m_removeMats.Clear();
		this.m_renderers = null;
		this.m_removeMats = null;
		base.Destroy();
	}

	// Token: 0x040013F4 RID: 5108
	private TransformC m_prefabTC;

	// Token: 0x040013F5 RID: 5109
	private MenuCharacter m_animationScript;

	// Token: 0x040013F6 RID: 5110
	private bool m_scaleOnTouch = true;

	// Token: 0x040013F7 RID: 5111
	private TweenC m_touchScaleTween;

	// Token: 0x040013F8 RID: 5112
	private Renderer[] m_renderers;

	// Token: 0x040013F9 RID: 5113
	private List<Material> m_removeMats = new List<Material>();
}
