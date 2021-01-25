using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Maps.Helpers;

namespace Maps.Core
{
    /// <include file='Docs/Core/MapDoc.xml' path='docs/members[@name="map"]/Map/*'/>
    public class Map : IDisposable
    {
        private static readonly string defaultOverlayName;
        private static readonly int defaultOpacity;

        private static readonly MarkersComparator markersComparator;
        private static readonly PolygonsComparator polygonsComparator;
        private static readonly RoutesComparator routesComparator;

        private readonly GMapOverlay defaultOverlay;

        private PolygonContext polygonContext;
        private RouteContext routeContext;

        static Map()
        {
            defaultOverlayName = "default";
            defaultOpacity = 30;

            markersComparator = new MarkersComparator();
            polygonsComparator = new PolygonsComparator();
            routesComparator = new RoutesComparator();
        }

        /// <include file='Docs/Core/MapDoc.xml' path='docs/members[@name="map"]/MapCtor1/*'/>
        public Map() : this(null)
        { }
        /// <include file='Docs/Core/MapDoc.xml' path='docs/members[@name="map"]/MapCtor2/*'/>
        public Map(GMapControl gMapControl)
        {
            if (gMapControl == null)
            {
                MapObject = new GMapControl();
                MapObject.Name = "MainMap";
            }
            else
            {
                MapObject = gMapControl;
            }

            polygonContext = null;

            defaultOverlay = new GMapOverlay(defaultOverlayName);

            MapObject.Overlays.Add(defaultOverlay);
        }

        /// <include file='Docs/Core/MapDoc.xml' path='docs/members[@name="map"]/MapObject/*'/>
        public GMapControl MapObject { get; private set; }

        /// <include file='Docs/Core/MapDoc.xml' path='docs/members[@name="map"]/SelectedMarker/*'/>
        public GMapMarker SelectedMarker { get; set; }
        /// <include file='Docs/Core/MapDoc.xml' path='docs/members[@name="map"]/SelectedPolygon/*'/>
        public GMapPolygon SelectedPolygon { get; set; }
        /// <include file='Docs/Core/MapDoc.xml' path='docs/members[@name="map"]/SelectedRoute/*'/>
        public GMapRoute SelectedRoute { get; set; }

