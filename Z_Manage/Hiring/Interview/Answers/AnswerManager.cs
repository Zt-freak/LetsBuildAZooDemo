// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Manage.Hiring.Interview.Answers.AnswerManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.GenericUI;
using TinyZoo.PlayerDir.employees;
using TinyZoo.Z_Manage.Hiring.Interview.Questions;

namespace TinyZoo.Z_Manage.Hiring.Interview.Answers
{
  internal class AnswerManager
  {
    private GameObjectNineSlice frame;
    private Color TextColour;
    private LerpHandler_Float lerper;
    private SimpleTextHandler text;
    private QuestionOption Ref_questionoption;
    private TextButton OKBtn;

    public AnswerManager(
      int QuestionIndex,
      PotentialHire Ref_potentialhire,
      QuestionOption questionoption)
    {
      this.OKBtn = new TextButton("Next");
      this.OKBtn.vLocation = new Vector2(900f, 700f);
      this.Ref_questionoption = questionoption;
      this.lerper = new LerpHandler_Float();
      this.lerper.SetLerp(true, 1f, 0.0f, 3f);
      Vector3 SecondaryColour;
      this.frame = new GameObjectNineSlice(StringInBox.GetFrameColourRect(BTNColour.Cream, out SecondaryColour), 7);
      this.frame.scale = 2f;
      this.TextColour = new Color(SecondaryColour);
      this.text = new SimpleTextHandler(InterviewManager.IQuestions.questions[QuestionIndex].Answers[Ref_potentialhire.employeestats.GetGreedAnswerIndex()].AnswerOption, false, 0.8f, 2f, false, false);
      this.text.paragraph.linemaker.SetAllColours(SecondaryColour);
    }

    public bool UpdateAnswerManager(float DeltaTime, Player player)
    {
      this.lerper.UpdateLerpHandler(DeltaTime);
      this.text.UpdateSimpleTextHandler(DeltaTime);
      return (double) this.lerper.Value == 0.0 && this.OKBtn.UpdateTextButton(player, Vector2.Zero, DeltaTime);
    }

    public void DrawAnswerManager(Vector2 Offset)
    {
      Offset.X += this.lerper.Value * 1024f;
      this.frame.vLocation.X = 512f;
      this.frame.vLocation.Y = 300f;
      this.frame.DrawGameObjectNineSlice(AssetContainer.pointspritebatch03, AssetContainer.SpriteSheet, Offset, new Vector2(900f, 300f));
      this.text.DrawSimpleTextHandler(Offset + new Vector2(102.4f, 200f));
      this.OKBtn.DrawTextButton(Offset);
    }
  }
}
