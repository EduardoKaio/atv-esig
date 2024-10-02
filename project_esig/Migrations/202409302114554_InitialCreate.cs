namespace project_esig.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cargoes",
                c => new
                    {
                        Id = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        Nome = c.String(),
                        Salario = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Pessoas",
                c => new
                    {
                        Id = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        Nome = c.String(),
                        Cidade = c.String(),
                        Email = c.String(),
                        CEP = c.String(),
                        Endereco = c.String(),
                        Pais = c.String(),
                        Usuario = c.String(),
                        Telefone = c.String(),
                        Data_Nascimento = c.DateTime(nullable: false),
                        CargoId = c.Decimal(nullable: false, precision: 10, scale: 0),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PessoaSalarios",
                c => new
                    {
                        PessoaID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        Nome = c.String(),
                        Salario = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.PessoaID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PessoaSalarios");
            DropTable("dbo.Pessoas");
            DropTable("dbo.Cargoes");
        }
    }
}
