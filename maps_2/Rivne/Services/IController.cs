using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.ComponentModel;
using UserMap.ViewModel;

namespace UserMap.Services
{
    internal interface IController : INotifyPropertyChanged
    {
        IEnumerable<object> Elements { get; }
        ReadOnlyDictionary<object, KeyValuePair<object, ChangeType>> ChangedElements { get; }
        object CurrentElement { get; }
        int ChangedELementsCount { get; }

        int CurrentElementIndex { get; set; }

        void Clear();

        int IndexOf(Func<object, bool> func);

        void AddElement(object element);
        void StartAddingNewElement(object elem);
        void CancelAddingNewElement();
        void EndAddingNewElement();

        bool RemoveElementAt(int index);
        bool RemoveEmission(object element);

        void StartEditElement();
        void StartEditElement(int index);
        void CancelEditElement();
        void EndEditElement();

        void RestoreElements();
        void ClearChangedElems();
    }
}
