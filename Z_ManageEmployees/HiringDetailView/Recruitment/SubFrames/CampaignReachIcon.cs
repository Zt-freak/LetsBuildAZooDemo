// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_ManageEmployees.HiringDetailView.Recruitment.SubFrames.CampaignReachIcon
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_ManageEmployees.HiringDetailView.Recruitment.SubFrames
{
  internal class CampaignReachIcon
  {
    public Vector2 location;
    private GameObject icon;
    private MouseoverHandler mouseOverHandler;

    public CampaignReachIcon(CampaignReachType reachType, float BaseScale)
    {
      this.icon = new GameObject();
      this.icon.scale = BaseScale;
      switch (reachType)
      {
        case CampaignReachType.CampaignReach:
          this.icon.DrawRect = new Rectangle(165, 504, 32, 32);
          break;
        case CampaignReachType.SkillRequirement:
          this.icon.DrawRect = new Rectangle(31, 409, 32, 32);
          break;
        default:
          this.icon.DrawRect = TinyZoo.Game1.WhitePixelRect;
          this.icon.scale = BaseScale * 36f;
          break;
      }
      this.icon.SetDrawOriginToCentre();
      this.mouseOverHandler = new MouseoverHandler(this.GetSize(), BaseScale);
    }

    public Vector2 GetSize() => new Vector2((float) this.icon.DrawRect.Width, (float) this.icon.DrawRect.Height) * this.icon.scale * Sengine.ScreenRatioUpwardsMultiplier;

    public bool GetIsMouseOver() => this.mouseOverHandler.mouseover;

    public void UpdateCampaignReachIcon(Player player, float DeltaTime, Vector2 offset)
    {
      offset += this.location;
      this.mouseOverHandler.UpdateMouseoverHandler(player, offset, DeltaTime);
    }

    public void DrawCampaignReachIcon(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.icon.Draw(spriteBatch, AssetContainer.SpriteSheet, offset);
      this.mouseOverHandler.DrawMouseOverHandler(spriteBatch, offset);
    }
  }
}
