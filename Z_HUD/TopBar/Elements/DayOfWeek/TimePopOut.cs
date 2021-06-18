// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_HUD.TopBar.Elements.DayOfWeek.TimePopOut
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.GenericUI;
using TinyZoo.Z_HUD.TopBar.MoralityPopUp;

namespace TinyZoo.Z_HUD.TopBar.Elements.DayOfWeek
{
  internal class TimePopOut : GenericTopBarPopOutFrame
  {
    private SimpleTextHandler text;

    public TimePopOut(float BaseScale)
      : base(BaseScale)
    {
      this.text = new SimpleTextHandler("Visitors will not enter the park after it is closed.", this.scaleHelper.ScaleX(150f), true, BaseScale, AutoComplete: true);
      this.text.SetAllColours(ColourData.Z_Cream);
      Vector2 vector2 = Vector2.Zero + this.scaleHelper.DefaultBuffer;
      this.text.Location.Y = vector2.Y;
      this.text.Location.Y += this.text.GetHeightOfOneLine() * 0.5f;
      Vector2 _frameSize = vector2 + this.text.GetSize(true) + this.scaleHelper.DefaultBuffer;
      this.FinalizeSize(_frameSize);
      this.text.Location.Y += (-_frameSize * 0.5f).Y;
    }

    public bool UpdateTimePopOut(Player player, float DeltaTime, Vector2 offset) => this.UpdatePopOutFrame(player, DeltaTime, ref offset);

    public void DrawTimePopOut(Vector2 offset, SpriteBatch spriteBatch)
    {
      this.DrawPopOutFrame(ref offset, spriteBatch);
      this.text.DrawSimpleTextHandler(offset, 1f, spriteBatch);
    }
  }
}
