// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_HUD.Z_Notification.NotificationRibbon.RibbonButton
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_Notification;

namespace TinyZoo.Z_HUD.Z_Notification.NotificationRibbon
{
  internal class RibbonButton
  {
    private GameObject icon;
    public Vector2 location;
    private float basescale;
    private UIScaleHelper uiScale;
    private float iconscale;
    private bool mouseover;

    public RibbonButton(float BaseScale, Z_NotificationType type = Z_NotificationType.Count)
    {
      this.basescale = BaseScale;
      this.uiScale = new UIScaleHelper(this.basescale);
      this.iconscale = 1f;
      this.icon = new GameObject();
      switch (type)
      {
        case Z_NotificationType.A_AnimalBirth:
          this.icon.DrawRect = new Rectangle(738, 102, 16, 17);
          break;
        case Z_NotificationType.A_AnimalBirthInBreedingRoom:
          this.icon.DrawRect = new Rectangle(666, 107, 16, 17);
          break;
        case Z_NotificationType.A_AnimalTransferedFromBreedingRoom:
          this.icon.DrawRect = new Rectangle(600, 82, 16, 17);
          break;
        case Z_NotificationType.A_AnimalHunger:
          this.icon.DrawRect = new Rectangle(721, 102, 16, 17);
          break;
        case Z_NotificationType.A_AnimalDeath:
          this.icon.DrawRect = new Rectangle(756, 102, 16, 17);
          break;
        case Z_NotificationType.A_CRISPR_HybridBirth:
          this.icon.DrawRect = new Rectangle(190, 28, 16, 17);
          break;
        case Z_NotificationType.CannotReachGate:
          this.icon.DrawRect = new Rectangle(840, 102, 16, 17);
          break;
        case Z_NotificationType.F_GateBroke:
          this.icon.DrawRect = new Rectangle(857, 102, 16, 17);
          break;
        case Z_NotificationType.F_BuildArchitect:
          this.icon.DrawRect = new Rectangle(258, 46, 16, 17);
          break;
        case Z_NotificationType.F_BuildABench:
          this.icon.DrawRect = new Rectangle(293, 444, 16, 17);
          break;
        case Z_NotificationType.F_BuildABin:
          this.icon.DrawRect = new Rectangle(412, 939, 16, 17);
          break;
        case Z_NotificationType.A_BuildFirstPen:
          this.icon.DrawRect = new Rectangle(258, 28, 16, 17);
          break;
        case Z_NotificationType.A_AddAnimalsToYourPen:
          this.icon.DrawRect = new Rectangle(258, 64, 16, 17);
          break;
        case Z_NotificationType.A_NoWater:
          this.icon.DrawRect = new Rectangle(241, 28, 16, 17);
          break;
        case Z_NotificationType.A_NoEnrichment:
          this.icon.DrawRect = new Rectangle(241, 46, 16, 17);
          break;
        case Z_NotificationType.A_BuildAnotherPen:
          this.icon.DrawRect = new Rectangle(258, 28, 16, 17);
          break;
        case Z_NotificationType.F_BuildAnyShop:
          this.icon.DrawRect = new Rectangle(241, 64, 16, 17);
          break;
        case Z_NotificationType.F_BuildAFoodShop:
          this.icon.DrawRect = new Rectangle(224, 28, 16, 17);
          break;
        case Z_NotificationType.F_BuildADrinksShop:
          this.icon.DrawRect = new Rectangle(224, 46, 16, 17);
          break;
        case Z_NotificationType.F_BuildAGiftShop:
          this.icon.DrawRect = new Rectangle(207, 28, 16, 17);
          break;
        case Z_NotificationType.F_AShopNeedsAnEmployee:
          this.icon.DrawRect = new Rectangle(207, 46, 16, 17);
          break;
        case Z_NotificationType.F_ShopHasExtraOpenPositions:
          this.icon.DrawRect = new Rectangle(207, 46, 16, 17);
          break;
        case Z_NotificationType.F_AJobHasApplicants:
          this.icon.DrawRect = new Rectangle(924, 171, 16, 17);
          break;
        case Z_NotificationType.Q_QuestComplete:
          this.icon.DrawRect = new Rectangle(0, 604, 16, 17);
          break;
        case Z_NotificationType.F_FoodTrash:
          this.icon.DrawRect = new Rectangle(412, 939, 16, 17);
          break;
        case Z_NotificationType.F_VomitTrash:
          this.icon.DrawRect = new Rectangle(155, 29, 16, 17);
          break;
        case Z_NotificationType.A_NoWaterConnection:
          this.icon.DrawRect = new Rectangle(612, 118, 16, 17);
          break;
        case Z_NotificationType.A_AnimalSick:
          this.icon.DrawRect = new Rectangle(0, 622, 16, 17);
          break;
        case Z_NotificationType.A_AnimalStarving:
          this.icon.DrawRect = new Rectangle(0, 707, 16, 17);
          break;
        case Z_NotificationType.ExpiredAnimalFood:
          this.icon.DrawRect = new Rectangle(0, 725, 16, 17);
          break;
        case Z_NotificationType.F_ResearchComplete:
          this.icon.DrawRect = new Rectangle(0, 743, 16, 17);
          break;
        case Z_NotificationType.F_TicketPrice:
          this.icon.DrawRect = new Rectangle(112, 836, 16, 17);
          break;
        default:
          this.icon.DrawRect = new Rectangle(721, 102, 16, 17);
          break;
      }
      this.icon.SetDrawOriginToCentre();
      this.icon.scale = this.iconscale * this.basescale;
    }

    public Vector2 GetSize() => this.iconscale * this.uiScale.ScaleVector2(new Vector2((float) this.icon.DrawRect.Width, (float) this.icon.DrawRect.Height));

    public bool UpdateRibbonButton(Player player, float DeltaTime, Vector2 offset)
    {
      this.mouseover = MathStuff.CheckPointCollision(true, offset + this.location, this.iconscale, this.uiScale.ScaleX((float) this.icon.DrawRect.Width), this.uiScale.ScaleY((float) this.icon.DrawRect.Height), player.inputmap.PointerLocation);
      bool flag = false;
      if (this.mouseover)
        flag = (double) player.player.touchinput.ReleaseTapArray[0].X > 0.0;
      return flag;
    }

    public void DrawRibbonButton(SpriteBatch spritebatch, Vector2 offset)
    {
      offset += this.location;
      this.icon.Draw(spritebatch, AssetContainer.SpriteSheet, offset);
    }
  }
}
