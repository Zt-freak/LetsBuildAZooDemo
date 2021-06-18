// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Manage.Lab.LabManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.GenericUI;

namespace TinyZoo.Z_Manage.Lab
{
  internal class LabManager
  {
    private ScreenHeading screenhead;
    private TextButton textbutton;
    private CharacterTextBox charactertextbox;

    public LabManager()
    {
      this.screenhead = new ScreenHeading("ANIMAL LAB", 70f);
      this.textbutton = new TextButton("Debug Animal Viewer", 100f);
      this.textbutton.vLocation = new Vector2(512f, 650f);
      this.charactertextbox = new CharacterTextBox(AnimalType.Administrator, "This is where you will use Science and CRISPR to make new animals!~~The developers are so lazy, that they haven't made it yet!~~For now you can use the debug viewer, which is errr... buggy, because of those lazy game developers.~~~To use it, click on an animal, the use left and right arrow keys to look at some of the freakish cross breeds.");
      this.charactertextbox.VSCale.Y = 500f;
    }

    public void UpdateLabManager(Player player, float DeltaTime)
    {
      this.charactertextbox.UpdateCharacterTextBox(DeltaTime);
      if (!this.textbutton.UpdateTextButton(player, Vector2.Zero, DeltaTime))
        return;
      Game1.screenfade.BeginFade(true);
      Game1.SetNextGameState(GAMESTATE.DebugAnimalViewerSetUp);
    }

    public void DrawLabManager()
    {
      if (this.screenhead != null)
        this.screenhead.DrawScreenHeading(Vector2.Zero, AssetContainer.pointspritebatch03);
      this.charactertextbox.VSCale.Y = 300f;
      this.charactertextbox.Frame.vLocation.Y = 90f;
      this.charactertextbox.DrawCharacterTextBox(new Vector2(512f, 200f), AssetContainer.pointspritebatchTop05);
      this.textbutton.DrawTextButton(Vector2.Zero);
    }
  }
}
