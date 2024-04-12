using System;
using UnityEngine;

// Token: 0x02000509 RID: 1289
public class SpriteSheet : IPoolable
{
	// Token: 0x0600255A RID: 9562 RVA: 0x0019CC16 File Offset: 0x0019B016
	public SpriteSheet()
	{
		this.CreateSpriteSheet(CameraS.m_uiCamera, null, null, 1f, 20);
	}

	// Token: 0x0600255B RID: 9563 RVA: 0x0019CC3C File Offset: 0x0019B03C
	public SpriteSheet(Camera _camera, Texture _texture, Shader _shader, float _globalSpriteScale)
	{
		this.CreateSpriteSheet(_camera, new Material(_shader)
		{
			mainTexture = _texture
		}, null, _globalSpriteScale, 100);
	}

	// Token: 0x0600255C RID: 9564 RVA: 0x0019CC71 File Offset: 0x0019B071
	public SpriteSheet(Camera _camera, Material _material, float _globalSpriteScale)
	{
		this.CreateSpriteSheet(_camera, _material, null, _globalSpriteScale, 100);
	}

	// Token: 0x0600255D RID: 9565 RVA: 0x0019CC8C File Offset: 0x0019B08C
	public SpriteSheet(Camera _camera, Material _material, TextAsset _atlas, float _globalSpriteScale)
	{
		this.CreateSpriteSheet(_camera, _material, _atlas, _globalSpriteScale, 100);
	}

	// Token: 0x170000C0 RID: 192
	// (get) Token: 0x0600255E RID: 9566 RVA: 0x0019CCA8 File Offset: 0x0019B0A8
	// (set) Token: 0x0600255F RID: 9567 RVA: 0x0019CCB0 File Offset: 0x0019B0B0
	public int m_index
	{
		get
		{
			return this._index;
		}
		set
		{
			this._index = value;
		}
	}

	// Token: 0x06002560 RID: 9568 RVA: 0x0019CCB9 File Offset: 0x0019B0B9
	public void Reset()
	{
		this.m_currentMeshCapacity = 0;
		this.setMeshData(0);
	}

	// Token: 0x06002561 RID: 9569 RVA: 0x0019CCCC File Offset: 0x0019B0CC
	private void CreateSpriteSheet(Camera _camera, Material _material, TextAsset _atlas, float _globalSpriteScale, int _spritePoolSize = 100)
	{
		if (_atlas != null)
		{
			this.m_atlas = new TextureAtlas(_atlas);
		}
		this.m_camera = _camera;
		this.m_gameObject = new GameObject("SpriteSheet");
		this.m_gameObject.layer = this.m_camera.gameObject.layer;
		this.m_meshFilter = this.m_gameObject.AddComponent<MeshFilter>();
		this.m_meshRenderer = this.m_gameObject.AddComponent<MeshRenderer>();
		this.m_material = _material;
		this.m_textureWidth = 0;
		this.m_textureHeight = 0;
		if (_material != null && _material.mainTexture != null)
		{
			this.m_textureWidth = _material.mainTexture.width;
			this.m_textureHeight = _material.mainTexture.height;
		}
		this.m_mesh = this.m_meshFilter.mesh;
		this.m_mesh.MarkDynamic();
		this.m_meshRenderer.bounds.SetMinMax(new Vector3(float.MinValue, float.MinValue, float.MinValue), new Vector3(float.MaxValue, float.MaxValue, float.MaxValue));
		this.m_meshRenderer.GetComponent<Renderer>().material = this.m_material;
		this.m_globalSpriteScale = _globalSpriteScale;
		this.m_gameObject.transform.position = Vector3.zero;
		this.m_components = new DynamicArray<SpriteC>(100, 0.5f, 0.25f, 0.5f);
		this.m_currentMeshCapacity = 0;
		this.setMeshData(0);
	}

	// Token: 0x06002562 RID: 9570 RVA: 0x0019CE50 File Offset: 0x0019B250
	public void SetMaterial(Material _material)
	{
		this.m_material = _material;
		if (_material.mainTexture != null)
		{
			this.m_textureWidth = _material.mainTexture.width;
			this.m_textureHeight = _material.mainTexture.height;
		}
		this.m_meshRenderer.GetComponent<Renderer>().material = this.m_material;
	}

	// Token: 0x06002563 RID: 9571 RVA: 0x0019CEAD File Offset: 0x0019B2AD
	public void SetAtlas(TextAsset _atlas)
	{
		if (_atlas != null)
		{
			this.m_atlas = new TextureAtlas(_atlas);
		}
	}

	// Token: 0x06002564 RID: 9572 RVA: 0x0019CEC7 File Offset: 0x0019B2C7
	public void SetAtlas(TextureAtlas _atlas)
	{
		if (_atlas != null)
		{
			this.m_atlas = _atlas;
		}
	}

	// Token: 0x06002565 RID: 9573 RVA: 0x0019CED6 File Offset: 0x0019B2D6
	public void SetCamera(Camera _camera)
	{
		this.m_camera = _camera;
		this.m_gameObject.layer = this.m_camera.gameObject.layer;
	}

