using MudBlazor;

namespace Site.Themes
{
    public class BaseTheme : MudTheme
    {
        public BaseTheme()
        {
            Palette = new PaletteLight
            {
                Background = new MudBlazor.Utilities.MudColor("212529"),
                AppbarBackground = new MudBlazor.Utilities.MudColor("495057")
            };
        }
    }
}
