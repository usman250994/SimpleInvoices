
using Microsoft.EntityFrameworkCore;

namespace SimpleInvoices {
    public class InvoiceContext :DbContext{

        public InvoiceContext(DbContextOptions<InvoiceContext> dbContext):base(dbContext){
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
             modelBuilder.Entity<LedgerDetails>().HasKey(x => new { x.ledgersId, x.productId });
        }
       
        public DbSet<FieldValue> FieldValues {get;set;}
        public DbSet <SimpleInvoices.Users> users {get;set;}
        public DbSet <product> products {get;set;}
        public DbSet <Design> design {get;set;}
        public DbSet <Customer> customer {get;set;}
        public DbSet<Billers> biller{get;set;} 
        public DbSet<Ledgers> ledgers {get;set;}
        public DbSet<Payment> payment {get;set;}
        public DbSet<LedgerDetails> ledgerDetails{get;set;}
        public DbSet<PaymentTypes> paymentTypes {get;set;}
        public DbSet<Taxes> taxes {get;set;}
        public DbSet<CustomFields> customFields {get;set;}
    }
    
}