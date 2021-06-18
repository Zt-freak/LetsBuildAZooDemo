// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Manage.MainButtons.ButtonExtras.ButtonProgressBar
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using System.Collections.Generic;
using TinyZoo.GenericUI;

namespace TinyZoo.Z_Manage.MainButtons.ButtonExtras
{
  internal class ButtonProgressBar : GameObject
  {
    private List<ProgressPip> progresspips;
    private string WriteThis;
    public Vector2 Location;

    public ButtonProgressBar(string _WriteThis)
    {
      this.WriteThis = _WriteThis;
      this.progresspips = new List<ProgressPip>();
      for (int index = 0; index < 5; ++index)
      {
        this.progresspips.Add(new ProgressPip());
        this.progresspips[index].vLocation.X = (float) (index * 30);
      }
      Vector3 SecondaryColour;
      StringInBox.GetFrameColourRect(BTNColour.Cream, out SecondaryColour);
      this.SetAllColours(SecondaryColour);
    }

    public void SetFullNess(float PercZeroToOne)
    {
      PercZeroToOne *= 5f;
      for (int index = 0; index < 5; ++index)
        this.progresspips[index].SetFullness(PercZeroToOne - (float) index);
    }

    public void UpdateButtonProgressBar()
    {
    }

    public void DrawButtonProgressBar(Vector2 Offset)
    {
      Offset += new Vector2(0.0f, -40f);
      TextFunctions.DrawJustifiedText(this.WriteThis, 3f, Offset + this.Location + new Vector2(-25f, 0.0f), this.GetColour(), 1f, AssetContainer.springFont, AssetContainer.pointspritebatchTop05);
      for (int index = 0; index < 5; ++index)
        this.progresspips[index].DrawProgressPip(Offset + this.Location + new Vector2(30f, -3f));
    }
  }
}
