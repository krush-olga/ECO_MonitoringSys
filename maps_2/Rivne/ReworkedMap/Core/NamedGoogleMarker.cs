using System;
using GMap.NET;
using GMap.NET.WindowsForms.Markers;
using System.Drawing;
using System.Runtime.Serialization;
using Data;

namespace Maps.Core
{
    public class NamedGoogleMarker : GMarkerGoogle, IDescribable, IEquatable<NamedGoogleMarker>
    {
        private string name;
        private string description;
        private string format;
        private string type;
        private string creatorFullName;
        private Role role;

        public NamedGoogleMarker(PointLatLng p, GMarkerGoogleType type) 
            : this(p, type, string.Empty, null, null) { }
        public NamedGoogleMarker(PointLatLng p, GMarkerGoogleType type, string format) 
            : this(p, type, format, null, null) { }
        public NamedGoogleMarker(PointLatLng p, GMarkerGoogleType type, string format, string name) 
            : this(p, type, format, name, null) { }
        public NamedGoogleMarker(PointLatLng p, GMarkerGoogleType type, string format, string name, string description) 
            : base(p, type) 
        {
            InitializeMarker(format, name, description);
        }
        public NamedGoogleMarker(PointLatLng p, Bitmap Bitmap) 
            : this(p, Bitmap, string.Empty, null, null) { }
        public NamedGoogleMarker(PointLatLng p, Bitmap Bitmap, string format) 
            : this(p, Bitmap, format, null, null) { }
        public NamedGoogleMarker(PointLatLng p, Bitmap Bitmap, string format, string name) 
            : this(p, Bitmap, format, name, null) { }
        public NamedGoogleMarker(PointLatLng p, Bitmap Bitmap, string format, string name, string description) 
            : base(p, Bitmap) 
        {
            InitializeMarker(format, name, description);
        }
        protected NamedGoogleMarker(SerializationInfo info, StreamingContext context) 
            : base(info, context) { }

        public string Name
        {
            get 
            { 
                return name; 
            }
            set 
            { 
                name = value;
                ChangeMarkerTextInfo();
            }
        }
        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                description = value;
                ChangeMarkerTextInfo();
            }
        }
        public string Format
        {
            get
            {
                return format;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentException("Значение формата не может отсутствовать.");
                }

                format = value;
                ChangeMarkerTextInfo();
            }
        }
        string IDescribable.Type 
        { 
            get => type;
            set => type = value; 
        }
        public string CreatorFullName 
        {
            get => creatorFullName;
            set => creatorFullName = value;
        }
        public Role CreatorRole 
        {
            get => role;
            set => role = value;
        }

        public int Id { get; set; }
        public bool IsDependent { get; set; }

        public new string ToolTipText
        {
            get
            {
                return base.ToolTipText;
            }
        }

        public override bool Equals(object obj)
        {
            if (!(obj is NamedGoogleMarker))
            {
                return false;
            }

            if (ReferenceEquals(obj, this))
            {
                return true;
            }

            return this.Equals((NamedGoogleMarker)obj);
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode() ^ Name.GetHashCode() ^ ((IDescribable)this).Type.GetHashCode();
        }
        public override string ToString()
        {
            return Name; /*string.Format("{0} ({1})", Name, ((IDescribable)this).Type);*/
        }

        public virtual bool Equals(NamedGoogleMarker other)
        {
            if (other == null)
            {
                return false;
            }

            if (other.Id == this.Id && other.Name == this.Name &&
                ((IDescribable)other).Type == ((IDescribable)this).Type)
            {
                return true;
            }

            return false;
        }

        private void InitializeMarker(string format, string name, string description)
        {
            Format = format;
            Name = name;
            Description = description;
        }

        private void ChangeMarkerTextInfo()
        {
            base.ToolTipText = string.Format(format, name, description);
        }

    }
}
