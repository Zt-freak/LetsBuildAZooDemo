// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Research_.IconGrid.BG3D.BG3DManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using System.Collections.Generic;
using TinyZoo.GenericUI;
using TinyZoo.OverWorld.OverWorldEnv.Fog;

namespace TinyZoo.Z_Research_.IconGrid.BG3D
{
  internal class BG3DManager
  {
    private BlackOut blackout;
    private List<PawCrawler> paws;
    private FogManager fog;

    public BG3DManager()
    {
      this.blackout = new BlackOut();
      this.blackout.SetAllColours(0.3647059f, 0.6156863f, 0.6117647f);
      this.paws = new List<PawCrawler>();
      for (int index = 0; index < 100; ++index)
        this.paws.Add(new PawCrawler(true));
      this.fog = new FogManager();
    }

    public void UpdateBG3DManager(float DeltaTime)
    {
      for (int index = 0; index < this.paws.Count; ++index)
        this.paws[index].UpdatePawCrawler(DeltaTime);
      this.fog.UpateFogManagerInResearch(DeltaTime);
    }

    public void DrawBG3DManager()
    {
      this.blackout.DrawBlackOut(Vector2.Zero, AssetContainer.pointspritebatch01);
      this.fog.DrawFogManagerInResearch();
      for (int index = 0; index < this.paws.Count; ++index)
        this.paws[index].DrawPawCrawler();
    }
  }
}
