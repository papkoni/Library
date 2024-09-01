using System;
using Library.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Library.API.Extensions
{
	public static class MigrationExtensions
	{
		public static void ApplyMigrations(this IApplicationBuilder app)
		{
			using IServiceScope scope = app.ApplicationServices.CreateAsyncScope();

			using LibraryDbContext libraryDbContext =
				scope.ServiceProvider.GetRequiredService<LibraryDbContext>();

			libraryDbContext.Database.Migrate();
        }

    }
}

