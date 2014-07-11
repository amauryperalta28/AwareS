namespace AwareswebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cambiosReportesModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reportes", "userName", c => c.String());
            DropColumn("dbo.Reportes", "idUsuario");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Reportes", "idUsuario", c => c.Int(nullable: false));
            DropColumn("dbo.Reportes", "userName");
        }
    }
}
