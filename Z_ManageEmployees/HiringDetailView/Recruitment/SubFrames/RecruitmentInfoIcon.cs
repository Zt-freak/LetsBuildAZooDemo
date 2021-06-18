// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_ManageEmployees.HiringDetailView.Recruitment.SubFrames.RecruitmentInfoIcon
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_ManageEmployees.HiringDetailView.Recruitment.SubFrames
{
  internal class RecruitmentInfoIcon : GameObject
  {
    private MouseoverHandler mouseoverHandler;

    public RecruitmentInfoIcon(JobPostingModifiers infoType, float BaseScale)
    {
      this.scale = BaseScale;
      switch (infoType)
      {
        case JobPostingModifiers.AdminCost:
          this.DrawRect = new Rectangle(962, 144, 20, 20);
          break;
        case JobPostingModifiers.SocialMedia:
          this.DrawRect = new Rectangle(962, 165, 20, 20);
          break;
        case JobPostingModifiers.JobPortal:
          this.DrawRect = new Rectangle(941, 168, 20, 20);
          break;
      }
      this.SetDrawOriginToCentre();
      this.mouseoverHandler = new MouseoverHandler(this.GetSize(), BaseScale);
    }

    public void SetActive(bool _isActive)
    {
      if (_isActive)
        this.SetAllColours(Color.LightGray.ToVector3());
      else
        this.SetAllColours(Color.White.ToVector3());
    }

    public Vector2 GetSize() => new Vector2((float) this.DrawRect.Width * this.scale, (float) this.DrawRect.Height * this.scale) * Sengine.ScreenRatioUpwardsMultiplier;

    public bool GetIsMouseOver() => this.mouseoverHandler.mouseover;

    public void UpdateRecruitmentInfoIcon(Player player, float DeltaTime, Vector2 offset)
    {
      offset += this.vLocation;
      this.mouseoverHandler.UpdateMouseoverHandler(player, offset, DeltaTime);
    }

    public void DrawRecruitmentInfoIcon(Vector2 offset, SpriteBatch spriteBatch)
    {
      this.Draw(spriteBatch, AssetContainer.SpriteSheet, offset);
      offset += this.vLocation;
      this.mouseoverHandler.DrawMouseOverHandler(spriteBatch, offset);
    }
  }
}
