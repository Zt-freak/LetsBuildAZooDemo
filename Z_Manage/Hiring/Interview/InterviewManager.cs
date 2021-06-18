// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Manage.Hiring.Interview.InterviewManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using System.Collections.Generic;
using TinyZoo.GenericUI;
using TinyZoo.PlayerDir.employees;
using TinyZoo.Z_Manage.Hiring.Interview.Answers;
using TinyZoo.Z_Manage.Hiring.Interview.Office;
using TinyZoo.Z_Manage.Hiring.Interview.Questions;
using TinyZoo.Z_Manage.Hiring.Negotiation;

namespace TinyZoo.Z_Manage.Hiring.Interview
{
  internal class InterviewManager
  {
    private BackButton close;
    private OfficeEnvironment officeenvironment;
    private LerpHandler_Float lerper;
    private QuestionSelect questionselect;
    private NegotiationManager negotiationmanager;
    private bool AskingQuestions;
    private bool AnsweringQuestions;
    private bool WalkingIn;
    private float WalkIngOffset;
    private int Question;
    private ScreenHeading screenheading;
    private List<int> QuestionsUsed;
    private AnswerManager answermanager;
    internal static InterviwQuestionsData IQuestions;
    private PotentialHire Ref_potentialhire;
    public bool newPersonGotJob;

    public InterviewManager(PotentialHire potentialhire, Player player)
    {
      this.Ref_potentialhire = potentialhire;
      InterviewManager.IQuestions = new InterviwQuestionsData();
      this.Question = 0;
      this.screenheading = new ScreenHeading("Interview: " + potentialhire.intakeperson.Name, 100f);
      this.WalkIngOffset = 100f;
      this.lerper = new LerpHandler_Float();
      this.lerper.SetLerp(true, 1f, 0.0f, 3f);
      this.officeenvironment = new OfficeEnvironment(potentialhire, player);
      this.QuestionsUsed = new List<int>();
      for (int index = 0; index < InterviewManager.IQuestions.questions.Length; ++index)
        this.QuestionsUsed.Add(index);
    }

    public void Exit()
    {
      if ((double) this.lerper.TargetValue == 1.0)
        return;
      this.lerper.SetLerp(true, 0.0f, 1f, 3f);
    }

    public bool UpdateInterviewManager(Player player, float DeltaTime, Vector2 Offset)
    {
      if ((double) this.WalkIngOffset > 0.0)
      {
        this.WalkIngOffset -= DeltaTime * 170f;
        if ((double) this.WalkIngOffset < 0.0)
        {
          this.WalkIngOffset = 0.0f;
          this.questionselect = new QuestionSelect(ref this.Question, ref this.QuestionsUsed, this.Ref_potentialhire);
          this.AskingQuestions = true;
        }
      }
      this.lerper.UpdateLerpHandler(DeltaTime);
      Offset.X += this.lerper.Value * 1024f;
      if (this.questionselect != null)
      {
        int QuestionIndex = this.questionselect.UpdateQuestionSelect(player, DeltaTime, Offset + new Vector2(0.0f, -50f), ref this.QuestionsUsed);
        if (QuestionIndex > -1)
        {
          this.answermanager = new AnswerManager(QuestionIndex, this.Ref_potentialhire, this.questionselect.GetQuestionOption());
          this.QuestionsUsed.Remove(QuestionIndex);
          this.AskingQuestions = false;
          this.AnsweringQuestions = true;
          this.questionselect = (QuestionSelect) null;
        }
      }
      if (this.answermanager != null && this.answermanager.UpdateAnswerManager(DeltaTime, player))
      {
        if (this.Question < 2)
        {
          this.questionselect = new QuestionSelect(ref this.Question, ref this.QuestionsUsed, this.Ref_potentialhire);
          this.AskingQuestions = true;
          this.AnsweringQuestions = false;
        }
        else
          this.negotiationmanager = new NegotiationManager(this.Ref_potentialhire);
        this.answermanager = (AnswerManager) null;
      }
      if (this.negotiationmanager != null && this.negotiationmanager.UpdateNegotiationManager(player, DeltaTime, ref this.newPersonGotJob))
        this.Exit();
      this.lerper.UpdateLerpHandler(DeltaTime);
      this.officeenvironment.UpdateOfficeEnvironment(DeltaTime);
      return (double) this.lerper.Value == 1.0 && (double) this.lerper.TargetValue == 1.0;
    }

    public void DrawInterviewManager(Vector2 Offset)
    {
      Offset.X += this.lerper.Value * 1024f;
      this.screenheading.DrawScreenHeading(Offset, AssetContainer.pointspritebatchTop05);
      this.officeenvironment.DrawOfficeEnvironment(Offset, this.AskingQuestions, this.AnsweringQuestions, this.WalkIngOffset);
      if (this.questionselect != null)
        this.questionselect.DrawQuestionSelect(Offset + new Vector2(0.0f, -50f));
      if (this.answermanager != null)
        this.answermanager.DrawAnswerManager(Offset);
      if (this.negotiationmanager == null)
        return;
      this.negotiationmanager.DrawNegotiationManager(Offset);
    }
  }
}
