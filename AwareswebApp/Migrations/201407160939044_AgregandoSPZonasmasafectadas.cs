namespace AwareswebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AgregandoSPZonasmasafectadas : DbMigration
    {
        public override void Up()
        {
            CreateStoredProcedure(
                "dbo.Reporte_Insert",
                p => new
                    {
                        numReporteUsr = p.Int(),
                        userName = p.String(),
                        Descripcion = p.String(),
                        situacion = p.String(),
                        ubicacion = p.String(),
                        longitud = p.Double(),
                        latitud = p.Double(),
                        FotoUrl = p.String(),
                        Comentarios = p.String(),
                        estatus = p.String(),
                        fechaCreacion = p.DateTime(),
                        fechaCorreccion = p.DateTime(),
                    },
                body:
                    @"INSERT [dbo].[Reportes]([numReporteUsr], [userName], [Descripcion], [situacion], [ubicacion], [longitud], [latitud], [FotoUrl], [Comentarios], [estatus], [fechaCreacion], [fechaCorreccion])
                      VALUES (@numReporteUsr, @userName, @Descripcion, @situacion, @ubicacion, @longitud, @latitud, @FotoUrl, @Comentarios, @estatus, @fechaCreacion, @fechaCorreccion)
                      
                      DECLARE @numReporte int
                      SELECT @numReporte = [numReporte]
                      FROM [dbo].[Reportes]
                      WHERE @@ROWCOUNT > 0 AND [numReporte] = scope_identity()
                      
                      SELECT t0.[numReporte]
                      FROM [dbo].[Reportes] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[numReporte] = @numReporte"
            );
            
            CreateStoredProcedure(
                "dbo.Reporte_Update",
                p => new
                    {
                        numReporte = p.Int(),
                        numReporteUsr = p.Int(),
                        userName = p.String(),
                        Descripcion = p.String(),
                        situacion = p.String(),
                        ubicacion = p.String(),
                        longitud = p.Double(),
                        latitud = p.Double(),
                        FotoUrl = p.String(),
                        Comentarios = p.String(),
                        estatus = p.String(),
                        fechaCreacion = p.DateTime(),
                        fechaCorreccion = p.DateTime(),
                    },
                body:
                    @"UPDATE [dbo].[Reportes]
                      SET [numReporteUsr] = @numReporteUsr, [userName] = @userName, [Descripcion] = @Descripcion, [situacion] = @situacion, [ubicacion] = @ubicacion, [longitud] = @longitud, [latitud] = @latitud, [FotoUrl] = @FotoUrl, [Comentarios] = @Comentarios, [estatus] = @estatus, [fechaCreacion] = @fechaCreacion, [fechaCorreccion] = @fechaCorreccion
                      WHERE ([numReporte] = @numReporte)"
            );
            
            CreateStoredProcedure(
                "dbo.Reporte_Delete",
                p => new
                    {
                        numReporte = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[Reportes]
                      WHERE ([numReporte] = @numReporte)"
            );

            CreateStoredProcedure(
                "dbo.zonasmasfectadas",
                p => new
                {
                    sector = p.String(),
                },
                
                body:
                    @"SELECT ubicacion
	                from [dbo].[Reportes]
	                where estatus = 1
	                group by ubicacion
	                having count(*) >=1"
            );
            
        }
        
        public override void Down()
        {
            DropStoredProcedure("dbo.Reporte_Delete");
            DropStoredProcedure("dbo.Reporte_Update");
            DropStoredProcedure("dbo.Reporte_Insert");
        }
    }
}
