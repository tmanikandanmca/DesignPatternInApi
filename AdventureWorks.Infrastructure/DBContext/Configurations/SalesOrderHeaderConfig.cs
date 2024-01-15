using AdventureWorks.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdventureWorks.Infrastructure.DBContext.Configurations;

internal class SalesOrderHeaderConfig : IEntityTypeConfiguration<SalesOrderHeader>
{
    public void Configure(EntityTypeBuilder<SalesOrderHeader> entity)
    {
        entity.HasKey(e => e.SalesOrderID).HasName("PK_SalesOrderHeader_SalesOrderID");

        entity.ToTable("SalesOrderHeader", "Sales", tb =>
        {
            tb.HasComment("General sales order information.");
            tb.HasTrigger("uSalesOrderHeader");
        });

        entity.HasIndex(e => e.SalesOrderNumber, "AK_SalesOrderHeader_SalesOrderNumber").IsUnique();

        entity.HasIndex(e => e.rowguid, "AK_SalesOrderHeader_rowguid").IsUnique();

        entity.HasIndex(e => e.CustomerID, "IX_SalesOrderHeader_CustomerID");

        entity.HasIndex(e => e.SalesPersonID, "IX_SalesOrderHeader_SalesPersonID");

        entity.Property(e => e.SalesOrderID).HasComment("Primary key.");
        entity.Property(e => e.AccountNumber)
            .HasMaxLength(15)
            .HasComment("Financial accounting number reference.");
        entity.Property(e => e.BillToAddressID).HasComment("Customer billing address. Foreign key to Address.AddressID.");
        entity.Property(e => e.Comment)
            .HasMaxLength(128)
            .HasComment("Sales representative comments.");
        entity.Property(e => e.CreditCardApprovalCode)
            .HasMaxLength(15)
            .IsUnicode(false)
            .HasComment("Approval code provided by the credit card company.");
        entity.Property(e => e.CreditCardID).HasComment("Credit card identification number. Foreign key to CreditCard.CreditCardID.");
        entity.Property(e => e.CurrencyRateID).HasComment("Currency exchange rate used. Foreign key to CurrencyRate.CurrencyRateID.");
        entity.Property(e => e.CustomerID).HasComment("Customer identification number. Foreign key to Customer.BusinessEntityID.");
        entity.Property(e => e.DueDate)
            .HasComment("Date the order is due to the customer.")
            .HasColumnType("datetime");
        entity.Property(e => e.Freight)
            .HasComment("Shipping cost.")
            .HasColumnType("money");
        entity.Property(e => e.ModifiedDate)
            .HasDefaultValueSql("(getdate())")
            .HasComment("Date and time the record was last updated.")
            .HasColumnType("datetime");
        entity.Property(e => e.OnlineOrderFlag)
            .HasDefaultValue(true)
            .HasComment("0 = Order placed by sales person. 1 = Order placed online by customer.");
        entity.Property(e => e.OrderDate)
            .HasDefaultValueSql("(getdate())")
            .HasComment("Dates the sales order was created.")
            .HasColumnType("datetime");
        entity.Property(e => e.PurchaseOrderNumber)
            .HasMaxLength(25)
            .HasComment("Customer purchase order number reference. ");
        entity.Property(e => e.RevisionNumber).HasComment("Incremental number to track changes to the sales order over time.");
        entity.Property(e => e.SalesOrderNumber)
            .HasMaxLength(25)
            .HasComputedColumnSql("(isnull(N'SO'+CONVERT([nvarchar](23),[SalesOrderID]),N'*** ERROR ***'))", false)
            .HasComment("Unique sales order identification number.");
        entity.Property(e => e.SalesPersonID).HasComment("Sales person who created the sales order. Foreign key to SalesPerson.BusinessEntityID.");
        entity.Property(e => e.ShipDate)
            .HasComment("Date the order was shipped to the customer.")
            .HasColumnType("datetime");
        entity.Property(e => e.ShipMethodID).HasComment("Shipping method. Foreign key to ShipMethod.ShipMethodID.");
        entity.Property(e => e.ShipToAddressID).HasComment("Customer shipping address. Foreign key to Address.AddressID.");
        entity.Property(e => e.Status)
            .HasDefaultValue((byte)1)
            .HasComment("Order current status. 1 = In process; 2 = Approved; 3 = Backordered; 4 = Rejected; 5 = Shipped; 6 = Cancelled");
        entity.Property(e => e.SubTotal)
            .HasComment("Sales subtotal. Computed as SUM(SalesOrderDetail.LineTotal)for the appropriate SalesOrderID.")
            .HasColumnType("money");
        entity.Property(e => e.TaxAmt)
            .HasComment("Tax amount.")
            .HasColumnType("money");
        entity.Property(e => e.TerritoryID).HasComment("Territory in which the sale was made. Foreign key to SalesTerritory.SalesTerritoryID.");
        entity.Property(e => e.TotalDue)
            .HasComputedColumnSql("(isnull(([SubTotal]+[TaxAmt])+[Freight],(0)))", false)
            .HasComment("Total due from customer. Computed as Subtotal + TaxAmt + Freight.")
            .HasColumnType("money");
        entity.Property(e => e.rowguid)
            .HasDefaultValueSql("(newid())")
            .HasComment("ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.");

        entity.HasOne(d => d.BillToAddress).WithMany(p => p.SalesOrderHeaderBillToAddresses)
            .HasForeignKey(d => d.BillToAddressID)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.HasOne(d => d.CreditCard).WithMany(p => p.SalesOrderHeaders).HasForeignKey(d => d.CreditCardID);

        entity.HasOne(d => d.CurrencyRate).WithMany(p => p.SalesOrderHeaders).HasForeignKey(d => d.CurrencyRateID);

        entity.HasOne(d => d.Customer).WithMany(p => p.SalesOrderHeaders)
            .HasForeignKey(d => d.CustomerID)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.HasOne(d => d.SalesPerson).WithMany(p => p.SalesOrderHeaders).HasForeignKey(d => d.SalesPersonID);

        entity.HasOne(d => d.ShipMethod).WithMany(p => p.SalesOrderHeaders)
            .HasForeignKey(d => d.ShipMethodID)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.HasOne(d => d.ShipToAddress).WithMany(p => p.SalesOrderHeaderShipToAddresses)
            .HasForeignKey(d => d.ShipToAddressID)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.HasOne(d => d.Territory).WithMany(p => p.SalesOrderHeaders).HasForeignKey(d => d.TerritoryID);

    }
}
