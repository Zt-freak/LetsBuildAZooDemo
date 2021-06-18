// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_HUD.VirtualMouse.MouseParticle
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;

namespace TinyZoo.Z_HUD.VirtualMouse
{
  internal class MouseParticle : GameObject
  {
    private float Latency;
    private int Index;
    private bool IsMicroDot;

    public MouseParticle(int Idnex, int TotalParticles, bool _IsMicroDot = false)
    {
      this.IsMicroDot = _IsMicroDot;
      this.Index = Idnex;
      this.Latency = (float) Idnex;
      this.DrawRect = new Rectangle(0, 254, 88, 88);
      this.SetDrawOriginToCentre();
      this.scale = 0.2f;
      this.fAlpha = (float) (1.0 - (double) this.Latency / (double) (TotalParticles - 1));
      this.Latency = this.fAlpha;
      this.Latency *= 0.8f;
      this.Latency += 0.2f;
      double fAlpha = (double) this.fAlpha;
      if (this.IsMicroDot)
      {
        this.scale *= 0.4f;
        this.SetAllColours(0.0f, 0.0f, 0.0f);
      }
      switch ((this.Index - 1) % 7)
      {
        case 0:
          this.SetAllColours(new Vector3(188f, 49f, 254f) / (float) byte.MaxValue);
          break;
        case 1:
          this.SetAllColours(new Vector3(75f, 0.0f, 129f) / (float) byte.MaxValue);
          break;
        case 2:
          this.SetAllColours(new Vector3(0.0f, 0.0f, 254f) / (float) byte.MaxValue);
          break;
        case 3:
          this.SetAllColours(new Vector3(0.0f, 128f, 1f) / (float) byte.MaxValue);
          break;
        case 4:
          this.SetAllColours(new Vector3((float) byte.MaxValue, (float) byte.MaxValue, 1f) / (float) byte.MaxValue);
          break;
        case 5:
          this.SetAllColours(new Vector3((float) byte.MaxValue, 99f, 1f) / (float) byte.MaxValue);
          break;
        case 6:
          this.SetAllColours(new Vector3(254f, 0.0f, 1f) / (float) byte.MaxValue);
          break;
      }
      this.SetAlpha(0.0f);
    }

    public void ForceSet(Vector2 Location) => this.vLocation = Location;

    public void UpdateMouseParticle(
      Vector2 Location,
      float DeltaTime,
      Vector2 LocationOfPrevious,
      bool DoNow)
    {
      if (this.Index == 0)
      {
        this.vLocation = Location;
      }
      else
      {
        if (DoNow)
        {
          this.vLocation = Location;
          this.SetAlpha(false, 0.5f, 1f, 0.0f);
        }
        this.UpdateColours(DeltaTime);
        if (this.vLocation != LocationOfPrevious)
          this.vLocation = this.vLocation + (LocationOfPrevious - this.vLocation) * this.Latency * DeltaTime;
        int num = Location != this.vLocation ? 1 : 0;
      }
    }

    public void DrawMouseParticle(float MasterALpha)
    {
      if (this.IsMicroDot)
        this.Draw(AssetContainer.pointspritebatch07Final, AssetContainer.UISheet, Vector2.Zero, this.scale * 0.5f, MasterALpha * 0.8f);
      else if (this.Index == 0)
        this.Draw(AssetContainer.pointspritebatch07Final, AssetContainer.UISheet, Vector2.Zero, this.scale * 0.5f, MasterALpha);
      else
        this.Draw(AssetContainer.pointspritebatch07Final, AssetContainer.UISheet, Vector2.Zero, this.scale * 0.4f, this.fAlpha);
    }
  }
}
