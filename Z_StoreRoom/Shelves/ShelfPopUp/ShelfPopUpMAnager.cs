// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_StoreRoom.Shelves.ShelfPopUp.ShelfPopUpMAnager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using System;
using TinyZoo.GenericUI;
using TinyZoo.PlayerDir.StoreRooms;

namespace TinyZoo.Z_StoreRoom.Shelves.ShelfPopUp
{
  internal class ShelfPopUpMAnager
  {
    private GameObjectNineSlice frame;
    private GameObjectNineSlice InnerFrameHole;
    private BackButton Close;
    private SimpleTextHandler texticle;
    private SimpleTextHandler texticle2;
    public Vector2 Location;
    public Vector2 VScale;
    public Vector2 InnerFrameHoleVScale;
    private TrashButton trashbutton;
    private LerpHandler_Float lerper;
    private LerpHandler_Float AlphaLerper;
    private string StockTypeName;

    public ShelfPopUpMAnager(StockEntry RefSTockEntry, Player player)
    {
      this.lerper = new LerpHandler_Float();
      this.lerper.SetLerp(true, 0.0f, 1f, 3f);
      this.AlphaLerper = new LerpHandler_Float();
      this.AlphaLerper.SetLerp(true, 0.0f, 1f, 3f);
      this.Close = new BackButton(true);
      this.frame = new GameObjectNineSlice(new Rectangle(992, 600, 21, 21), 7);
      this.frame.scale = 2f;
      this.VScale = new Vector2(400f, 250f * Sengine.ScreenRatioUpwardsMultiplier.Y);
      this.InnerFrameHole = new GameObjectNineSlice(new Rectangle(885, 590, 21, 21), 7);
      this.InnerFrameHole.scale = 2f;
      this.InnerFrameHoleVScale = new Vector2(250f, 180f * Sengine.ScreenRatioUpwardsMultiplier.Y);
      this.InnerFrameHole.vLocation = new Vector2(-60f, 20f);
      this.Close.vLocation = new Vector2((float) ((double) this.VScale.X * 0.5 - 15.0), (float) ((double) this.VScale.Y * -0.5 + 15.0));
      this.Location = new Vector2(800f, 250f * Sengine.ScreenRatioUpwardsMultiplier.Y);
      string TextToWrite1 = "Units:~Expiry:~Value:~~Consumption Yesterday:~~Scheduled Orders:";
      string TextToWrite2;
      if (RefSTockEntry != null)
      {
        int totalOfTheseOnOrder = player.storerooms.GetTotalOfTheseOnOrder(RefSTockEntry.foodtype);
        string str = "No";
        if (totalOfTheseOnOrder > 0)
          str = string.Concat((object) totalOfTheseOnOrder);
        TextToWrite2 = "           " + (object) Math.Round((double) RefSTockEntry.GetTotalStock(), 1) + "~           " + (object) RefSTockEntry.ShelfLifeRemaining + " days~           $" + (object) (float) ((double) AnimalFoodData.GetAnimalFoodInfo(RefSTockEntry.foodtype).Cost * (double) RefSTockEntry.GetTotalStock()) + "~~~" + (object) Math.Round((double) player.storerooms.YesterdaysUse[(int) RefSTockEntry.foodtype], 2) + " units~~" + str;
        this.StockTypeName = AnimalFoodData.GetAnimalFoodTypeToString(RefSTockEntry.foodtype);
      }
      else
      {
        TextToWrite1 = " ";
        TextToWrite2 = "Order Stock for~your warehouse";
      }
      this.texticle = new SimpleTextHandler(TextToWrite1, false, 0.5f, 2f, false, false);
      this.texticle2 = new SimpleTextHandler(TextToWrite2, false, 0.5f, 2f, false, false);
      this.texticle.paragraph.linemaker.SetAllColours(ColourData.Z_CreamFADED);
      this.texticle2.paragraph.linemaker.SetAllColours(ColourData.Z_Cream);
      this.texticle2.AutoCompleteParagraph();
      this.texticle.AutoCompleteParagraph();
      this.trashbutton = new TrashButton();
      this.trashbutton.vLocation = new Vector2(170f, 90f);
    }

    public bool UpdaeShelfPopUpMAnager(Vector2 Offset, Player player, float DeltaTime)
    {
      this.lerper.UpdateLerpHandler(DeltaTime);
      Offset += this.Location;
      if (!this.lerper.IsComplete())
        return false;
      this.AlphaLerper.UpdateLerpHandler(DeltaTime);
      this.trashbutton.UpdateTrashButton(Offset, player);
      return this.Close.UpdateBackButton(player, DeltaTime);
    }

    public void DrawShelfPopUpMAnager(Vector2 Offset, SpriteBatch spritebatch)
    {
      Offset += this.Location;
      this.frame.DrawGameObjectNineSlice(spritebatch, AssetContainer.SpriteSheet, Offset, this.VScale * this.lerper.Value);
      this.InnerFrameHole.fAlpha = this.lerper.Value;
      this.InnerFrameHole.DrawGameObjectNineSlice(spritebatch, AssetContainer.SpriteSheet, Offset, this.InnerFrameHoleVScale * this.lerper.Value);
      TextFunctions.DrawTextWithDropShadow(this.StockTypeName, 1f, Offset + new Vector2(-180f, -107f * Sengine.ScreenRatioUpwardsMultiplier.Y) * this.lerper.Value, new Color(ColourData.Z_Cream), this.lerper.Value, AssetContainer.roundaboutFont, spritebatch, false);
      if (!this.lerper.IsComplete())
        return;
      this.texticle.DrawSimpleTextHandler(Offset + new Vector2(-170f, -60f), this.AlphaLerper.Value, spritebatch);
      this.texticle2.DrawSimpleTextHandler(Offset + new Vector2(-170f, -60f), this.AlphaLerper.Value, spritebatch);
      this.trashbutton.fAlpha = this.AlphaLerper.Value;
      this.trashbutton.DrawTrashButton(Offset, spritebatch);
      this.Close.fAlpha = this.AlphaLerper.Value;
      this.Close.vLocation = Offset + new Vector2((float) ((double) this.VScale.X * 0.5 - 25.0), (float) ((double) this.VScale.Y * -0.5 + 25.0 * (double) Sengine.ScreenRatioUpwardsMultiplier.Y));
      this.Close.DrawBackButton(Vector2.Zero);
    }
  }
}
