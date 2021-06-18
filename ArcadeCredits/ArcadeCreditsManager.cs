// Decompiled with JetBrains decompiler
// Type: TinyZoo.ArcadeCredits.ArcadeCreditsManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using TinyZoo.Audio;
using TinyZoo.OverWorld.Store_Local.StoreBG;
using TinyZoo.Settings.Credits;

namespace TinyZoo.ArcadeCredits
{
  internal class ArcadeCreditsManager
  {
    private CreditsManager credits;
    private Bouncy bouncy;
    private StoreBGManager storeBG;

    public ArcadeCreditsManager()
    {
      MusicManager.playsong(SongTitle.UnbearablyBad, true);
      this.credits = new CreditsManager(true);
      this.storeBG = new StoreBGManager(GameFlags.DifficultyIsEasy);
      this.storeBG.SetDark();
      this.bouncy = new Bouncy();
    }

    public void UpdateArcadeCreditsManager(Player player, float DeltaTime)
    {
      this.storeBG.UpdateStoreBGManager(DeltaTime);
      this.credits.UpdateCreditsManager(player, DeltaTime);
      this.bouncy.uPDATEBouncy(DeltaTime);
    }

    public void DrawArcadeCreditsManager()
    {
      this.storeBG.DrawStoreBGManager(Vector2.Zero);
      this.bouncy.DrawBouncy();
      this.credits.DrawCreditsManager();
    }
  }
}
