using System;
using UnityEngine;

// Token: 0x020005BB RID: 1467
public class MassCreateTestState : BasicState
{
	// Token: 0x06002AD9 RID: 10969 RVA: 0x001BBE54 File Offset: 0x001BA254
	public SpriteC createSprite(Vector2 pos, Color col, string tag)
	{
		float num = 10f;
		Entity entity = EntityManager.AddEntity(tag);
		TransformC transformC = TransformS.AddComponent(entity, "sprite");
		SpriteC spriteC = SpriteS.AddComponent(transformC, new Frame(0f, 0f, 5f, 5f), this.m_spriteSheet);
		SpriteS.SetDimensions(spriteC, num * 1f, num * 1f);
		SpriteS.SetColor(spriteC, col);
		TransformS.SetPosition(transformC, pos);
		return spriteC;
	}

	// Token: 0x06002ADA RID: 10970 RVA: 0x001BBEC8 File Offset: 0x001BA2C8
	public override void Enter(IStatedObject _parent)
	{
		this.m_spriteSheet = FrameworkTestScene.m_spriteSheet;
		this.ticker = 0;
	}

	// Token: 0x06002ADB RID: 10971 RVA: 0x001BBEDC File Offset: 0x001BA2DC
	public override void Execute()
	{
		this.ticker++;
		if (this.ticker % 2 == 0)
		{
			for (int i = 0; i < 100; i++)
			{
				Vector2 vector;
				vector..ctor((float)Random.Range(-50, 50), (float)Random.Range(-50, 50));
				this.createSprite(vector, new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0.5f, 1f)), "SmallStack");
			}
		}
		else if (this.ticker % 2 == 1)
		{
			EntityManager.RemoveEntitiesByTag("SmallStack");
		}
		if (this.ticker % 200 == 0)
		{
			for (int j = 0; j < 500; j++)
			{
				Vector2 vector2;
				vector2..ctor((float)Random.Range(-200, 200), (float)Random.Range(-200, 200));
				this.createSprite(vector2, new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0.5f, 1f)), "BigStack");
			}
		}
		else if (this.ticker % 200 == 100)
		{
			EntityManager.RemoveEntitiesByTag("BigStack");
		}
	}

	// Token: 0x06002ADC RID: 10972 RVA: 0x001BC05B File Offset: 0x001BA45B
	public override void Exit()
	{
		EntityManager.RemoveAllEntities();
	}

	// Token: 0x04002FF8 RID: 12280
	public SpriteSheet m_spriteSheet;

	// Token: 0x04002FF9 RID: 12281
	private int ticker;
}
