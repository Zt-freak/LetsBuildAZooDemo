// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.AvatarViewer.AvatarThoughts
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.GenericUI;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_SummaryPopUps.People.AvatarViewer
{
  internal class AvatarThoughts
  {
    public Vector2 location;
    private AnimalInFrame avatarImage;
    private CustomerFrame customerFrame;
    private SimpleTextHandler para;
    private ZGenericText header;
    private float imageSize;
    private float BaseScale;

    public AvatarThoughts(Player player, float _BaseScale, float ForceThisWidth = -1f)
    {
      this.BaseScale = _BaseScale;
      UIScaleHelper uiScaleHelper = new UIScaleHelper(this.BaseScale);
      Vector2 defaultBuffer = uiScaleHelper.DefaultBuffer;
      this.customerFrame = new CustomerFrame(Vector2.Zero, CustomerFrameColors.Brown, this.BaseScale);
      Vector2 _vScale = Vector2.Zero + defaultBuffer;
      this.imageSize = 50f * this.BaseScale;
      this.avatarImage = new AnimalInFrame((AnimalType) player.Stats.ZooKeeperAvatarIndex, AnimalType.None, TargetSize: this.imageSize, FrameEdgeBuffer: (6f * this.BaseScale), BaseScale: this.BaseScale);
      this.avatarImage.Location = _vScale;
      this.avatarImage.Location += this.avatarImage.GetSize() * 0.5f;
      _vScale.X += this.avatarImage.GetSize().X;
      _vScale.X += defaultBuffer.X;
      float width_ = uiScaleHelper.ScaleX(300f);
      if ((double) ForceThisWidth != -1.0)
        width_ = ForceThisWidth - defaultBuffer.X * 2f - this.avatarImage.GetSize().X;
      else
        ForceThisWidth = width_ + defaultBuffer.X * 2f + this.avatarImage.GetSize().X;
      Vector2 vector2_1 = _vScale;
      _vScale.Y += this.avatarImage.GetSize().Y;
      _vScale.Y += defaultBuffer.Y;
      this.header = new ZGenericText("Thoughts", this.BaseScale, false, _UseOnePointFiveFont: true);
      this.header.vLocation = vector2_1;
      vector2_1.Y += this.header.GetSize().Y;
      vector2_1.Y += defaultBuffer.Y;
      this.para = new SimpleTextHandler(this.GetAvatarThoughtsText(), width_, _Scale: this.BaseScale, AutoComplete: true);
      this.para.SetAllColours(ColourData.Z_Cream);
      this.para.Location = vector2_1;
      vector2_1.Y += this.para.GetHeightOfParagraph();
      vector2_1.Y += defaultBuffer.Y;
      _vScale.Y = Math.Max(_vScale.Y, vector2_1.Y);
      _vScale.X = ForceThisWidth;
      this.customerFrame.Resize(_vScale);
      Vector2 vector2_2 = -this.customerFrame.VSCale * 0.5f;
      this.para.Location += vector2_2;
      this.avatarImage.Location += vector2_2;
      ZGenericText header = this.header;
      header.vLocation = header.vLocation + vector2_2;
    }

    public Vector2 GetSize() => this.customerFrame.VSCale;

    private string GetAvatarThoughtsText()
    {
      switch (Game1.Rnd.Next(0, 5))
      {
        case 0:
          return "Running a zoo is my dream job!";
        case 1:
          return "I am going to grow this zoo, and make it the best in the world.";
        case 2:
          return "We really need to grow our business.";
        default:
          return "The black market might give us the head start we need! But am I ok with that morally?";
      }
    }

    public void RefreshImage(Player player)
    {
      Vector2 location = this.avatarImage.Location;
      this.avatarImage = new AnimalInFrame((AnimalType) player.Stats.ZooKeeperAvatarIndex, AnimalType.None, TargetSize: this.imageSize, FrameEdgeBuffer: (6f * this.BaseScale), BaseScale: this.BaseScale);
      this.avatarImage.Location = location;
    }

    public void UpdateAvatarThoughts()
    {
    }

    public void DrawAvatarThoughts(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.customerFrame.DrawCustomerFrame(offset, spriteBatch);
      this.avatarImage.DrawAnimalInFrame(offset, spriteBatch);
      this.para.DrawSimpleTextHandler(offset, 1f, spriteBatch);
      this.header.DrawZGenericText(offset, spriteBatch);
    }
  }
}
