using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e94131114_practice_2_2    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~目前進度:換日線準備好，接下來做 1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\n歡迎來到 NCKU 證券交易系統!\n");

            //工具列
            string line = "~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~";
            double money = 3000;
            int choise = -1;
            int timeee = 0;
            int Day = 1;
            double lend=0;               //還欠多少
            double lend_o = 0;             //本金
            double lend_limit = 3000;
            double buy=0;                  //以Buy存買、賣的交割款
            int stock_num = 3;
            int Day1_ = 1;
            string[] stock_name = { "台肌電", "肯德基", "我熱烈的吻" };
            int[] stock_ids = { 2330, 47, 58 };
            double[] stock_prices = { 1180, 144.58, 2 };
            int[] stock_held = { 0, 0, 0 };
            double[] stock_dividends = { 0.01, 0.05, 0.5 };
            string[] hist = {" "," "," "," "," "}; //記歷史

            double stock_change = (stock_prices[0]+ stock_prices[1]+ stock_prices[2]);  //記大盤走向(getR裡面還有)
            double[] stock_chanlist = new double[4];
            stock_chanlist[3]=stock_change;
            double[] listchange = { 0, 0, 0 };

            

            Random random = new Random();
            void GetR()  //時間推進
            {
                for (int j = 0; j < 3; j++) //股價改變
                {

                    double RR = (random.NextDouble() * 2 - 1) * 0.1;
                    stock_prices[j] = stock_prices[j] * (1 + RR);
                }

                timeee++;

                for (int iii = 0; iii < 3; iii++) stock_chanlist[iii] = stock_chanlist[iii+1];   //紀錄大盤走向
                stock_chanlist[3]= (stock_prices[0] + stock_prices[1] + stock_prices[2]);
                for (int iiii = 0; iiii < 3; iiii++) listchange[iiii] = stock_chanlist[iiii + 1] - stock_chanlist[iiii];

                if ((timeee % 3 == 0) && timeee != 0 && timeee!=3) //換日
                {
                    Day++;
                    Console.WriteLine("\n----------國際換日線------------------------------------------------------");
                    Console.WriteLine("收到的股息金額 {0:F2}", ((stock_held[0] * stock_prices[0]* stock_dividends[0]) +(stock_held[1] * stock_prices[1] * stock_dividends[1]) + (stock_held[2] * stock_prices[2] * stock_dividends[1])));
                    money += ((stock_held[0] * stock_prices[0] * stock_dividends[0]) + (stock_held[1] * stock_prices[1] * stock_dividends[1]) + (stock_held[2] * stock_prices[2] * stock_dividends[2]));   //收股息

                    Console.WriteLine("貸款還了{0:F2}、利息{1:F2}尚餘{2:F2}",lend_o*0.2,lend*0.1, lend-(lend_o*0.2)); //處理貸款
                    money -= (lend_o * 0.2+ lend * 0.1); //還本金*0.2+一趴的利息
                    lend -= (lend_o * 0.2);

                    Console.WriteLine("最後交割金額:{0:F2}",buy);   //處理交割
                    if ((money+buy)<0 && buy!=0)  Console.WriteLine("金額不足交割...");
                    money += buy;           //buy:+表示賣股票賺 -表買股用
                    buy = 0;
                    
                }
            }

            GetR(); GetR(); GetR();  //
           

            //~~~~~~~~~~~~~~~~~~~~~

            while (choise == -1 || choise == 1 || choise == 2 || choise == 3 || choise == 4 || choise == 5)
            {
                switch (choise)
                {
                    case -1:
                        GetR();  //推時間
                        if (Day1_ == 1)
                        {
                            timeee--; //糾正，使Day1能操作三次
                            Day1_ = 0;
                        }

                        if (money <= 0) {
                            Console.WriteLine("破產，最後金額為 {0:F2}",money);
                            choise = 99; //中斷
                            break;
                        }
                        
                        Console.WriteLine(line);
                        Console.Write($"Day: {Day}  \t|\t");
                        Console.WriteLine($"Money: {money:f2}");
                        Console.WriteLine("(1) 買股\n(2) 賣股\n(3) 股市走勢\n(4) 歷史交易\n(5) 貸款(有趣的一句話)\n輸入1~5以外字元退出");
                        Console.WriteLine(line);
                        Console.Write("輸入您的操作:");
                        string choi_test = Console.ReadLine();
                        if (int.TryParse(choi_test, out choise)) ;   //防止非數字
                        else break;
                        
                        break;
                    case 1:
                        Console.WriteLine($"\n{line}");
                        Console.WriteLine("完整股市:");
                        for (int i = 0; i < 3; i++)
                        {
                            Console.WriteLine("- {0}({1}): {2:F2}", stock_name[i], stock_ids[i], stock_prices[i]);
                        }

                        //買股票
                        Console.WriteLine(line);
                        int choise_st = 0; //存id
                        while (true)
                        {
                            Console.Write("輸入希望購買的股票代號:");
                            
                            string choise_st_test = Console.ReadLine();
                            if (int.TryParse(choise_st_test, out choise_st))
                            {
                                if (choise_st == -1)  //回主頁
                                {
                                    choise = -1;
                                    break;
                                }
                                if (!stock_ids.Contains(choise_st))    //輸錯ids 重輸
                                {
                                    Console.WriteLine("未知代碼！請重新輸入或是 -1 回到主頁");
                                    continue;
                                }
                                break; //沒錯繼續
                            }
                            else
                            {
                                Console.WriteLine("未知代碼！請重新輸入或是 -1 回到主頁"); //輸入非數字錯
                                continue;
                            }
                        }

                        if (choise == -1) break; //-1回主頁


                        double byy = stock_prices[Array.IndexOf(stock_ids, choise_st)];  //找股價
                        Console.Write("輸入希望購買的股數:");
                        int input;
                        while (true)
                        {
                            string input_test = Console.ReadLine();
                            
                            if (int.TryParse(input_test, out input))    //防非數字
                            {
                                if (input > 0 || input == -1) break;
                                Console.WriteLine("錯誤，請重新輸入或是 -1 回到主頁:");//防負數
                            }
                            else Console.WriteLine("錯誤，請重新輸入或是 -1 回到主頁");
                        }
                        if (input == -1) //回主
                        {
                            choise = -1;
                            break;
                        }

                        
                        
                        Console.WriteLine("成功買入{0}共{1}股,交割金額 {2:F2}", choise_st, input, input * byy);  //inupt是股票數，byy是對應股價
                        stock_held[Array.IndexOf(stock_ids, choise_st)] += input;
                        buy -= (input * byy);         //每換日重製buy
                        choise = -1;
                        Console.WriteLine(line);

                        for (int jjj = 0; jjj < 4; jjj++) hist[jjj] = hist[jjj + 1];   //記歷史
                        hist[4] = ($"+ {stock_name[Array.IndexOf(stock_ids, choise_st)]}");


                        break;
                    case 2:
                        Console.WriteLine($"\n{line}");
                        Console.WriteLine("持有股票:");
                        for (int ii = 0; ii < 3; ii++)
                        {  //印持有者
                            if (stock_held[ii] > 0)
                            {
                                Console.WriteLine("- {0}({1}):{2:F2} 共 {3} 股", stock_name[ii], stock_ids[ii], stock_prices[ii], stock_held[ii]);
                            }
                        }
                        Console.WriteLine(line);

                        int sole;
                        while (true)
                        {
                            Console.Write("輸入希望(全部)售出的股票代號:");
                            string sole_test = Console.ReadLine();
                            
                            if (int.TryParse(sole_test, out sole))
                            {
                                if (sole == -1)
                                {
                                    choise = -1;
                                    break;    //回主(下方還有一行配合)
                                }


                                if (!stock_ids.Contains(sole))
                                {    //防呆:查無此人
                                    Console.WriteLine("未知代碼！請重新輸入或是 -1 回到主頁");
                                    continue;
                                }

                                if (stock_held[Array.IndexOf(stock_ids, sole)] == 0) //防呆:0張股票
                                {
                                    Console.WriteLine("您未持有該股票");
                                    continue;
                                }


                                break;
                            }
                            else
                            {       //防呆:非數字
                                Console.WriteLine("未知代碼！請重新輸入或是 -1 回到主頁");
                                continue;
                            }
                        }
                        if (choise == -1) break;




                        stock_num = Array.IndexOf(stock_ids, sole);
                        Console.WriteLine("賣出 {0} 共 {1} 股，交割金額 {2:F2} ", stock_ids[stock_num], stock_held[stock_num], stock_held[stock_num] * stock_prices[stock_num]);
                        buy += stock_held[stock_num] * stock_prices[stock_num];
                        stock_held[stock_num] = 0;
                        choise = -1;

                        for (int jjj = 0; jjj < 4; jjj++) hist[jjj] = hist[jjj + 1];   //記歷史
                        hist[4] = ($"- {stock_name[Array.IndexOf(stock_ids, sole)]}");

                        break;
                    case 3:
                        Console.WriteLine($"\n{line}");              //這部分分別在工具列下跟getR內
                        Console.WriteLine("大盤走勢:\n{0:+0.00;-0.00;0}, {1:+0.00;-0.00;0}, {2:+0.00;-0.00;0}", listchange[0], listchange[1], listchange[2] );
                        Console.WriteLine($"{line}\n");
                        choise = -1;
                        break;
                    case 4:
                        Console.WriteLine($"\n{line}");              //這部分在1 2內完成
                        for (int jj = 0; jj < 5; jj++) Console.WriteLine(hist[jj]);
                        Console.WriteLine($"{line}\n");
                        choise = -1;
                        break;
                    case 5:
                        Console.WriteLine($"\n{line}");

                        Console.WriteLine("我思故我在 果思故果醬");
                        if (lend > 0) {
                            Console.WriteLine("貸款尚未還完，滾");
                            choise = -1;
                            break;
                        }

                        Console.WriteLine("貸款配額還剩 {0} ,每天還本金20%,天利率為10%",lend_limit);
                        
                        string l_test;
                        double l;

                        while (true) {
                            Console.Write("請輸入希望貸款的金額:");
                            l_test =Console.ReadLine();
                            if (double.TryParse(l_test, out l))
                            {
                                if (l < 0 && l != -1) {
                                    Console.WriteLine("數字不能為負數!請重新輸入或-1回到主頁");
                                    continue;
                                }
                                
                                if (l > lend_limit) {
                                    Console.WriteLine("銀行不想借你那麼多 >-<");
                                    continue;
                                }

                                break;  // 是數字，大於0或為-1，沒超過配額
                            }
                            else {
                                Console.WriteLine("請輸入數字，或-1回主頁");
                                continue;
                            }
                        
                        }
                        if (l == -1) {
                            choise =-1;
                            break;
                        }


                        Console.WriteLine("成功貸出 {0:F2}", l);
                        lend_limit -= l;
                        money += l;
                        lend += l;
                        lend_o = l;
                        choise = -1;

                        Console.WriteLine($"{line}\n");
                        break;
                }

            }














        }


    }
}