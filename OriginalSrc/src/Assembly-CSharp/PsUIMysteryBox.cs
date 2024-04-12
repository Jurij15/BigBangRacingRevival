using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000354 RID: 852
public class PsUIMysteryBox : UIScrollableCanvas
{
	// Token: 0x060018E4 RID: 6372 RVA: 0x0010F344 File Offset: 0x0010D744
	public PsUIMysteryBox()
		: base(null, "Gacha")
	{
		this.m_duration = 0f;
		this.m_selectionDuration = 1.5f;
		this.m_items = new List<TransformC>();
		this.m_spriteSheet = SpriteS.AddSpriteSheet(this.m_camera, ResourceManager.GetMaterial(RESOURCE.UiAtlasMat_Material), ResourceManager.GetTextAsset(RESOURCE.UiAtlas_TextAsset), 1f);
		this.m_frames = new List<Frame>();
		this.m_frames.Add(this.m_spriteSheet.m_atlas.GetFrame("menu_resources_key_icon", null));
		this.m_frames.Add(this.m_spriteSheet.m_atlas.GetFrame("menu_resources_coin_icon", null));
		this.m_frames.Add(this.m_spriteSheet.m_atlas.GetFrame("menu_resources_diamond_icon", null));
		this.m_frames.Add(this.m_spriteSheet.m_atlas.GetFrame("menu_resources_bolt_icon", null));
		this.m_lastRandomFrame = 0;
		this.m_selectedFrame = Random.Range(0, this.m_frames.Count);
		this.m_speed = -0.015f;
		this.SetSize(0.2f, 0.3f, RelativeTo.ScreenHeight);
		this.SetAlign(0.05f, 0.75f);
		this.RemoveTouchAreas();
		this.Update();
	}

	// Token: 0x060018E5 RID: 6373 RVA: 0x0010F490 File Offset: 0x0010D890
	private Frame GetRandomFrame()
	{
		int num;
		for (num = Random.Range(0, this.m_frames.Count); num == this.m_lastRandomFrame; num = Random.Range(0, this.m_frames.Count))
		{
		}
		this.m_lastRandomFrame = num;
		return this.m_frames[num];
	}

	// Token: 0x060018E6 RID: 6374 RVA: 0x0010F4E8 File Offset: 0x0010D8E8
	public override void Step()
	{
		this.m_currentTime += Main.m_dt;
		if (this.m_currentTime >= this.m_duration && this.m_speed == 0f)
		{
			this.Destroy();
			return;
		}
		if (this.m_speed == 0f)
		{
			return;
		}
		bool flag = false;
		if (this.m_currentTime >= this.m_selectionDuration)
		{
			flag = true;
		}
		while (this.m_items.Count < 5)
		{
			Entity entity = EntityManager.AddEntity();
			entity.m_persistent = true;
			TransformC transformC = TransformS.AddComponent(entity);
			TransformS.ParentComponent(transformC, this.m_TC);
			Frame randomFrame = this.GetRandomFrame();
			transformC.m_identifier = this.m_lastRandomFrame;
			SpriteC spriteC = SpriteS.AddComponent(transformC, randomFrame, this.m_spriteSheet);
			SpriteS.SetDimensions(spriteC, (float)Screen.height * 0.15f, (float)Screen.height * 0.15f * (randomFrame.height / randomFrame.width));
			if (this.m_items.Count == 0)
			{
				TransformS.SetPosition(transformC, Vector3.up * (float)Screen.height * -0.02f);
			}
			else
			{
				TransformS.SetPosition(transformC, this.m_items[this.m_items.Count - 1].transform.localPosition + Vector3.up * (float)Screen.height * 0.2f);
			}
			this.m_items.Add(transformC);
		}
		for (int i = this.m_items.Count - 1; i > -1; i--)
		{
			TransformS.Move(this.m_items[i], Vector3.up * (float)Screen.height * this.m_speed);
			if (flag && this.m_items[i].m_identifier == this.m_selectedFrame && this.m_items[i].transform.localPosition.y < 0f && this.m_items[i].transform.localPosition.y > (float)Screen.height * -0.02f)
			{
				this.m_speed = 0f;
				this.m_duration = this.m_currentTime + 1f;
			}
			else if (this.m_items[i].transform.localPosition.y < (float)Screen.height * -0.3f)
			{
				EntityManager.RemoveEntity(this.m_items[i].p_entity);
				this.m_items.RemoveAt(i);
			}
		}
		base.Step();
	}

	// Token: 0x060018E7 RID: 6375 RVA: 0x0010F7AC File Offset: 0x0010DBAC
	public override void Destroy()
	{
		base.Destroy();
		while (this.m_items.Count > 0)
		{
			EntityManager.RemoveEntity(this.m_items[0].p_entity);
			this.m_items.RemoveAt(0);
		}
	}

	// Token: 0x04001B74 RID: 7028
	private float m_duration;

	// Token: 0x04001B75 RID: 7029
	private float m_selectionDuration;

	// Token: 0x04001B76 RID: 7030
	private float m_currentTime;

	// Token: 0x04001B77 RID: 7031
	private List<TransformC> m_items;

	// Token: 0x04001B78 RID: 7032
	private List<Frame> m_frames;

	// Token: 0x04001B79 RID: 7033
	private SpriteSheet m_spriteSheet;

	// Token: 0x04001B7A RID: 7034
	private int m_lastRandomFrame;

	// Token: 0x04001B7B RID: 7035
	private int m_selectedFrame;

	// Token: 0x04001B7C RID: 7036
	private float m_speed;
}
