using cropto.Backend;
using Radzen;

namespace cropto
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();

            builder.Services.AddScoped<GetDecreasing>();
            builder.Services.AddScoped<GetHighestTradingVolume>();
            builder.Services.AddScoped<GetLowestTradingVolume>();

            builder.Services.AddScoped<DialogService>();
            builder.Services.AddScoped<NotificationService>();
            builder.Services.AddScoped<TooltipService>();
            builder.Services.AddScoped<ContextMenuService>();

            FetchDataFromApiAsync();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");

            app.Run();

        }



        private static void FetchDataFromApiAsync()
        {
            long starttime = ((DateTimeOffset)DateTime.UtcNow.AddYears(-13)).ToUnixTimeSeconds();
            long endtime = ((DateTimeOffset)DateTime.UtcNow).ToUnixTimeSeconds();
            string _apiUrl = $"https://api.coingecko.com/api/v3/coins/bitcoin/market_chart/range?vs_currency=eur&from={starttime}&to={endtime}";
            new DataFetch().ApiSearchDataTask(_apiUrl);
        }


    }
    
}