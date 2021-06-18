// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.Customer.VIPSpecificInfo.AnimalWelfareOfficerInfo
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.OverWorld.OverWorldEnv.Customers;
using TinyZoo.PlayerDir.IntakeStuff;
using TinyZoo.Tile_Data;
using TinyZoo.Z_AnimalsAndPeople.Sim_Person;
using TinyZoo.Z_Collection.Shared.Grid;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.EventReport;

namespace TinyZoo.Z_SummaryPopUps.People.Customer.VIPSpecificInfo
{
  internal class AnimalWelfareOfficerInfo : VIPInfo
  {
    private static Rectangle smallA = new Rectangle(232, 834, 18, 18);
    private static Rectangle smallB = new Rectangle(232, 853, 18, 18);
    private static Rectangle smallC = new Rectangle(232, 872, 18, 18);
    private static Rectangle smallF = new Rectangle(232, 891, 18, 18);
    private static Rectangle grassborder = new Rectangle(263, 644, 29, 31);
    private static Rectangle forestborder = new Rectangle(263, 612, 29, 31);
    private static Rectangle desertborder = new Rectangle(263, 580, 29, 31);
    private static Rectangle arcticborder = new Rectangle(293, 575, 29, 31);
    private static Rectangle mountainborder = new Rectangle(323, 575, 29, 31);
    private static Rectangle tropicalborder = new Rectangle(263, 548, 29, 31);
    private static Rectangle savannahborder = new Rectangle(357, 552, 29, 31);
    private CustomerFrame customerFrame;
    private List<ZGenericUIDrawObject> grades;
    private List<ZGenericUIDrawObject> frames;
    private ZGenericUIDrawObject estimatedscorestamp;
    private ZGenericText estimatedscoretext;
    private ZGenericText animalHeading;
    private AnimalInFrameGrid animalRow;
    private int totalpens;

