/* ---------------------------------------------------------------
 * Prometheus
 * Bungie Map-based Multi-Game Editing Kit
 * 2004-2005, Halo-Dev
 * ---------------------------------------------------------------
 * Class       : Prometheus.Core.Tags.Antr.Animations
 * Description : Encapsulates a Halo ANTR tag, and allows for
 *             : loading of it's various structures.
 *             : and returns a list of selected files.
 * Author      : Grenadiac
 * Co-Authors  : 
 * ---------------------------------------------------------------
 */

using System.IO;
using System.Diagnostics;
using Prometheus.Core.Util;
using Prometheus.Core;

namespace Prometheus.Core.Tags.Antr
{

  #region ANTR Structs

  /// <summary>
  /// ANTR_HEADER is the animation tag header.  It locates all of
  /// the animation data blocks relevant to that tag.
  /// </summary>
  public class ANTR_HEADER
  {
    public REFLEXIVE Objects = new REFLEXIVE();
    public REFLEXIVE Units = new REFLEXIVE();
    public REFLEXIVE Weapons = new REFLEXIVE();
    public REFLEXIVE Vehicles = new REFLEXIVE();
    public REFLEXIVE Devices = new REFLEXIVE();
    public REFLEXIVE UnitDamage = new REFLEXIVE();
    public REFLEXIVE FirstPersonWeapon = new REFLEXIVE();
    public REFLEXIVE SoundRefs = new REFLEXIVE();
    public uint RawData1;
    public uint RawData2;
    public REFLEXIVE Nodes = new REFLEXIVE();
    public REFLEXIVE Animations = new REFLEXIVE();

    public void Load(ref BinaryReader br)
    {
      Objects.Load(ref br);
      Units.Load(ref br);
      Weapons.Load(ref br);
      Vehicles.Load(ref br);
      Devices.Load(ref br);
      UnitDamage.Load(ref br);
      FirstPersonWeapon.Load(ref br);
      SoundRefs.Load(ref br);
      RawData1 = br.ReadUInt32();
      RawData2 = br.ReadUInt32();
      Nodes.Load(ref br);
      Animations.Load(ref br);
    }
  }

  public class ANI_FRAME_BOUNDS
  {
    private float RightYawPerFrame;
    private float LeftYawPerFrame;
    private short RightFrameCount;
    private short LeftFrameCount;
    private float DownPitchPerFrame;
    private float UpPitchPerFrame;
    private short DownPitchFrameCount;
    private short UpPitchFrameCount;

    public void Load(ref BinaryReader br)
    {
      RightYawPerFrame = br.ReadSingle();
      LeftYawPerFrame = br.ReadSingle();
      RightFrameCount = br.ReadInt16();
      LeftFrameCount = br.ReadInt16();
      DownPitchPerFrame = br.ReadSingle();
      UpPitchPerFrame = br.ReadSingle();
      DownPitchFrameCount = br.ReadInt16();
      UpPitchFrameCount = br.ReadInt16();
    }
  }

  public class ANTR_OBJECT
  {
    private byte[] unk = new byte[20];

    public void Load(ref BinaryReader br)
    {
      unk = br.ReadBytes(20);
    }
  }

  public class ANTR_WEAPONS
  {
    private uint[] unk = new uint[4];
    private REFLEXIVE Animations = new REFLEXIVE();

    public void Load(ref BinaryReader br)
    {
      unk[0] = br.ReadUInt32();
      unk[1] = br.ReadUInt32();
      unk[2] = br.ReadUInt32();
      unk[3] = br.ReadUInt32();
      Animations.Load(ref br);

      //TODO: load reflexives
      for (int i = 0; i < Animations.Count; i++) ;
    }
  }

  public class ANTR_VEHICLES
  {
    private ANI_FRAME_BOUNDS Bounds = new ANI_FRAME_BOUNDS();
    private byte[] unk = new byte[68];
    private REFLEXIVE Animations = new REFLEXIVE();
    private REFLEXIVE SuspensionAnimations = new REFLEXIVE();

