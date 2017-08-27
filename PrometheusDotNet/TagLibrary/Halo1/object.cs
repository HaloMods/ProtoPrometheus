using System.IO;
using System.Diagnostics;
using TagLibrary.Types;

namespace TagLibrary.Halo1
{
  public class Object : IBlock
  {
    public ObjectBlock ObjectValues = new ObjectBlock();
    public virtual void Read(BinaryReader reader)
    {
      Trace.WriteLine("Loading 'Object'------------------------------------------------------");
      ObjectValues.Read(reader);
    }
    public virtual void ReadChildData(BinaryReader reader)
    {
      ObjectValues.ReadChildData(reader);
    }
    public virtual void Write(BinaryWriter writer)
    {
      ObjectValues.Write(writer);
    }
    public virtual void WriteChildData(BinaryWriter writer)
    {
      ObjectValues.WriteChildData(writer);
    }
public class ObjectBlock : IBlock
{
private Pad  __unnamed;	
private Flags  _flags;	
private Real _boundingRadius = new Real();
private RealPoint3D _boundingOffset = new RealPoint3D();
private RealPoint3D _originOffset = new RealPoint3D();
private Real _accelerationScale = new Real();
private Pad  __unnamed2;	
private TagReference _model = new TagReference();
private TagReference _animationGraph = new TagReference();
private Pad  __unnamed3;	
private TagReference _collisionModel = new TagReference();
private TagReference _physics = new TagReference();
private TagReference _modifierShader = new TagReference();
private TagReference _creationEffect = new TagReference();
private Pad  __unnamed4;	
private Real _renderBoundingRadius = new Real();
private Enum _aIn = new Enum();
private Enum _bIn = new Enum();
private Enum _cIn = new Enum();
private Enum _dIn = new Enum();
private Pad  __unnamed5;	
private ShortInteger _hudTextMessageIndex = new ShortInteger();
private ShortInteger _forcedShaderPermuationIndex = new ShortInteger();
private Block _attachments = new Block();
private Block _widgets = new Block();
private Block _functions = new Block();
private Block _changeColors = new Block();
private Block _predictedResources = new Block();
public class ObjectAttachmentBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public ObjectAttachmentBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(ObjectAttachmentBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new ObjectAttachmentBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public ObjectAttachmentBlock this[int index]
  {
    get { return (InnerList[index] as ObjectAttachmentBlock); }
  }
}
private ObjectAttachmentBlockCollection _attachmentsCollection;
public ObjectAttachmentBlockCollection Attachments
{
  get { return _attachmentsCollection; }
}
public class ObjectWidgetBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public ObjectWidgetBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(ObjectWidgetBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new ObjectWidgetBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public ObjectWidgetBlock this[int index]
  {
    get { return (InnerList[index] as ObjectWidgetBlock); }
  }
}
private ObjectWidgetBlockCollection _widgetsCollection;
public ObjectWidgetBlockCollection Widgets
{
  get { return _widgetsCollection; }
}
public class ObjectFunctionBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public ObjectFunctionBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(ObjectFunctionBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new ObjectFunctionBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public ObjectFunctionBlock this[int index]
  {
    get { return (InnerList[index] as ObjectFunctionBlock); }
  }
}
private ObjectFunctionBlockCollection _functionsCollection;
public ObjectFunctionBlockCollection Functions
{
  get { return _functionsCollection; }
}
public class ObjectChangeColorsBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public ObjectChangeColorsBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(ObjectChangeColorsBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new ObjectChangeColorsBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public ObjectChangeColorsBlock this[int index]
  {
    get { return (InnerList[index] as ObjectChangeColorsBlock); }
  }
}
private ObjectChangeColorsBlockCollection _changeColorsCollection;
public ObjectChangeColorsBlockCollection ChangeColors
{
  get { return _changeColorsCollection; }
}
public class PredictedResourceBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public PredictedResourceBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(PredictedResourceBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new PredictedResourceBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public PredictedResourceBlock this[int index]
  {
    get { return (InnerList[index] as PredictedResourceBlock); }
  }
}
private PredictedResourceBlockCollection _predictedResourcesCollection;
public PredictedResourceBlockCollection PredictedResources
{
  get { return _predictedResourcesCollection; }
}
public Flags Flags
{
  get { return _flags; }
  set { _flags = value; }
}
public Real BoundingRadius
{
  get { return _boundingRadius; }
  set { _boundingRadius = value; }
}
public RealPoint3D BoundingOffset
{
  get { return _boundingOffset; }
  set { _boundingOffset = value; }
}
public RealPoint3D OriginOffset
{
  get { return _originOffset; }
  set { _originOffset = value; }
}
public Real AccelerationScale
{
  get { return _accelerationScale; }
  set { _accelerationScale = value; }
}
public TagReference Model
{
  get { return _model; }
  set { _model = value; }
}
public TagReference AnimationGraph
{
  get { return _animationGraph; }
  set { _animationGraph = value; }
}
public TagReference CollisionModel
{
  get { return _collisionModel; }
  set { _collisionModel = value; }
}
public TagReference Physics
{
  get { return _physics; }
  set { _physics = value; }
}
public TagReference ModifierShader
{
  get { return _modifierShader; }
  set { _modifierShader = value; }
}
public TagReference CreationEffect
{
  get { return _creationEffect; }
  set { _creationEffect = value; }
}
public Real RenderBoundingRadius
{
  get { return _renderBoundingRadius; }
  set { _renderBoundingRadius = value; }
}
public Enum AIn
{
  get { return _aIn; }
  set { _aIn = value; }
}
public Enum BIn
{
  get { return _bIn; }
  set { _bIn = value; }
}
public Enum CIn
{
  get { return _cIn; }
  set { _cIn = value; }
}
public Enum DIn
{
  get { return _dIn; }
  set { _dIn = value; }
}
public ShortInteger HudTextMessageIndex
{
  get { return _hudTextMessageIndex; }
  set { _hudTextMessageIndex = value; }
}
public ShortInteger ForcedShaderPermuationIndex
{
  get { return _forcedShaderPermuationIndex; }
  set { _forcedShaderPermuationIndex = value; }
}
public ObjectBlock()
{
__unnamed = new Pad(2);
_flags = new Flags(2);
__unnamed2 = new Pad(4);
__unnamed3 = new Pad(40);
__unnamed4 = new Pad(84);
__unnamed5 = new Pad(44);
_attachmentsCollection = new ObjectAttachmentBlockCollection(_attachments);
_widgetsCollection = new ObjectWidgetBlockCollection(_widgets);
_functionsCollection = new ObjectFunctionBlockCollection(_functions);
_changeColorsCollection = new ObjectChangeColorsBlockCollection(_changeColors);
_predictedResourcesCollection = new PredictedResourceBlockCollection(_predictedResources);

}
public void Read(BinaryReader reader)
{
  __unnamed.Read(reader);
  _flags.Read(reader);
  _boundingRadius.Read(reader);
  _boundingOffset.Read(reader);
  _originOffset.Read(reader);
  _accelerationScale.Read(reader);
  __unnamed2.Read(reader);
  _model.Read(reader);
  _animationGraph.Read(reader);
  __unnamed3.Read(reader);
  _collisionModel.Read(reader);
  _physics.Read(reader);
  _modifierShader.Read(reader);
  _creationEffect.Read(reader);
  __unnamed4.Read(reader);
  _renderBoundingRadius.Read(reader);
  _aIn.Read(reader);
  _bIn.Read(reader);
  _cIn.Read(reader);
  _dIn.Read(reader);
  __unnamed5.Read(reader);
  _hudTextMessageIndex.Read(reader);
  _forcedShaderPermuationIndex.Read(reader);
  _attachments.Read(reader);
  _widgets.Read(reader);
  _functions.Read(reader);
  _changeColors.Read(reader);
  _predictedResources.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
_model.ReadString(reader);
_animationGraph.ReadString(reader);
_collisionModel.ReadString(reader);
_physics.ReadString(reader);
_modifierShader.ReadString(reader);
_creationEffect.ReadString(reader);
for (int x=0; x<_attachments.Count; x++)
{
  Attachments.AddNew();
  Attachments[x].Read(reader);
}
for (int x=0; x<_attachments.Count; x++)
  Attachments[x].ReadChildData(reader);
for (int x=0; x<_widgets.Count; x++)
{
  Widgets.AddNew();
  Widgets[x].Read(reader);
}
for (int x=0; x<_widgets.Count; x++)
  Widgets[x].ReadChildData(reader);
for (int x=0; x<_functions.Count; x++)
{
  Functions.AddNew();
  Functions[x].Read(reader);
}
for (int x=0; x<_functions.Count; x++)
  Functions[x].ReadChildData(reader);
for (int x=0; x<_changeColors.Count; x++)
{
  ChangeColors.AddNew();
  ChangeColors[x].Read(reader);
}
for (int x=0; x<_changeColors.Count; x++)
  ChangeColors[x].ReadChildData(reader);
for (int x=0; x<_predictedResources.Count; x++)
{
  PredictedResources.AddNew();
  PredictedResources[x].Read(reader);
}
for (int x=0; x<_predictedResources.Count; x++)
  PredictedResources[x].ReadChildData(reader);
}
public void Write(BinaryWriter writer)
{
    __unnamed.Write(writer);
    _flags.Write(writer);
    _boundingRadius.Write(writer);
    _boundingOffset.Write(writer);
    _originOffset.Write(writer);
    _accelerationScale.Write(writer);
    __unnamed2.Write(writer);
    _model.Write(writer);
    _animationGraph.Write(writer);
    __unnamed3.Write(writer);
    _collisionModel.Write(writer);
    _physics.Write(writer);
    _modifierShader.Write(writer);
    _creationEffect.Write(writer);
    __unnamed4.Write(writer);
    _renderBoundingRadius.Write(writer);
    _aIn.Write(writer);
    _bIn.Write(writer);
    _cIn.Write(writer);
    _dIn.Write(writer);
    __unnamed5.Write(writer);
    _hudTextMessageIndex.Write(writer);
    _forcedShaderPermuationIndex.Write(writer);
    _attachments.Write(writer);
    _widgets.Write(writer);
    _functions.Write(writer);
    _changeColors.Write(writer);
    _predictedResources.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_model.WriteString(writer);
_animationGraph.WriteString(writer);
_collisionModel.WriteString(writer);
_physics.WriteString(writer);
_modifierShader.WriteString(writer);
_creationEffect.WriteString(writer);
_attachments.UpdateReflexiveOffset(writer);
for (int x=0; x<_attachments.Count; x++)
{
  Attachments[x].Write(writer);
}
for (int x=0; x<_attachments.Count; x++)
  Attachments[x].WriteChildData(writer);
_widgets.UpdateReflexiveOffset(writer);
for (int x=0; x<_widgets.Count; x++)
{
  Widgets[x].Write(writer);
}
for (int x=0; x<_widgets.Count; x++)
  Widgets[x].WriteChildData(writer);
_functions.UpdateReflexiveOffset(writer);
for (int x=0; x<_functions.Count; x++)
{
  Functions[x].Write(writer);
}
for (int x=0; x<_functions.Count; x++)
  Functions[x].WriteChildData(writer);
_changeColors.UpdateReflexiveOffset(writer);
for (int x=0; x<_changeColors.Count; x++)
{
  ChangeColors[x].Write(writer);
}
for (int x=0; x<_changeColors.Count; x++)
  ChangeColors[x].WriteChildData(writer);
_predictedResources.UpdateReflexiveOffset(writer);
for (int x=0; x<_predictedResources.Count; x++)
{
  PredictedResources[x].Write(writer);
}
for (int x=0; x<_predictedResources.Count; x++)
  PredictedResources[x].WriteChildData(writer);
}
}
public class ObjectAttachmentBlock : IBlock
{
private TagReference _type = new TagReference();
private FixedLengthString _marker = new FixedLengthString();
private Enum _primaryScale = new Enum();
private Enum _secondaryScale = new Enum();
private Enum _changeColor = new Enum();
private Pad  __unnamed;	
private Pad  __unnamed2;	
public TagReference Type
{
  get { return _type; }
  set { _type = value; }
}
public FixedLengthString Marker
{
  get { return _marker; }
  set { _marker = value; }
}
public Enum PrimaryScale
{
  get { return _primaryScale; }
  set { _primaryScale = value; }
}
public Enum SecondaryScale
{
  get { return _secondaryScale; }
  set { _secondaryScale = value; }
}
public Enum ChangeColor
{
  get { return _changeColor; }
  set { _changeColor = value; }
}
public ObjectAttachmentBlock()
{
__unnamed = new Pad(2);
__unnamed2 = new Pad(16);

}
public void Read(BinaryReader reader)
{
  _type.Read(reader);
  _marker.Read(reader);
  _primaryScale.Read(reader);
  _secondaryScale.Read(reader);
  _changeColor.Read(reader);
  __unnamed.Read(reader);
  __unnamed2.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
_type.ReadString(reader);
}
public void Write(BinaryWriter writer)
{
    _type.Write(writer);
    _marker.Write(writer);
    _primaryScale.Write(writer);
    _secondaryScale.Write(writer);
    _changeColor.Write(writer);
    __unnamed.Write(writer);
    __unnamed2.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_type.WriteString(writer);
}
}
public class ObjectWidgetBlock : IBlock
{
private TagReference _reference = new TagReference();
private Pad  __unnamed;	
public TagReference Reference
{
  get { return _reference; }
  set { _reference = value; }
}
public ObjectWidgetBlock()
{
__unnamed = new Pad(16);

}
public void Read(BinaryReader reader)
{
  _reference.Read(reader);
  __unnamed.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
_reference.ReadString(reader);
}
public void Write(BinaryWriter writer)
{
    _reference.Write(writer);
    __unnamed.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_reference.WriteString(writer);
}
}
public class ObjectFunctionBlock : IBlock
{
private Flags  _flags;	
private Real _period = new Real();
private Enum _scalePeriodBy = new Enum();
private Enum _function = new Enum();
private Enum _scaleFunctionBy = new Enum();
private Enum _wobbleFunction = new Enum();
private Real _wobblePeriod = new Real();
private Real _wobbleMagnitude = new Real();
private RealFraction _squareWaveThreshold = new RealFraction();
private ShortInteger _stepCount = new ShortInteger();
private Enum _mapTo = new Enum();
private ShortInteger _sawtoothCount = new ShortInteger();
private Enum _add = new Enum();
private Enum _scaleResultBy = new Enum();
private Enum _boundsMode = new Enum();
private RealFractionBounds _bounds = new RealFractionBounds();
private Pad  __unnamed;	
private Pad  __unnamed2;	
private ShortBlockIndex _turnOffWith = new ShortBlockIndex();
private Real _scaleBy = new Real();
private Pad  __unnamed3;	
private Pad  __unnamed4;	
private FixedLengthString _usage = new FixedLengthString();
public Flags Flags
{
  get { return _flags; }
  set { _flags = value; }
}
public Real Period
{
  get { return _period; }
  set { _period = value; }
}
public Enum ScalePeriodBy
{
  get { return _scalePeriodBy; }
  set { _scalePeriodBy = value; }
}
public Enum Function
{
  get { return _function; }
  set { _function = value; }
}
public Enum ScaleFunctionBy
{
  get { return _scaleFunctionBy; }
  set { _scaleFunctionBy = value; }
}
public Enum WobbleFunction
{
  get { return _wobbleFunction; }
  set { _wobbleFunction = value; }
}
public Real WobblePeriod
{
  get { return _wobblePeriod; }
  set { _wobblePeriod = value; }
}
public Real WobbleMagnitude
{
  get { return _wobbleMagnitude; }
  set { _wobbleMagnitude = value; }
}
public RealFraction SquareWaveThreshold
{
  get { return _squareWaveThreshold; }
  set { _squareWaveThreshold = value; }
}
public ShortInteger StepCount
{
  get { return _stepCount; }
  set { _stepCount = value; }
}
public Enum MapTo
{
  get { return _mapTo; }
  set { _mapTo = value; }
}
public ShortInteger SawtoothCount
{
  get { return _sawtoothCount; }
  set { _sawtoothCount = value; }
}
public Enum Add
{
  get { return _add; }
  set { _add = value; }
}
public Enum ScaleResultBy
{
  get { return _scaleResultBy; }
  set { _scaleResultBy = value; }
}
public Enum BoundsMode
{
  get { return _boundsMode; }
  set { _boundsMode = value; }
}
public RealFractionBounds Bounds
{
  get { return _bounds; }
  set { _bounds = value; }
}
public ShortBlockIndex TurnOffWith
{
  get { return _turnOffWith; }
  set { _turnOffWith = value; }
}
public Real ScaleBy
{
  get { return _scaleBy; }
  set { _scaleBy = value; }
}
public FixedLengthString Usage
{
  get { return _usage; }
  set { _usage = value; }
}
public ObjectFunctionBlock()
{
_flags = new Flags(4);
__unnamed = new Pad(4);
__unnamed2 = new Pad(2);
__unnamed3 = new Pad(252);
__unnamed4 = new Pad(16);

}
public void Read(BinaryReader reader)
{
  _flags.Read(reader);
  _period.Read(reader);
  _scalePeriodBy.Read(reader);
  _function.Read(reader);
  _scaleFunctionBy.Read(reader);
  _wobbleFunction.Read(reader);
  _wobblePeriod.Read(reader);
  _wobbleMagnitude.Read(reader);
  _squareWaveThreshold.Read(reader);
  _stepCount.Read(reader);
  _mapTo.Read(reader);
  _sawtoothCount.Read(reader);
  _add.Read(reader);
  _scaleResultBy.Read(reader);
  _boundsMode.Read(reader);
  _bounds.Read(reader);
  __unnamed.Read(reader);
  __unnamed2.Read(reader);
  _turnOffWith.Read(reader);
  _scaleBy.Read(reader);
  __unnamed3.Read(reader);
  __unnamed4.Read(reader);
  _usage.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
}
public void Write(BinaryWriter writer)
{
    _flags.Write(writer);
    _period.Write(writer);
    _scalePeriodBy.Write(writer);
    _function.Write(writer);
    _scaleFunctionBy.Write(writer);
    _wobbleFunction.Write(writer);
    _wobblePeriod.Write(writer);
    _wobbleMagnitude.Write(writer);
    _squareWaveThreshold.Write(writer);
    _stepCount.Write(writer);
    _mapTo.Write(writer);
    _sawtoothCount.Write(writer);
    _add.Write(writer);
    _scaleResultBy.Write(writer);
    _boundsMode.Write(writer);
    _bounds.Write(writer);
    __unnamed.Write(writer);
    __unnamed2.Write(writer);
    _turnOffWith.Write(writer);
    _scaleBy.Write(writer);
    __unnamed3.Write(writer);
    __unnamed4.Write(writer);
    _usage.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
}
}
public class ObjectChangeColorsBlock : IBlock
{
private Enum _darkenBy = new Enum();
private Enum _scaleBy = new Enum();
private Flags  _scaleFlags;	
private RealRGBColor _colorLowerBound = new RealRGBColor();
private RealRGBColor _colorUpperBound = new RealRGBColor();
private Block _permutations = new Block();
public class ObjectChangeColorPermutationsBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public ObjectChangeColorPermutationsBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(ObjectChangeColorPermutationsBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new ObjectChangeColorPermutationsBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public ObjectChangeColorPermutationsBlock this[int index]
  {
    get { return (InnerList[index] as ObjectChangeColorPermutationsBlock); }
  }
}
private ObjectChangeColorPermutationsBlockCollection _permutationsCollection;
public ObjectChangeColorPermutationsBlockCollection Permutations
{
  get { return _permutationsCollection; }
}
public Enum DarkenBy
{
  get { return _darkenBy; }
  set { _darkenBy = value; }
}
public Enum ScaleBy
{
  get { return _scaleBy; }
  set { _scaleBy = value; }
}
public Flags ScaleFlags
{
  get { return _scaleFlags; }
  set { _scaleFlags = value; }
}
public RealRGBColor ColorLowerBound
{
  get { return _colorLowerBound; }
  set { _colorLowerBound = value; }
}
public RealRGBColor ColorUpperBound
{
  get { return _colorUpperBound; }
  set { _colorUpperBound = value; }
}
public ObjectChangeColorsBlock()
{
_scaleFlags = new Flags(4);
_permutationsCollection = new ObjectChangeColorPermutationsBlockCollection(_permutations);

}
public void Read(BinaryReader reader)
{
  _darkenBy.Read(reader);
  _scaleBy.Read(reader);
  _scaleFlags.Read(reader);
  _colorLowerBound.Read(reader);
  _colorUpperBound.Read(reader);
  _permutations.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
for (int x=0; x<_permutations.Count; x++)
{
  Permutations.AddNew();
  Permutations[x].Read(reader);
}
for (int x=0; x<_permutations.Count; x++)
  Permutations[x].ReadChildData(reader);
}
public void Write(BinaryWriter writer)
{
    _darkenBy.Write(writer);
    _scaleBy.Write(writer);
    _scaleFlags.Write(writer);
    _colorLowerBound.Write(writer);
    _colorUpperBound.Write(writer);
    _permutations.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_permutations.UpdateReflexiveOffset(writer);
for (int x=0; x<_permutations.Count; x++)
{
  Permutations[x].Write(writer);
}
for (int x=0; x<_permutations.Count; x++)
  Permutations[x].WriteChildData(writer);
}
}
public class ObjectChangeColorPermutationsBlock : IBlock
{
private Real _weight = new Real();
private RealRGBColor _colorLowerBound = new RealRGBColor();
private RealRGBColor _colorUpperBound = new RealRGBColor();
public Real Weight
{
  get { return _weight; }
  set { _weight = value; }
}
public RealRGBColor ColorLowerBound
{
  get { return _colorLowerBound; }
  set { _colorLowerBound = value; }
}
public RealRGBColor ColorUpperBound
{
  get { return _colorUpperBound; }
  set { _colorUpperBound = value; }
}
public ObjectChangeColorPermutationsBlock()
{

}
public void Read(BinaryReader reader)
{
  _weight.Read(reader);
  _colorLowerBound.Read(reader);
  _colorUpperBound.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
}
public void Write(BinaryWriter writer)
{
    _weight.Write(writer);
    _colorLowerBound.Write(writer);
    _colorUpperBound.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
}
}
public class PredictedResourceBlock : IBlock
{
private Enum _type = new Enum();
private ShortInteger _resourceIndex = new ShortInteger();
private LongInteger _tagIndex = new LongInteger();
public Enum Type
{
  get { return _type; }
  set { _type = value; }
}
public ShortInteger ResourceIndex
{
  get { return _resourceIndex; }
  set { _resourceIndex = value; }
}
public LongInteger TagIndex
{
  get { return _tagIndex; }
  set { _tagIndex = value; }
}
public PredictedResourceBlock()
{

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
public void Write(BinaryWriter writer)
{
    _type.Write(writer);
    _resourceIndex.Write(writer);
    _tagIndex.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
}
}
  }
}
