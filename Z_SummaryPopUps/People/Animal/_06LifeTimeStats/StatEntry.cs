// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.Animal._06LifeTimeStats.StatEntry
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_SummaryPopUps.People.Animal._06LifeTimeStats
{
  internal class StatEntry
  {
    public Vector2 location;
    private CustomerFrame customerframe;
    private ZGenericText text;
    private ZGenericText value;

    public StatEntry(
      float width,
      AnimalStatType animalstattype,
      PrisonerInfo animal,
      float BaseScale)
    {
      UIScaleHelper uiScaleHelper = new UIScaleHelper(BaseScale);
      Vector2 defaultBuffer = uiScaleHelper.DefaultBuffer;
      defaultBuffer.Y *= 0.5f;
      this.text = new ZGenericText(StatEntry.GetAnimalStatTypeToString(animalstattype), BaseScale, false, _UseOnePointFiveFont: true);
      this.value = new ZGenericText(0.ToString(), BaseScale, _UseOnePointFiveFont: true);
      this.text.vLocation.X = defaultBuffer.X;
      this.text.vLocation.Y -= this.text.GetSize().Y * 0.5f;
      this.value.vLocation.X = width - defaultBuffer.X - uiScaleHelper.ScaleX(25f);
      this.customerframe = new CustomerFrame(new Vector2(width, this.text.GetSize().Y + defaultBuffer.Y * 2f), CustomerFrameColors.Brown, BaseScale);
      Vector2 vector2 = -this.customerframe.VSCale * 0.5f;
      this.text.vLocation.X += vector2.X;
      this.value.vLocation.X += vector2.X;
    }

    public Vector2 GetSize() => this.customerframe.VSCale;

    internal static string GetAnimalStatTypeToString(AnimalStatType animalstat)
    {
      switch (animalstat)
      {
        case AnimalStatType.Escapes:
          return "Escapes";
        case AnimalStatType.Fights:
          return "Fights";
        case AnimalStatType.Prignancies:
          return "Pregnancies";
        case AnimalStatType.Infections:
          return "Infections";
        case AnimalStatType.VistitorIncidents:
          return "Visitor Incidents";
        default:
          throw new Exception("iosdfs");
      }
    }

    public void UpdateCustomerFrame()
    {
    }

    public void DrawCustomerFrame(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.customerframe.DrawCustomerFrame(offset, spriteBatch);
      this.text.DrawZGenericText(offset, spriteBatch);
      this.value.DrawZGenericText(offset, spriteBatch);
    }
  }
}