        /// <include file='Docs/Core/MapDoc.xml' path='docs/members[@name="map"]/AddMarkerPointBitmap2arg/*'/>
        public GMapMarker AddMarker(Point screenPoint, Bitmap img)
        {
            return AddMarker(screenPoint,  img, defaultOverlayName, string.Empty, null, null);
        }
        /// <include file='Docs/Core/MapDoc.xml' path='docs/members[@name="map"]/AddMarkerPointBitmap3arg/*'/>
        public GMapMarker AddMarker(Point screenPoint, Bitmap img, string layoutId)
        {
            return AddMarker(screenPoint, img, layoutId, string.Empty, null, null);
        }
        /// <include file='Docs/Core/MapDoc.xml' path='docs/members[@name="map"]/AddMarkerPointBitmap5arg/*'/>
        public GMapMarker AddMarker(Point screenPoint, Bitmap img, string format, string name, string description)
        {
            return AddMarker(screenPoint, img, defaultOverlayName, format, name, description);
        }
        /// <include file='Docs/Core/MapDoc.xml' path='docs/members[@name="map"]/AddMarkerPointBitmap6arg/*'/>
        public GMapMarker AddMarker(Point screenPoint, Bitmap img, string layoutId, string format, string name, string description)
        {
            if (img == null)
            {
                throw new ArgumentNullException("img");
            }

            NamedGoogleMarker marker = new NamedGoogleMarker(MapObject.FromLocalToLatLng(screenPoint.X, screenPoint.Y), 
                                                             img, format, name, description);
            return AddMarker(marker, layoutId);
        }
        /// <include file='Docs/Core/MapDoc.xml' path='docs/members[@name="map"]/AddMarkerPointLatLngBitmap2arg/*'/>
        public GMapMarker AddMarker(PointLatLng coords, Bitmap img)
        {
            return AddMarker(coords, img, defaultOverlayName, string.Empty, null, null);
        }
        /// <include file='Docs/Core/MapDoc.xml' path='docs/members[@name="map"]/AddMarkerPointLatLngBitmap3arg/*'/>
        public GMapMarker AddMarker(PointLatLng coords, Bitmap img, string layoutId)
        {
            return AddMarker(coords, img, layoutId, string.Empty, null, null);
        }
        /// <include file='Docs/Core/MapDoc.xml' path='docs/members[@name="map"]/AddMarkerPointLatLngBitmap5arg/*'/>
        public GMapMarker AddMarker(PointLatLng coords, Bitmap img, string format, string name, string description)
        {
            return AddMarker(coords, img, defaultOverlayName, format, name, description);
        }
        /// <include file='Docs/Core/MapDoc.xml' path='docs/members[@name="map"]/AddMarkerPointLatLngBitmap6arg/*'/>
        public GMapMarker AddMarker(PointLatLng coords, Bitmap img, string layoutId, string format, string name, string description)
        {
            if (img == null)
            {
                throw new ArgumentNullException("img");
            }

            return AddMarker(new NamedGoogleMarker(coords, img, format, name, description), layoutId);
        }
        /// <include file='Docs/Core/MapDoc.xml' path='docs/members[@name="map"]/AddMarkerPointGMarkerGoogleType2arg/*'/>
        public GMapMarker AddMarker(Point screenPoint, GMarkerGoogleType markerType)
        {
            return AddMarker(screenPoint, markerType, defaultOverlayName, string.Empty, null, null);
        }
        /// <include file='Docs/Core/MapDoc.xml' path='docs/members[@name="map"]/AddMarkerPointGMarkerGoogleType3arg/*'/>
        public GMapMarker AddMarker(Point screenPoint, GMarkerGoogleType markerType, string layoutId)
        {
            return AddMarker(screenPoint, markerType, layoutId, string.Empty, null, null);
        }
        /// <include file='Docs/Core/MapDoc.xml' path='docs/members[@name="map"]/AddMarkerPointGMarkerGoogleType5arg/*'/>
        public GMapMarker AddMarker(Point screenPoint, GMarkerGoogleType markerType, string format, string name, string description)
        {
            return AddMarker(screenPoint, markerType, defaultOverlayName, format, name, description);
        }
        /// <include file='Docs/Core/MapDoc.xml' path='docs/members[@name="map"]/AddMarkerPointGMarkerGoogleType6arg/*'/>
        public GMapMarker AddMarker(Point screenPoint, GMarkerGoogleType markerType, string layoutId, string format, string name, string description)
        {
            NamedGoogleMarker marker = new NamedGoogleMarker(MapObject.FromLocalToLatLng(screenPoint.X, screenPoint.Y), 
                                                             markerType, format, name, description);
            return AddMarker(marker, layoutId);
        }
        /// <include file='Docs/Core/MapDoc.xml' path='docs/members[@name="map"]/AddMarkerPointLatLngGMarkerGoogleType2arg/*'/>
        public GMapMarker AddMarker(PointLatLng coords, GMarkerGoogleType markerType)
        {
            return AddMarker(coords, markerType, defaultOverlayName,string.Empty, null, null);
        }
        /// <include file='Docs/Core/MapDoc.xml' path='docs/members[@name="map"]/AddMarkerPointLatLngGMarkerGoogleType3arg/*'/>
        public GMapMarker AddMarker(PointLatLng coords, GMarkerGoogleType markerType, string layoutId)
        {
            return AddMarker(coords, markerType, layoutId, string.Empty, null, null);
        }
        /// <include file='Docs/Core/MapDoc.xml' path='docs/members[@name="map"]/AddMarkerPointLatLngGMarkerGoogleType5arg/*'/>
        public GMapMarker AddMarker(PointLatLng coords, GMarkerGoogleType markerType, string format, string name, string description)
        {
            return AddMarker(coords, markerType, defaultOverlayName, format, name, description);
        }
        /// <include file='Docs/Core/MapDoc.xml' path='docs/members[@name="map"]/AddMarkerPointLatLngGMarkerGoogleType6arg/*'/>
        public GMapMarker AddMarker(PointLatLng coords, GMarkerGoogleType markerType, string layoutId, string format, string name, string description)
        {
            return AddMarker(new NamedGoogleMarker(coords, markerType, format, name, description), layoutId);
        }
        /// <include file='Docs/Core/MapDoc.xml' path='docs/members[@name="map"]/AddMarkerGMapMarker1arg/*'/>
        public GMapMarker AddMarker(GMapMarker marker)
        {
            return AddMarker(marker, defaultOverlayName);
        }
        /// <include file='Docs/Core/MapDoc.xml' path='docs/members[@name="map"]/AddMarkerGMapMarker2arg/*'/>
        public GMapMarker AddMarker(GMapMarker marker, string layoutId)
        {
            if (marker == null)
            {
                throw new ArgumentNullException("marker");
            }

            if (string.IsNullOrEmpty(layoutId))
            {
                throw new ArgumentNullException("layoutId");
            }

            GMapOverlay overlay = GetOverlayByIdOrNull(layoutId);

            if (overlay != null)
            {
                if (!overlay.Markers.Contains(marker, markersComparator))
                {
                    overlay.Markers.Add(marker);
                }
                else
                {
                    SelectedMarker = GetMarkerByCoordsOrNull(marker.Position);

                    return SelectedMarker;
                }
            }
            else
            {
                overlay = new GMapOverlay(layoutId);
                overlay.Markers.Add(marker);

                MapObject.Overlays.Add(overlay);

                ZoomPlus();
                ZoomMinus();
            }

            SelectedMarker = marker;

            return marker;
        }

