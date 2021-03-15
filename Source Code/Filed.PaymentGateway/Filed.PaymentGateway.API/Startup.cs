namespace Filed.PaymentGateway.API
{
    using Filed.PaymentGateway.BusinessInterface;
    using Filed.PaymentGateway.BusinessManager;
    using Filed.PaymentGateway.Data;
    using Filed.PaymentGateway.DataImplementation;
    using Filed.PaymentGateway.DataInterface;
    using Filed.PaymentGateway.PaymentProcessor;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    /// <summary>
    /// Startup
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <value>
        /// The configuration.
        /// </value>
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        /// <summary>
        /// Configures the services.
        /// </summary>
        /// <param name="services">The services.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddTransient<ICheapPaymentGateway, CheapPaymentGateway>();
            services.AddTransient<IExpensivePaymentGateway, ExpensivePaymentGateway>();
            services.AddTransient<IPremiumPaymentGateway, PremiumPaymentGateway>();
            services.AddTransient<IPaymentProcessManager, PaymentProcessManager>();
            services.AddTransient<IPaymentInformation, PaymentInformationData>();
            services.AddTransient<IPaymentState, PaymentStateData>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddDbContext<FiledDBContext>(opt => opt.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=Filed;Trusted_Connection=True;"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// <summary>
        /// Configures the specified application.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="env">The env.</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
