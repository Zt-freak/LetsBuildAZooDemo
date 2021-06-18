// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BuildingInfo.QuarantineBuildingInfo.Settings.Frame.Elements.ImportActionRow
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using TinyZoo.PlayerDir.Quarantine;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_Manage.Hiring.Interview.Negotiation.MakeOffer;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_BuildingInfo.QuarantineBuildingInfo.Settings.Frame.Elements
{
  internal class ImportActionRow
  {
    public Vector2 location;
    private ZGenericText Title;
    private PriceAdjuster priceAdjuster;
    private CustomerFrame customerFrame;
    private QuarantinePeriod selectedQuarantinePeriod;
    private ImportSource refSource;

    public ImportActionRow(ImportSource source, Player player, float BaseScale)
    {
      this.refSource = source;
      this.Title = new ZGenericText(ImportActionRow.GetImportSourceToString(source), BaseScale, false, _UseOnePointFiveFont: true);
      this.priceAdjuster = new PriceAdjuster(0, 3, (int) player.animalquarantine.GetQuarantinePeriodSetting(source), _BaseScale: BaseScale);
      this.SetString(true);
      UIScaleHelper uiScaleHelper = new UIScaleHelper(BaseScale);
      Vector2 defaultBuffer = uiScaleHelper.DefaultBuffer;
      Vector2 zero = Vector2.Zero;
      zero.X = defaultBuffer.X;
      this.Title.vLocation = zero;
      this.Title.vLocation.Y -= this.Title.GetSize().Y * 0.5f;
      zero.X += uiScaleHelper.ScaleX(150f);
      this.priceAdjuster.Location = zero;
      this.priceAdjuster.Location.X += this.priceAdjuster.GetSize().X * 0.5f;
      zero.X += this.priceAdjuster.GetSize().X;
      zero.Y = Math.Max(this.Title.GetSize().Y, this.priceAdjuster.GetSize().Y);
      this.customerFrame = new CustomerFrame(zero + defaultBuffer * 2f, CustomerFrameColors.DarkBrown, BaseScale);
      Vector2 vector2 = -this.customerFrame.VSCale * 0.5f;
      this.Title.vLocation.X += vector2.X;
      this.priceAdjuster.Location.X += vector2.X;
    }

    public static string GetImportSourceToString(ImportSource source)
    {
      switch (source)
      {
        case ImportSource.BlackMarket:
          return "Black Market";
        case ImportSource.Shelter:
          return "Shelter";
        case ImportSource.ZooTrades:
          return "Zoo Trades";
        default:
          return "NA_" + (object) source;
      }
    }

    private void SetNewOption(QuarantinePeriod newPeriodSet, Player player)
    {
      player.animalquarantine.SetQuarantinePeriodSetting(this.refSource, newPeriodSet);
      this.SetString();
    }

    private void SetString(bool IsCreate = false) => this.priceAdjuster.SetToString(AnimalQuarantine.GetQuarantinePeriodOptionToString((QuarantinePeriod) this.priceAdjuster.CurrentValue), 90f, IsCreate);

    public Vector2 GetSize() => this.customerFrame.VSCale;

    public void UpdateImportActionRow(Player player, float DeltaTime, Vector2 offset)
    {
      offset += this.location;
      if (!this.priceAdjuster.UpdatePriceAdjuster(player, offset, DeltaTime))
        return;
      this.SetNewOption((QuarantinePeriod) this.priceAdjuster.CurrentValue, player);
    }

    public void DrawImportActionRow(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.customerFrame.DrawCustomerFrame(offset, spriteBatch);
      this.Title.DrawZGenericText(offset, spriteBatch);
      this.priceAdjuster.DrawPriceAdjuster(offset, spriteBatch);
    }
  }
}
