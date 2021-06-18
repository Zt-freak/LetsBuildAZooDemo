// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.Animal_Dead.DeadAnimalPopUp
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_HUD.MaintenanceBar;
using TinyZoo.Z_SummaryPopUps.People.Animal;
using TinyZoo.Z_SummaryPopUps.People.Animal._01Animal.Longevity;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_SummaryPopUps.People.Animal_Dead
{
  internal class DeadAnimalPopUp
  {
    internal static float IDealDexcriptionHeight = 100f;
    private DeathDescription desthdescription;
    private PrisonerInfo REF_anaimal;
    private LongevityManager longevitymanager;
    private MaintenanceBarPanel barpanel;
    private BigBrownPanel panel;
    private Vector2 framescale;
    private float basescale;
    private Vector2 location;
    private UIScaleHelper uiscale;
    private LookAtThisThingButton lookAtThisThingButton;

    public DeadAnimalPopUp(PrisonerInfo anaimal, Player player, float basescale_)
    {
      this.basescale = basescale_;
      this.uiscale = new UIScaleHelper(this.basescale);
      this.REF_anaimal = anaimal;
      this.panel = new BigBrownPanel(this.framescale, true, "Carcass", this.basescale);
      this.barpanel = new MaintenanceBarPanel(anaimal, this.basescale, player);
      this.desthdescription = new DeathDescription(this.barpanel.GetSize().X, this.basescale, anaimal, player);
      this.framescale = this.barpanel.GetSize();
      this.framescale.Y += this.uiscale.DefaultBuffer.Y + this.desthdescription.GetSize().Y;
      this.longevitymanager = new LongevityManager(anaimal, this.framescale.X, this.basescale, true);
      this.framescale.Y += this.longevitymanager.GetSize().Y + this.uiscale.DefaultBuffer.Y;
      Vector2 zero = Vector2.Zero;
      zero.Y = -0.5f * this.framescale.Y;
      this.longevitymanager.Location = zero;
      this.longevitymanager.Location.Y += 0.5f * this.longevitymanager.GetSize().Y;
      zero.Y += this.longevitymanager.GetSize().Y + this.uiscale.DefaultBuffer.Y;
      this.barpanel.location.Y = zero.Y + 0.5f * this.barpanel.GetSize().Y;
      zero.Y += this.barpanel.GetSize().Y + this.uiscale.DefaultBuffer.Y;
      this.desthdescription.location.Y = zero.Y + 0.5f * this.desthdescription.GetSize().Y;
      this.panel.Finalize(this.framescale);
      this.location = new Vector2((float) (1024.0 - (double) this.uiscale.ScaleX(CustomerViewManager.topRightLocBuffer_Raw.X) - (double) this.framescale.X * 0.5), 384f);
      this.lookAtThisThingButton = new LookAtThisThingButton(anaimal, this.basescale);
      this.lookAtThisThingButton.location.X = this.panel.GetMiniHeadingSize(false).X + this.uiscale.DefaultBuffer.X;
      this.lookAtThisThingButton.location -= this.panel.vScale * 0.5f;
      this.lookAtThisThingButton.location.X += this.lookAtThisThingButton.GetSize().X * 0.5f + this.uiscale.DefaultBuffer.X;
      this.lookAtThisThingButton.location.Y += (float) ((double) this.panel.GetMiniHeadingSize(false).Y * 0.5 + (double) this.uiscale.DefaultBuffer.Y * 0.5);
      this.lookAtThisThingButton.location.Y -= this.lookAtThisThingButton.GetSize().Y * 0.5f;
    }

    public bool CheckMouseOver(Player player, Vector2 offset)
    {
      offset += this.location;
      return this.panel.CheckMouseOver(player, offset);
    }

    public bool UpdateDeadAnimalPopUp(Player player, Vector2 offset, float DeltaTime)
    {
      offset += this.location;
      bool flag = false;
      if (this.panel.UpdatePanelCloseButton(player, DeltaTime, offset))
        flag = true;
      this.panel.UpdateDragger(player, ref this.location, DeltaTime);
      this.desthdescription.UpdateDeathDescription();
      this.barpanel.UpdateMaintenanceBarPanel(DeltaTime, player, offset, out bool _);
      if (this.lookAtThisThingButton != null)
      {
        this.lookAtThisThingButton.UpdateLookAtThisThingButton(player, DeltaTime, offset);
        if (flag)
          this.lookAtThisThingButton.OnButtonDestroy();
      }
      return flag;
    }

    public void DrawDeadAnimalPopUp(Vector2 offset, SpriteBatch spritebatch)
    {
      offset += this.location;
      this.panel.DrawBigBrownPanel(offset);
      this.desthdescription.DrawDeathDescription(offset);
      this.barpanel.DrawMaintenanceBarPanel(offset, spritebatch);
      this.longevitymanager.DrawLongevityManager(offset, spritebatch);
      if (this.lookAtThisThingButton == null)
        return;
      this.lookAtThisThingButton.DrawLookAtThisThingButton(offset, spritebatch);
    }
  }
}
