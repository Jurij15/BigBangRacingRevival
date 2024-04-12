using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000266 RID: 614
public class PsUICrewMemberBase : UICanvas
{
	// Token: 0x06001264 RID: 4708 RVA: 0x000B5EA8 File Offset: 0x000B42A8
	public PsUICrewMemberBase(Camera _camera, string _tag)
		: base(null, false, _tag, null, string.Empty)
	{
		this.RemoveDrawHandler();
		this.SetHeight(1f, RelativeTo.ScreenHeight);
		this.SetWidth(1f, RelativeTo.ScreenWidth);
		this.SetMargins(0.02f, 0.02f, 0.02f, 0.04f, RelativeTo.ScreenHeight);
		this.SetCamera(_camera, true, false);
	}

	// Token: 0x06001265 RID: 4709 RVA: 0x000B5F08 File Offset: 0x000B4308
	public void CreateCharacter(PsDialogueCharacter _character, PsDialogueCharacterPosition _position, float _customScale = 1f)
	{
		if (this.m_character != null)
		{
			this.m_character.Destroy();
			this.m_character = null;
		}
		this.m_character = new PsUICharacter(this, _character, _position, _customScale);
		if (_position == PsDialogueCharacterPosition.Right)
		{
			this.m_character.SetAlign(1f, 0f);
		}
		else if (_position == PsDialogueCharacterPosition.Left)
		{
			this.m_character.SetAlign(0f, 0f);
		}
		this.Update();
	}

	// Token: 0x06001266 RID: 4710 RVA: 0x000B5F84 File Offset: 0x000B4384
	public void StartDialogue(PsDialogueStep _dialogue)
	{
		if (this.m_speechContent == null)
		{
			this.m_speechContent = new UIVerticalList(this, string.Empty);
			this.m_speechContent.SetMargins(0.05f, 0.05f, 0.05f, 0.05f, RelativeTo.ScreenHeight);
			this.m_speechContent.SetWidth(0.5f, RelativeTo.ScreenWidth);
			this.m_speechContent.SetSpacing(0.05f, RelativeTo.ScreenHeight);
			this.m_speechContent.SetDrawHandler(new UIDrawDelegate(this.SpeechBubbleDrawhandler));
			this.m_speechContent.SetDepthOffset(-20f);
		}
		if (_dialogue.m_characterPosition == PsDialogueCharacterPosition.Right)
		{
			this.m_speechContent.SetAlign(0.1f, 0.4f);
			this.m_bubbleHandlePos = SpeechBubbleHandlePosition.BottomRight;
			this.m_bubbleHandleType = SpeechBubbleHandleType.SmallToRight;
		}
		else
		{
			this.m_speechContent.SetAlign(0.9f, 0.4f);
			this.m_bubbleHandlePos = SpeechBubbleHandlePosition.BottomLeft;
			this.m_bubbleHandleType = SpeechBubbleHandleType.SmallToLeft;
		}
		string text = ((_dialogue.m_characterTextLocalized == StringID.EMPTY) ? _dialogue.m_charactertext : PsStrings.Get(_dialogue.m_characterTextLocalized));
		if (!string.IsNullOrEmpty(text))
		{
			if (this.m_speechText != null)
			{
				this.m_speechText.SetText(text);
			}
			else
			{
				this.m_speechText = new UITextbox(this.m_speechContent, false, "text", text, PsFontManager.GetFont(PsFonts.HurmeSemiBold), 0.03f, RelativeTo.ScreenHeight, true, Align.Center, Align.Top, "#4dcfff", true, null);
			}
		}
		if (this.m_buttonList == null)
		{
			this.m_buttonList = new UIHorizontalList(this.m_speechContent, string.Empty);
			this.m_buttonList.SetSpacing(0.05f, RelativeTo.ScreenHeight);
			this.m_buttonList.RemoveDrawHandler();
		}
		this.m_buttonList.SetHorizontalAlign(0.5f);
		if (!string.IsNullOrEmpty(_dialogue.m_proceedText))
		{
			if (this.m_ok != null)
			{
				this.m_ok.SetText(_dialogue.m_proceedText, 0.04f, 0f, RelativeTo.ScreenHeight, false, RelativeTo.ScreenShortest);
			}
			else
			{
				this.m_ok = new PsUIGenericButton(this.m_buttonList, 0.25f, 0.25f, 0.005f, "Button");
				this.m_ok.SetText(_dialogue.m_proceedText, 0.04f, 0f, RelativeTo.ScreenHeight, false, RelativeTo.ScreenShortest);
				this.m_ok.SetHeight(0.075f, RelativeTo.ScreenHeight);
			}
			this.m_ok.SetGreenColors(true);
		}
		else if (this.m_ok != null)
		{
			this.m_ok.Destroy();
			this.m_ok = null;
		}
		if (!string.IsNullOrEmpty(_dialogue.m_cancelText))
		{
			if (this.m_cancel != null)
			{
				this.m_cancel.SetText(_dialogue.m_cancelText, 0.04f, 0f, RelativeTo.ScreenHeight, false, RelativeTo.ScreenShortest);
			}
			else
			{
				this.m_cancel = new PsUIGenericButton(this.m_buttonList, 0.25f, 0.25f, 0.005f, "Button");
				this.m_cancel.SetText(_dialogue.m_cancelText, 0.04f, 0f, RelativeTo.ScreenHeight, false, RelativeTo.ScreenShortest);
				this.m_cancel.SetHeight(0.075f, RelativeTo.ScreenHeight);
			}
		}
		else if (this.m_cancel != null)
		{
			this.m_cancel.Destroy();
			this.m_cancel = null;
		}
		this.Update();
	}

