using Quartz;
using Quartz.AspNetCore;
using Quartz_Demo;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddQuartz(q =>
{
    // base Quartz scheduler, job and trigger configuration
    var jobKey = new JobKey("PrintInConsole");
    q.AddJob<PrintJob>(opts => opts.WithIdentity(jobKey));
    q.AddTrigger(opts => opts
        .ForJob(jobKey)
        .WithIdentity("PrintInConsole-trigger")
        // Configure to run every 10 seconds
        .WithSimpleSchedule(x => x
            .WithIntervalInSeconds(10)
            .RepeatForever())
    );
});


// ASP.NET Core hosting
builder.Services.AddQuartzServer(options =>
{
    // when shutting down we want jobs to complete gracefully
    options.WaitForJobsToComplete = true;
});



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
