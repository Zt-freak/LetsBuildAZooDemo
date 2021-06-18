// Decompiled with JetBrains decompiler
// Type: TinyZoo.TitleScreen.GameLogo
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;

namespace TinyZoo.TitleScreen
{
  internal class GameLogo
  {
    public GameObject Logo;

    public GameLogo()
    {
      this.Logo = new GameObject();
      this.Logo.DrawRect = new Rectangle(0, 2166, 1736, 1038);
      this.Logo.scale = 0.13f;
      this.Logo.SetDrawOriginToCentre();
    }

    public void UpdateGameLogo()
    {
    }

    public void DrawGameLogo(Vector2 Offset) => this.Logo.Draw(AssetContainer.spritebatch06, AssetContainer.TitleScreen, Offset);
  }
}
