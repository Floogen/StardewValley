using MudBlazor;
using MudBlazor.Utilities;

namespace Site.Themes
{
    public class FashionSenseTheme : BaseTheme
    {
        private static MudColor _mainThemeColor = new MudColor("F2BAC9");
        private static MudColor _darkMainThemeColor = new MudColor("672952");
        public FashionSenseTheme()
        {
            Palette = new PaletteLight
            {
                Background = new MudColor("4C4C4C"),
                Dark = new MudColor("979287"),
                Surface = _mainThemeColor,
                TextPrimary = Colors.Shades.Black,
                TextSecondary = Colors.Shades.White,
                AppbarText = Colors.Shades.Black,
                AppbarBackground = _mainThemeColor,
                Primary = _mainThemeColor,
                TextDisabled = Colors.Shades.Black,
                ActionDisabled = Colors.Grey.Lighten1,
                ActionDefault = Colors.Shades.White,
                ActionDisabledBackground = Colors.Grey.Darken2,
                LinesInputs = Colors.Shades.White,
                DrawerBackground = _mainThemeColor,
                DrawerText = Colors.Shades.Black,
                Secondary = _mainThemeColor.ColorLighten(0.05),
                Tertiary = _mainThemeColor.ColorDarken(0.3)
            };

            PaletteDark = new PaletteDark
            {
                Background = new MudColor("100D14"),
                Dark = new MudColor("018EAA"),
                Surface = _darkMainThemeColor,
                TextPrimary = Colors.Shades.White,
                TextSecondary = Colors.Shades.White,
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
                Secondary = _darkMainThemeColor.ColorLighten(0.06),
                Tertiary = _darkMainThemeColor.ColorDarken(0.3)
            };
        }
    }
}
