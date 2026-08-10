-- Tabela PRODUTO
IF OBJECT_ID(N'dbo.PRODUTO', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.PRODUTO (
        COD_PRODUTO     CHAR(4)     NOT NULL,
        DES_PRODUTO     VARCHAR(30) NULL,
        STA_STATUS      CHAR(1)     NULL,
        CONSTRAINT PK_PRODUTO PRIMARY KEY (COD_PRODUTO)
    );
    PRINT 'Tabela PRODUTO criada com sucesso.';
END
ELSE
BEGIN
    PRINT 'Tabela PRODUTO já existe.';
END
GO

-- Tabela PRODUTO_COSIF
IF OBJECT_ID(N'dbo.PRODUTO_COSIF', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.PRODUTO_COSIF (
        COD_PRODUTO         CHAR(4)     NOT NULL,
        COD_COSIF           CHAR(11)    NOT NULL,
        COD_CLASSIFICACAO   CHAR(6)     NULL,
        STA_STATUS          CHAR(1)     NULL,
        CONSTRAINT PK_PRODUTO_COSIF PRIMARY KEY (COD_PRODUTO, COD_COSIF),
        CONSTRAINT FK_PRODUTO_COSIF_PRODUTO 
            FOREIGN KEY (COD_PRODUTO) 
            REFERENCES dbo.PRODUTO (COD_PRODUTO)
    );
    PRINT 'Tabela PRODUTO_COSIF criada com sucesso.';
END
ELSE
BEGIN
    PRINT 'Tabela PRODUTO_COSIF já existe.';
END
GO

-- Tabela MOVIMENTO_MANUAL
IF OBJECT_ID(N'dbo.MOVIMENTO_MANUAL', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.MOVIMENTO_MANUAL (
        DAT_MES         NUMERIC(2, 0)   NOT NULL,
        DAT_ANO         NUMERIC(4, 0)   NOT NULL,
        NUM_LANCAMENTO  NUMERIC(18, 0)  NOT NULL,
        COD_PRODUTO     CHAR(4)         NOT NULL,
        COD_COSIF       CHAR(11)        NOT NULL,
        DES_DESCRICAO   VARCHAR(50)     NOT NULL,
        DAT_MOVIMENTO   SMALLDATETIME   NOT NULL,
        COD_USUARIO     VARCHAR(15)     NOT NULL,
        VAL_VALOR       NUMERIC(18, 2)  NOT NULL,
        CONSTRAINT PK_MOVIMENTO_MANUAL 
            PRIMARY KEY (DAT_MES, DAT_ANO, NUM_LANCAMENTO, COD_PRODUTO, COD_COSIF),
        CONSTRAINT FK_MOVIMENTO_MANUAL_PRODUTO_COSIF 
            FOREIGN KEY (COD_PRODUTO, COD_COSIF) 
            REFERENCES dbo.PRODUTO_COSIF (COD_PRODUTO, COD_COSIF)
    );
    PRINT 'Tabela MOVIMENTO_MANUAL criada com sucesso.';
END
ELSE
BEGIN
    PRINT 'Tabela MOVIMENTO_MANUAL já existe.';
END
GO

-- Procedure usp_ListarMovimentosManuais
CREATE OR ALTER PROCEDURE dbo.usp_ListarMovimentosManuais
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        MM.DAT_MES          AS Mes,
        MM.DAT_ANO          AS Ano,
        MM.COD_PRODUTO      AS CodigoProduto,
        P.DES_PRODUTO       AS DescricaoProduto,
        MM.NUM_LANCAMENTO   AS NumeroLancamento,
        MM.DES_DESCRICAO    AS DescricaoMovimentoManual,
        MM.VAL_VALOR        AS Valor
    FROM 
        dbo.MOVIMENTO_MANUAL AS MM
        INNER JOIN dbo.PRODUTO AS P
            ON MM.COD_PRODUTO = P.COD_PRODUTO
    ORDER BY 
        MM.DAT_MES,
        MM.DAT_ANO,
        MM.NUM_LANCAMENTO;
END
GO

-- Bloco de inserções
INSERT INTO dbo.PRODUTO
(
    COD_PRODUTO,
    DES_PRODUTO,
    STA_STATUS
)
VALUES
    ('0001', 'Conta Corrente',       'A'),
    ('0002', 'Conta Poupanca',       'A'),
    ('0003', 'Emprestimo Pessoal',   'A'),
    ('0004', 'Financiamento',        'A'),
    ('0005', 'Aplicacao Financeira', 'A');

INSERT INTO dbo.PRODUTO_COSIF
(
    COD_PRODUTO,
    COD_COSIF,
    COD_CLASSIFICACAO,
    STA_STATUS
)
VALUES

    -- Para vermos mais diversificação na camada de UI, cada produto tem mais que 1 código Cosif ativo
    -- Produto 0001 - Conta Corrente
    ('0001', '11100000001', 'NORMAL', 'A'),
    ('0001', '41100000001', 'NORMAL', 'A'),
    ('0001', '41100000002', 'NORMAL', 'A'),

    -- Produto 0002 - Conta Poupanca
    ('0002', '41100000003', 'NORMAL', 'A'),
    ('0002', '41100000004', 'NORMAL', 'A'),

    -- Produto 0003 - Emprestimo Pessoal
    ('0003', '13100000001', 'NORMAL', 'A'),
    ('0003', '13100000002', 'NORMAL', 'A'),
    ('0003', '13100000003', 'NORMAL', 'A'),

    -- Produto 0004 - Financiamento
    ('0004', '13200000001', 'NORMAL', 'A'),
    ('0004', '13200000002', 'NORMAL', 'A'),

    -- Produto 0005 - Aplicacao Financeira
    ('0005', '12100000001', 'MTM',    'A'),
    ('0005', '12100000002', 'MTM',    'A');
