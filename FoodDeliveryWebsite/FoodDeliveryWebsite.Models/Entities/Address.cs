﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using FoodDeliveryWebsite.Models.Common;
using static FoodDeliveryWebsite.Models.Constants.AddressConstants;

namespace FoodDeliveryWebsite.Models.Entities
{
    public class Address : IEntity, IAuditable
    {
        public int Id { get; set; }

        public DateTime CreateDate { get; set; }

        public int CreatorUserId { get; set; }

        public string City { get; set; }

        public string Street { get; set; }

        public int StreetNo { get; set; }

        public int? Floor { get; set; }

        public int? ApartmentNo { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public bool IsDeleted { get; set; }
    }

    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.Property(b => b.City)
                .IsRequired()
                .HasMaxLength(CityMaxLength);

            builder.Property(b => b.Street)
                .IsRequired()
                .HasMaxLength(StreetMaxLength);
        }
    }
}