    public void Load(ref BinaryReader br)
    {
      Bounds.Load(ref br);
      unk = br.ReadBytes(68);
      Animations.Load(ref br);
      SuspensionAnimations.Load(ref br);

      //TODO: load reflexives

      //allocate

      //initialize
      for (int i = 0; i < Animations.Count; i++) ;

      for (int i = 0; i < SuspensionAnimations.Count; i++) ;
    }
  }

  public class SUSPENSION_ANI
  {
    private byte[] unk = new byte[20];

    public void Load(ref BinaryReader br)
    {
      unk = br.ReadBytes(20);
    }
  }

  public class ANTR_DEVICES
  {
    private byte[] unk = new byte[84];
    private REFLEXIVE DeviceAnimations = new REFLEXIVE();

    public void Load(ref BinaryReader br)
    {
      unk = br.ReadBytes(84);
      DeviceAnimations.Load(ref br);
    }
  }

  public class ANTR_UNIT
  {
    private char[] Label = new char[32];
    private ANI_FRAME_BOUNDS Bounds = new ANI_FRAME_BOUNDS();
    private uint Unk1;
    private uint Unk2;
    private REFLEXIVE UnitsAnimations = new REFLEXIVE();
    private REFLEXIVE UnitsIKPoints = new REFLEXIVE();
    private REFLEXIVE UnitsWeapons = new REFLEXIVE();

    public void Load(ref BinaryReader br)
    {
      Label = br.ReadChars(32);
      Bounds.Load(ref br);
      Unk1 = br.ReadUInt32();
      Unk2 = br.ReadUInt32();
      UnitsAnimations.Load(ref br);
      UnitsIKPoints.Load(ref br);
      UnitsWeapons.Load(ref br);
    }
  }

  public class ANTR_NODE
  {
    private char[] name = new char[32];
    private short NextSiblingNode;
    private short FirstChildNode;
    private short ParentNode;
    private short Reserved;
    private uint Flags;
    private float[] BaseVector = new float[3];
    private float VectorRange;
    private uint Unk;

    public void Load(ref BinaryReader br)
    {
      name = br.ReadChars(32);
      NextSiblingNode = br.ReadInt16();
      FirstChildNode = br.ReadInt16();
      ParentNode = br.ReadInt16();
      Reserved = br.ReadInt16();
      Flags = br.ReadUInt32();
      BaseVector[0] = br.ReadSingle();
      BaseVector[1] = br.ReadSingle();
      BaseVector[2] = br.ReadSingle();
      VectorRange = br.ReadSingle();
      Unk = br.ReadUInt32();
    }
  }

  public class ANTR_ANIMATION
  {
    public char[] name = new char[32];
    public short Type;
    public short FrameCount;
    public short FrameSize;
    public short FrameInfoType;
    public uint NodeListChecksum;
    public short NodeCount;
    public short LoopFrameIndex;
    public float Weight;
    public short KeyframeIndex;
    public short SecondKeyframeIndex;
    public short NextAnimation;
    public short Flags;
    public short Sound;
    public short SoundFrameIndex;
    public short LeftFootFrameIndexBad;
    public short RightFootFrameIndexBad;
    public float Unk1;
    public uint FrameInfoSize;
    public uint FrameInfoJunk;
    public REFLEXIVE FrameInfo = new REFLEXIVE();
    public uint TranslateStaticMask;
    //uint  Unk2[3];
    public uint RotateStaticMask;
    //uint  Unk3[3];
    public uint ScaleStaticMask;
    //uint  Unk4[3];
    public uint DefaultDataSize;
    public uint DefaultDataJunk;
    public REFLEXIVE DefaultData = new REFLEXIVE();
    public uint FrameDataSize;
    public uint FrameDataJunk;
    public REFLEXIVE FrameData = new REFLEXIVE();

