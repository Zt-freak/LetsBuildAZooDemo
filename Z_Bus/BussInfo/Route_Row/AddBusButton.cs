// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Bus.BussInfo.Route_Row.AddBusButton
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;

namespace TinyZoo.Z_Bus.BussInfo.Route_Row
{
  internal class AddBusButton
  {
    private TextButton addbutton;
    private float BaseScale;
    private string Heading;
    private GameObject HeadingOBJ;

    public AddBusButton(float _BaseScale, bool IsHeading, float LeftX)
    {
      if (IsHeading)
      {
        this.Heading = "EDIT";
        this.HeadingOBJ = new GameObject();
        this.HeadingOBJ.scale = this.BaseScale;
        this.HeadingOBJ.SetAllColours(ColourData.Z_Cream);
      }
      this.BaseScale = _BaseScale;
      this.addbutton = new TextButton(this.BaseScale, "Add", 20f);
      this.addbutton.vLocation.X = LeftX + 20f * this.BaseScale;
    }

    public float GetWidth() => this.BaseScale * 50f;

    public bool UpdateAddBusButton(Vector2 Offset, Player player, float DeltaTime) => this.HeadingOBJ == null && this.addbutton.UpdateTextButton(player, Offset, DeltaTime);

    public void DrawAddBusButton(Vector2 Offset, SpriteBatch spritebatch)
    {
    }
  }
}
