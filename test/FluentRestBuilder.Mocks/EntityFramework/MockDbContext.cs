// <copyright file="MockDbContext.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Mocks.EntityFramework
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    public class MockDbContext : DbContext
    {
        public MockDbContext()
            : this(ConfigureInMemoryContextOptions())
        {
        }

        public MockDbContext(DbContextOptions<MockDbContext> options)
            : base(options)
        {
        }

        public DbSet<Entity> Entities { get; set; }

        public DbSet<MultiKeyEntity> MultiKeyEntities { get; set; }

        public DbSet<Parent> Parents { get; set; }

        public DbSet<Child> Children { get; set; }

        public static DbContextOptions<MockDbContext> ConfigureInMemoryContextOptions()
        {
            var builder = new DbContextOptionsBuilder<MockDbContext>();

            // Create a fresh service provider, and therefore a fresh
            // InMemory database instance.
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            // Create a new options instance telling the context to use an
            // InMemory database and the new service provider.
            return builder.UseInMemoryDatabase()
                   .UseInternalServiceProvider(serviceProvider)
                   .Options;
        }

        public static void CreateEntities(DbContextOptions<MockDbContext> options)
        {
            using (var context = new MockDbContext(options))
            {
                context.Entities.AddRange(Entity.Entities);
                context.SaveChanges();
            }
        }

        public static void CreateMultiKeyEntities(DbContextOptions<MockDbContext> options)
        {
            using (var context = new MockDbContext(options))
            {
                context.MultiKeyEntities.AddRange(MultiKeyEntity.Entities);
                context.SaveChanges();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<MultiKeyEntity>()
                .HasKey(e => new { e.FirstId, e.SecondId });
        }
    }
}
