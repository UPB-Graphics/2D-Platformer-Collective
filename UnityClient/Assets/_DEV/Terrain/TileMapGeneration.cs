using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class TileMapGeneration : MonoBehaviour
{
    public Tilemap tilemap;
    public Tilemap backgroundTilemap;
    public TileBase tile;
    public TileBase[] rocks = new TileBase[3];
    public TileBase[] grass = new TileBase[6];
    public TileBase[] bushes = new TileBase[4];
    int randomSectionWidth = 400;

    // Start is called before the first frame update
    void Start()
    {
        tilemap = GetComponent<Tilemap>();
        //tile = GetComponent<Tile>();
        //tilemap.SetTile(new Vector3Int(13, -2, 0), tile);

        System.Random rand = new System.Random();

        //Set our starting height
        int lastHeight = -3;

        //Cycle through our width
        for (int x = 0; x < randomSectionWidth; x++)
        {
            lastHeight = CreateTerrain(rand, lastHeight, x);
        }

        //Set our starting height
        lastHeight = -3;
        for (int x = -1; x > -randomSectionWidth; x--)
        {
            lastHeight = CreateTerrain(rand, lastHeight, x);
        }
    }

    private int CreateTerrain(System.Random rand, int lastHeight, int x)
    {
        #region Random Walk part
        //Roll a dice
        int nextMove = rand.Next(5);

        //If heads, and we aren't near the bottom, minus some height
        if (x != 0 && x != -1)
        {
            if (nextMove == 0 && lastHeight > -180)
            {
                lastHeight--;
                if (rand.Next(80) == 0)
                    lastHeight--;
            }//If tails, and we aren't near the top, add some height
            else if (nextMove == 4)
            {
                lastHeight++;
                if (rand.Next(80) == 0)
                    lastHeight++;
            }
        }

        //Circle through from the lastheight to the bottom
        for (int y = lastHeight; y >= -200; y--)
        {
            tilemap.SetTile(new Vector3Int(x, y, 0), tile);
        }
        #endregion

        #region Placing Rocks
        nextMove = rand.Next(12);
        if (nextMove < 3)
            tilemap.SetTile(new Vector3Int(x, lastHeight + 1, 0), rocks[nextMove]);
        #endregion

        #region Placing Grass
        nextMove = rand.Next(6);
        backgroundTilemap.SetTile(new Vector3Int(x, lastHeight + 1, 0), grass[nextMove]);
        #endregion

        #region Placing Bushes
        nextMove = rand.Next(7);
        if (nextMove < 4)
            backgroundTilemap.SetTile(new Vector3Int(x, lastHeight + 1, 0), bushes[nextMove]);
        #endregion

        return lastHeight;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