    public void Load(ref BinaryReader br)
    {
      name = br.ReadChars(32);
      Type = br.ReadInt16();
      FrameCount = br.ReadInt16();
      FrameSize = br.ReadInt16();
      FrameInfoType = br.ReadInt16();
      NodeListChecksum = br.ReadUInt32();
      NodeCount = br.ReadInt16();
      LoopFrameIndex = br.ReadInt16();
      Weight = br.ReadSingle();
      KeyframeIndex = br.ReadInt16();
      SecondKeyframeIndex = br.ReadInt16();
      NextAnimation = br.ReadInt16();
      Flags = br.ReadInt16();
      Sound = br.ReadInt16();
      SoundFrameIndex = br.ReadInt16();
      LeftFootFrameIndexBad = br.ReadInt16();
      RightFootFrameIndexBad = br.ReadInt16();
      Unk1 = br.ReadSingle();
      FrameInfoSize = br.ReadUInt32();
      FrameInfoJunk = br.ReadUInt32();
      FrameInfo.Load(ref br);
      TranslateStaticMask = br.ReadUInt32();
      br.BaseStream.Seek(12, SeekOrigin.Current);
      RotateStaticMask = br.ReadUInt32();
      br.BaseStream.Seek(12, SeekOrigin.Current);
      ScaleStaticMask = br.ReadUInt32();
      br.BaseStream.Seek(12, SeekOrigin.Current);
      DefaultDataSize = br.ReadUInt32();
      DefaultDataJunk = br.ReadUInt32();
      DefaultData.Load(ref br);
      FrameDataSize = br.ReadUInt32();
      FrameDataJunk = br.ReadUInt32();
      FrameData.Load(ref br);
    }
  }

  public class KEYFRAME
  {
    public float[] translation = new float[3];
    public float[] rotation = new float[4];
    public float scale;

    public void Load()
    {
    }
  }

  #endregion

  public class AnimationTimer
  {
    private HighResTimer m_Timer = new HighResTimer();
    private float m_CurrentTime;
    private float m_AniEndTime;
    private int m_NumKeyFrames = 0;
    private const float ANIMATION_FPS = 20;

    public bool m_bLooping = true;
    public int m_NextKeyFrame = 0;
    public int m_PrevKeyFrame = 0;
    public float m_Interp = 0;

    public AnimationTimer()
    {
    }

    public void Init(int num_frames)
    {
      m_CurrentTime = 0;
      m_AniEndTime = num_frames*1.0f/ANIMATION_FPS;
      m_NumKeyFrames = num_frames;
      m_NextKeyFrame = 0;
      m_PrevKeyFrame = 0;
      m_Timer.ResetTimer();
    }
    public void UpdateKeyframeInterpolation()
    {
      m_CurrentTime = (float)m_Timer.GetElapsedTime();

      //Check to see if animation has played to the end, roll over
      if(m_CurrentTime > m_AniEndTime)
      {
        if(m_bLooping)
        {
          m_Timer.ResetTimer();
          m_CurrentTime = 0;
          m_NextKeyFrame = 0;
        }
        else
          m_CurrentTime = m_AniEndTime;
      }

      //Find out what the nearest "next" frame is
      while((m_NextKeyFrame < m_NumKeyFrames)&&((float)(m_NextKeyFrame/ANIMATION_FPS) < m_CurrentTime))
        m_NextKeyFrame++;

      if(m_NextKeyFrame == 0)
      {
        m_PrevKeyFrame = 0;
        m_Interp = 0f;
      }
      else if(m_NextKeyFrame == m_NumKeyFrames)
      {
        m_NextKeyFrame = m_NumKeyFrames - 1;
        m_PrevKeyFrame = m_NumKeyFrames - 1;
        m_Interp = 0f;
      }
      else
      {
        float timeDelta = 1.0f/ANIMATION_FPS;
        m_Interp = (float)((m_CurrentTime-(m_NextKeyFrame-1)/ANIMATION_FPS)/timeDelta);
        m_PrevKeyFrame = m_NextKeyFrame-1;
      }

      //Trace.Write("Elapsed time: "+m_CurrentTime.ToString()+" next KF="+m_NextKeyFrame.ToString()+
      //            " interp= "+m_Interp.ToString()+"\n");
    }
  }
  /// <summary>
  /// Summary description for Animations.
  /// </summary>
  public class Animations : TagBase
  {
    private ANTR_HEADER m_Header;
    private ANTR_NODE[] m_Nodes;
    private ANTR_ANIMATION[] m_Animations;
    private KEYFRAME[][] m_Keyframes;
    private int m_CurrentFrame;
    private int m_ActiveAnimation;
    public string[] m_AnimationNames;

    private AnimationTimer AniTmr = new AnimationTimer();
    private Quaternion m_tween = new Quaternion();
    private Quaternion m_q1 = new Quaternion();
    private Quaternion m_q2 = new Quaternion();

