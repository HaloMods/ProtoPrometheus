using System.IO;
using System.Diagnostics;
using TagLibrary.Types;

namespace TagLibrary.Halo1
{
  public class Physics : IBlock
  {
    public PhysicsBlock PhysicsValues = new PhysicsBlock();
    public void Read(BinaryReader reader)
    {
      Trace.WriteLine("Loading 'Physics'------------------------------------------------------");
      PhysicsValues.Read(reader);
    }
    public void ReadChildData(BinaryReader reader)
    {
      PhysicsValues.ReadChildData(reader);
    }
    public void Write(BinaryWriter writer)
    {
      PhysicsValues.Write(writer);
    }
    public void WriteChildData(BinaryWriter writer)
    {
      PhysicsValues.WriteChildData(writer);
    }
public class PhysicsBlock : IBlock
{
private Real _radius = new Real();
private RealFraction _momentScale = new RealFraction();
private Real _mass = new Real();
private RealPoint3D _centerOfMass = new RealPoint3D();
private Real _density = new Real();
private Real _gravityScale = new Real();
private Real _groundFriction = new Real();
private Real _groundDepth = new Real();
private RealFraction _groundDampFraction = new RealFraction();
private Real _groundNormalK1 = new Real();
private Real _groundNormalK0 = new Real();
private Pad  __unnamed;	
private Real _waterFriction = new Real();
private Real _waterDepth = new Real();
private Real _waterDensity = new Real();
private Pad  __unnamed2;	
private RealFraction _airFriction = new RealFraction();
private Pad  __unnamed3;	
private Real _xxMoment = new Real();
private Real _yyMoment = new Real();
private Real _zzMoment = new Real();
private Block _inertialMatrixAndInverse = new Block();
private Block _poweredMassPoints = new Block();
private Block _massPoints = new Block();
public class InertialMatrixBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public InertialMatrixBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(InertialMatrixBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new InertialMatrixBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public InertialMatrixBlock this[int index]
  {
    get { return (InnerList[index] as InertialMatrixBlock); }
  }
}
private InertialMatrixBlockCollection _inertialMatrixAndInverseCollection;
public InertialMatrixBlockCollection InertialMatrixAndInverse
{
  get { return _inertialMatrixAndInverseCollection; }
}
public class PoweredMassPointBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public PoweredMassPointBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(PoweredMassPointBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new PoweredMassPointBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public PoweredMassPointBlock this[int index]
  {
    get { return (InnerList[index] as PoweredMassPointBlock); }
  }
}
private PoweredMassPointBlockCollection _poweredMassPointsCollection;
public PoweredMassPointBlockCollection PoweredMassPoints
{
  get { return _poweredMassPointsCollection; }
}
public class MassPointBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public MassPointBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(MassPointBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new MassPointBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public MassPointBlock this[int index]
  {
    get { return (InnerList[index] as MassPointBlock); }
  }
}
private MassPointBlockCollection _massPointsCollection;
public MassPointBlockCollection MassPoints
{
  get { return _massPointsCollection; }
}
public Real Radius
{
  get { return _radius; }
  set { _radius = value; }
}
public RealFraction MomentScale
{
  get { return _momentScale; }
  set { _momentScale = value; }
}
public Real Mass
{
  get { return _mass; }
  set { _mass = value; }
}
public RealPoint3D CenterOfMass
{
  get { return _centerOfMass; }
  set { _centerOfMass = value; }
}
public Real Density
{
  get { return _density; }
  set { _density = value; }
}
public Real GravityScale
{
  get { return _gravityScale; }
  set { _gravityScale = value; }
}
public Real GroundFriction
{
  get { return _groundFriction; }
  set { _groundFriction = value; }
}
public Real GroundDepth
{
  get { return _groundDepth; }
  set { _groundDepth = value; }
}
public RealFraction GroundDampFraction
{
  get { return _groundDampFraction; }
  set { _groundDampFraction = value; }
}
public Real GroundNormalK1
{
  get { return _groundNormalK1; }
  set { _groundNormalK1 = value; }
}
public Real GroundNormalK0
{
  get { return _groundNormalK0; }
  set { _groundNormalK0 = value; }
}
public Real WaterFriction
{
  get { return _waterFriction; }
  set { _waterFriction = value; }
}
public Real WaterDepth
{
  get { return _waterDepth; }
  set { _waterDepth = value; }
}
public Real WaterDensity
{
  get { return _waterDensity; }
  set { _waterDensity = value; }
}
public RealFraction AirFriction
{
  get { return _airFriction; }
  set { _airFriction = value; }
}
public Real XxMoment
{
  get { return _xxMoment; }
  set { _xxMoment = value; }
}
public Real YyMoment
{
  get { return _yyMoment; }
  set { _yyMoment = value; }
}
public Real ZzMoment
{
  get { return _zzMoment; }
  set { _zzMoment = value; }
}
public PhysicsBlock()
{
__unnamed = new Pad(4);
__unnamed2 = new Pad(4);
__unnamed3 = new Pad(4);
_inertialMatrixAndInverseCollection = new InertialMatrixBlockCollection(_inertialMatrixAndInverse);
_poweredMassPointsCollection = new PoweredMassPointBlockCollection(_poweredMassPoints);
_massPointsCollection = new MassPointBlockCollection(_massPoints);

}
public void Read(BinaryReader reader)
{
  _radius.Read(reader);
  _momentScale.Read(reader);
  _mass.Read(reader);
  _centerOfMass.Read(reader);
  _density.Read(reader);
  _gravityScale.Read(reader);
  _groundFriction.Read(reader);
  _groundDepth.Read(reader);
  _groundDampFraction.Read(reader);
  _groundNormalK1.Read(reader);
  _groundNormalK0.Read(reader);
  __unnamed.Read(reader);
  _waterFriction.Read(reader);
  _waterDepth.Read(reader);
  _waterDensity.Read(reader);
  __unnamed2.Read(reader);
  _airFriction.Read(reader);
  __unnamed3.Read(reader);
  _xxMoment.Read(reader);
  _yyMoment.Read(reader);
  _zzMoment.Read(reader);
  _inertialMatrixAndInverse.Read(reader);
  _poweredMassPoints.Read(reader);
  _massPoints.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
for (int x=0; x<_inertialMatrixAndInverse.Count; x++)
{
  InertialMatrixAndInverse.AddNew();
  InertialMatrixAndInverse[x].Read(reader);
}
for (int x=0; x<_inertialMatrixAndInverse.Count; x++)
  InertialMatrixAndInverse[x].ReadChildData(reader);
for (int x=0; x<_poweredMassPoints.Count; x++)
{
  PoweredMassPoints.AddNew();
  PoweredMassPoints[x].Read(reader);
}
for (int x=0; x<_poweredMassPoints.Count; x++)
  PoweredMassPoints[x].ReadChildData(reader);
for (int x=0; x<_massPoints.Count; x++)
{
  MassPoints.AddNew();
  MassPoints[x].Read(reader);
}
for (int x=0; x<_massPoints.Count; x++)
  MassPoints[x].ReadChildData(reader);
}
public void Write(BinaryWriter writer)
{
    _radius.Write(writer);
    _momentScale.Write(writer);
    _mass.Write(writer);
    _centerOfMass.Write(writer);
    _density.Write(writer);
    _gravityScale.Write(writer);
    _groundFriction.Write(writer);
    _groundDepth.Write(writer);
    _groundDampFraction.Write(writer);
    _groundNormalK1.Write(writer);
    _groundNormalK0.Write(writer);
    __unnamed.Write(writer);
    _waterFriction.Write(writer);
    _waterDepth.Write(writer);
    _waterDensity.Write(writer);
    __unnamed2.Write(writer);
    _airFriction.Write(writer);
    __unnamed3.Write(writer);
    _xxMoment.Write(writer);
    _yyMoment.Write(writer);
    _zzMoment.Write(writer);
    _inertialMatrixAndInverse.Write(writer);
    _poweredMassPoints.Write(writer);
    _massPoints.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_inertialMatrixAndInverse.UpdateReflexiveOffset(writer);
for (int x=0; x<_inertialMatrixAndInverse.Count; x++)
{
  InertialMatrixAndInverse[x].Write(writer);
}
for (int x=0; x<_inertialMatrixAndInverse.Count; x++)
  InertialMatrixAndInverse[x].WriteChildData(writer);
_poweredMassPoints.UpdateReflexiveOffset(writer);
for (int x=0; x<_poweredMassPoints.Count; x++)
{
  PoweredMassPoints[x].Write(writer);
}
for (int x=0; x<_poweredMassPoints.Count; x++)
  PoweredMassPoints[x].WriteChildData(writer);
_massPoints.UpdateReflexiveOffset(writer);
for (int x=0; x<_massPoints.Count; x++)
{
  MassPoints[x].Write(writer);
}
for (int x=0; x<_massPoints.Count; x++)
  MassPoints[x].WriteChildData(writer);
}
}
public class InertialMatrixBlock : IBlock
{
private RealVector3D _yy_Pluszz = new RealVector3D();
private RealVector3D __unnamed = new RealVector3D();
private RealVector3D __unnamed2 = new RealVector3D();
public RealVector3D Yy_Pluszz
{
  get { return _yy_Pluszz; }
  set { _yy_Pluszz = value; }
}
public RealVector3D _unnamed
{
  get { return __unnamed; }
  set { __unnamed = value; }
}
public RealVector3D _unnamed2
{
  get { return __unnamed2; }
  set { __unnamed2 = value; }
}
public InertialMatrixBlock()
{

}
public void Read(BinaryReader reader)
{
  _yy_Pluszz.Read(reader);
  __unnamed.Read(reader);
  __unnamed2.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
}
public void Write(BinaryWriter writer)
{
    _yy_Pluszz.Write(writer);
    __unnamed.Write(writer);
    __unnamed2.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
}
}
public class PoweredMassPointBlock : IBlock
{
private FixedLengthString _name = new FixedLengthString();
private Flags  _flags;	
private Real _antigravStrength = new Real();
private Real _antigravOffset = new Real();
private Real _antigravHeight = new Real();
private Real _antigravDampFraction = new Real();
private Real _antigravNormalK1 = new Real();
private Real _antigravNormalK0 = new Real();
private Pad  __unnamed;	
public FixedLengthString Name
{
  get { return _name; }
  set { _name = value; }
}
public Flags Flags
{
  get { return _flags; }
  set { _flags = value; }
}
public Real AntigravStrength
{
  get { return _antigravStrength; }
  set { _antigravStrength = value; }
}
public Real AntigravOffset
{
  get { return _antigravOffset; }
  set { _antigravOffset = value; }
}
public Real AntigravHeight
{
  get { return _antigravHeight; }
  set { _antigravHeight = value; }
}
public Real AntigravDampFraction
{
  get { return _antigravDampFraction; }
  set { _antigravDampFraction = value; }
}
public Real AntigravNormalK1
{
  get { return _antigravNormalK1; }
  set { _antigravNormalK1 = value; }
}
public Real AntigravNormalK0
{
  get { return _antigravNormalK0; }
  set { _antigravNormalK0 = value; }
}
public PoweredMassPointBlock()
{
_flags = new Flags(4);
__unnamed = new Pad(68);

}
public void Read(BinaryReader reader)
{
  _name.Read(reader);
  _flags.Read(reader);
  _antigravStrength.Read(reader);
  _antigravOffset.Read(reader);
  _antigravHeight.Read(reader);
  _antigravDampFraction.Read(reader);
  _antigravNormalK1.Read(reader);
  _antigravNormalK0.Read(reader);
  __unnamed.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
}
public void Write(BinaryWriter writer)
{
    _name.Write(writer);
    _flags.Write(writer);
    _antigravStrength.Write(writer);
    _antigravOffset.Write(writer);
    _antigravHeight.Write(writer);
    _antigravDampFraction.Write(writer);
    _antigravNormalK1.Write(writer);
    _antigravNormalK0.Write(writer);
    __unnamed.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
}
}
public class MassPointBlock : IBlock
{
private FixedLengthString _name = new FixedLengthString();
private ShortBlockIndex _poweredMassPoint = new ShortBlockIndex();
private ShortInteger _modelNode = new ShortInteger();
private Flags  _flags;	
private Real _relativeMass = new Real();
private Real _mass = new Real();
private Real _relativeDensity = new Real();
private Real _density = new Real();
private RealPoint3D _position = new RealPoint3D();
private RealVector3D _forward = new RealVector3D();
private RealVector3D _up = new RealVector3D();
private Enum _frictionType = new Enum();
private Pad  __unnamed;	
private Real _frictionParallelScale = new Real();
private Real _frictionPerpendicularScale = new Real();
private Real _radius = new Real();
private Pad  __unnamed2;	
public FixedLengthString Name
{
  get { return _name; }
  set { _name = value; }
}
public ShortBlockIndex PoweredMassPoint
{
  get { return _poweredMassPoint; }
  set { _poweredMassPoint = value; }
}
public ShortInteger ModelNode
{
  get { return _modelNode; }
  set { _modelNode = value; }
}
public Flags Flags
{
  get { return _flags; }
  set { _flags = value; }
}
public Real RelativeMass
{
  get { return _relativeMass; }
  set { _relativeMass = value; }
}
public Real Mass
{
  get { return _mass; }
  set { _mass = value; }
}
public Real RelativeDensity
{
  get { return _relativeDensity; }
  set { _relativeDensity = value; }
}
public Real Density
{
  get { return _density; }
  set { _density = value; }
}
public RealPoint3D Position
{
  get { return _position; }
  set { _position = value; }
}
public RealVector3D Forward
{
  get { return _forward; }
  set { _forward = value; }
}
public RealVector3D Up
{
  get { return _up; }
  set { _up = value; }
}
public Enum FrictionType
{
  get { return _frictionType; }
  set { _frictionType = value; }
}
public Real FrictionParallelScale
{
  get { return _frictionParallelScale; }
  set { _frictionParallelScale = value; }
}
public Real FrictionPerpendicularScale
{
  get { return _frictionPerpendicularScale; }
  set { _frictionPerpendicularScale = value; }
}
public Real Radius
{
  get { return _radius; }
  set { _radius = value; }
}
public MassPointBlock()
{
_flags = new Flags(4);
__unnamed = new Pad(2);
__unnamed2 = new Pad(20);

}
public void Read(BinaryReader reader)
{
  _name.Read(reader);
  _poweredMassPoint.Read(reader);
  _modelNode.Read(reader);
  _flags.Read(reader);
  _relativeMass.Read(reader);
  _mass.Read(reader);
  _relativeDensity.Read(reader);
  _density.Read(reader);
  _position.Read(reader);
  _forward.Read(reader);
  _up.Read(reader);
  _frictionType.Read(reader);
  __unnamed.Read(reader);
  _frictionParallelScale.Read(reader);
  _frictionPerpendicularScale.Read(reader);
  _radius.Read(reader);
  __unnamed2.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
}
public void Write(BinaryWriter writer)
{
    _name.Write(writer);
    _poweredMassPoint.Write(writer);
    _modelNode.Write(writer);
    _flags.Write(writer);
    _relativeMass.Write(writer);
    _mass.Write(writer);
    _relativeDensity.Write(writer);
    _density.Write(writer);
    _position.Write(writer);
    _forward.Write(writer);
    _up.Write(writer);
    _frictionType.Write(writer);
    __unnamed.Write(writer);
    _frictionParallelScale.Write(writer);
    _frictionPerpendicularScale.Write(writer);
    _radius.Write(writer);
    __unnamed2.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
}
}
  }
}
