using System;
using UnityEngine;

// Token: 0x0200050B RID: 1291
public static class TextMeshS
{
	// Token: 0x0600258E RID: 9614 RVA: 0x0019EDE4 File Offset: 0x0019D1E4
	public static void Initialize()
	{
		TextMeshS.m_components = new DynamicArray<TextMeshC>(100, 0.5f, 0.25f, 0.5f);
		TextMeshS.m_emptyGameObject = new GameObject("TextMeshSystem: InstantiateHelper");
		TextMesh textMesh = TextMeshS.m_emptyGameObject.AddComponent<TextMesh>();
		textMesh.characterSize = (float)TextMeshS.m_defaultCharacterSize;
		MeshRenderer component = TextMeshS.m_emptyGameObject.GetComponent<MeshRenderer>();
		component.enabled = false;
	}

	// Token: 0x0600258F RID: 9615 RVA: 0x0019EE44 File Offset: 0x0019D244
	public static TextMeshC AddComponent(TransformC _tc, Vector3 _offset, string _fontResourcePath, float _textboxWidth, float _textboxHeight, float _fontSize, Align _horizontalAlign, Align _verticalAlign, Camera _camera, string _name = "")
	{
		TextMeshC textMeshC = TextMeshS.m_components.AddItem();
		textMeshC.m_TC = _tc;
		textMeshC.m_go = Object.Instantiate<GameObject>(TextMeshS.m_emptyGameObject);
		textMeshC.m_go.layer = _camera.gameObject.layer;
		textMeshC.m_go.transform.name = _name;
		textMeshC.m_go.transform.parent = textMeshC.m_TC.transform;
		textMeshC.m_go.transform.localPosition = _offset;
		textMeshC.m_go.transform.localRotation = Quaternion.Euler(Vector3.zero);
		Font font = ResourceManager.GetFont(_fontResourcePath);
		textMeshC.m_textMesh = textMeshC.m_go.GetComponent("TextMesh") as TextMesh;
		textMeshC.m_textMesh.font = font;
		FontInfo fontInfo = FontInfoManager.GetFontInfo(_fontResourcePath);
		textMeshC.m_textMesh.lineSpacing = fontInfo.lineSpacing;
		textMeshC.m_textMesh.characterSize = (float)fontInfo.characterSize;
		TextMeshS.SetFontSize(textMeshC, _fontSize);
		textMeshC.m_renderer = textMeshC.m_go.GetComponent<MeshRenderer>();
		textMeshC.m_renderer.material = font.material;
		textMeshC.m_renderer.castShadows = false;
		textMeshC.m_renderer.receiveShadows = false;
		textMeshC.m_renderer.enabled = true;
		textMeshC.m_textboxWidth = _textboxWidth;
		textMeshC.m_textboxHeight = _textboxHeight;
		textMeshC.m_horizontalAlign = _horizontalAlign;
		textMeshC.m_verticalAlign = _verticalAlign;
		textMeshC.m_wasVisible = true;
		EntityManager.AddComponentToEntity(_tc.p_entity, textMeshC);
		return textMeshC;
	}

	// Token: 0x06002590 RID: 9616 RVA: 0x0019EFBD File Offset: 0x0019D3BD
	public static void SetCharSizeAndSpacing(TextMeshC _c, int _charSize, float _lineSpacing)
	{
		_c.m_textMesh.characterSize = (float)_charSize;
		_c.m_textMesh.lineSpacing = _lineSpacing;
	}

	// Token: 0x06002591 RID: 9617 RVA: 0x0019EFD8 File Offset: 0x0019D3D8
	public static void RemoveComponent(TextMeshC _c)
	{
		if (_c.p_entity == null)
		{
			Debug.LogWarning("Trying to remove component that has already been removed");
			return;
		}
		if (_c.m_textMesh != null)
		{
			Object.Destroy(_c.m_textMesh);
		}
		if (_c.m_renderer)
		{
			Object.Destroy(_c.m_renderer.material);
		}
		if (_c.m_go)
		{
			Object.Destroy(_c.m_go);
		}
		_c.m_renderer = null;
		_c.m_textMesh = null;
		_c.m_go = null;
		_c.m_TC = null;
		_c.m_wasVisible = false;
		EntityManager.RemoveComponentFromEntity(_c);
		TextMeshS.m_components.RemoveItem(_c);
	}

