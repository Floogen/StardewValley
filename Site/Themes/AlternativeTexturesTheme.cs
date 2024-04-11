using MudBlazor;
using MudBlazor.Utilities;

namespace Site.Themes
{
    public class AlternativeTexturesTheme : BaseTheme
    {
        private static MudColor _mainThemeColor = new MudColor("807467");
        private static MudColor _darkMainThemeColor = new MudColor("64605A");
        public AlternativeTexturesTheme()
        {
            Palette = new PaletteLight
            {
                Background = new MudColor("4C4A46"),
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
                Secondary = _mainThemeColor.ColorLighten(0.05),
                Tertiary = _mainThemeColor.ColorLighten(0.3)
            };

            PaletteDark = new PaletteDark
            {
                Background = new MudColor("1F2224"),
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
                Secondary = _darkMainThemeColor.ColorLighten(0.05),
                Tertiary = _darkMainThemeColor.ColorLighten(0.3)
            };
        }
    }
}
