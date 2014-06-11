namespace AwareswebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cambiandoModeloReportes : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Reportes", "longitud", c => c.Double(nullable: false));
            AlterColumn("dbo.Reportes", "latitud", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Reportes", "latitud", c => c.String());
            AlterColumn("dbo.Reportes", "longitud", c => c.String());
        }
    }
}
