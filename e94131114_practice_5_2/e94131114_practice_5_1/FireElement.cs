using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e94131114_practice_5_1
{
    public class FireElement: Element // 宣告一個新類別（說明它是誰）
    {
        public FireElement() { //類別被建立時會做什麼（初始化設定）  ，此為建構子
            Name = "Fire";
            Count = 0;
        }

        public override void Skill()
        {
            
        }
    }
}
