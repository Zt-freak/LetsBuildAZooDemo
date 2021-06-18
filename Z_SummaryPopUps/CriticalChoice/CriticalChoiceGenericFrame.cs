// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.CriticalChoice.CriticalChoiceGenericFrame
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using System.Collections.Generic;
using TinyZoo.Audio;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.GenericUI;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_Research_.RData;
using TinyZoo.Z_SummaryPopUps.EventReport.CriticalChoiceData;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_SummaryPopUps.CriticalChoice
{
  internal class CriticalChoiceGenericFrame : CriticalChoiceFrame
  {
    private static Rectangle baserect = new Rectangle(456, 1025, 532, 424);
    private static Rectangle painterrect = new Rectangle(594, 777, 48, 48);
    private static Rectangle scientistrect = new Rectangle(594, 728, 48, 48);
    private static Rectangle genomeguyrect = new Rectangle(284, 488, 48, 48);
    private static Rectangle zebrarect = new Rectangle(531, 460, 119, 35);
    private static Rectangle paintrect = new Rectangle(512, 477, 18, 18);
    private static Rectangle genomerect = new Rectangle(208, 203, 145, 44);
    private static Rectangle rabbitsnakerect = new Rectangle(147, 178, 47, 19);
    private static Rectangle rabbithipporect = new Rectangle(89, 178, 57, 19);
    private static Rectangle sciencerect1 = new Rectangle(89, 198, 118, 49);
    private static Rectangle sciencerect2 = new Rectangle(101, 107, 80, 29);
    private ZGenericText heading;
    private ZGenericText subheading;
    private PortraitRow portraitrow;
    private ZGenericUIDrawObject paint;
    private ZGenericUIDrawObject science1;
    private ZGenericUIDrawObject science2;
    private ZGenericUIDrawObject genome;
    private ZGenericUIDrawObject rabbitsnake;
    private ZGenericUIDrawObject rabbithippo;
    private ZGenericUIDrawObject portrait;
    private SimpleTextHandler research;
    private SimpleTextHandler text;
    private List<CriticalChoicePick> choices;
    private GameObject headingline;
    private GameObject choicesline;
    private GameObject vertline;
    private ZGenericText name;
    private ZGenericText bottomtext;
    private ZGenericUIDrawObject baseobj;
    private CriticalChoiceSet criticalchoiceset;

    public CriticalChoiceGenericFrame(CriticalChoiceSet criticalchoiceset_, float basescale_)
      : base(basescale_)
    {
      this.criticalchoiceset = criticalchoiceset_;
      this.heading = new ZGenericText(this.criticalchoiceset.HeadingText, 2f * this.basescale, false, _UseOnePointFiveFont: true);
      this.heading.SetAllColours(ColourData.Z_DarkTextGray);
      this.subheading = new ZGenericText(this.criticalchoiceset.ObjectiveText, this.basescale, false, _UseOnePointFiveFont: true);
      this.subheading.SetAllColours(ColourData.Z_DarkTextGray);
      this.text = new SimpleTextHandler(this.criticalchoiceset.BodyText, this.scalehelper.ScaleX(320f), _Scale: this.basescale);
      this.text.SetAllColours(ColourData.Z_DarkTextGray);
      this.text.AutoCompleteParagraph();
      Rectangle drawrect = new Rectangle();
      this.headingline = new GameObject();
      this.headingline.DrawRect = TinyZoo.Game1.WhitePixelRect;
      this.headingline.SetAllColours(ColourData.Z_DarkTextGray);
      this.headingline.SetDrawOriginToPoint(DrawOriginPosition.CentreLeft);
      this.choicesline = new GameObject();
      this.choicesline.DrawRect = TinyZoo.Game1.WhitePixelRect;
      this.choicesline.SetAllColours(ColourData.Z_DarkTextGray);
      this.choicesline.SetDrawOriginToPoint(DrawOriginPosition.Centre);
      this.name = new ZGenericText(CriticalChoiceAction.GetCriticalChoiceCharacterToString(this.criticalchoiceset.criticalcharacter), this.basescale, _UseOnePointFiveFont: true);
      this.name.SetAllColours(ColourData.Z_DarkTextGray);
      this.vertline = new GameObject();
      this.vertline.DrawRect = TinyZoo.Game1.WhitePixelRect;
      this.vertline.SetAllColours(ColourData.Z_DarkTextGray);
      this.vertline.SetDrawOriginToPoint(DrawOriginPosition.CentreTop);
      this.bottomtext = new ZGenericText("Critical Choice - You have to make a decision", this.basescale, _UseOnePointFiveFont: true);
      this.bottomtext.SetAllColours(ColourData.Z_TextRed);
      switch (this.criticalchoiceset.criticalcharacter)
      {
        case CriticalChoiceCharacter.Scientist:
          drawrect = CriticalChoiceGenericFrame.scientistrect;
          this.research = new SimpleTextHandler("Research unlocks new features for your zoo.", this.scalehelper.ScaleX(150f), _Scale: this.basescale);
          this.research.SetAllColours(ColourData.Z_DarkTextGray);
          this.research.AutoCompleteParagraph();
          this.science1 = new ZGenericUIDrawObject(CriticalChoiceGenericFrame.sciencerect1, 2f * this.basescale, AssetContainer.UISheet);
          this.science2 = new ZGenericUIDrawObject(CriticalChoiceGenericFrame.sciencerect2, 2f * this.basescale, AssetContainer.UISheet);
          break;
        case CriticalChoiceCharacter.Painter:
          drawrect = CriticalChoiceGenericFrame.painterrect;
          this.portraitrow = new PortraitRow(8, this.basescale, drawframe_: true);
          this.portraitrow.Add(AnimalType.Horse, AnimalType.None);
          this.portraitrow.Add("+", true, 2f);
          this.portraitrow.Add(" ", false);
          this.portraitrow.Add("=", true, 2f);
          this.portraitrow.Add(AnimalType.Zebra, AnimalType.None);
          this.paint = new ZGenericUIDrawObject(CriticalChoiceGenericFrame.paintrect, 2f * this.basescale, AssetContainer.UISheet);
          break;
        case CriticalChoiceCharacter.GenomeGuy:
          drawrect = CriticalChoiceGenericFrame.genomeguyrect;
          this.research = new SimpleTextHandler("Genomes can be spliced to create hybrids", this.scalehelper.ScaleX(150f), _Scale: this.basescale);
          this.research.SetAllColours(ColourData.Z_DarkTextGray);
          this.research.AutoCompleteParagraph();
          this.genome = new ZGenericUIDrawObject(CriticalChoiceGenericFrame.genomerect, 2f * this.basescale, AssetContainer.UISheet);
          this.science2 = new ZGenericUIDrawObject(CriticalChoiceGenericFrame.sciencerect2, 2f * this.basescale, AssetContainer.UISheet);
          this.rabbithippo = new ZGenericUIDrawObject(CriticalChoiceGenericFrame.rabbithipporect, 2f * this.basescale, AssetContainer.UISheet);
          this.rabbitsnake = new ZGenericUIDrawObject(CriticalChoiceGenericFrame.rabbitsnakerect, 2f * this.basescale, AssetContainer.UISheet);
          break;
      }
      this.portrait = new ZGenericUIDrawObject(drawrect, 2f * this.basescale, AssetContainer.SpriteSheet);
      List<CriticalChoiceAction> criticalActions = this.criticalchoiceset.CriticalActions;
      this.choices = new List<CriticalChoicePick>();
      for (int index = 0; index < this.criticalchoiceset.CriticalActions.Count; ++index)
      {
        CriticalChoiceAction criticalAction = this.criticalchoiceset.CriticalActions[index];
        this.choices.Add(new CriticalChoicePick(criticalAction.moralitytype, criticalAction.GetButtonLabel(), criticalAction.TEXXXT, this.basescale));
      }
      if (this.choices.Count > 0)
        this.choices[0].ControllerSelected = true;
      this.baseobj = new ZGenericUIDrawObject(CriticalChoiceGenericFrame.baserect, this.basescale, AssetContainer.UISheet);
      this.framescale = this.baseobj.GetSize();
      this.framescale = this.framescale + 2f * this.pad;
      this.frame = new CustomerFrame(this.framescale, BaseScale: this.basescale);
      Vector2 vector2 = -0.5f * this.framescale + 4f * this.pad;
      this.heading.vLocation = vector2;
      vector2.Y += this.heading.GetSize().Y;
      this.headingline.vLocation = vector2;
      vector2.Y += 0.5f * this.pad.Y;
      this.subheading.vLocation = vector2;
      vector2.Y += this.subheading.GetSize().Y + 0.5f * this.pad.Y;
      this.text.Location = vector2;
      vector2.Y += this.text.GetSize().Y;
      vector2.X = 0.0f;
      vector2.Y += this.scalehelper.ScaleY(200f);
      this.choices[0].location.Y = (float) (0.5 * (double) this.framescale.Y - 0.5 * (double) this.choices[0].GetSize().Y - 5.0 * (double) this.pad.Y);
      this.choices[1].location.Y = (float) (0.5 * (double) this.framescale.Y - 0.5 * (double) this.choices[1].GetSize().Y - 5.0 * (double) this.pad.Y);
      this.choices[0].location.X = (float) (-0.5 * (double) this.choices[0].GetSize().X - 2.0 * (double) this.pad.X);
      this.choices[1].location.X = (float) (0.5 * (double) this.choices[1].GetSize().X + 2.0 * (double) this.pad.X);
      this.bottomtext.vLocation = this.choices[0].location;
      this.bottomtext.vLocation.Y += (float) (0.5 * (double) this.choices[0].GetSize().Y + 0.5 * (double) this.bottomtext.GetSize().Y) + this.pad.Y;
      this.bottomtext.vLocation.X = 0.0f;
      this.choicesline.vLocation = (this.choices[0].location + this.choices[1].location) * 0.5f;
      this.choicesline.vLocation.Y += -0.5f * this.choices[0].GetSize().Y - this.pad.Y;
      this.vertline.vLocation = this.choicesline.vLocation;
      this.portrait.location = 0.5f * new Vector2(this.framescale.X, -this.framescale.Y) + this.scalehelper.ScaleVector2(new Vector2(-110f, 96f));
      this.name.vLocation = this.portrait.location;
      this.name.vLocation.Y += this.scalehelper.ScaleY(70f);
      switch (this.criticalchoiceset.criticalcharacter)
      {
        case CriticalChoiceCharacter.Scientist:
          this.research.Location = this.scalehelper.ScaleVector2(new Vector2(65f, -12f));
          this.science1.location = this.scalehelper.ScaleVector2(new Vector2(-100f, -25f));
          this.science2.location = this.scalehelper.ScaleVector2(new Vector2(135f, -10f));
          break;
        case CriticalChoiceCharacter.Painter:
          this.portraitrow.location = Vector2.Zero;
          break;
        case CriticalChoiceCharacter.GenomeGuy:
          this.research.Location = this.scalehelper.ScaleVector2(new Vector2(65f, -12f));
          this.genome.location = this.scalehelper.ScaleVector2(new Vector2(-110f, -20f));
          this.science2.location = this.scalehelper.ScaleVector2(new Vector2(135f, -10f));
          this.rabbitsnake.location = this.choices[0].location;
          this.rabbitsnake.location.Y += 0.5f * this.pad.Y;
          this.rabbithippo.location = this.choices[1].location;
          this.rabbithippo.location.Y += 0.5f * this.pad.Y;
          break;
      }
    }

    private BTNColour GetBTNColour(StarColour morality)
    {
      BTNColour btnColour = BTNColour.CriticalNeutralBlue;
      switch (morality)
      {
        case StarColour.Good_Yellow:
          btnColour = BTNColour.CriticalGoodYellow;
          break;
        case StarColour.Evil_Purple:
          btnColour = BTNColour.CriticalEvilPurple;
          break;
        case StarColour.Neutral:
          btnColour = BTNColour.CriticalNeutralBlue;
          break;
      }
      return btnColour;
    }

    public override bool UpdateCriticalChoiceFrame(
      Player player,
      Vector2 offset,
      float DeltaTime,
      out int choice)
    {
      offset += this.location;
      bool flag = false;
      choice = 0;
      if (player.inputmap.HeldButtons[6] || player.inputmap.HeldButtons[19])
      {
        if (this.choices[0].ControllerSelected)
        {
          this.choices[0].ControllerSelected = false;
          this.choices[1].ControllerSelected = true;
          SoundEffectsManager.PlaySpecificSound(SoundEffectType.ClickSingle, 0.2f, 1f);
        }
      }
      else if ((player.inputmap.HeldButtons[5] || player.inputmap.HeldButtons[18]) && this.choices[1].ControllerSelected)
      {
        this.choices[1].ControllerSelected = false;
        this.choices[0].ControllerSelected = true;
        SoundEffectsManager.PlaySpecificSound(SoundEffectType.ClickSingle, 0.2f, 1f);
      }
      for (int index = 0; index < this.choices.Count; ++index)
      {
        if (this.choices[index].UpdateCriticalChoicePick(player, offset, DeltaTime))
        {
          flag = true;
          choice = index;
          break;
        }
      }
      return flag;
    }

    public override void DrawCriticalChoiceFrame(SpriteBatch spritebatch, Vector2 offset)
    {
      offset += this.location;
      this.frame.DrawCustomerFrame(offset, spritebatch);
      this.baseobj.DrawZGenericUIDrawObject(spritebatch, offset);
      this.heading.DrawZGenericText(offset, spritebatch);
      this.subheading.DrawZGenericText(offset, spritebatch);
      this.headingline.Draw(spritebatch, AssetContainer.SpriteSheet, offset, this.scalehelper.ScaleVector2(new Vector2(320f, 1f)));
      this.choicesline.Draw(spritebatch, AssetContainer.SpriteSheet, offset, this.scalehelper.ScaleVector2(new Vector2(480f, 1f)));
      this.vertline.Draw(spritebatch, AssetContainer.SpriteSheet, offset, this.scalehelper.ScaleVector2(new Vector2(1f, 140f)));
      this.bottomtext.DrawZGenericText(offset, spritebatch);
      for (int index = 0; index < this.choices.Count; ++index)
        this.choices[index].DrawCriticalChoicePick(spritebatch, offset);
      this.portrait.DrawZGenericUIDrawObject(spritebatch, offset);
      this.name.DrawZGenericText(offset, spritebatch);
      switch (this.criticalchoiceset.criticalcharacter)
      {
        case CriticalChoiceCharacter.Scientist:
          this.research.DrawSimpleTextHandler(offset, 1f, spritebatch);
          this.science1.DrawZGenericUIDrawObject(spritebatch, offset);
          this.science2.DrawZGenericUIDrawObject(spritebatch, offset);
          break;
        case CriticalChoiceCharacter.Painter:
          this.portraitrow.DrawPortraitRow(spritebatch, offset);
          this.paint.DrawZGenericUIDrawObject(spritebatch, offset);
          break;
        case CriticalChoiceCharacter.GenomeGuy:
          this.research.DrawSimpleTextHandler(offset, 1f, spritebatch);
          this.genome.DrawZGenericUIDrawObject(spritebatch, offset);
          this.rabbitsnake.DrawZGenericUIDrawObject(spritebatch, offset);
          this.rabbithippo.DrawZGenericUIDrawObject(spritebatch, offset);
          this.science2.DrawZGenericUIDrawObject(spritebatch, offset);
          break;
      }
      this.text.DrawSimpleTextHandler(offset, 1f, spritebatch);
    }
  }
}
