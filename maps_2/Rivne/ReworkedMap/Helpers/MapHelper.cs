using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using Maps.Core;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace Maps.Helpers
{
    internal static class MapHelper
    {
        private static readonly SolidBrush solidColorBlack;
        private static readonly SolidBrush solidColorWhite;

        static MapHelper()
        {
            solidColorBlack = new SolidBrush(Color.Black);
            solidColorWhite = new SolidBrush(Color.White);
        }

        public static GMapMarker CreateMarker(PointLatLng coords, Bitmap img, string format, string name, string description)
        {
            GMapMarker marker = new NamedGoogleMarker(coords, img, format, name, description);

            return marker;
        }
        public static GMapMarker CreateMarker(PointLatLng coords, GMarkerGoogleType markerType, string format, string name, string description)
        {
            GMapMarker marker = new NamedGoogleMarker(coords, markerType, format, name, description);

            return marker;
        }
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
        public static GMapRoute CreateRoute(List<PointLatLng> coords, string routeName)
        {
            GMapRoute route = new GMapRoute(coords, routeName)
            {
                Stroke = new Pen(solidColorBlack)
            };

            return route;
        }
        public static GMapRoute CreateRoute(List<PointLatLng> coords, Color stroke, string routeName)
        {
            GMapRoute route = new GMapRoute(coords, routeName)
            {
                Stroke = new Pen(new SolidBrush(stroke))
            };

            return route;
        }

        public static void DisposeElements(IEnumerable<IDisposable> disposables)
        {
            foreach (IDisposable disposable in disposables)
            {
                disposable.Dispose();
            }
        }
    }
}
