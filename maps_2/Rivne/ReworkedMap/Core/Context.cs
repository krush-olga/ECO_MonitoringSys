using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using Maps.Helpers;

namespace Maps.Core
{
    public abstract class DrawContext : IDisposable
    {
        private static uint noNameContextNumber;

        private bool disposed;

        protected string figureName;

        protected List<PointLatLng> pointCoords;
        protected List<GMapMarker> polygonMarkers;

        protected GMarkerGoogleType polygonPointsType;

        static DrawContext()
        {
            noNameContextNumber = 0;
        }

        public DrawContext(GMapOverlay overlay)
            : this(overlay, null)
        { }
        public DrawContext(GMapOverlay overlay, string figureName)
            : this(overlay, figureName, GMarkerGoogleType.arrow)
        { }
        public DrawContext(GMapOverlay overlay, string figureName, GMarkerGoogleType markerType)
        {
            if (string.IsNullOrEmpty(figureName))
            {
                figureName = $"NewContextPolygon{noNameContextNumber++}";
            }

            Overlay = overlay;

            pointCoords = new List<PointLatLng>();
            polygonMarkers = new List<GMapMarker>();

            polygonPointsType = markerType;

            this.figureName = figureName;

            disposed = false;
        }

        ~DrawContext()
        {
            Dispose(false);
        }

        public GMapOverlay Overlay { get; protected set; }

        public IEnumerable<PointLatLng> PolygonPoints
        {
            get
            {
                return pointCoords.AsReadOnly();
            }
        }

        public string GetFigureName()
        {
            return figureName;
        }
        public virtual void SetFigureName(string name)
        {
            figureName = name;
        }

        public virtual void AddCoord(PointLatLng coord)
        {
            pointCoords.Add(coord);

            GMapMarker marker = MapHelper.CreateMarker(coord, polygonPointsType, string.Empty, null, null);

            Overlay.Markers.Add(marker);
            polygonMarkers.Add(marker);

            DrawFigure();
        }

        public virtual void RemoveCoord(int coordIndex)
        {
            if (coordIndex >= 0 && coordIndex < pointCoords.Count)
            {
                RemoveCoord(pointCoords[coordIndex]);
            }
        }
        public virtual void RemoveCoord(PointLatLng coords)
        {
            pointCoords.Remove(coords);

            GMapMarker deleteMarker = polygonMarkers.Where(m => m.Position == coords).FirstOrDefault();
            polygonMarkers.Remove(deleteMarker);
            Overlay.Markers.Remove(deleteMarker);

            DrawFigure();
        }

        public virtual void MoveCoord(PointLatLng newCoords, int coordIndex)
        {
            if (coordIndex >= 0 && coordIndex < pointCoords.Count)
            {
                pointCoords[coordIndex] = newCoords;
                polygonMarkers[coordIndex].Position = newCoords;

                DrawFigure();
            }
        }

        public void ClearFigure()
        {
            MapHelper.DisposeElements(polygonMarkers);
            MapHelper.DisposeElements(Overlay.Polygons);
            MapHelper.DisposeElements(Overlay.Routes);

            pointCoords.Clear();
            polygonMarkers.Clear();
            Overlay.Clear();
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    MapHelper.DisposeElements(polygonMarkers);

                    pointCoords.Clear();
                    polygonMarkers.Clear();
                    Overlay.Markers.Clear();
                }

