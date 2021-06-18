// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Collection.Shared.Grid.AnimalRenderDescriptor
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using TinyZoo.GamePlay.Enemies;
using TinyZoo.PlayerDir.Farms_;

namespace TinyZoo.Z_Collection.Shared.Grid
{
  internal class AnimalRenderDescriptor
  {
    public AnimalType bodyAnimalType;
    public AnimalType headAnimalType;
    public CROPTYPE cropType;
    public int variant;
    public int headVariant;
    public bool IsUnlocked;
    public bool IsAvailable;
    public bool IsFemale;

    public AnimalRenderDescriptor(
      AnimalType _bodyAnimalType,
      int _variant = 0,
      AnimalType _headAnimalType = AnimalType.None,
      int _headVariant = 0,
      bool _IsUnlocked = true,
      bool _IsAvailable = true,
      bool _IsFemale = false,
      CROPTYPE _cropType = CROPTYPE.Count)
    {
      this.bodyAnimalType = _bodyAnimalType;
      this.headAnimalType = _headAnimalType;
      this.variant = _variant;
      this.headVariant = _headVariant;
      this.IsUnlocked = _IsUnlocked;
      this.IsAvailable = _IsAvailable;
      this.IsFemale = _IsFemale;
      this.cropType = _cropType;
    }

    public void RandomCreate(bool IncludeHybrid)
    {
      this.bodyAnimalType = (AnimalType) Game1.Rnd.Next(0, 56);
      this.variant = Game1.Rnd.Next(0, 10);
      if (IncludeHybrid)
      {
        this.headAnimalType = (AnimalType) Game1.Rnd.Next(0, 56);
        this.headVariant = Game1.Rnd.Next(0, 10);
      }
      this.IsFemale = Game1.Rnd.Next(0, 2) == 0;
    }
  }
}