    public Animations()
    {
      m_Header = new ANTR_HEADER();
      m_CurrentFrame = 0;
      m_ActiveAnimation = -1;
    }

    public void LoadTagData()
    {
      BinaryReader br = new BinaryReader(m_stream);

      //Load the ANTR header
      m_Header.Load(ref br);

      //Seek to the Animation Section
      br.BaseStream.Position = m_Header.Nodes.Offset;
      m_Nodes = new ANTR_NODE[m_Header.Nodes.Count];
      for (int n = 0; n < m_Header.Nodes.Count; n++)
      {
        m_Nodes[n] = new ANTR_NODE();
        m_Nodes[n].Load(ref br);
      }

      //Seek to the Animation Section
      br.BaseStream.Position = m_Header.Animations.Offset;

      m_Animations = new ANTR_ANIMATION[m_Header.Animations.Count];
      m_Keyframes = new KEYFRAME[m_Header.Animations.Count][];
      m_AnimationNames = new string[m_Header.Animations.Count];
      int len;
      for(int i = 0; i < m_Header.Animations.Count; i++)
      {
        m_Animations[i] = new ANTR_ANIMATION();
        m_Animations[i].Load(ref br);

        len=0;
        while((m_Animations[i].name[len++] != '\0')&&(len<32));
        m_AnimationNames[i] = new string(m_Animations[i].name, 0, len-1);
      }

      for (int i = 0; i < m_Header.Animations.Count; i++)
        DecompressAnimation(ref br, i);
    }

    private bool IsNodeDynamic(int node, uint mask)
    {
      uint bitmask = mask >> node;

      return ((bitmask & 0x1) == 1);
    }

    public void DecompressAnimation(ref BinaryReader br, int ani_index)
    {
      //allocate keyframe data to store decompressed animations
      ANTR_ANIMATION AnimationHeader = m_Animations[ani_index];
      m_Keyframes[ani_index] = new KEYFRAME[AnimationHeader.FrameCount*AnimationHeader.NodeCount];

      int node_count = AnimationHeader.NodeCount; //helper var for readability

      for (int f = 0; f < AnimationHeader.FrameCount; f++)
        for (int n = 0; n < AnimationHeader.NodeCount; n++)
          m_Keyframes[ani_index][f*node_count + n] = new KEYFRAME();


      //Skip FrameInfo section, nothing of interest (yet anyway)
      br.BaseStream.Seek(AnimationHeader.FrameInfoSize, SeekOrigin.Current);


      //Fill in static data
      float[] TransData = new float[3];
      short[] RotData = new short[4];
      float Scale;

      for (int n = 0; n < AnimationHeader.NodeCount; n++)
      {
        if (IsNodeDynamic(n, AnimationHeader.RotateStaticMask) == false)
        {
          RotData[0] = br.ReadInt16();
          RotData[1] = br.ReadInt16();
          RotData[2] = br.ReadInt16();
          RotData[3] = br.ReadInt16();

          for (int f = 0; f < AnimationHeader.FrameCount; f++)
          {
            Quaternion.DecompressQuaternion_64bit(RotData, m_Keyframes[ani_index][f*node_count + n].rotation);
            Quaternion.NormalizeQuaternion(m_Keyframes[ani_index][f*node_count + n].rotation);
          }
        }
        if (IsNodeDynamic(n, AnimationHeader.TranslateStaticMask) == false)
        {
          TransData[0] = br.ReadSingle();
          TransData[1] = br.ReadSingle();
          TransData[2] = br.ReadSingle();

          for (int f = 0; f < AnimationHeader.FrameCount; f++)
          {
            m_Keyframes[ani_index][f*node_count + n].translation[0] = TransData[0];
            m_Keyframes[ani_index][f*node_count + n].translation[1] = TransData[1];
            m_Keyframes[ani_index][f*node_count + n].translation[2] = TransData[2];
          }
        }
        if (IsNodeDynamic(n, AnimationHeader.ScaleStaticMask) == false)
        {
          Scale = br.ReadSingle();
          for (int f = 0; f < AnimationHeader.FrameCount; f++)
            m_Keyframes[ani_index][f*node_count + n].scale = Scale;
        }
      }

      //Fill in dynamic data
      for (int f = 0; f < AnimationHeader.FrameCount; f++)
      {
        for (int n = 0; n < AnimationHeader.NodeCount; n++)
        {
          if (IsNodeDynamic(n, AnimationHeader.RotateStaticMask) == true)
          {
            RotData[0] = br.ReadInt16();
            RotData[1] = br.ReadInt16();
            RotData[2] = br.ReadInt16();
            RotData[3] = br.ReadInt16();
            Quaternion.DecompressQuaternion_64bit(RotData, m_Keyframes[ani_index][f*node_count + n].rotation);
            Quaternion.NormalizeQuaternion(m_Keyframes[ani_index][f*node_count + n].rotation);
          }
          if (IsNodeDynamic(n, AnimationHeader.TranslateStaticMask) == true)
          {
            TransData[0] = br.ReadSingle();
            TransData[1] = br.ReadSingle();
            TransData[2] = br.ReadSingle();
            m_Keyframes[ani_index][f*node_count + n].translation[0] = TransData[0];
            m_Keyframes[ani_index][f*node_count + n].translation[1] = TransData[1];
            m_Keyframes[ani_index][f*node_count + n].translation[2] = TransData[2];
          }
          if (IsNodeDynamic(n, AnimationHeader.ScaleStaticMask) == true)
          {
            m_Keyframes[ani_index][f*node_count + n].scale = br.ReadSingle();
          }
        }
      }
    }
  