        /// <include file='Docs/Core/MapDoc.xml' path='docs/members[@name="map"]/AddPolygonPoint3arg/*'/>
        public GMapPolygon AddPolygon(IEnumerable<Point> points, Color fill, string polygonName)
        {
            return AddPolygon(points, fill, defaultOpacity, Color.Black, defaultOverlayName, polygonName);
        }
        /// <include file='Docs/Core/MapDoc.xml' path='docs/members[@name="map"]/AddPolygonPoint4arg/*'/>
        public GMapPolygon AddPolygon(IEnumerable<Point> points, Color fill, int opacity, string polygonName)
        {
            return AddPolygon(points, fill, opacity, Color.Black, defaultOverlayName, polygonName);
        }
        /// <include file='Docs/Core/MapDoc.xml' path='docs/members[@name="map"]/AddPolygonPoint5arg/*'/>
        public GMapPolygon AddPolygon(IEnumerable<Point> points, Color fill, int opacity, string polygonName, string layoutId)
        {
            return AddPolygon(points, fill, opacity, Color.Black, polygonName, layoutId);
        }
        /// <include file='Docs/Core/MapDoc.xml' path='docs/members[@name="map"]/AddPolygonPoint6arg/*'/>
        public GMapPolygon AddPolygon(IEnumerable<Point> points, Color fill, int opacity, Color stroke, string polygonName, string layoutId)
        {
            if (points == null)
            {
                throw new ArgumentNullException("points");
            }
            if (string.IsNullOrEmpty(polygonName))
            {
                throw new ArgumentException("Название полигона не может отсутствовать.");
            }

            List<PointLatLng> coords = points.Select(p => MapObject.FromLocalToLatLng(p.X, p.Y)).ToList();

            return AddPolygon(MapHelper.CreatePolygon(coords, fill, opacity, stroke, polygonName), layoutId);
        }
        /// <include file='Docs/Core/MapDoc.xml' path='docs/members[@name="map"]/AddPolygonPointLatLng3arg/*'/>
        public GMapPolygon AddPolygon(IEnumerable<PointLatLng> coords, Color fill, string polygonName)
        {
            return AddPolygon(coords, fill, defaultOpacity, Color.Red, defaultOverlayName, polygonName);
        }
        /// <include file='Docs/Core/MapDoc.xml' path='docs/members[@name="map"]/AddPolygonPointLatLng4arg/*'/>
        public GMapPolygon AddPolygon(IEnumerable<PointLatLng> coords, Color fill, int opacity, string polygonName)
        {
            return AddPolygon(coords, fill, opacity, Color.Red, defaultOverlayName, polygonName);
        }
        /// <include file='Docs/Core/MapDoc.xml' path='docs/members[@name="map"]/AddPolygonPointLatLng5arg/*'/>
        public GMapPolygon AddPolygon(IEnumerable<PointLatLng> coords, Color fill, int opacity, string polygonName, string layoutId)
        {
            return AddPolygon(coords, fill, opacity, Color.Red, polygonName, layoutId);
        }
        /// <include file='Docs/Core/MapDoc.xml' path='docs/members[@name="map"]/AddPolygonPointLatLng6arg/*'/>
        public GMapPolygon AddPolygon(IEnumerable<PointLatLng> coords, Color fill, int opacity, Color stroke, string polygonName, string layoutId)
        {
            if (coords == null)
            {
                throw new ArgumentNullException("coords");
            }
            if (string.IsNullOrEmpty(polygonName))
            {
                throw new ArgumentException("Название полигона не может отсутствовать.");
            }

            return AddPolygon(MapHelper.CreatePolygon(coords.ToList(), fill, opacity, stroke, polygonName), layoutId);
        }
        /// <include file='Docs/Core/MapDoc.xml' path='docs/members[@name="map"]/AddPolygonMarker3arg/*'/>
        public GMapPolygon AddPolygon(IEnumerable<GMapMarker> markers, Color fill, string polygonName)
        {
            return AddPolygon(markers, fill, defaultOpacity, Color.Black, defaultOverlayName, polygonName);
        }
        /// <include file='Docs/Core/MapDoc.xml' path='docs/members[@name="map"]/AddPolygonMarker4arg/*'/>
        public GMapPolygon AddPolygon(IEnumerable<GMapMarker> markers, Color fill, int opacity, string polygonName)
        {
            return AddPolygon(markers, fill, opacity, Color.Black, defaultOverlayName, polygonName);
        }
        /// <include file='Docs/Core/MapDoc.xml' path='docs/members[@name="map"]/AddPolygonMarker5arg/*'/>
        public GMapPolygon AddPolygon(IEnumerable<GMapMarker> markers, Color fill, int opacity, string polygonName, string layoutId)
        {
            return AddPolygon(markers, fill, opacity, Color.Black, polygonName, layoutId);
        }
        /// <include file='Docs/Core/MapDoc.xml' path='docs/members[@name="map"]/AddPolygonMarker6arg/*'/>
        public GMapPolygon AddPolygon(IEnumerable<GMapMarker> markers, Color fill, int opacity, Color stroke, string polygonName, string layoutId)
        {
            if (markers == null)
            {
                throw new ArgumentNullException("markers");
            }
            if (string.IsNullOrEmpty(polygonName))
            {
                throw new ArgumentException("Название полигона не может отсутствовать.");
            }

            GMapOverlay overlay = GetOverlayByIdOrNull(layoutId);

            List<PointLatLng> markersCoord = markers.Select(m =>
                                                    {
                                                        overlay.Markers.Add(m);
                                                        return m.Position;
                                                    })
                                                    .ToList();

            return AddPolygon(MapHelper.CreatePolygon(markersCoord, fill, opacity, stroke, polygonName), layoutId);
        }
        /// <include file='Docs/Core/MapDoc.xml' path='docs/members[@name="map"]/AddPolygonPolygon1arg/*'/>
        public GMapPolygon AddPolygon(GMapPolygon polygon)
        {
            return AddPolygon(polygon, defaultOverlayName);
        }
        /// <include file='Docs/Core/MapDoc.xml' path='docs/members[@name="map"]/AddPolygonPolygon2arg/*'/>
        public GMapPolygon AddPolygon(GMapPolygon polygon, string layoutId)
        {
            if (polygon == null)
            {
                throw new ArgumentNullException("polygon");
            }
            if (string.IsNullOrEmpty(layoutId))
            {
                throw new ArgumentNullException("layoutId");
            }

            GMapOverlay overlay = GetOverlayByIdOrNull(layoutId);

            if (overlay != null)
            {
                if (!overlay.Polygons.Contains(polygon, polygonsComparator))
                {
                    overlay.Polygons.Add(polygon);
                }
                else
                {
                    SelectedPolygon = GetPolygonByNameOrNull(polygon.Name);
                    return SelectedPolygon;
                }
            }
            else
            {
                overlay = new GMapOverlay(layoutId);
                overlay.Polygons.Add(polygon);

                MapObject.Overlays.Add(overlay);
            }

            SelectedPolygon = polygon;

            return polygon;
        }

