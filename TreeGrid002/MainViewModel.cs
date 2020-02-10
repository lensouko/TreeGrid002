using Prism.Mvvm;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace TreeGrid002
{
    public class MainViewModel : BindableBase
    {
        public List<Person> People { get; set; } = new List<Person>
        {
            new Person{Name="天財太郎", Age=19},
            new Person{Name="天財次郎", Age=17,},
            new Person{Name="天財女王", Age=54, Childs=new List<Person>
            {
                new Person{Name="天財息子", Age=29},
                new Person{Name="天財娘", Age=27,Childs=new List<Person>
                {
                    new Person { Name= "天財孫娘", Age=3 },
                    new Person { Name= "天財孫", Age=1 },
                } },
                new Person{Name="天財末子", Age=16},
            } },
        };

        public ObservableCollection<PersonItem> PeopleItems { get; private set; } = new ObservableCollection<PersonItem>();


        public MainViewModel()
        {
            addChildItem(People);
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
