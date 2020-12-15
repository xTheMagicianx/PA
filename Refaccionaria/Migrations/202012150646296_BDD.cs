namespace Refaccionaria.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BDD : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categorias",
                c => new
                    {
                        CategoriaID = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 50),
                        Descripcion = c.String(nullable: false, maxLength: 300),
                    })
                .PrimaryKey(t => t.CategoriaID);
            
            CreateTable(
                "dbo.Clientes",
                c => new
                    {
                        ClienteID = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 100, unicode: false),
                        Telefono = c.String(nullable: false),
                        Email = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ClienteID);
            
            CreateTable(
                "dbo.ProductoCarrito",
                c => new
                    {
                        ProductoCarritoID = c.Int(nullable: false, identity: true),
                        ProductoID = c.Int(nullable: false),
                        Cantidad = c.Int(nullable: false),
                        PrecioUnitario = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SubTotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ProductoCarritoID)
                .ForeignKey("dbo.Productos", t => t.ProductoID, cascadeDelete: true)
                .Index(t => t.ProductoID);
            
            CreateTable(
                "dbo.Productos",
                c => new
                    {
                        ProductoID = c.Int(nullable: false, identity: true),
                        CategoriaID = c.Int(nullable: false),
                        ProveedorID = c.Int(nullable: false),
                        Nombre = c.String(nullable: false, maxLength: 50, unicode: false),
                        Descripcion = c.String(nullable: false, maxLength: 300, unicode: false),
                        PrecioCompra = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PrecioVenta = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ProductoID)
                .ForeignKey("dbo.Categorias", t => t.CategoriaID, cascadeDelete: true)
                .ForeignKey("dbo.Proveedores", t => t.ProveedorID, cascadeDelete: true)
                .Index(t => t.CategoriaID)
                .Index(t => t.ProveedorID);
            
            CreateTable(
                "dbo.Proveedores",
                c => new
                    {
                        ProveedorID = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 100, unicode: false),
                        Telefono = c.String(nullable: false),
                        Email = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ProveedorID);
            
            CreateTable(
                "dbo.Ventas",
                c => new
                    {
                        VentaID = c.Int(nullable: false, identity: true),
                        ClienteID = c.Int(nullable: false),
                        ProductoID = c.Int(nullable: false),
                        Fecha = c.DateTime(nullable: false),
                        Cantidad = c.Int(nullable: false),
                        PrecioUnitario = c.Decimal(nullable: false, storeType: "money"),
                        Total = c.Decimal(nullable: false, storeType: "money"),
                    })
                .PrimaryKey(t => t.VentaID)
                .ForeignKey("dbo.Clientes", t => t.ClienteID, cascadeDelete: true)
                .ForeignKey("dbo.Productos", t => t.ProductoID, cascadeDelete: true)
                .Index(t => t.ClienteID)
                .Index(t => t.ProductoID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Ventas", "ProductoID", "dbo.Productos");
            DropForeignKey("dbo.Ventas", "ClienteID", "dbo.Clientes");
            DropForeignKey("dbo.ProductoCarrito", "ProductoID", "dbo.Productos");
            DropForeignKey("dbo.Productos", "ProveedorID", "dbo.Proveedores");
            DropForeignKey("dbo.Productos", "CategoriaID", "dbo.Categorias");
            DropIndex("dbo.Ventas", new[] { "ProductoID" });
            DropIndex("dbo.Ventas", new[] { "ClienteID" });
            DropIndex("dbo.Productos", new[] { "ProveedorID" });
            DropIndex("dbo.Productos", new[] { "CategoriaID" });
            DropIndex("dbo.ProductoCarrito", new[] { "ProductoID" });
            DropTable("dbo.Ventas");
            DropTable("dbo.Proveedores");
            DropTable("dbo.Productos");
            DropTable("dbo.ProductoCarrito");
            DropTable("dbo.Clientes");
            DropTable("dbo.Categorias");
        }
    }
}
