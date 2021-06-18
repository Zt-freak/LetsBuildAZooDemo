// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_StoreRoom.Ani_MAll.History.HistoryManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;

namespace TinyZoo.Z_StoreRoom.Ani_MAll.History
{
  internal class HistoryManager
  {
    public void UpdateHistoryManager()
    {
    }

    public void DrawHistoryManager() => TextFunctions.DrawJustifiedText("THINGS YOU HAVE PURCHASED BEFORE>>>>", 2f, new Vector2(400f, 300f), Color.Black, 1f, AssetContainer.springFont, AssetContainer.pointspritebatch03);
  }
}
