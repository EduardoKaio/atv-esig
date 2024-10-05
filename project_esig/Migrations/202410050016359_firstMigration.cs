namespace project_esig.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class firstMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "C##KAIO.Cargo",
                c => new
                    {
                        Id = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        Nome = c.String(),
                        Salario = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "C##KAIO.Pessoa",
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
                        Data_Nascimento = c.DateTime(),
                        CargoId = c.Decimal(nullable: false, precision: 10, scale: 0),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "C##KAIO.pessoa_salario",
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
            DropTable("C##KAIO.pessoa_salario");
            DropTable("C##KAIO.Pessoa");
            DropTable("C##KAIO.Cargo");
        }
    }
}
