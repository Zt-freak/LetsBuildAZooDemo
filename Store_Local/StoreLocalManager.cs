// Decompiled with JetBrains decompiler
// Type: TinyZoo.Store_Local.StoreLocalManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.Audio;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.GenericUI;
using TinyZoo.OverWorld.Store_Local.Entries;
using TinyZoo.OverWorld.Store_Local.EntryDetail;
using TinyZoo.OverWorld.Store_Local.StoreBG;
using TinyZoo.Z_BreedScreen;

namespace TinyZoo.Store_Local
{
  internal class StoreLocalManager
  {
    private BreedSystemManager breedsystemmanager;
    private EntryManager entrymanager;
    private StoreBGManager storeBGManager;
    private LerpHandler_Float screenlerper;
    private Vector2 Offset;
    private EntryDetailManager PurchaseDetails;
    private bool Exiting;
    private CharacterTextBox charactertextbos;
    private BackButton backbutton;

    public StoreLocalManager(Player player)
    {
      this.backbutton = new BackButton(true);
      this.screenlerper = new LerpHandler_Float();
      this.screenlerper.SetLerp(true, 1f, 0.0f, 3f);
      this.storeBGManager = new StoreBGManager(true);
      this.breedsystemmanager = new BreedSystemManager(player);
      if (this.breedsystemmanager != null)
        return;
      this.entrymanager = new EntryManager();
      this.PurchaseDetails = new EntryDetailManager();
      this.charactertextbos = new CharacterTextBox(AnimalType.ShopKeeper, "Welcome to my store! Here you can upgrade your Restraining Drone.", Sengine.UltraWideSreenDownardsMultiplier);
      this.charactertextbos.Location = new Vector2(512f, 150f);
      this.PurchaseDetails.SelectedNewIcon((StoreEntryType) this.entrymanager.Selected, true, player);
    }

    public bool UpdateStoreLocalManager(float DeltaTime, Player player)
    {
      if (!FeatureFlags.BlockExitFromBreed && this.backbutton.UpdateBackButton(player, DeltaTime))
        player.inputmap.ReleasedThisFrame[7] = true;
      if ((double) this.screenlerper.Value == 0.0 && !this.Exiting && player.inputmap.PressedBackOnController())
      {
        SoundEffectsManager.PlaySpecificSound(SoundEffectType.BackClick);
        SoundEffectsManager.PlaySpecificSound(SoundEffectType.MenuClose);
        this.Exiting = true;
        this.screenlerper.SetLerp(false, 0.0f, 1f, 3f, true);
        player.inputmap.ReleasedThisFrame[7] = false;
      }
      this.screenlerper.UpdateLerpHandler(DeltaTime);
      this.Offset = new Vector2(this.screenlerper.Value * 1024f, 0.0f);
      this.storeBGManager.UpdateStoreBGManager(DeltaTime);
      if (this.breedsystemmanager.UpdateBreedSystemManager(this.Offset, player, DeltaTime))
      {
        if (this.breedsystemmanager.gamestate == MATESTATE.Chamber)
          this.storeBGManager.BlendToNewColour(false, true);
        else
          this.storeBGManager.BlendToNewColour(true, false);
      }
      if (this.breedsystemmanager == null)
      {
        if (this.entrymanager.UpdateEntryManager(DeltaTime, this.Offset, player))
          this.PurchaseDetails.SelectedNewIcon((StoreEntryType) this.entrymanager.Selected, false, player);
        this.PurchaseDetails.UpdateEntryDetailManager(DeltaTime, player);
        this.charactertextbos.UpdateCharacterTextBox(DeltaTime);
      }
      return this.Exiting && (double) this.screenlerper.Value == (double) this.screenlerper.TargetValue;
    }

    public void DrawStoreLocalManager()
    {
      this.storeBGManager.DrawStoreBGManager(this.Offset);
      this.breedsystemmanager.DrawBreedSystemManager(this.Offset);
      if (this.breedsystemmanager == null)
      {
        this.entrymanager.DraEntryManager(this.Offset, AssetContainer.pointspritebatchTop05);
        this.PurchaseDetails.DrawEntryDetailManager(this.Offset, AssetContainer.pointspritebatchTop05);
      }
      if (!FeatureFlags.BlockExitFromBreed)
        this.backbutton.DrawBackButton(this.Offset);
      if (GameFlags.HasNotch)
        this.Offset.X += 10f;
      if (this.breedsystemmanager != null)
        return;
      this.charactertextbos.DrawCharacterTextBox(this.Offset, AssetContainer.pointspritebatchTop05);
    }
  }
}
