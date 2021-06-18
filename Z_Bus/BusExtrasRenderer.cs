// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Bus.BusExtrasRenderer
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.Z_DayNight;

namespace TinyZoo.Z_Bus
{
  internal class BusExtrasRenderer : AnimatedGameObject
  {
    private BusAttachmentType busattachmenttype;

    public BusExtrasRenderer()
    {
      this.busattachmenttype = BusAttachmentType.GarbageTruck;
      this.bActive = false;
    }

    public void StartCollectingGarbage(bool OneBag)
    {
      if (OneBag)
        this.DrawRect = new Rectangle(505, 1572, 25, 59);
      else
        this.DrawRect = new Rectangle(94, 1632, 25, 59);
      this.SetUpSimpleAnimation(26, 0.1f);
      this.PlayOnlyOnce = true;
      this.bActive = true;
    }

    public void UpdateBusExtrasRenderer(float DeltaTime)
    {
      if (!this.bActive || this.busattachmenttype != BusAttachmentType.GarbageTruck || !this.UpdateAnimation(DeltaTime))
        return;
      this.bActive = false;
    }

    public void DrawBusExtrasRenderer(Vector2 ParentLocation)
    {
      if (!this.bActive)
        return;
      this.SetAllColours(DayNightManager.SunShineValueR, DayNightManager.SunShineValueG, DayNightManager.SunShineValueB);
      this.DrawOrigin = new Vector2(8f, 79f);
      this.vLocation = ParentLocation;
      this.WorldOffsetDraw(AssetContainer.pointspritebatch01, AssetContainer.AnimalSheet);
    }
  }
}
