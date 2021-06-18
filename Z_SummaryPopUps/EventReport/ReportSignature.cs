// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.EventReport.ReportSignature
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_ManageEmployees.EmployeeView.PerformanceTable;

namespace TinyZoo.Z_SummaryPopUps.EventReport
{
  internal class ReportSignature
  {
    private static Rectangle signature = new Rectangle(190, 934, 138, 34);
    public Vector2 location;
    private float basescale;
    private UIScaleHelper scalehelper;
    private Vector2 framescale;
    private ZGenericUIDrawObject sign;
    private ZGenericText text;
    private RowSegmentRectangle line;

    public ReportSignature(float basescale_, SignatureType signatureType)
    {
      this.basescale = basescale_;
      this.scalehelper = new UIScaleHelper(this.basescale);
      Vector2 defaultBuffer = this.scalehelper.DefaultBuffer;
      Rectangle drawrect = Rectangle.Empty;
      Texture2D uiSheet = AssetContainer.UISheet;
      string _textToWrite = string.Empty;
      bool flag = false;
      switch (signatureType)
      {
        case SignatureType.HelloGuy:
          drawrect = ReportSignature.signature;
          _textToWrite = "Inspected By".ToUpper();
          break;
        case SignatureType.Banker:
          drawrect = new Rectangle(631, 0, 116, 26);
          _textToWrite = "Approved by And Signed";
          flag = true;
          break;
      }
      this.sign = new ZGenericUIDrawObject(drawrect, this.basescale, uiSheet);
      this.text = new ZGenericText(_textToWrite, this.basescale, _UseOnePointFiveFont: true);
      this.text.SetAllColours(ColourData.Z_DarkTextGray);
      if (flag)
        this.line = new RowSegmentRectangle(this.sign.GetSize().X + defaultBuffer.X * 2f, this.scalehelper.ScaleY(1f), ColourData.Z_DarkTextGray, 1f);
      this.framescale = this.sign.GetSize();
      this.framescale.Y += this.text.GetSize().Y;
      if (this.line != null)
      {
        this.framescale.X = this.line.GetSize().X;
        this.framescale.Y += this.line.GetSize().Y;
      }
      Vector2 vector2 = -0.5f * this.framescale;
      vector2.X = 0.0f;
      this.sign.location = vector2;
      this.sign.location.Y += 0.5f * this.sign.GetSize().Y;
      vector2.Y += this.sign.GetSize().Y;
      if (this.line != null)
      {
        this.line.vLocation.Y = vector2.Y;
        vector2.Y += this.line.GetSize().Y;
      }
      this.text.vLocation = vector2;
      this.text.vLocation.Y += 0.5f * this.text.GetSize().Y;
      vector2.Y += this.text.GetSize().Y;
    }

    public Vector2 GetSize() => this.framescale;

    public bool UpdateReportSignature(Player player, Vector2 offset, float DeltaTime)
    {
      offset += this.location;
      return false;
    }

    public void DrawReportSignature(SpriteBatch spritebatch, Vector2 offset)
    {
      offset += this.location;
      this.sign.DrawZGenericUIDrawObject(spritebatch, offset);
      this.text.DrawZGenericText(offset, spritebatch);
      if (this.line == null)
        return;
      this.line.DrawRowSegmentRectangle(offset, spritebatch);
    }
  }
}