        public GMapRoute AddRoute(IEnumerable<Point> points, Color stroke, string routeName)
        {
            return AddRoute(points, stroke, defaultOverlayName, routeName);
        }
        public GMapRoute AddRoute(IEnumerable<Point> points, Color stroke, string routeName, string layoutId)
        {
            List<PointLatLng> coords = points.Select(p => MapObject.FromLocalToLatLng(p.X, p.Y)).ToList();

            return AddRoute(MapHelper.CreateRoute(coords, stroke, routeName), layoutId);
        }
        public GMapRoute AddRoute(IEnumerable<PointLatLng> coords, Color stroke, string polygonName)
        {
            return AddRoute(coords, stroke, defaultOverlayName, polygonName);
        }
        public GMapRoute AddRoute(IEnumerable<PointLatLng> coords, Color stroke, string polygonName, string layoutId)
        {
            return AddRoute(MapHelper.CreateRoute(coords.ToList(), stroke, polygonName), layoutId);
        }
        public GMapRoute AddRoute(IEnumerable<GMapMarker> markers, Color stroke, string polygonName)
        {
            return AddRoute(markers, stroke, defaultOverlayName, polygonName);
        }
        public GMapRoute AddRoute(IEnumerable<GMapMarker> markers, Color stroke, string polygonName, string layoutId)
        {
            GMapOverlay overlay = GetOverlayByIdOrNull(layoutId);

            List<PointLatLng> markersCoord = markers.Select(m =>
                                                    {
                                                        overlay.Markers.Add(m);
                                                        return m.Position;
                                                    })
                                                    .ToList();

            return AddRoute(MapHelper.CreateRoute(markersCoord, stroke, polygonName), layoutId);
        }
        public GMapRoute AddRoute(GMapRoute route)
        {
            return AddRoute(route, defaultOverlayName);
        }
        public GMapRoute AddRoute(GMapRoute route, string layoutId)
        {
            if (route == null)
            {
                throw new ArgumentNullException("route");
            }

            if (string.IsNullOrEmpty(layoutId))
            {
                throw new ArgumentNullException("layoutId");
            }

            GMapOverlay overlay = GetOverlayByIdOrNull(layoutId);

            if (overlay != null)
            {
                if (!overlay.Routes.Contains(route, routesComparator))
                {
                    overlay.Routes.Add(route);
                }
                else
                {
                    SelectedRoute = GetRouteByNameOrNull(route.Name);
                    return SelectedRoute;
                }
            }
            else
            {
                overlay = new GMapOverlay(layoutId);
                overlay.Routes.Add(route);

                MapObject.Overlays.Add(overlay);
            }

            SelectedRoute = route;

            return route;
        }

