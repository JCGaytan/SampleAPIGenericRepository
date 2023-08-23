using APIGenericRepository.DataAccess;
using APIGenericRepository.Entities;
using EncryptionLibrary;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace APIGenericRepository
{
    /// <summary>
    /// The configuration and startup class for the API.
    /// </summary>
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Configure services for the application.
        /// </summary>
        /// <param name="services">The collection of services to configure.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            // Set up the database context using SQLite
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlite(_configuration.GetConnectionString("DefaultConnection")));

            // Register the generic repository implementation
            services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));

            // Register the AES encryption service
            services.AddTransient<AesEncryptor>();

            // Configure Cross-Origin Resource Sharing (CORS)
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                    });
            });

            // Configure JWT authentication
            var jwtSettings = _configuration.GetSection("JwtSettings");
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtSettings["Issuer"],
                        ValidAudience = jwtSettings["Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]))
                    };
                });

            // Add controllers
            services.AddControllers();

            // Add API documentation using Swagger
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }

        /// <summary>
        /// Configure the application pipeline.
        /// </summary>
        /// <param name="app">The application builder.</param>
        /// <param name="env">The hosting environment.</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Enable Swagger UI for development environment
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Ensure database is created and populate with default data if needed
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                dbContext.Database.EnsureCreated();

                if (!dbContext.Users.Any())
                {
                    // Create roles and users
                    var adminRole = new UserRole
                    {
                        Name = "Admin",
                        Description = "Administrator Role",
                        CreatedDate = DateTime.UtcNow,
                        IsActive = true
                    };
                    var userRole = new UserRole
                    {
                        Name = "User",
                        Description = "Regular User Role",
                        CreatedDate = DateTime.UtcNow,
                        IsActive = true
                    };

                    // Save roles to generate IDs
                    dbContext.UserRoles.AddRange(adminRole, userRole);
                    dbContext.SaveChanges();

                    // Encrypt passwords using AES encryption
                    var encryptionKey = _configuration["AppSettings:EncryptionKey"];
                    var encryptor = new AesEncryptor();

                    // Create admin and regular user accounts
                    var adminUser = new User
                    {
                        UserName = "admin",
                        Password = Convert.ToBase64String(encryptor.EncryptAES("adminpassword", encryptionKey)),
                        UserRoleId = adminRole.Id,
                        FirstName = "Admin",
                        LastName = "User",
                        Email = "admin@example.com",
                        CreatedDate = DateTime.UtcNow,
                        IsActive = true
                    };

                    var regularUser = new User
                    {
                        UserName = "user",
                        Password = Convert.ToBase64String(encryptor.EncryptAES("userpassword", encryptionKey)),
                        UserRoleId = userRole.Id,
                        FirstName = "Regular",
                        LastName = "User",
                        Email = "user@example.com",
                        CreatedDate = DateTime.UtcNow,
                        IsActive = true
                    };

                    // Save users
                    dbContext.Users.AddRange(adminUser, regularUser);
                    dbContext.SaveChanges();

                    // Seed test data for teachers, courses, and students
                    var teacher = new Teacher
                    {
                        FirstName = "John",
                        LastName = "Doe",
                        Email = "john@example.com",
                        CreatedDate = DateTime.UtcNow,
                        IsActive = true
                    };
                    dbContext.Teachers.Add(teacher);
                    dbContext.SaveChanges();

                    var course = new Course
                    {
                        Title = "Mathematics",
                        TeacherId = teacher.Id,
                        CreatedDate = DateTime.UtcNow,
                        IsActive = true
                    };
                    dbContext.Courses.Add(course);
                    dbContext.SaveChanges();

                    var student = new Student
                    {
                        CourseId = course.Id,
                        FirstName = "Alice",
                        LastName = "Smith",
                        Email = "alice@example.com",
                        CreatedDate = DateTime.UtcNow,
                        IsActive = true
                    };
                    dbContext.Students.Add(student);
                    dbContext.SaveChanges();
                }
            }

            // Set up application middleware
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors("AllowAllOrigins");
            app.UseAuthentication();
            app.UseAuthorization();

            // Map endpoints
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
