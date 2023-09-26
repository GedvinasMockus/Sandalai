using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System.Text;
using System.Text.RegularExpressions;

namespace SwordsAndSandals.Objects
{
    public class TextBox : Component
    {
        private SpriteFont font;
        private bool selected = false;
        private bool clicked = false;
        private GameWindow gw;
        private MouseState currentMouse;
        private MouseState previousMouse;
        private int cursorTime;
        public Color PenColour { get; set; }
        public StringBuilder TextString { get; set; }
        public float TextSize { get; set; }
        public float InputScale { get; set; }
        public float CursorScale { get; set; }
        public Texture2D TextureInput { get; private set; }
        public Texture2D TextureCursor { get; private set; }
        public Vector2 Position { get; set; }
        public Vector2 CurrentTextPosition { get; set; }
        public Vector2 CursorPosition { get; set; }
        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X - TextureInput.Width, (int)Position.Y - TextureInput.Height, (int)(TextureInput.Width * 2), (int)(TextureInput.Height * 2));
            }
        }

        public TextBox(Vector2 position, Vector2 currentTextPosition, Vector2 cursorPosition, GameWindow gw, Texture2D textureInput, Texture2D textureCursor, SpriteFont font, Color penColour, float textSize, float inputScale)
        {
            Position = position;
            CurrentTextPosition = currentTextPosition;
            CursorPosition = cursorPosition;
            this.gw = gw;
            TextureInput = textureInput;
            TextureCursor = textureCursor;
            this.font = font;
            TextString = new StringBuilder();
            PenColour = penColour;
            TextSize = textSize;
            InputScale = inputScale;
            CursorScale = (textureInput.Height - 14f) / textureCursor.Height * inputScale;
            cursorTime = 0;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(TextureInput, Position, null, Color.White, 0f, new Vector2(TextureInput.Width / 2, TextureInput.Height / 2), InputScale, SpriteEffects.None, 0);
            spriteBatch.DrawString(font, TextString, new Vector2(Position.X - (TextureInput.Width / 2 - 7) * InputScale, Position.Y + (TextureInput.Height / 2 - 7) * InputScale), PenColour, 0f, new Vector2(0, 29), TextSize, SpriteEffects.None, 1);
            if (selected && cursorTime >= 0 && cursorTime < 30)
            {
                spriteBatch.Draw(TextureCursor, new Vector2(Position.X - (TextureInput.Width / 2 - 7) * InputScale + font.MeasureString(TextString).X * TextSize, Position.Y + (TextureInput.Height / 2 - 7) * InputScale), null, Color.White, 0f, new Vector2(0, TextureCursor.Height), CursorScale, SpriteEffects.None, 1);
            }
        }

        public override void Update(GameTime gameTime)
        {
            previousMouse = currentMouse;
            currentMouse = Mouse.GetState();
            if (Rectangle.Contains(currentMouse.Position) && previousMouse.LeftButton == ButtonState.Pressed || selected)
            {
                cursorTime = (cursorTime + 1) % 60;
                if (!selected)
                {
                    selected = true;
                    gw.TextInput += OnInput;
                }
                else if (!Rectangle.Contains(currentMouse.Position) && previousMouse.LeftButton == ButtonState.Pressed)
                {
                    selected = false;
                    gw.TextInput -= OnInput;
                    cursorTime = 0;
                }
            }
        }

        public void OnInput(object sender, TextInputEventArgs e)
        {
            var c = e.Character;
            Regex r = new Regex(@"^[a-zA-Z0-9_\s+!#$%&()*,.\/\|\-{}<>]$");
            if (r.IsMatch(c.ToString()))
            {
                TextString.Append(c);
            }
            else if (TextString.Length > 0 && c.Equals('\b'))
            {
                TextString.Remove(TextString.Length - 1, 1);
            }

        }
    }
}
