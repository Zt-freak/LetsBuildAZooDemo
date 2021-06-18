// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_StoreRoom.Ani_MAll.CheckOut.ThingToBuy
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.Z_StoreRoom.Shelves;

namespace TinyZoo.Z_StoreRoom.Ani_MAll.CheckOut
{
  internal class ThingToBuy
  {
    private GameObjectNineSlice Frame;
    private Vector2 VSCale;
    private AnimalFoodIcon animalfoodicon;
    public Vector2 Location;
    private string BUYTHISMANY;
    public int COST;
    private TrashButton trashbutton;
    public AnimalFoodType animalfoodtype;
    public bool WasRemove;
    private LerpHandler_Float lerper;

    public ThingToBuy(Animall_Entry animalentry, int Index)
    {
      this.lerper = new LerpHandler_Float();
      this.lerper.SetLerp(true, (float) Index, (float) Index, 3f);
      this.animalfoodtype = animalentry.animalFoodType;
      this.Frame = new GameObjectNineSlice(new Rectangle(873, 282, 21, 21), 7);
      this.Frame.scale = 2f;
      this.animalfoodicon = new AnimalFoodIcon(animalentry.animalFoodType, 2f);
      this.VSCale = new Vector2(700f, 80f);
      this.animalfoodicon.vLocation = new Vector2(-270f, 0.0f);
      this.animalfoodicon.vLocation.Y = -10f;
      this.Location.X = 360f;
      this.BUYTHISMANY = animalentry.stocknumber.Value.ToString() + " @ $" + (object) animalentry.CostPerUnit;
      this.COST = animalentry.CostPerUnit * animalentry.stocknumber.Value;
      this.trashbutton = new TrashButton();
      this.trashbutton.vLocation = new Vector2(200f, 0.0f);
    }

    public void UpdateLocation(float DeltaTime, int Index)
    {
      if ((double) this.lerper.TargetValue != (double) Index)
        this.lerper.SetLerp(false, (float) Index, (float) Index, 3f, true);
      this.lerper.UpdateLerpHandler(DeltaTime);
    }

    public bool UpdareThingToBuy(Vector2 Offset, Player player, float DeltaTime)
    {
      Offset += this.Location;
      Offset.Y += this.lerper.Value * 90f * Sengine.ScreenRatioUpwardsMultiplier.Y;
      this.animalfoodicon.UpdateStringScroll(DeltaTime);
      if (!this.trashbutton.UpdateTrashButton(Offset, player))
        return false;
      this.WasRemove = true;
      return true;
    }

    public void DrawThingToBuy(Vector2 Offset)
    {
      Offset.Y += this.lerper.Value * 90f * Sengine.ScreenRatioUpwardsMultiplier.Y;
      Offset += this.Location;
      this.Frame.DrawGameObjectNineSlice(AssetContainer.pointspritebatch03, AssetContainer.SpriteSheet, Offset, this.VSCale * Sengine.ScreenRatioUpwardsMultiplier);
      this.animalfoodicon.DrawAnimalFoodIcon(Offset, true);
      TextFunctions.DrawJustifiedText(this.BUYTHISMANY, 2f, Offset + new Vector2(-50f, -20f), new Color(ColourData.Z_DarkTextGray), 1f, AssetContainer.springFont, AssetContainer.pointspritebatch03);
      TextFunctions.DrawJustifiedText("$" + (object) this.COST, 3f, Offset + new Vector2(-50f, 10f), new Color(0.8392157f, 0.3568628f, 0.2980392f), 1f, AssetContainer.springFont, AssetContainer.pointspritebatch03);
      this.trashbutton.DrawTrashButton(Offset, AssetContainer.pointspritebatch03);
    }
  }
}