    public bool ActivateAnimation(int index)
    {
      bool status=false;

      if(index < m_Header.Animations.Count)
      {
        m_ActiveAnimation = index;
        m_CurrentFrame = 0;
        AniTmr.Init(m_Animations[index].FrameCount);
        status = true;
      }

      return(status);
    }
    public void UpdateAnimationTimer()
    {
      if((m_ActiveAnimation < m_Header.Animations.Count)&&
         (m_ActiveAnimation != -1))
      {
        AniTmr.UpdateKeyframeInterpolation();

        if(m_CurrentFrame < m_Animations[m_ActiveAnimation].FrameCount)
          m_CurrentFrame++;
        else
          m_CurrentFrame = 0;
      }
    }
    public void GetAnimationNode(int node_index, ref float[] translation, ref float[] rotation)
    {
      if((m_ActiveAnimation < m_Header.Animations.Count)&&
         (m_ActiveAnimation != -1)&&
         (node_index < m_Header.Nodes.Count))
      {
        if(m_CurrentFrame < m_Animations[m_ActiveAnimation].FrameCount)
        {
          int k1 = AniTmr.m_PrevKeyFrame*m_Animations[m_ActiveAnimation].NodeCount + node_index;
          int k2 = AniTmr.m_NextKeyFrame*m_Animations[m_ActiveAnimation].NodeCount + node_index;
          float intr = AniTmr.m_Interp;

          translation[0] = m_Keyframes[m_ActiveAnimation][k1].translation[0] +
            intr*(m_Keyframes[m_ActiveAnimation][k2].translation[0]-
                  m_Keyframes[m_ActiveAnimation][k1].translation[0]);

          translation[1] = m_Keyframes[m_ActiveAnimation][k1].translation[1] +
            intr*(m_Keyframes[m_ActiveAnimation][k2].translation[1]-
            m_Keyframes[m_ActiveAnimation][k1].translation[1]);

          translation[2] = m_Keyframes[m_ActiveAnimation][k1].translation[2] +
            intr*(m_Keyframes[m_ActiveAnimation][k2].translation[2]-
            m_Keyframes[m_ActiveAnimation][k1].translation[2]);

          m_q1.Load(m_Keyframes[m_ActiveAnimation][k1].rotation);
          m_q1.conjugate();
          m_q2.Load(m_Keyframes[m_ActiveAnimation][k2].rotation);
          m_q2.conjugate();
          m_tween.slerp(ref m_q1, ref m_q2, intr);

          rotation[0] = m_tween.m_quat[0];
          rotation[1] = m_tween.m_quat[1];
          rotation[2] = m_tween.m_quat[2];
          rotation[3] = m_tween.m_quat[3];
        }
      }
    }
  }
}