    public AnimalWelfareOfficerInfo(WalkingPerson person, float BaseScale, float forceThisWidth)
    {
      Vector2 defaultBuffer = new UIScaleHelper(BaseScale).DefaultBuffer;
      AnimalWelfareController animalwelfarecontroller = person.simperson.memberofthepublic.animalwelfarecontroller;
      List<IntakePerson> aniamlsSeen = animalwelfarecontroller.GetAniamlsSeen();
      this.totalpens = animalwelfarecontroller.GetTotalPensThisOfficerWillVisit();
      this.totalpens = Math.Max(this.totalpens, 5);
      List<AnimalRenderDescriptor> animals = new List<AnimalRenderDescriptor>();
      List<ReportResultRank> ranskEarened = animalwelfarecontroller.GetRanskEarened();
      List<CellBlockType> pentypesvisited = animalwelfarecontroller.pentypesvisited;
      if (ranskEarened.Count == 0)
      {
        this.estimatedscoretext = new ZGenericText("Estimated Score: --", BaseScale);
      }
      else
      {
        this.estimatedscoretext = new ZGenericText("Estimated Score:", BaseScale);
        this.estimatedscorestamp = new ZGenericUIDrawObject(this.GetRankDrawRect(animalwelfarecontroller.GetFinalRank()), BaseScale, AssetContainer.UISheet);
      }
      this.frames = new List<ZGenericUIDrawObject>();
      this.grades = new List<ZGenericUIDrawObject>();
      for (int index = 0; index < aniamlsSeen.Count; ++index)
      {
        IntakePerson intakePerson = aniamlsSeen[index];
        animals.Add(new AnimalRenderDescriptor(intakePerson.animaltype, intakePerson.CLIndex, intakePerson.HeadType, intakePerson.HeadVariant));
        int num = (int) ranskEarened[index];
        Rectangle rectangle = new Rectangle();
        Rectangle drawrect = new Rectangle();
        Rectangle rankDrawRect = this.GetRankDrawRect(ranskEarened[index]);
        switch (pentypesvisited[index])
        {
          case CellBlockType.Grasslands:
            drawrect = AnimalWelfareOfficerInfo.grassborder;
            break;
          case CellBlockType.Forest:
            drawrect = AnimalWelfareOfficerInfo.forestborder;
            break;
          case CellBlockType.Savannah:
            drawrect = AnimalWelfareOfficerInfo.savannahborder;
            break;
          case CellBlockType.Desert:
            drawrect = AnimalWelfareOfficerInfo.desertborder;
            break;
          case CellBlockType.Mountain:
            drawrect = AnimalWelfareOfficerInfo.mountainborder;
            break;
          case CellBlockType.Arctic:
            drawrect = AnimalWelfareOfficerInfo.arcticborder;
            break;
          case CellBlockType.Tropical:
            drawrect = AnimalWelfareOfficerInfo.tropicalborder;
            break;
        }
        this.grades.Add(new ZGenericUIDrawObject(rankDrawRect, BaseScale, AssetContainer.UISheet));
        this.frames.Add(new ZGenericUIDrawObject(drawrect, BaseScale, AssetContainer.UISheet));
      }
      for (int count = aniamlsSeen.Count; count < this.totalpens; ++count)
        animals.Add(new AnimalRenderDescriptor(AnimalType.QuestionMark, _IsUnlocked: false));
      this.customerFrame = new CustomerFrame(Vector2.Zero, CustomerFrameColors.Brown, BaseScale);
      this.customerFrame.AddMiniHeading("Info");
      this.animalHeading = new ZGenericText("Animals Seen:", BaseScale, false);
      this.animalRow = new AnimalInFrameGrid(BaseScale, 5, defaultBuffer.X, defaultBuffer.Y, animals);
      Vector2 zero = Vector2.Zero;
      zero.X += defaultBuffer.X;
      zero.Y += this.customerFrame.GetMiniHeadingHeight();
      zero.Y += 0.5f * defaultBuffer.Y;
      this.animalHeading.vLocation = zero;
      zero.Y += this.animalHeading.GetSize().Y;
      zero.Y += 0.5f * defaultBuffer.Y;
      this.animalRow.location = zero;
      zero.Y += this.animalRow.GetSize().Y;
      if (this.grades.Count > 0)
        zero.Y += this.grades[0].GetSize().Y + 0.5f * defaultBuffer.Y;
      zero.Y += 0.5f * defaultBuffer.Y;
      this.estimatedscoretext.vLocation = zero + 0.5f * this.estimatedscoretext.GetSize();
      if (this.estimatedscorestamp != null)
      {
        this.estimatedscorestamp.location = zero + 0.5f * this.estimatedscorestamp.GetSize();
        this.estimatedscorestamp.location.X += this.estimatedscoretext.GetSize().X + 0.5f * defaultBuffer.X;
        this.estimatedscoretext.vLocation.Y = this.estimatedscorestamp.location.Y;
        zero.Y += this.estimatedscorestamp.GetSize().Y;
      }
      else
        zero.Y += this.estimatedscoretext.GetSize().Y;
      zero.Y += defaultBuffer.Y;
      this.customerFrame.Resize(new Vector2(forceThisWidth, zero.Y));
      Vector2 vector2 = -this.customerFrame.VSCale * 0.5f;
      ZGenericText animalHeading = this.animalHeading;
      animalHeading.vLocation = animalHeading.vLocation + vector2;
      this.animalRow.location += vector2;
      if (this.estimatedscorestamp != null)
        this.estimatedscorestamp.location += vector2;
      ZGenericText estimatedscoretext = this.estimatedscoretext;
      estimatedscoretext.vLocation = estimatedscoretext.vLocation + vector2;
      for (int index = 0; index < this.grades.Count; ++index)
      {
        this.grades[index].location = this.animalRow.location;
        this.grades[index].location += this.animalRow.GetFrameRelativePosition(index);
        this.grades[index].location.Y += (float) (0.5 * (double) this.animalRow.GetSize().Y + 0.5 * (double) this.grades[index].GetSize().Y + 0.5 * (double) defaultBuffer.Y);
      }
      for (int index = 0; index < this.frames.Count; ++index)
      {
        this.frames[index].location = this.animalRow.location;
        this.frames[index].location += this.animalRow.GetFrameRelativePosition(index);
      }
    }

    private Rectangle GetRankDrawRect(ReportResultRank rank)
    {
      Rectangle rectangle;
      switch (rank)
      {
        case ReportResultRank.A:
          rectangle = AnimalWelfareOfficerInfo.smallA;
          break;
        case ReportResultRank.B:
          rectangle = AnimalWelfareOfficerInfo.smallB;
          break;
        case ReportResultRank.C:
          rectangle = AnimalWelfareOfficerInfo.smallC;
          break;
        case ReportResultRank.F:
          rectangle = AnimalWelfareOfficerInfo.smallF;
          break;
        default:
          rectangle = AnimalWelfareOfficerInfo.smallF;
          break;
      }
      return rectangle;
    }

    public override Vector2 GetSize() => this.customerFrame.VSCale;

    public override void DrawVIPInfo(SpriteBatch spritebatch, Vector2 offset)
    {
      offset += this.location;
      this.customerFrame.DrawCustomerFrame(offset, spritebatch);
      this.animalHeading.DrawZGenericText(offset, spritebatch);
      this.estimatedscoretext.DrawZGenericText(offset, spritebatch);
      if (this.estimatedscorestamp != null)
        this.estimatedscorestamp.DrawZGenericUIDrawObject(spritebatch, offset);
      foreach (ZGenericUIDrawObject frame in this.frames)
        frame.DrawZGenericUIDrawObject(spritebatch, offset);
      this.animalRow.DrawAnimalInFrameGrid(offset, spritebatch);
      foreach (ZGenericUIDrawObject grade in this.grades)
        grade.DrawZGenericUIDrawObject(spritebatch, offset);
    }
  }
}
