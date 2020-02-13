using System.Collections.Generic;

namespace TreeGrid002
{
    /// <summary>
    /// 入れ子データ
    /// </summary>
    public class Person
    {
        // 名前
        public string Name { get; set; }

        // 年齢
        public int Age { get; set; }

        public List<Person> Childs { get; set; } = new List<Person>();
    }
}
