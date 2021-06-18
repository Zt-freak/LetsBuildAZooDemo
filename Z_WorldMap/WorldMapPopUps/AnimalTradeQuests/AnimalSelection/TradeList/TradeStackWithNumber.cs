// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_WorldMap.WorldMapPopUps.AnimalTradeQuests.AnimalSelection.TradeList.TradeStackWithNumber
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using TinyZoo.Z_Collection.Shared.Grid;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_WorldMap.WorldMapPopUps.AnimalTradeQuests.AnimalSelection.TradeList
{
  internal class TradeStackWithNumber
  {
    public Vector2 location;
    private List<AnimalInFrame> animalsInFrames;
    private ZGenericText number;

    public TradeStackWithNumber(float BaseScale, List<AnimalRenderDescriptor> animalsNeeded)
    {
      UIScaleHelper uiScaleHelper = new UIScaleHelper(BaseScale);
      float TargetSize = uiScaleHelper.ScaleX(25f);
      float FrameEdgeBuffer = uiScaleHelper.ScaleX(6f);
      float num1 = uiScaleHelper.ScaleX(5f);
      this.animalsInFrames = new List<AnimalInFrame>();
      for (int index = 0; index < animalsNeeded.Count; ++index)
      {
        AnimalInFrame animalInFrame = new AnimalInFrame(animalsNeeded[index].bodyAnimalType, animalsNeeded[index].headAnimalType, animalsNeeded[index].variant, TargetSize, FrameEdgeBuffer, BaseScale, animalsNeeded[index].headVariant);
        Vector2 size = animalInFrame.GetSize();
        animalInFrame.Location += size * 0.5f;
        animalInFrame.Location.X += num1 * (float) index;
        if (index < animalsNeeded.Count - 1)
        {
          float num2 = (float) ((double) index / (double) animalsNeeded.Count / 5.0 - 0.150000005960464);
          animalInFrame.SetFrameColour(Color.LightGray.ToVector3() + new Vector3(num2), true);
        }
        this.animalsInFrames.Add(animalInFrame);
      }
      this.number = new ZGenericText("x" + (object) animalsNeeded.Count, BaseScale, false, _UseOnePointFiveFont: true);
      this.number.vLocation.X = this.animalsInFrames[this.animalsInFrames.Count - 1].Location.X + this.animalsInFrames[this.animalsInFrames.Count - 1].GetSize().X * 0.5f + uiScaleHelper.GetDefaultXBuffer();
      this.number.vLocation.Y = this.animalsInFrames[this.animalsInFrames.Count - 1].Location.Y;
      this.number.vLocation.Y -= this.number.GetSize().Y * 0.5f;
    }

    public void UpdateTradeStackWithNumber()
    {
    }

    public void DrawTradeStackWithNumber(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      for (int index = 0; index < this.animalsInFrames.Count; ++index)
        this.animalsInFrames[index].DrawAnimalInFrame(offset, spriteBatch);
      this.number.DrawZGenericText(offset, spriteBatch);
    }
  }
}
