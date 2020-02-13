using Prism.Mvvm;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace TreeGrid002
{
    public class MainViewModel : BindableBase
    {
        private List<Person> people = new List<Person>();
        //{
        //    new Person{Name="天財太郎", Age=19},
        //    new Person{Name="天財次郎", Age=17,},
        //    new Person{Name="天財女王", Age=54, Childs=new List<Person>
        //    {
        //        new Person{Name="天財息子", Age=29},
        //        new Person{Name="天財娘", Age=27,Childs=new List<Person>
        //        {
        //            new Person { Name= "天財孫娘", Age=3 },
        //            new Person { Name= "天財孫", Age=1 },
        //        } },
        //        new Person{Name="天財末子", Age=16},
        //    } },
        //};
        //テストデータを作成する場合は上記のコメントを解除する

        /// <summary>
        /// 表示用に入れ子データを組みなおして1次元リストにしたもの
        /// </summary>
        public ObservableCollection<PersonItem> PeopleItems { get; private set; } = new ObservableCollection<PersonItem>();


        public MainViewModel()
        {
            //テストデータを作成する場合は以下のコメントを解除する
            //var JSoptions = new JsonSerializerOptions
            //{
            //    Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
            //    WriteIndented = true
            //};
            //var json = JsonSerializer.Serialize(People, JSoptions);
            //File.WriteAllText("data.json", json);
            people.Clear();
            people.AddRange(JsonSerializer.Deserialize<List<Person>>(File.ReadAllText("data.json")));
            addChildItem(people);
        }

        private void addChildItem(IReadOnlyList<Person> childs, PersonItem parent = null)
        {
            foreach (var child in childs)
            {
                var citem = new PersonItem(child, parent);
                parent?.AddChild(citem);
                PeopleItems.Add(citem);
                addChildItem(child.Childs, citem);
            }
        }

    }

}
