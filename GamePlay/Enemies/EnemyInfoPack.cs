// Decompiled with JetBrains decompiler
// Type: TinyZoo.GamePlay.Enemies.EnemyInfoPack
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using System.Collections.Generic;
using TinyZoo.Z_AnimalsAndPeople;

namespace TinyZoo.GamePlay.Enemies
{
  internal class EnemyInfoPack
  {
    private List<Rectangle> BaseWalk;
    private List<Rectangle> BaseWalkUp;
    private List<Rectangle> BaseWalkSideways;
    private List<Rectangle> IdleFrame;
    public Vector2 Origin;
    public int Frames;
    public ShadowInfo shadowdata;
    public Rectangle WalkUp;
    public Rectangle WalkDown;
    public Rectangle WalkRight;

    public EnemyInfoPack(Rectangle _BaseWalk, Rectangle _IdleFrame, Vector2 _Origin, int _Frames)
    {
      this.BaseWalk = new List<Rectangle>();
      this.BaseWalkSideways = new List<Rectangle>();
      this.BaseWalkUp = new List<Rectangle>();
      this.IdleFrame = new List<Rectangle>();
      this.BaseWalk.Add(_BaseWalk);
      this.IdleFrame.Add(_IdleFrame);
      this.Origin = _Origin;
      this.Frames = _Frames;
    }

    public EnemyInfoPack(Rectangle _BaseWalk, Vector2 _Origin, int _Frames)
    {
      this.BaseWalk = new List<Rectangle>();
      this.IdleFrame = new List<Rectangle>();
      this.BaseWalk.Add(_BaseWalk);
      this.IdleFrame.Add(_BaseWalk);
      this.Origin = _Origin;
      this.Frames = _Frames;
    }

    public EnemyInfoPack(
      Rectangle _BaseWalkDown,
      Rectangle _WalkRight,
      Rectangle _WalkUp,
      Vector2 _Origin,
      int _Frames)
    {
      this.BaseWalk = new List<Rectangle>();
      this.IdleFrame = new List<Rectangle>();
      this.BaseWalk.Add(_BaseWalkDown);
      this.IdleFrame.Add(_BaseWalkDown);
      this.Origin = _Origin;
      this.Frames = _Frames;
      this.WalkUp = _WalkUp;
      this.WalkDown = _BaseWalkDown;
      this.WalkRight = _WalkRight;
    }

    public int GetTotalVariants() => this.IdleFrame.Count;

    public void AddVariant(Rectangle newrect)
    {
      this.IdleFrame.Add(newrect);
      this.BaseWalk.Add(newrect);
    }

    public void AddVariant(Rectangle newrect, Rectangle _WalkUp, Rectangle _WalkSideways)
    {
      this.IdleFrame.Add(newrect);
      this.BaseWalk.Add(newrect);
    }

    public Rectangle GetBaseWalk(int Variant)
    {
      if (Variant >= this.IdleFrame.Count || Variant == -1)
        Variant = 0;
      return this.BaseWalk[Variant];
    }

    public Rectangle GetIdleFrame(int Variant)
    {
      if (Variant >= this.IdleFrame.Count || Variant == -1)
        Variant = 0;
      return this.IdleFrame[Variant];
    }
  }
}
