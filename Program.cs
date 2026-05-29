using Cosmetic_App.Repository;
using Cosmetic_App.Repository.Interfaces;
using Cosmetic_App.Service;
using Cosmetic_App.Service.Interfaces;
using Dapper;

var builder = WebApplication.CreateBuilder(args);

// --- 0. Cấu hình Dapper để hỗ trợ Guid ---
// Giúp Dapper hiểu cách ánh xạ Guid giữa C# và Database (MySQL)
SqlMapper.AddTypeHandler(new GuidTypeHandler());

// --- 1. Cấu hình CORS ---
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.WithOrigins("http://localhost:3000")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// --- 2. Đăng ký Dependency Injection (DI) ---
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IOrderService, OrderService>();

var app = builder.Build();

// --- 3. Cấu hình Middleware ---

// Middleware bắt lỗi: Nếu có lỗi 500, nó sẽ in ra Console của Visual Studio
app.Use(async (context, next) =>
{
    try
    {
        await next();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"[LỖI SERVER]: {ex.Message}");
        Console.WriteLine($"[STACK TRACE]: {ex.StackTrace}");
        context.Response.StatusCode = 500;
        await context.Response.WriteAsync("Lỗi hệ thống: " + ex.Message);
    }
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseHttpsRedirection();
}

app.UseCors("AllowReactApp");
app.UseAuthorization();
app.MapControllers();

app.Run();

// --- Helper class cho Dapper ---
public class GuidTypeHandler : SqlMapper.TypeHandler<Guid>
{
    public override void SetValue(System.Data.IDbDataParameter parameter, Guid value)
        => parameter.Value = value.ToString(); // Đảm bảo Guid được lưu dưới dạng chuỗi trong MySQL

    public override Guid Parse(object value)
    {
        if (value == null) return Guid.Empty;
        return Guid.Parse(value.ToString() ?? Guid.Empty.ToString());
    }
}