        public void HideAllLayout() 
        {
            foreach (GMapOverlay overlay in MapObject.Overlays)
            {
                overlay.IsVisibile = false;
            }
        }
        public void ShowAllLayout()
        {
            foreach (GMapOverlay overlay in MapObject.Overlays)
            {
                overlay.IsVisibile = true;
            }
        }

        public void HideLayoutById(string layoutId)
        {
            GMapOverlay overlay = GetOverlayByIdOrNull(layoutId);

            if (overlay != null)
            {
                overlay.IsVisibile = false;
            }
        }
        public void ShowLayoutById(string layoutId)
        {
            GMapOverlay overlay = GetOverlayByIdOrNull(layoutId);

            if (overlay != null)
            {
                overlay.IsVisibile = true;
            }
        }

        public void HidePolygonByName(string polygonName)
        {
            GMapPolygon polygon = GetPolygonByNameOrNull(polygonName);

            if (polygon != null)
            {
                polygon.IsVisible = false;
            }
        }
        public void ShowPolygonById(string polygonName)
        {
            GMapPolygon polygon = GetPolygonByNameOrNull(polygonName);

            if (polygon != null)
            {
                polygon.IsVisible = true;
            }
        }

        public DrawContext StartDrawPolygon(Color fill, int opacity, string polygonName)
        {
            return StartDrawPolygon(fill, opacity, polygonName, defaultOverlayName, GMarkerGoogleType.arrow);
        }
        public DrawContext StartDrawPolygon(Color fill, int opacity, string polygonName, string layoutId)
        {
            return StartDrawPolygon(fill, opacity, polygonName, layoutId, GMarkerGoogleType.arrow);
        }
        public DrawContext StartDrawPolygon(Color fill, int opacity, string polygonName, 
                                            string layoutId, GMarkerGoogleType markerType)
        {
            if (polygonContext == null)
            {
                GMapOverlay overlay = new GMapOverlay($"__{layoutId}__");
                GMapPolygon existingPolygon = GetPolygonByNameOrNull(polygonName);

                if (existingPolygon != null)
                {
                    overlay.Polygons.Add(existingPolygon);
                }

                MapObject.Overlays.Add(overlay);

                polygonContext = new PolygonContext(overlay, fill, opacity, polygonName, markerType);
            }

            return polygonContext;
        }
        public void CancelPolygonDraw()
        {
            if (polygonContext == null)
            {
                return;
            }

            GMapOverlay overlay = polygonContext.Overlay;
            MapObject.Overlays.Remove(overlay);

            polygonContext.Dispose();
            polygonContext = null;
        }
        public void EndPolygonDraw()
        {
            if (polygonContext == null)
            {
                return;
            }

            GMapOverlay overlay = polygonContext.Overlay;

            MapObject.Overlays.Remove(overlay);

            overlay.Id = overlay.Id.Remove(0, 2);
            overlay.Id = overlay.Id.Remove(overlay.Id.Length - 2);

            GMapOverlay existingOverlay = GetOverlayByIdOrNull(overlay.Id);

            if (existingOverlay == null)
            {
                MapObject.Overlays.Add(overlay);
            }
            else
            {
                GMapPolygon drawedPolygon = overlay.Polygons.FirstOrDefault();

                if (drawedPolygon != null)
                {
                    existingOverlay.Polygons.Add(drawedPolygon);
                }
            }

            polygonContext.Dispose();
            polygonContext = null;
        }