	// Token: 0x06002592 RID: 9618 RVA: 0x0019F088 File Offset: 0x0019D488
	public static void SetVisibilityByTransformComponent(TransformC _tc, bool _visible, bool _affectChildren = false, bool _affectWholeHierarchy = false)
	{
		if (_affectWholeHierarchy)
		{
			_tc = TransformS.GetRootTransformComponent(_tc);
		}
		if (_affectChildren || _affectWholeHierarchy)
		{
			for (int i = 0; i < _tc.childs.Count; i++)
			{
				TextMeshS.SetVisibilityByTransformComponent(_tc.childs[i], _visible, true, false);
			}
		}
		int aliveCount = TextMeshS.m_components.m_aliveCount;
		for (int j = 0; j < aliveCount; j++)
		{
			TextMeshC textMeshC = TextMeshS.m_components.m_array[TextMeshS.m_components.m_aliveIndices[j]];
			if (textMeshC.m_TC == _tc)
			{
				TextMeshS.SetVisibility(textMeshC, _visible, true);
			}
		}
	}

	// Token: 0x06002593 RID: 9619 RVA: 0x0019F128 File Offset: 0x0019D528
	public static void SetAlpha(TextMeshC _c, float _alpha)
	{
		Material material = _c.m_textMesh.transform.GetComponent<MeshRenderer>().material;
		Color color = material.GetColor("_Color");
		color.a = _alpha;
		material.SetColor("_Color", color);
	}

	// Token: 0x06002594 RID: 9620 RVA: 0x0019F16B File Offset: 0x0019D56B
	public static void SetVisibility(TextMeshC _c, bool _visible, bool _markVisibility = true)
	{
		_c.m_go.SetActive(_visible);
		if (_markVisibility)
		{
			_c.m_wasVisible = _visible;
		}
	}

	// Token: 0x06002595 RID: 9621 RVA: 0x0019F188 File Offset: 0x0019D588
	public static void FitTextToRect(TextMeshC _tmc, float _textboxWidth, float _textboxHeight, string _text, bool _fitTextToWidth = true)
	{
		_tmc.m_textboxWidth = _textboxWidth * ((float)TextMeshS.m_defaultCharacterSize / _tmc.m_textMesh.characterSize);
		_tmc.m_textboxHeight = _textboxHeight * ((float)TextMeshS.m_defaultCharacterSize / _tmc.m_textMesh.characterSize);
		int num = Mathf.FloorToInt(_tmc.m_textboxHeight);
		if (_fitTextToWidth && _tmc.m_textboxWidth > 0f)
		{
			Vector2 textSize = TextMeshS.GetTextSize(_tmc.m_textMesh.font, num, _tmc.m_textMesh.lineSpacing, _text);
			if (textSize.x > _tmc.m_textboxWidth)
			{
				float num2 = textSize.x / _tmc.m_textboxWidth;
				num = Mathf.FloorToInt(_tmc.m_textboxHeight / num2);
			}
		}
		else
		{
			num = Mathf.FloorToInt(_tmc.m_textboxHeight);
		}
		TextMeshS.SetFontSize(_tmc, (float)num);
		_tmc.m_textMesh.text = _text;
		_tmc.m_textWidth = _tmc.m_renderer.bounds.size.x;
		_tmc.m_textHeight = _tmc.m_renderer.bounds.size.y;
		TextMeshS.SetAlign(_tmc, _tmc.m_horizontalAlign, _tmc.m_verticalAlign, true);
	}

	// Token: 0x06002596 RID: 9622 RVA: 0x0019F2B6 File Offset: 0x0019D6B6
	public static void SetFontSize(TextMeshC _tmc, float _fontSizeINT)
	{
		_tmc.m_textMesh.fontSize = Mathf.FloorToInt(_fontSizeINT * ((float)TextMeshS.m_defaultCharacterSize / _tmc.m_textMesh.characterSize));
	}

