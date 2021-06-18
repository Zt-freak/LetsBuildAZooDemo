// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Manage.Hiring.Interview.Negotiation.OffserRejectOrAccept.OfferAcceptOrRejectManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using System;
using TinyZoo.GenericUI;
using TinyZoo.PlayerDir.employees;

namespace TinyZoo.Z_Manage.Hiring.Interview.Negotiation.OffserRejectOrAccept
{
  internal class OfferAcceptOrRejectManager
  {
    private SimpleTextBox CharacterIntrotextbox;
    private SimpleTextBox textbox;
    private LerpHandler_Float lerper;
    private string AcceptanceText;
    public bool NONEGOTIATIONPOSSIBLE;
    public bool GotJob;
    private TextButton NextButton;

    public OfferAcceptOrRejectManager(
      int OffserValue,
      PotentialHire REF_hirethisguy,
      bool IsFirstOffer)
    {
      int greedAnswerIndex = REF_hirethisguy.employeestats.GetGreedAnswerIndex();
      int num = REF_hirethisguy.employeestats.GetOfferToGreedLevel(OffserValue) - greedAnswerIndex;
      this.lerper = new LerpHandler_Float();
      this.lerper.SetLerp(true, 1f, 0.0f, 3f);
      int minimumWage = REF_hirethisguy.GetMinimumWage();
      if (OffserValue >= minimumWage && num < 0)
        throw new Exception("FIX THIS MONKEY FACED GLASSES WEARING MAN");
      if (OffserValue < minimumWage && num >= 0)
        num = -1;
      switch (num)
      {
        case -4:
          this.AcceptanceText = "Is this an insult?~You want me to leave!?~OK, I am done!";
          this.NONEGOTIATIONPOSSIBLE = true;
          break;
        case -3:
          this.AcceptanceText = "I heard you were tight!~But this is a bit ridiculous! I earn more from being a social influencer, and I only have 3 followers!";
          break;
        case -2:
          this.AcceptanceText = "I am sorry, but that's just a bit too low. I could get a second job, but I want to devote all of my time to the animals!";
          break;
        case -1:
          this.AcceptanceText = "OK, let me think about it...~...~... No, I think this offer is still slightly too low.";
          break;
        case 0:
          this.AcceptanceText = "I am not sure, I mean I want the job and everything, but.. This offer is kind of at my limits.~~Ahh, fine! I will take the job! You are a fine negotiator!";
          break;
        case 1:
          this.AcceptanceText = "Thanks for this job, it means so much to me! I think I might have a little bit of spare cash every month to spend playing Tiny Decks And Dungeons on my phone!";
          break;
        case 2:
          this.AcceptanceText = "I am so happy! And this is a slightly better offer than I was expecting! You are a generous employer!";
          if (TinyZoo.Game1.Rnd.Next(0, 5) < 2)
          {
            REF_hirethisguy.employeestats.Determination += TinyZoo.Game1.Rnd.Next(0, 100 - REF_hirethisguy.employeestats.Determination);
            break;
          }
          break;
        case 3:
          this.AcceptanceText = "Thank you! To be honest, this is more than I was expecting!~ Thanks, I will do my best for you and the zoo!";
          if (TinyZoo.Game1.Rnd.Next(0, 3) < 2)
          {
            REF_hirethisguy.employeestats.Determination += TinyZoo.Game1.Rnd.Next(0, 100 - REF_hirethisguy.employeestats.Determination);
            break;
          }
          break;
        case 4:
          this.AcceptanceText = "Wow! That's amazingly generous, I am quite taken back!~I can't quite believe you would offer so much!!~~I will be the best employee you have hired!";
          REF_hirethisguy.employeestats.Determination += TinyZoo.Game1.Rnd.Next(0, 100 - REF_hirethisguy.employeestats.Determination);
          break;
        case 5:
          this.AcceptanceText = "Wow! That's amazingly generous, I am quite taken back!~I can't quite believe you would offer so much!!~~I will be the best employee you have hired!";
          REF_hirethisguy.employeestats.Determination += TinyZoo.Game1.Rnd.Next(0, 100 - REF_hirethisguy.employeestats.Determination);
          break;
      }
      this.CharacterIntrotextbox = new SimpleTextBox(this.AcceptanceText, textScale: GameFlags.GetSmallTextScale());
      this.CharacterIntrotextbox.SetTextBoxToALternateColour(BTNColour.Cream);
      if (OffserValue >= REF_hirethisguy.GetMinimumWage())
      {
        this.GotJob = true;
        this.NextButton = new TextButton("Start Work!", 150f);
        this.textbox = new SimpleTextBox("SUCCESS!~~" + REF_hirethisguy.intakeperson.Name + " has accepted your offer of $" + (object) OffserValue + " per week!~They will start work immediately!", textScale: GameFlags.GetSmallTextScale());
        this.textbox.SetTextBoxToALternateColour(BTNColour.Green);
      }
      else
      {
        string TEXTTT = "Rejected!~~" + REF_hirethisguy.intakeperson.Name + " has rejected your offer.";
        if (this.NONEGOTIATIONPOSSIBLE)
          TEXTTT = "Rejected!~~" + REF_hirethisguy.intakeperson.Name + " has rejected your offer, and stormed out of the interview!";
        if (IsFirstOffer && !this.NONEGOTIATIONPOSSIBLE)
          TEXTTT += "~You have one chance to renegotiate.";
        if (!IsFirstOffer && !this.NONEGOTIATIONPOSSIBLE)
          TEXTTT += "~The interview is over.";
        this.textbox = new SimpleTextBox(TEXTTT, WillLrp: false, textScale: GameFlags.GetSmallTextScale());
        this.textbox.SetTextBoxToALternateColour(BTNColour.Grey);
        this.NextButton = new TextButton("Make Another Offer", 150f);
        if (!IsFirstOffer || this.NONEGOTIATIONPOSSIBLE)
          this.NextButton = new TextButton("Leave Interview", 150f);
        this.NextButton.SetButtonRed();
      }
    }

    public bool UpdateOfferAcceptOrRejectManager(float DeltaTime, Player player)
    {
      if (this.CharacterIntrotextbox != null)
      {
        this.CharacterIntrotextbox.UpdateSimpleTextBox(DeltaTime, player);
        if ((double) player.player.touchinput.ReleaseTapArray[0].X > 0.0 && this.CharacterIntrotextbox.text.TryToCompleteParagraph())
          this.CharacterIntrotextbox = (SimpleTextBox) null;
      }
      if (this.CharacterIntrotextbox == null)
      {
        this.lerper.UpdateLerpHandler(DeltaTime);
        this.textbox.UpdateSimpleTextBox(DeltaTime, player);
        if ((double) this.lerper.Value == 0.0 && this.NextButton.UpdateTextButton(player, Vector2.Zero, DeltaTime))
          return true;
      }
      return false;
    }

    public void DrawOfferAcceptOrRejectManager(Vector2 Offset)
    {
      if (this.CharacterIntrotextbox != null)
      {
        this.CharacterIntrotextbox.DrawSimpleTextBox(Offset + new Vector2(512f, 300f));
      }
      else
      {
        this.textbox.DrawSimpleTextBox(Offset + new Vector2(512f, 250f));
        this.NextButton.vLocation = new Vector2(512f, 400f);
        this.NextButton.DrawTextButton(new Vector2(this.lerper.Value * 1024f, 0.0f) + Offset, 1f, AssetContainer.pointspritebatchTop05);
      }
    }
  }
}
