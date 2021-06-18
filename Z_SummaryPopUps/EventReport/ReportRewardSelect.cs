// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.EventReport.ReportRewardSelect
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using System;
using TinyZoo.GenericUI;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.EventReport.EventData;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_SummaryPopUps.EventReport
{
  internal class ReportRewardSelect
  {
    private static Rectangle whitenineslice = new Rectangle(948, 484, 21, 21);
    public Vector2 location;
    private float basescale;
    private UIScaleHelper scalehelper;
    private CustomerFrame frame;
    private Vector2 framescale;
    private GameObjectNineSlice highlight;
    private SimpleTextHandler text;
    private MouseoverHandler mouseoverhandler;
    private ReportAction action;
    private TextButton button;
    public bool hidetextbutton;
    private bool chosen;

    public ReportRewardSelect(ReportAction action_, float basescale_)
    {
      this.basescale = basescale_;
      this.scalehelper = new UIScaleHelper(this.basescale);
      Vector2 defaultBuffer = this.scalehelper.DefaultBuffer;
      this.action = action_;
      this.text = new SimpleTextHandler(this.action.GetDescription(), this.scalehelper.ScaleX(320f), _Scale: this.basescale, _UseFontOnePointFive: true);
      this.text.SetAllColours(ColourData.Z_Cream);
      this.text.AutoCompleteParagraph();
      this.button = new TextButton(this.basescale, "SELECT", 40f);
      this.button.SetActivateAnimation();
      this.highlight = new GameObjectNineSlice(ReportRewardSelect.whitenineslice, 7);
      this.highlight.scale = basescale_;
      this.highlight.fAlpha = 0.6f;
      this.highlight.SetDrawOriginToCentre();
      this.highlight.SetAllColours(ColourData.Z_ButtonDarkGreen);
      this.framescale = 1f * defaultBuffer;
      this.framescale.X += defaultBuffer.X;
      this.framescale += this.text.GetSize();
      this.framescale.X += defaultBuffer.X + this.button.GetSize_True().X;
      this.framescale.Y = Math.Max(this.framescale.Y, this.button.GetSize_True().Y + defaultBuffer.Y);
      this.frame = new CustomerFrame(this.framescale, BaseScale: this.basescale);
      this.mouseoverhandler = new MouseoverHandler(this.framescale, basescale_);
      Vector2 vector2 = -0.5f * this.framescale;
      vector2.Y += 0.5f * defaultBuffer.Y;
      vector2.X += defaultBuffer.X;
      this.text.Location = vector2;
      vector2.X += this.text.GetSize().X + defaultBuffer.X;
      this.button.vLocation = new Vector2();
      this.button.vLocation.Y = 0.0f;
      this.button.vLocation.X = vector2.X + 0.5f * this.button.GetSize_True().X;
    }

    public Vector2 GetSize() => this.framescale;

    public string GetDescription() => this.action.GetDescription();

    public bool UpdateReportRewardSelect(Player player, Vector2 offset, float DeltaTime)
    {
      offset += this.location;
      bool flag = false;
      if (this.button.UpdateTextButton(player, offset, DeltaTime))
      {
        flag = true;
        this.chosen = true;
      }
      return flag;
    }

    public void DrawReportRewardSelect(SpriteBatch spritebatch, Vector2 offset)
    {
      offset += this.location;
      this.frame.DrawCustomerFrame(offset, spritebatch);
      if (this.chosen)
        this.highlight.DrawGameObjectNineSlice(spritebatch, AssetContainer.SpriteSheet, offset, this.framescale);
      this.text.DrawSimpleTextHandler(offset, 1f, spritebatch);
      if (this.hidetextbutton)
        return;
      this.button.DrawTextButton(offset, 1f, spritebatch);
    }
  }
}