        public DrawContext StartDrawRoute(Color stroke, string routeName)
        {
            return StartDrawRoute(stroke, routeName, defaultOverlayName, GMarkerGoogleType.arrow);
        }
        public DrawContext StartDrawRoute(Color stroke, string routeName, string layoutId)
        {
            return StartDrawRoute(stroke, routeName, layoutId, GMarkerGoogleType.arrow);
        }
        public DrawContext StartDrawRoute(Color stroke, string routeName, string layoutId, GMarkerGoogleType markerType)
        {
            if (routeContext == null)
            {
                GMapOverlay overlay = new GMapOverlay($"__{layoutId}__");
                GMapRoute existingRoute = GetRouteByNameOrNull(routeName);

                if (existingRoute != null)
                {
                    overlay.Routes.Add(existingRoute);
                }

                MapObject.Overlays.Add(overlay);

                routeContext = new RouteContext(overlay, routeName, stroke, markerType);
            }

            return routeContext;
        }
        public void CancelRouteDraw()
        {
            if (routeContext == null)
            {
                return;
            }

            GMapOverlay overlay = routeContext.Overlay;
            MapObject.Overlays.Remove(overlay);

            routeContext.Dispose();
            routeContext = null;
        }
        public void EndRouteDraw()
        {
            if (routeContext == null)
            {
                return;
            }

            GMapOverlay overlay = routeContext.Overlay;

            MapObject.Overlays.Remove(overlay);

            overlay.Id = overlay.Id.Remove(0, 2);
            overlay.Id = overlay.Id.Remove(overlay.Id.Length - 2);

            GMapOverlay existingOverlay = GetOverlayByIdOrNull(overlay.Id);

            if (existingOverlay == null)
            {
                MapObject.Overlays.Add(overlay);
            }
            else
            {
                GMapRoute drawedRoute = overlay.Routes.FirstOrDefault();

                if (drawedRoute != null)
                {
                    existingOverlay.Routes.Add(drawedRoute);
                }
            }

            routeContext.Dispose();
            routeContext = null;
        }

        public void RemoveMarker(Point screenPoint)
        {
            PointLatLng coords = MapObject.FromLocalToLatLng(screenPoint.X, screenPoint.Y);

            RemoveMarker(GetMarkerByCoordsOrNull(coords));
        }
        public void RemoveMarker(PointLatLng coords)
        {
            RemoveMarker(GetMarkerByCoordsOrNull(coords));
        }
        public void RemoveMarker(GMapMarker marker)
        {
            if (object.ReferenceEquals(marker, SelectedMarker))
            {
                SelectedMarker = null;
            }

            foreach (var overlay in MapObject.Overlays)
            {
                overlay.Markers.Remove(marker);
            }
        }
        
        public void RemovePolygon(string polygonName)
        {
            foreach (var overlay in MapObject.Overlays)
            {
                GMapPolygon _polygon = null;
                foreach (var polygon in overlay.Polygons)
                {
                    if (polygon.Name == polygonName)
                    {
                        _polygon = polygon;
                        break;
                    }
                }

                overlay.Polygons.Remove(_polygon);

                if (object.ReferenceEquals(_polygon, SelectedPolygon))
                {
                    SelectedPolygon = null;
                }
            }
        }
        public void RemovePolygon(GMapPolygon polygon)
        {
            if (object.ReferenceEquals(polygon, SelectedPolygon))
            {
                SelectedPolygon = null;
            }

            foreach (var overlay in MapObject.Overlays)
            {
                overlay.Polygons.Remove(polygon);
            }

            polygon.Dispose();
        }

        public void RemoveRoute(string routeName)
        {
            foreach (var overlay in MapObject.Overlays)
            {
                GMapRoute _route = null;
                foreach (var route in overlay.Routes)
                {
                    if (route.Name == routeName)
                    {
                        _route = route;
                        break;
                    }
                }

                overlay.Routes.Remove(_route);

                if (object.ReferenceEquals(_route, SelectedRoute))
                {
                    SelectedRoute = null;
                }
            }
        }
        public void RemoveRoute(GMapRoute route)
        {
            if (object.ReferenceEquals(route, SelectedRoute))
            {
                SelectedRoute = null;
            }

            foreach (var overlay in MapObject.Overlays)
            {
                overlay.Routes.Remove(route);
            }

            route.Dispose();
        }

        public void RemoveMarkerFromLayout(Point screenPoint, string layoutId = "default")
        {
            PointLatLng coords = MapObject.FromLocalToLatLng(screenPoint.X, screenPoint.Y);

            RemoveMarkerFromLayout(GetMarkerByCoordsOrNull(coords), layoutId);
        }
        public void RemoveMarkerFromLayout(PointLatLng coords, string layoutId = "default")
        {
            RemoveMarkerFromLayout(GetMarkerByCoordsOrNull(coords), layoutId);
        }
        public void RemoveMarkerFromLayout(GMapMarker marker, string layoutId = "default")
        {
            if (object.ReferenceEquals(marker, SelectedMarker))
            {
                SelectedMarker = null;
            }

            GMapOverlay overlay = GetOverlayByIdOrNull(layoutId);

            if (overlay != null)
            {
                overlay.Markers.Remove(marker);
            }
        }

