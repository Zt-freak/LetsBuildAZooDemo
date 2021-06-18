// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Manage.Hiring.Interview.Questions.QuestionSelect
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using System.Collections.Generic;
using TinyZoo.GenericUI;
using TinyZoo.PlayerDir.employees;

namespace TinyZoo.Z_Manage.Hiring.Interview.Questions
{
  internal class QuestionSelect
  {
    private LerpHandler_Float lerper;
    private List<QuestionOption> Questionoptions;
    private bool Exiting;
    private int RemainingQuestions;
    private GameObjectNineSlice frame;
    private Color TextColour;
    private string Name;
    private int SelectedIndex;

    public QuestionSelect(
      ref int QuestionIndex,
      ref List<int> QuestionsUsed,
      PotentialHire potentialhire)
    {
      this.Name = potentialhire.intakeperson.Name;
      this.RemainingQuestions = 2 - QuestionIndex;
      this.lerper = new LerpHandler_Float();
      this.lerper.SetLerp(true, 1f, 0.0f, 3f);
      ++QuestionIndex;
      this.Questionoptions = new List<QuestionOption>();
      int index1 = TinyZoo.Game1.Rnd.Next(0, QuestionsUsed.Count);
      this.Questionoptions.Add(new QuestionOption(QuestionsUsed[index1], 0));
      int index2 = index1;
      while (index2 == index1)
      {
        index2 = TinyZoo.Game1.Rnd.Next(0, QuestionsUsed.Count);
        if (index2 != index1)
          this.Questionoptions.Add(new QuestionOption(QuestionsUsed[index2], 1));
      }
      Vector3 SecondaryColour;
      this.frame = new GameObjectNineSlice(StringInBox.GetFrameColourRect(BTNColour.Cream, out SecondaryColour), 7);
      this.frame.scale = 2f;
      this.TextColour = new Color(SecondaryColour);
    }

    public int UpdateQuestionSelect(
      Player player,
      float DeltaTime,
      Vector2 Offset,
      ref List<int> QuestionsUsed)
    {
      int num = -1;
      this.lerper.UpdateLerpHandler(DeltaTime);
      Offset.X += this.lerper.Value * 1024f;
      if ((double) this.lerper.Value == 0.0)
      {
        for (int index = 0; index < this.Questionoptions.Count; ++index)
        {
          if (this.Questionoptions[index].UpdateQuestionOption(DeltaTime, player, Offset) && !this.Exiting)
          {
            this.Exiting = true;
            this.SelectedIndex = index;
            num = this.Questionoptions[index].QuestionIndex;
          }
        }
      }
      return num;
    }

    public QuestionOption GetQuestionOption() => this.Questionoptions[this.SelectedIndex];

    public void DrawQuestionSelect(Vector2 Offset)
    {
      Offset.X += this.lerper.Value * 1024f;
      this.frame.vLocation.X = 512f;
      this.frame.vLocation.Y = 340f;
      this.frame.DrawGameObjectNineSlice(AssetContainer.pointspritebatch03, AssetContainer.SpriteSheet, Offset + new Vector2(0.0f, -20f), new Vector2(900f, 200f));
      TextFunctions.DrawTextWithDropShadow("Pick a question to ask this candidate", 2f, new Vector2(100f, 230f) + Offset, this.TextColour, 1f, AssetContainer.springFont, AssetContainer.pointspritebatchTop05, false);
      TextFunctions.DrawTextWithDropShadow("Remaining Questions: " + (object) this.RemainingQuestions, 2f, new Vector2(100f, 250f) + Offset, this.TextColour, 1f, AssetContainer.springFont, AssetContainer.pointspritebatchTop05, false);
      for (int index = 0; index < this.Questionoptions.Count; ++index)
        this.Questionoptions[index].DrawQuestionOption(Offset);
    }
  }
}
