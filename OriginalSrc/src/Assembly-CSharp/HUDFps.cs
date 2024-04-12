using System;
using System.Collections;
using UnityEngine;

// Token: 0x020005B5 RID: 1461
[AddComponentMenu("Utilities/HUDFps")]
public class HUDFps : MonoBehaviour
{
	// Token: 0x06002AA6 RID: 10918 RVA: 0x001BA8D4 File Offset: 0x001B8CD4
	private void Start()
	{
		base.StartCoroutine(this.FPS());
	}

	// Token: 0x06002AA7 RID: 10919 RVA: 0x001BA8E3 File Offset: 0x001B8CE3
	private void Update()
	{
		this.accum += Time.timeScale / Time.deltaTime;
		this.frames++;
	}

	// Token: 0x06002AA8 RID: 10920 RVA: 0x001BA90C File Offset: 0x001B8D0C
	private IEnumerator FPS()
	{
		for (;;)
		{
			float fps = this.accum / (float)this.frames;
			this.sFPS = fps.ToString("f" + Mathf.Clamp(this.nbDecimal, 0, 10));
			this.color = ((fps < 30f) ? ((fps <= 10f) ? Color.yellow : Color.red) : Color.green);
			this.accum = 0f;
			this.frames = 0;
			yield return new WaitForSeconds(this.frequency);
		}
		yield break;
	}

	// Token: 0x06002AA9 RID: 10921 RVA: 0x001BA928 File Offset: 0x001B8D28
	private void OnGUI()
	{
		if (this.style == null)
		{
			this.style = new GUIStyle(GUI.skin.label);
			this.style.normal.textColor = Color.white;
			this.style.alignment = 4;
		}
		GUI.color = ((!this.updateColor) ? Color.white : this.color);
		this.startRect = GUI.Window(0, this.startRect, new GUI.WindowFunction(this.DoMyWindow), string.Empty);
	}

	// Token: 0x06002AAA RID: 10922 RVA: 0x001BA9BC File Offset: 0x001B8DBC
	private void DoMyWindow(int windowID)
	{
		GUI.Label(new Rect(0f, 0f, this.startRect.width, this.startRect.height), this.sFPS + " FPS", this.style);
		if (this.allowDrag)
		{
			GUI.DragWindow(new Rect(0f, 0f, (float)Screen.width, (float)Screen.height));
		}
	}

	// Token: 0x04002FD0 RID: 12240
	public Rect startRect = new Rect(10f, 10f, 75f, 50f);

	// Token: 0x04002FD1 RID: 12241
	public bool updateColor = true;

	// Token: 0x04002FD2 RID: 12242
	public bool allowDrag = true;

	// Token: 0x04002FD3 RID: 12243
	public float frequency = 0.5f;

	// Token: 0x04002FD4 RID: 12244
	public int nbDecimal = 1;

	// Token: 0x04002FD5 RID: 12245
	private float accum;

	// Token: 0x04002FD6 RID: 12246
	private int frames;

	// Token: 0x04002FD7 RID: 12247
	private Color color = Color.white;

	// Token: 0x04002FD8 RID: 12248
	private string sFPS = string.Empty;

	// Token: 0x04002FD9 RID: 12249
	private GUIStyle style;
}