        public void RemovePolygonFromLayout(string polygonName, string layoutId = "default")
        {
            RemovePolygonFromLayout(GetPolygonByNameOrNull(polygonName), layoutId);
        }
        public void RemovePolygonFromLayout(GMapPolygon polygon, string layoutId = "default")
        {
            if (object.ReferenceEquals(polygon, SelectedPolygon))
            {
                SelectedPolygon = null;
            }

            GMapOverlay overlay = GetOverlayByIdOrNull(layoutId);

            if (overlay != null)
            {
                int polygonIndex = overlay.Polygons.IndexOf(polygon);
                if (polygonIndex != -1)
                {

                    overlay.Polygons[polygonIndex].Dispose();
                    overlay.Polygons.RemoveAt(polygonIndex);
                }
            }
        }

        public void RemoveRouteFromLayout(string routeName, string layoutId = "default")
        {
            RemoveRouteFromLayout(GetRouteByNameOrNull(routeName), layoutId);
        }
        public void RemoveRouteFromLayout(GMapRoute route, string layoutId = "default")
        {
            if (object.ReferenceEquals(route, SelectedPolygon))
            {
                SelectedRoute = null;
            }

            GMapOverlay overlay = GetOverlayByIdOrNull(layoutId);

            if (overlay != null)
            {
                int routeIndex = overlay.Routes.IndexOf(route);
                if (routeIndex != -1)
                {

                    overlay.Routes[routeIndex].Dispose();
                    overlay.Routes.RemoveAt(routeIndex);
                }
            }
        }

        public ICollection<GMapMarker> GetMarkersByLayoutOrNull(string layoutId = "default")
        {
            GMapOverlay overlay = GetOverlayByIdOrNull(layoutId);

            return overlay != null ? overlay.Markers : null;
        }
        public ICollection<GMapPolygon> GetPolygonsByLayoutOrNull(string layoutId = "default")
        {
            GMapOverlay overlay = GetOverlayByIdOrNull(layoutId);

            return overlay != null ? overlay.Polygons : null;
        }
        public ICollection<GMapRoute> GetRoutesByLayoutOrNull(string layoutId = "default")
        {
            GMapOverlay overlay = GetOverlayByIdOrNull(layoutId);

            return overlay != null ? overlay.Routes : null;
        }

        public GMapMarker GetMarkerByCoordsOrNull(Point screenPoint)
        {
            return GetMarkerByCoordsOrNull(MapObject.FromLocalToLatLng(screenPoint.X, screenPoint.Y));
        }
        public GMapMarker GetMarkerByCoordsOrNull(PointLatLng coords)
        {
            return GetMarkerByCoordsInLayoutOrNull(coords, null);
        }
        public GMapMarker GetMarkerByCoordsInLayoutOrNull(Point screenPoint, string layoutId)
        {
            return GetMarkerByCoordsInLayoutOrNull(MapObject.FromLocalToLatLng(screenPoint.X, screenPoint.Y), layoutId);
        }
        public GMapMarker GetMarkerByCoordsInLayoutOrNull(PointLatLng coords, string layoutId)
        {
            double offsetLat = 0.004;
            double offsetLng = 0.002;

            if (MapObject.Zoom >= 0 && MapObject.Zoom <= 3)
            {
                offsetLat = 2.5;
                offsetLng = 2.4;
            }
            else if (MapObject.Zoom > 3 && MapObject.Zoom <= 5)
            {
                offsetLat = 0.9;
                offsetLng = 0.8;
            }
            else if (MapObject.Zoom > 5 && MapObject.Zoom <= 8)
            {
                offsetLat = 0.35;
                offsetLng = 0.2;
            }
            else if (MapObject.Zoom > 8 && MapObject.Zoom <= 11)
            {
                offsetLat = 0.05;
                offsetLng = 0.04;
            }
            else if (MapObject.Zoom > 11 && MapObject.Zoom <= 14)
            {
                offsetLat = 0.008;
                offsetLng = 0.005;
            }

            foreach (var overlay in MapObject.Overlays)
            {
                if (layoutId != null && overlay.Id != layoutId)
                {
                    continue;
                }

                foreach (var marker in overlay.Markers)
                {
                    if (coords.Lat > marker.Position.Lat - offsetLat && coords.Lat < marker.Position.Lat + offsetLat &&
                        coords.Lng > marker.Position.Lng - offsetLng && coords.Lng < marker.Position.Lng + offsetLng)
                    {
                        return marker;
                    }
                }
            }

            return null;
        }
        public GMapPolygon GetPolygonByNameOrNull(string polygonName)
        {
            foreach (var overlay in MapObject.Overlays)
            {
                foreach (var polygon in overlay.Polygons)
                {
                    if (polygon.Name == polygonName)
                    {
                        return polygon;
                    }
                }
            }

            return null;
        }
        public GMapRoute GetRouteByNameOrNull(string routeName)
        {
            foreach (var overlay in MapObject.Overlays)
            {
                foreach (var route in overlay.Routes)
                {
                    if (route.Name == routeName)
                    {
                        return route;
                    }
                }
            }

            return null;
        }

