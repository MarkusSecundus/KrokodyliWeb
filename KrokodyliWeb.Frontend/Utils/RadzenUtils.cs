using Radzen;

namespace KrokodyliWeb.Frontend.Utils
{
    public static class RadzenUtils
    {
        public static void AddRadzen(this IServiceCollection services)
        {
            services.AddScoped<DialogService>();
            services.AddScoped<NotificationService>();
            services.AddScoped<TooltipService>();
            services.AddScoped<ContextMenuService>();
        }



        public static readonly IReadOnlyDictionary<string, object> DataGridLocalizationParams = new Dictionary<string, object>
        {
            ["FilterText"] = "Filtr",
            ["EnumFilterSelectText"] = "Vybrat...",
            ["AndOperatorText"] = "A",
            ["OrOperatorText"] = "Nebo",
            ["ApplyFilterText"] = "OK",
            ["ClearFilterText"] = "Vyčistit",
            ["EqualsText"] = "Je rovno",
            ["NotEqualsText"] = "Není rovno",
            ["LessThanText"] = "Méně než",
            ["LessThanOrEqualsText"] = "Méně nebo rovno",
            ["GreaterThanText"] = "Více než",
            ["GreaterThanOrEqualsText"] = "Více nebo rovno",
            ["EndsWithText"] = "Má na konci",
            ["ContainsText"] = "Obsahuje",
            ["DoesNotContainText"] = "Neobsahuje",
            ["StartsWithText"] = "Má na začátku",
            ["IsNullText"] = "Je NULL",
            ["IsNotNullText"] = "Není NULL",
            ["IsEmptyText"] = "Je prázdný",
            ["IsNotEmptyText"] = "Není prázdný",
            ["ColumnsShowingText"] = "tabulka je právě vykreslována",
            ["AllColumnsText"] = "Vše",
            ["ColumnsText"] = "Sloupce",
            ["GroupPanelText"] = "Přetáhněte sloupec sem a upusťte ho, aby podle něj byla tabulka seřazena"
        };
    }
}
