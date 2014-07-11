namespace AwareswebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cambiosConsumosModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Consumoes", "UsernameColaborador", c => c.String());
            DropColumn("dbo.Consumoes", "idColaborador");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Consumoes", "idColaborador", c => c.Int(nullable: false));
            DropColumn("dbo.Consumoes", "UsernameColaborador");
        }
    }
}
