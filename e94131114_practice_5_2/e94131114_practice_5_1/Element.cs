using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e94131114_practice_5_1
{
    public abstract class Element  // 抽象方法：所有子類都要定義反應
    {
        public string Name;
        public int Count;

        public abstract void Skill(); 

    }//若用virtual表示允許子類別修改
}
