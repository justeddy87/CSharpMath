﻿using System;
using CSharpMath.FrontEnd;
using CSharpMath.Resources;
using Foundation;
using CoreText;
using TGlyph = System.UInt16;
using CSharpMath.Ios.Resources;

namespace CSharpMath.Apple {
  public static class AppleTypesetters {
    private static TypesettingContext<AppleMathFont, TGlyph> CreateTypesettingContext(CTFont someCtFontSizeIrrelevant) {
      var boundsProvider = AppleGlyphBoundsProvider.Instance;
      return new TypesettingContext<AppleMathFont, TGlyph>(
        (font, size) => new AppleMathFont(font, size),
        boundsProvider,
        CtFontGlyphFinder.Instance,
        UnicodeFontChanger.Instance,
        new JsonMathTable<AppleMathFont, TGlyph>(new AppleFontMeasurer(), IosResources.LatinMath, new AppleGlyphNameProvider(someCtFontSizeIrrelevant), boundsProvider)
      );
    }

    private static TypesettingContext<AppleMathFont, TGlyph> CreateLatinMath() {
      var fontSize = 20;
      var appleFont = AppleFontManager.LatinMath(fontSize);
      return CreateTypesettingContext(appleFont.CtFont);
    }

    private static TypesettingContext<AppleMathFont, TGlyph> _latinMath;

    private static readonly object _lock = new object();

    public static TypesettingContext<AppleMathFont, TGlyph> LatinMath {
      get {
        if (_latinMath == null) {
          lock(_lock) {
            if (_latinMath == null)
            {
              _latinMath = CreateLatinMath();
            }
          }
        }
        return _latinMath;
      }
    }
  }
}