	// Token: 0x06002597 RID: 9623 RVA: 0x0019F2DC File Offset: 0x0019D6DC
	public static void SetText(TextMeshC _tmc, string _text, bool _offsetGO = true)
	{
		_tmc.m_textMesh.text = _text;
		_tmc.m_textWidth = _tmc.m_renderer.bounds.size.x;
		_tmc.m_textHeight = _tmc.m_renderer.bounds.size.y;
		TextMeshS.SetAlign(_tmc, _tmc.m_horizontalAlign, _tmc.m_verticalAlign, _offsetGO);
	}

	// Token: 0x06002598 RID: 9624 RVA: 0x0019F34C File Offset: 0x0019D74C
	public static void SetTextOptimized(TextMeshC _tmc, string _text)
	{
		_tmc.m_textMesh.text = _text;
		_tmc.m_textWidth = _tmc.m_renderer.bounds.size.x;
		_tmc.m_textHeight = _tmc.m_renderer.bounds.size.y;
	}

	// Token: 0x06002599 RID: 9625 RVA: 0x0019F3A8 File Offset: 0x0019D7A8
	public static string CutTextToWidth(TextMeshC _tmc, string _text, float _width)
	{
		for (float num = TextMeshS.GetTextSize(_tmc, _text).x; num > _width; num = TextMeshS.GetTextSize(_tmc, _text).x)
		{
			int lastCharIndexWithoutTags = TextMeshS.GetLastCharIndexWithoutTags(_tmc, _text);
			if (lastCharIndexWithoutTags >= 0)
			{
				_text = _text.Remove(lastCharIndexWithoutTags, 1);
			}
		}
		return _text;
	}

	// Token: 0x0600259A RID: 9626 RVA: 0x0019F408 File Offset: 0x0019D808
	private static int GetLastCharIndexWithoutTags(TextMeshC _tmc, string _string)
	{
		bool flag = false;
		for (int i = _string.Length - 1; i >= 0; i--)
		{
			char c = _string.get_Chars(i);
			if (flag)
			{
				if (c == '<')
				{
					flag = false;
				}
			}
			else
			{
				if (c != '>')
				{
					return i;
				}
				flag = true;
			}
		}
		return -1;
	}

	// Token: 0x0600259B RID: 9627 RVA: 0x0019F460 File Offset: 0x0019D860
	public static void SetTextToTextbox(TextMeshC _tmc, float _textboxWidth, float _textboxHeight, string _text)
	{
		_tmc.m_textboxWidth = _textboxWidth * ((float)TextMeshS.m_defaultCharacterSize / _tmc.m_textMesh.characterSize);
		_tmc.m_textboxHeight = _textboxHeight * ((float)TextMeshS.m_defaultCharacterSize / _tmc.m_textMesh.characterSize);
		_tmc.m_textMesh.text = _text;
		_tmc.m_textWidth = _tmc.m_renderer.bounds.size.x;
		_tmc.m_textHeight = _tmc.m_renderer.bounds.size.y;
		TextMeshS.SetAlign(_tmc, _tmc.m_horizontalAlign, _tmc.m_verticalAlign, true);
	}

	// Token: 0x0600259C RID: 9628 RVA: 0x0019F504 File Offset: 0x0019D904
	public static void WrapTextToTextbox(TextMeshC _tmc, float _textboxWidth, float _textboxHeight, string _text, int _maxRows = -1)
	{
		_tmc.m_textboxWidth = _textboxWidth * ((float)TextMeshS.m_defaultCharacterSize / _tmc.m_textMesh.characterSize);
		_tmc.m_textboxHeight = _textboxHeight * ((float)TextMeshS.m_defaultCharacterSize / _tmc.m_textMesh.characterSize);
		_tmc.m_textMesh.text = TextMeshS.WrapText(_tmc, _text, _tmc.m_textboxWidth, _maxRows, false);
		_tmc.m_textWidth = _tmc.m_renderer.bounds.size.x;
		_tmc.m_textHeight = _tmc.m_renderer.bounds.size.y;
		TextMeshS.SetAlign(_tmc, _tmc.m_horizontalAlign, _tmc.m_verticalAlign, true);
	}

