// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Quests.HeroQuests.QuestAction.QuestAction
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using TinyZoo.GenericUI;
using TinyZoo.PlayerDir.HeroQuests;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_Quests.HeroQuests.QuestAction
{
  internal class QuestAction
  {
    public Vector2 location;
    private float basescale;
    private UIScaleHelper scalehelper;
    private CustomerFrame frame;
    private Vector2 framescale;
    private Vector2 pad;
    private SimpleTextHandler body;
    private LittleSummaryButton button;

    public QuestAction(HeroQuestDescription quest, float basescale_, float forceToThisWidth = -1f)
    {
      this.basescale = basescale_;
      this.scalehelper = new UIScaleHelper(this.basescale);
      this.pad = this.scalehelper.DefaultBuffer;
      if (quest.heroquesttype != HEROQUESTTYPE.DonateMoney)
        throw new NotFiniteNumberException();
      string text = "Donate";
      string TextToWrite = "Donate to this cause";
      Rectangle rectangle = new Rectangle(889, 120, 20, 20);
      float num = this.scalehelper.ScaleX(280f);
      if ((double) forceToThisWidth > 0.0)
        num = forceToThisWidth;
      this.button = new LittleSummaryButton(LittleSummaryButtonType.Action_Neutral, _BaseScale: this.basescale);
      this.body = new SimpleTextHandler(TextToWrite, num - 3f * this.pad.X - this.button.GetSize().X, _Scale: this.basescale);
      this.body.SetAllColours(ColourData.Z_Cream);
      this.body.AutoCompleteParagraph();
      this.frame = new CustomerFrame(Vector2.Zero, true, this.basescale);
      this.frame.AddMiniHeading(text);
      this.framescale = 2f * this.pad;
      this.framescale.Y += this.frame.GetMiniHeadingHeight();
      this.framescale += this.body.GetSize();
      this.framescale.X += this.pad.X + this.button.GetSize().X;
      this.frame.Resize(this.framescale);
      Vector2 vector2 = -0.5f * this.framescale + this.pad;
      vector2.Y += this.frame.GetMiniHeadingHeight();
      this.body.Location = vector2;
      vector2.X += this.body.GetSize().X + this.pad.X;
      this.button.vLocation = vector2 + 0.5f * this.button.GetSize();
      this.button.vLocation.Y = 0.0f;
    }

    public Vector2 GetSize() => this.framescale;

    public bool UpdateQuestAction(Player player, Vector2 offset, float DeltaTime)
    {
      offset += this.location;
      return (0 | (this.button.UpdateLittleSummaryButton(DeltaTime, player, offset) ? 1 : 0)) != 0;
    }

    public void DrawQuestAction(SpriteBatch spritebatch, Vector2 offset)
    {
      offset += this.location;
      this.frame.DrawCustomerFrame(offset, spritebatch);
      this.body.DrawSimpleTextHandler(offset, 1f, spritebatch);
      this.button.DrawLittleSummaryButton(offset, spritebatch);
    }
  }
}
