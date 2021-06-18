// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BreedScreen.ConfirmBreed.ConfirmBreedmanager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.GenericUI;
using TinyZoo.Z_BreedScreen.SelectNewBreed.SelectSpecies;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_BreedScreen.ConfirmBreed
{
  internal class ConfirmBreedmanager
  {
    public Vector2 Location;
    public BigBrownPanel bigBrownPanel;
    private SimpleTextHandler simpletext;
    private TextButton ConfirmButton;
    private ParentsAndOffspringDisplay parentsAndOffspringDisplay;
    public Parents_AndChild Ref_ParentsAndChild;
    private LerpHandler_Float lerper;

    public ConfirmBreedmanager(Parents_AndChild PandC, float BaseScale)
    {
      this.Ref_ParentsAndChild = PandC;
      UIScaleHelper uiScaleHelper = new UIScaleHelper(BaseScale);
      float defaultYbuffer = uiScaleHelper.GetDefaultYBuffer();
      float defaultXbuffer = uiScaleHelper.GetDefaultXBuffer();
      float num1 = 0.0f;
      float num2 = 0.0f;
      this.parentsAndOffspringDisplay = new ParentsAndOffspringDisplay(PandC, BaseScale);
      this.parentsAndOffspringDisplay.location.Y = num1 + this.parentsAndOffspringDisplay.customerFrame.VSCale.Y * 0.5f;
      float num3 = num1 + this.parentsAndOffspringDisplay.customerFrame.VSCale.Y;
      float num4 = num2 + this.parentsAndOffspringDisplay.customerFrame.VSCale.X;
      float num5 = num3 + defaultYbuffer;
      this.simpletext = new SimpleTextHandler("Moving these two animals into the selective breeding program will remove them from display to the public.~You can cancel the breeding program and retun them to their pens at any time.", true, num4 / Sengine.ReferenceScreenRes.X, BaseScale, AutoComplete: true);
      this.simpletext.SetAllColours(ColourData.Z_Cream);
      this.simpletext.Location.Y = num5;
      this.simpletext.Location.Y += this.simpletext.GetHeightOfOneLine() * 0.5f;
      float num6 = num5 + this.simpletext.GetHeightOfParagraph() + defaultYbuffer;
      this.ConfirmButton = new TextButton(BaseScale, "Confirm", 50f);
      Vector2 sizeTrue = this.ConfirmButton.GetSize_True();
      this.ConfirmButton.vLocation.Y = num6 + sizeTrue.Y * 0.5f;
      float y = num6 + sizeTrue.Y + defaultYbuffer;
      float x = num4 + defaultXbuffer;
      this.bigBrownPanel = new BigBrownPanel(Vector2.Zero, true, "Confirm Breed", BaseScale);
      this.bigBrownPanel.Finalize(new Vector2(x, y));
      Vector2 frameOffsetFromTop = this.bigBrownPanel.GetFrameOffsetFromTop();
      this.parentsAndOffspringDisplay.location.Y += frameOffsetFromTop.Y;
      this.simpletext.Location.Y += frameOffsetFromTop.Y;
      this.ConfirmButton.vLocation.Y += frameOffsetFromTop.Y;
      this.lerper = new LerpHandler_Float();
      this.LerpIn();
    }

    public void LerpIn() => this.lerper.SetLerp(true, 1f, 0.0f, 3f);

    public void LerpOff() => this.lerper.SetLerp(false, 0.0f, 1f, 3f);

    public void GetSelectedBreedInfo()
    {
    }

    public bool CheckMouseOver(Player player, Vector2 offset)
    {
      offset.X += this.lerper.Value * BreedPopUp.LerpDistance;
      offset += this.Location;
      return this.bigBrownPanel.CheckMouseOver(player, offset);
    }

    public bool UpdateConfirmBreedmanager(Player player, float DeltaTime, out bool GoBack)
    {
      Vector2 location = this.Location;
      GoBack = false;
      this.lerper.UpdateLerpHandler(DeltaTime);
      if ((double) this.lerper.Value != 0.0)
        return false;
      if (this.bigBrownPanel.UpdatePanelCloseButton(player, DeltaTime, location))
        GoBack = true;
      return this.ConfirmButton.UpdateTextButton(player, location, DeltaTime);
    }

    public void DrawConfirmBreedmanager(SpriteBatch spritebatch, Vector2 Offset)
    {
      if ((double) this.lerper.Value == 1.0)
        return;
      Offset += this.Location;
      Offset.X += this.lerper.Value * BreedPopUp.LerpDistance;
      this.bigBrownPanel.DrawBigBrownPanel(Offset, spritebatch);
      this.parentsAndOffspringDisplay.DrawParentsAndOffspringDisplay(Offset, spritebatch);
      this.ConfirmButton.DrawTextButton(Offset, 1f, spritebatch);
      this.simpletext.DrawSimpleTextHandler(Offset, 1f, spritebatch);
    }
  }
}
