using System;
using UnityEngine;

// Token: 0x02000195 RID: 405
public class EffectScanlines : MonoBehaviour
{
	// Token: 0x06000CE8 RID: 3304 RVA: 0x0007C3E4 File Offset: 0x0007A7E4
	private void Start()
	{
		this.scanlineObjects = new GameObject[this.scanlinesAmount];
		for (int i = 0; i < this.scanlinesAmount; i++)
		{
			this.scanlineObjects[i] = this.CreateScanlineObject(i + 1);
		}
		this.mainCamera = CameraS.m_mainCamera;
		if (this.mainCamera != null)
		{
			base.transform.parent = this.mainCamera.transform;
			base.transform.localPosition = new Vector3(0f, 0f, 25f);
			base.transform.localEulerAngles = Vector3.zero;
		}
	}

	// Token: 0x06000CE9 RID: 3305 RVA: 0x0007C48C File Offset: 0x0007A88C
	private GameObject CreateScanlineObject(int uvLine)
	{
		GameObject gameObject = new GameObject();
		gameObject.name = "Scanline" + uvLine;
		gameObject.transform.parent = base.transform;
		MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();
		MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
		meshRenderer.sharedMaterial = this.scanlineMaterial;
		Mesh mesh = new Mesh();
		Vector3[] array = new Vector3[]
		{
			new Vector3(base.transform.position.x + 25f, base.transform.position.y + 1f, base.transform.position.z),
			new Vector3(base.transform.position.x - 25f, base.transform.position.y + 1f, base.transform.position.z),
			new Vector3(base.transform.position.x + 25f, base.transform.position.y - 1f, base.transform.position.z),
			new Vector3(base.transform.position.x - 25f, base.transform.position.y - 1f, base.transform.position.z)
		};
		Vector2[] array2 = new Vector2[]
		{
			new Vector2(0f, (float)uvLine * 0.1f),
			new Vector2(1f, (float)uvLine * 0.1f),
			new Vector2(0f, 0.1f + (float)uvLine * 0.1f),
			new Vector2(1f, 0.1f + (float)uvLine * 0.1f)
		};
		int[] array3 = new int[] { 0, 1, 2, 2, 1, 3 };
		mesh.vertices = array;
		mesh.triangles = array3;
		mesh.uv = array2;
		meshFilter.mesh = mesh;
		return gameObject;
	}

	// Token: 0x06000CEA RID: 3306 RVA: 0x0007C714 File Offset: 0x0007AB14
	private void ShuffleScanlines()
	{
		foreach (GameObject gameObject in this.scanlineObjects)
		{
			gameObject.SetActive(true);
			if (Random.Range(0, 2) > 0)
			{
				gameObject.SetActive(false);
			}
			gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, Random.Range(-25f, 25f), gameObject.transform.localPosition.z);
		}
	}

	// Token: 0x06000CEB RID: 3307 RVA: 0x0007C7A4 File Offset: 0x0007ABA4
	private void Update()
	{
		this.scanlineUpdateTimer -= Main.GetDeltaTime();
		if (this.scanlineUpdateTimer < 0f)
		{
			this.ShuffleScanlines();
			this.scanlineUpdateTimer += 100f / this.scanlineUpdateRate * 0.01f;
		}
	}

	// Token: 0x04000E24 RID: 3620
	[Range(1f, 10f)]
	public int scanlinesAmount = 5;

	// Token: 0x04000E25 RID: 3621
	public float scanlineUpdateRate = 24f;

	// Token: 0x04000E26 RID: 3622
	private float scanlineUpdateTimer;

	// Token: 0x04000E27 RID: 3623
	public Material scanlineMaterial;

	// Token: 0x04000E28 RID: 3624
	private Camera mainCamera;

	// Token: 0x04000E29 RID: 3625
	private GameObject[] scanlineObjects;
}
