using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add Razor Pages và route mặc định (không custom homepage vì bạn dùng /Index)
builder.Services.AddRazorPages(options =>
{
    // Không cần AddPageRoute nếu giữ Pages/Index.cshtml làm trang chủ
    // Nếu sau này muốn /Cosmetics/Index là homepage thì sẽ cấu hình lại ở đây
});

// Inject cấu hình
builder.Services.AddSingleton(builder.Configuration);

// Cấu hình xác thực bằng Cookie
builder.Services.AddAuthentication("CookieAuth")
    .AddCookie("CookieAuth", options =>
    {
        options.LoginPath = "/Login"; // Khi chưa login thì chuyển về trang này
        options.AccessDeniedPath = "/Unauthorized"; // Khi không đủ quyền (nếu dùng role-based)
    });

// Bổ sung ủy quyền (policy nếu cần)
builder.Services.AddAuthorization();

var app = builder.Build();

// Middleware xử lý lỗi và HTTPS
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Bắt buộc để cookie authentication hoạt động
app.UseAuthentication();
app.UseAuthorization();

// Map Razor Pages
app.MapRazorPages();

app.Run();
