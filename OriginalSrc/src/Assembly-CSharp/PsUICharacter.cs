using System;
using UnityEngine;

// Token: 0x0200023D RID: 573
public class PsUICharacter : UICanvas
{
	// Token: 0x06001156 RID: 4438 RVA: 0x000A79D4 File Offset: 0x000A5DD4
	public PsUICharacter(UIComponent _parent, PsDialogueCharacter _character, PsDialogueCharacterPosition _position, float _customScale = 1f)
		: base(_parent, true, "PsUICharacter", null, string.Empty)
	{
		this.RemoveDrawHandler();
		this.m_character = _character;
		this.m_position = _position;
		this.m_customScale = _customScale;
		base.SetHeight(0.4f, RelativeTo.OwnHeight);
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
					gameObject = ResourceManager.GetGameObject(RESOURCE.MenuCharBuildPrefab_GameObject);
				}
			}
			else
			{
				gameObject = ResourceManager.GetGameObject(RESOURCE.MenuCharMechPrefab_GameObject);
			}
		}
		else
		{
			gameObject = ResourceManager.GetGameObject(RESOURCE.MenuCharProfPrefab_GameObject);
		}
		PrefabC prefabC = PrefabS.AddComponent(this.m_prefabTC, Vector3.zero, gameObject);
		PrefabS.SetCamera(prefabC, this.m_camera);
		this.m_animationScript = prefabC.p_gameObject.GetComponent<MenuCharacter>();
		TransformS.SetRotation(this.m_prefabTC, Vector3.up * 180f);
	}

	// Token: 0x06001157 RID: 4439 RVA: 0x000A7AE8 File Offset: 0x000A5EE8
	public override void Update()
	{
		base.Update();
		TransformS.SetScale(this.m_prefabTC, Vector3.one * (this.m_actualHeight / (float)Screen.height) * ((float)Screen.height * 0.001f * this.m_customScale));
		if (this.m_position == PsDialogueCharacterPosition.Right && this.m_character != PsDialogueCharacter.Architect)
		{
			TransformS.SetScale(this.m_prefabTC, new Vector3(-this.m_prefabTC.transform.localScale.x, this.m_prefabTC.transform.localScale.y, this.m_prefabTC.transform.localScale.z));
		}
		else if (this.m_position == PsDialogueCharacterPosition.Left && this.m_character == PsDialogueCharacter.Architect)
		{
			TransformS.SetScale(this.m_prefabTC, new Vector3(-this.m_prefabTC.transform.localScale.x, this.m_prefabTC.transform.localScale.y, this.m_prefabTC.transform.localScale.z));
		}
	}

	// Token: 0x06001158 RID: 4440 RVA: 0x000A7C1D File Offset: 0x000A601D
	public new void SetWidth(float _width, RelativeTo _relativeTo)
	{
		Debug.LogWarning("Do not use PsUICharacterButton.SetWidth() method!");
	}

	// Token: 0x06001159 RID: 4441 RVA: 0x000A7C29 File Offset: 0x000A6029
	public new void SetSize(float _width, float _height, RelativeTo _relativeTo)
	{
		Debug.LogWarning("Do not use PsUICharacterButton.SetSize() method!");
	}

	// Token: 0x0600115A RID: 4442 RVA: 0x000A7C38 File Offset: 0x000A6038
	public void Talk()
	{
		if (this.m_animationScript != null)
		{
			PsDialogueCharacter character = this.m_character;
			if (character != PsDialogueCharacter.Scientist)
			{
				if (character != PsDialogueCharacter.Mechanic)
				{
					if (character == PsDialogueCharacter.Architect)
					{
						SoundS.PlaySingleShot("/Ingame/Characters/Dialogue_Builder", Vector2.zero, 1f);
					}
				}
				else
				{
					SoundS.PlaySingleShot("/Ingame/Characters/Dialogue_Mechanic", Vector2.zero, 1f);
				}
			}
			else
			{
				SoundS.PlaySingleShot("/Ingame/Characters/Dialogue_Professor", Vector2.zero, 1f);
			}
			this.m_animationScript.Talk();
		}
	}

	// Token: 0x0600115B RID: 4443 RVA: 0x000A7CDB File Offset: 0x000A60DB
	public void LookAtCamera()
	{
		if (this.m_animationScript != null)
		{
			this.m_animationScript.LookAtCamera();
		}
	}

	// Token: 0x0600115C RID: 4444 RVA: 0x000A7CF9 File Offset: 0x000A60F9
	public void LookAwayFromCamera()
	{
		if (this.m_animationScript != null)
		{
			this.m_animationScript.LookAwayFromCamera();
		}
	}

	// Token: 0x04001447 RID: 5191
	private TransformC m_prefabTC;

	// Token: 0x04001448 RID: 5192
	private MenuCharacter m_animationScript;

	// Token: 0x04001449 RID: 5193
	private PsDialogueCharacterPosition m_position;

	// Token: 0x0400144A RID: 5194
	public PsDialogueCharacter m_character;

	// Token: 0x0400144B RID: 5195
	private float m_customScale;
}
