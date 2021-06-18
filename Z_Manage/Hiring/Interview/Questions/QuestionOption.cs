// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Manage.Hiring.Interview.Questions.QuestionOption
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.GenericUI;

namespace TinyZoo.Z_Manage.Hiring.Interview.Questions
{
  internal class QuestionOption
  {
    private SimpleTextBox textbox;
    private TextButton TBUTTN;
    private Vector2 Location;
    public int QuestionIndex;

    public QuestionOption(int QuestionOption, int Vertialc)
    {
      this.QuestionIndex = QuestionOption;
      this.textbox = new SimpleTextBox(InterviewManager.IQuestions.questions[QuestionOption].Question, WillLrp: false, textScale: 1f);
      this.textbox.text.AutoCompleteParagraph();
      this.TBUTTN = new TextButton(InterviewManager.IQuestions.questions[QuestionOption].Question, 250f * Sengine.ScreenRatioUpwardsMultiplier.Y);
      this.textbox.Location = new Vector2(512f, (float) (320 + Vertialc * 60));
      this.TBUTTN.vLocation = this.textbox.Location;
    }

    public bool UpdateQuestionOption(float DeltaTime, Player player, Vector2 Offset) => this.TBUTTN.UpdateTextButton(player, Offset, DeltaTime);

    public void DrawQuestionOption(Vector2 Offset) => this.TBUTTN.DrawTextButton(Offset + this.Location);
  }
}
