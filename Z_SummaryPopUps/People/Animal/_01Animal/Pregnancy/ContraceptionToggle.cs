// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.Animal._01Animal.Pregnancy.ContraceptionToggle
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_SummaryPopUps.People.Animal._01Animal.Pregnancy
{
  internal class ContraceptionToggle
  {
    public Vector2 location;
    private ZGenericText Text;
    private PrisonerInfo REF_animal;
    private float BaseScale;
    private OnOffToggle toggle;
    private Vector2 size;

    public ContraceptionToggle(PrisonerInfo animal, float _BaseScale)
    {
      this.BaseScale = _BaseScale;
      this.REF_animal = animal;
      this.SetUp(this.BaseScale);
    }

    private void SetUp(float BaseScale)
    {
      UIScaleHelper uiScaleHelper = new UIScaleHelper(BaseScale);
      this.Text = new ZGenericText("Contraception", BaseScale, false, true);
      this.toggle = new OnOffToggle(BaseScale, this.REF_animal.IsOnContraceptive);
      this.toggle.location += this.toggle.GetSize() * 0.5f;
      this.toggle.location.X += uiScaleHelper.DefaultBuffer.X;
      float x = this.Text.GetSize().X;
      this.Text.vLocation.X += x;
      this.toggle.location.X += x;
      this.size.X = this.toggle.GetSize().X + uiScaleHelper.DefaultBuffer.X + x;
      this.size.Y = this.toggle.GetSize().Y;
    }

    public Vector2 GetSize() => this.size;

    public void UpdateDrawContraceptionToggle(Vector2 offset, Player player, float DeltaTime)
    {
      offset += this.location;
      if (!this.toggle.UpdateOnOffToggle(player, offset, DeltaTime))
        return;
      this.REF_animal.IsOnContraceptive = this.toggle.On;
    }

    public void DrawContraceptionToggle(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.toggle.DrawOnOffToggle(spriteBatch, offset);
      this.Text.DrawZGenericText(offset, spriteBatch);
    }
  }
}
