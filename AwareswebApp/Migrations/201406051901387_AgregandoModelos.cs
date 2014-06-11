namespace AwareswebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AgregandoModelos : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Colaboradors",
                c => new
                    {
                        idColaborador = c.Int(nullable: false, identity: true),
                        nombreUsuario = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                        sector = c.String(),
                        localidad = c.String(),
                        tipoUsuario = c.String(),
                        fechaCreacion = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.idColaborador);
            
            CreateTable(
                "dbo.Reportes",
                c => new
                    {
                        numReporte = c.Int(nullable: false, identity: true),
                        numReporteUsr = c.Int(nullable: false),
                        idUsuario = c.Int(nullable: false),
                        Descripcion = c.String(),
                        situacion = c.String(),
                        ubicacion = c.String(),
                        longitud = c.String(),
                        latitud = c.String(),
                        FotoUrl = c.String(),
                        Comentarios = c.String(),
                        estatus = c.String(),
                        fechaCreacion = c.DateTime(nullable: false),
                        fechaCorreccion = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.numReporte);
            
            CreateTable(
                "dbo.Rutas",
                c => new
                    {
                        numRuta = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(),
                        idLugar = c.Int(nullable: false),
                        idUsuario = c.Int(nullable: false),
                        numReporte = c.Int(nullable: false),
                        situacion = c.String(),
                        ubicacion = c.String(),
                        longitud = c.String(),
                        latitud = c.String(),
                        estatusReporte = c.String(),
                        fechaCreacion = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.numRuta);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Rutas");
            DropTable("dbo.Reportes");
            DropTable("dbo.Colaboradors");
        }
    }
}
