// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_HUD.AnimalDeliveryUI.AnimalDeliveryPanel
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using System.Collections.Generic;
using TinyZoo.PlayerDir.Animals;
using TinyZoo.PlayerDir.IntakeStuff;
using TinyZoo.Z_AnimalNotification;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_HUD.AnimalDeliveryUI
{
  internal class AnimalDeliveryPanel
  {
    private static Rectangle arrowRect = new Rectangle(0, 570, 12, 7);
    public Vector2 location;
    private float basescale;
    private UIScaleHelper uiscale;
    private BigBrownPanel panel;
    private CustomerFrame frame;
    private CustomerFrame hideframe1;
    private CustomerFrame hideframe2;
    private Vector2 framescale;
    private Vector2 trayscale;
    private GameObject uparrow;
    private GameObject downarrow;
    private AnimalDeliveryList listpanel;
    private MovingTray tray;
    private AnimalDeliveryDetailedView detailedview;
    private ConfirmationDialog cancelconfirm;
    private MouseoverHandler mouseoverhandler;

    public AnimalDeliveryPanel(float basescale_, List<AnimalOrder> animalsonorderforthispen)
    {
      this.basescale = basescale_;
      this.uiscale = new UIScaleHelper(this.basescale);
      Vector2 defaultBuffer = this.uiscale.DefaultBuffer;
      this.listpanel = new AnimalDeliveryList(this.basescale, animalsonorderforthispen);
      this.detailedview = (AnimalDeliveryDetailedView) null;
      float y = this.uiscale.ScaleY(30f);
      this.framescale = this.listpanel.GetSize();
      this.framescale.Y += 2f * y;
      this.hideframe1 = new CustomerFrame(new Vector2(this.framescale.X, y), CustomerFrameColors.Brown, this.basescale);
      this.hideframe2 = new CustomerFrame(new Vector2(this.framescale.X, y), CustomerFrameColors.Brown, this.basescale);
      this.frame = new CustomerFrame(this.framescale, BaseScale: this.basescale);
      this.panel = new BigBrownPanel(this.framescale, true, SEngine.Localization.Localization.GetText(868), this.basescale);
      this.panel.Finalize(this.framescale);
      this.mouseoverhandler = new MouseoverHandler(this.framescale, this.basescale);
      this.uparrow = new GameObject();
      this.uparrow.DrawRect = AnimalDeliveryPanel.arrowRect;
      this.uparrow.SetDrawOriginToCentre();
      this.downarrow = new GameObject();
      this.downarrow.DrawRect = AnimalDeliveryPanel.arrowRect;
      this.downarrow.SetDrawOriginToCentre();
      this.hideframe1.frame.vLocation.Y += (float) (-0.5 * (double) this.framescale.Y + 0.5 * (double) y);
      this.hideframe2.frame.vLocation.Y = (float) (0.5 * (double) this.framescale.Y - 0.5 * (double) y);
      this.uparrow.vLocation = this.hideframe1.frame.vLocation;
      this.downarrow.vLocation = this.hideframe2.frame.vLocation;
    }

    public void MakeTray(IntakePerson animalinfo, AnimalOrder orderInfo)
    {
      this.detailedview = new AnimalDeliveryDetailedView(animalinfo, orderInfo, this.basescale);
      this.trayscale = this.detailedview.GetSize();
      this.tray = new MovingTray(this.trayscale, new Vector2((float) (-(double) this.trayscale.X - 1.5 * (double) this.uiscale.DefaultBuffer.X), 0.0f), MovingTrayDirection.Left, this.basescale, SEngine.Localization.Localization.GetText(972));
      this.tray.location.X = (float) (-0.5 * (double) this.framescale.X + 0.5 * (double) this.trayscale.X);
      this.tray.location.Y = (float) (-0.5 * (double) this.framescale.Y + 0.5 * (double) this.trayscale.Y);
      if (this.tray.IsOpen)
        return;
      this.tray.StartLerp();
    }

    public bool CheckMouseOver(Player player, Vector2 offset)
    {
      bool flag = false;
      offset += this.location;
      if (this.tray != null)
        flag |= this.tray.CheckMouseOver(player, offset);
      return flag | this.panel.CheckMouseOver(player, offset);
    }

    public bool UpdateAnimalDeliveryPanel(Player player, Vector2 offset, float DeltaTime)
    {
      Vector2 offset1 = offset + this.location;
      int num = 0 | (!this.panel.UpdatePanelCloseButton(player, DeltaTime, offset1) ? 0 : (this.cancelconfirm == null ? 1 : 0));
      this.panel.UpdateDragger(player, ref this.location, DeltaTime);
      Vector2 offset2 = offset + this.location;
      if (this.cancelconfirm == null)
      {
        if (this.mouseoverhandler.UpdateMouseoverHandler(player, offset2, DeltaTime))
          this.listpanel.UpdateScroller(player);
        IntakePerson animalinfo;
        AnimalOrder orderinfo;
        this.listpanel.UpdateAnimalDeliveryList(player, offset2, out animalinfo, out orderinfo, DeltaTime);
        if (animalinfo != null)
          this.MakeTray(animalinfo, orderinfo);
        if (this.detailedview == null)
          return num != 0;
        this.tray.UpdateMovingTray(player, DeltaTime, offset2);
        this.detailedview.location = this.tray.GetTruePosition();
        if (this.tray.Lerping)
          return num != 0;
        if (!this.detailedview.UpdateAnimalDeliveryDetailedView(player, offset2, DeltaTime))
          return num != 0;
        this.cancelconfirm = new ConfirmationDialog(SEngine.Localization.Localization.GetText(973), SEngine.Localization.Localization.GetText(974), this.basescale);
        return num != 0;
      }
      if (!this.cancelconfirm.UpdateConfirmationDialog(player, offset + Sengine.HalfReferenceScreenRes, DeltaTime, out bool _))
        return num != 0;
      this.cancelconfirm = (ConfirmationDialog) null;
      return num != 0;
    }

    public void DrawAnimalDeliveryPanel(SpriteBatch spritebatch, Vector2 offset)
    {
      Vector2 vector2 = offset + this.location;
      if (this.detailedview != null)
      {
        this.tray.DrawMovingTray(vector2, spritebatch);
        this.detailedview.DrawAnimalDeliveryDetailedView(spritebatch, vector2);
      }
      this.panel.DrawBigBrownPanel(vector2, spritebatch);
      this.frame.DrawCustomerFrame(vector2, spritebatch);
      this.listpanel.DrawAnimalDeliveryList(spritebatch, vector2);
      this.hideframe1.DrawCustomerFrame(vector2, spritebatch);
      this.hideframe2.DrawCustomerFrame(vector2, spritebatch);
      if (!this.listpanel.maxedup)
        this.uparrow.Draw(spritebatch, AssetContainer.SpriteSheet, vector2 + this.uparrow.vLocation, this.basescale, 0.0f, true);
      if (!this.listpanel.maxeddown)
        this.downarrow.Draw(spritebatch, AssetContainer.SpriteSheet, vector2 + this.downarrow.vLocation, this.basescale, 3.141593f, true);
      if (this.detailedview == null)
        return;
      this.tray.DrawOpenCloseButton(vector2, spritebatch);
      if (this.cancelconfirm == null)
        return;
      this.cancelconfirm.DrawConfirmationDialog(spritebatch, offset + Sengine.HalfReferenceScreenRes);
    }
  }
}
