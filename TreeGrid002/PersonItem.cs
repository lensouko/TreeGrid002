using Prism.Commands;
using Prism.Mvvm;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace TreeGrid002
{
    public class PersonItem : BindableBase
    {
        private PersonItem parentItem = null;
        public int NestLevel => (parentItem?.NestLevel ?? 0) + 1;

        private Person me;

        public string Name => $"{new string('　', NestLevel)}{me.Name}";

        public int Age => me.Age;

        private readonly List<PersonItem> childs = new List<PersonItem>();

        public void AddChild(PersonItem child)
        {
            childs.Add(child);
        }

        public bool HasChild => childs.Any();


        private bool isChildOpen = true;

        public bool IsChildOpen
        {
            get => (parentItem?.IsChildOpen ?? true) && isChildOpen;
            set
            {
                isChildOpen = value;
                SetChildVisibility();
            }
        }

        private DelegateCommand childOpenChangeCommand;

        public DelegateCommand ChildOpenChangeCommand => childOpenChangeCommand ?? (childOpenChangeCommand = new DelegateCommand(childOpenChange));

        private void childOpenChange()
        {
            IsChildOpen = !IsChildOpen;
            SetChildVisibility();
        }

        private void changeOpenState()
        {
            RaisePropertyChanged(nameof(IsChildOpen));
            RaisePropertyChanged(nameof(VisibleState));
        }

        public void SetChildVisibility()
        {
            changeOpenState();
            foreach (var item in childs)
            {
                item.SetChildVisibility();
            }
        }

        public Visibility VisibleState => (parentItem?.IsChildOpen ?? true) ? Visibility.Visible : Visibility.Collapsed;

        public PersonItem(Person person, PersonItem parent)
        {
            me = person;
            parentItem = parent;
        }


    }

}
