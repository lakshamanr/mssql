namespace MSSQL.DIARY.COMN.Constant
{
    public static partial class SqlQueryConstant
    {


        public static readonly string GetAllExtendedPropertiesofTheTable = @" 
                        SELECT  
                        sep.name AS [Name],
                        sep.value AS [Value],
                        (SCHEMA_NAME(t.schema_id)+'.'+t.name) as [Table]
                        FROM 
                        sys.tables t
                        INNER JOIN 
                        sys.extended_properties sep ON t.object_id = sep.major_id
                        WHERE 
                        sep.class_desc = 'OBJECT_OR_COLUMN' -- Filter for objects (tables)
                        AND t.name = @TableName
                        AND SCHEMA_NAME(t.schema_id) = @SchemaName
                        AND   minor_id=0 ";

        public static readonly string GetAllTablesExtendedProperties = @" 
                        SELECT  
                        sep.name AS [Name],
                        sep.value AS [Value]
                        FROM 
                        sys.tables t
                        INNER JOIN 
                        sys.extended_properties sep ON t.object_id = sep.major_id
                        WHERE 
                        sep.class_desc = 'OBJECT_OR_COLUMN' -- Filter for objects (tables)
                        
                        AND   minor_id=0 ";


        public static readonly string GetTableProperties = @"SELECT Property as Name, Value
                        FROM (
                        SELECT 
                        'TableName' AS Property, 
                        t.name AS Value,
                        1 AS OrderBy
                        FROM 
                        sys.tables t
                        INNER JOIN 
                        sys.schemas s ON t.schema_id = s.schema_id
                        WHERE 
                        t.name = @TableName AND s.name = @SchemaName
    
                        UNION ALL
    
                        SELECT 
                        'SchemaName', 
                        s.name,
                        2
                        FROM 
                        sys.tables t
                        INNER JOIN 
                        sys.schemas s ON t.schema_id = s.schema_id
                        WHERE 
                        t.name = @TableName AND s.name = @SchemaName
    
                        UNION ALL
    
                        SELECT 
                        'Created', 
                        CONVERT(NVARCHAR, t.create_date, 120),
                        3
                        FROM 
                        sys.tables t
                        INNER JOIN 
                        sys.schemas s ON t.schema_id = s.schema_id
                        WHERE 
                        t.name = @TableName AND s.name = @SchemaName
    
                        UNION ALL
    
                        SELECT 
                        'LastModified', 
                        CONVERT(NVARCHAR, t.modify_date, 120),
                        4
                        FROM 
                        sys.tables t
                        INNER JOIN 
                        sys.schemas s ON t.schema_id = s.schema_id
                        WHERE 
                        t.name = @TableName AND s.name = @SchemaName
    
                        UNION ALL
    
                        SELECT 
                        'RowCount', 
                        CONVERT(NVARCHAR, p.rows),
                        5
                        FROM 
                        sys.tables t
                        INNER JOIN 
                        sys.schemas s ON t.schema_id = s.schema_id
                        INNER JOIN 
                        sys.partitions p ON t.object_id = p.object_id
                        WHERE 
                        t.name = @TableName AND s.name = @SchemaName AND p.index_id IN (0, 1)
                        GROUP BY 
                        t.name, s.name, t.create_date, t.modify_date, p.rows
                        ) AS TableInfo
                        ORDER BY TableInfo.OrderBy;";



        /**/


        public static readonly string GetTableCreateScript = @"
                        DECLARE @object_name SYSNAME,       
                        @object_id   INT;  
  
                        SELECT 
                        @object_name = '[' + s.NAME + '].[' + o.NAME + ']',   
                        @object_id = o.[object_id]
                        FROM   sys.objects o WITH (NOWAIT)  
                        JOIN sys.schemas s WITH (NOWAIT)    
                        ON o.[schema_id] = s.[schema_id] 
  
                        WHERE  
                        s.NAME + '.' + o.NAME = @tableName    
                        AND o.[type] = 'U'       
                        AND o.is_ms_shipped = 0; 
  
                        DECLARE @SQL NVARCHAR(MAX) = ''; 
  
                        WITH index_column      AS 
                        (SELECT 				ic.[object_id],      
                        ic.index_id,              
                        ic.is_descending_key,            
                        ic.is_included_column,            
                        c.NAME          FROM   
                        sys.index_columns ic WITH (NOWAIT)     
                        JOIN sys.columns c WITH (NOWAIT)     
                        ON ic.[object_id] = c.[object_id]      
                        AND ic.column_id = c.column_id    
                        WHERE  ic.[object_id] = @object_id),     
                        fk_columns      AS 
                        (    
                        SELECT k.constraint_object_id,    
                        cname = c.NAME,        
                        rcname = rc.NAME    
                        FROM   sys.foreign_key_columns k WITH (NOWAIT)   
                        JOIN sys.columns rc WITH (NOWAIT)     
                        ON rc.[object_id] = k.referenced_object_id    
                        AND rc.column_id = k.referenced_column_id     
                        JOIN sys.columns c WITH (NOWAIT)              
                        ON c.[object_id] = k.parent_object_id        
                        AND c.column_id = k.parent_column_id      
                        WHERE  k.parent_object_id = @object_id)
                        SELECT @SQL = 
                        'CREATE TABLE ' + @object_name + CHAR(13) + '('          
                        + CHAR(13)               + STUFF(( SELECT CHAR(9) + ', [' + c.NAME + '] ' + CASE WHEN                      c.is_computed = 1 THEN 'AS ' + cc.[definition] ELSE UPPER(               tp.NAME)                      + CASE WHEN                      tp.NAME IN ('varchar', 'char', 'varbinary', 'binary',               'text') THEN                      '(' + CASE WHEN c.max_length = -1 THEN 'MAX' ELSE                      CAST(c.max_length AS VARCHAR(5)) END + ')' WHEN tp.NAME IN               (                      'nvarchar', 'nchar',                      'ntext') THEN '(' + CASE WHEN c.max_length = -1 THEN 'MAX'               ELSE                      CAST(c.max_length / 2 AS VARCHAR(5)) END + ')' WHEN tp.NAME               IN (                      'datetime2', 'time2', 'datetimeoffset') THEN '(' +               CAST(c.scale AS                      VARCHAR(5)) + ')' WHEN tp.NAME = 'decimal' THEN '(' +                      CAST(c.[precision] AS VARCHAR(5)) + ',' + CAST(c.scale AS               VARCHAR(                      5)) + ')' ELSE                      '' END + CASE WHEN c.collation_name IS NOT NULL THEN               ' COLLATE ' +                      c.collation_name ELSE '' END + CASE WHEN c.is_nullable = 1               THEN                      ' NULL' ELSE ' NOT NULL' END + CASE WHEN dc.[definition] IS               NOT                      NULL THEN ' DEFAULT' + dc.[definition] ELSE '' END + CASE               WHEN                      ic.is_identity = 1 THEN ' IDENTITY(' +               CAST(ISNULL(ic.seed_value,                      '0') AS CHAR(                      1)) + ',' + CAST(ISNULL(ic.increment_value, '1') AS CHAR(1)               ) + ')'                      ELSE '' END END + CHAR(13) FROM sys.columns c WITH (NOWAIT)               JOIN                      sys.types tp WITH (NOWAIT) ON c.user_type_id =               tp.user_type_id                      LEFT JOIN sys.computed_columns cc WITH (NOWAIT) ON               c.[object_id] =                      cc.[object_id] AND c.column_id = cc.column_id LEFT JOIN                      sys.default_constraints dc WITH (NOWAIT) ON               c.default_object_id !=                      0 AND c.[object_id]                      = dc.parent_object_id AND c.column_id = dc.parent_column_id               LEFT                      JOIN sys.identity_columns ic WITH (NOWAIT) ON c.is_identity               = 1                      AND c.[object_id] = ic.[object_id] AND c.column_id =               ic.column_id                      WHERE c.[object_id] = @object_id ORDER BY c.column_id FOR               XML PATH                      (''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 2, CHAR(9) +               ' ')               + ISNULL((SELECT CHAR(9) + ', CONSTRAINT [' + k.NAME +               '] PRIMARY KEY ('                      + (SELECT STUFF(( SELECT ', [' + c.NAME + '] ' + CASE WHEN                      ic.is_descending_key = 1 THEN 'DESC' ELSE 'ASC' END FROM                      sys.index_columns ic WITH (                      NOWAIT) JOIN sys.columns c WITH (NOWAIT) ON c.[object_id] =                      ic.[object_id] AND c.column_id = ic.column_id WHERE                      ic.is_included_column = 0                      AND ic.[object_id] = k.parent_object_id AND ic.index_id =                      k.unique_index_id FOR XML PATH(N''), TYPE).value('.',                      'NVARCHAR(MAX)'), 1, 2, ''))                      + ')' + CHAR(13) FROM sys.key_constraints k WITH (NOWAIT)               WHERE                      k.parent_object_id = @object_id AND k.[type] = 'PK'), '')               + ')' + CHAR(13) + ISNULL((SELECT ( SELECT CHAR(13) +               'ALTER TABLE ' +                      @object_name + ' WITH' + CASE WHEN fk.is_not_trusted = 1               THEN                      ' NOCHECK' ELSE ' CHECK' END + ' ADD CONSTRAINT [' +               fk.NAME +                      '] FOREIGN KEY(' + STUFF(( SELECT ', [' + k.cname + ']'               FROM                      fk_columns k WHERE k.constraint_object_id = fk.[object_id]               FOR XML                      PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 2, '') +               ')' +                      ' REFERENCES [' + SCHEMA_NAME(ro.[schema_id]) + '].[' +               ro.NAME +                      '] (' + STUFF(( SELECT ', [' + k.rcname + ']' FROM               fk_columns k                      WHERE k.constraint_object_id = fk.[object_id] FOR XML PATH(               ''),                      TYPE).value('.', 'NVARCHAR(MAX)'), 1, 2, '') + ')' + CASE               WHEN                      fk.delete_referential_action = 1 THEN ' ON DELETE CASCADE'               WHEN                      fk.delete_referential_action                      = 2 THEN ' ON DELETE SET NULL' WHEN               fk.delete_referential_action =                      3 THEN ' ON DELETE SET DEFAULT' ELSE '' END + CASE WHEN                      fk.update_referential_action = 1 THEN ' ON UPDATE CASCADE'               WHEN                      fk.update_referential_action                      = 2 THEN ' ON UPDATE SET NULL' WHEN               fk.update_referential_action =                      3 THEN ' ON UPDATE SET DEFAULT' ELSE '' END + CHAR(13) +                      'ALTER TABLE ' + @object_name + ' CHECK CONSTRAINT [' +               fk.NAME +                      ']' + CHAR(13) FROM sys.foreign_keys fk WITH (NOWAIT) JOIN                      sys.objects ro WITH (NOWAIT) ON ro.[object_id] =                      fk.referenced_object_id                      WHERE fk.parent_object_id = @object_id FOR XML PATH(N''),                      TYPE).value('.', 'NVARCHAR(MAX)')), '')               + ISNULL(((SELECT CHAR(13) + 'CREATE' + CASE WHEN i.is_unique = 1               THEN                      ' UNIQUE' ELSE '' END + ' NONCLUSTERED INDEX [' + i.NAME +               '] ON '                      + @object_name + ' (' + STUFF(( SELECT ', [' + c.NAME + ']'               + CASE                      WHEN c.is_descending_key = 1 THEN ' DESC' ELSE ' ASC' END               FROM                      index_column c WHERE c.is_included_column = 0 AND               c.index_id =                      i.index_id FOR XML PATH(''), TYPE).value('.',               'NVARCHAR(MAX)'), 1,                      2, '') +                      ')' + ISNULL(CHAR(13) + 'INCLUDE (' + STUFF(( SELECT ', ['               +                      c.NAME + ']' FROM index_column c WHERE c.is_included_column               = 1                      AND                      c.index_id = i.index_id FOR XML PATH(''), TYPE).value('.',                      'NVARCHAR(MAX)'),                      1, 2, '') + ')', '') + CHAR(13) FROM sys.indexes i WITH (               NOWAIT)                      WHERE i.[object_id] = @object_id AND i.is_primary_key = 0               AND                      i.[type] = 2 FOR XML PATH(''), TYPE).value('.',               'NVARCHAR(MAX)') )     
                        , ''); 
				 
                        SELECT @SQL; ";


        public static readonly string GetAllTablesColumn =
        @"
SELECT 
    TRY_CAST((colm.table_schema + '.' + colm.table_name) AS VARCHAR(100)) AS TableName,
    TRY_CAST(colm.column_name AS VARCHAR(100)) AS columnName,
    CASE i.is_primary_key
        WHEN 1 THEN 'Yes'
        ELSE 'No'
    END AS [Key],
    CASE idc.is_identity
        WHEN 1 THEN 'Yes'
        ELSE 'No'
    END AS [Identity],
    TRY_CAST(colm.data_type AS VARCHAR(100)) AS [DataType],
    ISNULL(TRY_CAST(colm.character_maximum_length AS VARCHAR(100)) ,'')AS [MaxLength],
    ISNULL(TRY_CAST(colm.is_nullable AS VARCHAR(100)), 'Unknown') AS [AllowNulls],
    ISNULL(TRY_CAST(colm.column_default AS VARCHAR(100)), 'None') AS [Default],
    (SELECT VALUE FROM ::fn_listextendedproperty ('MS_Description', 'schema', colm.table_schema, 'table', colm.table_name, 'column', colm.column_name)) AS [Description]
FROM sys.tables t
LEFT JOIN sys.columns c ON c.OBJECT_ID = t.OBJECT_ID
LEFT JOIN sys.identity_columns idc ON idc.OBJECT_ID = t.OBJECT_ID AND idc.column_id = c.column_id AND idc.is_identity = 1
LEFT JOIN sys.index_columns ic ON ic.OBJECT_ID = t.OBJECT_ID AND ic.column_id = c.column_id
LEFT JOIN sys.indexes i ON i.OBJECT_ID = t.OBJECT_ID AND i.index_id = ic.index_id AND i.is_primary_key = 1
INNER JOIN information_schema.columns colm ON colm.table_name = t.NAME
WHERE t.TYPE = 'U'
    AND (idc.is_identity = 1 OR i.is_primary_key = 1 OR c.NAME = 'ID')
    AND colm.table_schema + '.' + colm.table_name = @tblName;
        "; 
        public static readonly string GetAllTableDescriptionWithAll =
        @" SELECT     t.Name , SCHEMA_NAME(schema_id)+'.'+t.name, sep.value ,SCHEMA_NAME(schema_id)   FROM     sys.tables t INNER JOIN     sys.extended_properties sep ON t.object_id = sep.major_id where     sep.Name = @ExtendedProp    AND sep.minor_id = 0     ";

        public static readonly string GetTableIndex = @"
                        SELECT i.[name] AS IndexName,
                        SUBSTRING(column_names, 1, LEN(column_names) - 1) AS [Columns], 
                        CASE WHEN i.[type] = 1 THEN 'Clustered index'
                        WHEN i.[type] = 2 THEN 'Nonclustered unique index'
                        WHEN i.[type] = 3 THEN 'XML index'
                        WHEN i.[type] = 4 THEN 'Spatial index'
                        WHEN i.[type] = 5 THEN 'Clustered columnstore index'
                        WHEN i.[type] = 6 THEN 'Nonclustered columnstore index'
                        WHEN i.[type] = 7 THEN 'Nonclustered hash index'
                        END AS IndexType, 
                        CASE WHEN i.is_unique = 1 THEN 'Unique' ELSE 'Not unique' END AS [Unique], 
                        SCHEMA_NAME(t.schema_id) + '.' + t.[name] AS TableView, 
                        CASE WHEN t.[type] = 'U' THEN 'Table' WHEN t.[type] = 'V' THEN 'View' END AS [ObjectType]
                        FROM sys.objects t 
                        INNER JOIN sys.indexes i ON t.object_id = i.object_id 
                        CROSS APPLY (SELECT col.[name] + ', ' 
                        FROM sys.index_columns ic 
                        INNER JOIN sys.columns col ON ic.object_id = col.object_id AND ic.column_id = col.column_id 
                        WHERE ic.object_id = t.object_id AND ic.index_id = i.index_id 
                        ORDER BY col.column_id 
                        FOR XML PATH('')) D (column_names)
                        WHERE t.is_ms_shipped <> 1 
                        AND SCHEMA_NAME(t.schema_id) + '.' + t.[name] = @tblName 
                        AND index_id > 0
                        ORDER BY i.[name]          
                        ";
        public static readonly string GetAllTabledependencies =
        @" declare @Table table ([name] varchar(100),[type] varchar(1000))INSERT INTO @Table exec sp_depends @tblName select * from @Table ";

        public static readonly string GetAllTableForeignKeys =
        @"
                        SELECT COALESCE(ep.value, '') AS 'Value', 
                        ob.name AS FK_NAME, 
                        sch.name AS [SchemaName], 
                        tab1.name AS [Table], 
                        col1.name AS [Column], 
                        SCHEMA_NAME(tab2.schema_id) + '.' + tab2.name AS [ReferencedTable], 
                        col2.name AS [ReferencedColumn]
                        FROM sys.objects ob 
                        LEFT OUTER JOIN sys.extended_properties ep ON ep.major_id = ob.object_id 
                        INNER JOIN sys.foreign_key_columns fkc ON ob.object_id = fkc.constraint_object_id 
                        INNER JOIN sys.tables tab1 ON tab1.object_id = fkc.parent_object_id 
                        INNER JOIN sys.schemas sch ON tab1.schema_id = sch.schema_id 
                        INNER JOIN sys.columns col1 ON col1.column_id = parent_column_id AND col1.object_id = tab1.object_id 
                        INNER JOIN sys.tables tab2 ON tab2.object_id = fkc.referenced_object_id 
                        INNER JOIN sys.columns col2 ON col2.column_id = referenced_column_id AND col2.object_id = tab2.object_id 
                        WHERE ob.is_ms_shipped = 0 
                        AND (sch.name + '.' + tab1.name) = @tblName

   

                        ";

        public static readonly string GetAllKeyConstraints =
        @"select table_view,    object_type,     constraint_type,    constraint_name,    details from (    select schema_name(t.schema_id) + '.' + t.[name] as table_view,         case when t.[type] = 'U' then 'Table'            when t.[type] = 'V' then 'View'            end as [object_type],        case when c.[type] = 'PK' then 'Primary key'            when c.[type] = 'UQ' then 'Unique constraint'            when i.[type] = 1 then 'Unique clustered index'            when i.type = 2 then 'Unique index'            end as constraint_type,         isnull(c.[name], i.[name]) as constraint_name,        substring(column_names, 1, len(column_names)-1) as [details]    from sys.objects t        left outer join sys.indexes i            on t.object_id = i.object_id        left outer join sys.key_constraints c            on i.object_id = c.parent_object_id             and i.index_id = c.unique_index_id       cross apply (select col.[name] + ', '                        from sys.index_columns ic                            inner join sys.columns col                                on ic.object_id = col.object_id                                and ic.column_id = col.column_id                        where ic.object_id = t.object_id                            and ic.index_id = i.index_id                                order by col.column_id                                for xml path ('') ) D (column_names)    where is_unique = 1    and t.is_ms_shipped <> 1    union all     select schema_name(fk_tab.schema_id) + '.' + fk_tab.name as foreign_table,        'Table',        'Foreign key',        fk.name as fk_constraint_name,        schema_name(pk_tab.schema_id) + '.' + pk_tab.name    from sys.foreign_keys fk        inner join sys.tables fk_tab            on fk_tab.object_id = fk.parent_object_id        inner join sys.tables pk_tab            on pk_tab.object_id = fk.referenced_object_id        inner join sys.foreign_key_columns fk_cols            on fk_cols.constraint_object_id = fk.object_id    union all    select schema_name(t.schema_id) + '.' + t.[name],        'Table',        'Check constraint',        con.[name] as constraint_name,        con.[definition]    from sys.check_constraints con        left outer join sys.objects t            on con.parent_object_id = t.object_id        left outer join sys.all_columns col            on con.parent_column_id = col.column_id            and con.parent_object_id = col.object_id    union all    select schema_name(t.schema_id) + '.' + t.[name],        'Table',        'Default constraint',        con.[name],        col.[name] + ' = ' + con.[definition]    from sys.default_constraints con        left outer join sys.objects t            on con.parent_object_id = t.object_id        left outer join sys.all_columns col            on con.parent_column_id = col.column_id            and con.parent_object_id = col.object_id) a where a.table_view=@tblName order by table_view, constraint_type, constraint_name ";

        public static readonly string UpdateTableExtendedProperty =
        @"EXEC sys.sp_updateextendedproperty N'MS_Description',  @Table_value , N'SCHEMA', @Schema_Name, N'TABLE', @Table_Name";

        public static readonly string InsertTableExtendedProperty =
        @"EXEC sys.sp_addextendedproperty N'MS_Description',  @Table_value , N'SCHEMA', @Schema_Name, N'TABLE', @Table_Name";

        public static readonly string UpdateTableColumnExtendedProperty =
        @"EXEC sys.sp_updateextendedproperty @name=N'MS_Description', @value=@Column_value,@level0type=N'SCHEMA',@level0name=@Schema_Name,@level1type=N'TABLE',@level1name=@Table_Name,@level2type=N'COLUMN',@level2name=@Column_Name";

        public static readonly string InsertTableColumnExtendedProperty =
        @"EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=@Column_value,@level0type=N'SCHEMA',@level0name=@Schema_Name,@level1type=N'TABLE',@level1name=@Table_Name,@level2type=N'COLUMN',@level2name=@Column_Name";



    }
}