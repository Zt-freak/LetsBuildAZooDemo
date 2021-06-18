// Decompiled with JetBrains decompiler
// Type: TinyZoo.Utils.DeveloperMenu.PeopleAdder
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.GenericUI;
using TinyZoo.Z_Manage.Hiring.Interview.Negotiation.MakeOffer;

namespace TinyZoo.Utils.DeveloperMenu
{
  internal class PeopleAdder
  {
    private BackButton close;
    private PriceAdjuster priceadjust;
    private AnimalType SelectedAnimal;
    private TextButton Go;
    private TextButton RandomVariants;
    private bool Variants;
    private string TEXT;

    public PeopleAdder()
    {
      this.close = new BackButton(true, BaseScale: RenderMath.GetPixelZoomOneToOne());
      this.priceadjust = new PriceAdjuster(0, 500, 0);
      this.priceadjust.Location = new Vector2(200f, 700f);
      this.SelectedAnimal = AnimalType.None;
      this.Go = new TextButton(RenderMath.GetPixelZoomOneToOne(), "MAKE ANIMALS", 100f);
      this.Go.vLocation = new Vector2(900f, 700f);
      this.RandomVariants = new TextButton(RenderMath.GetPixelZoomOneToOne(), "Random Variants- Yes!", 200f);
      this.RandomVariants.vLocation = new Vector2(600f, 700f);
      this.Variants = true;
      this.TEXT = "MAKE MANY: Random Customers!";
    }

    public bool UpdatePeopleAdder(Player player, float DeltaTime)
    {
      AnimalType animalType = AnimalType.None;
      if (animalType != AnimalType.None)
      {
        this.SelectedAnimal = animalType;
        this.TEXT = "MAKE MANY: Random Customers!";
      }
      this.priceadjust.UpdatePriceAdjuster(player, Vector2.Zero, DeltaTime);
      if (this.priceadjust.CurrentValue > 0)
      {
        if (this.RandomVariants.UpdateTextButton(player, Vector2.Zero, DeltaTime))
        {
          this.Variants = !this.Variants;
          if (this.Variants)
            this.RandomVariants.SetText("Random Variants- Yes!");
          else
            this.RandomVariants.SetText("Random Variants- No!");
        }
        if (this.Go.UpdateTextButton(player, Vector2.Zero, DeltaTime))
        {
          DebugFunctions.AutoSpwnPeople += this.priceadjust.CurrentValue;
          Z_DebugFlags.SimulationIsVerbose = false;
        }
      }
      return this.close.UpdateBackButton(player, DeltaTime);
    }

    public void DrawPeopleAdder()
    {
      this.close.DrawBackButton(Vector2.Zero, AssetContainer.pointspritebatch07Final);
      this.priceadjust.DrawPriceAdjuster(Vector2.Zero, AssetContainer.pointspritebatch07Final);
      TextFunctions.DrawJustifiedText(this.TEXT, 1f, new Vector2(512f, 600f), Color.White, 1f, AssetContainer.SpringFontX1AndHalf, AssetContainer.pointspritebatch07Final);
      if (this.priceadjust.CurrentValue <= 0)
        return;
      this.Go.DrawTextButton(Vector2.Zero, 1f, AssetContainer.pointspritebatch07Final);
    }
  }
}
