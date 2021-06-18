// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_ManageEmployees.HiringDetailView.Recruitment.SubFrames.CampaignReachBarWithIcon
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using TinyZoo.PlayerDir.employees.openpositions;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_ManageEmployees.HiringDetailView.Recruitment.SubFrames
{
  internal class CampaignReachBarWithIcon
  {
    public Vector2 location;
    private CampaignReachBar bar;
    private CampaignReachIcon icon;
    private Vector2 size;

    public CampaignReachBarWithIcon(
      OpenPositions TEMPOPENPOSITIONS,
      float BaseScale,
      Player player,
      CampaignReachType reachType)
    {
      Vector2 defaultBuffer = new UIScaleHelper(BaseScale).DefaultBuffer;
      this.bar = new CampaignReachBar(TEMPOPENPOSITIONS, BaseScale, player, reachType);
      this.icon = new CampaignReachIcon(reachType, BaseScale);
      this.size = Vector2.Zero;
      this.icon.location = this.size;
      this.icon.location += this.icon.GetSize() * 0.5f;
      this.size.X += this.icon.GetSize().X;
      this.size.X += defaultBuffer.X;
      this.bar.location = this.size;
      this.bar.location.X += this.bar.GetSize().X * 0.5f;
      this.size.X += this.bar.GetSize().X;
      this.size.Y = Math.Max(this.icon.GetSize().Y, this.bar.GetSize().Y);
    }

    public Vector2 GetSize() => this.size;

    public void SetNewNumbers(OpenPositions TEMPOPENPOSITIONS, Player player) => this.bar.SetNewNumbers(TEMPOPENPOSITIONS, player);

    public void UpdateCampaignReachBarWithIcon(Player player, float DeltaTime, Vector2 offset)
    {
      offset += this.location;
      this.icon.UpdateCampaignReachIcon(player, DeltaTime, offset);
      this.bar.UpdateCampaignReachBar(player, offset, this.icon.GetIsMouseOver());
    }

    public void DrawCampaignReachBarWithIcon(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.icon.DrawCampaignReachIcon(offset, spriteBatch);
      this.bar.DrawCampaignReachBar(offset, spriteBatch);
    }
  }
}