	// Token: 0x0600259D RID: 9629 RVA: 0x0019F5B8 File Offset: 0x0019D9B8
	public static void WrapTextToTextbox(TextMeshC _tmc, float _textboxWidth, float _textboxHeight, float _rowHeight, string _text, int _maxRows = -1)
	{
		_tmc.m_textboxWidth = _textboxWidth * ((float)TextMeshS.m_defaultCharacterSize / _tmc.m_textMesh.characterSize);
		_tmc.m_textboxHeight = _textboxHeight * ((float)TextMeshS.m_defaultCharacterSize / _tmc.m_textMesh.characterSize);
		TextMeshS.SetFontSize(_tmc, _rowHeight);
		_tmc.m_textMesh.text = TextMeshS.WrapText(_tmc, _text, _tmc.m_textboxWidth, _maxRows, false);
		_tmc.m_textWidth = _tmc.m_renderer.bounds.size.x;
		_tmc.m_textHeight = _tmc.m_renderer.bounds.size.y;
		TextMeshS.SetAlign(_tmc, _tmc.m_horizontalAlign, _tmc.m_verticalAlign, true);
	}

	// Token: 0x0600259E RID: 9630 RVA: 0x0019F671 File Offset: 0x0019DA71
	public static float GetHeightToWidthRatio(TextMeshC _tmc, string _text)
	{
		return TextMeshS.GetHeightToWidthRatio(_tmc.m_textMesh.font, _tmc.m_textMesh.fontSize, _tmc.m_textMesh.lineSpacing, _text);
	}

	// Token: 0x0600259F RID: 9631 RVA: 0x0019F69C File Offset: 0x0019DA9C
	public static float GetHeightToWidthRatio(Font _font, int _fontSize, float _lineSpacing, string _text)
	{
		Vector2 textSize = TextMeshS.GetTextSize(_font, _fontSize, _lineSpacing, _text);
		return textSize.y / textSize.x;
	}

	// Token: 0x060025A0 RID: 9632 RVA: 0x0019F6C2 File Offset: 0x0019DAC2
	public static Vector2 GetTextSize(TextMeshC _tmc, string _text)
	{
		return TextMeshS.GetTextSize(_tmc.m_textMesh.font, _tmc.m_textMesh.fontSize, _tmc.m_textMesh.lineSpacing, _text);
	}

	// Token: 0x060025A1 RID: 9633 RVA: 0x0019F6EC File Offset: 0x0019DAEC
	public static Vector2 GetTextSize(Font _font, int _fontSize, float _lineSpacing, string _text)
	{
		_font.RequestCharactersInTexture(_text, _fontSize);
		GUIStyle guistyle = new GUIStyle();
		guistyle.font = _font;
		guistyle.fontSize = _fontSize;
		char[] array = new char[] { '\n' };
		string[] array2 = _text.Split(array);
		float num = 0f;
		float num2 = 0f;
		float num3 = 0f;
		for (int i = 0; i < array2.Length; i++)
		{
			char[] array3 = array2[i].ToCharArray();
			bool flag = false;
			int num4 = 0;
			for (int j = 0; j < array3.Length - num4; j++)
			{
				char c = array3[j];
				if (flag)
				{
					if (c == '>')
					{
						flag = false;
					}
				}
				else if (c == '<')
				{
					flag = true;
				}
				else
				{
					CharacterInfo characterInfo;
					_font.GetCharacterInfo(c, ref characterInfo, _fontSize, 0);
					num += characterInfo.width;
				}
			}
			num2 = ((num2 <= num) ? num : num2);
			num3 += guistyle.lineHeight * _lineSpacing;
			num = 0f;
		}
		return new Vector2(num2, num3);
	}

