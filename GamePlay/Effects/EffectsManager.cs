// Decompiled with JetBrains decompiler
// Type: TinyZoo.GamePlay.Effects.EffectsManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

namespace TinyZoo.GamePlay.Effects
{
  internal class EffectsManager
  {
    private DamageFlash effectsflash;
    private DestoyedBeams destroyedbeams;

    public EffectsManager()
    {
      this.effectsflash = new DamageFlash();
      this.destroyedbeams = new DestoyedBeams();
    }

    public void UpdateEffectsManager(float DeltaTime)
    {
      this.effectsflash.UpdateDamageFlash(DeltaTime);
      this.destroyedbeams.UpdateDestoyedBeams(DeltaTime);
    }

    public void DrawEffectsManager()
    {
      this.effectsflash.DrawDamageFlash();
      this.destroyedbeams.DrawDestoyedBeams();
    }
  }
}