	// Token: 0x06002566 RID: 9574 RVA: 0x0019CEFC File Offset: 0x0019B2FC
	public void setMeshData(int _capacity)
	{
		this.m_vertices = new Vector3[_capacity * 4];
		for (int i = 0; i < this.m_vertices.Length; i++)
		{
			this.m_vertices[i] = Vector3.zero;
		}
		this.m_normals = new Vector3[_capacity * 4];
		this.m_UVs = new Vector2[_capacity * 4];
		this.m_colors = new Color[_capacity * 4];
		this.m_triIndices = new int[_capacity * 6];
		this.m_freeVertexIndices = new int[_capacity];
		this.m_freeVertexIndicesCount = _capacity;
		for (int j = 0; j < _capacity; j++)
		{
			this.m_triIndices[j * 6 + 5] = j * 4;
			this.m_triIndices[j * 6 + 4] = j * 4 + 1;
			this.m_triIndices[j * 6 + 3] = j * 4 + 3;
			this.m_triIndices[j * 6 + 2] = j * 4 + 3;
			this.m_triIndices[j * 6 + 1] = j * 4 + 1;
			this.m_triIndices[j * 6] = j * 4 + 2;
			this.m_freeVertexIndices[j] = j;
		}
		this.m_mesh.triangles = null;
		this.m_mesh.vertices = null;
		this.m_mesh.vertices = this.m_vertices;
		this.m_mesh.triangles = this.m_triIndices;
		this.m_mesh.uv = this.m_UVs;
		this.m_mesh.colors = this.m_colors;
		this.m_mesh.normals = this.m_normals;
		this.m_mesh.bounds = new Bounds(Vector3.zero, new Vector3(99999f, 99999f, 99999f));
		this.m_meshCapacityChanged = true;
		this.m_currentMeshCapacity = _capacity;
	}

	// Token: 0x06002567 RID: 9575 RVA: 0x0019D0B4 File Offset: 0x0019B4B4
	public void resetVertexIndices()
	{
		this.m_freeVertexIndicesCount = this.m_currentMeshCapacity;
		for (int i = 0; i < this.m_freeVertexIndicesCount; i++)
		{
			this.m_freeVertexIndices[i] = i;
		}
	}

	// Token: 0x06002568 RID: 9576 RVA: 0x0019D0F0 File Offset: 0x0019B4F0
	public void assignVertexDataIndices()
	{
		for (int i = 0; i < this.m_components.m_aliveCount; i++)
		{
			int num = this.m_components.m_aliveIndices[i];
			SpriteC spriteC = this.m_components.m_array[num];
			spriteC.vertDataIndex = this.m_freeVertexIndices[this.m_freeVertexIndicesCount - 1];
			this.m_freeVertexIndicesCount--;
		}
	}

	// Token: 0x06002569 RID: 9577 RVA: 0x0019D158 File Offset: 0x0019B558
	public void Destroy()
	{
		SpriteS.RemoveAllComponentsFromSheet(this);
		Object.Destroy(this.m_meshRenderer.GetComponent<Renderer>().material);
	}

	// Token: 0x04002B03 RID: 11011
	private int _index;

	// Token: 0x04002B04 RID: 11012
	public DynamicArray<SpriteC> m_components;

	// Token: 0x04002B05 RID: 11013
	public Camera m_camera;

	// Token: 0x04002B06 RID: 11014
	public GameObject m_gameObject;

	// Token: 0x04002B07 RID: 11015
	public Mesh m_mesh;

	// Token: 0x04002B08 RID: 11016
	public MeshFilter m_meshFilter;

	// Token: 0x04002B09 RID: 11017
	public MeshRenderer m_meshRenderer;

	// Token: 0x04002B0A RID: 11018
	public Material m_material;

	// Token: 0x04002B0B RID: 11019
	public int m_textureWidth;

	// Token: 0x04002B0C RID: 11020
	public int m_textureHeight;

	// Token: 0x04002B0D RID: 11021
	public float m_globalSpriteScale;

	// Token: 0x04002B0E RID: 11022
	public int m_currentMeshCapacity;

	// Token: 0x04002B0F RID: 11023
	public int[] m_freeVertexIndices;

	// Token: 0x04002B10 RID: 11024
	public int m_freeVertexIndicesCount;

	// Token: 0x04002B11 RID: 11025
	public Vector3[] m_vertices;

	// Token: 0x04002B12 RID: 11026
	public Vector3[] m_normals;

	// Token: 0x04002B13 RID: 11027
	public Vector2[] m_UVs;

	// Token: 0x04002B14 RID: 11028
	public Color[] m_colors;

	// Token: 0x04002B15 RID: 11029
	public int[] m_triIndices;

	// Token: 0x04002B16 RID: 11030
	public bool m_meshCapacityChanged;

	// Token: 0x04002B17 RID: 11031
	public bool m_vertsChanged;

	// Token: 0x04002B18 RID: 11032
	public bool m_uvsChanged;

	// Token: 0x04002B19 RID: 11033
	public bool m_colorsChanged;

	// Token: 0x04002B1A RID: 11034
	public bool m_sortMesh;

	// Token: 0x04002B1B RID: 11035
	public bool m_sortingEnabled = true;

	// Token: 0x04002B1C RID: 11036
	public TextureAtlas m_atlas;

	// Token: 0x04002B1D RID: 11037
	public static Color m_defaultColor = new Color(0.5f, 0.5f, 0.5f, 1f);
}
