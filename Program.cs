using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;

public partial class Game : Form
{
	private Dictionary<int, Bitmap> tiles;
	private const int FORM_L = 400;
	private const int CELL_L = 100;
	private PictureBox[,] cells;
	private int[,] map;

	private void LoadTiles()
	{
		for (int i = 2; i <= 2048; i *= 2)
			tiles.Add(i, new Bitmap(Convert.ToString(i) + ".png"));

		tiles.Add(0, new Bitmap("0.png"));
	}

	private void SetUpCells()
	{
		for (int i = 0; i < cells.GetLength(0); i++)
			for (int j = 0; j < cells.GetLength(1); j++)
			{
				cells[i, j] = new PictureBox();
				cells[i, j].Size = new Size(CELL_L, CELL_L);
				cells[i, j].SizeMode = PictureBoxSizeMode.StretchImage;
			}
	}

	private void RefreshPictureBoxes()
	{
		for (int i = 0; i < 4; i++)
			for (int j = 0; j < 4; j++)
				cells[i, j].Image = tiles[map[i, j]];
	}

	private void AddPictureBoxes()
	{
		SetUpCells();

		for (int i = 0; i < cells.GetLength(0); i++)
			for (int j = 0; j < cells.GetLength(1); j++)
			{
				cells[i, j].Location = new Point(j * CELL_L, i * CELL_L);
				this.Controls.Add(cells[i, j]);
			}
	}

	public Game()
	{
		this.KeyDown += new KeyEventHandler(OnKeyboardPressed);

		cells = new PictureBox[4, 4];
		tiles = new Dictionary<int, Bitmap>();
		map = new int[4, 4];

		LoadTiles();

		this.Text = "2048";
		this.FormBorderStyle = FormBorderStyle.FixedDialog;
		this.Size = new Size(FORM_L + 6, FORM_L + 20);	
		this.StartPosition = FormStartPosition.CenterScreen;	

		AddPictureBoxes();

		AddNewTile();
	}

	private void AddNewTile()
	{
		List<int[]> vars = new List<int[]>();

		for (int i = 0; i < 4; i++)
			for (int j = 0; j < 4; j++)
				if (map[i, j] == 0)
					vars.Add(new int[] {i, j});

		Random rnd = new Random();	

		int[] newLoc = vars[rnd.Next(0, vars.Count)];
		map[newLoc[0], newLoc[1]] = 2;

		RefreshPictureBoxes();
	}

	private void GameWon()
	{

	}

	private void OnKeyboardPressed(object sender, KeyEventArgs e)
	{
		bool ifTileWasMoved = false;
		bool ifWin = false;

		switch (e.KeyCode.ToString())
		{
			case "Right":
				for (int x = 0; x < 3; x++)
					for (int y = 0; y < 4; y++)
						if (map[y, x] != 0)
						{
							if (map[y, x + 1] == 0)
							{
								map[y, x + 1] = map[y, x];
								map[y, x] = 0;
								ifTileWasMoved = true;
							}
							else if (map[y, x + 1] == map[y, x])
							{
								map[y, x + 1] = 2 * map[y, x];
								if (2 * map[y, x] == 2048)
									ifWin = true;
								map[y, x] = 0;
								ifTileWasMoved = true;
							}
						}
				break;
			case "Left":
				for (int x = 3; x > 0; x--)
					for (int y = 0; y < 4; y++)
						if (map[y, x] != 0)
						{
							if (map[y, x - 1] == 0)
							{
								map[y, x - 1] = map[y, x];
								map[y, x] = 0;
								ifTileWasMoved = true;
							}
							else if (map[y, x - 1] == map[y, x])
							{
								map[y, x - 1] = 2 * map[y, x];
								if (2 * map[y, x] == 2048)
									ifWin = true;
								map[y, x] = 0;
								ifTileWasMoved = true;
							}
						}
				break;
			case "Down":
				for (int x = 0; x < 4; x++)
					for (int y = 0; y < 3; y++)
						if (map[y, x] != 0)
						{
							if (map[y + 1, x] == 0)
							{
								map[y + 1, x] = map[y, x];
								map[y, x] = 0;
								ifTileWasMoved = true;
							}
							else if (map[y + 1, x] == map[y, x])
							{
								map[y + 1, x] = 2 * map[y, x];
								if (2 * map[y, x] == 2048)
									ifWin = true;
								map[y, x] = 0;	
								ifTileWasMoved = true;
							}
						}
				break;	
			case "Up": 
				for (int x = 0; x < 4; x++)
					for (int y = 3; y > 0; y--)
						if (map[y, x] != 0)
						{
							if (map[y - 1, x] == 0)
							{
								map[y - 1, x] = map[y, x];
								map[y, x] = 0;
								ifTileWasMoved = true;
							}
							else if (map[y - 1, x] == map[y, x])
							{
								map[y - 1, x] = 2 * map[y, x];
								if (2 * map[y, x] == 2048)
									ifWin = true;
								map[y, x] = 0;
								ifTileWasMoved = true;
							}
						}	
				break;    Sosi
		}

		if (ifTileWasMoved)
			AddNewTile();
		if (ifWin)
			GameWon();
	}
}

public class Program
{
	[STAThread]
	public static void Main()
	{
		Application.Run(new Game());	
	}
}
