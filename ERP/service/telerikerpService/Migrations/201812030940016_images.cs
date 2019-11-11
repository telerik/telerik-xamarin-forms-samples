namespace telerikerpService.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class images : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Customers", "ImageID", "dbo.Images");
            DropForeignKey("dbo.Products", "ImageID", "dbo.Images");
            DropForeignKey("dbo.Vendors", "ImageID", "dbo.Images");
            DropIndex("dbo.Customers", new[] { "ImageID" });
            DropIndex("dbo.Images", new[] { "CreatedAt" });
            DropIndex("dbo.Products", new[] { "ImageID" });
            DropIndex("dbo.Vendors", new[] { "ImageID" });
            AddColumn("dbo.Customers", "Image", c => c.String());
            AddColumn("dbo.Products", "Image", c => c.String());
            AddColumn("dbo.Vendors", "Image", c => c.String());
            DropColumn("dbo.Customers", "ImageID");
            DropColumn("dbo.Products", "ImageID");
            DropColumn("dbo.Vendors", "ImageID");
            DropTable("dbo.Images",
                removedColumnAnnotations: new Dictionary<string, IDictionary<string, object>>
                {
                    {
                        "CreatedAt",
                        new Dictionary<string, object>
                        {
                            { "ServiceTableColumn", "CreatedAt" },
                        }
                    },
                    {
                        "Deleted",
                        new Dictionary<string, object>
                        {
                            { "ServiceTableColumn", "Deleted" },
                        }
                    },
                    {
                        "Id",
                        new Dictionary<string, object>
                        {
                            { "ServiceTableColumn", "Id" },
                        }
                    },
                    {
                        "UpdatedAt",
                        new Dictionary<string, object>
                        {
                            { "ServiceTableColumn", "UpdatedAt" },
                        }
                    },
                    {
                        "Version",
                        new Dictionary<string, object>
                        {
                            { "ServiceTableColumn", "Version" },
                        }
                    },
                });
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128,
                            annotations: new Dictionary<string, AnnotationValues>
                            {
                                { 
                                    "ServiceTableColumn",
                                    new AnnotationValues(oldValue: null, newValue: "Id")
                                },
                            }),
                        Content = c.Binary(),
                        Filename = c.String(),
                        Width = c.Single(nullable: false),
                        Height = c.Single(nullable: false),
                        Version = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion",
                            annotations: new Dictionary<string, AnnotationValues>
                            {
                                { 
                                    "ServiceTableColumn",
                                    new AnnotationValues(oldValue: null, newValue: "Version")
                                },
                            }),
                        CreatedAt = c.DateTimeOffset(nullable: false, precision: 7,
                            annotations: new Dictionary<string, AnnotationValues>
                            {
                                { 
                                    "ServiceTableColumn",
                                    new AnnotationValues(oldValue: null, newValue: "CreatedAt")
                                },
                            }),
                        UpdatedAt = c.DateTimeOffset(precision: 7,
                            annotations: new Dictionary<string, AnnotationValues>
                            {
                                { 
                                    "ServiceTableColumn",
                                    new AnnotationValues(oldValue: null, newValue: "UpdatedAt")
                                },
                            }),
                        Deleted = c.Boolean(nullable: false,
                            annotations: new Dictionary<string, AnnotationValues>
                            {
                                { 
                                    "ServiceTableColumn",
                                    new AnnotationValues(oldValue: null, newValue: "Deleted")
                                },
                            }),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Vendors", "ImageID", c => c.String(maxLength: 128));
            AddColumn("dbo.Products", "ImageID", c => c.String(maxLength: 128));
            AddColumn("dbo.Customers", "ImageID", c => c.String(maxLength: 128));
            DropColumn("dbo.Vendors", "Image");
            DropColumn("dbo.Products", "Image");
            DropColumn("dbo.Customers", "Image");
            CreateIndex("dbo.Vendors", "ImageID");
            CreateIndex("dbo.Products", "ImageID");
            CreateIndex("dbo.Images", "CreatedAt", clustered: true);
            CreateIndex("dbo.Customers", "ImageID");
            AddForeignKey("dbo.Vendors", "ImageID", "dbo.Images", "Id");
            AddForeignKey("dbo.Products", "ImageID", "dbo.Images", "Id");
            AddForeignKey("dbo.Customers", "ImageID", "dbo.Images", "Id");
        }
    }
}
