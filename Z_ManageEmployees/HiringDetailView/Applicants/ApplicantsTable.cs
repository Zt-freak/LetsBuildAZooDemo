// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_ManageEmployees.HiringDetailView.Applicants.ApplicantsTable
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using TinyZoo.PlayerDir.employees.openpositions;
using TinyZoo.Z_BalanceSystems.Employees.JobApplicants;
using TinyZoo.Z_ManageEmployees.HiringDetailView.Applicants.Rows;

namespace TinyZoo.Z_ManageEmployees.HiringDetailView.Applicants
{
  internal class ApplicantsTable
  {
    public Vector2 location;
    private ApplicantRow headerRow;
    private ApplicantRow[] applicantRows;
    private float headerHeight;
    private float refBaseScale;
    private float refYbuffer;
    private float[] refWidths;
    private bool isAgencyInstantHire;
    private List<Applicant> applicantsToAdd;
    private List<int> IndexesToRemove;
    private List<int> GapIndexesToFill;

    public ApplicantsTable(
      List<Applicant> listOfApplicants,
      float BaseScale,
      float Ybuffer,
      ref float[] widths,
      bool _isAgencyInstantHire = false)
    {
      this.headerHeight = 0.0f;
      this.refBaseScale = BaseScale;
      this.refYbuffer = Ybuffer;
      this.refWidths = widths;
      this.isAgencyInstantHire = _isAgencyInstantHire;
      this.headerRow = new ApplicantRow((Applicant) null, BaseScale, ref widths, this.isAgencyInstantHire, true);
      this.headerRow.location.Y = this.headerHeight + this.headerRow.GetSize().Y * 0.5f;
      this.headerHeight += this.headerRow.GetSize().Y;
      this.applicantRows = new ApplicantRow[ApplicantGenerator.MaxApplicantsForDisplay];
      int num = Math.Min(this.applicantRows.Length, listOfApplicants.Count);
      for (int index = 0; index < num; ++index)
        this.AddRow(listOfApplicants[index], BaseScale, Ybuffer, ref widths, index, this.isAgencyInstantHire);
      this.applicantsToAdd = new List<Applicant>();
      this.IndexesToRemove = new List<int>();
      this.GapIndexesToFill = new List<int>();
    }

    public void AddRow(
      Applicant applicant,
      float BaseScale,
      float Ybuffer,
      ref float[] widths,
      int ForceListIndex = -1,
      bool isAgencyInstantHire = false,
      bool LerpInSmoothly = false)
    {
      ApplicantRow applicantRow = new ApplicantRow(applicant, BaseScale, ref widths, isAgencyInstantHire, LerpInSmoothly: LerpInSmoothly);
      applicantRow.location.Y = this.headerHeight;
      if (ForceListIndex == -1)
        ForceListIndex = this.GetNextEmptyIndex();
      if (ForceListIndex > 0 && this.applicantRows[ForceListIndex - 1] != null)
      {
        applicantRow.location.Y = this.applicantRows[ForceListIndex - 1].location.Y;
        applicantRow.location.Y += applicantRow.GetSize().Y * 0.5f;
      }
      applicantRow.location.Y += applicantRow.GetSize().Y * 0.5f;
      applicantRow.location.Y += Ybuffer;
      if (this.applicantRows[ForceListIndex] != null)
        throw new Exception("OVERWRITING");
      this.applicantRows[ForceListIndex] = applicantRow;
    }

    private int GetNextEmptyIndex()
    {
      for (int index = 0; index < this.applicantRows.Length; ++index)
      {
        if (this.applicantRows[index] == null)
          return index;
      }
      return -1;
    }

    public void RemoveRow(List<Applicant> applicant)
    {
      for (int index1 = 0; index1 < applicant.Count; ++index1)
      {
        for (int index2 = 0; index2 < this.applicantRows.Length; ++index2)
        {
          if (this.applicantRows[index2] != null && this.applicantRows[index2].refApplicant == applicant[index1])
          {
            this.applicantRows[index2].RemoveRow();
            this.IndexesToRemove.Add(index2);
            if (this.IndexesToRemove.Count > this.applicantsToAdd.Count)
            {
              this.GapIndexesToFill.Add(index2);
              break;
            }
            break;
          }
        }
      }
    }

