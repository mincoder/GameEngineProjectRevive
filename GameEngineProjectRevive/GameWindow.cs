using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using GameEngineProjectRevive.Objects;

namespace GameEngineProjectRevive
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class GameWindow : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D tileSet;
        Scene testScene;
        SpriteLayer layer1;
        SpriteLayer layer2;
        TileMap testMap;
        TileMap testMap1;
        Camera activeCamera;

        public GameWindow()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = 2 * 480;
            graphics.PreferredBackBufferWidth = 2 * 1024;
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            activeCamera = new Camera();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //testImage = Content.Load<Texture2D>("Stupid");

            testScene = new Scene(GraphicsDevice, spriteBatch);
            layer1 = new SpriteLayer();
            layer2 = new SpriteLayer();

            tileSet = Content.Load<Texture2D>("Tiles_64x64");
            testMap = new TileMap("TileMaps/Map.tmx", "TileSets/Tiles.tsx", 1);
            testMap1 = new TileMap("TileMaps/Map.tmx", "TileSets/Tiles.tsx", 2);

            testMap1.Parent = layer2;
            testMap.Parent = layer1;
            testMap.TileSet = tileSet;
            testMap1.TileSet = tileSet;
            testMap.Scale = new Vector2(0.5f, 0.5f);
            testMap1.Scale = new Vector2(0.5f, 0.5f);

            testScene.Layers.AddLast(layer1);
            testScene.Layers.AddLast(layer2);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                activeCamera.Translation += new Vector2(2, 0);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                activeCamera.Translation -= new Vector2(2, 0);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                activeCamera.Translation -= new Vector2(0, 2);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                activeCamera.Translation += new Vector2(0, 2);
            }
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            testScene.Render(activeCamera);

            base.Draw(gameTime);
        }
    }
}
