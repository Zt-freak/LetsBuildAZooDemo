// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.Animal._01Animal.AnimalTabManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Animal._01Animal.Diet;
using TinyZoo.Z_SummaryPopUps.People.Animal._01Animal.Longevity;
using TinyZoo.Z_SummaryPopUps.People.Animal._01Animal.Pregnancy;
using TinyZoo.Z_SummaryPopUps.People.Animal._01Animal.Sickness;
using TinyZoo.Z_SummaryPopUps.People.Animal._01Animal.Welfare;
using TinyZoo.Z_SummaryPopUps.People.Animal.Shared;
using TinyZoo.Z_SummaryPopUps.People.Animal.TabFrame;

namespace TinyZoo.Z_SummaryPopUps.People.Animal._01Animal
{
  internal class AnimalTabManager
  {
    public Vector2 location;
    private BottomButtons bottombuttons;
    private LongevityManager longevitymanager;
    private PregnancyManager pregnancymanager;
    private WalfareManager welfaremanager;
    private CurrentDietDisplay dietmanager;
    private AnimalSickStatus sickmanager;
    private LookAtThisThingButton lookatthisbutton;
    private Vector2 size;
    private float BaseScale;
    private UIScaleHelper scaleHelper;

    public AnimalTabManager(PrisonerInfo animal, Player player, float width, float _BaseScale)
    {
      this.BaseScale = _BaseScale;
      this.scaleHelper = new UIScaleHelper(this.BaseScale);
      Vector2 defaultBuffer = this.scaleHelper.DefaultBuffer;
      this.size = Vector2.Zero;
      this.longevitymanager = new LongevityManager(animal, width, this.BaseScale);
      this.longevitymanager.Location.Y += this.longevitymanager.GetSize().Y * 0.5f;
      this.size.Y += this.longevitymanager.GetSize().Y;
      this.size.Y += defaultBuffer.Y;
      this.dietmanager = new CurrentDietDisplay(animal, player, this.BaseScale, width);
      this.dietmanager.location.Y = this.size.Y;
      this.dietmanager.location.Y += this.dietmanager.GetSize().Y * 0.5f;
      this.size.Y += this.dietmanager.GetSize().Y;
      this.size.Y += defaultBuffer.Y;
      this.pregnancymanager = new PregnancyManager(width, animal, player, this.BaseScale);
      this.pregnancymanager.location.Y = this.size.Y;
      this.pregnancymanager.location.Y += this.pregnancymanager.GetSize().Y * 0.5f;
      this.size.Y += this.pregnancymanager.GetSize().Y;
      this.size.Y += defaultBuffer.Y;
      this.welfaremanager = new WalfareManager(animal, player, this.BaseScale);
      this.welfaremanager.location.Y = this.size.Y;
      this.welfaremanager.location.Y += this.welfaremanager.GetSize().Y * 0.5f;
      this.welfaremanager.location.X -= width * 0.5f;
      this.welfaremanager.location.X += this.welfaremanager.GetSize().X * 0.5f;
      this.size.X += this.welfaremanager.GetSize().X;
      this.size.X += defaultBuffer.X;
      this.sickmanager = new AnimalSickStatus(animal, player, this.BaseScale, width - this.size.X, this.welfaremanager.GetSize().Y);
      this.sickmanager.location.Y = this.size.Y;
      this.sickmanager.location.X = this.size.X - width * 0.5f;
      this.sickmanager.location += this.sickmanager.GetSize() * 0.5f;
      this.size.Y += this.welfaremanager.GetSize().Y;
      this.size.Y += defaultBuffer.Y;
      this.bottombuttons = new BottomButtons(AnimalViewTabType.Animal, width, animal, player, this.BaseScale);
      this.bottombuttons.location.Y = this.size.Y;
      this.bottombuttons.location.Y += this.bottombuttons.GetSize().Y * 0.5f;
      this.size.Y += this.bottombuttons.GetSize().Y;
      this.size.X = width;
    }

    public void AddLookAtThisButton(PrisonerInfo animal, Vector2 location)
    {
      this.lookatthisbutton = new LookAtThisThingButton(animal, this.BaseScale);
      this.lookatthisbutton.location = Vector2.Zero;
      this.lookatthisbutton.location += new Vector2(location.X, (float) (-(double) location.Y * 0.5));
      this.lookatthisbutton.location.Y -= this.scaleHelper.ScaleY(10f);
      this.lookatthisbutton.location.X += this.lookatthisbutton.GetSize().X * 0.5f;
      this.lookatthisbutton.location.X += this.scaleHelper.DefaultBuffer.X;
      this.lookatthisbutton.location.X -= this.size.X * 0.5f;
    }

    public Vector2 GetSize() => this.size;

    public void OnExit()
    {
      if (this.lookatthisbutton == null)
        return;
      this.lookatthisbutton.OnButtonDestroy();
    }

    public bool UpdateAnimalTabManager(
      Vector2 offset,
      Player player,
      float DeltaTime,
      out bool PopupQuarantineInfo)
    {
      offset += this.location;
      if (this.pregnancymanager != null)
        this.pregnancymanager.UpdatePregnancyManager(offset, player, DeltaTime);
      this.welfaremanager.UpdateWalfareManager(offset, player);
      this.longevitymanager.UpdateLongevityManager();
      this.dietmanager.UpdateCurrentDietDisplay(player, DeltaTime, offset);
      int num = this.bottombuttons.UpdateBottomButtons(offset, player, DeltaTime, out PopupQuarantineInfo) ? 1 : 0;
      if (this.lookatthisbutton == null)
        return num != 0;
      this.lookatthisbutton.UpdateLookAtThisThingButton(player, DeltaTime, offset);
      return num != 0;
    }

    public void DrawAnimalTabManager(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.longevitymanager.DrawLongevityManager(offset, spriteBatch);
      if (this.pregnancymanager != null)
        this.pregnancymanager.DrawPregnancyManager(offset, spriteBatch);
      this.dietmanager.DrawCurrentDietDisplay(offset, spriteBatch);
      this.sickmanager.DrawAnimalSickStatus(offset, spriteBatch);
      this.welfaremanager.DrawWalfareManager(offset, spriteBatch);
      this.bottombuttons.DrawBottomButtons(offset, spriteBatch);
      if (this.lookatthisbutton == null)
        return;
      this.lookatthisbutton.DrawLookAtThisThingButton(offset, spriteBatch);
    }
  }
}
