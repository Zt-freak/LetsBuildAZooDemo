// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_OverWorld.MoneyRendering.BankNote
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using SEngine.Lerp;

namespace TinyZoo.Z_OverWorld.MoneyRendering
{
  internal class BankNote : GameObject
  {
    private PopLerper poper;
    private float Timer;
    private Vector2 LocationInWorldSpace;
    private string Value;
    private bool Given;
    private int MoneyGive;
    private Color TextCOlour;
    private float BaseScale = 2f;
    private IconPopType poptype;
    private float VerticalDrift;
    private float HorizontalDrift;
    private float DriftSpeed;
    private Texture2D TexturePointer;
    private float YOffsetForText;
    private float XOffsetForText;

    public void Spawnicon(NoteType notetype, string DrawThis, Vector2 Location)
    {
      this.SpawnBankNote(notetype, 0, DrawThis, Location);
      this.Given = true;
      this.TextCOlour = Color.Red;
    }

    public void SpawnPopIconMoment(string DrawThis, Vector2 Location, IconPopType icontype)
    {
      this.TexturePointer = AssetContainer.SpriteSheet;
      this.Value = DrawThis;
      this.MoneyGive = 0;
      this.Given = true;
      this.BaseScale = 1f;
      this.poper = new PopLerper();
      DrawOriginPosition draworiginposition = DrawOriginPosition.CentreBottom;
      this.poptype = icontype;
      this.SetAlpha(1f);
      switch (icontype)
      {
        case IconPopType.Trash:
          this.DrawRect = new Rectangle(454, 706, 23, 22);
          break;
        case IconPopType.SeeAnimalsLove:
          this.DrawRect = new Rectangle(443, 839, 21, 19);
          this.scale = 2f;
          this.BaseScale = 2f;
          break;
        case IconPopType.UseToilet:
          this.DrawRect = new Rectangle(386, 802, 20, 22);
          break;
        case IconPopType.AnimalEatFood:
          this.DrawRect = new Rectangle(428, 706, 25, 21);
          break;
        case IconPopType.GetEnergy:
          this.DrawRect = new Rectangle(407, 802, 19, 22);
          break;
        case IconPopType.JoinedQueue:
          this.DrawRect = new Rectangle(404, 706, 23, 23);
          break;
        case IconPopType.LeftQueue:
          this.DrawRect = new Rectangle(428, 801, 22, 23);
          break;
        case IconPopType.Damage:
          this.DrawRect = new Rectangle(0, 0, 0, 0);
          this.DriftSpeed = (float) TinyZoo.Game1.Rnd.Next(-100, 100);
          this.DriftSpeed *= 0.01f;
          break;
        case IconPopType.AttackFlash:
          this.DrawRect = new Rectangle(1385, 129, 22, 23);
          this.SetAlpha(false, 0.4f, 1f, 0.0f);
          break;
        case IconPopType.FightStart:
          this.DrawRect = new Rectangle(1994, 0, 54, 58);
          this.TexturePointer = AssetContainer.AnimalSheet;
          this.DriftSpeed = 0.0f;
          this.SetAlpha(false, 0.4f, 1f, 0.0f);
          this.poper.SetComplete();
          draworiginposition = DrawOriginPosition.Centre;
          this.scale = 1f;
          this.BaseScale = 1f;
          break;
        case IconPopType.Disgusted:
          this.DrawRect = new Rectangle(588, 287, 14, 15);
          this.scale = 2f;
          this.BaseScale = 2f;
          break;
        case IconPopType.Bored:
          this.DrawRect = new Rectangle(573, 287, 14, 15);
          this.scale = 2f;
          this.BaseScale = 2f;
          break;
        case IconPopType.Painting:
          this.DrawRect = new Rectangle(450, 799, 21, 22);
          break;
        case IconPopType.SeeAnimalsSmallLove:
          this.DrawRect = new Rectangle(446, 824, 17, 14);
          this.scale = 2f;
          this.BaseScale = 2f;
          break;
        case IconPopType.Angry:
          this.DrawRect = new Rectangle(927, 372, 14, 15);
          this.scale = 2f;
          this.BaseScale = 2f;
          break;
        case IconPopType.Confused:
          this.DrawRect = new Rectangle(293, 426, 17, 17);
          this.scale = 2f;
          this.BaseScale = 2f;
          break;
        case IconPopType.Caffinated:
          this.DrawRect = new Rectangle(194, 483, 20, 22);
          break;
        case IconPopType.AddCO2:
          this.DrawRect = new Rectangle(36, 442, 27, 20);
          this.YOffsetForText = 5f * this.scale;
          this.XOffsetForText = 25f * this.scale;
          this.TextCOlour = Color.MediumPurple;
          break;
        case IconPopType.ReduceCO2:
          this.DrawRect = new Rectangle(0, 484, 26, 19);
          this.YOffsetForText = 5f * this.scale;
          this.XOffsetForText = 25f * this.scale;
          this.TextCOlour = Color.GreenYellow;
          break;
      }
      this.bActive = true;
      Location.Y -= (float) TinyZoo.Game1.Rnd.Next(15, 25);
      this.SetDrawOriginToPoint(draworiginposition);
      this.Timer = 0.0f;
      this.LocationInWorldSpace = Location;
    }

