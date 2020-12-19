using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using GMap.NET.MapProviders;


namespace Maps
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

        public static GMapMarker CreateMarker(PointLatLng coords, Bitmap img, string description)
        {
            GMapMarker marker = new GMarkerGoogle(coords, img);

            if (description != null)
            {
                GMapToolTip toolTip = new GMapToolTip(marker)
                {
                    Fill = solidColorBlack,
                    Foreground = solidColorWhite,
                };

                marker.ToolTip = toolTip;
                marker.ToolTipText = description;
            }

            return marker;
        }
        public static GMapMarker CreateMarker(PointLatLng coords, GMarkerGoogleType markerType, string description)
        {
            GMapMarker marker = new GMarkerGoogle(coords, markerType);

            if (description != null)
            {
                GMapToolTip toolTip = new GMapToolTip(marker)
                {
                    Fill = solidColorBlack,
                    Foreground = solidColorWhite,
                };

                marker.ToolTip = toolTip;
                marker.ToolTipText = description;
            }

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
                                                int opacity, Pen pen, string polygonName)
        {
            GMapPolygon polygon = new GMapPolygon(coords, polygonName)
            {
                Fill = new SolidBrush(Color.FromArgb(opacity, fill)),
                Stroke = pen
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
        public static GMapRoute CreateRoute(List<PointLatLng> coords, Pen pen, string routeName)
        {
            GMapRoute route = new GMapRoute(coords, routeName)
            {
                Stroke = pen
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

    public class ReworkedMap : IDisposable
    {
        private static readonly string defaultOverlayName;
        private static readonly int defaultOpacity;

        private static readonly MarkersComparator markersComparator;
        private static readonly PolygonsComparator polygonsComparator;
        private static readonly RoutesComparator routesComparator;

        private readonly GMapOverlay defaultOverlay;

        private PolygonContext polygonContext;
        private RouteContext routeContext;

        static ReworkedMap()
        {
            defaultOverlayName = "default";
            defaultOpacity = 30;

            markersComparator = new MarkersComparator();
            polygonsComparator = new PolygonsComparator();
            routesComparator = new RoutesComparator();
        }

        public ReworkedMap() : this(null)
        { }
        public ReworkedMap(GMapControl gMapControl)
        {
            if (gMapControl == null)
            {
                Map = new GMapControl();
                Map.Name = "MainMap";
            }
            else
            {
                Map = gMapControl;
            }

            polygonContext = null;

            defaultOverlay = new GMapOverlay(defaultOverlayName);

            Map.Overlays.Add(defaultOverlay);
        }

        public GMapControl Map { get; private set; }

        public GMapMarker SelectedMarker { get; set; }
        public GMapPolygon SelectedPolygon { get; set; }
        public GMapRoute SelectedRoute { get; set; }

        public GMapMarker AddMarker(Point screenPoint, Bitmap img)
        {
            if (img == null)
            {
                throw new ArgumentNullException("marker");
            }

            return AddMarker(MapHelper.CreateMarker(Map.FromLocalToLatLng(screenPoint.X, screenPoint.Y), img, null));
        }
        public GMapMarker AddMarker(Point screenPoint, Bitmap img, string description)
        {
            if (img == null)
            {
                throw new ArgumentNullException("marker");
            }

            return AddMarker(MapHelper.CreateMarker(Map.FromLocalToLatLng(screenPoint.X, screenPoint.Y), img, description));
        }
        public GMapMarker AddMarker(Point screenPoint, Bitmap img, string description, string layoutId)
        {
            if (img == null)
            {
                throw new ArgumentNullException("marker");
            }

            return AddMarker(MapHelper.CreateMarker(Map.FromLocalToLatLng(screenPoint.X, screenPoint.Y), img, description), layoutId);
        }
        public GMapMarker AddMarker(PointLatLng coords, Bitmap img)
        {
            if (img == null)
            {
                throw new ArgumentNullException("marker");
            }

            return AddMarker(MapHelper.CreateMarker(coords, img, null));
        }
        public GMapMarker AddMarker(PointLatLng coords, Bitmap img, string description)
        {
            if (img == null)
            {
                throw new ArgumentNullException("marker");
            }

            return AddMarker(MapHelper.CreateMarker(coords, img, description));
        }
        public GMapMarker AddMarker(PointLatLng coords, Bitmap img, string description, string layoutId)
        {
            if (img == null)
            {
                throw new ArgumentNullException("marker");
            }

            return AddMarker(MapHelper.CreateMarker(coords, img, description), layoutId);
        }
        public GMapMarker AddMarker(Point screenPoint, GMarkerGoogleType markerType)
        {

            return AddMarker(MapHelper.CreateMarker(Map.FromLocalToLatLng(screenPoint.X, screenPoint.Y), markerType, null));
        }
        public GMapMarker AddMarker(Point screenPoint, GMarkerGoogleType markerType, string description)
        {

            return AddMarker(MapHelper.CreateMarker(Map.FromLocalToLatLng(screenPoint.X, screenPoint.Y), markerType, description));
        }
        public GMapMarker AddMarker(Point screenPoint, GMarkerGoogleType markerType, string description, string layoutId)
        {

            return AddMarker(MapHelper.CreateMarker(Map.FromLocalToLatLng(screenPoint.X, screenPoint.Y), markerType, description), layoutId);
        }
        public GMapMarker AddMarker(PointLatLng coords, GMarkerGoogleType markerType)
        {

            return AddMarker(MapHelper.CreateMarker(coords, markerType, null));
        }
        public GMapMarker AddMarker(PointLatLng coords, GMarkerGoogleType markerType, string description)
        {

            return AddMarker(MapHelper.CreateMarker(coords, markerType, description));
        }
        public GMapMarker AddMarker(PointLatLng coords, GMarkerGoogleType markerType, string description, string layoutId)
        {

            return AddMarker(MapHelper.CreateMarker(coords, markerType, description), layoutId);
        }
        public GMapMarker AddMarker(GMapMarker marker)
        {
            return AddMarker(marker, defaultOverlayName);
        }
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

                Map.Overlays.Add(overlay);

                ZoomPlus();
                ZoomMinus();
            }

            SelectedMarker = marker;

            return marker;
        }

        public GMapPolygon AddPolygon(IEnumerable<Point> points, Color fill, string polygonName)
        {
            return AddPolygon(points, fill, defaultOverlayName, defaultOpacity, polygonName);
        }
        public GMapPolygon AddPolygon(IEnumerable<Point> points, Color fill, string polygonName, int opacity)
        {
            return AddPolygon(points, fill, defaultOverlayName, opacity, polygonName);
        }
        public GMapPolygon AddPolygon(IEnumerable<Point> points, Color fill, string polygonName, int opacity, string layoutId)
        {
            List<PointLatLng> coords = points.Select(p => Map.FromLocalToLatLng(p.X, p.Y)).ToList();

            return AddPolygon(MapHelper.CreatePolygon(coords, fill, opacity, polygonName), layoutId);
        }
        public GMapPolygon AddPolygon(IEnumerable<PointLatLng> coords, Color fill, string polygonName)
        {
            return AddPolygon(coords, fill, defaultOverlayName, defaultOpacity, polygonName);
        }
        public GMapPolygon AddPolygon(IEnumerable<PointLatLng> coords, Color fill, string polygonName, int opacity)
        {
            return AddPolygon(coords, fill, defaultOverlayName, opacity, polygonName);
        }
        public GMapPolygon AddPolygon(IEnumerable<PointLatLng> coords, Color fill, string polygonName, int opacity, string layoutId)
        {
            return AddPolygon(MapHelper.CreatePolygon(coords.ToList(), fill, opacity, polygonName), layoutId);
        }
        public GMapPolygon AddPolygon(IEnumerable<GMapMarker> markers, Color fill, string polygonName)
        {
            return AddPolygon(markers, fill, defaultOverlayName, defaultOpacity, polygonName);
        }
        public GMapPolygon AddPolygon(IEnumerable<GMapMarker> markers, Color fill, string polygonName, int opacity)
        {
            return AddPolygon(markers, fill, defaultOverlayName, opacity, polygonName);
        }
        public GMapPolygon AddPolygon(IEnumerable<GMapMarker> markers, Color fill, string polygonName, int opacity, string layoutId)
        {
            GMapOverlay overlay = GetOverlayByIdOrNull(layoutId);

            List<PointLatLng> markersCoord = markers.Select(m =>
                                                            {
                                                                overlay.Markers.Add(m);
                                                                return m.Position;
                                                            })
                                                    .ToList();

            return AddPolygon(MapHelper.CreatePolygon(markersCoord, fill, opacity, polygonName), layoutId);
        }
        public GMapPolygon AddPolygon(GMapPolygon polygon)
        {
            return AddPolygon(polygon, defaultOverlayName);
        }
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

                Map.Overlays.Add(overlay);
            }

            SelectedPolygon = polygon;

            return polygon;
        }

        public GMapRoute AddRoute(IEnumerable<Point> points, Pen stroke, string routeName)
        {
            return AddRoute(points, stroke, defaultOverlayName, routeName);
        }
        public GMapRoute AddRoute(IEnumerable<Point> points, Pen stroke, string routeName, string layoutId)
        {
            List<PointLatLng> coords = points.Select(p => Map.FromLocalToLatLng(p.X, p.Y)).ToList();

            return AddRoute(MapHelper.CreateRoute(coords, stroke, routeName), layoutId);
        }
        public GMapRoute AddRoute(IEnumerable<PointLatLng> coords, Pen stroke, string polygonName)
        {
            return AddRoute(coords, stroke, defaultOverlayName, polygonName);
        }
        public GMapRoute AddRoute(IEnumerable<PointLatLng> coords, Pen stroke, string polygonName, string layoutId)
        {
            return AddRoute(MapHelper.CreateRoute(coords.ToList(), stroke, polygonName), layoutId);
        }
        public GMapRoute AddRoute(IEnumerable<GMapMarker> markers, Pen stroke, string polygonName)
        {
            return AddRoute(markers, stroke, defaultOverlayName, polygonName);
        }
        public GMapRoute AddRoute(IEnumerable<GMapMarker> markers, Pen stroke, string polygonName, string layoutId)
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

                Map.Overlays.Add(overlay);
            }

            SelectedRoute = route;

            return route;
        }

        public void HideAllLayout() 
        {
            foreach (GMapOverlay overlay in Map.Overlays)
            {
                overlay.IsVisibile = false;
            }
        }
        public void ShowAllLayout()
        {
            foreach (GMapOverlay overlay in Map.Overlays)
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

                Map.Overlays.Add(overlay);

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
            Map.Overlays.Remove(overlay);

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

            Map.Overlays.Remove(overlay);

            overlay.Id = overlay.Id.Remove(0, 2);
            overlay.Id = overlay.Id.Remove(overlay.Id.Length - 2);

            GMapOverlay existingOverlay = GetOverlayByIdOrNull(overlay.Id);

            if (existingOverlay == null)
            {
                Map.Overlays.Add(overlay);
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

                Map.Overlays.Add(overlay);

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
            Map.Overlays.Remove(overlay);

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

            Map.Overlays.Remove(overlay);

            overlay.Id = overlay.Id.Remove(0, 2);
            overlay.Id = overlay.Id.Remove(overlay.Id.Length - 2);

            GMapOverlay existingOverlay = GetOverlayByIdOrNull(overlay.Id);

            if (existingOverlay == null)
            {
                Map.Overlays.Add(overlay);
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
            PointLatLng coords = Map.FromLocalToLatLng(screenPoint.X, screenPoint.Y);

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

            foreach (var overlay in Map.Overlays)
            {
                overlay.Markers.Remove(marker);
            }
        }
        
        public void RemovePolygon(string polygonName)
        {
            RemovePolygon(GetPolygonByNameOrNull(polygonName));
        }
        public void RemovePolygon(GMapPolygon polygon)
        {
            foreach (var overlay in Map.Overlays)
            {
                int polygonIndex = overlay.Polygons.IndexOf(polygon);
                if (polygonIndex != -1)
                {
                    if (object.ReferenceEquals(overlay.Polygons[polygonIndex], SelectedPolygon))
                    {
                        SelectedPolygon = null;
                    }

                    overlay.Polygons[polygonIndex].Dispose();
                    overlay.Polygons.RemoveAt(polygonIndex);
                }
            }
        }

        public void RemoveRoute(string routeName)
        {
            RemovePolygon(GetPolygonByNameOrNull(routeName));
        }
        public void RemovePolygon(GMapRoute route)
        {
            foreach (var overlay in Map.Overlays)
            {
                int routeIndex = overlay.Routes.IndexOf(route);
                if (routeIndex != -1)
                {
                    if (object.ReferenceEquals(overlay.Routes[routeIndex], SelectedRoute))
                    {
                        SelectedRoute = null;
                    }

                    overlay.Routes[routeIndex].Dispose();
                    overlay.Routes.RemoveAt(routeIndex);
                }
            }
        }

        public IEnumerable<GMapMarker> GetMarkersByLayoutOrNull(string layoutId = "default")
        {
            GMapOverlay overlay = GetOverlayByIdOrNull(layoutId);

            return overlay != null ? overlay.Markers : null;
        }
        public IEnumerable<GMapPolygon> GetPolygonsByLayoutOrNull(string layoutId = "default")
        {
            GMapOverlay overlay = GetOverlayByIdOrNull(layoutId);

            return overlay != null ? overlay.Polygons : null;
        }

        public GMapMarker GetMarkerByCoordsOrNull(Point screenPoint)
        {
            return GetMarkerByCoordsOrNull(Map.FromLocalToLatLng(screenPoint.X, screenPoint.Y));
        }
        public GMapMarker GetMarkerByCoordsOrNull(PointLatLng coords)
        {
            double offsetLat = 0.0001;
            double offsetLng = 0.00009;

            if (Map.Zoom >= 0 && Map.Zoom <= 3)
            {
                offsetLat = 2.5;
                offsetLng = 2.4;
            }
            else if (Map.Zoom > 3 && Map.Zoom <= 5)
            {
                offsetLat = 0.9;
                offsetLng = 0.8;
            }
            else if (Map.Zoom > 5 && Map.Zoom <= 8)
            {
                offsetLat = 0.35;
                offsetLng = 0.2;
            }
            else if (Map.Zoom > 8 && Map.Zoom <= 11)
            {
                offsetLat = 0.05;
                offsetLng = 0.04;
            }
            else if (Map.Zoom > 11 && Map.Zoom <= 14)
            {
                offsetLat = 0.008;
                offsetLng = 0.005;
            }

            foreach (var overlay in Map.Overlays)
            {
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
            foreach (var overlay in Map.Overlays)
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
            foreach (var overlay in Map.Overlays)
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

        public void ClearMap()
        {
            EndPolygonDraw();
            EndRouteDraw();

            MapHelper.DisposeElements(defaultOverlay.Polygons);
            MapHelper.DisposeElements(defaultOverlay.Routes);

            foreach (GMapOverlay overlay in Map.Overlays)
            {
                overlay.Clear();
            }

            Map.Overlays.Clear();

            Map.Overlays.Add(defaultOverlay);
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
            foreach (var overlay in Map.Overlays)
            {
                overlay.Markers.Clear();
            }
        }
        public void ClearAllPolygons()
        {
            foreach (var overlay in Map.Overlays)
            {
                overlay.Polygons.Clear();
            }
        }
        public void ClearAllRoutes()
        {
            foreach (var overlay in Map.Overlays)
            {
                overlay.Routes.Clear();
            }
        }

        public void AddLayout(string layoutId)
        {
            GMapOverlay overlay = new GMapOverlay(layoutId);
            Map.Overlays.Add(overlay);
        }
        public void RemoveLayout(string layoutId)
        {
            GMapOverlay overlay = GetOverlayByIdOrNull(layoutId);

            if (overlay == null)
            {
                return;
            }

            Map.Overlays.Remove(overlay);

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
            Map.Zoom++;
        }
        public void ZoomMinus()
        {
            Map.Zoom--;
        }

        public void Dispose()
        {
            Map.Dispose();
        }

        private GMapOverlay GetOverlayByIdOrNull(string overlayId)
        {
            foreach (var overlay in Map.Overlays)
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
        public bool Equals(GMapMarker x, GMapMarker y)
        {
            if (x == null || y == null)
            {
                return false;
            }

            return x.Position.Lat > y.Position.Lat - 0.0008 && x.Position.Lat < y.Position.Lat + 0.0008 &&
                   x.Position.Lng > y.Position.Lng - 0.0008 && x.Position.Lng < y.Position.Lng + 0.0008;
        }

        public int GetHashCode(GMapMarker obj)
        {
            return obj.GetHashCode();
        }
    }
    public class PolygonsComparator : IEqualityComparer<GMapPolygon>
    {
        public bool Equals(GMapPolygon x, GMapPolygon y)
        {
            if (x == null || y == null)
            {
                return false;
            }

            return x.Name == y.Name;
        }

        public int GetHashCode(GMapPolygon obj)
        {
            return obj.GetHashCode();
        }
    }
    public class RoutesComparator : IEqualityComparer<GMapRoute>
    {
        public bool Equals(GMapRoute x, GMapRoute y)
        {
            if (x == null || y == null)
            {
                return false;
            }

            return x.Name == y.Name;
        }

        public int GetHashCode(GMapRoute obj)
        {
            return obj.GetHashCode();
        }
    }
}
