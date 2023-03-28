var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
// Configure the app to use the appsettings.json file
builder.Configuration.AddJsonFile("appsettings.json");
// 


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.Use(async (context, next) =>
{
    //deny IFrame embedding
    context.Response.Headers.Add("X-Frame-Options", "DENY");
    //blocks site loading if cross site scripting is detected
    context.Response.Headers.Add("X-Xss-Protection", "1; mode=block");
    //stop mime sniffing
    context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
    //block referer headers
    context.Response.Headers.Add("Referrer-Policy", "no-referrer");
    //block cross site requests
    context.Response.Headers.Add("X-Permitted-Cross-Domain-Policies", "none");
    //block uneeded browser features
    context.Response.Headers.Add("Permissions-Policy", "accelerometer=(), camera=(), geolocation=(), gyroscope=(), magnetometer=(), microphone=(), payment=(), usb=()");
    //default content security policy restrict where resources can be loaded from
    context.Response.Headers.Add("Content-Security-Policy", "default-src 'self'");
    await next();
});

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
