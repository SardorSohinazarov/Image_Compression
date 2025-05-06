using Image_Compression.Api.Services;
using Image_Compression.Api.Services.Compressors;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

/// .Net yordamida lekin bu faqat Windowsda ishlaydi
builder.Services.AddScoped<ICompressor, BitmapCompressor>();
/// Magick.NET yordamida lunixda qo'shimcha narsalar o'natish kerak
builder.Services.AddScoped<ICompressor, MagickCompressor>();
/// ImageSharp top
builder.Services.AddScoped<ICompressor, ImageSharpCompressor>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