	// Token: 0x06001267 RID: 4711 RVA: 0x000B62B0 File Offset: 0x000B46B0
	public override void Update()
	{
		if (this.m_ok != null)
		{
			this.m_ok.SetDepthOffset(-10f);
		}
		if (this.m_cancel != null)
		{
			this.m_cancel.SetDepthOffset(-10f);
		}
		base.Update();
	}

	// Token: 0x06001268 RID: 4712 RVA: 0x000B62F0 File Offset: 0x000B46F0
	public void SpeechBubbleDrawhandler(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] array = DebugDraw.GetBezierRect(_c.m_actualWidth, _c.m_actualHeight, 0.025f * (float)Screen.height, 10, Vector2.zero);
		array = DebugDraw.AddSpeechHandleToVectorArray(array, this.m_bubbleHandlePos, this.m_bubbleHandleType);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * -5f, array, (float)Screen.width * 0.005f, DebugDraw.HexToColor("#4dcfff"), ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line8Mat_Material), _c.m_camera, Position.Center, true);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * -3f, array, (float)Screen.width * 0.008f, new Color(1f, 1f, 1f, 0.5f), ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line8Gradient2Mat_Material), this.m_camera, Position.Inside, true);
		PrefabS.CreateFlatPrefabComponentsFromVectorArray(_c.m_TC, Vector3.forward * -2f, array, DebugDraw.HexToUint("#FFFFFF"), DebugDraw.HexToUint("#FFFFFF"), ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera, string.Empty, null);
	}

	// Token: 0x06001269 RID: 4713 RVA: 0x000B6424 File Offset: 0x000B4824
	public override void Step()
	{
		if (this.m_buttons != null)
		{
			for (int i = 0; i < this.m_buttons.Count; i++)
			{
				if (this.m_buttons[i].Key.m_hit)
				{
					this.m_buttons[i].Value.Invoke();
					break;
				}
			}
		}
		base.Step();
	}

	// Token: 0x0600126A RID: 4714 RVA: 0x000B649A File Offset: 0x000B489A
	public void Talk()
	{
		if (this.m_character != null)
		{
			this.m_character.Talk();
		}
	}

	// Token: 0x0600126B RID: 4715 RVA: 0x000B64B2 File Offset: 0x000B48B2
	public void LookAtCamera()
	{
		if (this.m_character != null)
		{
			this.m_character.LookAtCamera();
		}
	}

	// Token: 0x0600126C RID: 4716 RVA: 0x000B64CA File Offset: 0x000B48CA
	public void LookAwayFromCamera()
	{
		if (this.m_character != null)
		{
			this.m_character.LookAwayFromCamera();
		}
	}

	// Token: 0x04001593 RID: 5523
	public UIHorizontalList m_contentList;

	// Token: 0x04001594 RID: 5524
	public UIVerticalList m_speechContent;

	// Token: 0x04001595 RID: 5525
	public UIHorizontalList m_buttonList;

	// Token: 0x04001596 RID: 5526
	public List<KeyValuePair<PsUIGenericButton, Action>> m_buttons;

	// Token: 0x04001597 RID: 5527
	public string m_text;

	// Token: 0x04001598 RID: 5528
	public string m_label;

	// Token: 0x04001599 RID: 5529
	public string m_characterFrameName;

	// Token: 0x0400159A RID: 5530
	public UITextbox m_speechText;

	// Token: 0x0400159B RID: 5531
	private PsDialogueCharacter m_lastCharacter;

	// Token: 0x0400159C RID: 5532
	private UIFittedSprite m_characterSprite;

	// Token: 0x0400159D RID: 5533
	public PsUIGenericButton m_ok;

	// Token: 0x0400159E RID: 5534
	public PsUIGenericButton m_cancel;

	// Token: 0x0400159F RID: 5535
	public SpeechBubbleHandlePosition m_bubbleHandlePos;

	// Token: 0x040015A0 RID: 5536
	public SpeechBubbleHandleType m_bubbleHandleType;

	// Token: 0x040015A1 RID: 5537
	public PsUICharacter m_character;
}
