using System.IO;
using System.Diagnostics;
using TagLibrary.Types;

namespace TagLibrary.Halo1
{
  public class MaterialEffects : IBlock
  {
    public MaterialEffectsBlock MaterialEffectsValues = new MaterialEffectsBlock();
    public void Read(BinaryReader reader)
    {
      Trace.WriteLine("Loading 'MaterialEffects'------------------------------------------------------");
      MaterialEffectsValues.Read(reader);
    }
    public void ReadChildData(BinaryReader reader)
    {
      MaterialEffectsValues.ReadChildData(reader);
    }
    public void Write(BinaryWriter writer)
    {
      MaterialEffectsValues.Write(writer);
    }
    public void WriteChildData(BinaryWriter writer)
    {
      MaterialEffectsValues.WriteChildData(writer);
    }
public class MaterialEffectsBlock : IBlock
{
private Block _effects = new Block();
private Pad  __unnamed;	
public class MaterialEffectBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public MaterialEffectBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(MaterialEffectBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new MaterialEffectBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public MaterialEffectBlock this[int index]
  {
    get { return (InnerList[index] as MaterialEffectBlock); }
  }
}
private MaterialEffectBlockCollection _effectsCollection;
public MaterialEffectBlockCollection Effects
{
  get { return _effectsCollection; }
}
public MaterialEffectsBlock()
{
__unnamed = new Pad(128);
_effectsCollection = new MaterialEffectBlockCollection(_effects);

}
public void Read(BinaryReader reader)
{
  _effects.Read(reader);
  __unnamed.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
for (int x=0; x<_effects.Count; x++)
{
  Effects.AddNew();
  Effects[x].Read(reader);
}
for (int x=0; x<_effects.Count; x++)
  Effects[x].ReadChildData(reader);
}
public void Write(BinaryWriter writer)
{
    _effects.Write(writer);
    __unnamed.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_effects.UpdateReflexiveOffset(writer);
for (int x=0; x<_effects.Count; x++)
{
  Effects[x].Write(writer);
}
for (int x=0; x<_effects.Count; x++)
  Effects[x].WriteChildData(writer);
}
}
public class MaterialEffectBlock : IBlock
{
private Block _materials = new Block();
private Pad  __unnamed;	
public class MaterialEffectMaterialBlockCollection : System.Collections.CollectionBase
{
  private Block linkedBlock;
  public MaterialEffectMaterialBlockCollection(Block linkedBlock)
  {
    this.linkedBlock = linkedBlock;
  }
  public void Add(MaterialEffectMaterialBlock block)
  {
    InnerList.Add(block);
    if (linkedBlock.Count < InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public void AddNew()
  {
    Add(new MaterialEffectMaterialBlock());
  }
  public void Remove(int index)
  {
    InnerList.RemoveAt(index);
    if (linkedBlock.Count > InnerList.Count) linkedBlock.Count = InnerList.Count;
  }
  public MaterialEffectMaterialBlock this[int index]
  {
    get { return (InnerList[index] as MaterialEffectMaterialBlock); }
  }
}
private MaterialEffectMaterialBlockCollection _materialsCollection;
public MaterialEffectMaterialBlockCollection Materials
{
  get { return _materialsCollection; }
}
public MaterialEffectBlock()
{
__unnamed = new Pad(16);
_materialsCollection = new MaterialEffectMaterialBlockCollection(_materials);

}
public void Read(BinaryReader reader)
{
  _materials.Read(reader);
  __unnamed.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
for (int x=0; x<_materials.Count; x++)
{
  Materials.AddNew();
  Materials[x].Read(reader);
}
for (int x=0; x<_materials.Count; x++)
  Materials[x].ReadChildData(reader);
}
public void Write(BinaryWriter writer)
{
    _materials.Write(writer);
    __unnamed.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_materials.UpdateReflexiveOffset(writer);
for (int x=0; x<_materials.Count; x++)
{
  Materials[x].Write(writer);
}
for (int x=0; x<_materials.Count; x++)
  Materials[x].WriteChildData(writer);
}
}
public class MaterialEffectMaterialBlock : IBlock
{
private TagReference _effect = new TagReference();
private TagReference _sound = new TagReference();
private Pad  __unnamed;	
public TagReference Effect
{
  get { return _effect; }
  set { _effect = value; }
}
public TagReference Sound
{
  get { return _sound; }
  set { _sound = value; }
}
public MaterialEffectMaterialBlock()
{
__unnamed = new Pad(16);

}
public void Read(BinaryReader reader)
{
  _effect.Read(reader);
  _sound.Read(reader);
  __unnamed.Read(reader);
}
public void ReadChildData(BinaryReader reader)
{
_effect.ReadString(reader);
_sound.ReadString(reader);
}
public void Write(BinaryWriter writer)
{
    _effect.Write(writer);
    _sound.Write(writer);
    __unnamed.Write(writer);
}
public void WriteChildData(BinaryWriter writer)
{
_effect.WriteString(writer);
_sound.WriteString(writer);
}
}
  }
}