	// Token: 0x060025A2 RID: 9634 RVA: 0x0019F7FD File Offset: 0x0019DBFD
	public static string WrapText(TextMeshC _tmc, string _text, float _width, int _maxRows = -1, bool _dots = false)
	{
		return TextMeshS.WrapText(_tmc.m_textMesh.font, _tmc.m_textMesh.fontSize, _tmc.m_textMesh.lineSpacing, _text, _width, _maxRows, _dots);
	}

	// Token: 0x060025A3 RID: 9635 RVA: 0x0019F82C File Offset: 0x0019DC2C
	public static string WrapText(Font _font, int _fontSize, float _lineSpacing, string _text, float _width, int _maxRows = -1, bool _dots = false)
	{
		if (_width == 0f || _text.Length <= 0 || (_maxRows >= 0 && _maxRows < 2 && !_dots))
		{
			return _text;
		}
		_font.RequestCharactersInTexture(_text, _fontSize);
		bool flag = false;
		string text = string.Empty;
		char[] array = new char[] { '\n' };
		string[] array2 = _text.Split(array);
		string text2 = string.Empty;
		float num = 0f;
		float num2 = 0f;
		int num3 = 1;
		for (int i = 0; i < array2.Length; i++)
		{
			string text3 = string.Empty;
			float num4 = 0f;
			float num5 = 0f;
			float num6 = 0f;
			char[] array3 = array2[i].ToCharArray();
			int num7 = array3.Length;
			foreach (char c in array3)
			{
				char c;
				if (flag)
				{
					num7--;
					if (c == '>')
					{
						flag = false;
					}
				}
				else if (c == '<')
				{
					num7--;
					flag = true;
				}
			}
			int num8 = 0;
			for (int k = 0; k < array3.Length; k++)
			{
				char c = array3[k];
				if (flag)
				{
					text += c.ToString();
					if (c == '>')
					{
						flag = false;
						text3 += text;
					}
					if (k == array3.Length - 1)
					{
						text2 += text3;
					}
				}
				else if (c == '<')
				{
					flag = true;
					text = string.Empty;
					text += c.ToString();
				}
				else
				{
					CharacterInfo characterInfo;
					_font.GetCharacterInfo(c, ref characterInfo, _fontSize, 0);
					float width = characterInfo.width;
					num5 = ((num5 <= -characterInfo.vert.height) ? (-characterInfo.vert.height) : num5);
					if (c == ' ' || num8 == num7 - 1)
					{
						if (c != ' ')
						{
							text3 += c.ToString();
							num4 += width;
						}
						if (num6 + num4 <= _width || num8 == num7 - 1)
						{
							num6 += num4;
							text2 += text3;
							text3 = string.Empty;
							num4 = 0f;
						}
					}
					if (num6 + num4 > _width && num7 - 1 - num8 >= 1)
					{
						num3++;
						if (_maxRows > 0 && num3 > _maxRows)
						{
							if (_dots)
							{
								if (_maxRows >= 0 && _maxRows < 2)
								{
									int num9 = Mathf.Min(text3.Length, 4);
									text3 = text3.Remove(text3.Length - num9);
									text3 += "...";
									return text2 + text3;
								}
								_font.GetCharacterInfo('.', ref characterInfo, _fontSize, 0);
								float width2 = characterInfo.width;
								if (width2 * 3f + num6 <= _width)
								{
									text2 += "...";
								}
								else
								{
									int num10 = text2.LastIndexOf(" ");
									if (num10 >= 0)
									{
										text2 = text2.Remove(num10);
										text2 += "...";
									}
								}
							}
							Debug.LogWarning(text2);
							return text2;
						}
						num2 = ((num2 <= num6) ? num6 : num2);
						num += num5 + _lineSpacing;
						if (text3.Contains(" "))
						{
							text2 += text3.Replace(" ", "\n");
							num6 = num4;
						}
						else if (text3.Contains("-"))
						{
							int num11 = text3.LastIndexOf('-');
							text2 += text3.Insert(num11 + 1, "\n");
							num6 = num4;
						}
						else if (text3.Length > 0)
						{
							text2 += text3.Insert(text3.Length - 1, "\n");
							num6 = width;
						}
						num5 = 0f;
						text3 = string.Empty;
						num4 = 0f;
					}
					if (num8 != num7 - 1)
					{
						text3 += c.ToString();
						num4 += width;
					}
					num8++;
				}
			}
			num2 = ((num2 <= num6) ? num6 : num2);
			num += num5 + _lineSpacing * 10f;
			if (_maxRows > -1 && i > _maxRows - 2)
			{
				return text2;
			}
			if (i < array2.Length - 1)
			{
				text2 += "\n";
			}
		}
		return text2;
	}

