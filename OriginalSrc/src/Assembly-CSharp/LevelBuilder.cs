using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000091 RID: 145
public class LevelBuilder : MonoBehaviour
{
	// Token: 0x0600031B RID: 795 RVA: 0x0002FE1C File Offset: 0x0002E21C
	public void InitializeBackground(int _seed, float _domeWidth)
	{
		Debug.Log("LevelBuilder Dome width: " + _domeWidth, null);
		Random.seed = _seed;
		foreach (GameObject gameObject in this.spawnedObjects)
		{
			Object.Destroy(gameObject);
		}
		this.spawnedObjects.Clear();
		for (int i = 0; i < this.levelObjects.Length; i++)
		{
			levelBuilderObject levelBuilderObject = this.levelObjects[i];
			int num = Random.Range(levelBuilderObject.minAmount, levelBuilderObject.maxAmount);
			for (int j = 0; j < num; j++)
			{
				float num2 = Random.Range(levelBuilderObject.minX, levelBuilderObject.maxX);
				float num3 = Random.Range(levelBuilderObject.minZ, levelBuilderObject.maxZ);
				float num4 = Random.Range(levelBuilderObject.minHeight, levelBuilderObject.maxHeight);
				float num5 = Random.Range(levelBuilderObject.minScale, levelBuilderObject.maxScale);
				bool flag = true;
				GameObject gameObject2 = Object.Instantiate<GameObject>(levelBuilderObject.levelObject);
				gameObject2.transform.parent = base.transform;
				gameObject2.transform.localPosition = new Vector3(num2, num4, num3);
				gameObject2.transform.localEulerAngles = new Vector3(gameObject2.transform.localEulerAngles.x, gameObject2.transform.localEulerAngles.y + Random.Range(levelBuilderObject.rotationOffset, -levelBuilderObject.rotationOffset), gameObject2.transform.localEulerAngles.z);
				if (levelBuilderObject.allowMirror && Random.value > 0.5f)
				{
					gameObject2.transform.localScale = new Vector3(num5 * -1f, num5, num5);
				}
				else
				{
					gameObject2.transform.localScale = new Vector3(num5, num5, num5);
				}
				foreach (GameObject gameObject3 in this.spawnedObjects)
				{
					if (gameObject3.GetComponent<Collider>().bounds.Intersects(gameObject2.GetComponent<Collider>().bounds) && this.levelObjects != null)
					{
						flag = false;
						Object.Destroy(gameObject2);
					}
				}
				if (flag)
				{
					this.spawnedObjects.Add(gameObject2);
				}
				foreach (GameObject gameObject4 in this.spawnedObjects)
				{
					Object.Destroy(gameObject4.GetComponent<Collider>());
				}
			}
		}
	}

	// Token: 0x04000401 RID: 1025
	public levelBuilderObject[] levelObjects;

	// Token: 0x04000402 RID: 1026
	public List<GameObject> spawnedObjects;
}