                disposed = true;
            }
        }

        protected abstract void DrawFigure();
    }

    internal class PolygonContext : DrawContext
    {
        private readonly SolidBrush brush;
        private readonly Pen stroke;

        private GMapPolygon currentPolygon;

        public PolygonContext(GMapOverlay overlay, Color fill,
                              int opacity)
            : this(overlay, fill, opacity, null)
        {
        }
        public PolygonContext(GMapOverlay overlay, Color fill,
                              int opacity, string polygonName)
            : this(overlay, fill, opacity, polygonName, GMarkerGoogleType.arrow)
        { }
        public PolygonContext(GMapOverlay overlay, Color fill,
                              int opacity, string polygonName,
                              GMarkerGoogleType markerType)
            : base(overlay, polygonName, markerType)
        {

            this.brush = new SolidBrush(Color.FromArgb(opacity, fill));
            this.stroke = new Pen(new SolidBrush(Color.Red), 2);

            if (overlay.Polygons.Count != 0)
            {
                pointCoords = overlay.Polygons.FirstOrDefault()?.Points;

                foreach (var pointCoord in pointCoords)
                {
                    MapHelper.CreateMarker(pointCoord, markerType, string.Empty, null, null);
                }

                DrawFigure();
            }
        }

        public void SetOpacity(int opacity)
        {
            if (opacity < 0 || opacity > 255)
            {
                throw new ArgumentOutOfRangeException("opacity", "Прозрачность должна быть в пределе от 0 до 255");
            }

            brush.Color = Color.FromArgb(opacity, brush.Color);
        }
        public void SetFill(Color fill)
        {
            brush.Color = Color.FromArgb(brush.Color.A, fill);
        }
        public void SetStroke(Color stroke)
        {
            this.stroke.Color = stroke;
        }

        public int GetOpacity()
        {
            return brush.Color.A;
        }
        public Color GetFill()
        {
            return brush.Color;
        }
        public Color GetStroke()
        {
            return stroke.Color;
        }

        public override void SetFigureName(string name)
        {
            base.SetFigureName(name);

            if (currentPolygon != null)
            {
                currentPolygon.Name = name;
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (Overlay != null)
            {
                MapHelper.DisposeElements(Overlay.Routes);
                Overlay.Routes.Clear();
                Overlay = null;
            }
        }

        protected override void DrawFigure()
        {
            for (int i = 0; i < pointCoords.Count; i++)
            {
                polygonMarkers[i].Position = pointCoords[i];
                polygonMarkers[i].ToolTipText = i.ToString();
            }

            if (currentPolygon != null)
            {
                currentPolygon.Dispose();
                Overlay.Polygons.Remove(currentPolygon);
            }

            if (pointCoords.Count == 0)
            {
                return;
            }

            currentPolygon = new GMapPolygon(pointCoords, GetFigureName());
            currentPolygon.Fill = brush;
            currentPolygon.Stroke = stroke;

            Overlay.Polygons.Insert(0, currentPolygon);
        }
    }

    internal class RouteContext : DrawContext
    {
        private readonly Pen stroke;

        private GMapRoute currentRoute;

        public RouteContext(GMapOverlay overlay, Color routeStroke)
            : this(overlay, null, routeStroke)
        { }
        public RouteContext(GMapOverlay overlay, string routeName, Color routeStroke)
            : this(overlay, routeName, routeStroke, GMarkerGoogleType.arrow)
        { }
        public RouteContext(GMapOverlay overlay, string routeName,
                            Color routeStroke, GMarkerGoogleType markerType)
            : base(overlay, routeName, markerType)
        {
            stroke = new Pen(new SolidBrush(routeStroke), 2);

            if (overlay.Routes.Count != 0)
            {
                pointCoords = overlay.Routes.FirstOrDefault()?.Points;

                foreach (var pointCoord in pointCoords)
                {
                    MapHelper.CreateMarker(pointCoord, markerType, string.Empty, null, null);
                }

                DrawFigure();
            }
        }

        public void SetStroke(Color stroke)
        {
            this.stroke.Color = stroke;
        }
        public Color GetStroke()
        {
            return stroke.Color;
        }

        public override void SetFigureName(string name)
        {
            base.SetFigureName(name);

            if (currentRoute != null)
            {
                currentRoute.Name = name;
            }
        }
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (Overlay != null)
            {
                MapHelper.DisposeElements(Overlay.Polygons);
                Overlay.Polygons.Clear();
                Overlay = null;
            }
        }

        protected override void DrawFigure()
        {
            for (int i = 0; i < pointCoords.Count; i++)
            {
                polygonMarkers[i].Position = pointCoords[i];
                polygonMarkers[i].ToolTipText = i.ToString();
            }

            if (currentRoute != null)
            {
                currentRoute.Dispose();
                Overlay.Routes.Remove(currentRoute);
            }

            if (pointCoords.Count == 0)
            {
                return;
            }

            currentRoute = new GMapRoute(pointCoords, GetFigureName());
            currentRoute.Stroke = stroke;

            Overlay.Routes.Insert(0, currentRoute);
        }
    }
}
