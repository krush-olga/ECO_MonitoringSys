using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using UserMap.Helpers;

namespace UserMap.Core
{
    /// <include file='Docs/Core/ContextDoc.xml' path='docs/members[@name="draw_context"]/DrawContext/*'/>
    public abstract class DrawContext : IDisposable
    {
#pragma warning disable CS1591 // Отсутствует комментарий XML для открытого видимого типа или члена
        protected internal enum DrawReason
        {
            PointAdded,
            PointMoved,
            PointDeleted
        }

        private static uint noNameContextNumber;

        private bool disposed;

        protected readonly GMarkerGoogleType polygonPointsType;

        private PointLatLng[] cache;

        protected DrawReason drawReason;
        protected string figureName;
#pragma warning restore CS1591 // Отсутствует комментарий XML для открытого видимого типа или члена

        static DrawContext()
        {
            noNameContextNumber = 0;
        }

        /// <include file='Docs/Core/ContextDoc.xml' path='docs/members[@name="draw_context"]/DrawContextCtor1/*'/>
        protected DrawContext(GMapOverlay overlay)
            : this(overlay, null)
        { }
        /// <include file='Docs/Core/ContextDoc.xml' path='docs/members[@name="draw_context"]/DrawContextCtor2/*'/>
        protected DrawContext(GMapOverlay overlay, string figureName)
            : this(overlay, figureName, GMarkerGoogleType.arrow)
        { }
        /// <include file='Docs/Core/ContextDoc.xml' path='docs/members[@name="draw_context"]/DrawContextCtor3/*'/>
        protected DrawContext(GMapOverlay overlay, string figureName, GMarkerGoogleType markerType)
        {
            if (string.IsNullOrEmpty(figureName))
            {
                figureName = $"NewContextPolygon{noNameContextNumber++}";
            }

            Overlay = overlay;

            polygonPointsType = markerType;

            this.figureName = figureName;

            disposed = false;
        }

        /// <include file='Docs/Core/ContextDoc.xml' path='docs/members[@name="draw_context"]/Overlay/*'/>
        public GMapOverlay Overlay { get; protected set; }

        /// <include file='Docs/Core/ContextDoc.xml' path='docs/members[@name="draw_context"]/PolygonPoints/*'/>
        public PointLatLng[] PolygonPoints
        {//TODO Оптимизировать алгоритм при добавлении новых точек.
         //Был вариант сохранят дополнительный список с точками.
            get
            {
                return cache ?? (cache = Overlay.Markers.Select(marker => marker.Position).ToArray());
            }
        }

        /// <include file='Docs/Core/ContextDoc.xml' path='docs/members[@name="draw_context"]/GetFigureName/*'/>
        public string GetFigureName()
        {
            return figureName;
        }


        /// <include file='Docs/Core/ContextDoc.xml' path='docs/members[@name="draw_context"]/SetFigureName/*'/>
        public virtual void SetFigureName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Имя фигуры не может быть пустым.", "name");
            }

