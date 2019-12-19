using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MTSDiscount.Core.Infrastructure;
using MTSDiscount.Core.Interfaces;
using MTSDiscount.Core.Repository;

namespace MTSDiscount.Web {    
    public class Startup {
        private IConfigurationRoot _confString;

        public Startup(IWebHostEnvironment hostEnv) { // � ������������ Startup-� �������� ������ ������������ �� ����� dbsettings.json)
            // SetBasePath - ��������� ����
            // hostEnv.ContentRootPath - ������ �����
            // AddJsonFile("dbsettings.json") - ��������� ��� ����
            // Build() - ��������� ������ ������ ����� ����� ��������� ���� � �� � ���� ����������
            _confString = new ConfigurationBuilder().SetBasePath(hostEnv.ContentRootPath).AddJsonFile("dbsettings.json").Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services.AddControllersWithViews();
            // ���������� ���� �������� � ��, _confString ��������� � ����� ������� �� ��������
            services. AddDbContext<AppDbContext>(options => options.UseSqlServer(_confString.GetConnectionString("DefaultConnection")));
            // ����������: 1-� �������� - � ����� ����������� ��������, 2-� - ����� ����� ��������� ���� ���������
            services.AddTransient<IDiscountRepository, DiscountRepository>(); // ��������� IDiscountRepository ����������� � ������ DiscountRepository                                   
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            } else {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            
            app.UseEndpoints(endpoints => {

                /*endpoints.MapControllerRoute(
                    name: null,
                    //url: "Page{page}",
                    defaults: new {
                        controller = "Game", action = "List"
                    });*/

                endpoints.MapControllerRoute(
                    name: "list",
                    pattern: "controller/action/{page?}");                    

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Discounts}/{action=List}/{page?}");
            });

            using (var scope = app.ApplicationServices.CreateScope()) { // ������� ������� (���������) ��� ������������� ������� AppDBContent
                AppDbContext context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                AppDbContextSeed.Initial(context); // ���������� � �� �� ��������
            }
        }
    }
}
