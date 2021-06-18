// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Events.ChoiceUI.ChoiceEntry
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using SEngine.Buttons;
using SEngine.Lerp;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.GenericUI;
using TinyZoo.OverWorld.Intake.InmateSummary.Psners;
using TinyZoo.OverWorld.OverWorldEnv.Customers;

namespace TinyZoo.Z_Events.ChoiceUI
{
  internal class ChoiceEntry
  {
    private SimpleTextHandler text;
    private GameObjectNineSlice FrameForWHoleThing;
    public TextButton textbutton;
    public Vector2 Location;
    private PopLerper lerper;
    private bool OnLeft;
    private WalkingPerson person;
    private Vector2 Offset;
    public float Delay;
    private PrisonerSprite alienentry;
    private LerpHandler_Float Nlerper;

    public ChoiceEntry(
      string MainText,
      string ButtonText,
      bool IsBuy,
      bool _OnLeft,
      AnimalType animaltype,
      bool IsAnimal = false)
    {
      this.Nlerper = new LerpHandler_Float();
      this.Nlerper.SetLerp(true, 1f, 0.0f, 3f);
      this.OnLeft = _OnLeft;
      Vector3 SecondaryColour;
      this.FrameForWHoleThing = new GameObjectNineSlice(StringInBox.GetFrameColourRect(BTNColour.Cream, out SecondaryColour), 7);
      this.FrameForWHoleThing.scale = 2f;
      this.text = new SimpleTextHandler(MainText, true, 0.3f, RenderMath.GetPixelSizeBestMatch(2f));
      this.text.paragraph.linemaker.SetAllColours(SecondaryColour);
      this.text.Location.Y -= 200f;
      this.textbutton = new TextButton(ButtonText, 80f);
      this.lerper = new PopLerper();
      this.Location = new Vector2(768f, 400f);
      if (this.OnLeft)
        this.Location = new Vector2(256f, 400f);
      this.person = new WalkingPerson(0, animaltype);
      this.person.ForceRotationAndHold(DirectionPressed.Down, 0.0f);
      this.person.scale = 2f;
      this.text.paragraph.AutoCompleteParagraph();
      if (!IsAnimal)
        return;
      this.alienentry = new PrisonerSprite(animaltype, 3);
      this.alienentry.scale = 2f;
    }

    public void UpdateChoiceEntry(float DeltaTime)
    {
      if ((double) this.Delay >= 0.0)
      {
        this.Delay -= DeltaTime;
      }
      else
      {
        this.Nlerper.UpdateLerpHandler(DeltaTime);
        int num = (int) this.lerper.OnUpdate(DeltaTime);
      }
      this.Offset.X = (float) ((1.0 - (double) this.lerper.CurrentValue) * 100.0);
      this.Offset.X = this.Nlerper.Value * 400f;
      if (!this.OnLeft)
        return;
      this.Offset.X *= -1f;
    }

    public void DrawChoiceEntry()
    {
      this.textbutton.vLocation.Y = 80f;
      this.Offset.Y = 0.0f;
      this.Offset += this.Location;
      this.FrameForWHoleThing.DrawGameObjectNineSlice(AssetContainer.pointspritebatchTop05, AssetContainer.SpriteSheet, this.Offset, new Vector2(300f, 550f));
      this.text.DrawSimpleTextHandler(this.Offset, 1f, AssetContainer.pointspritebatchTop05);
      this.textbutton.DrawTextButton(this.Offset, 1f, AssetContainer.pointspritebatchTop05);
      if (this.alienentry != null)
      {
        this.alienentry.scale = 5f;
        this.alienentry.DrawPrisonerSprite(this.Offset, AssetContainer.pointspritebatchTop05);
      }
      else
        this.person.ScreenSpaceDraw(this.Offset, AssetContainer.pointspritebatchTop05);
    }
  }
}
