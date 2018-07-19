﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Text;
using CSharpMath.Enumerations;
using Typography.OpenFont;

namespace CSharpMath.Rendering {
  public interface IPainter<TSource, TColor> {
    #region Non-redisplaying properties
    TColor HighlightColor { get; set; }
    TColor TextColor { get; set; }
    TColor ErrorColor { get; set; }
    /// <summary>
    /// Unit of measure: points;
    /// Defaults to <see cref="FontSize"/>.
    /// </summary>
    float? ErrorFontSize { get; set; }
    bool DisplayErrorInline { get; set; }
    PaintStyle PaintStyle { get; set; }
    float Magnification { get; set; }

    RectangleF? Measure { get; }

    string ErrorMessage { get; }
    #endregion Non-redisplaying properties

    #region Redisplaying properties
    /// <summary>
    /// Unit of measure: points
    /// </summary>
    float FontSize { get; set; }
    ObservableCollection<Typeface> LocalTypefaces { get; }
    LineStyle LineStyle { get; set; }
    //(Color glyph, Color textRun)? GlyphBoxColor { get; set; }
    TSource Source { get; set; }
    #endregion Redisplaying properties

  }
  public interface ICanvasPainter<TCanvas, TSource, TColor> : IPainter<TSource, TColor> {
    #region Methods
    ICanvas WrapCanvas(TCanvas canvas);
    Structures.Color WrapColor(TColor color);
    TColor UnwrapColor(Structures.Color color);

    void UpdateDisplay();
    
    void Draw(TCanvas canvas, TextAlignment alignment = TextAlignment.Center, Thickness padding = default, float offsetX = 0, float offsetY = 0);
    void Draw(TCanvas canvas, float x, float y);
    void Draw(TCanvas canvas, PointF position);
    #endregion
  }
}