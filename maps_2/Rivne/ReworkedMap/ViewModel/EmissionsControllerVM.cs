using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maps.ViewModel
{
    internal sealed class EmissionsControllerVM : INotifyPropertyChanged
    {
        private int emissionIndex;
        private List<Data.Entity.Emission> emissions;

        public EmissionsControllerVM()
        {
            emissions = new List<Data.Entity.Emission>();
            emissionIndex = -1;
        }

        public List<Data.Entity.Emission> Emissions { get { return emissions; } }
        public Data.Entity.Emission CurrentEmission
        {
            get
            {
                if (emissionIndex == -1)
                {
                    return null;
                }

                return emissions[emissionIndex];
            }
        }
        public int CurrentEmissionIndex
        {
            get { return emissionIndex; }
            set
            {
                if (emissionIndex < -1 && emissionIndex >= emissions.Count)
                {
                    throw new ArgumentOutOfRangeException("value");
                }

                emissionIndex = value;
                OnPropertyChanged();
                OnPropertyChanged("CurrentEmission");
            }
        }

        public void Clear()
        {
            emissions.Clear();
        }
        public void AddEmission(Data.Entity.Emission emission)
        {
            if (emission == null)
            {
                throw new ArgumentNullException("emission");
            }

            emissions.Add(emission);

            if (emissionIndex == -1)
            {
                CurrentEmissionIndex = 0;
            }
        }
        public bool RemoveEmission(Data.Entity.Emission emission)
        {
            bool res = emissions.Remove(emission);

            if (emissionIndex >= emissions.Count)
            {
                CurrentEmissionIndex -= 1;
            }

            return res;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName]string propName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
