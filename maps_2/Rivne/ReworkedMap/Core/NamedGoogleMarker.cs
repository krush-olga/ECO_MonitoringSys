using System;
using GMap.NET;
using GMap.NET.WindowsForms.Markers;
using System.Drawing;
using System.Runtime.Serialization;
using Data;

namespace UserMap.Core
{
    /// <include file='Docs/Core/NamedGoogleMarkerDoc.xml' path='docs/members[@name="named_google_marker"]/NamedGoogleMarker/*'/>
    public class NamedGoogleMarker : GMarkerGoogle, IDescribable, IEquatable<NamedGoogleMarker>
    {
        private string name;
        private string description;
        private string format;
        private string type;
        private string creatorFullName;
        private Role role;

        /// <include file='Docs/Core/NamedGoogleMarkerDoc.xml' path='docs/members[@name="named_google_marker"]/NamedGoogleMarkerCtor1/*'/>
        public NamedGoogleMarker(PointLatLng p, GMarkerGoogleType type) 
            : this(p, type, string.Empty, null, null) { }
        /// <include file='Docs/Core/NamedGoogleMarkerDoc.xml' path='docs/members[@name="named_google_marker"]/NamedGoogleMarkerCtor2/*'/>
        public NamedGoogleMarker(PointLatLng p, GMarkerGoogleType type, string format) 
            : this(p, type, format, null, null) { }
        /// <include file='Docs/Core/NamedGoogleMarkerDoc.xml' path='docs/members[@name="named_google_marker"]/NamedGoogleMarkerCtor3/*'/>
        public NamedGoogleMarker(PointLatLng p, GMarkerGoogleType type, string format, string name) 
            : this(p, type, format, name, null) { }
        /// <include file='Docs/Core/NamedGoogleMarkerDoc.xml' path='docs/members[@name="named_google_marker"]/NamedGoogleMarkerCtor4/*'/>
        public NamedGoogleMarker(PointLatLng p, GMarkerGoogleType type, string format, string name, string description) 
            : base(p, type) 
        {
            InitializeMarker(format, name, description);
        }
        /// <include file='Docs/Core/NamedGoogleMarkerDoc.xml' path='docs/members[@name="named_google_marker"]/NamedGoogleMarkerCtor5/*'/>
        public NamedGoogleMarker(PointLatLng p, Bitmap Bitmap) 
            : this(p, Bitmap, string.Empty, null, null) { }
        /// <include file='Docs/Core/NamedGoogleMarkerDoc.xml' path='docs/members[@name="named_google_marker"]/NamedGoogleMarkerCtor6/*'/>
        public NamedGoogleMarker(PointLatLng p, Bitmap Bitmap, string format) 
            : this(p, Bitmap, format, null, null) { }
        /// <include file='Docs/Core/NamedGoogleMarkerDoc.xml' path='docs/members[@name="named_google_marker"]/NamedGoogleMarkerCtor7/*'/>
        public NamedGoogleMarker(PointLatLng p, Bitmap Bitmap, string format, string name) 
            : this(p, Bitmap, format, name, null) { }
        /// <include file='Docs/Core/NamedGoogleMarkerDoc.xml' path='docs/members[@name="named_google_marker"]/NamedGoogleMarkerCtor8/*'/>
        public NamedGoogleMarker(PointLatLng p, Bitmap Bitmap, string format, string name, string description) 
            : base(p, Bitmap) 
        {
            InitializeMarker(format, name, description);
        }
        /// <include file='Docs/Core/NamedGoogleMarkerDoc.xml' path='docs/members[@name="named_google_marker"]/NamedGoogleMarkerCtor9/*'/>
        protected NamedGoogleMarker(SerializationInfo info, StreamingContext context) 
            : base(info, context) { }

        /// <inheritdoc/>
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
        /// <inheritdoc/>
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
        /// <include file='Docs/Core/NamedGoogleMarkerDoc.xml' path='docs/members[@name="named_google_marker"]/Format/*'/>
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
                    throw new ArgumentNullException("value");
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
        /// <inheritdoc/>
        public string CreatorFullName 
        {
            get => creatorFullName;
            set => creatorFullName = value;
        }
        /// <inheritdoc/>
        public Role CreatorRole 
        {
            get => role;
            set => role = value;
        }

        /// <include file='Docs/Core/NamedGoogleMarkerDoc.xml' path='docs/members[@name="named_google_marker"]/Id/*'/>
        public int Id { get; set; }
        /// <include file='Docs/Core/NamedGoogleMarkerDoc.xml' path='docs/members[@name="named_google_marker"]/IsDependent/*'/>
        public bool IsDependent { get; set; }

        /// <include file='Docs/Core/NamedGoogleMarkerDoc.xml' path='docs/members[@name="named_google_marker"]/ToolTipText/*'/>
        public new string ToolTipText
        {
            get
            {
                return base.ToolTipText;
            }
        }

        /// <inheritdoc/>
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
        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return Id.GetHashCode() ^ Name.GetHashCode() ^ ((IDescribable)this).Type.GetHashCode();
        }
        /// <inheritdoc/>
        public override string ToString()
        {
            return Name; /*string.Format("{0} ({1})", Name, ((IDescribable)this).Type);*/
        }

        /// <inheritdoc/>
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
