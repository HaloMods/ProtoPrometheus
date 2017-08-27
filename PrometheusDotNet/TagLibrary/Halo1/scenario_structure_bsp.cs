// ------------------------------------------------------
// Prometheus Tag Library - Halo1
// Tag definition for 'ScenarioStructureBsp'
// Generated on 12/16/2005 at 5:18 PM by ARETE_AZ\jcleary
// ------------------------------------------------------
namespace TagLibrary.Halo1
{
  using System.IO;
  using TagLibrary.Types;
  
  public class ScenarioStructureBsp : IBlock
  {
    private ScenarioStructureBspBlock ScenarioStructureBspValues = new ScenarioStructureBspBlock();
    public virtual void Read(BinaryReader reader)
    {
      ScenarioStructureBspValues.Read(reader);
    }
    public virtual void ReadChildData(BinaryReader reader)
    {
      ScenarioStructureBspValues.ReadChildData(reader);
    }
    public class ScenarioStructureBspBlock : IBlock
    {
      private TagReference _lightmaps = new TagReference();
      private Real _vehicleFloor = new Real();
      private Real _vehicleCeiling = new Real();
      private Pad _unnamed;
      private RealRGBColor _defaultAmbientColor = new RealRGBColor();
      private Pad _unnamed2;
      private RealRGBColor _defaultDistantLight0Color = new RealRGBColor();
      private RealVector3D _defaultDistantLight0Direction = new RealVector3D();
      private RealRGBColor _defaultDistantLight1Color = new RealRGBColor();
      private RealVector3D _defaultDistantLight1Direction = new RealVector3D();
      private Pad _unnamed3;
      private RealARGBColor _defaultReflectionTint = new RealARGBColor();
      private RealVector3D _defaultShadowVector = new RealVector3D();
      private RealRGBColor _defaultShadowColor = new RealRGBColor();
      private Pad _unnamed4;
      private Block _collisionMaterials = new Block();
      private Block _collisionBsp = new Block();
      private Block _nodes = new Block();
      private RealBounds _worldBoundsX = new RealBounds();
      private RealBounds _worldBoundsY = new RealBounds();
      private RealBounds _worldBoundsZ = new RealBounds();
      private Block _leaves = new Block();
      private Block _leafSurfaces = new Block();
      private Block _surfaces = new Block();
      private Block _lightmaps2 = new Block();
      private Pad _unnamed5;
      private Block _lensFlares = new Block();
      private Block _lensFlareMarkers = new Block();
      private Block _clusters = new Block();
      private Data _clusterData = new Data();
      private Block _clusterPortals = new Block();
      private Pad _unnamed6;
      private Block _breakableSurfaces = new Block();
      private Block _fogPlanes = new Block();
      private Block _fogRegions = new Block();
      private Block _fogPalette = new Block();
      private Pad _unnamed7;
      private Block _weatherPalette = new Block();
      private Block _weatherPolyhedra = new Block();
      private Pad _unnamed8;
      private Block _pathfindingSurfaces = new Block();
      private Block _pathfindingEdges = new Block();
      private Block _backgroundSoundPalette = new Block();
      private Block _soundEnvironmentPalette = new Block();
      private Data _soundPASData = new Data();
      private Pad _unnamed9;
      private Block _markers = new Block();
      private Block _detailObjects = new Block();
      private Block _runtimeDecals = new Block();
      private Pad _unnamed10;
      private Pad _unnamed11;
      private Block _leafMapLeaves = new Block();
      private Block _leafMapPortals = new Block();
      private StructureCollisionMaterialsBlockCollection _collisionMaterialsCollection;
      private BspBlockCollection _collisionBspCollection;
      private StructureBspNodeBlockCollection _nodesCollection;
      private StructureBspLeafBlockCollection _leavesCollection;
      private StructureBspSurfaceReferenceBlockCollection _leafSurfacesCollection;
      private StructureBspSurfaceBlockCollection _surfacesCollection;
      private StructureBspLightmapBlockCollection _lightmaps2Collection;
      private StructureBspLensFlareBlockCollection _lensFlaresCollection;
      private StructureBspLensFlareMarkerBlockCollection _lensFlareMarkersCollection;
      private StructureBspClusterBlockCollection _clustersCollection;
      private StructureBspClusterPortalBlockCollection _clusterPortalsCollection;
      private StructureBspBreakableSurfaceBlockCollection _breakableSurfacesCollection;
      private StructureBspFogPlaneBlockCollection _fogPlanesCollection;
      private StructureBspFogRegionBlockCollection _fogRegionsCollection;
      private StructureBspFogPaletteBlockCollection _fogPaletteCollection;
      private StructureBspWeatherPaletteBlockCollection _weatherPaletteCollection;
      private StructureBspWeatherPolyhedronBlockCollection _weatherPolyhedraCollection;
      private StructureBspPathfindingSurfacesBlockCollection _pathfindingSurfacesCollection;
      private StructureBspPathfindingEdgesBlockCollection _pathfindingEdgesCollection;
      private StructureBspBackgroundSoundPaletteBlockCollection _backgroundSoundPaletteCollection;
      private StructureBspSoundEnvironmentPaletteBlockCollection _soundEnvironmentPaletteCollection;
      private StructureBspMarkerBlockCollection _markersCollection;
      private StructureBspDetailObjectDataBlockCollection _detailObjectsCollection;
      private StructureBspRuntimeDecalBlockCollection _runtimeDecalsCollection;
      private GlobalMapLeafBlockCollection _leafMapLeavesCollection;
      private GlobalLeafPortalBlockCollection _leafMapPortalsCollection;
      public ScenarioStructureBspBlock()
      {
        this._collisionMaterialsCollection = new StructureCollisionMaterialsBlockCollection(this._collisionMaterials);
        this._collisionBspCollection = new BspBlockCollection(this._collisionBsp);
        this._nodesCollection = new StructureBspNodeBlockCollection(this._nodes);
        this._leavesCollection = new StructureBspLeafBlockCollection(this._leaves);
        this._leafSurfacesCollection = new StructureBspSurfaceReferenceBlockCollection(this._leafSurfaces);
        this._surfacesCollection = new StructureBspSurfaceBlockCollection(this._surfaces);
        this._lightmaps2Collection = new StructureBspLightmapBlockCollection(this._lightmaps2);
        this._lensFlaresCollection = new StructureBspLensFlareBlockCollection(this._lensFlares);
        this._lensFlareMarkersCollection = new StructureBspLensFlareMarkerBlockCollection(this._lensFlareMarkers);
        this._clustersCollection = new StructureBspClusterBlockCollection(this._clusters);
        this._clusterPortalsCollection = new StructureBspClusterPortalBlockCollection(this._clusterPortals);
        this._breakableSurfacesCollection = new StructureBspBreakableSurfaceBlockCollection(this._breakableSurfaces);
        this._fogPlanesCollection = new StructureBspFogPlaneBlockCollection(this._fogPlanes);
        this._fogRegionsCollection = new StructureBspFogRegionBlockCollection(this._fogRegions);
        this._fogPaletteCollection = new StructureBspFogPaletteBlockCollection(this._fogPalette);
        this._weatherPaletteCollection = new StructureBspWeatherPaletteBlockCollection(this._weatherPalette);
        this._weatherPolyhedraCollection = new StructureBspWeatherPolyhedronBlockCollection(this._weatherPolyhedra);
        this._pathfindingSurfacesCollection = new StructureBspPathfindingSurfacesBlockCollection(this._pathfindingSurfaces);
        this._pathfindingEdgesCollection = new StructureBspPathfindingEdgesBlockCollection(this._pathfindingEdges);
        this._backgroundSoundPaletteCollection = new StructureBspBackgroundSoundPaletteBlockCollection(this._backgroundSoundPalette);
        this._soundEnvironmentPaletteCollection = new StructureBspSoundEnvironmentPaletteBlockCollection(this._soundEnvironmentPalette);
        this._markersCollection = new StructureBspMarkerBlockCollection(this._markers);
        this._detailObjectsCollection = new StructureBspDetailObjectDataBlockCollection(this._detailObjects);
        this._runtimeDecalsCollection = new StructureBspRuntimeDecalBlockCollection(this._runtimeDecals);
        this._leafMapLeavesCollection = new GlobalMapLeafBlockCollection(this._leafMapLeaves);
        this._leafMapPortalsCollection = new GlobalLeafPortalBlockCollection(this._leafMapPortals);
        this._unnamed = new Pad(20);
        this._unnamed2 = new Pad(4);
        this._unnamed3 = new Pad(12);
        this._unnamed4 = new Pad(4);
        this._unnamed5 = new Pad(12);
        this._unnamed6 = new Pad(12);
        this._unnamed7 = new Pad(24);
        this._unnamed8 = new Pad(24);
        this._unnamed9 = new Pad(24);
        this._unnamed10 = new Pad(8);
        this._unnamed11 = new Pad(4);
      }
      public StructureCollisionMaterialsBlockCollection CollisionMaterials
      {
        get
        {
          return this._collisionMaterialsCollection;
        }
      }
      public BspBlockCollection CollisionBsp
      {
        get
        {
          return this._collisionBspCollection;
        }
      }
      public StructureBspNodeBlockCollection Nodes
      {
        get
        {
          return this._nodesCollection;
        }
      }
      public StructureBspLeafBlockCollection Leaves
      {
        get
        {
          return this._leavesCollection;
        }
      }
      public StructureBspSurfaceReferenceBlockCollection LeafSurfaces
      {
        get
        {
          return this._leafSurfacesCollection;
        }
      }
      public StructureBspSurfaceBlockCollection Surfaces
      {
        get
        {
          return this._surfacesCollection;
        }
      }
      public StructureBspLightmapBlockCollection Lightmaps2
      {
        get
        {
          return this._lightmaps2Collection;
        }
      }
      public StructureBspLensFlareBlockCollection LensFlares
      {
        get
        {
          return this._lensFlaresCollection;
        }
      }
      public StructureBspLensFlareMarkerBlockCollection LensFlareMarkers
      {
        get
        {
          return this._lensFlareMarkersCollection;
        }
      }
      public StructureBspClusterBlockCollection Clusters
      {
        get
        {
          return this._clustersCollection;
        }
      }
      public StructureBspClusterPortalBlockCollection ClusterPortals
      {
        get
        {
          return this._clusterPortalsCollection;
        }
      }
      public StructureBspBreakableSurfaceBlockCollection BreakableSurfaces
      {
        get
        {
          return this._breakableSurfacesCollection;
        }
      }
      public StructureBspFogPlaneBlockCollection FogPlanes
      {
        get
        {
          return this._fogPlanesCollection;
        }
      }
      public StructureBspFogRegionBlockCollection FogRegions
      {
        get
        {
          return this._fogRegionsCollection;
        }
      }
      public StructureBspFogPaletteBlockCollection FogPalette
      {
        get
        {
          return this._fogPaletteCollection;
        }
      }
      public StructureBspWeatherPaletteBlockCollection WeatherPalette
      {
        get
        {
          return this._weatherPaletteCollection;
        }
      }
      public StructureBspWeatherPolyhedronBlockCollection WeatherPolyhedra
      {
        get
        {
          return this._weatherPolyhedraCollection;
        }
      }
      public StructureBspPathfindingSurfacesBlockCollection PathfindingSurfaces
      {
        get
        {
          return this._pathfindingSurfacesCollection;
        }
      }
      public StructureBspPathfindingEdgesBlockCollection PathfindingEdges
      {
        get
        {
          return this._pathfindingEdgesCollection;
        }
      }
      public StructureBspBackgroundSoundPaletteBlockCollection BackgroundSoundPalette
      {
        get
        {
          return this._backgroundSoundPaletteCollection;
        }
      }
      public StructureBspSoundEnvironmentPaletteBlockCollection SoundEnvironmentPalette
      {
        get
        {
          return this._soundEnvironmentPaletteCollection;
        }
      }
      public StructureBspMarkerBlockCollection Markers
      {
        get
        {
          return this._markersCollection;
        }
      }
      public StructureBspDetailObjectDataBlockCollection DetailObjects
      {
        get
        {
          return this._detailObjectsCollection;
        }
      }
      public StructureBspRuntimeDecalBlockCollection RuntimeDecals
      {
        get
        {
          return this._runtimeDecalsCollection;
        }
      }
      public GlobalMapLeafBlockCollection LeafMapLeaves
      {
        get
        {
          return this._leafMapLeavesCollection;
        }
      }
      public GlobalLeafPortalBlockCollection LeafMapPortals
      {
        get
        {
          return this._leafMapPortalsCollection;
        }
      }
      public TagReference Lightmaps
      {
        get
        {
          return this._lightmaps;
        }
        set
        {
          this._lightmaps = value;
        }
      }
      public Real VehicleFloor
      {
        get
        {
          return this._vehicleFloor;
        }
        set
        {
          this._vehicleFloor = value;
        }
      }
      public Real VehicleCeiling
      {
        get
        {
          return this._vehicleCeiling;
        }
        set
        {
          this._vehicleCeiling = value;
        }
      }
      public RealRGBColor DefaultAmbientColor
      {
        get
        {
          return this._defaultAmbientColor;
        }
        set
        {
          this._defaultAmbientColor = value;
        }
      }
      public RealRGBColor DefaultDistantLight0Color
      {
        get
        {
          return this._defaultDistantLight0Color;
        }
        set
        {
          this._defaultDistantLight0Color = value;
        }
      }
      public RealVector3D DefaultDistantLight0Direction
      {
        get
        {
          return this._defaultDistantLight0Direction;
        }
        set
        {
          this._defaultDistantLight0Direction = value;
        }
      }
      public RealRGBColor DefaultDistantLight1Color
      {
        get
        {
          return this._defaultDistantLight1Color;
        }
        set
        {
          this._defaultDistantLight1Color = value;
        }
      }
      public RealVector3D DefaultDistantLight1Direction
      {
        get
        {
          return this._defaultDistantLight1Direction;
        }
        set
        {
          this._defaultDistantLight1Direction = value;
        }
      }
      public RealARGBColor DefaultReflectionTint
      {
        get
        {
          return this._defaultReflectionTint;
        }
        set
        {
          this._defaultReflectionTint = value;
        }
      }
      public RealVector3D DefaultShadowVector
      {
        get
        {
          return this._defaultShadowVector;
        }
        set
        {
          this._defaultShadowVector = value;
        }
      }
      public RealRGBColor DefaultShadowColor
      {
        get
        {
          return this._defaultShadowColor;
        }
        set
        {
          this._defaultShadowColor = value;
        }
      }
      public RealBounds WorldBoundsX
      {
        get
        {
          return this._worldBoundsX;
        }
        set
        {
          this._worldBoundsX = value;
        }
      }
      public RealBounds WorldBoundsY
      {
        get
        {
          return this._worldBoundsY;
        }
        set
        {
          this._worldBoundsY = value;
        }
      }
      public RealBounds WorldBoundsZ
      {
        get
        {
          return this._worldBoundsZ;
        }
        set
        {
          this._worldBoundsZ = value;
        }
      }
      public Data ClusterData
      {
        get
        {
          return this._clusterData;
        }
        set
        {
          this._clusterData = value;
        }
      }
      public Data SoundPASData
      {
        get
        {
          return this._soundPASData;
        }
        set
        {
          this._soundPASData = value;
        }
      }
      public void Read(BinaryReader reader)
      {
        _lightmaps.Read(reader);
        _vehicleFloor.Read(reader);
        _vehicleCeiling.Read(reader);
        _unnamed.Read(reader);
        _defaultAmbientColor.Read(reader);
        _unnamed2.Read(reader);
        _defaultDistantLight0Color.Read(reader);
        _defaultDistantLight0Direction.Read(reader);
        _defaultDistantLight1Color.Read(reader);
        _defaultDistantLight1Direction.Read(reader);
        _unnamed3.Read(reader);
        _defaultReflectionTint.Read(reader);
        _defaultShadowVector.Read(reader);
        _defaultShadowColor.Read(reader);
        _unnamed4.Read(reader);
        _collisionMaterials.Read(reader);
        _collisionBsp.Read(reader);
        _nodes.Read(reader);
        _worldBoundsX.Read(reader);
        _worldBoundsY.Read(reader);
        _worldBoundsZ.Read(reader);
        _leaves.Read(reader);
        _leafSurfaces.Read(reader);
        _surfaces.Read(reader);
        _lightmaps2.Read(reader);
        _unnamed5.Read(reader);
        _lensFlares.Read(reader);
        _lensFlareMarkers.Read(reader);
        _clusters.Read(reader);
        _clusterData.Read(reader);
        _clusterPortals.Read(reader);
        _unnamed6.Read(reader);
        _breakableSurfaces.Read(reader);
        _fogPlanes.Read(reader);
        _fogRegions.Read(reader);
        _fogPalette.Read(reader);
        _unnamed7.Read(reader);
        _weatherPalette.Read(reader);
        _weatherPolyhedra.Read(reader);
        _unnamed8.Read(reader);
        _pathfindingSurfaces.Read(reader);
        _pathfindingEdges.Read(reader);
        _backgroundSoundPalette.Read(reader);
        _soundEnvironmentPalette.Read(reader);
        _soundPASData.Read(reader);
        _unnamed9.Read(reader);
        _markers.Read(reader);
        _detailObjects.Read(reader);
        _runtimeDecals.Read(reader);
        _unnamed10.Read(reader);
        _unnamed11.Read(reader);
        _leafMapLeaves.Read(reader);
        _leafMapPortals.Read(reader);
      }
      public void ReadChildData(BinaryReader reader)
      {
        int x = 0;
        _lightmaps.ReadString(reader);
        for (x = 0; (x < _collisionMaterials.Count); x = (x + 1))
        {
          CollisionMaterials.AddNew();
          CollisionMaterials[x].Read(reader);
        }
        for (x = 0; (x < _collisionMaterials.Count); x = (x + 1))
        {
          CollisionMaterials[x].ReadChildData(reader);
        }
        for (x = 0; (x < _collisionBsp.Count); x = (x + 1))
        {
          CollisionBsp.AddNew();
          CollisionBsp[x].Read(reader);
        }
        for (x = 0; (x < _collisionBsp.Count); x = (x + 1))
        {
          CollisionBsp[x].ReadChildData(reader);
        }
        for (x = 0; (x < _nodes.Count); x = (x + 1))
        {
          Nodes.AddNew();
          Nodes[x].Read(reader);
        }
        for (x = 0; (x < _nodes.Count); x = (x + 1))
        {
          Nodes[x].ReadChildData(reader);
        }
        for (x = 0; (x < _leaves.Count); x = (x + 1))
        {
          Leaves.AddNew();
          Leaves[x].Read(reader);
        }
        for (x = 0; (x < _leaves.Count); x = (x + 1))
        {
          Leaves[x].ReadChildData(reader);
        }
        for (x = 0; (x < _leafSurfaces.Count); x = (x + 1))
        {
          LeafSurfaces.AddNew();
          LeafSurfaces[x].Read(reader);
        }
        for (x = 0; (x < _leafSurfaces.Count); x = (x + 1))
        {
          LeafSurfaces[x].ReadChildData(reader);
        }
        for (x = 0; (x < _surfaces.Count); x = (x + 1))
        {
          Surfaces.AddNew();
          Surfaces[x].Read(reader);
        }
        for (x = 0; (x < _surfaces.Count); x = (x + 1))
        {
          Surfaces[x].ReadChildData(reader);
        }
        for (x = 0; (x < _lightmaps2.Count); x = (x + 1))
        {
          Lightmaps2.AddNew();
          Lightmaps2[x].Read(reader);
        }
        for (x = 0; (x < _lightmaps2.Count); x = (x + 1))
        {
          Lightmaps2[x].ReadChildData(reader);
        }
        for (x = 0; (x < _lensFlares.Count); x = (x + 1))
        {
          LensFlares.AddNew();
          LensFlares[x].Read(reader);
        }
        for (x = 0; (x < _lensFlares.Count); x = (x + 1))
        {
          LensFlares[x].ReadChildData(reader);
        }
        for (x = 0; (x < _lensFlareMarkers.Count); x = (x + 1))
        {
          LensFlareMarkers.AddNew();
          LensFlareMarkers[x].Read(reader);
        }
        for (x = 0; (x < _lensFlareMarkers.Count); x = (x + 1))
        {
          LensFlareMarkers[x].ReadChildData(reader);
        }
        for (x = 0; (x < _clusters.Count); x = (x + 1))
        {
          Clusters.AddNew();
          Clusters[x].Read(reader);
        }
        for (x = 0; (x < _clusters.Count); x = (x + 1))
        {
          Clusters[x].ReadChildData(reader);
        }
        _clusterData.ReadBinary(reader);
        for (x = 0; (x < _clusterPortals.Count); x = (x + 1))
        {
          ClusterPortals.AddNew();
          ClusterPortals[x].Read(reader);
        }
        for (x = 0; (x < _clusterPortals.Count); x = (x + 1))
        {
          ClusterPortals[x].ReadChildData(reader);
        }
        for (x = 0; (x < _breakableSurfaces.Count); x = (x + 1))
        {
          BreakableSurfaces.AddNew();
          BreakableSurfaces[x].Read(reader);
        }
        for (x = 0; (x < _breakableSurfaces.Count); x = (x + 1))
        {
          BreakableSurfaces[x].ReadChildData(reader);
        }
        for (x = 0; (x < _fogPlanes.Count); x = (x + 1))
        {
          FogPlanes.AddNew();
          FogPlanes[x].Read(reader);
        }
        for (x = 0; (x < _fogPlanes.Count); x = (x + 1))
        {
          FogPlanes[x].ReadChildData(reader);
        }
        for (x = 0; (x < _fogRegions.Count); x = (x + 1))
        {
          FogRegions.AddNew();
          FogRegions[x].Read(reader);
        }
        for (x = 0; (x < _fogRegions.Count); x = (x + 1))
        {
          FogRegions[x].ReadChildData(reader);
        }
        for (x = 0; (x < _fogPalette.Count); x = (x + 1))
        {
          FogPalette.AddNew();
          FogPalette[x].Read(reader);
        }
        for (x = 0; (x < _fogPalette.Count); x = (x + 1))
        {
          FogPalette[x].ReadChildData(reader);
        }
        for (x = 0; (x < _weatherPalette.Count); x = (x + 1))
        {
          WeatherPalette.AddNew();
          WeatherPalette[x].Read(reader);
        }
        for (x = 0; (x < _weatherPalette.Count); x = (x + 1))
        {
          WeatherPalette[x].ReadChildData(reader);
        }
        for (x = 0; (x < _weatherPolyhedra.Count); x = (x + 1))
        {
          WeatherPolyhedra.AddNew();
          WeatherPolyhedra[x].Read(reader);
        }
        for (x = 0; (x < _weatherPolyhedra.Count); x = (x + 1))
        {
          WeatherPolyhedra[x].ReadChildData(reader);
        }
        for (x = 0; (x < _pathfindingSurfaces.Count); x = (x + 1))
        {
          PathfindingSurfaces.AddNew();
          PathfindingSurfaces[x].Read(reader);
        }
        for (x = 0; (x < _pathfindingSurfaces.Count); x = (x + 1))
        {
          PathfindingSurfaces[x].ReadChildData(reader);
        }
        for (x = 0; (x < _pathfindingEdges.Count); x = (x + 1))
        {
          PathfindingEdges.AddNew();
          PathfindingEdges[x].Read(reader);
        }
        for (x = 0; (x < _pathfindingEdges.Count); x = (x + 1))
        {
          PathfindingEdges[x].ReadChildData(reader);
        }
        for (x = 0; (x < _backgroundSoundPalette.Count); x = (x + 1))
        {
          BackgroundSoundPalette.AddNew();
          BackgroundSoundPalette[x].Read(reader);
        }
        for (x = 0; (x < _backgroundSoundPalette.Count); x = (x + 1))
        {
          BackgroundSoundPalette[x].ReadChildData(reader);
        }
        for (x = 0; (x < _soundEnvironmentPalette.Count); x = (x + 1))
        {
          SoundEnvironmentPalette.AddNew();
          SoundEnvironmentPalette[x].Read(reader);
        }
        for (x = 0; (x < _soundEnvironmentPalette.Count); x = (x + 1))
        {
          SoundEnvironmentPalette[x].ReadChildData(reader);
        }
        _soundPASData.ReadBinary(reader);
        for (x = 0; (x < _markers.Count); x = (x + 1))
        {
          Markers.AddNew();
          Markers[x].Read(reader);
        }
        for (x = 0; (x < _markers.Count); x = (x + 1))
        {
          Markers[x].ReadChildData(reader);
        }
        for (x = 0; (x < _detailObjects.Count); x = (x + 1))
        {
          DetailObjects.AddNew();
          DetailObjects[x].Read(reader);
        }
        for (x = 0; (x < _detailObjects.Count); x = (x + 1))
        {
          DetailObjects[x].ReadChildData(reader);
        }
        for (x = 0; (x < _runtimeDecals.Count); x = (x + 1))
        {
          RuntimeDecals.AddNew();
          RuntimeDecals[x].Read(reader);
        }
        for (x = 0; (x < _runtimeDecals.Count); x = (x + 1))
        {
          RuntimeDecals[x].ReadChildData(reader);
        }
        for (x = 0; (x < _leafMapLeaves.Count); x = (x + 1))
        {
          LeafMapLeaves.AddNew();
          LeafMapLeaves[x].Read(reader);
        }
        for (x = 0; (x < _leafMapLeaves.Count); x = (x + 1))
        {
          LeafMapLeaves[x].ReadChildData(reader);
        }
        for (x = 0; (x < _leafMapPortals.Count); x = (x + 1))
        {
          LeafMapPortals.AddNew();
          LeafMapPortals[x].Read(reader);
        }
        for (x = 0; (x < _leafMapPortals.Count); x = (x + 1))
        {
          LeafMapPortals[x].ReadChildData(reader);
        }
      }
      public class StructureCollisionMaterialsBlockCollection : System.Collections.CollectionBase
      {
        private Block linkedBlock;
        public StructureCollisionMaterialsBlockCollection(Block linkedBlock)
        {
          this.linkedBlock = linkedBlock;
        }
        public StructureCollisionMaterialsBlock this[int index]
        {
          get
          {
            return ((StructureCollisionMaterialsBlock)(this.InnerList[index]));
          }
        }
        public void Add(StructureCollisionMaterialsBlock block)
        {
          InnerList.Add(block);
          if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
        }
        public void AddNew()
        {
          this.Add(new StructureCollisionMaterialsBlock());
        }
        public void Remove(int index)
        {
          InnerList.RemoveAt(index);
          if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
        }
      }
      public class BspBlockCollection : System.Collections.CollectionBase
      {
        private Block linkedBlock;
        public BspBlockCollection(Block linkedBlock)
        {
          this.linkedBlock = linkedBlock;
        }
        public BspBlock this[int index]
        {
          get
          {
            return ((BspBlock)(this.InnerList[index]));
          }
        }
        public void Add(BspBlock block)
        {
          InnerList.Add(block);
          if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
        }
        public void AddNew()
        {
          this.Add(new BspBlock());
        }
        public void Remove(int index)
        {
          InnerList.RemoveAt(index);
          if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
        }
      }
      public class StructureBspNodeBlockCollection : System.Collections.CollectionBase
      {
        private Block linkedBlock;
        public StructureBspNodeBlockCollection(Block linkedBlock)
        {
          this.linkedBlock = linkedBlock;
        }
        public StructureBspNodeBlock this[int index]
        {
          get
          {
            return ((StructureBspNodeBlock)(this.InnerList[index]));
          }
        }
        public void Add(StructureBspNodeBlock block)
        {
          InnerList.Add(block);
          if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
        }
        public void AddNew()
        {
          this.Add(new StructureBspNodeBlock());
        }
        public void Remove(int index)
        {
          InnerList.RemoveAt(index);
          if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
        }
      }
      public class StructureBspLeafBlockCollection : System.Collections.CollectionBase
      {
        private Block linkedBlock;
        public StructureBspLeafBlockCollection(Block linkedBlock)
        {
          this.linkedBlock = linkedBlock;
        }
        public StructureBspLeafBlock this[int index]
        {
          get
          {
            return ((StructureBspLeafBlock)(this.InnerList[index]));
          }
        }
        public void Add(StructureBspLeafBlock block)
        {
          InnerList.Add(block);
          if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
        }
        public void AddNew()
        {
          this.Add(new StructureBspLeafBlock());
        }
        public void Remove(int index)
        {
          InnerList.RemoveAt(index);
          if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
        }
      }
      public class StructureBspSurfaceReferenceBlockCollection : System.Collections.CollectionBase
      {
        private Block linkedBlock;
        public StructureBspSurfaceReferenceBlockCollection(Block linkedBlock)
        {
          this.linkedBlock = linkedBlock;
        }
        public StructureBspSurfaceReferenceBlock this[int index]
        {
          get
          {
            return ((StructureBspSurfaceReferenceBlock)(this.InnerList[index]));
          }
        }
        public void Add(StructureBspSurfaceReferenceBlock block)
        {
          InnerList.Add(block);
          if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
        }
        public void AddNew()
        {
          this.Add(new StructureBspSurfaceReferenceBlock());
        }
        public void Remove(int index)
        {
          InnerList.RemoveAt(index);
          if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
        }
      }
      public class StructureBspSurfaceBlockCollection : System.Collections.CollectionBase
      {
        private Block linkedBlock;
        public StructureBspSurfaceBlockCollection(Block linkedBlock)
        {
          this.linkedBlock = linkedBlock;
        }
        public StructureBspSurfaceBlock this[int index]
        {
          get
          {
            return ((StructureBspSurfaceBlock)(this.InnerList[index]));
          }
        }
        public void Add(StructureBspSurfaceBlock block)
        {
          InnerList.Add(block);
          if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
        }
        public void AddNew()
        {
          this.Add(new StructureBspSurfaceBlock());
        }
        public void Remove(int index)
        {
          InnerList.RemoveAt(index);
          if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
        }
      }
      public class StructureBspLightmapBlockCollection : System.Collections.CollectionBase
      {
        private Block linkedBlock;
        public StructureBspLightmapBlockCollection(Block linkedBlock)
        {
          this.linkedBlock = linkedBlock;
        }
        public StructureBspLightmapBlock this[int index]
        {
          get
          {
            return ((StructureBspLightmapBlock)(this.InnerList[index]));
          }
        }
        public void Add(StructureBspLightmapBlock block)
        {
          InnerList.Add(block);
          if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
        }
        public void AddNew()
        {
          this.Add(new StructureBspLightmapBlock());
        }
        public void Remove(int index)
        {
          InnerList.RemoveAt(index);
          if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
        }
      }
      public class StructureBspLensFlareBlockCollection : System.Collections.CollectionBase
      {
        private Block linkedBlock;
        public StructureBspLensFlareBlockCollection(Block linkedBlock)
        {
          this.linkedBlock = linkedBlock;
        }
        public StructureBspLensFlareBlock this[int index]
        {
          get
          {
            return ((StructureBspLensFlareBlock)(this.InnerList[index]));
          }
        }
        public void Add(StructureBspLensFlareBlock block)
        {
          InnerList.Add(block);
          if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
        }
        public void AddNew()
        {
          this.Add(new StructureBspLensFlareBlock());
        }
        public void Remove(int index)
        {
          InnerList.RemoveAt(index);
          if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
        }
      }
      public class StructureBspLensFlareMarkerBlockCollection : System.Collections.CollectionBase
      {
        private Block linkedBlock;
        public StructureBspLensFlareMarkerBlockCollection(Block linkedBlock)
        {
          this.linkedBlock = linkedBlock;
        }
        public StructureBspLensFlareMarkerBlock this[int index]
        {
          get
          {
            return ((StructureBspLensFlareMarkerBlock)(this.InnerList[index]));
          }
        }
        public void Add(StructureBspLensFlareMarkerBlock block)
        {
          InnerList.Add(block);
          if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
        }
        public void AddNew()
        {
          this.Add(new StructureBspLensFlareMarkerBlock());
        }
        public void Remove(int index)
        {
          InnerList.RemoveAt(index);
          if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
        }
      }
      public class StructureBspClusterBlockCollection : System.Collections.CollectionBase
      {
        private Block linkedBlock;
        public StructureBspClusterBlockCollection(Block linkedBlock)
        {
          this.linkedBlock = linkedBlock;
        }
        public StructureBspClusterBlock this[int index]
        {
          get
          {
            return ((StructureBspClusterBlock)(this.InnerList[index]));
          }
        }
        public void Add(StructureBspClusterBlock block)
        {
          InnerList.Add(block);
          if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
        }
        public void AddNew()
        {
          this.Add(new StructureBspClusterBlock());
        }
        public void Remove(int index)
        {
          InnerList.RemoveAt(index);
          if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
        }
      }
      public class StructureBspClusterPortalBlockCollection : System.Collections.CollectionBase
      {
        private Block linkedBlock;
        public StructureBspClusterPortalBlockCollection(Block linkedBlock)
        {
          this.linkedBlock = linkedBlock;
        }
        public StructureBspClusterPortalBlock this[int index]
        {
          get
          {
            return ((StructureBspClusterPortalBlock)(this.InnerList[index]));
          }
        }
        public void Add(StructureBspClusterPortalBlock block)
        {
          InnerList.Add(block);
          if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
        }
        public void AddNew()
        {
          this.Add(new StructureBspClusterPortalBlock());
        }
        public void Remove(int index)
        {
          InnerList.RemoveAt(index);
          if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
        }
      }
      public class StructureBspBreakableSurfaceBlockCollection : System.Collections.CollectionBase
      {
        private Block linkedBlock;
        public StructureBspBreakableSurfaceBlockCollection(Block linkedBlock)
        {
          this.linkedBlock = linkedBlock;
        }
        public StructureBspBreakableSurfaceBlock this[int index]
        {
          get
          {
            return ((StructureBspBreakableSurfaceBlock)(this.InnerList[index]));
          }
        }
        public void Add(StructureBspBreakableSurfaceBlock block)
        {
          InnerList.Add(block);
          if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
        }
        public void AddNew()
        {
          this.Add(new StructureBspBreakableSurfaceBlock());
        }
        public void Remove(int index)
        {
          InnerList.RemoveAt(index);
          if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
        }
      }
      public class StructureBspFogPlaneBlockCollection : System.Collections.CollectionBase
      {
        private Block linkedBlock;
        public StructureBspFogPlaneBlockCollection(Block linkedBlock)
        {
          this.linkedBlock = linkedBlock;
        }
        public StructureBspFogPlaneBlock this[int index]
        {
          get
          {
            return ((StructureBspFogPlaneBlock)(this.InnerList[index]));
          }
        }
        public void Add(StructureBspFogPlaneBlock block)
        {
          InnerList.Add(block);
          if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
        }
        public void AddNew()
        {
          this.Add(new StructureBspFogPlaneBlock());
        }
        public void Remove(int index)
        {
          InnerList.RemoveAt(index);
          if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
        }
      }
      public class StructureBspFogRegionBlockCollection : System.Collections.CollectionBase
      {
        private Block linkedBlock;
        public StructureBspFogRegionBlockCollection(Block linkedBlock)
        {
          this.linkedBlock = linkedBlock;
        }
        public StructureBspFogRegionBlock this[int index]
        {
          get
          {
            return ((StructureBspFogRegionBlock)(this.InnerList[index]));
          }
        }
        public void Add(StructureBspFogRegionBlock block)
        {
          InnerList.Add(block);
          if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
        }
        public void AddNew()
        {
          this.Add(new StructureBspFogRegionBlock());
        }
        public void Remove(int index)
        {
          InnerList.RemoveAt(index);
          if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
        }
      }
      public class StructureBspFogPaletteBlockCollection : System.Collections.CollectionBase
      {
        private Block linkedBlock;
        public StructureBspFogPaletteBlockCollection(Block linkedBlock)
        {
          this.linkedBlock = linkedBlock;
        }
        public StructureBspFogPaletteBlock this[int index]
        {
          get
          {
            return ((StructureBspFogPaletteBlock)(this.InnerList[index]));
          }
        }
        public void Add(StructureBspFogPaletteBlock block)
        {
          InnerList.Add(block);
          if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
        }
        public void AddNew()
        {
          this.Add(new StructureBspFogPaletteBlock());
        }
        public void Remove(int index)
        {
          InnerList.RemoveAt(index);
          if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
        }
      }
      public class StructureBspWeatherPaletteBlockCollection : System.Collections.CollectionBase
      {
        private Block linkedBlock;
        public StructureBspWeatherPaletteBlockCollection(Block linkedBlock)
        {
          this.linkedBlock = linkedBlock;
        }
        public StructureBspWeatherPaletteBlock this[int index]
        {
          get
          {
            return ((StructureBspWeatherPaletteBlock)(this.InnerList[index]));
          }
        }
        public void Add(StructureBspWeatherPaletteBlock block)
        {
          InnerList.Add(block);
          if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
        }
        public void AddNew()
        {
          this.Add(new StructureBspWeatherPaletteBlock());
        }
        public void Remove(int index)
        {
          InnerList.RemoveAt(index);
          if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
        }
      }
      public class StructureBspWeatherPolyhedronBlockCollection : System.Collections.CollectionBase
      {
        private Block linkedBlock;
        public StructureBspWeatherPolyhedronBlockCollection(Block linkedBlock)
        {
          this.linkedBlock = linkedBlock;
        }
        public StructureBspWeatherPolyhedronBlock this[int index]
        {
          get
          {
            return ((StructureBspWeatherPolyhedronBlock)(this.InnerList[index]));
          }
        }
        public void Add(StructureBspWeatherPolyhedronBlock block)
        {
          InnerList.Add(block);
          if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
        }
        public void AddNew()
        {
          this.Add(new StructureBspWeatherPolyhedronBlock());
        }
        public void Remove(int index)
        {
          InnerList.RemoveAt(index);
          if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
        }
      }
      public class StructureBspPathfindingSurfacesBlockCollection : System.Collections.CollectionBase
      {
        private Block linkedBlock;
        public StructureBspPathfindingSurfacesBlockCollection(Block linkedBlock)
        {
          this.linkedBlock = linkedBlock;
        }
        public StructureBspPathfindingSurfacesBlock this[int index]
        {
          get
          {
            return ((StructureBspPathfindingSurfacesBlock)(this.InnerList[index]));
          }
        }
        public void Add(StructureBspPathfindingSurfacesBlock block)
        {
          InnerList.Add(block);
          if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
        }
        public void AddNew()
        {
          this.Add(new StructureBspPathfindingSurfacesBlock());
        }
        public void Remove(int index)
        {
          InnerList.RemoveAt(index);
          if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
        }
      }
      public class StructureBspPathfindingEdgesBlockCollection : System.Collections.CollectionBase
      {
        private Block linkedBlock;
        public StructureBspPathfindingEdgesBlockCollection(Block linkedBlock)
        {
          this.linkedBlock = linkedBlock;
        }
        public StructureBspPathfindingEdgesBlock this[int index]
        {
          get
          {
            return ((StructureBspPathfindingEdgesBlock)(this.InnerList[index]));
          }
        }
        public void Add(StructureBspPathfindingEdgesBlock block)
        {
          InnerList.Add(block);
          if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
        }
        public void AddNew()
        {
          this.Add(new StructureBspPathfindingEdgesBlock());
        }
        public void Remove(int index)
        {
          InnerList.RemoveAt(index);
          if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
        }
      }
      public class StructureBspBackgroundSoundPaletteBlockCollection : System.Collections.CollectionBase
      {
        private Block linkedBlock;
        public StructureBspBackgroundSoundPaletteBlockCollection(Block linkedBlock)
        {
          this.linkedBlock = linkedBlock;
        }
        public StructureBspBackgroundSoundPaletteBlock this[int index]
        {
          get
          {
            return ((StructureBspBackgroundSoundPaletteBlock)(this.InnerList[index]));
          }
        }
        public void Add(StructureBspBackgroundSoundPaletteBlock block)
        {
          InnerList.Add(block);
          if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
        }
        public void AddNew()
        {
          this.Add(new StructureBspBackgroundSoundPaletteBlock());
        }
        public void Remove(int index)
        {
          InnerList.RemoveAt(index);
          if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
        }
      }
      public class StructureBspSoundEnvironmentPaletteBlockCollection : System.Collections.CollectionBase
      {
        private Block linkedBlock;
        public StructureBspSoundEnvironmentPaletteBlockCollection(Block linkedBlock)
        {
          this.linkedBlock = linkedBlock;
        }
        public StructureBspSoundEnvironmentPaletteBlock this[int index]
        {
          get
          {
            return ((StructureBspSoundEnvironmentPaletteBlock)(this.InnerList[index]));
          }
        }
        public void Add(StructureBspSoundEnvironmentPaletteBlock block)
        {
          InnerList.Add(block);
          if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
        }
        public void AddNew()
        {
          this.Add(new StructureBspSoundEnvironmentPaletteBlock());
        }
        public void Remove(int index)
        {
          InnerList.RemoveAt(index);
          if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
        }
      }
      public class StructureBspMarkerBlockCollection : System.Collections.CollectionBase
      {
        private Block linkedBlock;
        public StructureBspMarkerBlockCollection(Block linkedBlock)
        {
          this.linkedBlock = linkedBlock;
        }
        public StructureBspMarkerBlock this[int index]
        {
          get
          {
            return ((StructureBspMarkerBlock)(this.InnerList[index]));
          }
        }
        public void Add(StructureBspMarkerBlock block)
        {
          InnerList.Add(block);
          if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
        }
        public void AddNew()
        {
          this.Add(new StructureBspMarkerBlock());
        }
        public void Remove(int index)
        {
          InnerList.RemoveAt(index);
          if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
        }
      }
      public class StructureBspDetailObjectDataBlockCollection : System.Collections.CollectionBase
      {
        private Block linkedBlock;
        public StructureBspDetailObjectDataBlockCollection(Block linkedBlock)
        {
          this.linkedBlock = linkedBlock;
        }
        public StructureBspDetailObjectDataBlock this[int index]
        {
          get
          {
            return ((StructureBspDetailObjectDataBlock)(this.InnerList[index]));
          }
        }
        public void Add(StructureBspDetailObjectDataBlock block)
        {
          InnerList.Add(block);
          if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
        }
        public void AddNew()
        {
          this.Add(new StructureBspDetailObjectDataBlock());
        }
        public void Remove(int index)
        {
          InnerList.RemoveAt(index);
          if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
        }
      }
      public class StructureBspRuntimeDecalBlockCollection : System.Collections.CollectionBase
      {
        private Block linkedBlock;
        public StructureBspRuntimeDecalBlockCollection(Block linkedBlock)
        {
          this.linkedBlock = linkedBlock;
        }
        public StructureBspRuntimeDecalBlock this[int index]
        {
          get
          {
            return ((StructureBspRuntimeDecalBlock)(this.InnerList[index]));
          }
        }
        public void Add(StructureBspRuntimeDecalBlock block)
        {
          InnerList.Add(block);
          if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
        }
        public void AddNew()
        {
          this.Add(new StructureBspRuntimeDecalBlock());
        }
        public void Remove(int index)
        {
          InnerList.RemoveAt(index);
          if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
        }
      }
      public class GlobalMapLeafBlockCollection : System.Collections.CollectionBase
      {
        private Block linkedBlock;
        public GlobalMapLeafBlockCollection(Block linkedBlock)
        {
          this.linkedBlock = linkedBlock;
        }
        public GlobalMapLeafBlock this[int index]
        {
          get
          {
            return ((GlobalMapLeafBlock)(this.InnerList[index]));
          }
        }
        public void Add(GlobalMapLeafBlock block)
        {
          InnerList.Add(block);
          if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
        }
        public void AddNew()
        {
          this.Add(new GlobalMapLeafBlock());
        }
        public void Remove(int index)
        {
          InnerList.RemoveAt(index);
          if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
        }
      }
      public class GlobalLeafPortalBlockCollection : System.Collections.CollectionBase
      {
        private Block linkedBlock;
        public GlobalLeafPortalBlockCollection(Block linkedBlock)
        {
          this.linkedBlock = linkedBlock;
        }
        public GlobalLeafPortalBlock this[int index]
        {
          get
          {
            return ((GlobalLeafPortalBlock)(this.InnerList[index]));
          }
        }
        public void Add(GlobalLeafPortalBlock block)
        {
          InnerList.Add(block);
          if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
        }
        public void AddNew()
        {
          this.Add(new GlobalLeafPortalBlock());
        }
        public void Remove(int index)
        {
          InnerList.RemoveAt(index);
          if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
        }
      }
    }
    public class StructureCollisionMaterialsBlock : IBlock
    {
      private TagReference _shader = new TagReference();
      private Pad _unnamed;
      public StructureCollisionMaterialsBlock()
      {
        this._unnamed = new Pad(4);
      }
      public TagReference Shader
      {
        get
        {
          return this._shader;
        }
        set
        {
          this._shader = value;
        }
      }
      public void Read(BinaryReader reader)
      {
        _shader.Read(reader);
        _unnamed.Read(reader);
      }
      public void ReadChildData(BinaryReader reader)
      {
        _shader.ReadString(reader);
      }
    }
    public class BspBlock : IBlock
    {
      private Block _bsp3dNodes = new Block();
      private Block _planes = new Block();
      private Block _leaves = new Block();
      private Block _bsp2dReferences = new Block();
      private Block _bsp2dNodes = new Block();
      private Block _surfaces = new Block();
      private Block _edges = new Block();
      private Block _vertices = new Block();
      private Bsp3dnodeBlockCollection _bsp3dNodesCollection;
      private PlaneBlockCollection _planesCollection;
      private LeafBlockCollection _leavesCollection;
      private Bsp2dreferenceBlockCollection _bsp2dReferencesCollection;
      private Bsp2dnodeBlockCollection _bsp2dNodesCollection;
      private SurfaceBlockCollection _surfacesCollection;
      private EdgeBlockCollection _edgesCollection;
      private VertexBlockCollection _verticesCollection;
      public BspBlock()
      {
        this._bsp3dNodesCollection = new Bsp3dnodeBlockCollection(this._bsp3dNodes);
        this._planesCollection = new PlaneBlockCollection(this._planes);
        this._leavesCollection = new LeafBlockCollection(this._leaves);
        this._bsp2dReferencesCollection = new Bsp2dreferenceBlockCollection(this._bsp2dReferences);
        this._bsp2dNodesCollection = new Bsp2dnodeBlockCollection(this._bsp2dNodes);
        this._surfacesCollection = new SurfaceBlockCollection(this._surfaces);
        this._edgesCollection = new EdgeBlockCollection(this._edges);
        this._verticesCollection = new VertexBlockCollection(this._vertices);
      }
      public Bsp3dnodeBlockCollection Bsp3dNodes
      {
        get
        {
          return this._bsp3dNodesCollection;
        }
      }
      public PlaneBlockCollection Planes
      {
        get
        {
          return this._planesCollection;
        }
      }
      public LeafBlockCollection Leaves
      {
        get
        {
          return this._leavesCollection;
        }
      }
      public Bsp2dreferenceBlockCollection Bsp2dReferences
      {
        get
        {
          return this._bsp2dReferencesCollection;
        }
      }
      public Bsp2dnodeBlockCollection Bsp2dNodes
      {
        get
        {
          return this._bsp2dNodesCollection;
        }
      }
      public SurfaceBlockCollection Surfaces
      {
        get
        {
          return this._surfacesCollection;
        }
      }
      public EdgeBlockCollection Edges
      {
        get
        {
          return this._edgesCollection;
        }
      }
      public VertexBlockCollection Vertices
      {
        get
        {
          return this._verticesCollection;
        }
      }
      public void Read(BinaryReader reader)
      {
        _bsp3dNodes.Read(reader);
        _planes.Read(reader);
        _leaves.Read(reader);
        _bsp2dReferences.Read(reader);
        _bsp2dNodes.Read(reader);
        _surfaces.Read(reader);
        _edges.Read(reader);
        _vertices.Read(reader);
      }
      public void ReadChildData(BinaryReader reader)
      {
        int x = 0;
        for (x = 0; (x < _bsp3dNodes.Count); x = (x + 1))
        {
          Bsp3dNodes.AddNew();
          Bsp3dNodes[x].Read(reader);
        }
        for (x = 0; (x < _bsp3dNodes.Count); x = (x + 1))
        {
          Bsp3dNodes[x].ReadChildData(reader);
        }
        for (x = 0; (x < _planes.Count); x = (x + 1))
        {
          Planes.AddNew();
          Planes[x].Read(reader);
        }
        for (x = 0; (x < _planes.Count); x = (x + 1))
        {
          Planes[x].ReadChildData(reader);
        }
        for (x = 0; (x < _leaves.Count); x = (x + 1))
        {
          Leaves.AddNew();
          Leaves[x].Read(reader);
        }
        for (x = 0; (x < _leaves.Count); x = (x + 1))
        {
          Leaves[x].ReadChildData(reader);
        }
        for (x = 0; (x < _bsp2dReferences.Count); x = (x + 1))
        {
          Bsp2dReferences.AddNew();
          Bsp2dReferences[x].Read(reader);
        }
        for (x = 0; (x < _bsp2dReferences.Count); x = (x + 1))
        {
          Bsp2dReferences[x].ReadChildData(reader);
        }
        for (x = 0; (x < _bsp2dNodes.Count); x = (x + 1))
        {
          Bsp2dNodes.AddNew();
          Bsp2dNodes[x].Read(reader);
        }
        for (x = 0; (x < _bsp2dNodes.Count); x = (x + 1))
        {
          Bsp2dNodes[x].ReadChildData(reader);
        }
        for (x = 0; (x < _surfaces.Count); x = (x + 1))
        {
          Surfaces.AddNew();
          Surfaces[x].Read(reader);
        }
        for (x = 0; (x < _surfaces.Count); x = (x + 1))
        {
          Surfaces[x].ReadChildData(reader);
        }
        for (x = 0; (x < _edges.Count); x = (x + 1))
        {
          Edges.AddNew();
          Edges[x].Read(reader);
        }
        for (x = 0; (x < _edges.Count); x = (x + 1))
        {
          Edges[x].ReadChildData(reader);
        }
        for (x = 0; (x < _vertices.Count); x = (x + 1))
        {
          Vertices.AddNew();
          Vertices[x].Read(reader);
        }
        for (x = 0; (x < _vertices.Count); x = (x + 1))
        {
          Vertices[x].ReadChildData(reader);
        }
      }
      public class Bsp3dnodeBlockCollection : System.Collections.CollectionBase
      {
        private Block linkedBlock;
        public Bsp3dnodeBlockCollection(Block linkedBlock)
        {
          this.linkedBlock = linkedBlock;
        }
        public Bsp3dnodeBlock this[int index]
        {
          get
          {
            return ((Bsp3dnodeBlock)(this.InnerList[index]));
          }
        }
        public void Add(Bsp3dnodeBlock block)
        {
          InnerList.Add(block);
          if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
        }
        public void AddNew()
        {
          this.Add(new Bsp3dnodeBlock());
        }
        public void Remove(int index)
        {
          InnerList.RemoveAt(index);
          if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
        }
      }
      public class PlaneBlockCollection : System.Collections.CollectionBase
      {
        private Block linkedBlock;
        public PlaneBlockCollection(Block linkedBlock)
        {
          this.linkedBlock = linkedBlock;
        }
        public PlaneBlock this[int index]
        {
          get
          {
            return ((PlaneBlock)(this.InnerList[index]));
          }
        }
        public void Add(PlaneBlock block)
        {
          InnerList.Add(block);
          if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
        }
        public void AddNew()
        {
          this.Add(new PlaneBlock());
        }
        public void Remove(int index)
        {
          InnerList.RemoveAt(index);
          if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
        }
      }
      public class LeafBlockCollection : System.Collections.CollectionBase
      {
        private Block linkedBlock;
        public LeafBlockCollection(Block linkedBlock)
        {
          this.linkedBlock = linkedBlock;
        }
        public LeafBlock this[int index]
        {
          get
          {
            return ((LeafBlock)(this.InnerList[index]));
          }
        }
        public void Add(LeafBlock block)
        {
          InnerList.Add(block);
          if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
        }
        public void AddNew()
        {
          this.Add(new LeafBlock());
        }
        public void Remove(int index)
        {
          InnerList.RemoveAt(index);
          if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
        }
      }
      public class Bsp2dreferenceBlockCollection : System.Collections.CollectionBase
      {
        private Block linkedBlock;
        public Bsp2dreferenceBlockCollection(Block linkedBlock)
        {
          this.linkedBlock = linkedBlock;
        }
        public Bsp2dreferenceBlock this[int index]
        {
          get
          {
            return ((Bsp2dreferenceBlock)(this.InnerList[index]));
          }
        }
        public void Add(Bsp2dreferenceBlock block)
        {
          InnerList.Add(block);
          if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
        }
        public void AddNew()
        {
          this.Add(new Bsp2dreferenceBlock());
        }
        public void Remove(int index)
        {
          InnerList.RemoveAt(index);
          if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
        }
      }
      public class Bsp2dnodeBlockCollection : System.Collections.CollectionBase
      {
        private Block linkedBlock;
        public Bsp2dnodeBlockCollection(Block linkedBlock)
        {
          this.linkedBlock = linkedBlock;
        }
        public Bsp2dnodeBlock this[int index]
        {
          get
          {
            return ((Bsp2dnodeBlock)(this.InnerList[index]));
          }
        }
        public void Add(Bsp2dnodeBlock block)
        {
          InnerList.Add(block);
          if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
        }
        public void AddNew()
        {
          this.Add(new Bsp2dnodeBlock());
        }
        public void Remove(int index)
        {
          InnerList.RemoveAt(index);
          if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
        }
      }
      public class SurfaceBlockCollection : System.Collections.CollectionBase
      {
        private Block linkedBlock;
        public SurfaceBlockCollection(Block linkedBlock)
        {
          this.linkedBlock = linkedBlock;
        }
        public SurfaceBlock this[int index]
        {
          get
          {
            return ((SurfaceBlock)(this.InnerList[index]));
          }
        }
        public void Add(SurfaceBlock block)
        {
          InnerList.Add(block);
          if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
        }
        public void AddNew()
        {
          this.Add(new SurfaceBlock());
        }
        public void Remove(int index)
        {
          InnerList.RemoveAt(index);
          if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
        }
      }
      public class EdgeBlockCollection : System.Collections.CollectionBase
      {
        private Block linkedBlock;
        public EdgeBlockCollection(Block linkedBlock)
        {
          this.linkedBlock = linkedBlock;
        }
        public EdgeBlock this[int index]
        {
          get
          {
            return ((EdgeBlock)(this.InnerList[index]));
          }
        }
        public void Add(EdgeBlock block)
        {
          InnerList.Add(block);
          if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
        }
        public void AddNew()
        {
          this.Add(new EdgeBlock());
        }
        public void Remove(int index)
        {
          InnerList.RemoveAt(index);
          if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
        }
      }
      public class VertexBlockCollection : System.Collections.CollectionBase
      {
        private Block linkedBlock;
        public VertexBlockCollection(Block linkedBlock)
        {
          this.linkedBlock = linkedBlock;
        }
        public VertexBlock this[int index]
        {
          get
          {
            return ((VertexBlock)(this.InnerList[index]));
          }
        }
        public void Add(VertexBlock block)
        {
          InnerList.Add(block);
          if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
        }
        public void AddNew()
        {
          this.Add(new VertexBlock());
        }
        public void Remove(int index)
        {
          InnerList.RemoveAt(index);
          if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
        }
      }
    }
    public class Bsp3dnodeBlock : IBlock
    {
      private LongInteger _plane = new LongInteger();
      private LongInteger _backChild = new LongInteger();
      private LongInteger _frontChild = new LongInteger();
      public Bsp3dnodeBlock()
      {
      }
      public LongInteger Plane
      {
        get
        {
          return this._plane;
        }
        set
        {
          this._plane = value;
        }
      }
      public LongInteger BackChild
      {
        get
        {
          return this._backChild;
        }
        set
        {
          this._backChild = value;
        }
      }
      public LongInteger FrontChild
      {
        get
        {
          return this._frontChild;
        }
        set
        {
          this._frontChild = value;
        }
      }
      public void Read(BinaryReader reader)
      {
        _plane.Read(reader);
        _backChild.Read(reader);
        _frontChild.Read(reader);
      }
      public void ReadChildData(BinaryReader reader)
      {
      }
    }
    public class PlaneBlock : IBlock
    {
      private RealPlane3D _plane = new RealPlane3D();
      public PlaneBlock()
      {
      }
      public RealPlane3D Plane
      {
        get
        {
          return this._plane;
        }
        set
        {
          this._plane = value;
        }
      }
      public void Read(BinaryReader reader)
      {
        _plane.Read(reader);
      }
      public void ReadChildData(BinaryReader reader)
      {
      }
    }
    public class LeafBlock : IBlock
    {
      private Flags _flags;
      private ShortInteger _bsp2dReferenceCount = new ShortInteger();
      private LongInteger _firstBsp2dReference = new LongInteger();
      public LeafBlock()
      {
        this._flags = new Flags(2);
      }
      public Flags Flags
      {
        get
        {
          return this._flags;
        }
        set
        {
          this._flags = value;
        }
      }
      public ShortInteger Bsp2dReferenceCount
      {
        get
        {
          return this._bsp2dReferenceCount;
        }
        set
        {
          this._bsp2dReferenceCount = value;
        }
      }
      public LongInteger FirstBsp2dReference
      {
        get
        {
          return this._firstBsp2dReference;
        }
        set
        {
          this._firstBsp2dReference = value;
        }
      }
      public void Read(BinaryReader reader)
      {
        _flags.Read(reader);
        _bsp2dReferenceCount.Read(reader);
        _firstBsp2dReference.Read(reader);
      }
      public void ReadChildData(BinaryReader reader)
      {
      }
    }
    public class Bsp2dreferenceBlock : IBlock
    {
      private LongInteger _plane = new LongInteger();
      private LongInteger _bsp2dNode = new LongInteger();
      public Bsp2dreferenceBlock()
      {
      }
      public LongInteger Plane
      {
        get
        {
          return this._plane;
        }
        set
        {
          this._plane = value;
        }
      }
      public LongInteger Bsp2dNode
      {
        get
        {
          return this._bsp2dNode;
        }
        set
        {
          this._bsp2dNode = value;
        }
      }
      public void Read(BinaryReader reader)
      {
        _plane.Read(reader);
        _bsp2dNode.Read(reader);
      }
      public void ReadChildData(BinaryReader reader)
      {
      }
    }
    public class Bsp2dnodeBlock : IBlock
    {
      private RealPlane2D _plane = new RealPlane2D();
      private LongInteger _leftChild = new LongInteger();
      private LongInteger _rightChild = new LongInteger();
      public Bsp2dnodeBlock()
      {
      }
      public RealPlane2D Plane
      {
        get
        {
          return this._plane;
        }
        set
        {
          this._plane = value;
        }
      }
      public LongInteger LeftChild
      {
        get
        {
          return this._leftChild;
        }
        set
        {
          this._leftChild = value;
        }
      }
      public LongInteger RightChild
      {
        get
        {
          return this._rightChild;
        }
        set
        {
          this._rightChild = value;
        }
      }
      public void Read(BinaryReader reader)
      {
        _plane.Read(reader);
        _leftChild.Read(reader);
        _rightChild.Read(reader);
      }
      public void ReadChildData(BinaryReader reader)
      {
      }
    }
    public class SurfaceBlock : IBlock
    {
      private LongInteger _plane = new LongInteger();
      private LongInteger _firstEdge = new LongInteger();
      private Flags _flags;
      private CharInteger _breakableSurface = new CharInteger();
      private ShortInteger _material = new ShortInteger();
      public SurfaceBlock()
      {
        this._flags = new Flags(1);
      }
      public LongInteger Plane
      {
        get
        {
          return this._plane;
        }
        set
        {
          this._plane = value;
        }
      }
      public LongInteger FirstEdge
      {
        get
        {
          return this._firstEdge;
        }
        set
        {
          this._firstEdge = value;
        }
      }
      public Flags Flags
      {
        get
        {
          return this._flags;
        }
        set
        {
          this._flags = value;
        }
      }
      public CharInteger BreakableSurface
      {
        get
        {
          return this._breakableSurface;
        }
        set
        {
          this._breakableSurface = value;
        }
      }
      public ShortInteger Material
      {
        get
        {
          return this._material;
        }
        set
        {
          this._material = value;
        }
      }
      public void Read(BinaryReader reader)
      {
        _plane.Read(reader);
        _firstEdge.Read(reader);
        _flags.Read(reader);
        _breakableSurface.Read(reader);
        _material.Read(reader);
      }
      public void ReadChildData(BinaryReader reader)
      {
      }
    }
    public class EdgeBlock : IBlock
    {
      private LongInteger _startVertex = new LongInteger();
      private LongInteger _endVertex = new LongInteger();
      private LongInteger _forwardEdge = new LongInteger();
      private LongInteger _reverseEdge = new LongInteger();
      private LongInteger _leftSurface = new LongInteger();
      private LongInteger _rightSurface = new LongInteger();
      public EdgeBlock()
      {
      }
      public LongInteger StartVertex
      {
        get
        {
          return this._startVertex;
        }
        set
        {
          this._startVertex = value;
        }
      }
      public LongInteger EndVertex
      {
        get
        {
          return this._endVertex;
        }
        set
        {
          this._endVertex = value;
        }
      }
      public LongInteger ForwardEdge
      {
        get
        {
          return this._forwardEdge;
        }
        set
        {
          this._forwardEdge = value;
        }
      }
      public LongInteger ReverseEdge
      {
        get
        {
          return this._reverseEdge;
        }
        set
        {
          this._reverseEdge = value;
        }
      }
      public LongInteger LeftSurface
      {
        get
        {
          return this._leftSurface;
        }
        set
        {
          this._leftSurface = value;
        }
      }
      public LongInteger RightSurface
      {
        get
        {
          return this._rightSurface;
        }
        set
        {
          this._rightSurface = value;
        }
      }
      public void Read(BinaryReader reader)
      {
        _startVertex.Read(reader);
        _endVertex.Read(reader);
        _forwardEdge.Read(reader);
        _reverseEdge.Read(reader);
        _leftSurface.Read(reader);
        _rightSurface.Read(reader);
      }
      public void ReadChildData(BinaryReader reader)
      {
      }
    }
    public class VertexBlock : IBlock
    {
      private RealPoint3D _point = new RealPoint3D();
      private LongInteger _firstEdge = new LongInteger();
      public VertexBlock()
      {
      }
      public RealPoint3D Point
      {
        get
        {
          return this._point;
        }
        set
        {
          this._point = value;
        }
      }
      public LongInteger FirstEdge
      {
        get
        {
          return this._firstEdge;
        }
        set
        {
          this._firstEdge = value;
        }
      }
      public void Read(BinaryReader reader)
      {
        _point.Read(reader);
        _firstEdge.Read(reader);
      }
      public void ReadChildData(BinaryReader reader)
      {
      }
    }
    public class StructureBspNodeBlock : IBlock
    {
      private Skip _unnamed;
      public StructureBspNodeBlock()
      {
        this._unnamed = new Skip(6);
      }
      public void Read(BinaryReader reader)
      {
        _unnamed.Read(reader);
      }
      public void ReadChildData(BinaryReader reader)
      {
      }
    }
    public class StructureBspLeafBlock : IBlock
    {
      private Skip _unnamed;
      private Pad _unnamed2;
      private ShortInteger _cluster = new ShortInteger();
      private ShortInteger _surfaceReferenceCount = new ShortInteger();
      private LongBlockIndex _surfaceReferences = new LongBlockIndex();
      public StructureBspLeafBlock()
      {
        this._unnamed = new Skip(6);
        this._unnamed2 = new Pad(2);
      }
      public ShortInteger Cluster
      {
        get
        {
          return this._cluster;
        }
        set
        {
          this._cluster = value;
        }
      }
      public ShortInteger SurfaceReferenceCount
      {
        get
        {
          return this._surfaceReferenceCount;
        }
        set
        {
          this._surfaceReferenceCount = value;
        }
      }
      public LongBlockIndex SurfaceReferences
      {
        get
        {
          return this._surfaceReferences;
        }
        set
        {
          this._surfaceReferences = value;
        }
      }
      public void Read(BinaryReader reader)
      {
        _unnamed.Read(reader);
        _unnamed2.Read(reader);
        _cluster.Read(reader);
        _surfaceReferenceCount.Read(reader);
        _surfaceReferences.Read(reader);
      }
      public void ReadChildData(BinaryReader reader)
      {
      }
    }
    public class StructureBspSurfaceReferenceBlock : IBlock
    {
      private LongBlockIndex _surface = new LongBlockIndex();
      private LongBlockIndex _node = new LongBlockIndex();
      public StructureBspSurfaceReferenceBlock()
      {
      }
      public LongBlockIndex Surface
      {
        get
        {
          return this._surface;
        }
        set
        {
          this._surface = value;
        }
      }
      public LongBlockIndex Node
      {
        get
        {
          return this._node;
        }
        set
        {
          this._node = value;
        }
      }
      public void Read(BinaryReader reader)
      {
        _surface.Read(reader);
        _node.Read(reader);
      }
      public void ReadChildData(BinaryReader reader)
      {
      }
    }
    public class StructureBspSurfaceBlock : IBlock
    {
      private ShortBlockIndex _tria = new ShortBlockIndex();
      private ShortBlockIndex _trib = new ShortBlockIndex();
      private ShortBlockIndex _tric = new ShortBlockIndex();
      public StructureBspSurfaceBlock()
      {
      }
      public ShortBlockIndex Tria
      {
        get
        {
          return this._tria;
        }
        set
        {
          this._tria = value;
        }
      }
      public ShortBlockIndex Trib
      {
        get
        {
          return this._trib;
        }
        set
        {
          this._trib = value;
        }
      }
      public ShortBlockIndex Tric
      {
        get
        {
          return this._tric;
        }
        set
        {
          this._tric = value;
        }
      }
      public void Read(BinaryReader reader)
      {
        _tria.Read(reader);
        _trib.Read(reader);
        _tric.Read(reader);
      }
      public void ReadChildData(BinaryReader reader)
      {
      }
    }
    public class StructureBspLightmapBlock : IBlock
    {
      private ShortInteger _bitmap = new ShortInteger();
      private Pad _unnamed;
      private Pad _unnamed2;
      private Block _materials = new Block();
      private StructureBspMaterialBlockCollection _materialsCollection;
      public StructureBspLightmapBlock()
      {
        this._materialsCollection = new StructureBspMaterialBlockCollection(this._materials);
        this._unnamed = new Pad(2);
        this._unnamed2 = new Pad(16);
      }
      public StructureBspMaterialBlockCollection Materials
      {
        get
        {
          return this._materialsCollection;
        }
      }
      public ShortInteger Bitmap
      {
        get
        {
          return this._bitmap;
        }
        set
        {
          this._bitmap = value;
        }
      }
      public void Read(BinaryReader reader)
      {
        _bitmap.Read(reader);
        _unnamed.Read(reader);
        _unnamed2.Read(reader);
        _materials.Read(reader);
      }
      public void ReadChildData(BinaryReader reader)
      {
        int x = 0;
        for (x = 0; (x < _materials.Count); x = (x + 1))
        {
          Materials.AddNew();
          Materials[x].Read(reader);
        }
        for (x = 0; (x < _materials.Count); x = (x + 1))
        {
          Materials[x].ReadChildData(reader);
        }
      }
      public class StructureBspMaterialBlockCollection : System.Collections.CollectionBase
      {
        private Block linkedBlock;
        public StructureBspMaterialBlockCollection(Block linkedBlock)
        {
          this.linkedBlock = linkedBlock;
        }
        public StructureBspMaterialBlock this[int index]
        {
          get
          {
            return ((StructureBspMaterialBlock)(this.InnerList[index]));
          }
        }
        public void Add(StructureBspMaterialBlock block)
        {
          InnerList.Add(block);
          if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
        }
        public void AddNew()
        {
          this.Add(new StructureBspMaterialBlock());
        }
        public void Remove(int index)
        {
          InnerList.RemoveAt(index);
          if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
        }
      }
    }
    public class StructureBspMaterialBlock : IBlock
    {
      private TagReference _shader = new TagReference();
      private ShortInteger _shaderPermutation = new ShortInteger();
      private Flags _flags;
      private LongBlockIndex _surfaces = new LongBlockIndex();
      private LongInteger _surfaceCount = new LongInteger();
      private RealPoint3D _centroid = new RealPoint3D();
      private RealRGBColor _ambientColor = new RealRGBColor();
      private ShortInteger _distantLightCount = new ShortInteger();
      private Pad _unnamed;
      private RealRGBColor _distantLight0Color = new RealRGBColor();
      private RealVector3D _distantLight0Direction = new RealVector3D();
      private RealRGBColor _distantLight1Color = new RealRGBColor();
      private RealVector3D _distantLight1Direction = new RealVector3D();
      private Pad _unnamed2;
      private RealARGBColor _reflectionTint = new RealARGBColor();
      private RealVector3D _shadowVector = new RealVector3D();
      private RealRGBColor _shadowColor = new RealRGBColor();
      private RealPlane3D _plane = new RealPlane3D();
      private ShortInteger _breakableSurface = new ShortInteger();
      private Pad _unnamed3;
      private Pad _grensFix1;
      private Data _uncompressedVertices = new Data();
      private Data _compressedVertices = new Data();
      public StructureBspMaterialBlock()
      {
        this._flags = new Flags(2);
        this._unnamed = new Pad(2);
        this._unnamed2 = new Pad(12);
        this._unnamed3 = new Pad(2);
        this._grensFix1 = new Pad(40);
      }
      public TagReference Shader
      {
        get
        {
          return this._shader;
        }
        set
        {
          this._shader = value;
        }
      }
      public ShortInteger ShaderPermutation
      {
        get
        {
          return this._shaderPermutation;
        }
        set
        {
          this._shaderPermutation = value;
        }
      }
      public Flags Flags
      {
        get
        {
          return this._flags;
        }
        set
        {
          this._flags = value;
        }
      }
      public LongBlockIndex Surfaces
      {
        get
        {
          return this._surfaces;
        }
        set
        {
          this._surfaces = value;
        }
      }
      public LongInteger SurfaceCount
      {
        get
        {
          return this._surfaceCount;
        }
        set
        {
          this._surfaceCount = value;
        }
      }
      public RealPoint3D Centroid
      {
        get
        {
          return this._centroid;
        }
        set
        {
          this._centroid = value;
        }
      }
      public RealRGBColor AmbientColor
      {
        get
        {
          return this._ambientColor;
        }
        set
        {
          this._ambientColor = value;
        }
      }
      public ShortInteger DistantLightCount
      {
        get
        {
          return this._distantLightCount;
        }
        set
        {
          this._distantLightCount = value;
        }
      }
      public RealRGBColor DistantLight0Color
      {
        get
        {
          return this._distantLight0Color;
        }
        set
        {
          this._distantLight0Color = value;
        }
      }
      public RealVector3D DistantLight0Direction
      {
        get
        {
          return this._distantLight0Direction;
        }
        set
        {
          this._distantLight0Direction = value;
        }
      }
      public RealRGBColor DistantLight1Color
      {
        get
        {
          return this._distantLight1Color;
        }
        set
        {
          this._distantLight1Color = value;
        }
      }
      public RealVector3D DistantLight1Direction
      {
        get
        {
          return this._distantLight1Direction;
        }
        set
        {
          this._distantLight1Direction = value;
        }
      }
      public RealARGBColor ReflectionTint
      {
        get
        {
          return this._reflectionTint;
        }
        set
        {
          this._reflectionTint = value;
        }
      }
      public RealVector3D ShadowVector
      {
        get
        {
          return this._shadowVector;
        }
        set
        {
          this._shadowVector = value;
        }
      }
      public RealRGBColor ShadowColor
      {
        get
        {
          return this._shadowColor;
        }
        set
        {
          this._shadowColor = value;
        }
      }
      public RealPlane3D Plane
      {
        get
        {
          return this._plane;
        }
        set
        {
          this._plane = value;
        }
      }
      public ShortInteger BreakableSurface
      {
        get
        {
          return this._breakableSurface;
        }
        set
        {
          this._breakableSurface = value;
        }
      }
      public Data UncompressedVertices
      {
        get
        {
          return this._uncompressedVertices;
        }
        set
        {
          this._uncompressedVertices = value;
        }
      }
      public Data CompressedVertices
      {
        get
        {
          return this._compressedVertices;
        }
        set
        {
          this._compressedVertices = value;
        }
      }
      public void Read(BinaryReader reader)
      {
        _shader.Read(reader);
        _shaderPermutation.Read(reader);
        _flags.Read(reader);
        _surfaces.Read(reader);
        _surfaceCount.Read(reader);
        _centroid.Read(reader);
        _ambientColor.Read(reader);
        _distantLightCount.Read(reader);
        _unnamed.Read(reader);
        _distantLight0Color.Read(reader);
        _distantLight0Direction.Read(reader);
        _distantLight1Color.Read(reader);
        _distantLight1Direction.Read(reader);
        _unnamed2.Read(reader);
        _reflectionTint.Read(reader);
        _shadowVector.Read(reader);
        _shadowColor.Read(reader);
        _plane.Read(reader);
        _breakableSurface.Read(reader);
        _unnamed3.Read(reader);
        _grensFix1.Read(reader);
        _uncompressedVertices.Read(reader);
        _compressedVertices.Read(reader);
      }
      public void ReadChildData(BinaryReader reader)
      {
        _shader.ReadString(reader);
        _uncompressedVertices.ReadBinary(reader);
        _compressedVertices.ReadBinary(reader);
      }
    }
    public class StructureBspLensFlareBlock : IBlock
    {
      private TagReference _lensFlare = new TagReference();
      public StructureBspLensFlareBlock()
      {
      }
      public TagReference LensFlare
      {
        get
        {
          return this._lensFlare;
        }
        set
        {
          this._lensFlare = value;
        }
      }
      public void Read(BinaryReader reader)
      {
        _lensFlare.Read(reader);
      }
      public void ReadChildData(BinaryReader reader)
      {
        _lensFlare.ReadString(reader);
      }
    }
    public class StructureBspLensFlareMarkerBlock : IBlock
    {
      private RealPoint3D _position = new RealPoint3D();
      private CharInteger _direction = new CharInteger();
      private CharInteger _direction2 = new CharInteger();
      private CharInteger _direction3 = new CharInteger();
      private CharInteger _lensFlareIndex = new CharInteger();
      public StructureBspLensFlareMarkerBlock()
      {
      }
      public RealPoint3D Position
      {
        get
        {
          return this._position;
        }
        set
        {
          this._position = value;
        }
      }
      public CharInteger Direction
      {
        get
        {
          return this._direction;
        }
        set
        {
          this._direction = value;
        }
      }
      public CharInteger Direction2
      {
        get
        {
          return this._direction2;
        }
        set
        {
          this._direction2 = value;
        }
      }
      public CharInteger Direction3
      {
        get
        {
          return this._direction3;
        }
        set
        {
          this._direction3 = value;
        }
      }
      public CharInteger LensFlareIndex
      {
        get
        {
          return this._lensFlareIndex;
        }
        set
        {
          this._lensFlareIndex = value;
        }
      }
      public void Read(BinaryReader reader)
      {
        _position.Read(reader);
        _direction.Read(reader);
        _direction2.Read(reader);
        _direction3.Read(reader);
        _lensFlareIndex.Read(reader);
      }
      public void ReadChildData(BinaryReader reader)
      {
      }
    }
    public class StructureBspClusterBlock : IBlock
    {
      private ShortInteger _sky = new ShortInteger();
      private ShortInteger _fog = new ShortInteger();
      private ShortBlockIndex _backgroundSound = new ShortBlockIndex();
      private ShortBlockIndex _soundEnvironment = new ShortBlockIndex();
      private ShortBlockIndex _weather = new ShortBlockIndex();
      private ShortInteger _transitionStructureBsp = new ShortInteger();
      private Pad _unnamed;
      private Pad _unnamed2;
      private Block _predictedResources = new Block();
      private Block _subclusters = new Block();
      private ShortInteger _firstLensFlareMarkerIndex = new ShortInteger();
      private ShortInteger _lensFlareMarkerCount = new ShortInteger();
      private Block _surfaceIndices = new Block();
      private Block _mirrors = new Block();
      private Block _portals = new Block();
      private PredictedResourceBlockCollection _predictedResourcesCollection;
      private StructureBspSubclusterBlockCollection _subclustersCollection;
      private StructureBspClusterSurfaceIndexBlockCollection _surfaceIndicesCollection;
      private StructureBspMirrorBlockCollection _mirrorsCollection;
      private StructureBspClusterPortalIndexBlockCollection _portalsCollection;
      public StructureBspClusterBlock()
      {
        this._predictedResourcesCollection = new PredictedResourceBlockCollection(this._predictedResources);
        this._subclustersCollection = new StructureBspSubclusterBlockCollection(this._subclusters);
        this._surfaceIndicesCollection = new StructureBspClusterSurfaceIndexBlockCollection(this._surfaceIndices);
        this._mirrorsCollection = new StructureBspMirrorBlockCollection(this._mirrors);
        this._portalsCollection = new StructureBspClusterPortalIndexBlockCollection(this._portals);
        this._unnamed = new Pad(4);
        this._unnamed2 = new Pad(24);
      }
      public PredictedResourceBlockCollection PredictedResources
      {
        get
        {
          return this._predictedResourcesCollection;
        }
      }
      public StructureBspSubclusterBlockCollection Subclusters
      {
        get
        {
          return this._subclustersCollection;
        }
      }
      public StructureBspClusterSurfaceIndexBlockCollection SurfaceIndices
      {
        get
        {
          return this._surfaceIndicesCollection;
        }
      }
      public StructureBspMirrorBlockCollection Mirrors
      {
        get
        {
          return this._mirrorsCollection;
        }
      }
      public StructureBspClusterPortalIndexBlockCollection Portals
      {
        get
        {
          return this._portalsCollection;
        }
      }
      public ShortInteger Sky
      {
        get
        {
          return this._sky;
        }
        set
        {
          this._sky = value;
        }
      }
      public ShortInteger Fog
      {
        get
        {
          return this._fog;
        }
        set
        {
          this._fog = value;
        }
      }
      public ShortBlockIndex BackgroundSound
      {
        get
        {
          return this._backgroundSound;
        }
        set
        {
          this._backgroundSound = value;
        }
      }
      public ShortBlockIndex SoundEnvironment
      {
        get
        {
          return this._soundEnvironment;
        }
        set
        {
          this._soundEnvironment = value;
        }
      }
      public ShortBlockIndex Weather
      {
        get
        {
          return this._weather;
        }
        set
        {
          this._weather = value;
        }
      }
      public ShortInteger TransitionStructureBsp
      {
        get
        {
          return this._transitionStructureBsp;
        }
        set
        {
          this._transitionStructureBsp = value;
        }
      }
      public ShortInteger FirstLensFlareMarkerIndex
      {
        get
        {
          return this._firstLensFlareMarkerIndex;
        }
        set
        {
          this._firstLensFlareMarkerIndex = value;
        }
      }
      public ShortInteger LensFlareMarkerCount
      {
        get
        {
          return this._lensFlareMarkerCount;
        }
        set
        {
          this._lensFlareMarkerCount = value;
        }
      }
      public void Read(BinaryReader reader)
      {
        _sky.Read(reader);
        _fog.Read(reader);
        _backgroundSound.Read(reader);
        _soundEnvironment.Read(reader);
        _weather.Read(reader);
        _transitionStructureBsp.Read(reader);
        _unnamed.Read(reader);
        _unnamed2.Read(reader);
        _predictedResources.Read(reader);
        _subclusters.Read(reader);
        _firstLensFlareMarkerIndex.Read(reader);
        _lensFlareMarkerCount.Read(reader);
        _surfaceIndices.Read(reader);
        _mirrors.Read(reader);
        _portals.Read(reader);
      }
      public void ReadChildData(BinaryReader reader)
      {
        int x = 0;
        for (x = 0; (x < _predictedResources.Count); x = (x + 1))
        {
          PredictedResources.AddNew();
          PredictedResources[x].Read(reader);
        }
        for (x = 0; (x < _predictedResources.Count); x = (x + 1))
        {
          PredictedResources[x].ReadChildData(reader);
        }
        for (x = 0; (x < _subclusters.Count); x = (x + 1))
        {
          Subclusters.AddNew();
          Subclusters[x].Read(reader);
        }
        for (x = 0; (x < _subclusters.Count); x = (x + 1))
        {
          Subclusters[x].ReadChildData(reader);
        }
        for (x = 0; (x < _surfaceIndices.Count); x = (x + 1))
        {
          SurfaceIndices.AddNew();
          SurfaceIndices[x].Read(reader);
        }
        for (x = 0; (x < _surfaceIndices.Count); x = (x + 1))
        {
          SurfaceIndices[x].ReadChildData(reader);
        }
        for (x = 0; (x < _mirrors.Count); x = (x + 1))
        {
          Mirrors.AddNew();
          Mirrors[x].Read(reader);
        }
        for (x = 0; (x < _mirrors.Count); x = (x + 1))
        {
          Mirrors[x].ReadChildData(reader);
        }
        for (x = 0; (x < _portals.Count); x = (x + 1))
        {
          Portals.AddNew();
          Portals[x].Read(reader);
        }
        for (x = 0; (x < _portals.Count); x = (x + 1))
        {
          Portals[x].ReadChildData(reader);
        }
      }
      public class PredictedResourceBlockCollection : System.Collections.CollectionBase
      {
        private Block linkedBlock;
        public PredictedResourceBlockCollection(Block linkedBlock)
        {
          this.linkedBlock = linkedBlock;
        }
        public PredictedResourceBlock this[int index]
        {
          get
          {
            return ((PredictedResourceBlock)(this.InnerList[index]));
          }
        }
        public void Add(PredictedResourceBlock block)
        {
          InnerList.Add(block);
          if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
        }
        public void AddNew()
        {
          this.Add(new PredictedResourceBlock());
        }
        public void Remove(int index)
        {
          InnerList.RemoveAt(index);
          if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
        }
      }
      public class StructureBspSubclusterBlockCollection : System.Collections.CollectionBase
      {
        private Block linkedBlock;
        public StructureBspSubclusterBlockCollection(Block linkedBlock)
        {
          this.linkedBlock = linkedBlock;
        }
        public StructureBspSubclusterBlock this[int index]
        {
          get
          {
            return ((StructureBspSubclusterBlock)(this.InnerList[index]));
          }
        }
        public void Add(StructureBspSubclusterBlock block)
        {
          InnerList.Add(block);
          if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
        }
        public void AddNew()
        {
          this.Add(new StructureBspSubclusterBlock());
        }
        public void Remove(int index)
        {
          InnerList.RemoveAt(index);
          if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
        }
      }
      public class StructureBspClusterSurfaceIndexBlockCollection : System.Collections.CollectionBase
      {
        private Block linkedBlock;
        public StructureBspClusterSurfaceIndexBlockCollection(Block linkedBlock)
        {
          this.linkedBlock = linkedBlock;
        }
        public StructureBspClusterSurfaceIndexBlock this[int index]
        {
          get
          {
            return ((StructureBspClusterSurfaceIndexBlock)(this.InnerList[index]));
          }
        }
        public void Add(StructureBspClusterSurfaceIndexBlock block)
        {
          InnerList.Add(block);
          if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
        }
        public void AddNew()
        {
          this.Add(new StructureBspClusterSurfaceIndexBlock());
        }
        public void Remove(int index)
        {
          InnerList.RemoveAt(index);
          if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
        }
      }
      public class StructureBspMirrorBlockCollection : System.Collections.CollectionBase
      {
        private Block linkedBlock;
        public StructureBspMirrorBlockCollection(Block linkedBlock)
        {
          this.linkedBlock = linkedBlock;
        }
        public StructureBspMirrorBlock this[int index]
        {
          get
          {
            return ((StructureBspMirrorBlock)(this.InnerList[index]));
          }
        }
        public void Add(StructureBspMirrorBlock block)
        {
          InnerList.Add(block);
          if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
        }
        public void AddNew()
        {
          this.Add(new StructureBspMirrorBlock());
        }
        public void Remove(int index)
        {
          InnerList.RemoveAt(index);
          if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
        }
      }
      public class StructureBspClusterPortalIndexBlockCollection : System.Collections.CollectionBase
      {
        private Block linkedBlock;
        public StructureBspClusterPortalIndexBlockCollection(Block linkedBlock)
        {
          this.linkedBlock = linkedBlock;
        }
        public StructureBspClusterPortalIndexBlock this[int index]
        {
          get
          {
            return ((StructureBspClusterPortalIndexBlock)(this.InnerList[index]));
          }
        }
        public void Add(StructureBspClusterPortalIndexBlock block)
        {
          InnerList.Add(block);
          if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
        }
        public void AddNew()
        {
          this.Add(new StructureBspClusterPortalIndexBlock());
        }
        public void Remove(int index)
        {
          InnerList.RemoveAt(index);
          if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
        }
      }
    }
    public class PredictedResourceBlock : IBlock
    {
      private Enum _type = new Enum();
      private ShortInteger _resourceIndex = new ShortInteger();
      private LongInteger _tagIndex = new LongInteger();
      public PredictedResourceBlock()
      {
      }
      public Enum Type
      {
        get
        {
          return this._type;
        }
        set
        {
          this._type = value;
        }
      }
      public ShortInteger ResourceIndex
      {
        get
        {
          return this._resourceIndex;
        }
        set
        {
          this._resourceIndex = value;
        }
      }
      public LongInteger TagIndex
      {
        get
        {
          return this._tagIndex;
        }
        set
        {
          this._tagIndex = value;
        }
      }
      public void Read(BinaryReader reader)
      {
        _type.Read(reader);
        _resourceIndex.Read(reader);
        _tagIndex.Read(reader);
      }
      public void ReadChildData(BinaryReader reader)
      {
      }
    }
    public class StructureBspSubclusterBlock : IBlock
    {
      private RealBounds _worldBoundsX = new RealBounds();
      private RealBounds _worldBoundsY = new RealBounds();
      private RealBounds _worldBoundsZ = new RealBounds();
      private Block _surfaceIndices = new Block();
      private StructureBspSubclusterSurfaceIndexBlockCollection _surfaceIndicesCollection;
      public StructureBspSubclusterBlock()
      {
        this._surfaceIndicesCollection = new StructureBspSubclusterSurfaceIndexBlockCollection(this._surfaceIndices);
      }
      public StructureBspSubclusterSurfaceIndexBlockCollection SurfaceIndices
      {
        get
        {
          return this._surfaceIndicesCollection;
        }
      }
      public RealBounds WorldBoundsX
      {
        get
        {
          return this._worldBoundsX;
        }
        set
        {
          this._worldBoundsX = value;
        }
      }
      public RealBounds WorldBoundsY
      {
        get
        {
          return this._worldBoundsY;
        }
        set
        {
          this._worldBoundsY = value;
        }
      }
      public RealBounds WorldBoundsZ
      {
        get
        {
          return this._worldBoundsZ;
        }
        set
        {
          this._worldBoundsZ = value;
        }
      }
      public void Read(BinaryReader reader)
      {
        _worldBoundsX.Read(reader);
        _worldBoundsY.Read(reader);
        _worldBoundsZ.Read(reader);
        _surfaceIndices.Read(reader);
      }
      public void ReadChildData(BinaryReader reader)
      {
        int x = 0;
        for (x = 0; (x < _surfaceIndices.Count); x = (x + 1))
        {
          SurfaceIndices.AddNew();
          SurfaceIndices[x].Read(reader);
        }
        for (x = 0; (x < _surfaceIndices.Count); x = (x + 1))
        {
          SurfaceIndices[x].ReadChildData(reader);
        }
      }
      public class StructureBspSubclusterSurfaceIndexBlockCollection : System.Collections.CollectionBase
      {
        private Block linkedBlock;
        public StructureBspSubclusterSurfaceIndexBlockCollection(Block linkedBlock)
        {
          this.linkedBlock = linkedBlock;
        }
        public StructureBspSubclusterSurfaceIndexBlock this[int index]
        {
          get
          {
            return ((StructureBspSubclusterSurfaceIndexBlock)(this.InnerList[index]));
          }
        }
        public void Add(StructureBspSubclusterSurfaceIndexBlock block)
        {
          InnerList.Add(block);
          if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
        }
        public void AddNew()
        {
          this.Add(new StructureBspSubclusterSurfaceIndexBlock());
        }
        public void Remove(int index)
        {
          InnerList.RemoveAt(index);
          if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
        }
      }
    }
    public class StructureBspSubclusterSurfaceIndexBlock : IBlock
    {
      private LongInteger _index = new LongInteger();
      public StructureBspSubclusterSurfaceIndexBlock()
      {
      }
      public LongInteger Index
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
      public void Read(BinaryReader reader)
      {
        _index.Read(reader);
      }
      public void ReadChildData(BinaryReader reader)
      {
      }
    }
    public class StructureBspClusterSurfaceIndexBlock : IBlock
    {
      private LongInteger _index = new LongInteger();
      public StructureBspClusterSurfaceIndexBlock()
      {
      }
      public LongInteger Index
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
      public void Read(BinaryReader reader)
      {
        _index.Read(reader);
      }
      public void ReadChildData(BinaryReader reader)
      {
      }
    }
    public class StructureBspMirrorBlock : IBlock
    {
      private RealPlane3D _plane = new RealPlane3D();
      private Pad _unnamed;
      private TagReference _shader = new TagReference();
      private Block _vertices = new Block();
      private StructureBspMirrorVertexBlockCollection _verticesCollection;
      public StructureBspMirrorBlock()
      {
        this._verticesCollection = new StructureBspMirrorVertexBlockCollection(this._vertices);
        this._unnamed = new Pad(20);
      }
      public StructureBspMirrorVertexBlockCollection Vertices
      {
        get
        {
          return this._verticesCollection;
        }
      }
      public RealPlane3D Plane
      {
        get
        {
          return this._plane;
        }
        set
        {
          this._plane = value;
        }
      }
      public TagReference Shader
      {
        get
        {
          return this._shader;
        }
        set
        {
          this._shader = value;
        }
      }
      public void Read(BinaryReader reader)
      {
        _plane.Read(reader);
        _unnamed.Read(reader);
        _shader.Read(reader);
        _vertices.Read(reader);
      }
      public void ReadChildData(BinaryReader reader)
      {
        int x = 0;
        _shader.ReadString(reader);
        for (x = 0; (x < _vertices.Count); x = (x + 1))
        {
          Vertices.AddNew();
          Vertices[x].Read(reader);
        }
        for (x = 0; (x < _vertices.Count); x = (x + 1))
        {
          Vertices[x].ReadChildData(reader);
        }
      }
      public class StructureBspMirrorVertexBlockCollection : System.Collections.CollectionBase
      {
        private Block linkedBlock;
        public StructureBspMirrorVertexBlockCollection(Block linkedBlock)
        {
          this.linkedBlock = linkedBlock;
        }
        public StructureBspMirrorVertexBlock this[int index]
        {
          get
          {
            return ((StructureBspMirrorVertexBlock)(this.InnerList[index]));
          }
        }
        public void Add(StructureBspMirrorVertexBlock block)
        {
          InnerList.Add(block);
          if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
        }
        public void AddNew()
        {
          this.Add(new StructureBspMirrorVertexBlock());
        }
        public void Remove(int index)
        {
          InnerList.RemoveAt(index);
          if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
        }
      }
    }
    public class StructureBspMirrorVertexBlock : IBlock
    {
      private RealPoint3D _point = new RealPoint3D();
      public StructureBspMirrorVertexBlock()
      {
      }
      public RealPoint3D Point
      {
        get
        {
          return this._point;
        }
        set
        {
          this._point = value;
        }
      }
      public void Read(BinaryReader reader)
      {
        _point.Read(reader);
      }
      public void ReadChildData(BinaryReader reader)
      {
      }
    }
    public class StructureBspClusterPortalIndexBlock : IBlock
    {
      private ShortInteger _portal = new ShortInteger();
      public StructureBspClusterPortalIndexBlock()
      {
      }
      public ShortInteger Portal
      {
        get
        {
          return this._portal;
        }
        set
        {
          this._portal = value;
        }
      }
      public void Read(BinaryReader reader)
      {
        _portal.Read(reader);
      }
      public void ReadChildData(BinaryReader reader)
      {
      }
    }
    public class StructureBspClusterPortalBlock : IBlock
    {
      private ShortInteger _frontCluster = new ShortInteger();
      private ShortInteger _backCluster = new ShortInteger();
      private LongInteger _planeIndex = new LongInteger();
      private RealPoint3D _centroid = new RealPoint3D();
      private Real _boundingRadius = new Real();
      private Flags _flags;
      private Pad _unnamed;
      private Block _vertices = new Block();
      private StructureBspClusterPortalVertexBlockCollection _verticesCollection;
      public StructureBspClusterPortalBlock()
      {
        this._verticesCollection = new StructureBspClusterPortalVertexBlockCollection(this._vertices);
        this._flags = new Flags(4);
        this._unnamed = new Pad(24);
      }
      public StructureBspClusterPortalVertexBlockCollection Vertices
      {
        get
        {
          return this._verticesCollection;
        }
      }
      public ShortInteger FrontCluster
      {
        get
        {
          return this._frontCluster;
        }
        set
        {
          this._frontCluster = value;
        }
      }
      public ShortInteger BackCluster
      {
        get
        {
          return this._backCluster;
        }
        set
        {
          this._backCluster = value;
        }
      }
      public LongInteger PlaneIndex
      {
        get
        {
          return this._planeIndex;
        }
        set
        {
          this._planeIndex = value;
        }
      }
      public RealPoint3D Centroid
      {
        get
        {
          return this._centroid;
        }
        set
        {
          this._centroid = value;
        }
      }
      public Real BoundingRadius
      {
        get
        {
          return this._boundingRadius;
        }
        set
        {
          this._boundingRadius = value;
        }
      }
      public Flags Flags
      {
        get
        {
          return this._flags;
        }
        set
        {
          this._flags = value;
        }
      }
      public void Read(BinaryReader reader)
      {
        _frontCluster.Read(reader);
        _backCluster.Read(reader);
        _planeIndex.Read(reader);
        _centroid.Read(reader);
        _boundingRadius.Read(reader);
        _flags.Read(reader);
        _unnamed.Read(reader);
        _vertices.Read(reader);
      }
      public void ReadChildData(BinaryReader reader)
      {
        int x = 0;
        for (x = 0; (x < _vertices.Count); x = (x + 1))
        {
          Vertices.AddNew();
          Vertices[x].Read(reader);
        }
        for (x = 0; (x < _vertices.Count); x = (x + 1))
        {
          Vertices[x].ReadChildData(reader);
        }
      }
      public class StructureBspClusterPortalVertexBlockCollection : System.Collections.CollectionBase
      {
        private Block linkedBlock;
        public StructureBspClusterPortalVertexBlockCollection(Block linkedBlock)
        {
          this.linkedBlock = linkedBlock;
        }
        public StructureBspClusterPortalVertexBlock this[int index]
        {
          get
          {
            return ((StructureBspClusterPortalVertexBlock)(this.InnerList[index]));
          }
        }
        public void Add(StructureBspClusterPortalVertexBlock block)
        {
          InnerList.Add(block);
          if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
        }
        public void AddNew()
        {
          this.Add(new StructureBspClusterPortalVertexBlock());
        }
        public void Remove(int index)
        {
          InnerList.RemoveAt(index);
          if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
        }
      }
    }
    public class StructureBspClusterPortalVertexBlock : IBlock
    {
      private RealPoint3D _point = new RealPoint3D();
      public StructureBspClusterPortalVertexBlock()
      {
      }
      public RealPoint3D Point
      {
        get
        {
          return this._point;
        }
        set
        {
          this._point = value;
        }
      }
      public void Read(BinaryReader reader)
      {
        _point.Read(reader);
      }
      public void ReadChildData(BinaryReader reader)
      {
      }
    }
    public class StructureBspBreakableSurfaceBlock : IBlock
    {
      private RealPoint3D _centroid = new RealPoint3D();
      private Real _radius = new Real();
      private LongInteger _collisionSurfaceIndex = new LongInteger();
      private Pad _unnamed;
      public StructureBspBreakableSurfaceBlock()
      {
        this._unnamed = new Pad(28);
      }
      public RealPoint3D Centroid
      {
        get
        {
          return this._centroid;
        }
        set
        {
          this._centroid = value;
        }
      }
      public Real Radius
      {
        get
        {
          return this._radius;
        }
        set
        {
          this._radius = value;
        }
      }
      public LongInteger CollisionSurfaceIndex
      {
        get
        {
          return this._collisionSurfaceIndex;
        }
        set
        {
          this._collisionSurfaceIndex = value;
        }
      }
      public void Read(BinaryReader reader)
      {
        _centroid.Read(reader);
        _radius.Read(reader);
        _collisionSurfaceIndex.Read(reader);
        _unnamed.Read(reader);
      }
      public void ReadChildData(BinaryReader reader)
      {
      }
    }
    public class StructureBspFogPlaneBlock : IBlock
    {
      private ShortBlockIndex _frontRegion = new ShortBlockIndex();
      private Pad _unnamed;
      private RealPlane3D _plane = new RealPlane3D();
      private Block _vertices = new Block();
      private StructureBspFogPlaneVertexBlockCollection _verticesCollection;
      public StructureBspFogPlaneBlock()
      {
        this._verticesCollection = new StructureBspFogPlaneVertexBlockCollection(this._vertices);
        this._unnamed = new Pad(2);
      }
      public StructureBspFogPlaneVertexBlockCollection Vertices
      {
        get
        {
          return this._verticesCollection;
        }
      }
      public ShortBlockIndex FrontRegion
      {
        get
        {
          return this._frontRegion;
        }
        set
        {
          this._frontRegion = value;
        }
      }
      public RealPlane3D Plane
      {
        get
        {
          return this._plane;
        }
        set
        {
          this._plane = value;
        }
      }
      public void Read(BinaryReader reader)
      {
        _frontRegion.Read(reader);
        _unnamed.Read(reader);
        _plane.Read(reader);
        _vertices.Read(reader);
      }
      public void ReadChildData(BinaryReader reader)
      {
        int x = 0;
        for (x = 0; (x < _vertices.Count); x = (x + 1))
        {
          Vertices.AddNew();
          Vertices[x].Read(reader);
        }
        for (x = 0; (x < _vertices.Count); x = (x + 1))
        {
          Vertices[x].ReadChildData(reader);
        }
      }
      public class StructureBspFogPlaneVertexBlockCollection : System.Collections.CollectionBase
      {
        private Block linkedBlock;
        public StructureBspFogPlaneVertexBlockCollection(Block linkedBlock)
        {
          this.linkedBlock = linkedBlock;
        }
        public StructureBspFogPlaneVertexBlock this[int index]
        {
          get
          {
            return ((StructureBspFogPlaneVertexBlock)(this.InnerList[index]));
          }
        }
        public void Add(StructureBspFogPlaneVertexBlock block)
        {
          InnerList.Add(block);
          if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
        }
        public void AddNew()
        {
          this.Add(new StructureBspFogPlaneVertexBlock());
        }
        public void Remove(int index)
        {
          InnerList.RemoveAt(index);
          if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
        }
      }
    }
    public class StructureBspFogPlaneVertexBlock : IBlock
    {
      private RealPoint3D _point = new RealPoint3D();
      public StructureBspFogPlaneVertexBlock()
      {
      }
      public RealPoint3D Point
      {
        get
        {
          return this._point;
        }
        set
        {
          this._point = value;
        }
      }
      public void Read(BinaryReader reader)
      {
        _point.Read(reader);
      }
      public void ReadChildData(BinaryReader reader)
      {
      }
    }
    public class StructureBspFogRegionBlock : IBlock
    {
      private Pad _unnamed;
      private ShortBlockIndex _fogPalette = new ShortBlockIndex();
      private ShortBlockIndex _weatherPalette = new ShortBlockIndex();
      public StructureBspFogRegionBlock()
      {
        this._unnamed = new Pad(36);
      }
      public ShortBlockIndex FogPalette
      {
        get
        {
          return this._fogPalette;
        }
        set
        {
          this._fogPalette = value;
        }
      }
      public ShortBlockIndex WeatherPalette
      {
        get
        {
          return this._weatherPalette;
        }
        set
        {
          this._weatherPalette = value;
        }
      }
      public void Read(BinaryReader reader)
      {
        _unnamed.Read(reader);
        _fogPalette.Read(reader);
        _weatherPalette.Read(reader);
      }
      public void ReadChildData(BinaryReader reader)
      {
      }
    }
    public class StructureBspFogPaletteBlock : IBlock
    {
      private FixedLengthString _name = new FixedLengthString();
      private TagReference _fog = new TagReference();
      private Pad _unnamed;
      private FixedLengthString _fogScaleFunction = new FixedLengthString();
      private Pad _unnamed2;
      public StructureBspFogPaletteBlock()
      {
        this._unnamed = new Pad(4);
        this._unnamed2 = new Pad(52);
      }
      public FixedLengthString Name
      {
        get
        {
          return this._name;
        }
        set
        {
          this._name = value;
        }
      }
      public TagReference Fog
      {
        get
        {
          return this._fog;
        }
        set
        {
          this._fog = value;
        }
      }
      public FixedLengthString FogScaleFunction
      {
        get
        {
          return this._fogScaleFunction;
        }
        set
        {
          this._fogScaleFunction = value;
        }
      }
      public void Read(BinaryReader reader)
      {
        _name.Read(reader);
        _fog.Read(reader);
        _unnamed.Read(reader);
        _fogScaleFunction.Read(reader);
        _unnamed2.Read(reader);
      }
      public void ReadChildData(BinaryReader reader)
      {
        _fog.ReadString(reader);
      }
    }
    public class StructureBspWeatherPaletteBlock : IBlock
    {
      private FixedLengthString _name = new FixedLengthString();
      private TagReference _particleSystem = new TagReference();
      private Pad _unnamed;
      private FixedLengthString _particleSystemScaleFunction = new FixedLengthString();
      private Pad _unnamed2;
      private TagReference _wind = new TagReference();
      private RealVector3D _windDirection = new RealVector3D();
      private Real _windMagnitude = new Real();
      private Pad _unnamed3;
      private FixedLengthString _windScaleFunction = new FixedLengthString();
      private Pad _unnamed4;
      public StructureBspWeatherPaletteBlock()
      {
        this._unnamed = new Pad(4);
        this._unnamed2 = new Pad(44);
        this._unnamed3 = new Pad(4);
        this._unnamed4 = new Pad(44);
      }
      public FixedLengthString Name
      {
        get
        {
          return this._name;
        }
        set
        {
          this._name = value;
        }
      }
      public TagReference ParticleSystem
      {
        get
        {
          return this._particleSystem;
        }
        set
        {
          this._particleSystem = value;
        }
      }
      public FixedLengthString ParticleSystemScaleFunction
      {
        get
        {
          return this._particleSystemScaleFunction;
        }
        set
        {
          this._particleSystemScaleFunction = value;
        }
      }
      public TagReference Wind
      {
        get
        {
          return this._wind;
        }
        set
        {
          this._wind = value;
        }
      }
      public RealVector3D WindDirection
      {
        get
        {
          return this._windDirection;
        }
        set
        {
          this._windDirection = value;
        }
      }
      public Real WindMagnitude
      {
        get
        {
          return this._windMagnitude;
        }
        set
        {
          this._windMagnitude = value;
        }
      }
      public FixedLengthString WindScaleFunction
      {
        get
        {
          return this._windScaleFunction;
        }
        set
        {
          this._windScaleFunction = value;
        }
      }
      public void Read(BinaryReader reader)
      {
        _name.Read(reader);
        _particleSystem.Read(reader);
        _unnamed.Read(reader);
        _particleSystemScaleFunction.Read(reader);
        _unnamed2.Read(reader);
        _wind.Read(reader);
        _windDirection.Read(reader);
        _windMagnitude.Read(reader);
        _unnamed3.Read(reader);
        _windScaleFunction.Read(reader);
        _unnamed4.Read(reader);
      }
      public void ReadChildData(BinaryReader reader)
      {
        _particleSystem.ReadString(reader);
        _wind.ReadString(reader);
      }
    }
    public class StructureBspWeatherPolyhedronBlock : IBlock
    {
      private RealPoint3D _boundingSphereCenter = new RealPoint3D();
      private Real _boundingSphereRadius = new Real();
      private Pad _unnamed;
      private Block _planes = new Block();
      private StructureBspWeatherPolyhedronPlaneBlockCollection _planesCollection;
      public StructureBspWeatherPolyhedronBlock()
      {
        this._planesCollection = new StructureBspWeatherPolyhedronPlaneBlockCollection(this._planes);
        this._unnamed = new Pad(4);
      }
      public StructureBspWeatherPolyhedronPlaneBlockCollection Planes
      {
        get
        {
          return this._planesCollection;
        }
      }
      public RealPoint3D BoundingSphereCenter
      {
        get
        {
          return this._boundingSphereCenter;
        }
        set
        {
          this._boundingSphereCenter = value;
        }
      }
      public Real BoundingSphereRadius
      {
        get
        {
          return this._boundingSphereRadius;
        }
        set
        {
          this._boundingSphereRadius = value;
        }
      }
      public void Read(BinaryReader reader)
      {
        _boundingSphereCenter.Read(reader);
        _boundingSphereRadius.Read(reader);
        _unnamed.Read(reader);
        _planes.Read(reader);
      }
      public void ReadChildData(BinaryReader reader)
      {
        int x = 0;
        for (x = 0; (x < _planes.Count); x = (x + 1))
        {
          Planes.AddNew();
          Planes[x].Read(reader);
        }
        for (x = 0; (x < _planes.Count); x = (x + 1))
        {
          Planes[x].ReadChildData(reader);
        }
      }
      public class StructureBspWeatherPolyhedronPlaneBlockCollection : System.Collections.CollectionBase
      {
        private Block linkedBlock;
        public StructureBspWeatherPolyhedronPlaneBlockCollection(Block linkedBlock)
        {
          this.linkedBlock = linkedBlock;
        }
        public StructureBspWeatherPolyhedronPlaneBlock this[int index]
        {
          get
          {
            return ((StructureBspWeatherPolyhedronPlaneBlock)(this.InnerList[index]));
          }
        }
        public void Add(StructureBspWeatherPolyhedronPlaneBlock block)
        {
          InnerList.Add(block);
          if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
        }
        public void AddNew()
        {
          this.Add(new StructureBspWeatherPolyhedronPlaneBlock());
        }
        public void Remove(int index)
        {
          InnerList.RemoveAt(index);
          if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
        }
      }
    }
    public class StructureBspWeatherPolyhedronPlaneBlock : IBlock
    {
      private RealPlane3D _plane = new RealPlane3D();
      public StructureBspWeatherPolyhedronPlaneBlock()
      {
      }
      public RealPlane3D Plane
      {
        get
        {
          return this._plane;
        }
        set
        {
          this._plane = value;
        }
      }
      public void Read(BinaryReader reader)
      {
        _plane.Read(reader);
      }
      public void ReadChildData(BinaryReader reader)
      {
      }
    }
    public class StructureBspPathfindingSurfacesBlock : IBlock
    {
      private CharInteger _data = new CharInteger();
      public StructureBspPathfindingSurfacesBlock()
      {
      }
      public CharInteger Data
      {
        get
        {
          return this._data;
        }
        set
        {
          this._data = value;
        }
      }
      public void Read(BinaryReader reader)
      {
        _data.Read(reader);
      }
      public void ReadChildData(BinaryReader reader)
      {
      }
    }
    public class StructureBspPathfindingEdgesBlock : IBlock
    {
      private CharInteger _midpoint = new CharInteger();
      public StructureBspPathfindingEdgesBlock()
      {
      }
      public CharInteger Midpoint
      {
        get
        {
          return this._midpoint;
        }
        set
        {
          this._midpoint = value;
        }
      }
      public void Read(BinaryReader reader)
      {
        _midpoint.Read(reader);
      }
      public void ReadChildData(BinaryReader reader)
      {
      }
    }
    public class StructureBspBackgroundSoundPaletteBlock : IBlock
    {
      private FixedLengthString _name = new FixedLengthString();
      private TagReference _backgroundSound = new TagReference();
      private Pad _unnamed;
      private FixedLengthString _scaleFunction = new FixedLengthString();
      private Pad _unnamed2;
      public StructureBspBackgroundSoundPaletteBlock()
      {
        this._unnamed = new Pad(4);
        this._unnamed2 = new Pad(32);
      }
      public FixedLengthString Name
      {
        get
        {
          return this._name;
        }
        set
        {
          this._name = value;
        }
      }
      public TagReference BackgroundSound
      {
        get
        {
          return this._backgroundSound;
        }
        set
        {
          this._backgroundSound = value;
        }
      }
      public FixedLengthString ScaleFunction
      {
        get
        {
          return this._scaleFunction;
        }
        set
        {
          this._scaleFunction = value;
        }
      }
      public void Read(BinaryReader reader)
      {
        _name.Read(reader);
        _backgroundSound.Read(reader);
        _unnamed.Read(reader);
        _scaleFunction.Read(reader);
        _unnamed2.Read(reader);
      }
      public void ReadChildData(BinaryReader reader)
      {
        _backgroundSound.ReadString(reader);
      }
    }
    public class StructureBspSoundEnvironmentPaletteBlock : IBlock
    {
      private FixedLengthString _name = new FixedLengthString();
      private TagReference _soundEnvironment = new TagReference();
      private Pad _unnamed;
      public StructureBspSoundEnvironmentPaletteBlock()
      {
        this._unnamed = new Pad(32);
      }
      public FixedLengthString Name
      {
        get
        {
          return this._name;
        }
        set
        {
          this._name = value;
        }
      }
      public TagReference SoundEnvironment
      {
        get
        {
          return this._soundEnvironment;
        }
        set
        {
          this._soundEnvironment = value;
        }
      }
      public void Read(BinaryReader reader)
      {
        _name.Read(reader);
        _soundEnvironment.Read(reader);
        _unnamed.Read(reader);
      }
      public void ReadChildData(BinaryReader reader)
      {
        _soundEnvironment.ReadString(reader);
      }
    }
    public class StructureBspMarkerBlock : IBlock
    {
      private FixedLengthString _name = new FixedLengthString();
      private RealQuaternion _rotation = new RealQuaternion();
      private RealPoint3D _position = new RealPoint3D();
      public StructureBspMarkerBlock()
      {
      }
      public FixedLengthString Name
      {
        get
        {
          return this._name;
        }
        set
        {
          this._name = value;
        }
      }
      public RealQuaternion Rotation
      {
        get
        {
          return this._rotation;
        }
        set
        {
          this._rotation = value;
        }
      }
      public RealPoint3D Position
      {
        get
        {
          return this._position;
        }
        set
        {
          this._position = value;
        }
      }
      public void Read(BinaryReader reader)
      {
        _name.Read(reader);
        _rotation.Read(reader);
        _position.Read(reader);
      }
      public void ReadChildData(BinaryReader reader)
      {
      }
    }
    public class StructureBspDetailObjectDataBlock : IBlock
    {
      private Block _cells = new Block();
      private Block _instances = new Block();
      private Block _counts = new Block();
      private Block _zReferenceVectors = new Block();
      private Pad _unnamed;
      private GlobalDetailObjectCellsBlockCollection _cellsCollection;
      private GlobalDetailObjectBlockCollection _instancesCollection;
      private GlobalDetailObjectCountsBlockCollection _countsCollection;
      private GlobalZReferenceVectorBlockCollection _zReferenceVectorsCollection;
      public StructureBspDetailObjectDataBlock()
      {
        this._cellsCollection = new GlobalDetailObjectCellsBlockCollection(this._cells);
        this._instancesCollection = new GlobalDetailObjectBlockCollection(this._instances);
        this._countsCollection = new GlobalDetailObjectCountsBlockCollection(this._counts);
        this._zReferenceVectorsCollection = new GlobalZReferenceVectorBlockCollection(this._zReferenceVectors);
        this._unnamed = new Pad(16);
      }
      public GlobalDetailObjectCellsBlockCollection Cells
      {
        get
        {
          return this._cellsCollection;
        }
      }
      public GlobalDetailObjectBlockCollection Instances
      {
        get
        {
          return this._instancesCollection;
        }
      }
      public GlobalDetailObjectCountsBlockCollection Counts
      {
        get
        {
          return this._countsCollection;
        }
      }
      public GlobalZReferenceVectorBlockCollection ZReferenceVectors
      {
        get
        {
          return this._zReferenceVectorsCollection;
        }
      }
      public void Read(BinaryReader reader)
      {
        _cells.Read(reader);
        _instances.Read(reader);
        _counts.Read(reader);
        _zReferenceVectors.Read(reader);
        _unnamed.Read(reader);
      }
      public void ReadChildData(BinaryReader reader)
      {
        int x = 0;
        for (x = 0; (x < _cells.Count); x = (x + 1))
        {
          Cells.AddNew();
          Cells[x].Read(reader);
        }
        for (x = 0; (x < _cells.Count); x = (x + 1))
        {
          Cells[x].ReadChildData(reader);
        }
        for (x = 0; (x < _instances.Count); x = (x + 1))
        {
          Instances.AddNew();
          Instances[x].Read(reader);
        }
        for (x = 0; (x < _instances.Count); x = (x + 1))
        {
          Instances[x].ReadChildData(reader);
        }
        for (x = 0; (x < _counts.Count); x = (x + 1))
        {
          Counts.AddNew();
          Counts[x].Read(reader);
        }
        for (x = 0; (x < _counts.Count); x = (x + 1))
        {
          Counts[x].ReadChildData(reader);
        }
        for (x = 0; (x < _zReferenceVectors.Count); x = (x + 1))
        {
          ZReferenceVectors.AddNew();
          ZReferenceVectors[x].Read(reader);
        }
        for (x = 0; (x < _zReferenceVectors.Count); x = (x + 1))
        {
          ZReferenceVectors[x].ReadChildData(reader);
        }
      }
      public class GlobalDetailObjectCellsBlockCollection : System.Collections.CollectionBase
      {
        private Block linkedBlock;
        public GlobalDetailObjectCellsBlockCollection(Block linkedBlock)
        {
          this.linkedBlock = linkedBlock;
        }
        public GlobalDetailObjectCellsBlock this[int index]
        {
          get
          {
            return ((GlobalDetailObjectCellsBlock)(this.InnerList[index]));
          }
        }
        public void Add(GlobalDetailObjectCellsBlock block)
        {
          InnerList.Add(block);
          if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
        }
        public void AddNew()
        {
          this.Add(new GlobalDetailObjectCellsBlock());
        }
        public void Remove(int index)
        {
          InnerList.RemoveAt(index);
          if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
        }
      }
      public class GlobalDetailObjectBlockCollection : System.Collections.CollectionBase
      {
        private Block linkedBlock;
        public GlobalDetailObjectBlockCollection(Block linkedBlock)
        {
          this.linkedBlock = linkedBlock;
        }
        public GlobalDetailObjectBlock this[int index]
        {
          get
          {
            return ((GlobalDetailObjectBlock)(this.InnerList[index]));
          }
        }
        public void Add(GlobalDetailObjectBlock block)
        {
          InnerList.Add(block);
          if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
        }
        public void AddNew()
        {
          this.Add(new GlobalDetailObjectBlock());
        }
        public void Remove(int index)
        {
          InnerList.RemoveAt(index);
          if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
        }
      }
      public class GlobalDetailObjectCountsBlockCollection : System.Collections.CollectionBase
      {
        private Block linkedBlock;
        public GlobalDetailObjectCountsBlockCollection(Block linkedBlock)
        {
          this.linkedBlock = linkedBlock;
        }
        public GlobalDetailObjectCountsBlock this[int index]
        {
          get
          {
            return ((GlobalDetailObjectCountsBlock)(this.InnerList[index]));
          }
        }
        public void Add(GlobalDetailObjectCountsBlock block)
        {
          InnerList.Add(block);
          if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
        }
        public void AddNew()
        {
          this.Add(new GlobalDetailObjectCountsBlock());
        }
        public void Remove(int index)
        {
          InnerList.RemoveAt(index);
          if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
        }
      }
      public class GlobalZReferenceVectorBlockCollection : System.Collections.CollectionBase
      {
        private Block linkedBlock;
        public GlobalZReferenceVectorBlockCollection(Block linkedBlock)
        {
          this.linkedBlock = linkedBlock;
        }
        public GlobalZReferenceVectorBlock this[int index]
        {
          get
          {
            return ((GlobalZReferenceVectorBlock)(this.InnerList[index]));
          }
        }
        public void Add(GlobalZReferenceVectorBlock block)
        {
          InnerList.Add(block);
          if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
        }
        public void AddNew()
        {
          this.Add(new GlobalZReferenceVectorBlock());
        }
        public void Remove(int index)
        {
          InnerList.RemoveAt(index);
          if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
        }
      }
    }
    public class GlobalDetailObjectCellsBlock : IBlock
    {
      private ShortInteger _unnamed = new ShortInteger();
      private ShortInteger _unnamed2 = new ShortInteger();
      private ShortInteger _unnamed3 = new ShortInteger();
      private ShortInteger _unnamed4 = new ShortInteger();
      private LongInteger _unnamed5 = new LongInteger();
      private LongInteger _unnamed6 = new LongInteger();
      private LongInteger _unnamed7 = new LongInteger();
      private Pad _unnamed8;
      public GlobalDetailObjectCellsBlock()
      {
        this._unnamed8 = new Pad(12);
      }
      public ShortInteger unnamed
      {
        get
        {
          return this._unnamed;
        }
        set
        {
          this._unnamed = value;
        }
      }
      public ShortInteger unnamed2
      {
        get
        {
          return this._unnamed2;
        }
        set
        {
          this._unnamed2 = value;
        }
      }
      public ShortInteger unnamed3
      {
        get
        {
          return this._unnamed3;
        }
        set
        {
          this._unnamed3 = value;
        }
      }
      public ShortInteger unnamed4
      {
        get
        {
          return this._unnamed4;
        }
        set
        {
          this._unnamed4 = value;
        }
      }
      public LongInteger unnamed5
      {
        get
        {
          return this._unnamed5;
        }
        set
        {
          this._unnamed5 = value;
        }
      }
      public LongInteger unnamed6
      {
        get
        {
          return this._unnamed6;
        }
        set
        {
          this._unnamed6 = value;
        }
      }
      public LongInteger unnamed7
      {
        get
        {
          return this._unnamed7;
        }
        set
        {
          this._unnamed7 = value;
        }
      }
      public void Read(BinaryReader reader)
      {
        _unnamed.Read(reader);
        _unnamed2.Read(reader);
        _unnamed3.Read(reader);
        _unnamed4.Read(reader);
        _unnamed5.Read(reader);
        _unnamed6.Read(reader);
        _unnamed7.Read(reader);
        _unnamed8.Read(reader);
      }
      public void ReadChildData(BinaryReader reader)
      {
      }
    }
    public class GlobalDetailObjectBlock : IBlock
    {
      private CharInteger _unnamed = new CharInteger();
      private CharInteger _unnamed2 = new CharInteger();
      private CharInteger _unnamed3 = new CharInteger();
      private CharInteger _unnamed4 = new CharInteger();
      private ShortInteger _unnamed5 = new ShortInteger();
      public GlobalDetailObjectBlock()
      {
      }
      public CharInteger unnamed
      {
        get
        {
          return this._unnamed;
        }
        set
        {
          this._unnamed = value;
        }
      }
      public CharInteger unnamed2
      {
        get
        {
          return this._unnamed2;
        }
        set
        {
          this._unnamed2 = value;
        }
      }
      public CharInteger unnamed3
      {
        get
        {
          return this._unnamed3;
        }
        set
        {
          this._unnamed3 = value;
        }
      }
      public CharInteger unnamed4
      {
        get
        {
          return this._unnamed4;
        }
        set
        {
          this._unnamed4 = value;
        }
      }
      public ShortInteger unnamed5
      {
        get
        {
          return this._unnamed5;
        }
        set
        {
          this._unnamed5 = value;
        }
      }
      public void Read(BinaryReader reader)
      {
        _unnamed.Read(reader);
        _unnamed2.Read(reader);
        _unnamed3.Read(reader);
        _unnamed4.Read(reader);
        _unnamed5.Read(reader);
      }
      public void ReadChildData(BinaryReader reader)
      {
      }
    }
    public class GlobalDetailObjectCountsBlock : IBlock
    {
      private ShortInteger _unnamed = new ShortInteger();
      public GlobalDetailObjectCountsBlock()
      {
      }
      public ShortInteger unnamed
      {
        get
        {
          return this._unnamed;
        }
        set
        {
          this._unnamed = value;
        }
      }
      public void Read(BinaryReader reader)
      {
        _unnamed.Read(reader);
      }
      public void ReadChildData(BinaryReader reader)
      {
      }
    }
    public class GlobalZReferenceVectorBlock : IBlock
    {
      private Real _unnamed = new Real();
      private Real _unnamed2 = new Real();
      private Real _unnamed3 = new Real();
      private Real _unnamed4 = new Real();
      public GlobalZReferenceVectorBlock()
      {
      }
      public Real unnamed
      {
        get
        {
          return this._unnamed;
        }
        set
        {
          this._unnamed = value;
        }
      }
      public Real unnamed2
      {
        get
        {
          return this._unnamed2;
        }
        set
        {
          this._unnamed2 = value;
        }
      }
      public Real unnamed3
      {
        get
        {
          return this._unnamed3;
        }
        set
        {
          this._unnamed3 = value;
        }
      }
      public Real unnamed4
      {
        get
        {
          return this._unnamed4;
        }
        set
        {
          this._unnamed4 = value;
        }
      }
      public void Read(BinaryReader reader)
      {
        _unnamed.Read(reader);
        _unnamed2.Read(reader);
        _unnamed3.Read(reader);
        _unnamed4.Read(reader);
      }
      public void ReadChildData(BinaryReader reader)
      {
      }
    }
    public class StructureBspRuntimeDecalBlock : IBlock
    {
      private Skip _unnamed;
      public StructureBspRuntimeDecalBlock()
      {
        this._unnamed = new Skip(16);
      }
      public void Read(BinaryReader reader)
      {
        _unnamed.Read(reader);
      }
      public void ReadChildData(BinaryReader reader)
      {
      }
    }
    public class GlobalMapLeafBlock : IBlock
    {
      private Block _faces = new Block();
      private Block _portalIndices = new Block();
      private MapLeafFaceBlockCollection _facesCollection;
      private MapLeafPortalIndexBlockCollection _portalIndicesCollection;
      public GlobalMapLeafBlock()
      {
        this._facesCollection = new MapLeafFaceBlockCollection(this._faces);
        this._portalIndicesCollection = new MapLeafPortalIndexBlockCollection(this._portalIndices);
      }
      public MapLeafFaceBlockCollection Faces
      {
        get
        {
          return this._facesCollection;
        }
      }
      public MapLeafPortalIndexBlockCollection PortalIndices
      {
        get
        {
          return this._portalIndicesCollection;
        }
      }
      public void Read(BinaryReader reader)
      {
        _faces.Read(reader);
        _portalIndices.Read(reader);
      }
      public void ReadChildData(BinaryReader reader)
      {
        int x = 0;
        for (x = 0; (x < _faces.Count); x = (x + 1))
        {
          Faces.AddNew();
          Faces[x].Read(reader);
        }
        for (x = 0; (x < _faces.Count); x = (x + 1))
        {
          Faces[x].ReadChildData(reader);
        }
        for (x = 0; (x < _portalIndices.Count); x = (x + 1))
        {
          PortalIndices.AddNew();
          PortalIndices[x].Read(reader);
        }
        for (x = 0; (x < _portalIndices.Count); x = (x + 1))
        {
          PortalIndices[x].ReadChildData(reader);
        }
      }
      public class MapLeafFaceBlockCollection : System.Collections.CollectionBase
      {
        private Block linkedBlock;
        public MapLeafFaceBlockCollection(Block linkedBlock)
        {
          this.linkedBlock = linkedBlock;
        }
        public MapLeafFaceBlock this[int index]
        {
          get
          {
            return ((MapLeafFaceBlock)(this.InnerList[index]));
          }
        }
        public void Add(MapLeafFaceBlock block)
        {
          InnerList.Add(block);
          if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
        }
        public void AddNew()
        {
          this.Add(new MapLeafFaceBlock());
        }
        public void Remove(int index)
        {
          InnerList.RemoveAt(index);
          if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
        }
      }
      public class MapLeafPortalIndexBlockCollection : System.Collections.CollectionBase
      {
        private Block linkedBlock;
        public MapLeafPortalIndexBlockCollection(Block linkedBlock)
        {
          this.linkedBlock = linkedBlock;
        }
        public MapLeafPortalIndexBlock this[int index]
        {
          get
          {
            return ((MapLeafPortalIndexBlock)(this.InnerList[index]));
          }
        }
        public void Add(MapLeafPortalIndexBlock block)
        {
          InnerList.Add(block);
          if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
        }
        public void AddNew()
        {
          this.Add(new MapLeafPortalIndexBlock());
        }
        public void Remove(int index)
        {
          InnerList.RemoveAt(index);
          if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
        }
      }
    }
    public class MapLeafFaceBlock : IBlock
    {
      private LongInteger _nodeIndex = new LongInteger();
      private Block _vertices = new Block();
      private MapLeafFaceVertexBlockCollection _verticesCollection;
      public MapLeafFaceBlock()
      {
        this._verticesCollection = new MapLeafFaceVertexBlockCollection(this._vertices);
      }
      public MapLeafFaceVertexBlockCollection Vertices
      {
        get
        {
          return this._verticesCollection;
        }
      }
      public LongInteger NodeIndex
      {
        get
        {
          return this._nodeIndex;
        }
        set
        {
          this._nodeIndex = value;
        }
      }
      public void Read(BinaryReader reader)
      {
        _nodeIndex.Read(reader);
        _vertices.Read(reader);
      }
      public void ReadChildData(BinaryReader reader)
      {
        int x = 0;
        for (x = 0; (x < _vertices.Count); x = (x + 1))
        {
          Vertices.AddNew();
          Vertices[x].Read(reader);
        }
        for (x = 0; (x < _vertices.Count); x = (x + 1))
        {
          Vertices[x].ReadChildData(reader);
        }
      }
      public class MapLeafFaceVertexBlockCollection : System.Collections.CollectionBase
      {
        private Block linkedBlock;
        public MapLeafFaceVertexBlockCollection(Block linkedBlock)
        {
          this.linkedBlock = linkedBlock;
        }
        public MapLeafFaceVertexBlock this[int index]
        {
          get
          {
            return ((MapLeafFaceVertexBlock)(this.InnerList[index]));
          }
        }
        public void Add(MapLeafFaceVertexBlock block)
        {
          InnerList.Add(block);
          if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
        }
        public void AddNew()
        {
          this.Add(new MapLeafFaceVertexBlock());
        }
        public void Remove(int index)
        {
          InnerList.RemoveAt(index);
          if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
        }
      }
    }
    public class MapLeafFaceVertexBlock : IBlock
    {
      private RealPoint2D _vertex = new RealPoint2D();
      public MapLeafFaceVertexBlock()
      {
      }
      public RealPoint2D Vertex
      {
        get
        {
          return this._vertex;
        }
        set
        {
          this._vertex = value;
        }
      }
      public void Read(BinaryReader reader)
      {
        _vertex.Read(reader);
      }
      public void ReadChildData(BinaryReader reader)
      {
      }
    }
    public class MapLeafPortalIndexBlock : IBlock
    {
      private LongInteger _portalIndex = new LongInteger();
      public MapLeafPortalIndexBlock()
      {
      }
      public LongInteger PortalIndex
      {
        get
        {
          return this._portalIndex;
        }
        set
        {
          this._portalIndex = value;
        }
      }
      public void Read(BinaryReader reader)
      {
        _portalIndex.Read(reader);
      }
      public void ReadChildData(BinaryReader reader)
      {
      }
    }
    public class GlobalLeafPortalBlock : IBlock
    {
      private LongInteger _planeIndex = new LongInteger();
      private LongInteger _backLeafIndex = new LongInteger();
      private LongInteger _frontLeafIndex = new LongInteger();
      private Block _vertices = new Block();
      private LeafPortalVertexBlockCollection _verticesCollection;
      public GlobalLeafPortalBlock()
      {
        this._verticesCollection = new LeafPortalVertexBlockCollection(this._vertices);
      }
      public LeafPortalVertexBlockCollection Vertices
      {
        get
        {
          return this._verticesCollection;
        }
      }
      public LongInteger PlaneIndex
      {
        get
        {
          return this._planeIndex;
        }
        set
        {
          this._planeIndex = value;
        }
      }
      public LongInteger BackLeafIndex
      {
        get
        {
          return this._backLeafIndex;
        }
        set
        {
          this._backLeafIndex = value;
        }
      }
      public LongInteger FrontLeafIndex
      {
        get
        {
          return this._frontLeafIndex;
        }
        set
        {
          this._frontLeafIndex = value;
        }
      }
      public void Read(BinaryReader reader)
      {
        _planeIndex.Read(reader);
        _backLeafIndex.Read(reader);
        _frontLeafIndex.Read(reader);
        _vertices.Read(reader);
      }
      public void ReadChildData(BinaryReader reader)
      {
        int x = 0;
        for (x = 0; (x < _vertices.Count); x = (x + 1))
        {
          Vertices.AddNew();
          Vertices[x].Read(reader);
        }
        for (x = 0; (x < _vertices.Count); x = (x + 1))
        {
          Vertices[x].ReadChildData(reader);
        }
      }
      public class LeafPortalVertexBlockCollection : System.Collections.CollectionBase
      {
        private Block linkedBlock;
        public LeafPortalVertexBlockCollection(Block linkedBlock)
        {
          this.linkedBlock = linkedBlock;
        }
        public LeafPortalVertexBlock this[int index]
        {
          get
          {
            return ((LeafPortalVertexBlock)(this.InnerList[index]));
          }
        }
        public void Add(LeafPortalVertexBlock block)
        {
          InnerList.Add(block);
          if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
        }
        public void AddNew()
        {
          this.Add(new LeafPortalVertexBlock());
        }
        public void Remove(int index)
        {
          InnerList.RemoveAt(index);
          if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
        }
      }
    }
    public class LeafPortalVertexBlock : IBlock
    {
      private RealPoint3D _point = new RealPoint3D();
      public LeafPortalVertexBlock()
      {
      }
      public RealPoint3D Point
      {
        get
        {
          return this._point;
        }
        set
        {
          this._point = value;
        }
      }
      public void Read(BinaryReader reader)
      {
        _point.Read(reader);
      }
      public void ReadChildData(BinaryReader reader)
      {
      }
    }
  }
}
