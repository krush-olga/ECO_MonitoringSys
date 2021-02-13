using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using UserMap.Core;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace UserMap.Helpers
{
    /// <include file='Docs/Helpers/MapHelperDoc.xml' path='docs/members[@name="map_helper"]/MapHelper/*'/>
    internal static class MapHelper
    {
        private static readonly SolidBrush solidColorBlack;
        private static readonly SolidBrush solidColorWhite;

        static MapHelper()
        {
            solidColorBlack = new SolidBrush(Color.Black);
            solidColorWhite = new SolidBrush(Color.White);
        }

        /// <include file='Docs/Helpers/MapHelperDoc.xml' path='docs/members[@name="map_helper"]/CreateMarkerImg/*'/>
        public static NamedGoogleMarker CreateMarker(PointLatLng coord, Bitmap img, string format, string name, string description)
        {
            return new NamedGoogleMarker(coord, img, format, name, description);
        }
        /// <include file='Docs/Helpers/MapHelperDoc.xml' path='docs/members[@name="map_helper"]/CreateMarkerGMarkerGoogleType/*'/>
        public static NamedGoogleMarker CreateMarker(PointLatLng coord, GMarkerGoogleType markerType, string format, string name, string description)
        {
            return new NamedGoogleMarker(coord, markerType, format, name, description);
        }

        /// <include file='Docs/Helpers/MapHelperDoc.xml' path='docs/members[@name="map_helper"]/CreatePolygon4arg/*'/>
        public static GMapPolygon CreatePolygon(List<PointLatLng> coords, Color fill,
                                                int opacity, string polygonName)
        {
            GMapPolygon polygon = new GMapPolygon(coords, polygonName)
            {
                Fill = new SolidBrush(Color.FromArgb(opacity, fill)),
                Stroke = new Pen(solidColorBlack)
            };

            return polygon;
        }
        /// <include file='Docs/Helpers/MapHelperDoc.xml' path='docs/members[@name="map_helper"]/CreatePolygon5arg/*'/>
        public static GMapPolygon CreatePolygon(List<PointLatLng> coords, Color fill,
                                                int opacity, Color stroke, string polygonName)
        {
            GMapPolygon polygon = new GMapPolygon(coords, polygonName)
            {
                Fill = new SolidBrush(Color.FromArgb(opacity, fill)),
                Stroke = new Pen(new SolidBrush(stroke))
            };

            return polygon;
        }

        /// <include file='Docs/Helpers/MapHelperDoc.xml' path='docs/members[@name="map_helper"]/CreateRoute2arg/*'/>
        public static GMapRoute CreateRoute(List<PointLatLng> coords, string routeName)
        {
            GMapRoute route = new GMapRoute(coords, routeName)
            {
                Stroke = new Pen(solidColorBlack)
            };

            return route;
        }
        /// <include file='Docs/Helpers/MapHelperDoc.xml' path='docs/members[@name="map_helper"]/CreateRoute3arg/*'/>
        public static GMapRoute CreateRoute(List<PointLatLng> coords, Color stroke, string routeName)
        {
            GMapRoute route = new GMapRoute(coords, routeName)
            {
                Stroke = new Pen(new SolidBrush(stroke))
            };

            return route;
        }

        /// <include file='Docs/Helpers/MapHelperDoc.xml' path='docs/members[@name="map_helper"]/DisposeElements/*'/>
        public static void DisposeElements(IEnumerable<IDisposable> disposables)
        {
            foreach (IDisposable disposable in disposables)
            {
                disposable.Dispose();
            }
        }
    }
}
