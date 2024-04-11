using MudBlazor;
using MudBlazor.Utilities;

namespace Site.Themes
{
    public class AlternativeTexturesTheme : BaseTheme
    {
        private static MudColor _mainThemeColor = new MudColor("96897B");
        private static MudColor _darkMainThemeColor = new MudColor("313638");
        public AlternativeTexturesTheme()
        {
            Palette = new PaletteLight
            {
                Background = new MudColor("DFD5A5"),
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
                Tertiary = _mainThemeColor.ColorLighten(0.3)
            };

            PaletteDark = new PaletteDark
            {
                Background = new MudColor("96897B"),
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
                Tertiary = _darkMainThemeColor.ColorLighten(0.3)
            };
        }
    }
}
