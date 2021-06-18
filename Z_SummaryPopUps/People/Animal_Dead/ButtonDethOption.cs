// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.Animal_Dead.ButtonDethOption
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.GenericUI;

namespace TinyZoo.Z_SummaryPopUps.People.Animal_Dead
{
  internal class ButtonDethOption : GameObject
  {
    private TextButton textbutton;
    public Vector2 Location;
    private string TextDesc;
    private SimpleTextHandler text;

    public ButtonDethOption(string ButtonName, string Description)
    {
      this.TextDesc = Description;
      this.textbutton = new TextButton(ButtonName, 40f, OverAllMultiplier: 0.5f);
      this.textbutton.stringinabox.Frame.scale = 1f;
      this.textbutton.vLocation.X = -100f;
      this.SetAllColours(ColourData.Z_Cream);
      this.text = new SimpleTextHandler(Description, false, 0.2f, RenderMath.GetPixelSizeBestMatch(1f), false, false);
      this.text.AutoCompleteParagraph();
      this.text.paragraph.linemaker.SetAllColours(ColourData.Z_Cream);
      this.text.Location.X = -70f;
      this.text.Location.Y = -10f;
    }

    public bool UpdateButtonDethOption(Vector2 Offset, Player player, float DeltaTime)
    {
      Offset += this.Location;
      return this.textbutton.UpdateTextButton(player, Offset, DeltaTime);
    }

    public void DrawButtonDethOption(Vector2 Offset)
    {
      this.vLocation.X = -50f;
      Offset += this.Location;
      this.textbutton.DrawTextButton(Offset, 1f, AssetContainer.pointspritebatchTop05);
      this.text.DrawSimpleTextHandler(Offset, 1f, AssetContainer.pointspritebatchTop05);
    }
  }
}
