// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Manage.Hiring.Interview.Negotiation.MakeOffer.MakeOfferManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.GenericUI;
using TinyZoo.PlayerDir.employees;

namespace TinyZoo.Z_Manage.Hiring.Interview.Negotiation.MakeOffer
{
  internal class MakeOfferManager
  {
    private SimpleTextBox tbox;
    private LerpHandler_Float lerper;
    private int Minx;
    private GameObjectNineSlice Framer;
    private PriceAdjuster priceaduster;
    private TextButton Confirm;
    private string HiringString;
    private GameObject HiringObj;

    public MakeOfferManager(PotentialHire REF_hirethisguy)
    {
      string TEXTTT = REF_hirethisguy.GetJobTitle() + "~~Salary Range: $" + (object) REF_hirethisguy.employeestats.MinimumWage + " to $" + (object) REF_hirethisguy.employeestats.MaximumWage + "~~Adjust how much you want to pay this employee and make an offer!";
      this.lerper = new LerpHandler_Float();
      this.tbox = new SimpleTextBox(TEXTTT, 900f, false, GameFlags.GetSmallTextScale());
      this.lerper.SetLerp(true, 1f, 0.0f, 3f);
      this.Minx = REF_hirethisguy.GetMinimumWage();
      this.priceaduster = new PriceAdjuster(REF_hirethisguy.employeestats.MinimumWage, REF_hirethisguy.employeestats.MaximumWage, _OverallMultiplier: 0.5f);
      this.priceaduster.Location = new Vector2(512f, 330f);
      this.priceaduster.SetIncrmemnt(5);
      this.Framer = new GameObjectNineSlice(StringInBox.GetFrameColourRect(BTNColour.Cream, out Vector3 _), 7);
      this.Framer.scale = 2f;
      this.Confirm = new TextButton("Make Offer!", 60f);
      this.Confirm.vLocation.X = 768f;
    }

    public int GetOfferValue() => this.priceaduster.CurrentValue;

    public bool UpdateMakeOfferManager(Player player, float DeltaTime)
    {
      Vector2 zero = Vector2.Zero;
      zero.X += this.lerper.Value * 1024f;
      this.lerper.UpdateLerpHandler(DeltaTime);
      if (this.Confirm.UpdateTextButton(player, zero + new Vector2(0.0f, this.priceaduster.Location.Y), DeltaTime))
        return true;
      this.priceaduster.UpdatePriceAdjuster(player, zero, DeltaTime);
      return false;
    }

    public void DrawMakeOffserManager(Vector2 Offset)
    {
      Offset.X += this.lerper.Value * 1024f;
      this.tbox.Location = new Vector2(512f, 180f);
      this.tbox.DrawSimpleTextBox(Offset);
      this.Framer.DrawGameObjectNineSlice(AssetContainer.pointspritebatch03, AssetContainer.SpriteSheet, Offset + new Vector2(512f, this.priceaduster.Location.Y), new Vector2(800f, 75f));
      this.priceaduster.DrawPriceAdjuster(Offset);
      this.Confirm.DrawTextButton(Offset + new Vector2(0.0f, this.priceaduster.Location.Y));
    }
  }
}
