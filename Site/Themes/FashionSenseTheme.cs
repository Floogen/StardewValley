using MudBlazor;
using MudBlazor.Utilities;

namespace Site.Themes
{
    public class FashionSenseTheme : BaseTheme
    {
        private static MudColor _mainThemeColor = new MudColor("FFB3C6");
        private static MudColor _darkMainThemeColor = new MudColor("67B0F8");
        public FashionSenseTheme()
        {
            Palette = new PaletteLight
            {
                Background = new MudColor("696d7d"),
                Dark = new MudColor("979287"),
                Surface = _mainThemeColor,
                TextPrimary = Colors.Shades.White,
                AppbarText = Colors.Shades.White,
                AppbarBackground = _mainThemeColor,
                Primary = _mainThemeColor,
                TextDisabled = Colors.Shades.White,
                ActionDisabled = Colors.Grey.Lighten1,
                ActionDefault = Colors.Shades.White,
                ActionDisabledBackground = Colors.Grey.Darken2,
                LinesInputs = Colors.Shades.White,
                DrawerBackground = _mainThemeColor,
                DrawerText = Colors.Shades.White,
                Tertiary = _mainThemeColor.ColorDarken(0.3)
            };

            PaletteDark = new PaletteDark
            {
                Background = new MudColor("003294"),
                Dark = new MudColor("018EAA"),
                Surface = _darkMainThemeColor,
                TextPrimary = Colors.Shades.White,
                AppbarText = Colors.Shades.White,
                AppbarBackground = _darkMainThemeColor,
                Primary = _darkMainThemeColor,
                TextDisabled = Colors.Shades.White,
                ActionDisabled = Colors.Grey.Lighten1,
                ActionDefault = Colors.Shades.White,
                ActionDisabledBackground = Colors.Grey.Darken2,
                LinesInputs = Colors.Shades.White,
                DrawerBackground = _darkMainThemeColor,
                DrawerText = Colors.Shades.White,
                Tertiary = _darkMainThemeColor.ColorDarken(0.3)
            };
        }
    }
}
