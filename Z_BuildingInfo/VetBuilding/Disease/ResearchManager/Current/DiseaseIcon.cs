// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BuildingInfo.VetBuilding.Disease.ResearchManager.Current.DiseaseIcon
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.Z_BuildingInfo.VetBuilding.Disease.ResearchManager.Current.IC;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_BuildingInfo.VetBuilding.Disease.ResearchManager.Current
{
  internal class DiseaseIcon
  {
    private CustomerFrame customerframe;
    private CustomerFrame MouseOverFrame;
    public Vector2 Position;
    private bool MouseOver;
    private DIcon icon;
    private D_TextInfo dtextinfo;
    public TinyZoo.Z_Diseases.Disease Ref_Disease;

    public DiseaseIcon(float BaseScale, TinyZoo.Z_Diseases.Disease disease, Player plater, bool IsJournal)
    {
      this.Ref_Disease = disease;
      Vector2 _VSCale = new Vector2(180f, 90f) * BaseScale;
      this.customerframe = new CustomerFrame(_VSCale, BaseScale: BaseScale);
      if (disease == null)
        return;
      this.MouseOverFrame = new CustomerFrame(_VSCale, Vector3.One, BaseScale);
      this.MouseOverFrame.ResetColor();
      this.MouseOverFrame.frame.SetAlpha(Z_GameFlags.DefaultMouseOverAlphaValue);
      this.icon = new DIcon(DiseaseType.KnownDisease, BaseScale);
      this.icon.vLocation.X = this.customerframe.location.X;
      this.icon.vLocation.X -= this.MouseOverFrame.VSCale.X * 0.5f;
      this.icon.vLocation.X += 10f * BaseScale;
      this.icon.vLocation.X += (float) ((double) this.icon.DrawRect.Width * (double) BaseScale * 0.5);
      this.dtextinfo = new D_TextInfo(BaseScale, DiseaseType.UnknownDisease, IsJournal, disease);
    }

    public Vector2 GetSize() => this.customerframe.VSCale;

    public bool UpdateDiseaseIcon(float DeltaTime, Player player, Vector2 Offset)
    {
      if (this.icon != null)
      {
        Offset += this.Position;
        this.MouseOver = this.customerframe.CheckCollicion(player.inputmap.PointerLocation, Offset);
        if ((double) player.player.touchinput.ReleaseTapArray[0].X >= 0.0)
          return true;
      }
      return false;
    }

    public void DrawDiseaseIcon(Vector2 Offset, SpriteBatch spritebatch)
    {
      Offset += this.Position;
      this.customerframe.DrawCustomerFrame(Offset, spritebatch);
      if (this.icon == null)
        return;
      this.icon.DrawDIcon(spritebatch, Offset);
      this.dtextinfo.DrawD_TextInfo(Offset, spritebatch);
      if (!this.MouseOver)
        return;
      this.MouseOverFrame.DrawCustomerFrame(Offset, spritebatch);
    }
  }
}
