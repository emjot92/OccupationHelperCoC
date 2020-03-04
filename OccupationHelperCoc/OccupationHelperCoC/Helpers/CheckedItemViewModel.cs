using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;

namespace OccupationHelperCoC.Helpers
{
    public class CheckedItemViewModel : ReactiveObject, IEquatable<CheckedItemViewModel>, ICheckedChangeableItem
    {
        public CheckedItemViewModel(string name, bool isChecked)
        {
            Name = name;
            IsChecked = isChecked;
        }

        public string Name { get; }

        [Reactive]
        public bool IsChecked { get; set; }

        public override bool Equals(object obj) => Equals(obj as CheckedItemViewModel);

        public bool Equals(CheckedItemViewModel other) => other != null && Name == other.Name;

        public override int GetHashCode() => 539060726 + EqualityComparer<string>.Default.GetHashCode(Name);
    }

    public interface ICheckedChangeableItem
    {
        bool IsChecked { get; set; }
    }
}
