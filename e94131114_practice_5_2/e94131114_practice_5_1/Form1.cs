using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace e94131114_practice_5_1  //~~~~目前的下一步:
{
    public partial class Form1 : Form
    {
        //各項參數初始值
        public int time = 0, hp = 100, burnDamage = 0;
        public float moveSpeed = 10, rainSpeed = 10;//移動邏輯:一次10px，由各timer的interval決定一秒移幾次(速率) 。這個speed只是print出來，不是實際在code裡用到
        public string color = "white",clock="00:00";
        public int movement = 0; //控制移動方向，0對應不動，3左4右
        List<PictureBox> rains = new List<PictureBox>(); //存所有雨滴的list
        Random rnd = new Random();
        PictureBox player = new PictureBox(); //玩家(設定在初始化)
        FireElement fire = new FireElement(); //建立三個元素的""
        IceElement ice = new IceElement();
        GrassElement grass = new GrassElement();
        //


        private void moveTimer_Tick(object sender, EventArgs e) // 玩家timer(移動)
        {
            if (movement == 3 && player.Left>0) player.Left -= 10;
            else if(movement == 4 && player.Left<this.ClientSize.Width-player.Width) player.Left += 10;
        }

        private void rainTimer_Tick(object sender, EventArgs e)  //雨滴timer
        {
            foreach (PictureBox rain in rains.ToList()) //tolist相當於操作的是rains的副本
            {                              //避免邊看(foreach)邊移除(remove)同個list導致的程式崩潰
                rain.Top += 10;            //副本用來看(foreach)，移除(remove)是從本體rains移除
                if (rain.Top > this.ClientSize.Height)
                {
                    this.Controls.Remove(rain);  //正式把這滴雨從form1拿掉
                    rains.Remove(rain);  //把這滴雨從rains(統整所有雨滴的list)拿掉

                    continue;
                }

                //雨滴撞上玩家~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
                if (rain.Bounds.IntersectsWith(player.Bounds)) 
                {
                    //不同色觸發傷害跟碰撞效果，同色則蒐集
                    if (rain.BackColor == Color.Red)
                    {
                        if (player.BackColor != Color.Red)
                        {
                            hp--;
                            burnDamage++;
                        }
                        else if (fire.Count < 10) fire.Count++;
                    }
                    else if (rain.BackColor == Color.Blue)
                    {
                        if (player.BackColor != Color.Blue)
                        {
                            hp--;
                            burnDamage = 0;
                            if (moveSpeed < 2000) moveTimer.Interval *= 2; //每秒移動的次數降低 
                            else moveTimer.Interval = 10;
                            moveSpeed = 10 * (1000 / moveTimer.Interval);
                        }
                        else if(ice.Count < 10) ice.Count++;
                    }
                    else if (rain.BackColor == Color.Green)
                    {
                        hp += 5;  //總之回5血
                        if (hp > 100) hp = 100;
                        if (player.BackColor == Color.Green && grass.Count<10) grass.Count++;
                    }
                    else hp--; //黑雨
                    updateInform();
                    this.Controls.Remove(rain);
                    rains.Remove(rain);


                    //血量歸零
                    if (hp <= 0) {  
                        gameTimer.Enabled = false;
                        moveTimer.Enabled = false;
                        rainTimer.Enabled = false;
                        creatRainTimer.Enabled = false;

                        DialogResult result=MessageBox.Show($"遊戲結束！存活時間 {time} 秒","",
                            MessageBoxButtons.OK);
                        if (result == DialogResult.OK) Application.Exit();
                        return;
                    }
                    updateInform();
                }
            }
        }

        private void gameTimer_Tick(object sender, EventArgs e) //遊戲總計時器
        {
            time++;
            int min = time / 60;
            int sec = time % 60;
            clock = $"{min:00}:{sec:00}";

            //更新雨速
            if (rainSpeed < 2000)
            {
                int newRainInterval = (int)(rainTimer.Interval * 0.95);
                rainTimer.Interval = Math.Max(1, newRainInterval);
                rainSpeed = 10 * (1000 / rainTimer.Interval); //每次動10px * 每秒動幾次
            }
            if (rainSpeed >= 2000) rainSpeed = 2000;

            //更新玩家速
            if (moveSpeed < 2000)
            {
                int newMoveInterval = (int)(moveTimer.Interval * 0.95);
                moveTimer.Interval = Math.Max(1, newMoveInterval);
                moveSpeed = 10 * (1000 / moveTimer.Interval); //同理雨滴
            }
            if (moveSpeed >= 2000) moveSpeed = 2000;

            //計算燒傷
            if(player.BackColor!=Color.Red)hp-=burnDamage;

            updateInform();
        }



        public Form1() 
        {
            InitializeComponent();
        }

        

        private void Form1_Load(object sender, EventArgs e)
        {
            infoLabel.Top = 5;
            infoLabel.Left = this.ClientSize.Width - 180;
            infoLabel.Font = new Font("Consolas", 9);
            
            elementLabel.Text = ("test");  //更新中~_~_~__~_~_~~__~_~_~_~_~_
            elementLabel.Left = 20;
            elementLabel.Top = this.ClientSize.Height  - 15;

            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            Initialize();
        } //起點

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.A) movement = 3;
            else if (e.KeyCode == Keys.Right || e.KeyCode == Keys.D) movement = 4;

            else if (e.KeyCode == Keys.D1) player.BackColor = Color.Red;
            else if (e.KeyCode == Keys.D2) player.BackColor = Color.Blue;
            else if (e.KeyCode == Keys.D3) player.BackColor = Color.Green;
            else if (e.KeyCode == Keys.Space) releaseSkill();
            updateInform();
        } //感應按下鍵盤(調整移動方向 & 切換主角元素)

        private void releaseSkill() {
            if (player.BackColor == Color.Red && fire.Count==10) {
                fire.Count = 0;
                foreach (PictureBox rain in rains.ToList()) {
                    this.Controls.Remove(rain);
                    rains.Remove(rain);
                }
            }
            if (player.BackColor == Color.Blue && ice.Count == 10)
            {
                ice.Count = 0;
                rainTimer.Interval *= 5;
                rainSpeed = 10 * (1000 / rainTimer.Interval); //每次動10px * 每秒動幾次
                if (rainSpeed >= 2000) rainSpeed = 2000;
            }
            if (player.BackColor == Color.Green && grass.Count == 10)
            {
                grass.Count = 0;
                hp = 100;
            }
        } //放技能

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.A) movement = 0;
            else if (e.KeyCode == Keys.Right || e.KeyCode == Keys.D) movement = 0;
        } //鬆開鍵盤結束動作

        private void Initialize() {
            infoLabel.Text = ($"Time: {clock}{Environment.NewLine}" +
                $"HP: {hp}{Environment.NewLine}" +
                $"Burn Damage: {burnDamage}{Environment.NewLine}" +
                $"Move Speed: {moveSpeed:F1} px/s{Environment.NewLine}" +
                $"Color: {color}{Environment.NewLine}" +
                $"Rain Speed: {rainSpeed:F1} px/s");
            elementLabel.Text = ($"[Red]: {fire.Count}/10  [Blue]: {ice.Count}/10  [Green]: {grass.Count}/10");
            //初始化所有timer
            rainTimer.Interval = 60;
            rainTimer.Enabled = true;
            moveTimer.Interval = 40;
            moveTimer.Enabled = true;
            gameTimer.Interval = 1000;
            gameTimer.Enabled = true;
            creatRainTimer.Interval = rnd.Next(300, 500);
            creatRainTimer.Enabled = true;

            //初始化player
            
            player.Size = new Size(40, 40);
            player.BackColor = Color.White; 
            player.Top = this.ClientSize.Height - player.Height - 10;
            player.Left = (this.ClientSize.Width - player.Width) / 2;
            this.Controls.Add(player);
        } //初始化 

        private void updateInform() {
            if (player.BackColor == Color.Red) color = "red";
            if (player.BackColor == Color.Green) color = "green";
            if (player.BackColor == Color.Blue) color = "blue";
            infoLabel.Text = ($"Time: {clock}{Environment.NewLine}" +
                $"HP: {hp}{Environment.NewLine}" +
                $"Burn Damage: {burnDamage}{Environment.NewLine}" +
                $"Move Speed: {moveSpeed:F1} px/s{Environment.NewLine}" +
                $"Color: {color}{Environment.NewLine}" +
                $"Rain Speed: {rainSpeed:F1} px/s");

            elementLabel.Text = ($"[Red]: {fire.Count}/10  [Blue]: {ice.Count}/10  [Green]: {grass.Count}/10");
        }//更新訊息

        private void creatRainTimer_Tick(object sender, EventArgs e)
        {
            creatRainTimer.Interval = rnd.Next(300, 500); 
            rainCreat();
        } //每0.3-0.5s生成雨滴 

        private void rainCreat() {
            
            int rainNum = rnd.Next(1, 4); //生成1-3滴雨滴
            

            for (int i=0;i<rainNum;i++) {
                int rainType = rnd.Next(1, 101); //1-100隨機數 用來決定雨滴元素
                PictureBox rain = new PictureBox();
                rain.Size = new Size(20, 20);

                if (rainType > 0 && rainType < 21)  //根據機率生成不同元素雨滴
                {
                    rain.BackColor = Color.Red;
                }
                else if (rainType < 36)
                {
                    rain.BackColor = Color.Blue;
                }
                else if (rainType < 41)
                {
                    rain.BackColor = Color.Green;
                }
                else
                {
                    rain.BackColor = Color.Black;
                }

                rain.Top = 0;
                rain.Left = rnd.Next(0, this.ClientSize.Width - rain.Width);
                this.Controls.Add(rain); //這行正式將 rain 這個PictureBox加入form1
                rains.Add( rain );
            }
            
        } //生成雨滴


    }
}
