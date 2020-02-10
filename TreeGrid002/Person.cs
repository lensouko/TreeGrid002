using Prism.Mvvm;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace TreeGrid002
{
    // DataGridに表示するデータ
    public class Person : BindableBase
    {

        // 名前
        private string name;
        public string Name 
        {
            get => $"{new string('　',NestLevel*2)}{name}"; 
            set => name = value; 
        }

        // 年齢
        private int age;
        public int Age
        {
            get => age;
            set
            {
                if (value < 0)
                {
                    MessageBox.Show("範囲外の値が設定されました。");
                    RaisePropertyChanged(nameof(Age));
                }
                else
                {
                    SetProperty(ref age, value);
                }
            }
        }

        private bool isOpen = true;
        public bool IsOpen
        {
            get => (parent?.isOpen ?? true) && isOpen;
            set => SetProperty(ref isOpen, value);
        }

        public Visibility Visibility => (parent?.IsOpen ?? true) ? Visibility.Visible : Visibility.Collapsed;

        public List<Person> Childs { get; set; } = new List<Person>();

        private Person parent = null;

        public int NestLevel
        {
            get
            {
                return (parent?.NestLevel ?? -1) + 1;
            }
        }

        public Person() { }

        // 子階層と親階層を設定する
        public Person(List<Person> child = null, Person parent = null)
        {
            Childs.AddRange(child);
            this.parent = parent;
        }
    }
}
