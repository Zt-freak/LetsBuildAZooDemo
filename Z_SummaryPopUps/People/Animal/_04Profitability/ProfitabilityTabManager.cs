// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.Animal._04Profitability.ProfitabilityTabManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Animal._04Profitability.Appeal;
using TinyZoo.Z_SummaryPopUps.People.Animal._04Profitability.OtherSources;
using TinyZoo.Z_SummaryPopUps.People.Animal._04Profitability.Upkeep;

namespace TinyZoo.Z_SummaryPopUps.People.Animal._04Profitability
{
  internal class ProfitabilityTabManager
  {
    public Vector2 location;
    private UpkeepManager upkeepmanager;
    private OtherSourcesManager othersources;
    private AppealManager appealmanager;
    private Vector2 size;

    public ProfitabilityTabManager(
      PrisonerInfo animal,
      Player player,
      float width,
      float BaseScale)
    {
      Vector2 defaultBuffer = new UIScaleHelper(BaseScale).DefaultBuffer;
      this.size = Vector2.Zero;
      this.appealmanager = new AppealManager(width, BaseScale);
      this.appealmanager.location.Y = this.size.Y;
      this.appealmanager.location.Y += this.appealmanager.GetSize().Y * 0.5f;
      this.size.Y += this.appealmanager.GetSize().Y;
      this.size.Y += defaultBuffer.Y;
      this.othersources = new OtherSourcesManager(width, BaseScale);
      this.othersources.location.Y = this.size.Y;
      this.othersources.location.Y += this.othersources.GetSize().Y * 0.5f;
      this.size.Y += this.othersources.GetSize().Y;
      this.size.Y += defaultBuffer.Y;
      this.upkeepmanager = new UpkeepManager(width, BaseScale);
      this.upkeepmanager.location.Y = this.size.Y;
      this.upkeepmanager.location.Y += this.upkeepmanager.GetSize().Y * 0.5f;
      this.size.Y += this.upkeepmanager.GetSize().Y;
      this.size.X = width;
    }

    public Vector2 GetSize() => this.size;

    public void UpdateProfitabilityTabManager()
    {
    }

    public void DrawProfitabilityTabManager(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.upkeepmanager.DrawUpkeepManager(offset, spriteBatch);
      this.othersources.DrawOtherSourcesManager(offset, spriteBatch);
      this.appealmanager.DrawAppealManager(offset, spriteBatch);
    }
  }
}
