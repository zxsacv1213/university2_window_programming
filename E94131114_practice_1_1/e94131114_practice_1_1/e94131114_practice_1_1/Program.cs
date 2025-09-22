using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _e94131114
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Welcome to NCKU CSIE Shop\n");
            Console.Write("Please enter the number of product types for today: "); 
            string producttypes = Console.ReadLine();  //輸入
            int type = Convert.ToInt32(producttypes);  //輸入商品種類數

            Dictionary<string, int> product = new Dictionary<string, int>(); //寫菜單用
            Dictionary<string, int> outcome = new Dictionary<string, int>(); //用於輸出最終營業結果
            int profit = 0; //記賣了多少錢

            for (int i = 1; i <= type; i++)

            {   //重複次數:根據商品種類數
                Console.Write("Please enter the name and price of the item {0}: ", i);
                string a; //名
                int b;    //價
                string input = Console.ReadLine();
                string[] parts = input.Split(' ');
                if (parts.Length == 2) //防呆
                {
                    a = parts[0];
                    b = Convert.ToInt32(parts[1]);
                    product.Add(a, b);  //寫進菜單
                    outcome.Add(a, 0);  //記賣多少用(從0開始)
                }
                else
                {
                    Console.Write("Invalid input, please try again!\n");
                    i--; //這輪loop不算
                }

            }

            Console.Write("輸入1新增訂單；輸入2查詢商品；輸入3刪除商品；輸入4新增商品；輸入5關店\n");
            Console.Write("Please input option: ");
            int choise = Convert.ToInt16(Console.ReadLine()); //記選項
            while (choise != 0) //終止條件
            {
                switch (choise)
                {
                    case 1:  //新增訂單
                        Console.Write("Please enter the customer's purchase information (item name, quantity, and customer's payment amount):\n");
                        string listtt = Console.ReadLine();
                        string[] parts2 = listtt.Split(' ');

                        while (parts2.Length != 3)  //防呆 (3項內容)
                        {
                            Console.Write("Invalid input, please try again!\n");
                            listtt = Console.ReadLine();
                            parts2 = listtt.Split(' ');
                        }

                        string A = parts2[0];  //買啥
                        while (!product.ContainsKey(A))    //防呆：確認買的東西有在菜單上
                        {
                            Console.Write("Invalid input, please try again!\n");
                            listtt = Console.ReadLine();
                            parts2 = listtt.Split(' ');
                            A = parts2[0];
                        }
                        int B = Convert.ToInt32(parts2[1]);  //買幾份
                        int C = Convert.ToInt32(parts2[2]);  //付多少錢

                        outcome[A] = B + outcome[A];  //以outcome紀錄各項物品共買了幾分



                        int price = product[A] * B;  //要價
                        int charge = C - price;  //要找多少

                        profit += price; //紀錄總收入

                        int count1000 = 0;
                        int count500 = 0;
                        int count100 = 0;
                        int count50 = 0;
                        int count10 = 0;
                        int count5 = 0;
                        int count1 = 0;
                        while (charge > 0) 
                        {
                            if (charge >= 1000)
                            {
                                charge -= 1000;
                                count1000++;

                            }
                            else if (charge >= 500 && charge < 1000)
                            {
                                charge -= 500;
                                count500++;

                            }
                            else if (charge >= 100 && charge < 500)
                            {
                                charge -= 100;
                                count100++;


                            }
                            else if (charge >= 50 && charge < 100)
                            {
                                charge -= 50;
                                count50++;
                            }
                            else if (charge >= 10 && charge < 50)
                            {
                                charge -= 10;
                                count10++;

                            }
                            else if (charge >= 5 && charge < 10)
                            {
                                charge -= 5;
                                count5++;

                            }
                            else if (charge >= 1 && charge < 5)
                            {
                                charge--;
                                count1++;
                            }

                        }//找錢系統

                        Console.Write("Change 1000: {0}\n", count1000);
                        Console.Write("Change 500: {0}\n", count500);
                        Console.Write("Change 100: {0}\n", count100);
                        Console.Write("Change 50: {0}\n", count50);
                        Console.Write("Change 10: {0}\n", count10);
                        Console.Write("Change 5: {0}\n", count5);
                        Console.Write("Change 1: {0}\n", count1);

                        Console.Write("Please input option: ");
                        choise = Convert.ToInt16(Console.ReadLine());
                        break; //買
                    case 2:
                        Console.Write("Please choose the query item: ");
                        string find = Console.ReadLine();
                        if(!product.ContainsKey(find)) Console.Write("Item not find! \n");  //沒查到時
                        else Console.Write("Item name:{0}, Item price:{1}, count:{2} \n", find, product[find], outcome[find]);
                        //印出 名,價,賣了多少

                        Console.Write("Please input option: ");
                        choise = Convert.ToInt16(Console.ReadLine());
                        break; //查
                    case 3:
                        Console.Write("Please choose delete item: ");
                        string dele = Console.ReadLine();
                        if (!product.ContainsKey(dele)) Console.Write("Item not found!\n"); //確認在否
                        else {
                            product.Remove(dele);
                            //outcome.Remove(dele);    此處只把商品從菜單移除而不動 最終營業結果
                        }

                        Console.Write("Please input option: ");
                        choise = Convert.ToInt16(Console.ReadLine());

                        break; //刪
                    case 4:
                        int new_c = 0; 
                        while (new_c!=1) //終止條件(配合防呆用
                        {
                            Console.Write("Please enter the item name and price: ");
                            string newpro = Console.ReadLine();

                            string[] new_parts = newpro.Split(' ');

                            if (new_parts.Length != 2) {   //輸錯時
                                Console.Write("Invalid input, please try again!\n");
                                continue;  //跳過下方程序，回到while開頭
                            }

                            string new_a = new_parts[0];         //新商品名
                            int new_b= Convert.ToInt32(new_parts[1]); //價

                            if (product.ContainsKey(new_a)) {
                                Console.Write("Invalid input\n");
                                break;
                            }

                            product.Add(new_a, new_b);   //加入菜單
                            outcome.Add(new_a, 0);    //加入最終結算表
                            new_c = 1; //中止此loop
                        }

                        Console.Write("Please input option: ");
                        choise = Convert.ToInt16(Console.ReadLine());

                        break; //增
                    case 5:
                        foreach (KeyValuePair<string, int> item in outcome)   //輸出各項物賣幾份
                        {
                            Console.WriteLine($"{item.Key}:{item.Value}");  
                        }
                        Console.WriteLine("Tayal:{0}",profit);  //輸出銷售額
                        choise = 0; //終止迴圈
                        break; //結算
                }
            }
        }
    }
}
