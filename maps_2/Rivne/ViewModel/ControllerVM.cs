using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization.Formatters.Binary;
using System.Linq;
using UserMap.Services;

namespace UserMap.ViewModel
{
    /* 
     * Dictionary<T, KeyValuePair<T, ChangeType>>:
     * T = новый элемент (если произошло удаление элемента, но его 
     * не было с самого начала, то ставим тот же, что и при удалении),
     * 
     * KeyValuePair<T, ChangeType>> - содержит пару, где: 
     *      T - старый элемент (если было добавлено элемент, то будет default),
     *      ChangeType = что произошло с этим элементом.
     */

    internal class ControllerVM<T>
    {
        private bool isAddedInternaly;
        private bool isAddingNewElem;
        private bool isRestoring;
        private int elementIndex;

        private T currentEditableElement;
        private T originElement;
        protected ObservableCollection<T> elements;
        protected Dictionary<T, KeyValuePair<T, ChangeType>> changedElements;

        public ControllerVM()
        {
            elements = new ObservableCollection<T>();
            changedElements = new Dictionary<T, KeyValuePair<T, ChangeType>>();
            elementIndex = -1;

            elements.CollectionChanged += Elements_CollectionChanged;
        }

        public ObservableCollection<T> Elements { get { return elements; } }
        public ReadOnlyDictionary<T, KeyValuePair<T, ChangeType>> ChangedElements
        {
            get
            {
                return new ReadOnlyDictionary<T, KeyValuePair<T, ChangeType>>(changedElements);
            }
        }
        public T CurrentElement
        {
            get
            {
                if (elementIndex == -1)
                {
                    return default;
                }

                return elements[elementIndex];
            }
        }
        public int ChangedELementsCount
        {
            get { return changedElements.Count; }
        }

        public int CurrentElementIndex
        {
            get { return elementIndex; }
            set
            {
                if (value < -1 && value >= elements.Count)
                {
                    throw new ArgumentOutOfRangeException("value");
                }

                elementIndex = value;
                OnPropertyChanged();
                OnPropertyChanged("CurrentElement");
            }
        }

        public int IndexOf(Func<T, bool> func)
        {
            int index = -1;
            foreach (var element in elements)
            {
                ++index;
                if (func(element))
                {
                    return index;
                }
            }

            return -1;
        }
        public int IndexOf(Func<object, bool> func)
        {
            int index = -1;
            foreach (var element in elements)
            {
                ++index;
                if (func(element))
                {
                    return index;
                }
            }

            return -1;
        }

        public void Sort<TKey>(Func<T, TKey> keySelector)
        {
            var oldElements = elements;

            elements = new ObservableCollection<T>(elements.OrderBy(keySelector).ToList());
            elements.CollectionChanged += Elements_CollectionChanged;

            oldElements.Clear();
        }

        public void Clear()
        {
            CancelEditElement();

            elements.Clear();
            changedElements.Clear();
        }

        public void AddElement(T element)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }

            CancelAddingNewElement();

            isAddedInternaly = true;
            elements.Add(element);
            isAddedInternaly = false;

            CurrentElementIndex = elements.Count - 1;
        }
        public void StartAddingNewElement(T elem)
        {
            if (elem == null)
            {
                throw new ArgumentNullException("elem");
            }

            elements.Add(elem);
            isAddingNewElem = true;
            CurrentElementIndex = elements.Count - 1;
        }
        public void CancelAddingNewElement()
        {
            if (isAddingNewElem)
            {
                elements.RemoveAt(elements.Count - 1);
                isAddingNewElem = false;

                ResetElementIndex();
            }
        }
        public void EndAddingNewElement()
        {
            if (isAddingNewElem)
            {
                isAddingNewElem = false;

                ResetElementIndex();
            }
        }

        public bool RemoveElementAt(int index)
        {
            if (index < 0 || index >= elements.Count)
                return false;

            var element = elements[index];

            return RemoveElement(element);
        }
        public bool RemoveElement(T element)
        {
            bool res = elements.Remove(element);

            if (elementIndex >= elements.Count)
            {
                CurrentElementIndex -= 1;
            }

            return res;
        }

        public void StartEditElement()
        {
            StartEditElement(elementIndex);
        }
        public void StartEditElement(int index)
        {
            if (index < 0 && index >= elements.Count)
            {
                throw new ArgumentOutOfRangeException("index");
            }

            var currentElem = elements[index];

            if (!changedElements.ContainsKey(currentElem))
            {
                originElement = currentElem;

                var newCurrentElem = CloneElement(currentElem);
                elements[index] = newCurrentElem;
                currentEditableElement = newCurrentElem;
            }
        }
        public void CancelEditElement()
        {
            if (originElement != null)
            {
                elements[elementIndex] = originElement;
                currentEditableElement = default;
                originElement = default;
            }
        }
        public void EndEditElement()
        {
            if (originElement != null)
            {
                changedElements[currentEditableElement] = new KeyValuePair<T, ChangeType>(originElement, ChangeType.Changed);
                currentEditableElement = default;
                originElement = default;
            }
        }

        public void RestoreElements()
        {
            isRestoring = true;

            foreach (var item in changedElements)
            {
                switch (item.Value.Value)
                {
                    case ChangeType.Added:
                        elements.Remove(item.Key);
                        break;
                    case ChangeType.Changed:
                        var index = elements.IndexOf(item.Key);
                        elements[index] = item.Value.Key;
                        break;
                    case ChangeType.Deleted:
                        elements.Add(item.Key);
                        break;
                }
            }

            changedElements.Clear();
            ResetElementIndex();

            isRestoring = false;
        }
        public void ClearChangedElems()
        {
            changedElements.Clear();
        }

        private void ResetElementIndex()
        {
            if (CurrentElementIndex >= elements.Count)
            {
                CurrentElementIndex = elements.Count - 1;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private T CloneElement(T target)
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();

            using (var mStream = new System.IO.MemoryStream())
            {
                binaryFormatter.Serialize(mStream, target);

                mStream.Position = 0;
                
                return (T)binaryFormatter.Deserialize(mStream);
            }
        }

        private void Elements_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (isRestoring)
            {
                return;
            }

            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    foreach (T item in e.NewItems)
                    {
                        if (item != null && !isAddedInternaly)
                        {
                            changedElements[item] = new KeyValuePair<T, ChangeType>(default, ChangeType.Added);
                        }
                    }
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                    foreach (T item in e.OldItems)
                    {
                        if (changedElements.ContainsKey(item))
                        {
                            var elemInfo = changedElements[item];

                            if (elemInfo.Value != ChangeType.Added)
                            {
                                changedElements[item] = new KeyValuePair<T, ChangeType>(elemInfo.Key, ChangeType.Deleted);
                            }
                            else
                            {
                                changedElements.Remove(item);
                            }
                        }
                        else
                        {
                            changedElements[item] = new KeyValuePair<T, ChangeType>(item, ChangeType.Deleted);
                        }
                    }
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Replace:
                    OnPropertyChanged("CurrentElement");
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Reset:
                    CurrentElementIndex = -1;
                    break;
            }
        }

        private void Element_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (sender is T elem && !changedElements.ContainsKey(elem))
            {
                changedElements[elem] = new KeyValuePair<T, ChangeType>(originElement, ChangeType.Changed);
            }
        }

        private void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
