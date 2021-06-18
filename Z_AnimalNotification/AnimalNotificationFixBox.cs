// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_AnimalNotification.AnimalNotificationFixBox
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using System;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.GenericUI;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.Tile_Data;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_Manage.MainButtons;
using TinyZoo.Z_PenInfo.MainBar;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_AnimalNotification
{
  internal class AnimalNotificationFixBox
  {
    private static float descriptionScreenWidthFraction = 0.16f;
    private SimpleBuildingRenderer building;
    private Vector2 buildingsize;
    private AnimalInFrame portrait;
    private CustomerFrame frame;
    private float basescale;
    public Vector2 location;
    private UIScaleHelper uiScale;
    private AnimalDietIcons dietIcons;
    private TextButton fixbutton;
    private SimpleTextHandler description;
    private string descriptionStr = "";
    private Vector2 textSize;
    private Vector2 currRefLoc = Vector2.Zero;
    private LabelledBar labelledbar;
    private SpecialIcon specialIcon;
    private AnimalNotificationType notificationType;
    private int reason;
    private Vector2 fixboxSize;
    private Vector2 buildingloc;

    public AnimalNotificationFixBox(
      AnimalNotificationType notificationType_,
      PrisonerInfo info,
      Vector2 panelsize,
      float basescale_,
      Player player,
      int reason_ = -1)
    {
      this.notificationType = notificationType_;
      this.reason = reason_;
      this.basescale = basescale_;
      this.uiScale = new UIScaleHelper(basescale_);
      float TargetSize = 50f * this.basescale;
      this.textSize = this.uiScale.ScaleVector2(AssetContainer.SpringFontX1AndHalf.MeasureString("some arbitrary string!!"));
      float num1 = 2f * this.textSize.Y;
      float num2 = this.uiScale.ScaleY(10f);
      this.fixboxSize.Y = num2;
      switch (this.reason)
      {
        case 1:
          this.building = new SimpleBuildingRenderer(TILETYPE.StoreRoom);
          this.buildingsize = this.uiScale.ScaleVector2(new Vector2(60f, 60f));
          this.building.SetSize(60f, 99f);
          this.descriptionStr = "You need to build a Store Room to store food for the animals";
          break;
        case 2:
          this.portrait = new AnimalInFrame(AnimalType.FemaleZookeeper, AnimalType.None, TargetSize: TargetSize, BaseScale: (2f * this.basescale));
          this.descriptionStr = "There are no zookeepers that can feed this animal.";
          this.portrait.SetAnimalGreyedOut(true);
          this.fixboxSize.Y += this.portrait.GetSize().Y + num2;
          break;
        case 3:
          this.dietIcons = new AnimalDietIcons(info.intakeperson.animaltype, this.basescale, player);
          this.descriptionStr = "You have run out of ingredients suitable for this animal.";
          this.fixboxSize.Y += this.dietIcons.GetSize().Y + num2;
          break;
        case 4:
          this.portrait = new AnimalInFrame(AnimalType.FemaleZookeeper, AnimalType.None, TargetSize: TargetSize, BaseScale: (2f * this.basescale));
          this.descriptionStr = "Your animal is hungry due to a logistical issue.~It could be a navigation issue, a zoning issue or you simply just need to wait.";
          this.fixboxSize.Y += this.portrait.GetSize().Y + num2;
          break;
      }
      this.fixbutton = new TextButton(basescale_, "Fix");
      this.description = new SimpleTextHandler(this.descriptionStr, false, AnimalNotificationFixBox.descriptionScreenWidthFraction * this.basescale, this.basescale, false, false);
      this.fixboxSize.Y += (float) ((double) this.fixbutton.GetSize_True().Y + (double) num1 + 2.0 * (double) num2);
      this.fixboxSize = new Vector2(panelsize.X, Math.Max(panelsize.Y, this.fixboxSize.Y));
      this.frame = new CustomerFrame(this.fixboxSize, BaseScale: basescale_);
      this.currRefLoc.Y = -0.5f * this.fixboxSize.Y + num2;
      switch (this.reason)
      {
        case 1:
          this.buildingloc = this.currRefLoc;
          this.buildingloc.Y += 0.5f * this.buildingsize.Y;
          this.currRefLoc.Y += this.buildingsize.Y + num2;
          break;
        case 2:
          this.portrait.Location.Y = this.currRefLoc.Y + 0.5f * TargetSize * Sengine.ScreenRatioUpwardsMultiplier.Y;
          this.currRefLoc.Y += this.portrait.GetSize().Y + num2;
          break;
        case 3:
          this.dietIcons.location = this.currRefLoc;
          this.dietIcons.location.Y += 0.5f * this.dietIcons.GetSize().Y;
          this.currRefLoc.Y += this.dietIcons.GetSize().Y + num2;
          break;
        case 4:
          this.portrait.Location.Y = this.currRefLoc.Y + 0.5f * TargetSize * Sengine.ScreenRatioUpwardsMultiplier.Y;
          this.currRefLoc.Y += this.portrait.GetSize().Y + num2;
          break;
      }
      this.description.Location = this.currRefLoc;
      this.description.Location.X = -0.5f * AnimalNotificationFixBox.descriptionScreenWidthFraction * this.basescale * Sengine.ReferenceScreenRes.X;
      this.description.SetAllColours(ColourData.Z_Cream);
      this.description.AutoCompleteParagraph();
      this.currRefLoc.Y += num1 + num2;
      int reason = this.reason;
      this.fixbutton.vLocation.Y = this.currRefLoc.Y + 0.5f * this.fixbutton.GetSize_True().Y;
    }

    public bool UpdateAnimalNotificationFixBox(Player player, Vector2 offset, float DeltaTime) => false;

    public void DrawAnimalNotificationFixBox(Vector2 offset, SpriteBatch spritebatch)
    {
      offset += this.location;
      this.frame.DrawCustomerFrame(offset, spritebatch);
      switch (this.reason)
      {
        case 1:
          this.building.DrawSimpleBuildingRenderer(offset + this.buildingloc, spritebatch);
          break;
        case 2:
          this.portrait.DrawAnimalInFrame(offset, spritebatch);
          break;
        case 3:
          this.dietIcons.DrawAnimalDietIcons(offset, spritebatch);
          break;
        case 4:
          this.portrait.DrawAnimalInFrame(offset, spritebatch);
          break;
      }
      this.description.DrawSimpleTextHandler(offset, 1f, AssetContainer.pointspritebatchTop05);
    }

    public Vector2 GetSize_Frameless() => this.fixboxSize;

    public Vector2 GetSize() => this.frame.VSCale * this.basescale * Sengine.ScreenRatioUpwardsMultiplier;
  }
}
