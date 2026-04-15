using EkartAPI.Models;
using EkartAPI.Models.ResponseModels;
using Microsoft.EntityFrameworkCore;

namespace EkartAPI.Data
{
    public class EkartDBcontext : DbContext
    {
        public EkartDBcontext(DbContextOptions<EkartDBcontext> options) : base(options) { }

        public DbSet<userModel> tbl_users { get; set; }
        public DbSet<Product> tbl_products { get; set; }
        public DbSet<Cart> tbl_cart { get; set; }
        public DbSet<DeliveryAddressModel> tbl_DeliveryAddress { get; set; }
        public DbSet<CategoryModel> TblCategory { get; set; }
        public DbSet<OrdersModel> Orders { get; set; }
        public DbSet<OrdersResModel> OrdersResModel { get; set; }

        public DbSet<OrderDetails> OrderDetails { get; set; }

        public DbSet<forgotpasswordreponse> password_reset { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<OrdersResModel>().HasNoKey();
            modelBuilder.Entity<userCount>().HasNoKey();
            modelBuilder.Entity<ProductByCategoryModel>().HasNoKey();
            modelBuilder.Entity<Category>().HasNoKey();
            modelBuilder.Entity<SubCategoryFK>().HasNoKey();
            modelBuilder.Entity<RegisterFKResponseModel>().HasNoKey();
            modelBuilder.Entity<CartFKResponseModel>().HasNoKey();
            modelBuilder.Entity<CartFK>().HasNoKey();
            modelBuilder.Entity<Variation>().HasNoKey();
            modelBuilder.Entity<ProductsFK>().HasNoKey();
            modelBuilder.Entity<OrderCheckout>().HasNoKey();
            modelBuilder.Entity<OrderProductDto>().HasNoKey();
            modelBuilder.Entity<ProductStatusDto>().HasNoKey();
            modelBuilder.Entity<CancelOrderRequest>().HasNoKey();
            modelBuilder.Entity<CancelOrderInfo>().HasNoKey();
            modelBuilder.Entity<OrderDetails>().HasKey(o => o.OrderId);
            modelBuilder.Entity<OrderStatus>()
    .Property(o => o.created_At)
    .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<OrderStatus>()
                .Property(o => o.updated_At)
                .HasDefaultValueSql("GETDATE()");


            modelBuilder.Entity<Attachment  >().HasNoKey();
            modelBuilder.Entity<OrdersCheckout>().HasNoKey(); 
            modelBuilder.Entity<ContactDetails>().HasNoKey();
            modelBuilder.Entity<BankDetails>().HasNoKey();
            modelBuilder.Entity<OrderRefundDto>().HasNoKey();
        }

        public DbSet<ContactUsModel> ContactUsDetails { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<WishList> WishList { get; set; }

        public DbSet<Categories> Categories { get; set; }

        public DbSet<OrderDeliveryDetail> OrderDeliveryDetails {  get; set; }
        public DbSet<OrderStatus> OrderStatus { get; set; }
        public DbSet<CanceledOrder> CanceledOrders { get; set; }
        public DbSet<ContactSetting> tbl_ContactSettings { get; set; }
        public DbSet<ContactInfo> tbl_ContactInfo { get; set; }
        public DbSet<ShippingAddress> ShippingAddress { get; set; }

        public DbSet<OrdersProduct> OrdersProduct { get; set; }
        public DbSet<ProductResponceModel> RecentProducts { get; set; }
        public DbSet<OrderRefund> OrderRefunds { get; set; }
        public DbSet<ErrorLog> ErrorLogs { get; set; }

    }
}
