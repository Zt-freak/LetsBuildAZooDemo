// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Research_.ResearcherInfo.ResearcherSummary
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using System.Collections.Generic;
using TinyZoo.PlayerDir;

namespace TinyZoo.Z_Research_.ResearcherInfo
{
  internal class ResearcherSummary
  {
    public Vector2 location;
    private List<MiniResearchGuy> miniGuys;
    private float Ybuffer;

    public ResearcherSummary(
      Player player,
      float BaseScale,
      bool UseExtraMiniVersion = false,
      bool IsForResearchBuilding = false)
    {
      this.miniGuys = new List<MiniResearchGuy>();
      this.Ybuffer = 10f * Sengine.ScreenRatioUpwardsMultiplier.Y * BaseScale;
      for (int index = 0; index < Z_Research.researchers.Count; ++index)
      {
        this.miniGuys.Add(new MiniResearchGuy(Z_Research.researchers[index], BaseScale, UseExtraMiniVersion, IsForResearchBuilding));
        this.miniGuys[this.miniGuys.Count - 1].Location += this.miniGuys[this.miniGuys.Count - 1].GetSize() * 0.5f;
        this.miniGuys[this.miniGuys.Count - 1].Location.Y += (float) (this.miniGuys.Count - 1) * (this.miniGuys[this.miniGuys.Count - 1].GetSize().Y + this.Ybuffer);
      }
    }

    public Vector2 GetSize()
    {
      if (this.miniGuys.Count <= 0)
        return Vector2.Zero;
      Vector2 size = this.miniGuys[0].GetSize();
      float y = size.Y * (float) this.miniGuys.Count;
      if (this.miniGuys.Count > 1)
        y += this.Ybuffer * (float) (this.miniGuys.Count - 1);
      return new Vector2(size.X, y);
    }

    public int GetNumberOfResearchersShown() => this.miniGuys.Count;

    public void UpdateResearcherSummary(Player player, float DeltatTime)
    {
      for (int index = 0; index < this.miniGuys.Count; ++index)
        this.miniGuys[index].UpdateMiniResearchGuy();
    }

    public void DrawResearcherSummary(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      for (int index = 0; index < this.miniGuys.Count; ++index)
        this.miniGuys[index].DrawMiniResearchGuy(offset, spriteBatch);
    }
  }
}
