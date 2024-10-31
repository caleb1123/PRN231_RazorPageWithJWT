var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddHttpClient(); // Thêm dịch vụ HttpClient
builder.Services.AddSession(); // Thêm dịch vụ Session
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseSession(); // Sử dụng session trong ứng dụng


app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