        public bool LayoutExist(string layoutId)
        {
            foreach (var overlay in MapObject.Overlays)
            {
                if (overlay.Id == layoutId)
                {
                    return true;
                }
            }

            return false;
        }

        public void ClearMap()
        {
            EndPolygonDraw();
            EndRouteDraw();

            MapHelper.DisposeElements(defaultOverlay.Polygons);
            MapHelper.DisposeElements(defaultOverlay.Routes);

            foreach (GMapOverlay overlay in MapObject.Overlays)
            {
                overlay.Clear();
            }

            MapObject.Overlays.Clear();

            MapObject.Overlays.Add(defaultOverlay);
        }
        public void ClearLayout(string layoutId)
        {
            GMapOverlay overlay = GetOverlayByIdOrNull(layoutId);

            if (overlay != null)
            {
                MapHelper.DisposeElements(overlay.Markers);
                MapHelper.DisposeElements(overlay.Polygons);
                MapHelper.DisposeElements(overlay.Routes);

                overlay.Clear();
            }
        }
        public void ClearAllMarkers()
        {
            var demendentMarkers = (from overlay in MapObject.Overlays
                                    from marker in overlay.Markers
                                    where ((NamedGoogleMarker)marker).IsDependent
                                    select new { Marker = marker, OverlayId = overlay.Id })
                                    .ToArray();

            foreach (var overlay in MapObject.Overlays)
            {
                overlay.Markers.Clear();
            }

            foreach (var dependetMarker in demendentMarkers)
            {
                AddMarker(dependetMarker.Marker, dependetMarker.OverlayId);
            }
        }
        public void ClearAllPolygons()
        {
            foreach (var overlay in MapObject.Overlays)
            {
                overlay.Polygons.Clear();
            }
        }
        public void ClearAllRoutes()
        {
            foreach (var overlay in MapObject.Overlays)
            {
                overlay.Routes.Clear();
            }
        }

        public void AddLayout(string layoutId)
        {
            GMapOverlay overlay = new GMapOverlay(layoutId);
            MapObject.Overlays.Add(overlay);
        }
        public void RemoveLayout(string layoutId)
        {
            GMapOverlay overlay = GetOverlayByIdOrNull(layoutId);

            if (overlay == null)
            {
                return;
            }

            MapObject.Overlays.Remove(overlay);

            MapHelper.DisposeElements(overlay.Polygons);
            MapHelper.DisposeElements(overlay.Routes);

            overlay.Clear();
            overlay = null;

            SelectedMarker = null;
            SelectedPolygon = null;
            SelectedRoute = null;
        }

        public void ZoomPlus()
        {
            MapObject.Zoom++;
        }
        public void ZoomMinus()
        {
            MapObject.Zoom--;
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            MapObject.Dispose();
        }

        private GMapOverlay GetOverlayByIdOrNull(string overlayId)
        {
            foreach (var overlay in MapObject.Overlays)
            {
                if (overlay.Id.Equals(overlayId, StringComparison.Ordinal))
                {
                    return overlay;
                }
            }

            return null;
        }
    }
    
    public class MarkersComparator : IEqualityComparer<GMapMarker>
    {
        /// <inheritdoc/>
        public bool Equals(GMapMarker x, GMapMarker y)
        {
            if (x == null || y == null)
            {
                return false;
            }

            if (ReferenceEquals(x, y))
            {
                return true;
            }

            return x.Position.Lat > y.Position.Lat - 0.0008 && x.Position.Lat < y.Position.Lat + 0.0008 &&
                   x.Position.Lng > y.Position.Lng - 0.0008 && x.Position.Lng < y.Position.Lng + 0.0008;
        }

        /// <inheritdoc/>
        public int GetHashCode(GMapMarker obj)
        {
            return obj.GetHashCode();
        }
    }
    public class PolygonsComparator : IEqualityComparer<GMapPolygon>
    {
        /// <inheritdoc/>
        public bool Equals(GMapPolygon x, GMapPolygon y)
        {
            if (x == null || y == null)
            {
                return false;
            }

            if (ReferenceEquals(x, y))
            {
                return true;
            }

            return x.Name == y.Name;
        }

        /// <inheritdoc/>
        public int GetHashCode(GMapPolygon obj)
        {
            return obj.GetHashCode();
        }
    }
    public class RoutesComparator : IEqualityComparer<GMapRoute>
    {
        /// <inheritdoc/>
        public bool Equals(GMapRoute x, GMapRoute y)
        {
            if (x == null || y == null)
            {
                return false;
            }

            if (ReferenceEquals(x, y))
            {
                return true;
            }

            return x.Name == y.Name;
        }

        /// <inheritdoc/>
        public int GetHashCode(GMapRoute obj)
        {
            return obj.GetHashCode();
        }
    }
}
