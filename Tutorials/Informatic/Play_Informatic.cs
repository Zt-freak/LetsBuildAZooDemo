// Decompiled with JetBrains decompiler
// Type: TinyZoo.Tutorials.Informatic.Play_Informatic
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.GenericUI;

namespace TinyZoo.Tutorials.Informatic
{
  internal class Play_Informatic
  {
    private int Index;
    private float ImageTimer;
    private GameObject InfoImage;
    private string TEXT;
    private BlackOut blackout;
    private float Timer;
    private int TotalCycles;
    private bool Exiting;
    private TextButton Next;

    public Play_Informatic()
    {
      this.InfoImage = new GameObject();
      this.InfoImage.DrawRect = new Rectangle(649, 0, 67, 42);
      this.InfoImage.SetDrawOriginToCentre();
      this.InfoImage.scale = 9f * Sengine.UltraWideSreenDownardsMultiplier;
      this.SetRect();
      this.TEXT = "";
      this.blackout = new BlackOut();
      this.blackout.SetAlpha(false, 0.3f, 0.0f, 0.7f);
      this.InfoImage.SetAlpha(false, 0.5f, 0.0f, 1f);
      string str = "";
      int num = GameFlags.IsConsoleVersion ? 1 : 0;
      this.Next = new TextButton(str + SEngine.Localization.Localization.GetText(14), 45f);
      this.Next.AddControllerButton(ControllerButton.XboxA);
      this.Next.vLocation = new Vector2(900f, 650f);
    }

    private void SetRect()
    {
      ++this.Index;
      ++this.TotalCycles;
      if (this.Index > 3)
        this.Index = 0;
      this.InfoImage.DrawRect = new Rectangle(649 + this.Index * 68, 0, 67, 42);
    }

    public bool UpdatePlay_Informatic(float DeltaTime, Player player)
    {
      this.blackout.UpdateColours(DeltaTime);
      this.InfoImage.UpdateColours(DeltaTime);
      this.ImageTimer += DeltaTime;
      if ((double) this.ImageTimer > 0.5)
      {
        this.ImageTimer = 0.0f;
        this.SetRect();
      }
      if (this.TotalCycles > 5)
      {
        this.Next.UpdateTextButton(player, Vector2.Zero, DeltaTime);
        if (((double) player.player.touchinput.ReleaseTapArray[0].X > 0.0 || player.inputmap.PressedThisFrame[0] || (player.inputmap.PressedBackOnController() || player.inputmap.PressedThisFrame[11])) && !this.Exiting)
        {
          this.Exiting = true;
          this.InfoImage.SetAlpha(true, 0.5f, 0.0f, 0.0f);
          this.blackout.SetAlpha(true, 0.5f, 0.0f, 0.0f);
        }
      }
      this.InfoImage.vLocation = new Vector2(512f, 350f);
      return this.Exiting && (double) this.InfoImage.fAlpha == 0.0;
    }

    public void DrawPlay_Informatic()
    {
      this.blackout.DrawBlackOut(Vector2.Zero, AssetContainer.pointspritebatchTop05);
      this.InfoImage.Draw(AssetContainer.pointspritebatchTop05, AssetContainer.SpriteSheet);
      TextFunctions.DrawJustifiedText(this.TEXT, 4f, new Vector2(512f, 50f), new Color(ColourData.IconOrange), this.InfoImage.fAlpha, AssetContainer.springFont, AssetContainer.pointspritebatchTop05);
      if (this.TotalCycles <= 5)
        return;
      this.Next.DrawTextButton(Vector2.Zero, 1f, AssetContainer.pointspritebatchTop05);
    }
  }
}
