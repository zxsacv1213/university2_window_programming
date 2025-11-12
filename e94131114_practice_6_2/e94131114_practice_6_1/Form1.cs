using System.Text.Json;

namespace e94131114_practice_6_1
{
    public partial class Form1 : Form
    {
        //基本資料~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        const int blockSize = 70;
        const int cols = 18;
        const int rows = 12;
        public static int count = 0;
        public static int palseCoice = 0;
        PictureBox pictureBoxPlayer; //宣告主角
        PictureBox myhouse; //房子
        Panel canvas;  //存 "控制項" (外層)
        Panel map; //(內層)
        PictureBox[,] blocks = new PictureBox[rows, cols]; //存所有背景方塊(pbr)
        Random rnd = new Random();
        Image dirt = Image.FromFile(@"..\..\..\..\images\dirt.png");
        Image grass = Image.FromFile(@"..\..\..\..\images\grass.png");
        Image path = Image.FromFile(@"..\..\..\..\images\path.png");
        Image player = Image.FromFile(@"..\..\..\..\images\playerSprite.png");
        Image player2 = Image.FromFile(@"..\..\..\..\images\playerSprite2.png");
        Image house = Image.FromFile(@"..\..\..\..\images\house.png");
        Image hoed = Image.FromFile(@"..\..\..\..\images\hoed.png");
        Image parsnip = Image.FromFile(@"..\..\..\..\images\parsnip.png");
        Image seeded = Image.FromFile(@"..\..\..\..\images\seeded.png");
        Image sprout = Image.FromFile(@"..\..\..\..\images\sprout.png");
        List<Image> anime;
        int countAni = 0;
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) //初始設定
        {
            labelChangeDay.Visible = false;
            //若要固定視窗大小:this.FormBorderStyle = FormBorderStyle.FixedSingle;
            initializeMap();  //初始化地圖
            creatBlock();     //鋪方塊
            placeMeHouse(map); //放主角 房子 路

            anime = new List<Image>();
            Image aniPart;
            for (int i = 1; i <= 17; i++) {
                aniPart = Image.FromFile(@"..\..\..\..\images\Animation" + i + ".png");
                anime.Add(aniPart);
            }
            timer1.Enabled = true;
        }


