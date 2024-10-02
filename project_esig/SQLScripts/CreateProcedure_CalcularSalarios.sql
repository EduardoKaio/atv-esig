create or replace PROCEDURE CALCULARSALARIOS AS 
BEGIN
    -- Limpar a tabela antes de inserir novos dados
    DELETE FROM "pessoa_salario";

    -- Inserir novos dados
    INSERT INTO "pessoa_salario" ("PessoaID", "Nome", "Salario")
    SELECT p."Id", p."Nome", c."Salario"
    FROM "Pessoa" p
    JOIN "Cargo" c 
    ON p."CargoId" = c."Id";
    
    -- Confirmar as mudanças
    COMMIT;
END CALCULARSALARIOS;