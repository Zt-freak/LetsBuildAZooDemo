// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.BlackMarket.PersonAndTip
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.GenericUI;
using TinyZoo.OverWorld.OverWorldEnv.Customers;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Animal;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_SummaryPopUps.People.BlackMarket
{
  internal class PersonAndTip
  {
    private AnimalInFrame animalinframe;
    public CustomerFrame frame;
    private SimpleTextHandler text;
    public Vector2 Location;

    public PersonAndTip(WalkingPerson person, Vector2 MainFrameScale)
    {
      float TargetSize = 50f;
      this.animalinframe = new AnimalInFrame(person.thispersontype, AnimalType.None, TargetSize: TargetSize);
      this.frame = new CustomerFrame(new Vector2(MainFrameScale.X - AnimalPopUpManager.Space, TargetSize * Sengine.ScreenRatioUpwardsMultiplier.Y + AnimalPopUpManager.VerticalBuffer));
      this.frame.frame.SetAllColours(0.0f, 0.0f, 0.0f);
      float PercentagePfScreenWidth = (float) (((double) this.frame.VSCale.X - ((double) TargetSize + (double) AnimalPopUpManager.VerticalBuffer * 1.5)) / 1024.0);
      this.text = new SimpleTextHandler(PersonAndTip.GetText(), false, PercentagePfScreenWidth, RenderMath.GetPixelSizeBestMatch(1f), false, false);
      this.text.paragraph.linemaker.SetAllColours(ColourData.BlackMarketPaleBlue);
      this.text.Location.X = this.frame.VSCale.X * -0.5f;
      this.text.Location.X += AnimalPopUpManager.VerticalBuffer;
      this.text.Location.X += TargetSize;
      this.text.Location.Y = this.frame.VSCale.Y * -0.5f;
      this.text.Location.Y += AnimalPopUpManager.VerticalBuffer;
      this.text.AutoCompleteParagraph();
      this.animalinframe.Location.X = this.frame.VSCale.X * -0.5f;
      this.animalinframe.Location.X += (float) ((double) TargetSize * 0.5 + (double) AnimalPopUpManager.VerticalBuffer * 0.5);
    }

    private static string GetText()
    {
      switch (TinyZoo.Game1.Rnd.Next(0, 3))
      {
        case 0:
          return "You want to get something special!?~I have something really interesting, just don't ask me where I got it!";
        case 1:
          return "Pssst, I think this shabby looking zoo could use some more animal variety!";
        default:
          return "You wont get anywhere in this world by following thre rules, my sources have given me something that is sure to help you turn a profit!";
      }
    }

    public void DrawPersonAndTip(Vector2 Offset)
    {
      Offset += this.Location;
      this.frame.DrawCustomerFrame(Offset, AssetContainer.pointspritebatchTop05);
      this.animalinframe.DrawAnimalInFrame(Offset);
      this.text.DrawSimpleTextHandler(Offset, 1f, AssetContainer.pointspritebatchTop05);
    }
  }
}
