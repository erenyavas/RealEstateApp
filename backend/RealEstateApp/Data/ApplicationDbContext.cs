using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using RealEstateApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RealEstateApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<RealEstateType> RealEstateTypes { get; set; }
        public DbSet<RealEstate> RealEstates { get; set; }
        public DbSet<RealEstateStatus> RealEstateStatuses { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Neighborhood> Neighborhoods { get; set; }
        public DbSet<DynamicFeature> DynamicFeatures { get; set; }
        public DbSet<RealEstateTypeFeature> RealEstateTypeFeatures { get; set; }
        public DbSet<RealEstateFeatureValue> RealEstateFeatureValues { get; set; }
        public DbSet<FeatureCategory> FeatureCategories { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<RealEstateTypeFeatureCategory> RealEstateTypeFeatureCategories { get; set; }
        public DbSet<RealEstateFeature> RealEstateFeatures { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Relationship configurations
            modelBuilder.Entity<RealEstateTypeFeature>()
                .HasKey(rt => new { rt.RealEstateTypeId, rt.FeatureId });

            modelBuilder.Entity<RealEstateTypeFeature>()
                .HasOne(rt => rt.RealEstateType)
                .WithMany(r => r.RealEstateTypeFeatures)
                .HasForeignKey(rt => rt.RealEstateTypeId);

            modelBuilder.Entity<RealEstateTypeFeature>()
                .HasOne(rt => rt.Feature)
                .WithMany(f => f.RealEstateTypeFeatures)
                .HasForeignKey(rt => rt.FeatureId);

            modelBuilder.Entity<RealEstateFeatureValue>()
                .HasOne(rv => rv.RealEstate)
                .WithMany(r => r.RealEstateFeatureValues)
                .HasForeignKey(rv => rv.RealEstateId);

            modelBuilder.Entity<RealEstateFeatureValue>()
                .HasOne(rv => rv.Feature)
                .WithMany(f => f.RealEstateFeatureValues)
                .HasForeignKey(rv => rv.FeatureId);

            modelBuilder.Entity<RealEstate>()
                .HasOne(re => re.RealEstateType)
                .WithMany(rt => rt.RealEstates)
                .HasForeignKey(re => re.RealEstateTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RealEstate>()
                .HasOne(re => re.RealEstateStatus)
                .WithMany(rs => rs.RealEstates)
                .HasForeignKey(re => re.RealEstateStatusId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RealEstate>()
                .HasOne(re => re.Currency)
                .WithMany(c => c.RealEstates)
                .HasForeignKey(re => re.CurrencyId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RealEstate>()
                .HasOne(re => re.Owner)
                .WithMany(o => o.RealEstates)
                .HasForeignKey(re => re.OwnerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<District>()
                .HasOne(d => d.City)
                .WithMany(c => c.Districts)
                .HasForeignKey(d => d.CityId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Neighborhood>()
                .HasOne(n => n.District)
                .WithMany(d => d.Neighborhoods)
                .HasForeignKey(n => n.DistrictId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RealEstateTypeFeatureCategory>()
                .HasKey(rtf => new { rtf.RealEstateTypeId, rtf.FeatureCategoryId });

            modelBuilder.Entity<RealEstateTypeFeatureCategory>()
                .HasOne(rtf => rtf.RealEstateType)
                .WithMany(rt => rt.RealEstateTypeFeatureCategories)
                .HasForeignKey(rtf => rtf.RealEstateTypeId);

            modelBuilder.Entity<RealEstateTypeFeatureCategory>()
                .HasOne(rtf => rtf.FeatureCategory)
                .WithMany(fc => fc.RealEstateTypeFeatureCategories)
                .HasForeignKey(rtf => rtf.FeatureCategoryId);

            modelBuilder.Entity<Feature>()
                .HasOne(f => f.FeatureCategory)
                .WithMany(fc => fc.Features)
                .HasForeignKey(f => f.FeatureCategoryId);

            modelBuilder.Entity<RealEstateFeature>()
                .HasKey(rf => new { rf.RealEstateId, rf.FeatureId });

            modelBuilder.Entity<RealEstateFeature>()
                .HasOne(rf => rf.RealEstate)
                .WithMany(re => re.RealEstateFeatures)
                .HasForeignKey(rf => rf.RealEstateId);

            modelBuilder.Entity<RealEstateFeature>()
                .HasOne(rf => rf.Feature)
                .WithMany(f => f.RealEstateFeatures)
                .HasForeignKey(rf => rf.FeatureId);

            // Convert List<string> to string for Options property
            modelBuilder.Entity<DynamicFeature>()
                .Property(df => df.Options)
                .HasConversion(
                    v => string.Join(',', v),
                    v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList())
                .Metadata
                .SetValueComparer(new ValueComparer<List<string>>(
                    (c1, c2) => c1.SequenceEqual(c2),
                    c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                    c => c.ToList()));

            // Convert List<string> to string for Photos property
            modelBuilder.Entity<RealEstate>()
                .Property(re => re.Photos)
                .HasConversion(
                    v => string.Join(',', v),
                    v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList())
                .Metadata
                .SetValueComparer(new ValueComparer<List<string>>(
                    (c1, c2) => c1.SequenceEqual(c2),
                    c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                    c => c.ToList()));

            // Specify precision and scale for X and Y properties
            modelBuilder.Entity<RealEstate>()
                .Property(re => re.X)
                .HasColumnType("decimal(18,6)");

            modelBuilder.Entity<RealEstate>()
                .Property(re => re.Y)
                .HasColumnType("decimal(18,6)");
        }
    }
}
