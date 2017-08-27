using System.IO;
using TagLibrary.Types;

namespace Core.TagSystem.TagDefinitions
{
  public class Shader
  {
    public MainBlock ShaderValues = new MainBlock();
    public void Read(BinaryReader reader)
    {
      ShaderValues.Read(reader);
    }
    public void ReadChildData(BinaryReader reader)
    {
      ShaderValues.ReadChildData(reader);
    }
    // *****************************************************************************
    public class MainBlock : IBlock
      // *****************************************************************************
    {
      private Flags  _flags;	
      private Enum _detailLevel = new Enum();
      private Real _power = new Real();
      private RealRGBColor _colorOfEmittedLight = new RealRGBColor();
      private RealRGBColor _tintColor = new RealRGBColor();
      private Flags  _flags2;	
      private Enum _materialType = new Enum();
      private Pad  __unnamed;	
      private Pad  __unnamed2;	
      public Flags Flags
      {
        get { return _flags; }
        set { _flags = value; }
      }
      public Enum DetailLevel
      {
        get { return _detailLevel; }
        set { _detailLevel = value; }
      }
      public Real Power
      {
        get { return _power; }
        set { _power = value; }
      }
      public RealRGBColor ColorOfEmittedLight
      {
        get { return _colorOfEmittedLight; }
        set { _colorOfEmittedLight = value; }
      }
      public RealRGBColor TintColor
      {
        get { return _tintColor; }
        set { _tintColor = value; }
      }
      public Flags Flags2
      {
        get { return _flags2; }
        set { _flags2 = value; }
      }
      public Enum MaterialType
      {
        get { return _materialType; }
        set { _materialType = value; }
      }
      public MainBlock()
      {
        _flags = new Flags(2);
        _flags2 = new Flags(2);
        __unnamed = new Pad(2);
        __unnamed2 = new Pad(2);

      }
      public void Read(BinaryReader reader)
      {
        _flags.Read(reader);
        _detailLevel.Read(reader);
        _power.Read(reader);
        _colorOfEmittedLight.Read(reader);
        _tintColor.Read(reader);
        _flags2.Read(reader);
        _materialType.Read(reader);
        __unnamed.Read(reader);
        __unnamed2.Read(reader);
      }
      public void ReadChildData(BinaryReader reader)
      {
      }
    }
  }
  public class ShaderTransparentChicagoExtended : Shader
  {
    public MainBlock ShaderTransparentChicagoExtendedValues = new MainBlock();
    public new void Read(BinaryReader reader)
    {
      base.Read(reader);
      ShaderTransparentChicagoExtendedValues.Read(reader);
    }
    public new void ReadChildData(BinaryReader reader)
    {
      base.ReadChildData(reader);
      ShaderTransparentChicagoExtendedValues.ReadChildData(reader);
    }
    // *****************************************************************************
    public class MainBlock : IBlock
      // *****************************************************************************
    {
      private CharInteger _numericCounterLimit = new CharInteger();
      private Flags  _flags;	
      private Enum _firstMapType = new Enum();
      private Enum _framebufferBlendFunction = new Enum();
      private Enum _framebufferFadeMode = new Enum();
      private Enum _framebufferFadeSource = new Enum();
      private Pad  __unnamed;	
      private Real _lensFlareSpacing = new Real();
      private TagReference _lensFlare = new TagReference();
      private Block _extraLayers = new Block();
      private Block _4StageMaps = new Block();
      private Block _2StageMaps = new Block();
      private Flags  _extraFlags;	
      private Pad  __unnamed2;	
      public class ShaderTransparentLayerBlockCollection : System.Collections.CollectionBase
      {
        private Block linkedBlock;
        public ShaderTransparentLayerBlockCollection(Block linkedBlock)
        {
          this.linkedBlock = linkedBlock;
        }
        public void Add(ShaderTransparentLayerBlock block)
        {
          InnerList.Add(block);
          if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
        }
        public void AddNew()
        {
          Add(new ShaderTransparentLayerBlock());
        }
        public void Remove(int index)
        {
          InnerList.RemoveAt(index);
          if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
        }
        public ShaderTransparentLayerBlock this[int index]
        {
          get { return (InnerList[index] as ShaderTransparentLayerBlock); }
        }
      }
      private ShaderTransparentLayerBlockCollection _extraLayersCollection;
      public ShaderTransparentLayerBlockCollection ExtraLayers
      {
        get { return _extraLayersCollection; }
      }
      private ShaderTransparentChicagoMapBlockCollection _fourStageMapsCollection;
      public ShaderTransparentChicagoMapBlockCollection FourStageMaps
      {
        get { return _fourStageMapsCollection; }
      }
      public class ShaderTransparentChicagoMapBlockCollection : System.Collections.CollectionBase
      {
        private Block linkedBlock;
        public ShaderTransparentChicagoMapBlockCollection(Block linkedBlock)
        {
          this.linkedBlock = linkedBlock;
        }
        public void Add(ShaderTransparentChicagoMapBlock block)
        {
          InnerList.Add(block);
          if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
        }
        public void AddNew()
        {
          Add(new ShaderTransparentChicagoMapBlock());
        }
        public void Remove(int index)
        {
          InnerList.RemoveAt(index);
          if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
        }
        public ShaderTransparentChicagoMapBlock this[int index]
        {
          get { return (InnerList[index] as ShaderTransparentChicagoMapBlock); }
        }
      }
      private ShaderTransparentChicagoMapBlockCollection _twoStageMapsCollection;
      public ShaderTransparentChicagoMapBlockCollection TwoStageMaps
      {
        get { return _twoStageMapsCollection; }
      }
      public CharInteger NumericCounterLimit
      {
        get { return _numericCounterLimit; }
        set { _numericCounterLimit = value; }
      }
      public Flags Flags
      {
        get { return _flags; }
        set { _flags = value; }
      }
      public Enum FirstMapType
      {
        get { return _firstMapType; }
        set { _firstMapType = value; }
      }
      public Enum FramebufferBlendFunction
      {
        get { return _framebufferBlendFunction; }
        set { _framebufferBlendFunction = value; }
      }
      public Enum FramebufferFadeMode
      {
        get { return _framebufferFadeMode; }
        set { _framebufferFadeMode = value; }
      }
      public Enum FramebufferFadeSource
      {
        get { return _framebufferFadeSource; }
        set { _framebufferFadeSource = value; }
      }
      public Real LensFlareSpacing
      {
        get { return _lensFlareSpacing; }
        set { _lensFlareSpacing = value; }
      }
      public TagReference LensFlare
      {
        get { return _lensFlare; }
        set { _lensFlare = value; }
      }
      public Flags ExtraFlags
      {
        get { return _extraFlags; }
        set { _extraFlags = value; }
      }
      public MainBlock()
      {
        _flags = new Flags(1);
        __unnamed = new Pad(2);
        _extraFlags = new Flags(4);
        __unnamed2 = new Pad(8);
        _extraLayersCollection = new ShaderTransparentLayerBlockCollection(_extraLayers);
        _fourStageMapsCollection = new ShaderTransparentChicagoMapBlockCollection(_4StageMaps);
        _twoStageMapsCollection = new ShaderTransparentChicagoMapBlockCollection(_2StageMaps);

      }
      public void Read(BinaryReader reader)
      {
        _numericCounterLimit.Read(reader);
        _flags.Read(reader);
        _firstMapType.Read(reader);
        _framebufferBlendFunction.Read(reader);
        _framebufferFadeMode.Read(reader);
        _framebufferFadeSource.Read(reader);
        __unnamed.Read(reader);
        _lensFlareSpacing.Read(reader);
        _lensFlare.Read(reader);
        _extraLayers.Read(reader);
        _4StageMaps.Read(reader);
        _2StageMaps.Read(reader);
        _extraFlags.Read(reader);
        __unnamed2.Read(reader);
      }
      public void ReadChildData(BinaryReader reader)
      {
        _lensFlare.ReadString(reader);
        for (int x=0; x<_extraLayers.Count; x++)
        {
          ExtraLayers.AddNew();
          ExtraLayers[x].Read(reader);
        }
        for (int x=0; x<_extraLayers.Count; x++)
          ExtraLayers[x].ReadChildData(reader);
        for (int x=0; x<_4StageMaps.Count; x++)
        {
          FourStageMaps.AddNew();
          FourStageMaps[x].Read(reader);
        }
        for (int x=0; x<_4StageMaps.Count; x++)
          FourStageMaps[x].ReadChildData(reader);
        for (int x=0; x<_2StageMaps.Count; x++)
        {
          TwoStageMaps.AddNew();
          TwoStageMaps[x].Read(reader);
        }
        for (int x=0; x<_2StageMaps.Count; x++)
          TwoStageMaps[x].ReadChildData(reader);
      }
    }
    public class ShaderTransparentLayerBlock : IBlock
    {
      private TagReference _shader = new TagReference();
      public TagReference Shader
      {
        get { return _shader; }
        set { _shader = value; }
      }
      public ShaderTransparentLayerBlock()
      {

      }
      public void Read(BinaryReader reader)
      {
        _shader.Read(reader);
      }
      public void ReadChildData(BinaryReader reader)
      {
        _shader.ReadString(reader);
      }
    }
    public class ShaderTransparentChicagoMapBlock : IBlock
    {
      private Flags  _flags;	
      private Pad  __unnamed;	
      private Pad  __unnamed2;	
      private Enum _colorFunction = new Enum();
      private Enum _alphaFunction = new Enum();
      private Pad  __unnamed3;	
      private Real _map = new Real();
      private Real _map2 = new Real();
      private Real _map3 = new Real();
      private Real _map4 = new Real();
      private Real _mapRotation = new Real();
      private RealFraction _mipmapBias = new RealFraction();
      private TagReference _map5 = new TagReference();
      private Pad  __unnamed4;	
      private Enum __unnamed5 = new Enum();
      private Enum __unnamed6 = new Enum();
      private Real __unnamed7 = new Real();
      private Real __unnamed8 = new Real();
      private Real __unnamed9 = new Real();
      private Enum __unnamed10 = new Enum();
      private Enum __unnamed11 = new Enum();
      private Real __unnamed12 = new Real();
      private Real __unnamed13 = new Real();
      private Real __unnamed14 = new Real();
      private Enum _rotatio = new Enum();
      private Enum _rotatio2 = new Enum();
      private Real _rotatio3 = new Real();
      private Real _rotatio4 = new Real();
      private Real _rotatio5 = new Real();
      private RealPoint2D _rotatio6 = new RealPoint2D();
      public Flags Flags
      {
        get { return _flags; }
        set { _flags = value; }
      }
      public Enum ColorFunction
      {
        get { return _colorFunction; }
        set { _colorFunction = value; }
      }
      public Enum AlphaFunction
      {
        get { return _alphaFunction; }
        set { _alphaFunction = value; }
      }
      public Real Map
      {
        get { return _map; }
        set { _map = value; }
      }
      public Real Map2
      {
        get { return _map2; }
        set { _map2 = value; }
      }
      public Real Map3
      {
        get { return _map3; }
        set { _map3 = value; }
      }
      public Real Map4
      {
        get { return _map4; }
        set { _map4 = value; }
      }
      public Real MapRotation
      {
        get { return _mapRotation; }
        set { _mapRotation = value; }
      }
      public RealFraction MipmapBias
      {
        get { return _mipmapBias; }
        set { _mipmapBias = value; }
      }
      public TagReference Map5
      {
        get { return _map5; }
        set { _map5 = value; }
      }
      public Enum _unnamed5
      {
        get { return __unnamed5; }
        set { __unnamed5 = value; }
      }
      public Enum _unnamed6
      {
        get { return __unnamed6; }
        set { __unnamed6 = value; }
      }
      public Real _unnamed7
      {
        get { return __unnamed7; }
        set { __unnamed7 = value; }
      }
      public Real _unnamed8
      {
        get { return __unnamed8; }
        set { __unnamed8 = value; }
      }
      public Real _unnamed9
      {
        get { return __unnamed9; }
        set { __unnamed9 = value; }
      }
      public Enum _unnamed10
      {
        get { return __unnamed10; }
        set { __unnamed10 = value; }
      }
      public Enum _unnamed11
      {
        get { return __unnamed11; }
        set { __unnamed11 = value; }
      }
      public Real _unnamed12
      {
        get { return __unnamed12; }
        set { __unnamed12 = value; }
      }
      public Real _unnamed13
      {
        get { return __unnamed13; }
        set { __unnamed13 = value; }
      }
      public Real _unnamed14
      {
        get { return __unnamed14; }
        set { __unnamed14 = value; }
      }
      public Enum Rotatio
      {
        get { return _rotatio; }
        set { _rotatio = value; }
      }
      public Enum Rotatio2
      {
        get { return _rotatio2; }
        set { _rotatio2 = value; }
      }
      public Real Rotatio3
      {
        get { return _rotatio3; }
        set { _rotatio3 = value; }
      }
      public Real Rotatio4
      {
        get { return _rotatio4; }
        set { _rotatio4 = value; }
      }
      public Real Rotatio5
      {
        get { return _rotatio5; }
        set { _rotatio5 = value; }
      }
      public RealPoint2D Rotatio6
      {
        get { return _rotatio6; }
        set { _rotatio6 = value; }
      }
      public ShaderTransparentChicagoMapBlock()
      {
        _flags = new Flags(2);
        __unnamed = new Pad(2);
        __unnamed2 = new Pad(40);
        __unnamed3 = new Pad(36);
        __unnamed4 = new Pad(40);

      }
      public void Read(BinaryReader reader)
      {
        _flags.Read(reader);
        __unnamed.Read(reader);
        __unnamed2.Read(reader);
        _colorFunction.Read(reader);
        _alphaFunction.Read(reader);
        __unnamed3.Read(reader);
        _map.Read(reader);
        _map2.Read(reader);
        _map3.Read(reader);
        _map4.Read(reader);
        _mapRotation.Read(reader);
        _mipmapBias.Read(reader);
        _map5.Read(reader);
        __unnamed4.Read(reader);
        __unnamed5.Read(reader);
        __unnamed6.Read(reader);
        __unnamed7.Read(reader);
        __unnamed8.Read(reader);
        __unnamed9.Read(reader);
        __unnamed10.Read(reader);
        __unnamed11.Read(reader);
        __unnamed12.Read(reader);
        __unnamed13.Read(reader);
        __unnamed14.Read(reader);
        _rotatio.Read(reader);
        _rotatio2.Read(reader);
        _rotatio3.Read(reader);
        _rotatio4.Read(reader);
        _rotatio5.Read(reader);
        _rotatio6.Read(reader);
      }
      public void ReadChildData(BinaryReader reader)
      {
        _map5.ReadString(reader);
      }
    }
  }
}