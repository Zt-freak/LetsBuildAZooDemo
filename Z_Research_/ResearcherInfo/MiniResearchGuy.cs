// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Research_.ResearcherInfo.MiniResearchGuy
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.PlayerDir;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.Generic;
using TinyZoo.Z_SummaryPopUps.People.Customer;
using TinyZoo.Z_SummaryPopUps.People.Customer.SatisfactionBars;

namespace TinyZoo.Z_Research_.ResearcherInfo
{
  internal class MiniResearchGuy
  {
    private AnimalInFrame animalInFrame;
    public CustomerFrame brownFrame;
    public Vector2 Location;
    private SatisfactionBarAndText satisfactionandtext;
    private ZGenericText nameText;
    private ZGenericText daysEmployedText;
    private ZGenericText totalPointsText;
    private Employee employee;
    private ExtraMiniResearchGuy extramini;

    public MiniResearchGuy(
      Employee _employee,
      float BaseScale,
      bool UseExtraMiniVersion = false,
      bool IsForResearchBuilding = false)
    {
      if (UseExtraMiniVersion)
      {
        this.extramini = new ExtraMiniResearchGuy(_employee, BaseScale, IsForResearchBuilding);
      }
      else
      {
        this.employee = _employee;
        string name = this.employee.quickemplyeedescription.NAME;
        int daysEmployed = this.employee.DaysEmployed;
        int allResearchPoints = this.employee.GetAllResearchPoints(TimeSegmentType.AllTime, out bool _);
        this.nameText = new ZGenericText(name, BaseScale, false);
        this.animalInFrame = new AnimalInFrame(this.employee.quickemplyeedescription.thisemployee, AnimalType.None, TargetSize: (35f * BaseScale), FrameEdgeBuffer: (6f * BaseScale), BaseScale: BaseScale);
        this.daysEmployedText = new ZGenericText("Days Employed: " + (object) daysEmployed, BaseScale, false);
        this.totalPointsText = new ZGenericText("Points Discovered: " + (object) allResearchPoints, BaseScale, false);
        this.satisfactionandtext = new SatisfactionBarAndText((float) this.employee.ResearchProgress / 100f, "Progress to next point", BaseScale);
        Vector2 defaultBuffer = new UIScaleHelper(BaseScale).DefaultBuffer;
        float y1 = defaultBuffer.Y;
        float x1 = defaultBuffer.X;
        this.nameText.vLocation = new Vector2(x1, y1);
        float y2 = y1 + this.nameText.GetSize().Y + defaultBuffer.Y;
        this.animalInFrame.Location = new Vector2(defaultBuffer.X, y2);
        this.animalInFrame.Location += this.animalInFrame.GetSize() * 0.5f;
        float x2 = defaultBuffer.X + this.animalInFrame.GetSize().X + defaultBuffer.X;
        float y3 = y2;
        this.daysEmployedText.vLocation = new Vector2(x2, y3);
        float y4 = y3 + this.daysEmployedText.GetSize().Y;
        this.totalPointsText.vLocation = new Vector2(x2, y4);
        float num = y4 + this.totalPointsText.GetSize().Y;
        float y5 = y2 + this.animalInFrame.GetSize().Y + defaultBuffer.Y;
        Vector2 size = this.satisfactionandtext.GetSize();
        this.satisfactionandtext.Location = new Vector2(x1, y5);
        this.satisfactionandtext.Location += size * 0.5f;
        this.satisfactionandtext.Location.X += this.satisfactionandtext.GetTextWidth();
        size.X += this.satisfactionandtext.GetTextWidth();
        float y6 = y5 + size.Y + defaultBuffer.Y;
        this.brownFrame = new CustomerFrame(new Vector2(Math.Max(Math.Max(defaultBuffer.X + size.X, x2 + this.totalPointsText.GetSize().X), x2 + this.daysEmployedText.GetSize().X) + defaultBuffer.X, y6), CustomerFrameColors.Brown, BaseScale);
        Vector2 vector2 = -this.brownFrame.VSCale * 0.5f;
        ZGenericText nameText = this.nameText;
        nameText.vLocation = nameText.vLocation + vector2;
        this.animalInFrame.Location += vector2;
        ZGenericText daysEmployedText = this.daysEmployedText;
        daysEmployedText.vLocation = daysEmployedText.vLocation + vector2;
        ZGenericText totalPointsText = this.totalPointsText;
        totalPointsText.vLocation = totalPointsText.vLocation + vector2;
        this.satisfactionandtext.Location += vector2;
      }
    }

    public Vector2 GetSize() => this.extramini != null ? this.extramini.GetSize() : this.brownFrame.VSCale;

    public void UpdateMiniResearchGuy()
    {
      if (this.extramini == null)
        return;
      this.extramini.UpdateExtraMiniResearchGuy();
    }

    public void DrawMiniResearchGuy(Vector2 Offset, SpriteBatch spriteBatch)
    {
      Offset += this.Location;
      if (this.extramini != null)
      {
        this.extramini.DrawExtraMiniResearchGuy(Offset, spriteBatch);
      }
      else
      {
        this.brownFrame.DrawCustomerFrame(Offset, spriteBatch);
        this.animalInFrame.DrawAnimalInFrame(Offset, spriteBatch);
        this.nameText.DrawZGenericText(Offset, spriteBatch);
        this.daysEmployedText.DrawZGenericText(Offset, spriteBatch);
        this.totalPointsText.DrawZGenericText(Offset, spriteBatch);
        this.satisfactionandtext.DrawSatisfactionBarAndText(spriteBatch, Offset);
      }
    }
  }
}
