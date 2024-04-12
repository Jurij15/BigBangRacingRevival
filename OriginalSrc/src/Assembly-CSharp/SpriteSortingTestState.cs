using System;
using UnityEngine;

// Token: 0x020005BD RID: 1469
public class SpriteSortingTestState : BasicState
{
	// Token: 0x06002AE5 RID: 10981 RVA: 0x001BC090 File Offset: 0x001BA490
	public SpriteC createSprite(Vector2 pos, Color col)
	{
		float num = 10f;
		Entity entity = EntityManager.AddEntity("spriteEnt");
		TransformC transformC = TransformS.AddComponent(entity, "sprite");
		SpriteC spriteC = SpriteS.AddComponent(transformC, new Frame(0f, 0f, 5f, 5f), this.m_spriteSheet);
		SpriteS.SetDimensions(spriteC, num * 1f, num * 1f);
		SpriteS.SetColor(spriteC, col);
		TransformS.SetPosition(transformC, pos);
		return spriteC;
	}

	// Token: 0x06002AE6 RID: 10982 RVA: 0x001BC108 File Offset: 0x001BA508
	public override void Enter(IStatedObject _parent)
	{
		this.m_spriteSheet = FrameworkTestScene.m_spriteSheet;
		this.ticker = 0;
		this.dir = 1;
		this.spriteNum = 0;
	}

	// Token: 0x06002AE7 RID: 10983 RVA: 0x001BC12C File Offset: 0x001BA52C
	public override void Execute()
	{
		this.ticker++;
		if (this.ticker % 2 == 0)
		{
			Vector2 vector;
			vector..ctor((float)(-100 + this.spriteNum * 4 - this.spriteNum / 50 * 50 * 4), (float)(30 + this.spriteNum - this.spriteNum / 50 * 60));
			SpriteC spriteC = this.createSprite(vector, new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0.5f, 1f)));
			SpriteS.SetSortValue(spriteC, (float)(this.dir * this.spriteNum));
			SpriteS.SetVisibility(spriteC, this.dir > 0);
			this.spriteNum++;
		}
		if (this.ticker % 60 == 0)
		{
			this.dir = -this.dir;
			for (int i = 0; i < this.m_spriteSheet.m_components.m_aliveCount; i++)
			{
				int num = this.m_spriteSheet.m_components.m_aliveIndices[i];
				SpriteC spriteC2 = this.m_spriteSheet.m_components.m_array[num];
				SpriteS.SetSortValue(spriteC2, (float)(this.dir * i));
			}
		}
	}

	// Token: 0x06002AE8 RID: 10984 RVA: 0x001BC27A File Offset: 0x001BA67A
	public override void Exit()
	{
		EntityManager.RemoveAllEntities();
	}

	// Token: 0x04002FFA RID: 12282
	public SpriteSheet m_spriteSheet;

	// Token: 0x04002FFB RID: 12283
	private int ticker;

	// Token: 0x04002FFC RID: 12284
	private int dir;

	// Token: 0x04002FFD RID: 12285
	private int spriteNum;
}
