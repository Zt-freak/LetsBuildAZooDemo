// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BuildingInfo.QuarantineBuildingInfo.Settings.Frame.ImportActionSettings
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using TinyZoo.GenericUI;
using TinyZoo.PlayerDir.Quarantine;
using TinyZoo.Z_BuildingInfo.QuarantineBuildingInfo.Settings.Frame.Elements;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_BuildingInfo.QuarantineBuildingInfo.Settings.Frame
{
  internal class ImportActionSettings
  {
    public Vector2 location;
    private CustomerFrame customerFrame;
    private SimpleTextHandler helperDesc;
    private List<ImportActionRow> rows;

    public ImportActionSettings(Player player, float BaseScale)
    {
      UIScaleHelper uiScaleHelper = new UIScaleHelper(BaseScale);
      Vector2 defaultBuffer = uiScaleHelper.DefaultBuffer;
      this.customerFrame = new CustomerFrame(Vector2.Zero, CustomerFrameColors.Brown, BaseScale);
      this.customerFrame.AddMiniHeading("Import Actions");
      this.helperDesc = new SimpleTextHandler("Configure if you would like animals delivered from specific sources to be first quarantined for a period of time, and then transferred to their respective pens.", uiScaleHelper.ScaleX(286f), true, BaseScale, AutoComplete: true);
      Vector2 zero = Vector2.Zero;
      zero.Y += this.customerFrame.GetMiniHeadingHeight();
      zero.Y += defaultBuffer.Y;
      this.helperDesc.SetAllColours(ColourData.Z_Cream);
      this.helperDesc.Location = zero;
      this.helperDesc.Location.Y += this.helperDesc.GetHeightOfOneLine() * 0.5f;
      zero.Y += this.helperDesc.GetHeightOfParagraph();
      zero.Y += defaultBuffer.Y;
      this.rows = new List<ImportActionRow>();
      for (int index = 0; index < 3; ++index)
      {
        ImportActionRow importActionRow = new ImportActionRow((ImportSource) index, player, BaseScale);
        importActionRow.location = zero;
        importActionRow.location.Y += importActionRow.GetSize().Y * 0.5f;
        zero.Y += importActionRow.GetSize().Y;
        zero.Y += defaultBuffer.Y;
        this.rows.Add(importActionRow);
      }
      zero.X = this.rows[0].GetSize().X + defaultBuffer.X * 2f;
      this.customerFrame.Resize(zero);
      Vector2 vector2 = -this.customerFrame.VSCale * 0.5f;
      this.helperDesc.Location.Y += vector2.Y;
      for (int index = 0; index < this.rows.Count; ++index)
        this.rows[index].location.Y += vector2.Y;
    }

    public Vector2 GetSize() => this.customerFrame.VSCale;

    public void UpdateImportActionSettings(Player player, float Deltatime, Vector2 offset)
    {
      offset += this.location;
      for (int index = 0; index < this.rows.Count; ++index)
        this.rows[index].UpdateImportActionRow(player, Deltatime, offset);
    }

    public void DrawImportActionSettings(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.customerFrame.DrawCustomerFrame(offset, spriteBatch);
      this.helperDesc.DrawSimpleTextHandler(offset, 1f, spriteBatch);
      for (int index = 0; index < this.rows.Count; ++index)
        this.rows[index].DrawImportActionRow(offset, spriteBatch);
    }
  }
}