    public void SpawnFoodMoment(string DrawThis, Vector2 Location)
    {
      this.TexturePointer = AssetContainer.SpriteSheet;
      this.poptype = IconPopType.AnimalEatFood;
      this.SpawnPopIconMoment(DrawThis, Location, this.poptype);
    }

    public void SpawnBankNote(NoteType nottype, int _MoneyGive, string DrawThis, Vector2 Location)
    {
      this.TexturePointer = AssetContainer.SpriteSheet;
      this.poptype = IconPopType.Count;
      this.MoneyGive = _MoneyGive;
      Location.X += (float) TinyZoo.Game1.Rnd.Next(-10, 10);
      Location.Y += (float) TinyZoo.Game1.Rnd.Next(-5, 10);
      this.Given = false;
      this.Value = DrawThis;
      this.SetAlpha(1f);
      this.TextCOlour = Color.Goldenrod;
      this.bActive = true;
      this.BaseScale = 2f;
      this.YOffsetForText = 17f * this.scale;
      this.XOffsetForText = 0.0f;
      switch (nottype)
      {
        case NoteType.Bano01:
          this.DrawRect = new Rectangle(335, 179, 15, 15);
          break;
        case NoteType.Bank02:
          this.DrawRect = new Rectangle(312, 180, 22, 14);
          break;
        case NoteType.Bank03:
          this.DrawRect = new Rectangle(0, 397, 23, 15);
          break;
        case NoteType.TooExpensive:
          this.DrawRect = new Rectangle(927, 372, 14, 15);
          break;
      }
      this.SetDrawOriginToCentre();
      this.poper = new PopLerper();
      this.Timer = 0.0f;
      this.LocationInWorldSpace = Location;
    }

    public void UpdateBankNote(float DeltaTime, Player player)
    {
      if (!this.bActive)
        return;
      if (!this.Given)
      {
        this.Given = true;
        player.Stats.GiveCash(this.MoneyGive / 100, player, true, this.MoneyGive % 100);
      }
      this.UpdateColours(DeltaTime);
      int num = (int) this.poper.OnUpdate(DeltaTime);
      if ((double) this.Timer < 3.0)
      {
        this.Timer += DeltaTime;
        if ((double) this.Timer >= 3.0)
          this.SetAlpha(false, 0.5f, 1f, 0.0f);
      }
      if ((double) this.fAlpha == 0.0)
        this.bActive = false;
      if (this.poptype != IconPopType.Damage)
        return;
      this.VerticalDrift += DeltaTime;
      this.HorizontalDrift += DeltaTime * this.DriftSpeed;
    }

    public void DrawBankNote()
    {
      if (!this.bActive)
        return;
      Vector2 screenSpace = RenderMath.TranslateWorldSpaceToScreenSpace(this.LocationInWorldSpace);
      this.scale = this.BaseScale * this.poper.CurrentValue;
      if (this.DrawRect.Width > 0)
        this.Draw(AssetContainer.pointspritebatch01, this.TexturePointer, screenSpace);
      if (this.poptype == IconPopType.Damage)
        TextFunctions.DrawJustifiedText(this.Value, 2f * this.poper.CurrentValue, screenSpace + new Vector2(this.XOffsetForText * this.poper.CurrentValue, -this.YOffsetForText * this.poper.CurrentValue) + new Vector2(this.HorizontalDrift * 0.01f, (float) (-(double) this.VerticalDrift * 0.00999999977648258)), Color.White, this.fAlpha, AssetContainer.PixelNumWithBlackOutline, AssetContainer.pointspritebatch01);
      else
        TextFunctions.DrawJustifiedText(this.Value, 0.5f * this.poper.CurrentValue, screenSpace + new Vector2(this.XOffsetForText * this.poper.CurrentValue, -this.YOffsetForText * this.poper.CurrentValue), this.TextCOlour, this.fAlpha, AssetContainer.roundaboutFont, AssetContainer.pointspritebatch01, true);
    }
  }
}
