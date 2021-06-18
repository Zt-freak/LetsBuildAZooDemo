// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_HUD.TopBar.MoralityPopUp.Unlocks.MoralityUnlockPopOut
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using TinyZoo.GenericUI;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_MoralitySummary;
using TinyZoo.Z_SummaryPopUps.People.Animal.Shared;

namespace TinyZoo.Z_HUD.TopBar.MoralityPopUp.Unlocks
{
  internal class MoralityUnlockPopOut : GenericTopBarPopOutFrame
  {
    private MiniHeading miniHeading;
    private List<MoralityUnlocksFrame> frames;
    private List<MoralityHeader> headers;
    private BackButton previousButton;
    private new float BaseScale;
    private Vector2 buffer;

    public MoralityUnlockPopOut(Player player, float _BaseScale)
      : base(_BaseScale)
    {
      this.BaseScale = _BaseScale;
      this.buffer = new UIScaleHelper(this.BaseScale).DefaultBuffer;
      this.miniHeading = new MiniHeading(Vector2.Zero, "Unlocks", 1f, this.BaseScale);
      this.frames = new List<MoralityUnlocksFrame>();
      this.headers = new List<MoralityHeader>();
      Vector2 zero = Vector2.Zero;
      zero.Y += this.buffer.Y + this.miniHeading.GetTextHeight(true);
      zero.Y += this.buffer.Y;
      bool flag = (double) player.livestats.MoralityScore >= 0.0;
      for (int index = 0; index < 2; ++index)
      {
        bool isGoodNotEvil = index != 0 ? !flag : flag;
        string empty = string.Empty;
        string text;
        float morality_negEvilposGood;
        if (isGoodNotEvil)
        {
          text = "Good";
          morality_negEvilposGood = 1f;
          if (flag && (double) player.livestats.MoralityScore != 0.0)
            morality_negEvilposGood = player.livestats.MoralityScore;
        }
        else
        {
          text = "Evil";
          morality_negEvilposGood = -1f;
          if (!flag)
            morality_negEvilposGood = player.livestats.MoralityScore;
        }
        MoralityUnlocksFrame moralityUnlocksFrame = new MoralityUnlocksFrame(isGoodNotEvil, player, this.BaseScale);
        MoralityHeader moralityHeader = new MoralityHeader(morality_negEvilposGood, text, this.BaseScale, moralityUnlocksFrame.GetSize().X, isGoodNotEvil == flag && (double) player.livestats.MoralityScore != 0.0);
        moralityHeader.location = zero;
        moralityHeader.location.Y += moralityHeader.GetSize().Y * 0.5f;
        zero.Y += moralityHeader.GetSize().Y + this.buffer.Y;
        moralityUnlocksFrame.location = zero;
        moralityUnlocksFrame.location.Y += moralityUnlocksFrame.GetSize().Y * 0.5f;
        zero.Y += moralityUnlocksFrame.GetSize().Y;
        zero.Y += this.buffer.Y;
        this.frames.Add(moralityUnlocksFrame);
        this.headers.Add(moralityHeader);
      }
      zero.X = this.frames[0].GetSize().X + this.buffer.X * 2f;
      this.FinalizeSize(zero, ColourData.Z_FrameMidBrown, 1f);
      this.miniHeading.SetTextPosition(zero);
      Vector2 vector2 = -zero * 0.5f;
      for (int index = 0; index < this.frames.Count; ++index)
        this.frames[index].location.Y += vector2.Y;
      for (int index = 0; index < this.headers.Count; ++index)
        this.headers[index].location.Y += vector2.Y;
    }

    public new void AddPreviousButton()
    {
      this.previousButton = new BackButton(true, BaseScale: this.BaseScale, _IsPrevious: true);
      this.previousButton.vLocation = new Vector2(this.GetSize().X * 0.5f, (float) (-(double) this.GetSize().Y * 0.5));
      BackButton previousButton1 = this.previousButton;
      previousButton1.vLocation = previousButton1.vLocation + new Vector2((float) (-(double) this.previousButton.GetSize().X * 0.5), this.previousButton.GetSize().Y * 0.5f);
      BackButton previousButton2 = this.previousButton;
      previousButton2.vLocation = previousButton2.vLocation + new Vector2(-this.buffer.X, this.buffer.Y * 0.5f);
    }

    public void RefreshValues(Player player)
    {
      for (int index = 0; index < this.frames.Count; ++index)
        this.frames[index].RefreshValues(player);
    }

    public bool UpdateMoralityUnlockPopOut(Player player, float DeltaTime, Vector2 offset)
    {
      this.UpdatePopOutFrame(player, DeltaTime, ref offset);
      for (int index = 0; index < this.frames.Count; ++index)
        this.frames[index].UpdateMoralityUnlocksFrame(player, DeltaTime, offset);
      return this.previousButton != null && this.previousButton.UpdateBackButton(player, DeltaTime, offset);
    }

    public void DrawMoralityUnlockPopOut(Vector2 offset, SpriteBatch spriteBatch)
    {
      this.DrawPopOutFrame(ref offset, spriteBatch);
      for (int index = 0; index < this.frames.Count; ++index)
        this.frames[index].DrawMoralityUnlocksFrame(offset, spriteBatch);
      this.miniHeading.DrawMiniHeading(offset, spriteBatch);
      if (this.previousButton != null)
        this.previousButton.DrawBackButton(offset, spriteBatch);
      for (int index = 0; index < this.headers.Count; ++index)
        this.headers[index].DrawMoralityHeader(spriteBatch, offset);
    }
  }
}