        private void initializeMap()
        {
            /*整體架構:Form1 -> 外層Panel(canva) -> 內層Panel(map) ->背景格子*/


            //外層Panel:控制ScrollBar
            canvas = new Panel();
            canvas.AutoScroll = true;// 啟用自動捲動
            canvas.Dock = DockStyle.Fill;// 填滿整個視窗
            this.Controls.Add(canvas); //******將整個外層Panel新增至Form內

            //內層Panel:大地圖(畫面裝不下的)
            map = new Panel();
            map.Size = new Size(blockSize * cols, blockSize * rows);
            canvas.Controls.Add(map); //******將整個內層Panel新增至外層Panel內
                                      //Form1 -> 外層Panel(canva) -> 內層Panel(map)

            //建立空白格子（PictureBox）
            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < cols; x++)
                {
                    PictureBox pb = new PictureBox();
                    pb.Size = new Size(blockSize, blockSize);
                    pb.Location = new Point(x * blockSize, y * blockSize);//Location定位控件左上角 在哪個位置。
                    pb.SizeMode = PictureBoxSizeMode.StretchImage; //此模式會將圖片拉伸，填滿整個PictureBox
                    pb.MouseDown += Form1_MouseDown;  //pb被鼠標點擊觸發
                    map.Controls.Add(pb); //******將格子新增至內層Panel內
                    blocks[y, x] = pb;   //Form1 -> 外層Panel(canva) -> 內層Panel(map) ->格子
                }
            }


        } //初始化地圖
        private void creatBlock() //規劃 並建立 地形
        {
            /*第一步，規劃哪裡草哪裡土*/
            //理想草地占比、擴散平滑次數可作微調

            int[,] dirtOrGrass = new int[rows, cols]; //以二維數字陣列指代二維地圖
            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < cols; x++)
                {
                    dirtOrGrass[y, x] = (rnd.NextDouble() < 0.3) ? 1 : 0;
                    //0.3為我設定的理想草地占比    ；   0位置為土，1位置為草
                }
            }
            //dirtOrGrass為最初版，隨機生成的、雜亂沒規律的地形

            // 進行2次「擴散平滑」讓草地群聚
            for (int iter = 0; iter < 2; iter++)
            {
                int[,] newDOG = new int[rows, cols];
                for (int y = 0; y < rows; y++)
                {
                    for (int x = 0; x < cols; x++)
                    {
                        int neighbors = CountNeighbors(dirtOrGrass, x, y);
                        if (neighbors >= 4)
                            newDOG[y, x] = 1; // 草多(周圍8格過半是草) → 留草
                        else if (neighbors <= 2)
                            newDOG[y, x] = 0; // 草少 → 變土
                        else
                            newDOG[y, x] = dirtOrGrass[y, x]; // 保持
                    }
                }
                dirtOrGrass = newDOG;//更新地圖規劃
            }

            /*第二步 正式生成地形*/
            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < cols; x++)
                {
                    blocks[y, x].Image = (dirtOrGrass[y, x] == 1) ? grass : dirt;
                }
            }
        }




        private int CountNeighbors(int[,] map, int x, int y) // 計算周圍8格有多少草(配合在creatBlock()呼叫的前後文)
        {
            int count = 0;
            for (int dy = -1; dy <= 1; dy++)
            {
                for (int dx = -1; dx <= 1; dx++)
                {
                    if (dx == 0 && dy == 0) continue; //dx,dy皆零就是本格，而要確認的是它鄰居
                    int nx = x + dx;
                    int ny = y + dy;  //(nx,ny)即是鄰居座標
                    if (nx >= 0 && nx < cols && ny >= 0 && ny < rows)//確保沒超出地圖邊界
                    {
                        if (map[ny, nx] == 1) count++; //算草地數
                    }
                }
            }
            //回傳草地數
            return count;
        }

        private void placeMeHouse(Panel map)
        {
            //設定主角
            pictureBoxPlayer = new PictureBox();
            pictureBoxPlayer.Size = new Size(80, 80);
            pictureBoxPlayer.Image = player;
            pictureBoxPlayer.Location = new Point(70 + 125, 380 + 5);//更新主角位置
            pictureBoxPlayer.SizeMode = PictureBoxSizeMode.StretchImage;
            map.Controls.Add(pictureBoxPlayer);

            pictureBoxPlayer.Move += pb_Move; //主角移動時觸發

            //設定房子
            myhouse = new PictureBox();
            myhouse.Size = new Size(320, 440);
            myhouse.Image = house;
            myhouse.Location = new Point(70, -20);
            myhouse.SizeMode = PictureBoxSizeMode.StretchImage;
            map.Controls.Add(myhouse);
            myhouse.BringToFront();
            pictureBoxPlayer.BringToFront();//主角在最上層

            //設定地磚步道
            for (int i = 0; i < cols; i++)
            {
                blocks[6, i].Image = path;
            }

        } //初始化主角房子路

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            //切換工具
            if (e.KeyCode == Keys.D1) labelTool.Text = "None";
            if (e.KeyCode == Keys.D2) labelTool.Text = "Scythe";//鐮刀
            if (e.KeyCode == Keys.D3) labelTool.Text = "Hoe";//鋤頭
            if (e.KeyCode == Keys.D4) labelTool.Text = "Can";//灑水湖
            if (e.KeyCode == Keys.D5) labelTool.Text = "Seed";//種子

            //部 移動
            //由於房子的位置，往下OR右走時沒有撞房子的可能性，故碰撞檢測只做左上
            if (e.KeyCode == Keys.W)
            {
                pictureBoxPlayer.Top -= 20;
                if (checkPHCollision(pictureBoxPlayer, myhouse))
                {
                    if (!checkPlayerDoorCollision()) pictureBoxPlayer.Top += 25;
                }
            }
            if (e.KeyCode == Keys.A)
            {
                pictureBoxPlayer.Left -= 20;
                pictureBoxPlayer.Image = player2;
                if (checkPHCollision(pictureBoxPlayer, myhouse))
                {
                    if (!checkPlayerDoorCollision()) pictureBoxPlayer.Left += 25;
                }
            }
            if (e.KeyCode == Keys.S) pictureBoxPlayer.Top += 20;
            if (e.KeyCode == Keys.D)
            {
                pictureBoxPlayer.Left += 20;
                pictureBoxPlayer.Image = player;
            }

            if (e.KeyCode == Keys.Escape)
            {
                Form formpalse = new FormPalse();
                formpalse.ShowDialog(); //顯示暫停form並暫停此form
                ReadAndWrite(); //存或讀檔
            }

        } //按鍵盤

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            labelChangeDay.Visible = false;
            PictureBox ablock = sender as PictureBox; //sender儲存"誰(picturebox)觸發了本事件"
            if (e.Button == MouseButtons.Left)
            {//按鼠標左鍵時 
                if (((ablock.Top - pictureBoxPlayer.Top) * (ablock.Top - pictureBoxPlayer.Top) + (ablock.Left - pictureBoxPlayer.Left) * (ablock.Left - pictureBoxPlayer.Left)) <= 9 * blockSize * blockSize) //確認距離
                {
                    if (ablock.Image == grass && labelTool.Text == "Scythe") ablock.Image = dirt;//鐮刀 + 草 → 泥土
                    
                    else if ((ablock.Image == grass || ablock.Image == dirt) && labelTool.Text == "Hoe") ablock.Image = hoed;//鋤頭 + 泥土 or 草地 → 耕地
                    else if (ablock.Image == hoed && labelTool.Text == "Hoe") ablock.Image = dirt;//鋤頭 + 耕地 → 泥土
                    else if (ablock.Image == hoed && labelTool.Text == "Seed") ablock.Image = seeded;//種子 + 耕地 → 種下歐防風
                    else if ((ablock.Image == seeded || ablock.Image == sprout || ablock.Image == parsnip) && labelTool.Text == "Hoe") { ablock.Image = hoed; ablock.BorderStyle = BorderStyle.None; }//鋤頭 + 作物(包括成熟) → 耕地 (移除
                    else if ((ablock.Image == seeded || ablock.Image == sprout ) && labelTool.Text == "Can") ablock.BorderStyle = BorderStyle.Fixed3D;//灑水壺 + 非成熟作物 → 澆水
                                                                                                                                                                                //這裡用else if確保只執行其中一種情況

                }

            }
            else if (e.Button == MouseButtons.Right)
            {    //按鼠標右鍵時
                if (ablock.Image == parsnip)
                {
                    ablock.BorderStyle = BorderStyle.None;
                    count++;
                    ablock.Image = hoed;
                }
            }
        } //處理鼠標按下的反應(配合初始化地圖時的picturebox)


        private void pb_Move(object sender, EventArgs e)//主角移動時觸發
        {
            CheckBoundaryCollision();
            if (!(pictureBoxPlayer.Top == 385 && pictureBoxPlayer.Left == 195)) labelChangeDay.Visible = false;

            if (pictureBoxPlayer.Top + 40 > (-20 + 240) && pictureBoxPlayer.Top + 40 < (-20 + 370))
            {
                if (pictureBoxPlayer.Left + 40 > (70 + 125) && pictureBoxPlayer.Left + 40 < (70 + 190))
                {
                    //(-20,70)是房子左上位置，40是主角大小的一半

                    DialogResult op;
                    op = MessageBox.Show("要睡覺？", "睡覺", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (op == DialogResult.Yes) //換日
                    {

                        labelChangeDay.Visible = true;

                        //作物生長
                        for (int y = 0; y < rows; y++)
                        {
                            for (int x = 0; x < cols; x++)
                            {
                                if (blocks[y, x].Image == seeded && blocks[y, x].BorderStyle == BorderStyle.Fixed3D)
                                {
                                    blocks[y, x].Image = sprout;
                                    blocks[y, x].BorderStyle = BorderStyle.None;
                                }
                                else if (blocks[y, x].Image == sprout && blocks[y, x].BorderStyle == BorderStyle.Fixed3D)
                                {
                                    blocks[y, x].Image = parsnip;
                                    blocks[y, x].BorderStyle = BorderStyle.None;
                                }
                            }
                        }
                    }

                    pictureBoxPlayer.Location = new Point(70 + 125, 380 + 5);//碰門更新主角位置
                }

            } //碰門


        }

        private bool checkPlayerDoorCollision()
        {
            if (pictureBoxPlayer.Top + 40 > (-20 + 240) && pictureBoxPlayer.Top + 40 < (-20 + 370))
            {
                if (pictureBoxPlayer.Left + 40 > (70 + 125) && pictureBoxPlayer.Left + 40 < (70 + 190))
                {
                    return true;
                }
            }
            return false;
        } //檢測碰門

        private bool checkPHCollision(PictureBox P, PictureBox H)
        {
            //Rectangler決定碰撞體積
            Rectangle PlayerRec = new Rectangle(P.Left, P.Top + 30, P.Width, P.Height); //主角腦袋一部分無碰撞需求
            Rectangle HouseRec = new Rectangle(H.Left, H.Top, H.Width, H.Height - 70); //(房子前有可走的地方)
            return PlayerRec.IntersectsWith(HouseRec);
        }  //檢測主角碰撞房子

        private void CheckBoundaryCollision()
        {
            // 檢查左邊界
            if (pictureBoxPlayer.Left < 0)
                pictureBoxPlayer.Left = 0;

            // 檢查上邊界
            if (pictureBoxPlayer.Top < 0)
                pictureBoxPlayer.Top = 0;

            // 檢查右邊界
            if (pictureBoxPlayer.Right > map.Width)
                pictureBoxPlayer.Left = map.Width - pictureBoxPlayer.Width;

            // 檢查下邊界
            if (pictureBoxPlayer.Bottom > map.Height)
                pictureBoxPlayer.Top = map.Height - pictureBoxPlayer.Height;
        } //防超出邊界


        private void ReadAndWrite()
        {
            if (palseCoice == 0) return; //palse處選返回遊戲
            else if (palseCoice == 1) //...存檔
            {
                Save.countSave = count;
                Save.blocksRWTrans = new List<List<int>>(); //以數字表示各方塊
                for (int y = 0; y < rows; y++)
                {
                    List<int> row = new List<int>();
                    for (int x = 0; x < cols; x++)
                    {
                        //0路；1泥土;2草;3耕地;4種;5發芽;6結果;7種澆水;8發芽澆水
                        if (blocks[y, x].Image == path) row.Add(0);
                        else if (blocks[y, x].Image == dirt) row.Add(1);
                        else if (blocks[y, x].Image == grass) row.Add(2);
                        else if (blocks[y, x].Image == hoed) row.Add(3);
                        else if (blocks[y, x].Image == seeded && blocks[y, x].BorderStyle != BorderStyle.Fixed3D) row.Add(4);
                        else if (blocks[y, x].Image == sprout && blocks[y, x].BorderStyle != BorderStyle.Fixed3D) row.Add(5);
                        else if (blocks[y, x].Image == parsnip) row.Add(6);
                        else if (blocks[y, x].Image == seeded) row.Add(7);
                        else if (blocks[y, x].Image == sprout) row.Add(8);
                    }
                    Save.blocksRWTrans.Add(row);
                }

                //進行存檔動作：把數據寫成.json檔，存到->bin->debug->net8.0->(這裡)
                string jsonMap = JsonSerializer.Serialize(Save.blocksRWTrans);
                string jsonCount = JsonSerializer.Serialize(Save.countSave);
                File.WriteAllText("map.json", jsonMap);
                File.WriteAllText("Count.json", jsonCount);


                MessageBox.Show("存檔成功，即將重啟", "存檔成功", MessageBoxButtons.OK);
                Application.Restart();
            }
            else if (palseCoice == 2)  //讀檔 
            {
                try
                { //把數據從.json檔拿出來
                    string jsonMap = File.ReadAllText("map.json");
                    Save.blocksRWTrans = JsonSerializer.Deserialize<List<List<int>>>(jsonMap);

                    string jsonCount = File.ReadAllText("Count.json");
                    Save.countSave = JsonSerializer.Deserialize<int>(jsonCount);
                }
                catch (FileNotFoundException)
                {
                    MessageBox.Show("你 沒 存 檔", "錯誤", MessageBoxButtons.OK);
                    return;
                }

                count = Save.countSave;
                for (int y = 0; y < rows; y++)
                {
                    for (int x = 0; x < cols; x++)
                    {//0路；1泥土;2草;3耕地;4種;5發芽;6結果;7種澆水;8發芽澆水
                        if (Save.blocksRWTrans[y][x] == 0) blocks[y, x].Image = path;
                        else if (Save.blocksRWTrans[y][x] == 1) blocks[y, x].Image = dirt;
                        else if (Save.blocksRWTrans[y][x] == 2) blocks[y, x].Image = grass;
                        else if (Save.blocksRWTrans[y][x] == 3) blocks[y, x].Image = hoed;
                        else if (Save.blocksRWTrans[y][x] == 4) blocks[y, x].Image = seeded;
                        else if (Save.blocksRWTrans[y][x] == 5) blocks[y, x].Image = sprout;
                        else if (Save.blocksRWTrans[y][x] == 6) blocks[y, x].Image = parsnip;
                        else if (Save.blocksRWTrans[y][x] == 7) { blocks[y, x].Image = seeded; blocks[y, x].BorderStyle = BorderStyle.Fixed3D; }
                        else if (Save.blocksRWTrans[y][x] == 8) { blocks[y, x].Image = sprout; blocks[y, x].BorderStyle = BorderStyle.Fixed3D; }
                    }
                }
                pictureBoxPlayer.Location = new Point(70 + 125, 380 + 5);//更新主角位置
                MessageBox.Show("讀檔成功", "讀檔成功", MessageBoxButtons.OK);
                palseCoice = 0;

            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (countAni >= 17) {
                timer1.Enabled = false;
                return;
            }
            
            pictureBoxPlayer.Image = anime[countAni];
            countAni++;
        }
    }


}