            figureName = name;
        }

        /// <include file='Docs/Core/ContextDoc.xml' path='docs/members[@name="draw_context"]/AddCoord/*'/>
        public void AddCoord(PointLatLng coord)
        {
            GMapMarker marker = MapHelper.CreateMarker(coord, polygonPointsType, string.Empty, null, null);

            Overlay.Markers.Add(marker);

            drawReason = DrawReason.PointAdded;

            cache = null;

            DrawFigure();
        }

        /// <include file='Docs/Core/ContextDoc.xml' path='docs/members[@name="draw_context"]/RemoveCoordInt/*'/>
        public void RemoveCoord(int coordIndex)
        {
            if (coordIndex >= 0 && coordIndex < Overlay.Markers.Count)
            {
                //Overlay.Markers.Remove() по какой-то причине удаляет первый маркер из всей коллекции,
                //даже если он не первый и был явно найден
                Overlay.Markers.RemoveAt(coordIndex);

                drawReason = DrawReason.PointDeleted;

                cache = null;

                DrawFigure();
            }
        }
        /// <include file='Docs/Core/ContextDoc.xml' path='docs/members[@name="draw_context"]/RemoveCoordPointLatLng/*'/>
        public void RemoveCoord(PointLatLng coord)
        {
            var _index = -1;

            var deleteMarker = Overlay.Markers.Where((m, index) => { _index = index; return m.Position == coord; }).FirstOrDefault();

            RemoveCoord(_index);
        }

        public void RemoveCoordOnArea(RectLatLng area)
        {
            var topLeft = area.LocationTopLeft;
            var bottomRight = area.LocationRightBottom;

            for (int i = 0; i < Overlay.Markers.Count; i++)
            {
                var marker = Overlay.Markers[i];

                if (marker.Position.Lat < topLeft.Lat && marker.Position.Lat > bottomRight.Lat &&
                    marker.Position.Lng > topLeft.Lng && marker.Position.Lng < bottomRight.Lng)
                {
                    Overlay.Markers.RemoveAt(i);
                    i--;
                }
            }

            drawReason = DrawReason.PointDeleted;

            cache = null;

            DrawFigure();
        }

        /// <include file='Docs/Core/ContextDoc.xml' path='docs/members[@name="draw_context"]/MoveCoord/*'/>
        public void MoveCoord(PointLatLng newCoord, int coordIndex)
        {
            if (coordIndex >= 0 && coordIndex < Overlay.Markers.Count)
            {
                Overlay.Markers[coordIndex].Position = newCoord;

                if (cache != null)
                    cache[coordIndex] = newCoord;

                drawReason = DrawReason.PointMoved;

                DrawFigure();
            }
        }

        /// <include file='Docs/Core/ContextDoc.xml' path='docs/members[@name="draw_context"]/ClearFigure/*'/>
        public void ClearFigure()
        {
            MapHelper.DisposeElements(Overlay.Polygons);
            MapHelper.DisposeElements(Overlay.Routes);

            Overlay.Clear();
        }

        /// <inheritdoc/>
        public virtual void Dispose()
        {
            if (!disposed)
            {
                Overlay.Markers.Clear();

                disposed = true;
            }
        }

        /// <include file='Docs/Core/ContextDoc.xml' path='docs/members[@name="draw_context"]/DrawFigure/*'/>
        protected abstract void DrawFigure();
    }

    /// <include file='Docs/Core/ContextDoc.xml' path='docs/members[@name="polygon_context"]/PolygonContext/*'/>
    public class PolygonContext : DrawContext
    {
        private readonly SolidBrush brush;
        private readonly Pen stroke;

        private GMapPolygon currentPolygon;

        /// <include file='Docs/Core/ContextDoc.xml' path='docs/members[@name="polygon_context"]/PolygonContextCtor1/*'/>
        public PolygonContext(GMapOverlay overlay, Color fill,
                              int opacity)
            : this(overlay, fill, opacity, null)
        {
        }
        /// <include file='Docs/Core/ContextDoc.xml' path='docs/members[@name="polygon_context"]/PolygonContextCtor2/*'/>
        public PolygonContext(GMapOverlay overlay, Color fill,
                              int opacity, string polygonName)
            : this(overlay, fill, opacity, polygonName, GMarkerGoogleType.arrow)
        { }
        /// <include file='Docs/Core/ContextDoc.xml' path='docs/members[@name="polygon_context"]/PolygonContextCtor3/*'/>
        public PolygonContext(GMapOverlay overlay, Color fill,
                              int opacity, string polygonName,
                              GMarkerGoogleType markerType)
            : base(overlay, polygonName, markerType)
        {

            this.brush = new SolidBrush(Color.FromArgb(opacity, fill));
            this.stroke = new Pen(new SolidBrush(Color.Red), 2);
        }

        /// <include file='Docs/Core/ContextDoc.xml' path='docs/members[@name="polygon_context"]/SetOpacity/*'/>
        public void SetOpacity(int opacity)
        {
            if (opacity < 0 || opacity > 255)
            {
                throw new ArgumentOutOfRangeException("opacity", "Прозрачность должна быть в пределе от 0 до 255");
            }

            brush.Color = Color.FromArgb(opacity, brush.Color);
        }
        /// <include file='Docs/Core/ContextDoc.xml' path='docs/members[@name="polygon_context"]/SetFill/*'/>
        public void SetFill(Color fill)
        {
            brush.Color = Color.FromArgb(brush.Color.A, fill);
        }
        /// <include file='Docs/Core/ContextDoc.xml' path='docs/members[@name="polygon_context"]/SetStroke/*'/>
        public void SetStroke(Color stroke)
        {
            this.stroke.Color = stroke;
        }

        /// <include file='Docs/Core/ContextDoc.xml' path='docs/members[@name="polygon_context"]/GetOpacity/*'/>
        public int GetOpacity()
        {
            return brush.Color.A;
        }
        /// <include file='Docs/Core/ContextDoc.xml' path='docs/members[@name="polygon_context"]/GetFill/*'/>
        public Color GetFill()
        {
            return brush.Color;
        }
        /// <include file='Docs/Core/ContextDoc.xml' path='docs/members[@name="polygon_context"]/GetStroke/*'/>
        public Color GetStroke()
        {
            return stroke.Color;
        }

        /// <inheritdoc/>
        public override void SetFigureName(string name)
        {
            base.SetFigureName(name);

            if (currentPolygon != null)
            {
                currentPolygon.Name = name;
            }
        }

        /// <inheritdoc/>
        public override void Dispose()
        {
            base.Dispose();

            if (Overlay != null)
            {
                MapHelper.DisposeElements(Overlay.Routes);
                Overlay.Routes.Clear();
                Overlay = null;
            }
        }

        /// <inheritdoc/>
        protected override void DrawFigure()
        {
            if (drawReason != DrawReason.PointMoved)
            {
                for (int i = 0; i < Overlay.Markers.Count; i++)
                {
                    Overlay.Markers[i].ToolTipText = i.ToString();
                }
            }

            if (currentPolygon != null)
            {
                currentPolygon.Dispose();
                Overlay.Polygons.Remove(currentPolygon);
            }

            if (Overlay.Markers.Count == 0)
            {
                return;
            }

            currentPolygon = new GMapPolygon(Overlay.Markers.Select(marker => marker.Position).ToList(), GetFigureName());
            currentPolygon.Fill = brush;
            currentPolygon.Stroke = stroke;

            Overlay.Polygons.Insert(0, currentPolygon);
        }
    }

    /// <include file='Docs/Core/ContextDoc.xml' path='docs/members[@name="route_context"]/RouteContext/*'/>
    public class RouteContext : DrawContext
    {
        private readonly Pen stroke;

        private GMapRoute currentRoute;

        /// <include file='Docs/Core/ContextDoc.xml' path='docs/members[@name="route_context"]/RouteContextCtor1/*'/>
        public RouteContext(GMapOverlay overlay, Color routeStroke)
            : this(overlay, null, routeStroke)
        { }
        /// <include file='Docs/Core/ContextDoc.xml' path='docs/members[@name="route_context"]/RouteContextCtor2/*'/>
        public RouteContext(GMapOverlay overlay, string routeName, Color routeStroke)
            : this(overlay, routeName, routeStroke, GMarkerGoogleType.arrow)
        { }
        /// <include file='Docs/Core/ContextDoc.xml' path='docs/members[@name="route_context"]/RouteContextCtor3/*'/>
        public RouteContext(GMapOverlay overlay, string routeName,
                            Color routeStroke, GMarkerGoogleType markerType)
            : base(overlay, routeName, markerType)
        {
            stroke = new Pen(new SolidBrush(routeStroke), 2);
        }

        /// <include file='Docs/Core/ContextDoc.xml' path='docs/members[@name="route_context"]/SetStroke/*'/>
        public void SetStroke(Color stroke)
        {
            this.stroke.Color = stroke;
        }
        /// <include file='Docs/Core/ContextDoc.xml' path='docs/members[@name="route_context"]/GetStroke/*'/>
        public Color GetStroke()
        {
            return stroke.Color;
        }

        /// <inheritdoc/>
        public override void SetFigureName(string name)
        {
            base.SetFigureName(name);

            if (currentRoute != null)
            {
                currentRoute.Name = name;
            }
        }
        /// <inheritdoc/>
        public override void Dispose()
        {
            base.Dispose();

            if (Overlay != null)
            {
                MapHelper.DisposeElements(Overlay.Polygons);
                Overlay.Polygons.Clear();
                Overlay = null;
            }
        }

        /// <inheritdoc/>
        protected override void DrawFigure()
        {
            for (int i = 0; i < Overlay.Markers.Count; i++)
            {
                Overlay.Markers[i].ToolTipText = i.ToString();
            }

            if (currentRoute != null)
            {
                currentRoute.Dispose();
                Overlay.Routes.Remove(currentRoute);
            }

            if (Overlay.Markers.Count == 0)
            {
                return;
            }

            currentRoute = new GMapRoute(Overlay.Markers.Select(marker => marker.Position).ToList(), GetFigureName());
            currentRoute.Stroke = stroke;

            Overlay.Routes.Insert(0, currentRoute);
        }
    }
}
