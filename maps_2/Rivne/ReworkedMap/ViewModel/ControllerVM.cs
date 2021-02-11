using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace UserMap.ViewModel
{
    internal enum ChangeType
    {
        Added = 0,
        Changed,
        Deleted
    }

    /* 
     * Dictionary<T, KeyValuePair<T, ChangeType>>:
     * T = новый элемент (если произошло удаление элемента, но его 
     * не было с самого начала, то ставим тот же, что и при удалении),
     * 
     * KeyValuePair<T, ChangeType>> - содержит пару, где: 
     *      T - старый элемент (если было добавлено элемент, то будет null),
     *      ChangeType = что произошло с этим элементом.
     */

    internal sealed class ControllerVM<T> : INotifyPropertyChanged
        where T : class, INotifyPropertyChanged, new()
    {
        private bool isAddedInternaly;
        private bool isAddingNewElem;
        private bool isRestoring;
        private int elementIndex;

        private T currentEditableElement;
        private T originElement;
        private List<object> recursionElement;
        private ObservableCollection<T> elements;
        private Dictionary<T, KeyValuePair<T, ChangeType>> changedElements;

        public ControllerVM()
        {
            elements = new ObservableCollection<T>();
            changedElements = new Dictionary<T, KeyValuePair<T, ChangeType>>();
            recursionElement = new List<object>();
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
        public void StartAddingNewElement()
        {
            elements.Add(new T());
            isAddingNewElem = true;
            CurrentElementIndex = elements.Count - 1;
        }
        public void CancelAddingNewElement()
        {
            if (isAddingNewElem)
            {
                elements.RemoveAt(elements.Count - 1);
                isAddingNewElem = false;
            }
        }
        public void EndAddingNewElement()
        {
            if (isAddingNewElem)
            {
                isAddingNewElem = false;
            }
        }

        public bool RemoveEmissionAt(int index)
        {
            if (index < 0 || index >= elements.Count)
                return false;

            var element = elements[index];

            return RemoveEmission(element);
        }
        public bool RemoveEmission(T element)
        {
            bool res = elements.Remove(element);

            if (elementIndex >= elements.Count)
            {
                CurrentElementIndex -= 1;
            }

            if (res)
            {
                if (changedElements.ContainsKey(element))
                {
                    var elemInfo = changedElements[element];

                    if (elemInfo.Value != ChangeType.Added)
                    {
                        changedElements[element] = new KeyValuePair<T, ChangeType>(elemInfo.Key, ChangeType.Deleted);
                    }
                    else
                    {
                        changedElements.Remove(element);
                    }
                }
                else
                {
                    changedElements[element] = new KeyValuePair<T, ChangeType>(element, ChangeType.Deleted);
                }
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

                var newCurrentElem = new T();
                elements[index] = newCurrentElem;
                currentEditableElement = newCurrentElem;

                CopyElement(ref originElement, ref newCurrentElem);
            }
        }
        public void CancelEditElement()
        {
            if (originElement != null)
            {
                elements[elementIndex] = originElement;
                currentEditableElement = null;
                originElement = null;
            }
        }
        public void EndEditElement()
        {
            if (originElement != null)
            {
                changedElements[currentEditableElement] = new KeyValuePair<T, ChangeType>(originElement, ChangeType.Changed);
                currentEditableElement = null;
                originElement = null;
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

        private void CopyElement<CType>(ref CType oldElement, ref CType newElement)
        {
            Type elemType = oldElement.GetType();
            System.Reflection.PropertyInfo[] propertyInfos = elemType.GetProperties();

            for (int i = 0; i < propertyInfos.Length; i++)
            {
                var propInfo = propertyInfos[i];
                var propType = propInfo.GetGetMethod().ReturnType;

                if (propType.IsClass && propType != typeof(string))
                {
                    var innerElem = propInfo.GetValue(oldElement);

                    if (recursionElement.Contains(innerElem))
                    {
                        propInfo.SetValue(newElement, innerElem);
                    }
                    else
                    {
                        recursionElement.Add(innerElem);

                        var newInnerElement = Activator.CreateInstance(innerElem.GetType());
                        CopyElement(ref innerElem, ref newInnerElement);

                        propInfo.SetValue(newElement, newInnerElement);

                        recursionElement.Remove(innerElem);
                    }
                }
                else if (propInfo.CanWrite && propInfo.CanRead)
                {
                    propInfo.SetValue(newElement, propInfo.GetValue(oldElement));
                }
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
                            changedElements[item] = new KeyValuePair<T, ChangeType>(null, ChangeType.Added);
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
            var elem = sender as T;

            if (!changedElements.ContainsKey(elem))
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
