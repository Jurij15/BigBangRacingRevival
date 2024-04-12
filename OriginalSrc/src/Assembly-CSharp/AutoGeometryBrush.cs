using System;
using UnityEngine;

// Token: 0x020004A2 RID: 1186
public class AutoGeometryBrush
{
	// Token: 0x060021E9 RID: 8681 RVA: 0x00189ED8 File Offset: 0x001882D8
	public AutoGeometryBrush(float _brushSize, bool _rectBrush, float _flow = 0.5f, float _solidArea = 0f)
	{
		this.m_subPixelAccuracy = _brushSize < 3f && !_rectBrush;
		this.m_width = Mathf.FloorToInt(_brushSize * 2f);
		this.m_height = Mathf.FloorToInt(_brushSize * 2f);
		float num = Mathf.Floor((float)this.m_width * 0.5f);
		float num2 = Mathf.Floor((float)this.m_height * 0.5f);
		float num3 = (float)this.m_width * 0.5f;
		Vector2 zero = Vector2.zero;
		this.m_bytes = new byte[this.m_width * this.m_height];
		if (!_rectBrush)
		{
			for (int i = 0; i < this.m_height; i++)
			{
				zero.y = (float)i - num2;
				for (int j = 0; j < this.m_width; j++)
				{
					zero.x = (float)j - num;
					float magnitude = zero.magnitude;
					float num4 = num3 * _solidArea;
					float num5 = 1f - ToolBox.getPositionBetween(magnitude, num4, num3);
					byte b = (byte)(num5 * _flow * 255f);
					int num6 = i * this.m_width + j;
					this.m_bytes[num6] = b;
				}
			}
		}
		else
		{
			for (int k = 0; k < this.m_bytes.Length; k++)
			{
				this.m_bytes[k] = byte.MaxValue;
			}
		}
	}

	// Token: 0x060021EA RID: 8682 RVA: 0x0018A044 File Offset: 0x00188444
	public AutoGeometryBrush(string _resourceName, bool _useSubPixelAccuracy, bool _storeBytes = true)
	{
		this.m_subPixelAccuracy = _useSubPixelAccuracy;
		if (_resourceName != null)
		{
			this.m_texture = ResourceManager.GetTexture(_resourceName) as Texture2D;
			this.m_width = this.m_texture.width;
			this.m_height = this.m_texture.height;
			if (_storeBytes)
			{
				Color[] pixels = this.m_texture.GetPixels();
				this.m_bytes = new byte[pixels.Length];
				for (int i = 0; i < pixels.Length; i++)
				{
					this.m_bytes[i] = (byte)(pixels[i].a * 255f);
				}
			}
		}
		else
		{
			this.m_width = 1;
			this.m_height = 1;
			this.m_bytes = new byte[1];
			this.m_bytes[0] = byte.MaxValue;
		}
	}

	// Token: 0x060021EB RID: 8683 RVA: 0x0018A112 File Offset: 0x00188512
	public void Destroy()
	{
		this.m_bytes = null;
		this.m_texture = null;
	}

	// Token: 0x04002809 RID: 10249
	public int m_width;

	// Token: 0x0400280A RID: 10250
	public int m_height;

	// Token: 0x0400280B RID: 10251
	public byte[] m_bytes;

	// Token: 0x0400280C RID: 10252
	public bool m_subPixelAccuracy;

	// Token: 0x0400280D RID: 10253
	public Texture2D m_texture;
}
