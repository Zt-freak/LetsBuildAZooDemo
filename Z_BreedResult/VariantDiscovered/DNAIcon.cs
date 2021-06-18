// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BreedResult.VariantDiscovered.DNAIcon
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_BreedResult.VariantDiscovered
{
  internal class DNAIcon : AnimatedGameObject
  {
    private static Rectangle rect = new Rectangle(930, 307, 12, 19);
    private CustomerFrame frame;

    public DNAIcon(float basescale, float scale_ = 1f, bool AddFrame = false)
    {
      this.DrawRect = DNAIcon.rect;
      this.scale = basescale * scale_;
      UIScaleHelper uiScaleHelper = new UIScaleHelper(basescale);
      if (!AddFrame)
        return;
      this.frame = new CustomerFrame(uiScaleHelper.ScaleVector2(new Vector2(25f, 25f)), CustomerFrameColors.DarkerCream, basescale);
      this.SetDrawOriginToCentre();
    }

    public void SetUpSimpleAnimation()
    {
      this.DrawRect = new Rectangle(128, 637, 12, 19);
      this.SetUpSimpleAnimation(5, 0.2f);
    }

    public Vector2 GetSize(bool noScreenRatioMult = false)
    {
      if (this.frame != null)
        return this.frame.VSCale;
      Vector2 vector2 = new Vector2((float) this.DrawRect.Width, (float) this.DrawRect.Height) * this.scale;
      if (!noScreenRatioMult)
        vector2 *= Sengine.ScreenRatioUpwardsMultiplier;
      return vector2;
    }

    public void UpdateDNAIconAnimation(float DeltaTime) => this.UpdateAnimation(DeltaTime);

    public bool UpdateDNAIcon(
      Player player,
      Vector2 offset,
      float DeltaTime,
      out bool MouseOvered)
    {
      offset += this.vLocation;
      MouseOvered = false;
      this.UpdateDNAIconAnimation(DeltaTime);
      return this.frame != null && this.frame.UpdateForMouseOverAndClick(player, DeltaTime, offset, out MouseOvered);
    }

    public void DrawDNAIcon(Vector2 offset, SpriteBatch spritebatch)
    {
      if (this.frame != null)
        this.frame.DrawCustomerFrame(offset + this.vLocation, spritebatch);
      this.Draw(spritebatch, AssetContainer.SpriteSheet, offset);
      if (this.frame == null)
        return;
      this.frame.PostDrawMouseoverOverlay(offset + this.vLocation, spritebatch);
    }
  }
}
