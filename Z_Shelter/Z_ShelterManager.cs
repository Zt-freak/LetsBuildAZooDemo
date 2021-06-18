// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Shelter.Z_ShelterManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using TinyZoo.GenericUI;
using TinyZoo.OverWorld.Store_Local.StoreBG;
using TinyZoo.Z_Shelter.SheltderedAnimals;

namespace TinyZoo.Z_Shelter
{
  internal class Z_ShelterManager
  {
    private StoreBGManager storeBGManager;
    private ScreenHeading heading;
    private ShelteredAnimalManager shelteredanimals;
    private BackButton CloseButton;
    private ShelterSummary sheltersummary;

    public Z_ShelterManager(Player player)
    {
      this.shelteredanimals = new ShelteredAnimalManager(player);
      this.CloseButton = new BackButton();
      this.storeBGManager = new StoreBGManager(IsAutumnal: true);
      this.heading = new ScreenHeading("ANIMAL RESCUE", 100f);
      this.sheltersummary = new ShelterSummary(player);
      this.sheltersummary.Location = new Vector2(512f, 100f);
    }

    public bool UpdateZ_ShelterManager(Player player, float DeltaTime)
    {
      this.shelteredanimals.UpdateShelteredAnimalManager(player, DeltaTime, Vector2.Zero);
      this.storeBGManager.UpdateStoreBGManager(DeltaTime);
      if (this.CloseButton.UpdateBackButton(player, DeltaTime))
      {
        if (!FeatureFlags.NewAnimalGot)
          return true;
        Game1.SetNextGameState(GAMESTATE.OverWorld);
        Game1.screenfade.BeginFade(true);
      }
      return false;
    }

    public void DrawZ_ShelterManager()
    {
      this.storeBGManager.DrawStoreBGManager(Vector2.Zero);
      this.sheltersummary.DrawShelterSummary(Vector2.Zero);
      this.heading.DrawScreenHeading(Vector2.Zero, AssetContainer.pointspritebatch03);
      this.shelteredanimals.DrawShelteredAnimalManager(Vector2.Zero);
      this.CloseButton.DrawBackButton(Vector2.Zero);
    }
  }
}