    public Vector2 GetSize()
    {
      int index = this.GetNextEmptyIndex() - 1;
      if (index < 0)
        index = this.applicantRows.Length - 1;
      float y = (float) ((double) this.applicantRows[index].location.Y - (double) this.headerRow.location.Y + (double) this.headerRow.GetSize().Y * 0.5 + (double) this.applicantRows[0].GetSize().Y * 0.5);
      return new Vector2(this.applicantRows[0].GetSize().X, y);
    }

    public Vector2 GetSizeOfOneRow(bool GetHeaderRow = false)
    {
      if (GetHeaderRow)
        return this.headerRow.GetSize();
      return this.applicantRows.Length != 0 ? this.applicantRows[0].GetSize() : Vector2.Zero;
    }

    public int GetNumberOfEntries()
    {
      int num = 0;
      for (int index = 0; index < this.applicantRows.Length; ++index)
      {
        if (this.applicantRows[index] != null)
          ++num;
      }
      return num - this.IndexesToRemove.Count + this.applicantsToAdd.Count;
    }

    public bool IsBusy() => this.applicantsToAdd.Count > 0 | this.IndexesToRemove.Count > 0 | this.GapIndexesToFill.Count > 0;

    public bool AreLerpsAllDone()
    {
      for (int index = 0; index < this.applicantRows.Length; ++index)
      {
        if (this.applicantRows[index] != null && !this.applicantRows[index].IsDoneLerping())
          return false;
      }
      return true;
    }

    public Applicant UpdateApplicantsTable(
      Player player,
      float DeltaTime,
      Vector2 offset,
      out bool isDismiss)
    {
      offset += this.location;
      isDismiss = false;
      for (int index = 0; index < this.applicantRows.Length; ++index)
      {
        if (this.applicantRows[index] != null)
        {
          Applicant applicant = this.applicantRows[index].UpdateApplicantRow(player, DeltaTime, offset, out isDismiss, this.applicantsToAdd.Count > 0);
          if (applicant != null)
            return applicant;
        }
      }
      if (this.IndexesToRemove.Count > 0)
      {
        if ((double) this.applicantRows[this.IndexesToRemove[0]].entryExitLerper.Value == 0.0)
        {
          for (int index = 0; index < this.IndexesToRemove.Count; ++index)
            this.applicantRows[this.IndexesToRemove[index]] = (ApplicantRow) null;
          this.IndexesToRemove.Clear();
        }
      }
      else if (this.applicantsToAdd.Count > 0)
      {
        for (int index = 0; index < this.applicantsToAdd.Count; ++index)
          this.AddRow(this.applicantsToAdd[index], this.refBaseScale, this.refYbuffer, ref this.refWidths, isAgencyInstantHire: this.isAgencyInstantHire, LerpInSmoothly: true);
        this.applicantsToAdd.Clear();
      }
      else if (this.GapIndexesToFill.Count > 0)
      {
        for (int index1 = 0; index1 < this.GapIndexesToFill.Count; ++index1)
        {
          for (int index2 = this.GapIndexesToFill[index1] + 1; index2 < this.applicantRows.Length; ++index2)
          {
            if (this.applicantRows[index2] != null)
            {
              this.applicantRows[index2].LerpThisUp(this.applicantRows[index2].GetSize().Y + this.refYbuffer);
              this.applicantRows[index2 - 1] = this.applicantRows[index2];
              this.applicantRows[index2] = (ApplicantRow) null;
            }
          }
        }
        this.GapIndexesToFill.Clear();
      }
      return (Applicant) null;
    }

    public void DismissApplicants(List<Applicant> applicant, List<Applicant> addThisApplicantToo = null)
    {
      this.applicantsToAdd = addThisApplicantToo;
      this.RemoveRow(applicant);
    }

    public void AddApplicants(List<Applicant> addThisApplicant) => this.applicantsToAdd = addThisApplicant;

    public List<Applicant> GetAllApplicantsDisplayed()
    {
      List<Applicant> applicantList = new List<Applicant>();
      for (int index = 0; index < this.applicantRows.Length; ++index)
      {
        if (this.applicantRows[index] != null)
          applicantList.Add(this.applicantRows[index].refApplicant);
      }
      return applicantList;
    }

    public void DrawApplicantsTable(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.headerRow.DrawApplicantRow(offset, spriteBatch);
      for (int index = 0; index < this.applicantRows.Length; ++index)
      {
        if (this.applicantRows[index] != null)
          this.applicantRows[index].DrawApplicantRow(offset, spriteBatch);
      }
    }
  }
}
