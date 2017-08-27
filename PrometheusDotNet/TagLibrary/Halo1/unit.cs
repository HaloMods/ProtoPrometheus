// ---------------------------------------------------
// Prometheus Tag Library - Halo1
// Tag definition for 'Unit' (derived from 'Object')
// Generated on 11/20/2005 at 12:50 PM by BLACK\Justin
// ---------------------------------------------------
namespace TagLibrary.Halo1
{
  using System.IO;
  using TagLibrary.Types;
  
  public class Unit : Object
  {
    private UnitBlock UnitValues = new UnitBlock();
    public override void Read(BinaryReader reader)
    {
      base.Read(reader);
      UnitValues.Read(reader);
    }
    public override void ReadChildData(BinaryReader reader)
    {
      base.ReadChildData(reader);
      UnitValues.ReadChildData(reader);
    }
    public class UnitBlock : IBlock
    {
      private Flags flags;
      private Enum defaultTeam = new Enum();
      private Enum constantSoundVolume = new Enum();
      private Real riderDamageFraction = new Real();
      private TagReference integratedLightToggle = new TagReference();
      private Enum aIn = new Enum();
      private Enum bIn = new Enum();
      private Enum cIn = new Enum();
      private Enum dIn = new Enum();
      private Angle cameraFieldOfView = new Angle();
      private Real cameraStiffness = new Real();
      private FixedLengthString cameraMarkerName = new FixedLengthString();
      private FixedLengthString cameraSubmergedMarkerName = new FixedLengthString();
      private Angle pitchAut = new Angle();
      private AngleBounds pitchRange = new AngleBounds();
      private Block cameraTracks = new Block();
      private RealVector3D seatAccelerationScale = new RealVector3D();
      private Pad _unnamed;
      private Real softPingThreshold = new Real();
      private Real softPingInterruptTime = new Real();
      private Real hardPingThreshold = new Real();
      private Real hardPingInterruptTime = new Real();
      private Real hardDeathThreshold = new Real();
      private Real feignDeathThreshold = new Real();
      private Real feignDeathTime = new Real();
      private Real distanceOfEvadeAnim = new Real();
      private Real distanceOfDiveAnim = new Real();
      private Pad _unnamed2;
      private Real stunnedMovementThreshold = new Real();
      private Real feignDeathChance = new Real();
      private Real feignRepeatChance = new Real();
      private TagReference spawnedActor = new TagReference();
      private ShortBounds spawnedActorCount = new ShortBounds();
      private Real spawnedVelocity = new Real();
      private Angle aimingVelocityMaximum = new Angle();
      private Angle aimingAccelerationMaximum = new Angle();
      private RealFraction casualAimingModifier = new RealFraction();
      private Angle lookingVelocityMaximum = new Angle();
      private Angle lookingAccelerationMaximum = new Angle();
      private Pad _unnamed3;
      private Real aIVehicleRadius = new Real();
      private Real aIDangerRadius = new Real();
      private TagReference meleeDamage = new TagReference();
      private Enum motionSensorBlipSize = new Enum();
      private Pad _unnamed4;
      private Pad _unnamed5;
      private Block nEWHUDINTERFACES = new Block();
      private Block dialogueVariants = new Block();
      private Real grenadeVelocity = new Real();
      private Enum grenadeType = new Enum();
      private ShortInteger grenadeCount = new ShortInteger();
      private Pad _unnamed6;
      private Block poweredSeats = new Block();
      private Block weapons = new Block();
      private Block seats = new Block();
      private UnitCameraTrackBlockCollection cameraTracksCollection;
      private UnitHudReferenceBlockCollection nEWHUDINTERFACESCollection;
      private DialogueVariantBlockCollection dialogueVariantsCollection;
      private PoweredSeatBlockCollection poweredSeatsCollection;
      private UnitWeaponBlockCollection weaponsCollection;
      private UnitSeatBlockCollection seatsCollection;
      public UnitBlock()
      {
        this.cameraTracksCollection = new UnitCameraTrackBlockCollection(this.cameraTracks);
        this.nEWHUDINTERFACESCollection = new UnitHudReferenceBlockCollection(this.nEWHUDINTERFACES);
        this.dialogueVariantsCollection = new DialogueVariantBlockCollection(this.dialogueVariants);
        this.poweredSeatsCollection = new PoweredSeatBlockCollection(this.poweredSeats);
        this.weaponsCollection = new UnitWeaponBlockCollection(this.weapons);
        this.seatsCollection = new UnitSeatBlockCollection(this.seats);
        this.flags = new Flags(4);
        this._unnamed = new Pad(12);
        this._unnamed2 = new Pad(4);
        this._unnamed3 = new Pad(8);
        this._unnamed4 = new Pad(2);
        this._unnamed5 = new Pad(12);
        this._unnamed6 = new Pad(4);
      }
      public UnitCameraTrackBlockCollection CameraTracks
      {
        get
        {
          return this.cameraTracksCollection;
        }
      }
      public UnitHudReferenceBlockCollection NEWHUDINTERFACES
      {
        get
        {
          return this.nEWHUDINTERFACESCollection;
        }
      }
      public DialogueVariantBlockCollection DialogueVariants
      {
        get
        {
          return this.dialogueVariantsCollection;
        }
      }
      public PoweredSeatBlockCollection PoweredSeats
      {
        get
        {
          return this.poweredSeatsCollection;
        }
      }
      public UnitWeaponBlockCollection Weapons
      {
        get
        {
          return this.weaponsCollection;
        }
      }
      public UnitSeatBlockCollection Seats
      {
        get
        {
          return this.seatsCollection;
        }
      }
      public Flags Flags
      {
        get
        {
          return this.flags;
        }
        set
        {
          this.flags = value;
        }
      }
      public Enum DefaultTeam
      {
        get
        {
          return this.defaultTeam;
        }
        set
        {
          this.defaultTeam = value;
        }
      }
      public Enum ConstantSoundVolume
      {
        get
        {
          return this.constantSoundVolume;
        }
        set
        {
          this.constantSoundVolume = value;
        }
      }
      public Real RiderDamageFraction
      {
        get
        {
          return this.riderDamageFraction;
        }
        set
        {
          this.riderDamageFraction = value;
        }
      }
      public TagReference IntegratedLightToggle
      {
        get
        {
          return this.integratedLightToggle;
        }
        set
        {
          this.integratedLightToggle = value;
        }
      }
      public Enum AIn
      {
        get
        {
          return this.aIn;
        }
        set
        {
          this.aIn = value;
        }
      }
      public Enum BIn
      {
        get
        {
          return this.bIn;
        }
        set
        {
          this.bIn = value;
        }
      }
      public Enum CIn
      {
        get
        {
          return this.cIn;
        }
        set
        {
          this.cIn = value;
        }
      }
      public Enum DIn
      {
        get
        {
          return this.dIn;
        }
        set
        {
          this.dIn = value;
        }
      }
      public Angle CameraFieldOfView
      {
        get
        {
          return this.cameraFieldOfView;
        }
        set
        {
          this.cameraFieldOfView = value;
        }
      }
      public Real CameraStiffness
      {
        get
        {
          return this.cameraStiffness;
        }
        set
        {
          this.cameraStiffness = value;
        }
      }
      public FixedLengthString CameraMarkerName
      {
        get
        {
          return this.cameraMarkerName;
        }
        set
        {
          this.cameraMarkerName = value;
        }
      }
      public FixedLengthString CameraSubmergedMarkerName
      {
        get
        {
          return this.cameraSubmergedMarkerName;
        }
        set
        {
          this.cameraSubmergedMarkerName = value;
        }
      }
      public Angle PitchAut
      {
        get
        {
          return this.pitchAut;
        }
        set
        {
          this.pitchAut = value;
        }
      }
      public AngleBounds PitchRange
      {
        get
        {
          return this.pitchRange;
        }
        set
        {
          this.pitchRange = value;
        }
      }
      public RealVector3D SeatAccelerationScale
      {
        get
        {
          return this.seatAccelerationScale;
        }
        set
        {
          this.seatAccelerationScale = value;
        }
      }
      public Real SoftPingThreshold
      {
        get
        {
          return this.softPingThreshold;
        }
        set
        {
          this.softPingThreshold = value;
        }
      }
      public Real SoftPingInterruptTime
      {
        get
        {
          return this.softPingInterruptTime;
        }
        set
        {
          this.softPingInterruptTime = value;
        }
      }
      public Real HardPingThreshold
      {
        get
        {
          return this.hardPingThreshold;
        }
        set
        {
          this.hardPingThreshold = value;
        }
      }
      public Real HardPingInterruptTime
      {
        get
        {
          return this.hardPingInterruptTime;
        }
        set
        {
          this.hardPingInterruptTime = value;
        }
      }
      public Real HardDeathThreshold
      {
        get
        {
          return this.hardDeathThreshold;
        }
        set
        {
          this.hardDeathThreshold = value;
        }
      }
      public Real FeignDeathThreshold
      {
        get
        {
          return this.feignDeathThreshold;
        }
        set
        {
          this.feignDeathThreshold = value;
        }
      }
      public Real FeignDeathTime
      {
        get
        {
          return this.feignDeathTime;
        }
        set
        {
          this.feignDeathTime = value;
        }
      }
      public Real DistanceOfEvadeAnim
      {
        get
        {
          return this.distanceOfEvadeAnim;
        }
        set
        {
          this.distanceOfEvadeAnim = value;
        }
      }
      public Real DistanceOfDiveAnim
      {
        get
        {
          return this.distanceOfDiveAnim;
        }
        set
        {
          this.distanceOfDiveAnim = value;
        }
      }
      public Real StunnedMovementThreshold
      {
        get
        {
          return this.stunnedMovementThreshold;
        }
        set
        {
          this.stunnedMovementThreshold = value;
        }
      }
      public Real FeignDeathChance
      {
        get
        {
          return this.feignDeathChance;
        }
        set
        {
          this.feignDeathChance = value;
        }
      }
      public Real FeignRepeatChance
      {
        get
        {
          return this.feignRepeatChance;
        }
        set
        {
          this.feignRepeatChance = value;
        }
      }
      public TagReference SpawnedActor
      {
        get
        {
          return this.spawnedActor;
        }
        set
        {
          this.spawnedActor = value;
        }
      }
      public ShortBounds SpawnedActorCount
      {
        get
        {
          return this.spawnedActorCount;
        }
        set
        {
          this.spawnedActorCount = value;
        }
      }
      public Real SpawnedVelocity
      {
        get
        {
          return this.spawnedVelocity;
        }
        set
        {
          this.spawnedVelocity = value;
        }
      }
      public Angle AimingVelocityMaximum
      {
        get
        {
          return this.aimingVelocityMaximum;
        }
        set
        {
          this.aimingVelocityMaximum = value;
        }
      }
      public Angle AimingAccelerationMaximum
      {
        get
        {
          return this.aimingAccelerationMaximum;
        }
        set
        {
          this.aimingAccelerationMaximum = value;
        }
      }
      public RealFraction CasualAimingModifier
      {
        get
        {
          return this.casualAimingModifier;
        }
        set
        {
          this.casualAimingModifier = value;
        }
      }
      public Angle LookingVelocityMaximum
      {
        get
        {
          return this.lookingVelocityMaximum;
        }
        set
        {
          this.lookingVelocityMaximum = value;
        }
      }
      public Angle LookingAccelerationMaximum
      {
        get
        {
          return this.lookingAccelerationMaximum;
        }
        set
        {
          this.lookingAccelerationMaximum = value;
        }
      }
      public Real AIVehicleRadius
      {
        get
        {
          return this.aIVehicleRadius;
        }
        set
        {
          this.aIVehicleRadius = value;
        }
      }
      public Real AIDangerRadius
      {
        get
        {
          return this.aIDangerRadius;
        }
        set
        {
          this.aIDangerRadius = value;
        }
      }
      public TagReference MeleeDamage
      {
        get
        {
          return this.meleeDamage;
        }
        set
        {
          this.meleeDamage = value;
        }
      }
      public Enum MotionSensorBlipSize
      {
        get
        {
          return this.motionSensorBlipSize;
        }
        set
        {
          this.motionSensorBlipSize = value;
        }
      }
      public Real GrenadeVelocity
      {
        get
        {
          return this.grenadeVelocity;
        }
        set
        {
          this.grenadeVelocity = value;
        }
      }
      public Enum GrenadeType
      {
        get
        {
          return this.grenadeType;
        }
        set
        {
          this.grenadeType = value;
        }
      }
      public ShortInteger GrenadeCount
      {
        get
        {
          return this.grenadeCount;
        }
        set
        {
          this.grenadeCount = value;
        }
      }
      public void Read(BinaryReader reader)
      {
        flags.Read(reader);
        defaultTeam.Read(reader);
        constantSoundVolume.Read(reader);
        riderDamageFraction.Read(reader);
        integratedLightToggle.Read(reader);
        aIn.Read(reader);
        bIn.Read(reader);
        cIn.Read(reader);
        dIn.Read(reader);
        cameraFieldOfView.Read(reader);
        cameraStiffness.Read(reader);
        cameraMarkerName.Read(reader);
        cameraSubmergedMarkerName.Read(reader);
        pitchAut.Read(reader);
        pitchRange.Read(reader);
        cameraTracks.Read(reader);
        seatAccelerationScale.Read(reader);
        _unnamed.Read(reader);
        softPingThreshold.Read(reader);
        softPingInterruptTime.Read(reader);
        hardPingThreshold.Read(reader);
        hardPingInterruptTime.Read(reader);
        hardDeathThreshold.Read(reader);
        feignDeathThreshold.Read(reader);
        feignDeathTime.Read(reader);
        distanceOfEvadeAnim.Read(reader);
        distanceOfDiveAnim.Read(reader);
        _unnamed2.Read(reader);
        stunnedMovementThreshold.Read(reader);
        feignDeathChance.Read(reader);
        feignRepeatChance.Read(reader);
        spawnedActor.Read(reader);
        spawnedActorCount.Read(reader);
        spawnedVelocity.Read(reader);
        aimingVelocityMaximum.Read(reader);
        aimingAccelerationMaximum.Read(reader);
        casualAimingModifier.Read(reader);
        lookingVelocityMaximum.Read(reader);
        lookingAccelerationMaximum.Read(reader);
        _unnamed3.Read(reader);
        aIVehicleRadius.Read(reader);
        aIDangerRadius.Read(reader);
        meleeDamage.Read(reader);
        motionSensorBlipSize.Read(reader);
        _unnamed4.Read(reader);
        _unnamed5.Read(reader);
        nEWHUDINTERFACES.Read(reader);
        dialogueVariants.Read(reader);
        grenadeVelocity.Read(reader);
        grenadeType.Read(reader);
        grenadeCount.Read(reader);
        _unnamed6.Read(reader);
        poweredSeats.Read(reader);
        weapons.Read(reader);
        seats.Read(reader);
      }
      public void ReadChildData(BinaryReader reader)
      {
        int x = 0;
        integratedLightToggle.ReadString(reader);
        for (x = 0; (x < cameraTracks.Count); x = (x + 1))
        {
          CameraTracks.AddNew();
          CameraTracks[x].Read(reader);
        }
        for (x = 0; (x < cameraTracks.Count); x = (x + 1))
        {
          CameraTracks[x].ReadChildData(reader);
        }
        spawnedActor.ReadString(reader);
        meleeDamage.ReadString(reader);
        for (x = 0; (x < nEWHUDINTERFACES.Count); x = (x + 1))
        {
          NEWHUDINTERFACES.AddNew();
          NEWHUDINTERFACES[x].Read(reader);
        }
        for (x = 0; (x < nEWHUDINTERFACES.Count); x = (x + 1))
        {
          NEWHUDINTERFACES[x].ReadChildData(reader);
        }
        for (x = 0; (x < dialogueVariants.Count); x = (x + 1))
        {
          DialogueVariants.AddNew();
          DialogueVariants[x].Read(reader);
        }
        for (x = 0; (x < dialogueVariants.Count); x = (x + 1))
        {
          DialogueVariants[x].ReadChildData(reader);
        }
        for (x = 0; (x < poweredSeats.Count); x = (x + 1))
        {
          PoweredSeats.AddNew();
          PoweredSeats[x].Read(reader);
        }
        for (x = 0; (x < poweredSeats.Count); x = (x + 1))
        {
          PoweredSeats[x].ReadChildData(reader);
        }
        for (x = 0; (x < weapons.Count); x = (x + 1))
        {
          Weapons.AddNew();
          Weapons[x].Read(reader);
        }
        for (x = 0; (x < weapons.Count); x = (x + 1))
        {
          Weapons[x].ReadChildData(reader);
        }
        for (x = 0; (x < seats.Count); x = (x + 1))
        {
          Seats.AddNew();
          Seats[x].Read(reader);
        }
        for (x = 0; (x < seats.Count); x = (x + 1))
        {
          Seats[x].ReadChildData(reader);
        }
      }
      public class UnitCameraTrackBlockCollection : System.Collections.CollectionBase
      {
        private Block linkedBlock;
        public UnitCameraTrackBlockCollection(Block linkedBlock)
        {
          this.linkedBlock = linkedBlock;
        }
        public UnitCameraTrackBlock this[int index]
        {
          get
          {
            return ((UnitCameraTrackBlock)(this.InnerList[index]));
          }
        }
        public void Add(UnitCameraTrackBlock block)
        {
          InnerList.Add(block);
          if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
        }
        public void AddNew()
        {
          this.Add(new UnitCameraTrackBlock());
        }
        public void Remove(int index)
        {
          InnerList.RemoveAt(index);
          if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
        }
      }
      public class UnitHudReferenceBlockCollection : System.Collections.CollectionBase
      {
        private Block linkedBlock;
        public UnitHudReferenceBlockCollection(Block linkedBlock)
        {
          this.linkedBlock = linkedBlock;
        }
        public UnitHudReferenceBlock this[int index]
        {
          get
          {
            return ((UnitHudReferenceBlock)(this.InnerList[index]));
          }
        }
        public void Add(UnitHudReferenceBlock block)
        {
          InnerList.Add(block);
          if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
        }
        public void AddNew()
        {
          this.Add(new UnitHudReferenceBlock());
        }
        public void Remove(int index)
        {
          InnerList.RemoveAt(index);
          if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
        }
      }
      public class DialogueVariantBlockCollection : System.Collections.CollectionBase
      {
        private Block linkedBlock;
        public DialogueVariantBlockCollection(Block linkedBlock)
        {
          this.linkedBlock = linkedBlock;
        }
        public DialogueVariantBlock this[int index]
        {
          get
          {
            return ((DialogueVariantBlock)(this.InnerList[index]));
          }
        }
        public void Add(DialogueVariantBlock block)
        {
          InnerList.Add(block);
          if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
        }
        public void AddNew()
        {
          this.Add(new DialogueVariantBlock());
        }
        public void Remove(int index)
        {
          InnerList.RemoveAt(index);
          if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
        }
      }
      public class PoweredSeatBlockCollection : System.Collections.CollectionBase
      {
        private Block linkedBlock;
        public PoweredSeatBlockCollection(Block linkedBlock)
        {
          this.linkedBlock = linkedBlock;
        }
        public PoweredSeatBlock this[int index]
        {
          get
          {
            return ((PoweredSeatBlock)(this.InnerList[index]));
          }
        }
        public void Add(PoweredSeatBlock block)
        {
          InnerList.Add(block);
          if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
        }
        public void AddNew()
        {
          this.Add(new PoweredSeatBlock());
        }
        public void Remove(int index)
        {
          InnerList.RemoveAt(index);
          if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
        }
      }
      public class UnitWeaponBlockCollection : System.Collections.CollectionBase
      {
        private Block linkedBlock;
        public UnitWeaponBlockCollection(Block linkedBlock)
        {
          this.linkedBlock = linkedBlock;
        }
        public UnitWeaponBlock this[int index]
        {
          get
          {
            return ((UnitWeaponBlock)(this.InnerList[index]));
          }
        }
        public void Add(UnitWeaponBlock block)
        {
          InnerList.Add(block);
          if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
        }
        public void AddNew()
        {
          this.Add(new UnitWeaponBlock());
        }
        public void Remove(int index)
        {
          InnerList.RemoveAt(index);
          if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
        }
      }
      public class UnitSeatBlockCollection : System.Collections.CollectionBase
      {
        private Block linkedBlock;
        public UnitSeatBlockCollection(Block linkedBlock)
        {
          this.linkedBlock = linkedBlock;
        }
        public UnitSeatBlock this[int index]
        {
          get
          {
            return ((UnitSeatBlock)(this.InnerList[index]));
          }
        }
        public void Add(UnitSeatBlock block)
        {
          InnerList.Add(block);
          if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
        }
        public void AddNew()
        {
          this.Add(new UnitSeatBlock());
        }
        public void Remove(int index)
        {
          InnerList.RemoveAt(index);
          if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
        }
      }
    }
    public class UnitCameraTrackBlock : IBlock
    {
      private TagReference track = new TagReference();
      private Pad _unnamed;
      public UnitCameraTrackBlock()
      {
        this._unnamed = new Pad(12);
      }
      public TagReference Track
      {
        get
        {
          return this.track;
        }
        set
        {
          this.track = value;
        }
      }
      public void Read(BinaryReader reader)
      {
        track.Read(reader);
        _unnamed.Read(reader);
      }
      public void ReadChildData(BinaryReader reader)
      {
        track.ReadString(reader);
      }
    }
    public class UnitHudReferenceBlock : IBlock
    {
      private TagReference unitHudInterface = new TagReference();
      private Pad _unnamed;
      public UnitHudReferenceBlock()
      {
        this._unnamed = new Pad(32);
      }
      public TagReference UnitHudInterface
      {
        get
        {
          return this.unitHudInterface;
        }
        set
        {
          this.unitHudInterface = value;
        }
      }
      public void Read(BinaryReader reader)
      {
        unitHudInterface.Read(reader);
        _unnamed.Read(reader);
      }
      public void ReadChildData(BinaryReader reader)
      {
        unitHudInterface.ReadString(reader);
      }
    }
    public class DialogueVariantBlock : IBlock
    {
      private ShortInteger variantNumber = new ShortInteger();
      private Pad _unnamed;
      private Pad _unnamed2;
      private TagReference dialogue = new TagReference();
      public DialogueVariantBlock()
      {
        this._unnamed = new Pad(2);
        this._unnamed2 = new Pad(4);
      }
      public ShortInteger VariantNumber
      {
        get
        {
          return this.variantNumber;
        }
        set
        {
          this.variantNumber = value;
        }
      }
      public TagReference Dialogue
      {
        get
        {
          return this.dialogue;
        }
        set
        {
          this.dialogue = value;
        }
      }
      public void Read(BinaryReader reader)
      {
        variantNumber.Read(reader);
        _unnamed.Read(reader);
        _unnamed2.Read(reader);
        dialogue.Read(reader);
      }
      public void ReadChildData(BinaryReader reader)
      {
        dialogue.ReadString(reader);
      }
    }
    public class PoweredSeatBlock : IBlock
    {
      private Pad _unnamed;
      private Real driverPowerupTime = new Real();
      private Real driverPowerdownTime = new Real();
      private Pad _unnamed2;
      public PoweredSeatBlock()
      {
        this._unnamed = new Pad(4);
        this._unnamed2 = new Pad(56);
      }
      public Real DriverPowerupTime
      {
        get
        {
          return this.driverPowerupTime;
        }
        set
        {
          this.driverPowerupTime = value;
        }
      }
      public Real DriverPowerdownTime
      {
        get
        {
          return this.driverPowerdownTime;
        }
        set
        {
          this.driverPowerdownTime = value;
        }
      }
      public void Read(BinaryReader reader)
      {
        _unnamed.Read(reader);
        driverPowerupTime.Read(reader);
        driverPowerdownTime.Read(reader);
        _unnamed2.Read(reader);
      }
      public void ReadChildData(BinaryReader reader)
      {
      }
    }
    public class UnitWeaponBlock : IBlock
    {
      private TagReference weapon = new TagReference();
      private Pad _unnamed;
      public UnitWeaponBlock()
      {
        this._unnamed = new Pad(20);
      }
      public TagReference Weapon
      {
        get
        {
          return this.weapon;
        }
        set
        {
          this.weapon = value;
        }
      }
      public void Read(BinaryReader reader)
      {
        weapon.Read(reader);
        _unnamed.Read(reader);
      }
      public void ReadChildData(BinaryReader reader)
      {
        weapon.ReadString(reader);
      }
    }
    public class UnitSeatBlock : IBlock
    {
      private Flags flags;
      private FixedLengthString label = new FixedLengthString();
      private FixedLengthString markerName = new FixedLengthString();
      private Pad _unnamed;
      private RealVector3D accelerationScale = new RealVector3D();
      private Pad _unnamed2;
      private Real yawRate = new Real();
      private Real pitchRate = new Real();
      private FixedLengthString cameraMarkerName = new FixedLengthString();
      private FixedLengthString cameraSubmergedMarkerName = new FixedLengthString();
      private Angle pitchAut = new Angle();
      private AngleBounds pitchRange = new AngleBounds();
      private Block cameraTracks = new Block();
      private Block unitHudInterface = new Block();
      private Pad _unnamed3;
      private ShortInteger hudTextMessageIndex = new ShortInteger();
      private Pad _unnamed4;
      private Angle yawMinimum = new Angle();
      private Angle yawMaximum = new Angle();
      private TagReference buil = new TagReference();
      private Pad _unnamed5;
      private UnitCameraTrackBlockCollection cameraTracksCollection;
      private UnitHudReferenceBlockCollection unitHudInterfaceCollection;
      public UnitSeatBlock()
      {
        this.cameraTracksCollection = new UnitCameraTrackBlockCollection(this.cameraTracks);
        this.unitHudInterfaceCollection = new UnitHudReferenceBlockCollection(this.unitHudInterface);
        this.flags = new Flags(4);
        this._unnamed = new Pad(32);
        this._unnamed2 = new Pad(12);
        this._unnamed3 = new Pad(4);
        this._unnamed4 = new Pad(2);
        this._unnamed5 = new Pad(20);
      }
      public UnitCameraTrackBlockCollection CameraTracks
      {
        get
        {
          return this.cameraTracksCollection;
        }
      }
      public UnitHudReferenceBlockCollection UnitHudInterface
      {
        get
        {
          return this.unitHudInterfaceCollection;
        }
      }
      public Flags Flags
      {
        get
        {
          return this.flags;
        }
        set
        {
          this.flags = value;
        }
      }
      public FixedLengthString Label
      {
        get
        {
          return this.label;
        }
        set
        {
          this.label = value;
        }
      }
      public FixedLengthString MarkerName
      {
        get
        {
          return this.markerName;
        }
        set
        {
          this.markerName = value;
        }
      }
      public RealVector3D AccelerationScale
      {
        get
        {
          return this.accelerationScale;
        }
        set
        {
          this.accelerationScale = value;
        }
      }
      public Real YawRate
      {
        get
        {
          return this.yawRate;
        }
        set
        {
          this.yawRate = value;
        }
      }
      public Real PitchRate
      {
        get
        {
          return this.pitchRate;
        }
        set
        {
          this.pitchRate = value;
        }
      }
      public FixedLengthString CameraMarkerName
      {
        get
        {
          return this.cameraMarkerName;
        }
        set
        {
          this.cameraMarkerName = value;
        }
      }
      public FixedLengthString CameraSubmergedMarkerName
      {
        get
        {
          return this.cameraSubmergedMarkerName;
        }
        set
        {
          this.cameraSubmergedMarkerName = value;
        }
      }
      public Angle PitchAut
      {
        get
        {
          return this.pitchAut;
        }
        set
        {
          this.pitchAut = value;
        }
      }
      public AngleBounds PitchRange
      {
        get
        {
          return this.pitchRange;
        }
        set
        {
          this.pitchRange = value;
        }
      }
      public ShortInteger HudTextMessageIndex
      {
        get
        {
          return this.hudTextMessageIndex;
        }
        set
        {
          this.hudTextMessageIndex = value;
        }
      }
      public Angle YawMinimum
      {
        get
        {
          return this.yawMinimum;
        }
        set
        {
          this.yawMinimum = value;
        }
      }
      public Angle YawMaximum
      {
        get
        {
          return this.yawMaximum;
        }
        set
        {
          this.yawMaximum = value;
        }
      }
      public TagReference Buil
      {
        get
        {
          return this.buil;
        }
        set
        {
          this.buil = value;
        }
      }
      public void Read(BinaryReader reader)
      {
        flags.Read(reader);
        label.Read(reader);
        markerName.Read(reader);
        _unnamed.Read(reader);
        accelerationScale.Read(reader);
        _unnamed2.Read(reader);
        yawRate.Read(reader);
        pitchRate.Read(reader);
        cameraMarkerName.Read(reader);
        cameraSubmergedMarkerName.Read(reader);
        pitchAut.Read(reader);
        pitchRange.Read(reader);
        cameraTracks.Read(reader);
        unitHudInterface.Read(reader);
        _unnamed3.Read(reader);
        hudTextMessageIndex.Read(reader);
        _unnamed4.Read(reader);
        yawMinimum.Read(reader);
        yawMaximum.Read(reader);
        buil.Read(reader);
        _unnamed5.Read(reader);
      }
      public void ReadChildData(BinaryReader reader)
      {
        int x = 0;
        for (x = 0; (x < cameraTracks.Count); x = (x + 1))
        {
          CameraTracks.AddNew();
          CameraTracks[x].Read(reader);
        }
        for (x = 0; (x < cameraTracks.Count); x = (x + 1))
        {
          CameraTracks[x].ReadChildData(reader);
        }
        for (x = 0; (x < unitHudInterface.Count); x = (x + 1))
        {
          UnitHudInterface.AddNew();
          UnitHudInterface[x].Read(reader);
        }
        for (x = 0; (x < unitHudInterface.Count); x = (x + 1))
        {
          UnitHudInterface[x].ReadChildData(reader);
        }
        buil.ReadString(reader);
      }
      public class UnitCameraTrackBlockCollection : System.Collections.CollectionBase
      {
        private Block linkedBlock;
        public UnitCameraTrackBlockCollection(Block linkedBlock)
        {
          this.linkedBlock = linkedBlock;
        }
        public UnitCameraTrackBlock this[int index]
        {
          get
          {
            return ((UnitCameraTrackBlock)(this.InnerList[index]));
          }
        }
        public void Add(UnitCameraTrackBlock block)
        {
          InnerList.Add(block);
          if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
        }
        public void AddNew()
        {
          this.Add(new UnitCameraTrackBlock());
        }
        public void Remove(int index)
        {
          InnerList.RemoveAt(index);
          if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
        }
      }
      public class UnitHudReferenceBlockCollection : System.Collections.CollectionBase
      {
        private Block linkedBlock;
        public UnitHudReferenceBlockCollection(Block linkedBlock)
        {
          this.linkedBlock = linkedBlock;
        }
        public UnitHudReferenceBlock this[int index]
        {
          get
          {
            return ((UnitHudReferenceBlock)(this.InnerList[index]));
          }
        }
        public void Add(UnitHudReferenceBlock block)
        {
          InnerList.Add(block);
          if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
        }
        public void AddNew()
        {
          this.Add(new UnitHudReferenceBlock());
        }
        public void Remove(int index)
        {
          InnerList.RemoveAt(index);
          if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
        }
      }
    }
  }
}
