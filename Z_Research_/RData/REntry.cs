// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Research_.RData.REntry
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using System.Collections.Generic;
using TinyZoo.Tile_Data;

namespace TinyZoo.Z_Research_.RData
{
  internal class REntry
  {
    public string Heading;
    public string Description;
    public int Cost;
    public UnlockTYPE unlocktype;
    public Rectangle IconRect;
    public List<TILETYPE> WillUnlockThese;
    public UpgradeCategory upgradeCategory;

    public REntry(
      UnlockTYPE _unlocktype,
      Rectangle _IconRect,
      string _Heading,
      string _Description,
      int _Cost,
      params TILETYPE[] values)
    {
      this.ConstructREntry(_unlocktype, _IconRect, _Heading, _Description, _Cost, UpgradeCategory.Count, values);
    }

    public REntry(
      UnlockTYPE _unlocktype,
      Rectangle _IconRect,
      string _Heading,
      string _Description,
      int _Cost,
      UpgradeCategory _upgradeCategory,
      params TILETYPE[] values)
    {
      this.ConstructREntry(_unlocktype, _IconRect, _Heading, _Description, _Cost, _upgradeCategory, values);
    }

    private void ConstructREntry(
      UnlockTYPE _unlocktype,
      Rectangle _IconRect,
      string _Heading,
      string _Description,
      int _Cost,
      UpgradeCategory _upgradeCategory,
      params TILETYPE[] values)
    {
      this.WillUnlockThese = new List<TILETYPE>();
      if (values != null)
      {
        for (int index = 0; index < values.Length; ++index)
          this.WillUnlockThese.Add(values[index]);
      }
      this.IconRect = _IconRect;
      this.unlocktype = _unlocktype;
      this.Cost = _Cost;
      this.Description = _Description;
      this.Heading = _Heading;
      this.upgradeCategory = _upgradeCategory;
    }
  }
}