	// Token: 0x060025A4 RID: 9636 RVA: 0x0019FCE4 File Offset: 0x0019E0E4
	public static void SetAlign(TextMeshC _tmc, Align _horizontal, Align _vertical = Align.Top, bool _offsetGO = true)
	{
		_tmc.m_horizontalAlign = _horizontal;
		_tmc.m_verticalAlign = _vertical;
		Vector3 zero = Vector3.zero;
		if (_tmc.m_textboxWidth > 0f)
		{
			if (_horizontal == Align.Left)
			{
				zero.x = _tmc.m_textboxWidth * -0.5f;
			}
			else if (_horizontal == Align.Right)
			{
				zero.x = _tmc.m_textboxWidth * 0.5f;
			}
		}
		if (_tmc.m_textboxHeight > 0f)
		{
			if (_vertical == Align.Top)
			{
				zero.y = _tmc.m_textboxHeight * 0.5f;
			}
			else if (_vertical == Align.Bottom)
			{
				zero.y = _tmc.m_textboxHeight * -0.5f;
			}
		}
		if (_offsetGO)
		{
			_tmc.m_go.transform.localPosition = zero;
		}
		if (_horizontal == Align.Left && _vertical == Align.Top)
		{
			_tmc.m_textMesh.alignment = 0;
			_tmc.m_textMesh.anchor = 0;
		}
		else if (_horizontal == Align.Left && _vertical == Align.Middle)
		{
			_tmc.m_textMesh.alignment = 0;
			_tmc.m_textMesh.anchor = 3;
		}
		else if (_horizontal == Align.Left && _vertical == Align.Bottom)
		{
			_tmc.m_textMesh.alignment = 0;
			_tmc.m_textMesh.anchor = 6;
		}
		else if (_horizontal == Align.Center && _vertical == Align.Top)
		{
			_tmc.m_textMesh.alignment = 1;
			_tmc.m_textMesh.anchor = 1;
		}
		else if (_horizontal == Align.Center && _vertical == Align.Middle)
		{
			_tmc.m_textMesh.alignment = 1;
			_tmc.m_textMesh.anchor = 4;
		}
		else if (_horizontal == Align.Center && _vertical == Align.Bottom)
		{
			_tmc.m_textMesh.alignment = 1;
			_tmc.m_textMesh.anchor = 7;
		}
		else if (_horizontal == Align.Right && _vertical == Align.Top)
		{
			_tmc.m_textMesh.alignment = 2;
			_tmc.m_textMesh.anchor = 2;
		}
		else if (_horizontal == Align.Right && _vertical == Align.Middle)
		{
			_tmc.m_textMesh.alignment = 2;
			_tmc.m_textMesh.anchor = 5;
		}
		else if (_horizontal == Align.Right && _vertical == Align.Bottom)
		{
			_tmc.m_textMesh.alignment = 2;
			_tmc.m_textMesh.anchor = 8;
		}
	}

	// Token: 0x060025A5 RID: 9637 RVA: 0x0019FF28 File Offset: 0x0019E328
	public static void Update()
	{
		TextMeshS.m_components.Update();
	}

	// Token: 0x04002B28 RID: 11048
	public static DynamicArray<TextMeshC> m_components;

	// Token: 0x04002B29 RID: 11049
	public static GameObject m_emptyGameObject;

	// Token: 0x04002B2A RID: 11050
	public static int m_defaultCharacterSize = 10;
}
