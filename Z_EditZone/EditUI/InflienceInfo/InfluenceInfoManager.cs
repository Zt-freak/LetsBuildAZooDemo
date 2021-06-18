// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_EditZone.EditUI.InflienceInfo.InfluenceInfoManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using System;
using TinyZoo.GenericUI;
using TinyZoo.Tile_Data;
using TinyZoo.Z_Employees.WorkZonePane;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_EditZone.EditUI.InflienceInfo
{
  internal class InfluenceInfoManager
  {
    public CustomerFrame customerframe;
    public Vector2 Location;
    private SimpleTextHandler simpletext;
    private WorkZoneInfo workzoneinfo;
    private NumericSummaryText numericsummary;
    private TextButton OK;

    public InfluenceInfoManager(
      float BaseScale,
      WorkZoneInfo _workzoneinfo,
      float buffer,
      TILETYPE buildingtye = TILETYPE.Count)
    {
      this.workzoneinfo = _workzoneinfo;
      this.customerframe = new CustomerFrame(new Vector2(400f, 150f) * BaseScale);
      string TextToWrite = "NO TEXT";
      if (_workzoneinfo.workzonetype == WorkZoneType.Pens)
      {
        switch (buildingtye)
        {
          case TILETYPE.None:
            TextToWrite = SEngine.Localization.Localization.GetText(939);
            break;
          case TILETYPE.MeatProcessor:
            TextToWrite = SEngine.Localization.Localization.GetText(938);
            break;
          case TILETYPE.Count:
            break;
          default:
            throw new Exception("ohisdfsd");
        }
      }
      else
        TextToWrite = SEngine.Localization.Localization.GetText(937);
      this.simpletext = new SimpleTextHandler(TextToWrite, true, 0.3f * BaseScale, BaseScale);
      this.simpletext.AutoCompleteParagraph();
      this.numericsummary = new NumericSummaryText(BaseScale, _workzoneinfo.GetNumericSummary());
      this.numericsummary.vLocation = this.customerframe.VSCale * 0.5f;
      this.numericsummary.vLocation.Y *= -1f;
      this.numericsummary.vLocation.X -= buffer * BaseScale;
      this.numericsummary.vLocation.Y += buffer * BaseScale * Sengine.ScreenRatioUpwardsMultiplier.Y;
      this.OK = new TextButton(BaseScale, SEngine.Localization.Localization.GetText(935));
      this.OK.vLocation.Y = this.customerframe.VSCale.Y * 0.5f;
      this.OK.vLocation.Y -= this.OK.GetSize().Y * 0.5f * Sengine.ScreenRatioUpwardsMultiplier.Y;
      this.OK.vLocation.Y -= buffer * BaseScale * Sengine.ScreenRatioUpwardsMultiplier.Y;
    }

    public void UnsetRed() => this.customerframe.SetBackToBrown(false);

    public void SetRed() => this.customerframe.frame.SetPrimaryColours(0.5f, new Vector3(1f, 0.0f, 0.0f));

    public bool UpdateInfluenceInfoManager(Vector2 Offset, Player player, float DeltaTime)
    {
      this.customerframe.frame.UpdateColours(DeltaTime);
      Offset += this.Location;
      this.numericsummary.SetString(SEngine.Localization.Localization.GetText(940) + this.workzoneinfo.GetNumericSummary());
      return false;
    }

    public void DrawInfluenceInfoManager(Vector2 Offset, SpriteBatch spritebatch)
    {
      Offset += this.Location;
      this.customerframe.DrawCustomerFrame(Offset, spritebatch);
      this.simpletext.DrawSimpleTextHandler(Offset, 1f, spritebatch);
      this.numericsummary.DrawNumericSummaryText(Offset, spritebatch);
    }
  }
}
