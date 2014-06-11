namespace AwareswebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Consumoes",
                c => new
                    {
                        idConsumo = c.Int(nullable: false, identity: true),
                        idColaborador = c.Int(nullable: false),
                        tipoConsumo = c.String(),
                        lectura = c.String(),
                        fechaCreacion = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.idConsumo);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Consumoes");
        }
    }
}
