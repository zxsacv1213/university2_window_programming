using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e94131114_practice_3_1
{
    class Product {   //class在第一層(商品表功能第一部分)
        public string Name;
        public int N_;
        public double Price, Weight;
        public double Len, Wide, High;
        public string Date, Birth;
    }



    internal class Program
    {
        static List<Product> products = new List<Product>(); //List在第二層(商品表功能第二部分)
                                       /*products為整份產品清單(List)，Product為清單內產品的格式*/

        static void Main(string[] args)
        {
            int choise = 0;
            string choi_test;

            string Prompt(string message)  //打字同時讀字
            {
                Console.Write(message);
                return Console.ReadLine();
            }

            while (choise>=0 && choise<=5) {  //輸入0-5以外結束
                switch (choise) {
                    case 0:
                        Console.Write("\n請選擇功能:\n1.新增商品\n2.修改商品\n3.刪除商品\n4.查詢商品\n5.顯示所有商品\n請輸入選項(1-5):");
                        choi_test = Console.ReadLine();
                        if (!int.TryParse(choi_test, out choise)) {
                            Console.WriteLine("輸入錯誤，請重新選擇。");
                        } // 防非數字
                        Console.Write("\n");
                        break;
                    case 1:


                        string name;
                        int N;
                        double price, weight;
                        double len, wide, high;
                        string date, birth;

                        name = Prompt("請輸入商品名稱:");
                        while (products.Any(p => p.Name == name)) {    //防重複
                            Console.WriteLine("商品名重複，請重新輸入");
                            name = Prompt("請輸入商品名稱:");
                        }




                        while (true)                                      //數量(防呆
                        {
                            string N_test = Prompt("請輸入商品數量:");
                            if (!int.TryParse(N_test, out N))
                            { //N_test轉數字輸出給N，若失敗(==0)則執行裡面
                                Console.WriteLine("無效的數量，請重新輸入");
                                continue;
                            }
                            if (N < 0)
                            {
                                Console.WriteLine("無效的數量，請重新輸入");
                                continue;
                            }

                            break; //為數字且非負
                        }



                        while (true)                                      //價(防呆
                        {
                            string pr_test = Prompt("請輸入商品價格:");
                            if (!double.TryParse(pr_test, out price))
                            {
                                Console.WriteLine("無效的價格，請重新輸入");
                                continue;
                            }
                            if (price < 0)
                            {
                                Console.WriteLine("無效的價格，請重新輸入");
                                continue;
                            }

                            break; //為數字且非負
                        }


                        while (true)                                      //重(防呆
                        {
                            string wei_test = Prompt("請輸入商品重量(克):");
                            if (!double.TryParse(wei_test, out weight))
                            {
                                Console.WriteLine("無效的重量，請重新輸入");
                                continue;
                            }
                            if (weight < 0)
                            {
                                Console.WriteLine("無效的重量，請重新輸入");
                                continue;
                            }

                            break; //為數字且非負
                        }


                        while (true)                                       //長(防呆
                        {
                            string len_tset = Prompt("請輸入商品長度(公分):");
                            if (!double.TryParse(len_tset, out len))
                            {
                                Console.WriteLine("無效的長度，請重新輸入");
                                continue;
                            }
                            if (len < 0)
                            {
                                Console.WriteLine("無效的長度，請重新輸入");
                                continue;
                            }

                            break; //為數字且非負
                        }


                        while (true)                                       //寬(防呆
                        {
                            string wi_tset = Prompt("請輸入商品寬度(公分):");
                            if (!double.TryParse(wi_tset, out wide))
                            {
                                Console.WriteLine("無效的寬度，請重新輸入");
                                continue;
                            }
                            if (wide < 0)
                            {
                                Console.WriteLine("無效的寬度，請重新輸入");
                                continue;
                            }

                            break; //為數字且非負
                        }


                        while (true)                                       //高(防呆
                        {
                            string hi_tset = Prompt("請輸入商品高度(公分):");
                            if (!double.TryParse(hi_tset, out high))
                            {
                                Console.WriteLine("無效的高度，請重新輸入");
                                continue;
                            }
                            if (high < 0)
                            {
                                Console.WriteLine("無效的高度，請重新輸入");
                                continue;
                            }

                            break; //為數字且非負
                        }


                        birth = Prompt("請輸入商品生產日期(YYYY-MM-DD):");   //生產日期
                        date = Prompt("請輸入商品有效日期(YYYY-MM-DD):");    //有效日期

                        products.Add(new Product { Name = name, N_ = N, Price = price, Weight = weight, Len = len, Wide = wide, High = high, Birth = birth, Date = date });
                        choise = 0;
                        Console.WriteLine("商品新增成功!");
                        break;
                    case 2:
                        string name2;
                        int N2;
                        double price2;


                        name2 = Prompt("請輸入要修改的商品名稱:");
                        Product product2 = products.Find(p => p.Name == name2); /*pruduct2是對應輸入的name2的那條陣列(一項商品)*/
                        if (product2 == null) {
                            Console.WriteLine("商品不存在。\n");
                            choise = 0;
                            break;
                        }

                        while (true) {
                            string N2_test = Prompt("請輸入新的商品數量:");
                            if (!int.TryParse(N2_test, out N2)) {
                                Console.WriteLine("無效的數量，請重新輸入");
                                continue;
                            }
                            if (N2 < 0) {
                                Console.WriteLine("無效的數量，請重新輸入");
                                continue;

                            }

                            break;
                        }   //輸入數量(防呆

                        while (true)
                        {
                            string price_test = Prompt("請輸入新的商品價格:");
                            if (!double.TryParse(price_test, out price2))
                            {
                                Console.WriteLine("無效的價格，請重新輸入");
                                continue;
                            }
                            if (price2 < 0)
                            {
                                Console.WriteLine("無效的價格，請重新輸入");
                                continue;

                            }

                            break;
                        }  //輸入價格(防呆

                        product2.N_ = N2;
                        product2.Price = price2;

                        Console.WriteLine("商品新增成功!");

                        choise = 0;
                        break;
                    case 3:
                        string name3= Prompt("請輸入要刪除的商品名稱："); 
                        Product product3 = products.FirstOrDefault(p => p.Name == name3);

                        if (product3 == null) {
                            Console.WriteLine("商品不存在。");
                        }
                        else { 
                        products.Remove(product3);
                        Console.WriteLine("商品刪除成功！");
                        }

                        choise = 0;
                        break;
                    case 4:
                        string name4 = Prompt("請輸入要查詢的商品名稱：");
                        Product product4 = products.Find(p=>p.Name==name4);

                        if (product4 == null) Console.WriteLine("商品不存在。");
                        else {
                            Console.WriteLine($"商品名稱:{product4.Name}\r\n商品數量：{product4.N_}\r\n商品價格：{product4.Price:F2}\r\n商品重量：{product4.Weight:F2}\r\n商品尺寸：{product4.Len:F2}x{product4.Wide:F2}x{product4.High:F2} 公分\r\n生產日期：{product4.Birth}\r\n有效日期：{product4.Date}");

                        }


                        choise = 0;
                        break;
                    case 5:
                        if (products.Count==0) Console.WriteLine("目前沒有商品。");  //注意List為空的表達方式
                        else {
                            foreach (Product product5 in products) {
                                Console.WriteLine($"商品名稱：{product5.Name}, 數量：{product5.N_}, 價格：{product5.Price:F2}");
                           
                            }
                        
                        
                        }


                        choise = 0;
                        break;
                    default: 
                        Console.WriteLine("輸入錯誤，請重新選擇。");
                        choise = 0;
                        break;






                }







            }

        }
    }
}
