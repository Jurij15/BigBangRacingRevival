using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020004AD RID: 1197
public class AutoGeometryProps
{
	// Token: 0x0600223B RID: 8763 RVA: 0x0018DD68 File Offset: 0x0018C168
	public AutoGeometryProps(AgTile _tile)
	{
		this.m_tileC = new AgTileC();
		this.m_tileC.m_tile = _tile;
		this.m_props = new List<AutoGeometryProps.AutoGeometryProp>();
		this.m_staticBody = ChipmunkProS.AddStaticBody(this.m_tileC.m_tile.TC, this.m_tileC);
		this.m_propStorage = _tile.agLayer.m_groundPropStorage;
	}

	// Token: 0x0600223C RID: 8764 RVA: 0x0018DDD0 File Offset: 0x0018C1D0
	public void GenerateRandomPropsForPolys(AgPolygon[] _polys)
	{
		foreach (AgPolygon agPolygon in _polys)
		{
			if (agPolygon.vertices.Count <= 0 || agPolygon.extraData.Count > 0)
			{
			}
		}
	}

	// Token: 0x0600223D RID: 8765 RVA: 0x0018DE18 File Offset: 0x0018C218
	public bool PointInsideGround(Vector3 _pos)
	{
		byte b = AutoGeometryManager.ReadMaxLayerValueFromWorldPos(_pos).b;
		return b >= 64 || this.m_tileC.m_tile.agLayer.ReadDataFromWorldPos(_pos) >= 64;
	}

	// Token: 0x0600223E RID: 8766 RVA: 0x0018DE68 File Offset: 0x0018C268
	public void AddTestProp(Vector3 _pos, Vector3 _normal, int _propIndex)
	{
		AutoGeometryProps.AutoGeometryProp autoGeometryProp = new AutoGeometryProps.AutoGeometryProp();
		autoGeometryProp.m_settings = this.m_propStorage.m_propSettingsLookup[_propIndex];
		autoGeometryProp.m_go = Object.Instantiate<GameObject>(autoGeometryProp.m_settings.gameObject);
		autoGeometryProp.m_go.transform.position = _pos + new Vector3(0f, 0f, -90f);
		autoGeometryProp.m_go.transform.rotation = Quaternion.LookRotation(_normal);
		autoGeometryProp.m_radius = autoGeometryProp.m_settings.m_radius;
		Vector2 vector = ChipmunkProWrapper.ucpBodyWorld2Local(this.m_staticBody.body, _pos);
		IntPtr intPtr = ChipmunkProWrapper.ucpCircleShapeNew(this.m_staticBody.body, autoGeometryProp.m_radius, vector, ucpCollisionType.None);
		ChipmunkProWrapper.ucpShapeSetLayers(intPtr, 268435456U);
		ChipmunkProWrapper.ucpSpaceAddShape(intPtr);
		autoGeometryProp.m_cpShape = intPtr;
		this.m_props.Add(autoGeometryProp);
	}

	// Token: 0x0600223F RID: 8767 RVA: 0x0018DF50 File Offset: 0x0018C350
	public void RemoveProps()
	{
		for (int i = this.m_props.Count - 1; i >= 0; i--)
		{
			this.RemoveProp(this.m_props[i]);
		}
	}

	// Token: 0x06002240 RID: 8768 RVA: 0x0018DF8D File Offset: 0x0018C38D
	public void Update(AgPolygon[] _polys)
	{
		this.GenerateRandomPropsForPolys(_polys);
	}

	// Token: 0x06002241 RID: 8769 RVA: 0x0018DF96 File Offset: 0x0018C396
	private void RemoveProp(AutoGeometryProps.AutoGeometryProp _prop)
	{
		Object.Destroy(_prop.m_go);
		ChipmunkProWrapper.ucpRemoveShape(_prop.m_cpShape);
		_prop.m_go = null;
		_prop.m_cpShape = IntPtr.Zero;
		this.m_props.Remove(_prop);
	}

	// Token: 0x06002242 RID: 8770 RVA: 0x0018DFCD File Offset: 0x0018C3CD
	public void Destroy()
	{
		this.RemoveProps();
		ChipmunkProS.RemoveBody(this.m_staticBody);
		this.m_staticBody = null;
		this.m_tileC.m_tile = null;
		this.m_tileC = null;
		this.m_props = null;
	}

	// Token: 0x04002871 RID: 10353
	public List<AutoGeometryProps.AutoGeometryProp> m_props;

	// Token: 0x04002872 RID: 10354
	public AgTileC m_tileC;

	// Token: 0x04002873 RID: 10355
	public ChipmunkBodyC m_staticBody;

	// Token: 0x04002874 RID: 10356
	public AGPropStorage m_propStorage;

	// Token: 0x020004AE RID: 1198
	public class AutoGeometryProp
	{
		// Token: 0x06002243 RID: 8771 RVA: 0x0018E001 File Offset: 0x0018C401
		public AutoGeometryProp()
		{
			this.m_go = null;
			this.m_cpShape = IntPtr.Zero;
			this.m_radius = 0f;
		}

		// Token: 0x04002875 RID: 10357
		public AGPropSettings m_settings;

		// Token: 0x04002876 RID: 10358
		public GameObject m_go;

		// Token: 0x04002877 RID: 10359
		public IntPtr m_cpShape;

		// Token: 0x04002878 RID: 10360
		public float m_radius;
	}
}
