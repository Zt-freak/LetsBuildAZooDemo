// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Bus.BussInfo.Route_Row.RouteName
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.GenericUI;
using TinyZoo.PlayerDir.BusTimetable;

namespace TinyZoo.Z_Bus.BussInfo.Route_Row
{
  internal class RouteName : GameObject
  {
    public Vector2 Location;
    private string Name;
    private float Alphaa = 1f;
    private SimpleTextHandler simpletext;

    public RouteName(BUSROUTE busroute, float BaseScale, bool IsLocked)
    {
      this.Name = busroute != BUSROUTE.Count ? (!IsLocked ? BusTimes.GetBusRouteName(busroute) : "This Route is locked~Unlock new routes by completing tasks for the City Planner.") : "BUS ROUTE";
      this.scale = BaseScale;
      this.vLocation.X = 50f * BaseScale;
      this.simpletext = new SimpleTextHandler(this.Name, !IsLocked, 0.5f, BaseScale, busroute != BUSROUTE.Count);
      this.simpletext.AutoCompleteParagraph();
      this.simpletext.SetAllColours(ColourData.Z_Cream);
      if (busroute != BUSROUTE.Count)
        this.vLocation.Y = this.simpletext.GetHeightOfOneLine() * -0.5f;
      if (!IsLocked)
        return;
      this.vLocation.Y *= 2f;
      this.Alphaa = 0.5f;
    }

    public float GetWidth() => this.vLocation.X * 2f;

    public void DrawRouteName(SpriteBatch spritebatch, Vector2 Offset)
    {
      Offset += this.vLocation + this.Location;
      this.simpletext.DrawSimpleTextHandler(Offset, this.Alphaa, spritebatch);
    }
  }
}
