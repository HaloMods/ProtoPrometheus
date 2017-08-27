using System;
using System.IO;

namespace Prometheus.Core.Tags
{
  class SCNR_HEADER
  {
    public char[] unk_str1 = new char[16];
    public char[] unk_str2 = new char[16];
    public char[] unk_str3 = new char[16];
    public REFLEXIVE SkyBox = new REFLEXIVE();
    public int unk1;
    public REFLEXIVE ChildScenarios = new REFLEXIVE();

    public uint[] unneeded1 = new uint[46];
    public int EditorScenarioSize;
    public int unk2;
    public int unk3;
    public uint pointertoindex;
    public uint[] unneeded2 = new uint[2];
    public uint pointertoendofindex;
    public uint[] zero1 = new uint[57];

    public REFLEXIVE ObjectNames = new REFLEXIVE();
    public REFLEXIVE Scenery = new REFLEXIVE();
    public REFLEXIVE SceneryRef = new REFLEXIVE();
    public REFLEXIVE Biped = new REFLEXIVE();
    public REFLEXIVE BipedRef = new REFLEXIVE();
    public REFLEXIVE Vehicle = new REFLEXIVE();
    public REFLEXIVE VehicleRef = new REFLEXIVE();
    public REFLEXIVE Equip = new REFLEXIVE();
    public REFLEXIVE EquipRef = new REFLEXIVE();
    public REFLEXIVE Weap = new REFLEXIVE();
    public REFLEXIVE WeapRef = new REFLEXIVE();
    public REFLEXIVE DeviceGroups = new REFLEXIVE();
    public REFLEXIVE Machine = new REFLEXIVE();
    public REFLEXIVE MachineRef = new REFLEXIVE();
    public REFLEXIVE Control = new REFLEXIVE();
    public REFLEXIVE ControlRef = new REFLEXIVE();
    public REFLEXIVE LightFixture = new REFLEXIVE();
    public REFLEXIVE LightFixtureRef = new REFLEXIVE();
    public REFLEXIVE SoundScenery = new REFLEXIVE();
    public REFLEXIVE SoundSceneryRef = new REFLEXIVE();
    public REFLEXIVE[] Unknown1 = new REFLEXIVE[7];
    public REFLEXIVE PlayerStartingProfile = new REFLEXIVE();
    public REFLEXIVE PlayerSpawn = new REFLEXIVE();
    public REFLEXIVE TriggerVolumes = new REFLEXIVE();
    public REFLEXIVE Animations = new REFLEXIVE();
    public REFLEXIVE MultiplayerFlags = new REFLEXIVE();
    public REFLEXIVE MpEquip = new REFLEXIVE();
    public REFLEXIVE StartingEquip = new REFLEXIVE();
    public REFLEXIVE BspSwitchTrigger = new REFLEXIVE();
    public REFLEXIVE Decals = new REFLEXIVE();
    public REFLEXIVE DecalsRef = new REFLEXIVE();
    public REFLEXIVE DetailObjCollRef = new REFLEXIVE();
    public REFLEXIVE[] Unknown3 = new REFLEXIVE[7];
    public REFLEXIVE ActorVariantRef = new REFLEXIVE();
    public REFLEXIVE Encounters = new REFLEXIVE();
    //below this, structs still not confirmed
    public REFLEXIVE CommandLists = new REFLEXIVE();
    public REFLEXIVE Unknown2 = new REFLEXIVE();
    public REFLEXIVE StartingLocations = new REFLEXIVE();
    public REFLEXIVE Platoons = new REFLEXIVE();
    public REFLEXIVE AiConversations = new REFLEXIVE();
    public uint ScriptSyntaxDataSize;
    public uint Unknown4;
    public REFLEXIVE ScriptCrap = new REFLEXIVE();
    public REFLEXIVE Commands = new REFLEXIVE();
    public REFLEXIVE Points = new REFLEXIVE();
    public REFLEXIVE AiAnimationRefs = new REFLEXIVE();
    public REFLEXIVE GlobalsVerified = new REFLEXIVE();
    public REFLEXIVE AiRecordingRefs = new REFLEXIVE();
    public REFLEXIVE Unknown5 = new REFLEXIVE();
    public REFLEXIVE Participants = new REFLEXIVE();
    public REFLEXIVE Lines = new REFLEXIVE();
    public REFLEXIVE ScriptTriggers = new REFLEXIVE();
    public REFLEXIVE VerifyCutscenes = new REFLEXIVE();
    public REFLEXIVE VerifyCutsceneTitle = new REFLEXIVE();
    public REFLEXIVE SourceFiles = new REFLEXIVE();
    public REFLEXIVE CutsceneFlags = new REFLEXIVE();
    public REFLEXIVE CutsceneCameraPoi = new REFLEXIVE();
    public REFLEXIVE CutsceneTitles = new REFLEXIVE();
    public REFLEXIVE[] Unknown6 = new REFLEXIVE[8];
    public uint Unknown7;
    public uint Unknown8;
    public REFLEXIVE StructBsp = new REFLEXIVE();
    public SCNR_HEADER()
    {
      for(int i=0; i<7; i++)
      {
        Unknown1[i] = new REFLEXIVE();
        Unknown3[i] = new REFLEXIVE();
      }

      for(int i=0; i<8; i++)
        Unknown6[i] = new REFLEXIVE();      
    }
    public void Load(ref BinaryReader br)
    {
      unk_str1 = br.ReadChars(16);
      unk_str2 = br.ReadChars(16);
      unk_str3 = br.ReadChars(16);
      
      SkyBox.Load(ref br);
      unk1 = br.ReadInt32();
      ChildScenarios.Load(ref br);
      for(int i=0; i<46; i++)unneeded1[i] = br.ReadUInt32();
      EditorScenarioSize = br.ReadInt32();
      unk2 = br.ReadInt32();
      unk3 = br.ReadInt32();
      pointertoindex = br.ReadUInt32();
      unneeded2[0] = br.ReadUInt32();
      unneeded2[1] = br.ReadUInt32();
      pointertoendofindex = br.ReadUInt32();
      for(int i=0; i<46; i++)zero1[i] = br.ReadUInt32();

      ObjectNames.Load(ref br);
      Scenery.Load(ref br);
      SceneryRef.Load(ref br);
      Biped.Load(ref br);
      BipedRef.Load(ref br);
      Vehicle.Load(ref br);
      VehicleRef.Load(ref br);
      Equip.Load(ref br);
      EquipRef.Load(ref br);
      Weap.Load(ref br);
      WeapRef.Load(ref br);
      DeviceGroups.Load(ref br);
      Machine.Load(ref br);
      MachineRef.Load(ref br);
      Control.Load(ref br);
      ControlRef.Load(ref br);
      LightFixture.Load(ref br);
      LightFixtureRef.Load(ref br);
      SoundScenery.Load(ref br);
      SoundSceneryRef.Load(ref br);
      for(int i=0; i<7; i++)Unknown1[i].Load(ref br);

      PlayerStartingProfile.Load(ref br);
      PlayerSpawn.Load(ref br);
      TriggerVolumes.Load(ref br);
      Animations.Load(ref br);
      MultiplayerFlags.Load(ref br);
      MpEquip.Load(ref br);
      StartingEquip.Load(ref br);
      BspSwitchTrigger.Load(ref br);
      Decals.Load(ref br);
      DecalsRef.Load(ref br);
      DetailObjCollRef.Load(ref br);
      for(int i=0; i<7; i++)Unknown3[i].Load(ref br);
      ActorVariantRef.Load(ref br);
      Encounters.Load(ref br);
      CommandLists.Load(ref br);
      Unknown2.Load(ref br);
      StartingLocations.Load(ref br);
      Platoons.Load(ref br);
      AiConversations.Load(ref br);

    }  
  }    
       
       
  ///  <summary>
	///  Summary description for TagScenario.
	///  </summary>
	public class TagScenario
	{    
		public TagScenario()
		{  
		}  
	}    
}      
       
       
       
       