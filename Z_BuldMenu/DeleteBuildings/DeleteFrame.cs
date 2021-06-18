// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BuldMenu.DeleteBuildings.DeleteFrame
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.GenericUI;

namespace TinyZoo.Z_BuldMenu.DeleteBuildings
{
  internal class DeleteFrame
  {
    private GameObject Gameobj;
    private ScreenHeading header;

    public DeleteFrame()
    {
      this.Gameobj = new GameObject();
      this.Gameobj.SetAllColours(1f, 0.2f, 0.2f);
      this.header = new ScreenHeading("DESTROY", BaseScale: Z_GameFlags.GetBaseScaleForUI());
      this.Gameobj.DrawRect = TinyZoo.Game1.WhitePixelRect;
      this.Gameobj.SetDrawOriginToCentre();
    }

    public void UpdateDeleteFrame()
    {
    }

    public void DrawDeleteFrame()
    {
      this.header.DrawScreenHeading(Vector2.Zero, AssetContainer.pointspritebatchTop05);
      this.Gameobj.DrawRect = TinyZoo.Game1.WhitePixelRect;
      this.Gameobj.SetDrawOriginToCentre();
      this.Gameobj.Draw(AssetContainer.pointspritebatchTop05, AssetContainer.SpriteSheet, new Vector2(0.0f, 384f), new Vector2(8f, 768f));
      this.Gameobj.Draw(AssetContainer.pointspritebatchTop05, AssetContainer.SpriteSheet, new Vector2(1024f, 384f), new Vector2(8f, 768f));
      this.Gameobj.Draw(AssetContainer.pointspritebatchTop05, AssetContainer.SpriteSheet, new Vector2(512f, 0.0f), new Vector2(1024f, 8f));
      this.Gameobj.Draw(AssetContainer.pointspritebatchTop05, AssetContainer.SpriteSheet, new Vector2(512f, 768f), new Vector2(1024f, 8f));
    }
  }
}
