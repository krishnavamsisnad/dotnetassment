
using Bookstoreapi_1.Business;
using Bookstoreapi_1.Data;
using Bookstoreapi_1.Repository.RepositryInterface;
using Bookstoreapi_1.Repository;
using Microsoft.EntityFrameworkCore;
using Sieve.Services;

namespace Bookstoreapi_1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
            builder.Services.AddScoped<CustomerService>();
            builder.Services.AddDbContext<BookstoreDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Book")));


            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddSingleton<SieveProcessor>();
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
        }
    }
}
