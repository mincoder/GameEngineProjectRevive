using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngineProjectRevive.Objects
{
    public class TileMap : GameObject
    {
        public int[,] TileData { get; private set; }//Will be changed with multiple layers
        public int Width { get; private set; }
        public int Height { get; private set; }
        public int TileHeight { get; private set; }
        public int TileWidth { get; private set; }
        public string TileSetImage { get; private set; }
        public Texture2D TileSet;

        private int TileColumns;
        private int TileCount;

        public override void Render(Camera activeCamera, GraphicsDevice device, SpriteBatch batch)
        {
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    int id = TileData[x, y];

                    if (id != -1)
                    {
                        int XCoord = id % TileColumns;
                        int YCoord = (id - XCoord) / (TileColumns);

                        Rectangle source = new Rectangle(XCoord * TileWidth, YCoord * TileHeight, TileWidth, TileHeight);
                        Rectangle destination = new Rectangle((int)(GlobalTranslation.X + Scale.X * x * TileWidth - activeCamera.Translation.X), 
                            (int)(GlobalTranslation.Y + Scale.Y * y * TileHeight - activeCamera.Translation.Y), (int)(Scale.X * TileWidth), (int)(Scale.Y * TileHeight));
                        batch.Draw(TileSet, destination, source, Color.White);
                    }
                }
            }
        }

        public TileMap(string TilePath, string TileSetPath, int layerID)
        {
            XmlDocument TileDoc = new XmlDocument();
            TileDoc.Load(TilePath);
            int[,] result = new int[0, 0];

            foreach (XmlNode n in TileDoc)
            {
                if (n.Name == "map")
                {
                    XmlNode layerData = n.ChildNodes[layerID];//May change with multiple layers

                    Width = int.Parse(layerData.Attributes[1].Value);
                    Height = int.Parse(layerData.Attributes[2].Value);

                    result = new int[Width, Height];

                    string[] raw = layerData.ChildNodes[0].InnerText.Split(',');
                    for (int y = 0; y < Height; y++)
                    {
                        for (int x = 0; x < Width; x++)
                        {
                            result[x, y] = int.Parse(raw[y * Width + x]) - 1;
                        }
                    }
                }
            }

            XmlDocument TileSetDoc = new XmlDocument();
            TileSetDoc.Load(TileSetPath);
            XmlNode mainNode = TileSetDoc.ChildNodes[1];
            TileWidth = int.Parse(mainNode.Attributes[1].Value);
            TileHeight = int.Parse(mainNode.Attributes[2].Value);
            TileCount = int.Parse(mainNode.Attributes[3].Value);
            TileColumns = int.Parse(mainNode.Attributes[4].Value);
            TileSetImage = mainNode.ChildNodes[0].Attributes[0].Value;

            TileData = result;
        }
    }
}
