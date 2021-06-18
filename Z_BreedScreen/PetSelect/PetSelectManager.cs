// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BreedScreen.PetSelect.PetSelectManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.CollectionScreen;
using TinyZoo.GamePlay.Enemies;

namespace TinyZoo.Z_BreedScreen.PetSelect
{
  internal class PetSelectManager
  {
    private LerpHandler_Float lerper;
    private CollectionScreenManager collectionmanager;
    private Vector2 Offset;
    public AnimalType SelectedAnimal;

    public PetSelectManager(Player player)
    {
      this.lerper = new LerpHandler_Float();
      this.lerper.SetLerp(true, 1f, 0.0f, 3f);
      this.collectionmanager = new CollectionScreenManager(player, _IsPetSelect: true);
    }

    public bool UpdateSelectManager(float DeltaTime, Player player)
    {
      this.lerper.UpdateLerpHandler(DeltaTime);
      this.Offset.X = 1024f * this.lerper.Value;
      bool ExitDone;
      int num = (int) this.collectionmanager.UpdateCollectionScreenManager(this.Offset, DeltaTime, player, out ExitDone, out bool _);
      if (ExitDone && (double) this.lerper.TargetValue != 1.0)
      {
        this.lerper.SetLerp(false, 0.0f, 1f, 3f);
        this.SelectedAnimal = this.collectionmanager.SelectedAnimal;
      }
      return (double) this.lerper.Value == 1.0 && this.lerper.IsComplete();
    }

    public void DrawPetSelectManager(Vector2 MainScreenOffset) => this.collectionmanager.DrawCollectionScreenManager(this.Offset + MainScreenOffset);
  }
}
