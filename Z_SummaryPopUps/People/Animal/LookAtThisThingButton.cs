// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.Animal.LookAtThisThingButton
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.OverWorld.OverWorldEnv;
using TinyZoo.OverWorld.OverWorldEnv.Customers;
using TinyZoo.PlayerDir.Layout;

namespace TinyZoo.Z_SummaryPopUps.People.Animal
{
  internal class LookAtThisThingButton
  {
    public Vector2 location;
    private LittleSummaryButton littleSummaryButton;
    private PrisonerInfo refPrisonerInfo;
    private WalkingPerson refWalkingPerson;
    private bool IsLookingAtAnimal;
    private bool IsLookingAtPerson;

    public LookAtThisThingButton(PrisonerInfo animalPrisonerInfo, float BaseScale)
    {
      this.IsLookingAtAnimal = true;
      this.refPrisonerInfo = animalPrisonerInfo;
      this.SetUp(BaseScale);
    }

    public LookAtThisThingButton(WalkingPerson walkingPerson, float BaseScale)
    {
      this.IsLookingAtPerson = true;
      this.refWalkingPerson = walkingPerson;
      this.SetUp(BaseScale);
    }

    private void SetUp(float BaseScale) => this.littleSummaryButton = new LittleSummaryButton(LittleSummaryButtonType.Locate, _BaseScale: BaseScale);

    public Vector2 GetSize() => this.littleSummaryButton.GetSize();

    public void OnButtonDestroy() => OverWorldEnvironmentManager.overworldcam.StopLookingAtThis();

    public void UpdateLookAtThisThingButton(Player player, float DeltaTime, Vector2 offset)
    {
      offset += this.location;
      if (this.littleSummaryButton.isDisabled)
        return;
      if (this.IsLookingAtPerson)
      {
        if (this.refWalkingPerson == null)
          this.littleSummaryButton.SetDisabled(true);
      }
      else if (this.IsLookingAtAnimal && this.refPrisonerInfo == null)
        this.littleSummaryButton.SetDisabled(true);
      if (!this.littleSummaryButton.UpdateLittleSummaryButton(DeltaTime, player, offset))
        return;
      if (this.refWalkingPerson != null)
      {
        OverWorldEnvironmentManager.overworldcam.LookAtThis(this.refWalkingPerson);
      }
      else
      {
        if (this.refPrisonerInfo == null)
          return;
        OverWorldEnvironmentManager.overworldcam.LookAtThis(this.refPrisonerInfo);
      }
    }

    public void DrawLookAtThisThingButton(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.littleSummaryButton.DrawLittleSummaryButton(offset, spriteBatch);
    }
  }
}
