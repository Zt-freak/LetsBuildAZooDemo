// Decompiled with JetBrains decompiler
// Type: TinyZoo.OverWorld.OverWorldBuildMenu.BuildSystem.ThingToBuild.BuildMessageType
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

namespace TinyZoo.OverWorld.OverWorldBuildMenu.BuildSystem.ThingToBuild
{
  internal enum BuildMessageType
  {
    None,
    PlaceNextToExistingWall,
    Overlapping,
    CanBuild,
    CanBuild_ButNoMoney,
    TooSmall,
    TooBig,
    MakeTaller,
    MakeWider,
    Count,
  }
}
