using System;
using UnityEngine;

// Token: 0x020003BD RID: 957
public class PsUIStarProgressBar : UICanvas
{
	// Token: 0x06001B4F RID: 6991 RVA: 0x00130E44 File Offset: 0x0012F244
	public PsUIStarProgressBar(UIComponent _parent, int _currentValue, int _fullValue, string _tag = "")
		: base(_parent, false, _tag, null, string.Empty)
	{
		this.m_currentValue = _currentValue;
		this.m_fullValue = _fullValue;
		this.SetDrawHandler(new UIDrawDelegate(this.DrawHandler));
	}

	// Token: 0x06001B50 RID: 6992 RVA: 0x00130E76 File Offset: 0x0012F276
	public void SetValues(int _currentValue, int _fullValue)
	{
		this.m_currentValue = _currentValue;
		this.m_fullValue = _fullValue;
		if (this.m_parent != null)
		{
			this.m_parent.Update();
		}
		else
		{
			this.Update();
		}
	}

	// Token: 0x06001B51 RID: 6993 RVA: 0x00130EA7 File Offset: 0x0012F2A7
	public void Increase(bool _sparks)
	{
		this.Increase(_sparks, true);
	}

	// Token: 0x06001B52 RID: 6994 RVA: 0x00130EB4 File Offset: 0x0012F2B4
	public void Increase(bool _sparks, bool _sound)
	{
		if (_sparks)
		{
			TimerS.AddComponent(this.m_TC.p_entity, string.Empty, 0f, 0f, false, delegate(TimerC c)
			{
				TimerS.RemoveComponent(c);
				Vector3 vector = Vector3.right * (this.m_actualWidth / -2f + (this.m_starWidth + this.m_space) * (float)this.m_currentValue);
				PrefabC prefabC = PrefabS.AddComponent(this.m_TC, vector, ResourceManager.GetGameObject("StarReward3_GameObject"));
				PrefabS.SetCamera(prefabC, this.m_camera);
				Renderer[] componentsInChildren = prefabC.p_gameObject.transform.GetComponentsInChildren<Renderer>();
				if (componentsInChildren != null)
				{
					for (int i = 0; i < componentsInChildren.Length; i++)
					{
						if (componentsInChildren[i].material != null)
						{
							componentsInChildren[i].material.renderQueue = 2800;
						}
					}
				}
				ParticleSystem[] components = prefabC.p_gameObject.GetComponents<ParticleSystem>();
				for (int j = 0; j < components.Length; j++)
				{
					components[j].Play();
				}
				if (_sound)
				{
					SoundS.PlaySingleShot("/Metagame/CreatorRank_GainStar", Vector3.zero, 1f);
				}
			});
		}
		this.m_currentValue++;
		this.Update();
	}

	// Token: 0x06001B53 RID: 6995 RVA: 0x00130F1C File Offset: 0x0012F31C
	private new void DrawHandler(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		this.m_starWidth = _c.m_actualWidth / (float)this.m_fullValue;
		this.m_space = 0f;
		if (this.m_starWidth > _c.m_actualHeight)
		{
			this.m_starWidth = _c.m_actualHeight;
			this.m_space = _c.m_actualWidth / (float)this.m_fullValue - this.m_starWidth;
		}
		float num = -(_c.m_actualWidth - this.m_starWidth) / 2f;
		Frame frame = PsState.m_uiSheet.m_atlas.GetFrame("menu_creatorrank_star_filled", null);
		int i;
		for (i = 0; i < this.m_currentValue; i++)
		{
			SpriteC spriteC = SpriteS.AddComponent(_c.m_TC, frame, PsState.m_uiSheet);
			SpriteS.SetDimensions(spriteC, this.m_starWidth, this.m_starWidth);
			SpriteS.SetOffset(spriteC, new Vector3(num, 0f), 0f);
			num += this.m_starWidth + this.m_space;
		}
		Frame frame2 = PsState.m_uiSheet.m_atlas.GetFrame("menu_creatorrank_star_empty", null);
		for (i = i; i < this.m_fullValue; i++)
		{
			SpriteC spriteC2 = SpriteS.AddComponent(_c.m_TC, frame2, PsState.m_uiSheet);
			SpriteS.SetDimensions(spriteC2, this.m_starWidth, this.m_starWidth);
			SpriteS.SetOffset(spriteC2, new Vector3(num, 0f), 0f);
			num += this.m_starWidth + this.m_space;
		}
		SpriteS.ConvertSpritesToPrefabComponent(_c.m_TC, _c.m_camera, true, null);
	}

	// Token: 0x04001DB0 RID: 7600
	private int m_fullValue;

	// Token: 0x04001DB1 RID: 7601
	private int m_currentValue;

	// Token: 0x04001DB2 RID: 7602
	private float m_starWidth;

	// Token: 0x04001DB3 RID: 7603
	private float m_space;
}
