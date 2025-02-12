namespace API.Common.Queries
{
    /// <summary>
    /// Contains SQL query constants used for various database operations.
    /// </summary>
    public static partial class SqlQueryConstant
    {
        /// <summary>
        /// SQL query to load all stored procedures.
        /// </summary>
        public const string LoadStoredProcedures =
            @"
                SELECT ((SCHEMA_NAME(O.SCHEMA_ID) )+'.'+ [NAME]) AS 'ProcedureName' 
                FROM SYS.SQL_MODULES M 
                INNER JOIN SYS.OBJECTS O ON M.OBJECT_ID = O.OBJECT_ID
                WHERE O.TYPE = 'P'";

        /// <summary>
        /// SQL query to load all database triggers.
        /// </summary>
        public const string LoadDatabaseTriggers =
            @"
                SELECT ((SCHEMA_NAME(O.SCHEMA_ID) )+'.'+ [NAME]) AS 'TriggerName' 
                FROM SYS.SQL_MODULES M 
                INNER JOIN SYS.OBJECTS O ON M.OBJECT_ID = O.OBJECT_ID
                WHERE O.TYPE = 'TR'";

        /// <summary>
        /// SQL query to load all user-defined data types.
        /// </summary>
        public const string LoadUserDefinedDataTypes =
            @"
                SELECT (SCHEMA_NAME(SCHEMA_ID) +'.'+[NAME]) AS 'UserTypeName' 
                FROM SYS.TYPES
                WHERE IS_USER_DEFINED = 1";

        /// <summary>
        /// SQL query to load all XML schema collections.
        /// </summary>
        public const string LoadXmlSchemaCollections =
            @"
                SELECT DISTINCT (SCHEMA_NAME(SCHEMA_ID)+'.'+XSC.NAME) AS 'SchemaName' 
                FROM SYS.XML_SCHEMA_COLLECTIONS XSC 
                JOIN SYS.XML_SCHEMA_NAMESPACES XSN  
                ON (XSC.XML_COLLECTION_ID = XSN.XML_COLLECTION_ID)";

        /// <summary>
        /// SQL query to load server properties.
        /// </summary>
        public const string LoadServerProperties =
            @"
                SELECT 
                    LEFT(@@VERSION, CHARINDEX(' - ', @@VERSION)) AS ProductName,
                    SERVERPROPERTY('ProductMajorVersion') AS ProductMajorVersion,
                    SERVERPROPERTY('ProductBuild') AS ProductBuild,
                    SERVERPROPERTY('InstanceDefaultLogPath') AS InstanceDefaultLogPath,
                    SERVERPROPERTY('Edition') AS Edition,
                    SERVERPROPERTY('BuildClrVersion') AS BuildClrVersion,
                    SERVERPROPERTY('Collation') AS Collation,
                    SERVERPROPERTY('ComputerNamePhysicalNetBIOS') AS ComputerNamePhysicalNetBIOS,
                    CASE
                        WHEN SERVERPROPERTY('EngineEdition') = 1 THEN 'Personal or Desktop Engine'
                        WHEN SERVERPROPERTY('EngineEdition') = 2 THEN 'Standard'
                        WHEN SERVERPROPERTY('EngineEdition') = 3 THEN 'Enterprise'
                        WHEN SERVERPROPERTY('EngineEdition') = 4 THEN 'Express'
                        WHEN SERVERPROPERTY('EngineEdition') = 5 THEN 'SQL Database'
                        WHEN SERVERPROPERTY('EngineEdition') = 6 THEN 'SQL Data Warehouse'
                        WHEN SERVERPROPERTY('EngineEdition') = 8 THEN 'Managed Instance'
                    END AS EngineEdition,
                    @@LANGUAGE AS Language,
                    (SELECT TOP 1 value_data 
                     FROM sys.dm_server_registry 
                     WHERE value_name = 'ObjectName') AS Platform,
                    CASE
                        WHEN SERVERPROPERTY('IsClustered') = 1 THEN 'Clustered'
                        WHEN SERVERPROPERTY('IsClustered') = 0 THEN 'Not Clustered'
                    END AS IsClustered";

        /// <summary>
        /// SQL query to load advanced server settings.
        /// </summary>
        public const string LoadAdvancedServerSettings =
            @"
                SELECT value_name AS name, 
                       CAST(value_data AS VARCHAR(1000)) AS Value 
                FROM sys.dm_server_registry";

        /// <summary>
        /// SQL query to load all databases excluding system databases.
        /// </summary>
        public const string LoadDatabases =
            @"
                SELECT name FROM master.dbo.sysdatabases 
                WHERE name NOT IN ('master', 'tempdb', 'model', 'msdb')";

        /// <summary>
        /// SQL query to load database files for a specific database.
        /// </summary>
        public static readonly string LoadDatabaseFiles =
            @"
                SELECT 
                    MF.NAME AS FileName,
                    CASE MF.type_desc 
                        WHEN 'ROWS' THEN 'DATA' 
                        ELSE MF.type_desc 
                    END AS FileType,
                    MF.physical_name AS FileLocation,
                    MF.size / 128 AS CurrentSizeMB,
                    CASE MF.max_size 
                        WHEN -1 THEN 'Unlimited' 
                        ELSE CONVERT(VARCHAR(20), MF.max_size / 128) 
                    END AS MaxSizeMB,
                    CONVERT(VARCHAR(20), MF.growth) + 
                    CASE MF.is_percent_growth 
                        WHEN 1 THEN ' Percent' 
                        ELSE ' Pages of 8KB' 
                    END AS GrowthType
                FROM 
                    master.sys.master_files MF
                JOIN 
                    master.sys.databases D 
                    ON MF.database_id = D.database_id
                WHERE 
                    D.name = @DatabaseName";

        /// <summary>
        /// SQL query to load view details.
        /// </summary>
        public static readonly string LoadViewDetails =
            @"
                SELECT 
                    SCHEMA_NAME(o.schema_id) + '.' + o.name AS ViewName,
                    ep.value AS ExtendedProperty
                FROM 
                    sys.extended_properties ep
                INNER JOIN 
                    sys.objects o ON ep.major_id = o.object_id
                WHERE 
                    o.type = 'V'";

        /// <summary>
        /// SQL query to load aggregate functions.
        /// </summary>
        public static readonly string LoadAggregateFunctions =
            @"
                SELECT SCHEMA_NAME(O.SCHEMA_ID) + '.' + O.NAME AS FunctionName
                FROM SYS.OBJECTS O
                INNER JOIN SYS.SQL_MODULES M ON O.OBJECT_ID = M.OBJECT_ID
                WHERE O.TYPE = 'IF'";

        /// <summary>
        /// SQL query to load scalar functions.
        /// </summary>
        public const string LoadScalarFunctions =
            @"
                SELECT ((SCHEMA_NAME(O.SCHEMA_ID) )+'.'+ [NAME]) AS 'FunctionName' 
                FROM SYS.SQL_MODULES M 
                INNER JOIN SYS.OBJECTS O ON M.OBJECT_ID = O.OBJECT_ID
                WHERE O.TYPE = 'FN'";

        /// <summary>
        /// SQL query to load table-valued functions.
        /// </summary>
        public const string LoadTableValuedFunctions =
            @"
                SELECT ((SCHEMA_NAME(O.SCHEMA_ID) )+'.'+ [NAME]) AS 'FunctionName' 
                FROM SYS.SQL_MODULES M 
                INNER JOIN SYS.OBJECTS O ON M.OBJECT_ID = O.OBJECT_ID
                WHERE O.TYPE = 'TF'";

        /// <summary>
        /// SQL query to load storage information.
        /// </summary>
        public const string LoadStorage =
            @"
                SELECT 
                    fg.name AS FileGroupName,
                    mf.name AS FileName,
                    mf.physical_name AS PhysicalPath,
                    mf.type_desc AS FileType,
                    mf.size / 128 AS SizeMB, 
                    mf.max_size / 128 AS MaxSizeMB,
                    mf.growth / 128 AS GrowthMB
                FROM sys.filegroups fg
                JOIN sys.master_files mf 
                    ON fg.data_space_id = mf.data_space_id";

        /// <summary>
        /// SQL query to load full-text catalogs.
        /// </summary>
        public const string LoadFullTextCatalogs =
            @"
                SELECT 
                    c.name AS CatalogName, 
                    ISNULL(SCHEMA_NAME(c.principal_id), 'dbo') AS SchemaName, 
                    c.is_default AS IsDefault,
                    c.is_accent_sensitivity_on AS IsAccentSensitive
                FROM sys.fulltext_catalogs c";

        /// <summary>
        /// SQL query to load security information.
        /// </summary>
        public const string LoadSecurity =
            @"
                SELECT 
                    dp.name AS PrincipalName,
                    dp.type_desc AS PrincipalType,
                    p.permission_name AS PermissionName,
                    p.state_desc AS PermissionState
                FROM sys.database_principals dp
                LEFT JOIN sys.database_permissions p 
                    ON dp.principal_id = p.grantee_principal_id";

        /// <summary>
        /// SQL query to load security users.
        /// </summary>
        public const string LoadSecurityUsers =
            @"
                SELECT 
                    dp.name AS UserName,
                    dp.type_desc AS UserType,
                    ISNULL(dp.default_schema_name, 'dbo') AS DefaultSchema
                FROM sys.database_principals dp
                WHERE dp.type IN ('S', 'U', 'G', 'C')"; // Filters for users and groups

        /// <summary>
        /// SQL query to get security roles.
        /// </summary>
        public const string GetSecurityRoles =
            @"
                SELECT 
                    r.name AS RoleName,
                    rl.name AS MemberName
                FROM sys.database_principals r
                LEFT JOIN sys.database_role_members drm 
                    ON r.principal_id = drm.role_principal_id
                LEFT JOIN sys.database_principals rl 
                    ON drm.member_principal_id = rl.principal_id
                WHERE r.type = 'R'"; // Filters only for roles

        /// <summary>
        /// SQL query to get security schemas.
        /// </summary>
        public const string GetSecuritySchemas =
            @"
                SELECT 
                    s.name AS SchemaName,
                    dp.name AS PrincipalName
                FROM sys.schemas s
                LEFT JOIN sys.database_principals dp 
                    ON s.principal_id = dp.principal_id";
    }
    public static partial class SqlQueryConstant
    {
        /// <summary>
        /// SQL query to fetch all stored procedures with descriptions.
        /// </summary>
        public static readonly string FetchAllStoredProceduresWithDescriptions =
            @"SELECT DISTINCT 
                    (SCHEMA_NAME(O.SCHEMA_ID) + '.' + O.[NAME]) AS StoredProcedure, 
                    ISNULL(EP.VALUE, '') AS ExtendedProperty
                FROM SYS.OBJECTS O  
                LEFT JOIN SYS.EXTENDED_PROPERTIES EP 
                ON EP.MAJOR_ID = O.OBJECT_ID 
                AND EP.CLASS_DESC = 'OBJECT_OR_COLUMN'
                WHERE O.TYPE = 'P';
            ";
   

    public static readonly string FetchStoredProcedureDependencies =
            @"SELECT OBJECT_SCHEMA_NAME(referencing_id) + '.' + OBJECT_NAME(referencing_id) AS ReferencingObjectName,     
                     obj.type_desc AS ReferencingObjectType,     
                     referenced_schema_name + '.' + referenced_entity_name AS ReferencedObjectName 
              FROM sys.sql_expression_dependencies AS sed 
              INNER JOIN sys.objects AS obj ON sed.referencing_id = obj.object_id 
              WHERE referencing_id = OBJECT_ID(@StoredProcedureName)";

        public static readonly string FetchStoredProcedureParametersWithDescriptions =
            @"SELECT o.name AS ParameterName,    
                     type_name(user_type_id) AS Type,    
                     max_length AS Length,    
                     CASE WHEN type_name(system_type_id) = 'uniqueidentifier' 
                          THEN precision 
                          ELSE OdbcPrec(system_type_id, max_length, precision) 
                     END AS Precision,    
                     OdbcScale(system_type_id, scale) AS Scale,    
                     parameter_id AS ParameterOrder,    
                     CONVERT(sysname, CASE 
                          WHEN system_type_id IN (35, 99, 167, 175, 231, 239) 
                          THEN ServerProperty('collation') 
                     END) AS Collation,  
                     ep.value AS ExtendedProperty  
              FROM sys.parameters o 
              LEFT JOIN sys.extended_properties EP  
              ON ep.major_id = O.object_id AND ep.minor_id = o.parameter_id 
              WHERE object_id = OBJECT_ID(@StoredProcedureName)";

        public static readonly string FetchStoredProcedureCreateScript =
            @"SELECT SCHEMA_NAME(schema_id) + '.' + [name] AS StoredProcedureName, 
                     object_definition(object_id) AS [ProcedureDefinition] 
              FROM sys.objects 
              WHERE type='P' AND (SCHEMA_NAME(schema_id) + '.' + [name]) = @StoredProcedureName";

        public static readonly string FetchExecutionPlan =
            @"SELECT TOP 1  
                     CAST(qp.query_plan AS VARCHAR(MAX)) AS QueryPlan,      
                     CAST(CP.usecounts AS VARCHAR(1000)) AS UseCounts,        
                     CAST(cp.cacheobjtype AS VARCHAR(100)) AS CacheObjectType,        
                     CAST(cp.size_in_bytes AS VARCHAR(1000)) AS SizeInBytes,         
                     CAST(SQLText.text AS VARCHAR(1000)) AS SQLText  
              FROM sys.dm_exec_cached_plans AS CP  
              CROSS APPLY sys.dm_exec_sql_text(plan_handle) AS SQLText  
              CROSS APPLY sys.dm_exec_query_plan(plan_handle) AS QP  
              WHERE objtype = 'Adhoc' AND cp.cacheobjtype = 'Compiled Plan'";

        public static readonly string FetchStoredProcedureExecutionPlan =
            @"SELECT CAST([qp].[query_plan] AS VARCHAR(MAX)) AS QueryPlan,  
                     '' AS UseCounts, '' AS CacheObjectType, '' AS SizeInBytes, '' AS SQLText  
              FROM [sys].[dm_exec_procedure_stats] AS [ps]       
              JOIN [sys].[dm_exec_query_stats] AS [qs] 
              ON [ps].[plan_handle] = [qs].[plan_handle]       
              CROSS APPLY [sys].[dm_exec_query_plan]([qs].[plan_handle]) AS [qp] 
              WHERE OBJECT_NAME([ps].[object_id], [ps].[database_id]) = @StoredProcedureName";

        public static readonly string FetchStoredProcedureDescription =
            @"SELECT ((SCHEMA_NAME(O.SCHEMA_ID) )+'.'+ O.[NAME]) AS 'StoredProcedure', 
                     ep.value AS ExtendedProperty  
              FROM sys.extended_properties EP 
              LEFT JOIN SYS.OBJECTS O ON ep.major_id = O.object_id  
              WHERE O.TYPE='P' AND class_desc='OBJECT_OR_COLUMN' 
              AND ((SCHEMA_NAME(O.SCHEMA_ID) )+'.'+ O.[NAME]) = @StoredProcedureName";

        public static readonly string MergeStoredProcedureExtendedProperty =
            @"
            IF EXISTS (
                SELECT 1 
                FROM sys.extended_properties ep
                JOIN sys.objects o ON ep.major_id = o.object_id
                JOIN sys.schemas s ON o.schema_id = s.schema_id
                WHERE ep.name = 'MS_Description'
                AND s.name = @SchemaName
                AND o.name = @StoredProcedureName
            )
            BEGIN
                EXEC sys.sp_updateextendedproperty
                    N'MS_Description',
                    @Description,
                    N'SCHEMA',
                    @SchemaName,
                    N'PROCEDURE',
                    @StoredProcedureName
            END
            ELSE
            BEGIN
                EXEC sys.sp_addextendedproperty
                    N'MS_Description',
                    @Description,
                    N'SCHEMA',
                    @SchemaName,
                    N'PROCEDURE',
                    @StoredProcedureName
            END";

        public static readonly string MergeStoredProcedureParameterExtendedProperty =
            @"
                        IF EXISTS (
                            SELECT 1 
                            FROM sys.extended_properties ep
                            JOIN sys.parameters p ON ep.major_id = p.object_id AND ep.minor_id = p.parameter_id
                            JOIN sys.objects o ON p.object_id = o.object_id
                            JOIN sys.schemas s ON o.schema_id = s.schema_id
                            WHERE ep.name = 'MS_Description'
                            AND s.name = @SchemaName
                            AND o.name = @StoredProcedureName
                            AND p.name = @ParameterName
                        )
                        BEGIN
                            EXEC sys.sp_updateextendedproperty
                                N'MS_Description',
                                @Description,
                                N'SCHEMA',
                                @SchemaName,
                                N'PROCEDURE',
                                @StoredProcedureName,
                                N'PARAMETER',
                                @ParameterName
                        END
                        ELSE
                        BEGIN
                            EXEC sys.sp_addextendedproperty
                                N'MS_Description',
                                @Description,
                                N'SCHEMA',
                                @SchemaName,
                                N'PROCEDURE',
                                @StoredProcedureName,
                                N'PARAMETER',
                                @ParameterName
                        END";

        public static string FetchStoredProcedureParameterDescription =
            @"
                    SELECT 
                        ep.value AS Description
                    FROM 
                        sys.extended_properties ep
                    JOIN 
                        sys.parameters p ON ep.major_id = p.object_id AND ep.minor_id = p.parameter_id
                    JOIN 
                        sys.objects o ON p.object_id = o.object_id
                    JOIN 
                        sys.schemas s ON o.schema_id = s.schema_id
                    WHERE 
                        ep.name = 'MS_Description'
                        AND s.name = @SchemaName
                        AND o.name = @StoredProcedureName
                        AND p.name = @ParameterName;
                    ";
    }
    public static partial class SqlQueryConstant
    {
        public static readonly string GetAllExtendedPropertiesofTheTable =
            @" 
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

        public static readonly string GetAllTablesExtendedProperties =
            @" 
                                        SELECT  
                                            (SCHEMA_NAME(t.schema_id) + '.' + t.name) AS TableName,
                                            sep.name AS [Name],
                                            sep.value AS [Value],
                                            'Table' AS [Level],
                                            '' AS columnName
                                        FROM 
                                            sys.tables t
                                            LEFT JOIN sys.extended_properties sep ON t.object_id = sep.major_id
                                                AND sep.class_desc = 'OBJECT_OR_COLUMN' -- Filter for objects (tables)
                                                AND sep.minor_id = 0 -- Ensure it's for the table, not columns

                                        UNION

                                        SELECT  
                                            (SCHEMA_NAME(t.schema_id) + '.' + t.name) AS TableName,
                                            sep.name AS [Name],
                                            sep.value AS [Value],
                                            'Column' AS [Level],
                                            c.COLUMN_NAME AS columnName
                                        FROM 
                                            sys.tables t
                                            INNER JOIN INFORMATION_SCHEMA.COLUMNS c ON c.TABLE_NAME = t.name 
                                                AND c.TABLE_SCHEMA = SCHEMA_NAME(t.schema_id)
                                            LEFT JOIN sys.extended_properties sep ON t.object_id = sep.major_id
                                                AND sep.class_desc = 'OBJECT_OR_COLUMN'  
                                                AND sep.minor_id = c.ORDINAL_POSITION  
 
";

        public static readonly string GetTableProperties =
            @"SELECT Property as Name, Value
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


        public static readonly string GetTableCreateScript =
            @"
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
                    CASE 
                        WHEN i.is_primary_key = 1 THEN 'Yes' 
                        ELSE 'No' 
                    END AS [Key],
                    CASE 
                        WHEN idc.is_identity = 1 THEN 'Yes' 
                        ELSE 'No' 
                    END AS [Identity],
                    TRY_CAST(colm.data_type AS VARCHAR(100)) AS [DataType],
                    ISNULL(TRY_CAST(colm.character_maximum_length AS VARCHAR(100)), '') AS [MaxLength],
                    ISNULL(TRY_CAST(colm.is_nullable AS VARCHAR(100)), 'Unknown') AS [AllowNulls],
                    ISNULL(TRY_CAST(colm.column_default AS VARCHAR(100)), 'None') AS [Default],
                    ISNULL(
                        (SELECT VALUE 
                         FROM ::fn_listextendedproperty ('MS_Description', 'schema', colm.table_schema, 'table', colm.table_name, 'column', colm.column_name)
                        ), 'No Description') AS [Description]
                FROM sys.tables t
                LEFT JOIN sys.columns c ON c.OBJECT_ID = t.OBJECT_ID
                LEFT JOIN sys.identity_columns idc ON idc.OBJECT_ID = t.OBJECT_ID AND idc.column_id = c.column_id
                LEFT JOIN sys.index_columns ic ON ic.OBJECT_ID = t.OBJECT_ID AND ic.column_id = c.column_id
                LEFT JOIN sys.indexes i ON i.OBJECT_ID = t.OBJECT_ID AND i.index_id = ic.index_id AND i.is_primary_key = 1
                INNER JOIN information_schema.columns colm ON colm.table_name = t.NAME AND colm.column_name = c.NAME
                WHERE t.TYPE = 'U'
                AND colm.table_schema + '.' + colm.table_name = @tblName
                ORDER BY colm.table_schema, colm.table_name;
;
        ";
        public static readonly string GetAllTableDescriptionWithAll =
            @" SELECT     t.Name , SCHEMA_NAME(schema_id)+'.'+t.name, sep.value ,SCHEMA_NAME(schema_id)   FROM     sys.tables t INNER JOIN     sys.extended_properties sep ON t.object_id = sep.major_id where     sep.Name = @ExtendedProp    AND sep.minor_id = 0     ";

        public static readonly string GetTableIndex =
            @"
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

        public static readonly string AllTableFragmentation =
            @"
                    SELECT 
                    SCHEMA_NAME(t.schema_id) + '.' + t.name AS TableName,
                    i.name AS IndexName,
                    CAST(frag.avg_fragmentation_in_percent AS INT) AS PercentFragmented
                    FROM 
                    sys.dm_db_index_physical_stats(DB_ID(), NULL, NULL, NULL, 'LIMITED') AS frag
                    JOIN 
                    sys.tables AS t ON frag.object_id = t.object_id
                    JOIN 
                    sys.indexes AS i ON frag.object_id = i.object_id AND frag.index_id = i.index_id
                    WHERE 
                    i.type != 0 
                    AND t.name != 'ThreadActionCount'
                    AND frag.avg_fragmentation_in_percent > 0
                    ORDER BY 
                    frag.avg_fragmentation_in_percent DESC;";

        /*0*/

        public static string ObjectThatDependsOn =
                        @"CREATE TABLE #references (
                    thepath VARCHAR(max),
                    thefullentityname VARCHAR(200),
                    thetype VARCHAR(20),
                    iteration INT
                );
    
                CREATE TABLE #databasedependencies (
                    entityname VARCHAR(200),
                    entitytype CHAR(5),
                    dependencytype CHAR(4),
                    thereferredentity VARCHAR(200),
                    thereferredtype CHAR(5)
                );
    
                INSERT INTO #databasedependencies (entityname, entitytype, dependencytype, thereferredentity, thereferredtype)
                SELECT
                    Object_schema_name(o.object_id) + '.' + o.NAME,
                    o.type,
                    'hard',
                    ty.NAME,
                    'UDT'
                FROM sys.objects o
                INNER JOIN sys.columns AS c ON c.object_id = o.object_id
                INNER JOIN sys.types ty ON ty.user_type_id = c.user_type_id
                WHERE is_user_defined = 1
                UNION ALL
                SELECT
                    Object_schema_name(tt.type_table_object_id) + '.' + tt.NAME,
                    'UDTT',
                    'hard',
                    ty.NAME,
                    'UDT'
                FROM sys.table_types tt
                INNER JOIN sys.columns AS c ON c.object_id = tt.type_table_object_id
                INNER JOIN sys.types ty ON ty.user_type_id = c.user_type_id
                WHERE ty.is_user_defined = 1;
    
                DECLARE @RowCount INT;
                DECLARE @ii INT;
    
                INSERT INTO #references (thepath, thefullentityname, thetype, iteration)
                SELECT COALESCE(Object_schema_name(object_id) + '.', '') + NAME,
                       COALESCE(Object_schema_name(object_id) + '.', '') + NAME,
                       type,
                       1
                FROM sys.objects
                WHERE NAME LIKE @ObjectName;
    
                SELECT @rowcount = @@ROWCOUNT, @ii = 2;
    
                IF 0 <> 0
                BEGIN
                    WHILE @ii < 40 AND @rowcount > 0
                    BEGIN
                        INSERT INTO #references (thepath, thefullentityname, thetype, iteration)
                        SELECT DISTINCT thepath + '/' + thereferredentity,
                                        thereferredentity,
                                        thereferredtype,
                                        @ii
                        FROM #databasedependencies
                        INNER JOIN #references previousReferences
                        ON previousReferences.thefullentityname = entityname
                        AND previousReferences.iteration = @ii - 1
                        WHERE thereferredentity <> entityname
                        AND thereferredentity NOT IN (SELECT thefullentityname FROM #references);
            
                        SELECT @rowcount = @@rowcount;
                        SELECT @ii = @ii + 1;
                    END
                END;
    
                SELECT * FROM #references;
    
                DROP TABLE #databasedependencies;
                DROP TABLE #references;";

        public static string ObjectOnWhichDepends =
                        @"CREATE TABLE #references (
                    thepath VARCHAR(max),
                    thefullentityname VARCHAR(200),
                    thetype VARCHAR(20),
                    iteration INT
                );
    
                CREATE TABLE #databasedependencies (
                    entityname VARCHAR(200),
                    entitytype CHAR(5),
                    dependencytype CHAR(4),
                    thereferredentity VARCHAR(200),
                    thereferredtype CHAR(5)
                );
    
                INSERT INTO #databasedependencies (entityname, entitytype, dependencytype, thereferredentity, thereferredtype)
                SELECT
                    Object_schema_name(o.object_id) + '.' + o.NAME,
                    o.type,
                    'hard',
                    ty.NAME,
                    'UDT'
                FROM sys.objects o
                INNER JOIN sys.columns AS c ON c.object_id = o.object_id
                INNER JOIN sys.types ty ON ty.user_type_id = c.user_type_id
                WHERE is_user_defined = 1;
    
                DECLARE @RowCount INT;
                DECLARE @ii INT;
    
                INSERT INTO #references (thepath, thefullentityname, thetype, iteration)
                SELECT COALESCE(Object_schema_name(object_id) + '.', '') + NAME,
                       COALESCE(Object_schema_name(object_id) + '.', '') + NAME,
                       type,
                       1
                FROM sys.objects
                WHERE NAME LIKE @ObjectName;
    
                SELECT @rowcount = @@ROWCOUNT, @ii = 2;
    
                IF 1 <> 0
                BEGIN
                    WHILE @ii < 40 AND @rowcount > 0
                    BEGIN
                        INSERT INTO #references (thepath, thefullentityname, thetype, iteration)
                        SELECT DISTINCT thepath + '/' + thereferredentity,
                                        thereferredentity,
                                        thereferredtype,
                                        @ii
                        FROM #databasedependencies
                        INNER JOIN #references previousReferences
                        ON previousReferences.thefullentityname = entityname
                        AND previousReferences.iteration = @ii - 1
                        WHERE thereferredentity <> entityname
                        AND thereferredentity NOT IN (SELECT thefullentityname FROM #references);
            
                        SELECT @rowcount = @@rowcount;
                        SELECT @ii = @ii + 1;
                    END
                END;
    
                SELECT * FROM #references;
    
                DROP TABLE #databasedependencies;
                DROP TABLE #references;";


        public static string ObjectThatDependsOn_new =
                    @"CREATE TABLE #references (
                        thepath VARCHAR(max),
                        thefullentityname VARCHAR(200) PRIMARY KEY,
                        thetype VARCHAR(20),
                        iteration INT
                    );
    
                    CREATE TABLE #databasedependencies (
                        entityname VARCHAR(200),
                        entitytype CHAR(5),
                        dependencytype CHAR(4),
                        thereferredentity VARCHAR(200),
                        thereferredtype CHAR(5),
                        PRIMARY KEY (entityname, thereferredentity)
                    );
    
                    INSERT INTO #databasedependencies (entityname, entitytype, dependencytype, thereferredentity, thereferredtype)
                    SELECT DISTINCT
                        OBJECT_SCHEMA_NAME(o.object_id) + '.' + o.name,
                        o.type,
                        'hard',
                        ty.name,
                        'UDT'
                    FROM sys.objects o
                    JOIN sys.columns c ON c.object_id = o.object_id
                    JOIN sys.types ty ON ty.user_type_id = c.user_type_id
                    WHERE ty.is_user_defined = 1;
    
                    DECLARE @RowCount INT = 0;
                    DECLARE @ii INT = 2;
    
                    INSERT INTO #references (thepath, thefullentityname, thetype, iteration)
                    SELECT SCHEMA_NAME(o.schema_id) + '.' + o.name, SCHEMA_NAME(o.schema_id) + '.' + o.name, o.type, 1
                    FROM sys.objects o
                    WHERE o.name = @ObjectName;
    
                    SET @RowCount = @@ROWCOUNT;
    
                    WHILE @ii < 40 AND @RowCount > 0
                    BEGIN
                        INSERT INTO #references (thepath, thefullentityname, thetype, iteration)
                        SELECT DISTINCT r.thepath + '/' + d.thereferredentity, d.thereferredentity, d.thereferredtype, @ii
                        FROM #databasedependencies d
                        JOIN #references r ON r.thefullentityname = d.entityname AND r.iteration = @ii - 1
                        WHERE d.thereferredentity <> d.entityname
                        AND NOT EXISTS (SELECT 1 FROM #references WHERE thefullentityname = d.thereferredentity);
        
                        SET @RowCount = @@ROWCOUNT;
                        SET @ii = @ii + 1;
                    END;
    
                    SELECT * FROM #references;
    
                    DROP TABLE #databasedependencies;
                    DROP TABLE #references;";

        public static string ObjectOnWhichDepends_new =
            @"CREATE TABLE #references (
                        thepath VARCHAR(max),
                        thefullentityname VARCHAR(200) PRIMARY KEY,
                        thetype VARCHAR(20),
                        iteration INT
                    );
    
                    CREATE TABLE #databasedependencies (
                        entityname VARCHAR(200),
                        entitytype CHAR(5),
                        dependencytype CHAR(4),
                        thereferredentity VARCHAR(200),
                        thereferredtype CHAR(5),
                        PRIMARY KEY (entityname, thereferredentity)
                    );
    
                    INSERT INTO #databasedependencies (entityname, entitytype, dependencytype, thereferredentity, thereferredtype)
                    SELECT DISTINCT
                        OBJECT_SCHEMA_NAME(o.object_id) + '.' + o.name,
                        o.type,
                        'hard',
                        ty.name,
                        'UDT'
                    FROM sys.objects o
                    JOIN sys.columns c ON c.object_id = o.object_id
                    JOIN sys.types ty ON ty.user_type_id = c.user_type_id
                    WHERE ty.is_user_defined = 1;
    
                    DECLARE @RowCount INT = 0;
                    DECLARE @ii INT = 2;
    
                    INSERT INTO #references (thepath, thefullentityname, thetype, iteration)
                    SELECT SCHEMA_NAME(o.schema_id) + '.' + o.name, SCHEMA_NAME(o.schema_id) + '.' + o.name, o.type, 1
                    FROM sys.objects o
                    WHERE o.name = @ObjectName;
    
                    SET @RowCount = @@ROWCOUNT;
    
                    WHILE @ii < 40 AND @RowCount > 0
                    BEGIN
                        INSERT INTO #references (thepath, thefullentityname, thetype, iteration)
                        SELECT DISTINCT r.thepath + '/' + d.thereferredentity, d.thereferredentity, d.thereferredtype, @ii
                        FROM #databasedependencies d
                        JOIN #references r ON r.thefullentityname = d.entityname AND r.iteration = @ii - 1
                        WHERE d.thereferredentity <> d.entityname
                        AND NOT EXISTS (SELECT 1 FROM #references WHERE thefullentityname = d.thereferredentity);
        
                        SET @RowCount = @@ROWCOUNT;
                        SET @ii = @ii + 1;
                    END;
    
                    SELECT * FROM #references; 
                    DROP TABLE #databasedependencies;
                    DROP TABLE #references;";

    }
    public static partial class SqlQueryConstant
    {
        public static readonly string GetAllDatabaseTrigger =
            @"
                        SELECT 
                        trigg.name AS Name, 
                        sep.value AS Description, 
                        module.definition AS Definition, 
                        obj.create_date AS CreateDate, 
                        obj.modify_date AS ModifyDate
                        FROM sys.triggers trigg
                        JOIN sys.sql_modules module ON trigg.object_id = module.object_id
                        JOIN sys.objects obj ON trigg.object_id = obj.object_id
                        LEFT JOIN sys.extended_properties sep ON sep.major_id = trigg.object_id
                        WHERE trigg.parent_class = 0

                        UNION 

                        SELECT 
                        SCHEMA_NAME(O.schema_id) + '.' + O.name AS Name, 
                        sep.value AS Description, 
                        M.definition AS Definition, 
                        O.create_date AS CreateDate, 
                        O.modify_date AS ModifyDate
                        FROM sys.objects O
                        JOIN sys.sql_modules M ON O.object_id = M.object_id
                        LEFT JOIN sys.extended_properties sep ON O.object_id = sep.major_id
                        WHERE O.type = 'TR';


    ";

        public static readonly string GetDatabaseTriggerdtlByName =
            @"
                    SELECT  
                    SCHEMA_NAME(O.schema_id) + '.' + O.name AS Name,   
                    COALESCE(sep.value, '') AS Description,         
                    M.definition AS Definition,                     
                    O.create_date AS CreateDate,                   
                    O.modify_date AS ModifyDate   

                    FROM sys.sql_modules M
                    INNER JOIN sys.objects O ON M.object_id = O.object_id
                    LEFT JOIN sys.extended_properties sep ON O.object_id = sep.major_id
                    WHERE O.type = 'TR' 
                    AND (SCHEMA_NAME(O.schema_id) + '.' + O.name) = @TriggerName;

            ";
        public static readonly string TriggerProperties =
            @"
                  SELECT 
                    tr.name AS TriggerName,
                    SCHEMA_NAME(obj.schema_id) AS SchemaName,
                    obj.name AS ObjectName,
                    obj.type_desc AS ObjectType,
                    obj.create_date AS CreateDate,
                    obj.modify_date AS ModifyDate,
                    tr.is_disabled AS IsDisabled,
                    OBJECTPROPERTY(OBJECT_ID(tr.name), 'ExecIsQuotedIdentOn') AS QuotedIdentifierOn,
                    OBJECTPROPERTY(OBJECT_ID(tr.name), 'ExecIsAnsiNullsOn') AS AnsiNullsOn
                FROM sys.triggers tr
                JOIN sys.objects obj ON tr.parent_id = obj.object_id
                WHERE ( SCHEMA_NAME(obj.schema_id)+'.'+tr.name) = @TriggerName
";

        public static readonly string MergeTriggerExtendedProperty =
            @"
                IF EXISTS (
                    SELECT 1 FROM sys.extended_properties 
                    WHERE name = N'MS_Description' 
                    AND major_id = OBJECT_ID(@Trigger_Name)
                )
                BEGIN
                    EXEC sys.sp_updateextendedproperty 
                        @name = N'MS_Description', 
                        @value = @Trigger_value,
                        @level0type = N'TRIGGER',
                        @level0name = @Trigger_Name;
                END
                ELSE
                BEGIN
                    EXEC sys.sp_addextendedproperty 
                        @name = N'MS_Description', 
                        @value = @Trigger_value,
                        @level0type = N'TRIGGER',
                        @level0name = @Trigger_Name;
                END
                ";
    }

    public static partial class SqlQueryConstant
    {
        /// <summary>
        /// Query to fetch descriptions of all scalar functions.
        /// </summary>
        public const string FetchScalarFunctionDescriptions =
            @"
            SELECT 
                QUOTENAME(SCHEMA_NAME(O.[schema_id])) + '.' + QUOTENAME(O.[name]) AS FunctionName,
                sep.[value] AS Description
            FROM sys.objects AS O
            LEFT JOIN sys.extended_properties AS sep
                ON O.[object_id] = sep.[major_id]
                AND sep.[name] = 'MS_Description'
            WHERE O.[type] = 'FN';";

        /// <summary>
        /// Query to fetch descriptions of all table-valued functions.
        /// </summary>
        public const string FetchTableFunctionDescriptions =
            @"
            SELECT 
                QUOTENAME(SCHEMA_NAME(O.[schema_id])) + '.' + QUOTENAME(O.[name]) AS FunctionName,
                sep.[value] AS Description
            FROM sys.objects AS O
            LEFT JOIN sys.extended_properties AS sep
                ON O.[object_id] = sep.[major_id]
                AND sep.[name] = 'MS_Description'
            WHERE O.[type] = 'TF';";

        /// <summary>
        /// Query to fetch descriptions of all aggregate functions.
        /// </summary>
        public const string FetchAggregateFunctionDescriptions =
            @"
            SELECT 
                QUOTENAME(SCHEMA_NAME(O.[schema_id])) + '.' + QUOTENAME(O.[name]) AS FunctionName,
                sep.[value] AS Description
            FROM sys.objects AS O
            LEFT JOIN sys.extended_properties AS sep
                ON O.[object_id] = sep.[major_id]
                AND sep.[name] = 'MS_Description'
            WHERE O.[type] = 'AF';";

        public static string RetrieveFunctionDetails =
            @"SELECT
                CONVERT(varchar(100), [uses_ansi_nulls]) AS [uses_ansi_nulls],
                CONVERT(varchar(100), [uses_quoted_identifier]) AS [uses_quoted_identifier],
                CONVERT(varchar(100), [create_date]) AS [create_date],
                CONVERT(varchar(100), [modify_date]) AS [modify_date],
                CONVERT(varchar(100), O.[name]) AS [name]
            FROM sys.sql_modules AS M
            INNER JOIN sys.objects AS O
                ON M.[object_id] = O.[object_id]
            WHERE
                O.[type] = @function_Type
                AND O.[name] = @function_name;";

        public static string FetchFunctionParametersWithDescriptions =
            @"SELECT
                        p.[name] AS [Parameter_name],
                        TYPE_NAME(p.[user_type_id]) AS [Type],
                        p.[max_length] AS [Length],
                        CASE 
                            WHEN TYPE_NAME(p.[system_type_id]) = 'uniqueidentifier' 
                            THEN p.[precision] 
                            ELSE OdbcPrec(p.[system_type_id], p.[max_length], p.[precision]) 
                        END AS [Prec],
                        OdbcScale(p.[system_type_id], p.[scale]) AS [Scale],
                        p.[parameter_id] AS [Param_order],
                        CONVERT(sysname, 
                            CASE WHEN p.[system_type_id] IN (35, 99, 167, 175, 231, 239)
                            THEN SERVERPROPERTY('collation') END) AS [Collation],
                        COALESCE(sep.[value], '') AS [ExtendedProperty]
                    FROM sys.objects AS Obj
                    INNER JOIN sys.parameters AS p
                        ON Obj.[object_id] = p.[object_id]
                    LEFT JOIN sys.extended_properties AS sep
                        ON p.[object_id] = sep.[major_id]
                        AND p.[parameter_id] = sep.[minor_id]
                        AND sep.[name] = 'MS_Description'  -- Only join if the extended property exists
                    WHERE
                        p.[name] IS NOT NULL AND  p.[name] <> '' AND
                        Obj.[type] = @function_Type
                        AND Obj.[object_id] = OBJECT_ID(@function_name);
                    ";

        public static string RetrieveFunctionDefinition =
            @"SELECT
                M.[definition]
            FROM sys.sql_modules AS M
            INNER JOIN sys.objects AS Obj
                ON M.[object_id] = Obj.[object_id]
            WHERE
                Obj.[type] = @function_Type
                AND Obj.[object_id] = OBJECT_ID(@function_name);";

        public static string FetchFunctionDependencies =
            @"DECLARE @Dependencies TABLE (
                [name] varchar(100),
                [type] varchar(1000),
                [Updated] varchar(1000),
                [Selected] varchar(1000),
                [column_name] varchar(1000)
            );
            
            INSERT INTO @Dependencies
            EXEC sys.sp_depends @objname = @function_name;
            
            SELECT * FROM @Dependencies;";

        public static readonly string ModifyFunctionDescription =
            @"EXEC sys.sp_updateextendedproperty
                @name = N'MS_Description',
                @value = @fun_value,
                @level0type = N'SCHEMA',
                @level0name = @Schema_Name,
                @level1type = N'FUNCTION',
                @level1name = @FunctionName;";

        public static readonly string AddFunctionDescription =
            @"EXEC sys.sp_addextendedproperty
                @name = N'MS_Description',
                @value = @fun_value,
                @level0type = N'SCHEMA',
                @level0name = @Schema_Name,
                @level1type = N'FUNCTION',
                @level1name = @FunctionName;";
    }
    public static partial class SqlQueryConstant
    {
        public static readonly string GetAllViewsDetailsWithMsDesc =
            @"SELECT SCHEMA_NAME(O.SCHEMA_ID) + '.' + O.[NAME] AS 'ViewName', ep.value AS ViewExtendedProperties
          FROM sys.extended_properties EP
          INNER JOIN SYS.OBJECTS O ON ep.major_id = O.object_id
          WHERE O.TYPE = 'V'";

        public static readonly string GetViewProperties =
            @"SELECT CAST(uses_ansi_nulls AS VARCHAR(1)) AS uses_ansi_nulls,
                 CAST(uses_quoted_identifier AS VARCHAR(1)) AS uses_quoted_identifier,
                 CAST(create_date AS VARCHAR(100)) AS create_date,
                 CAST(modify_date AS VARCHAR(100)) AS modify_date
          FROM sys.views vs
          INNER JOIN sys.sql_modules sqlM ON vs.object_id = sqlM.object_id
          WHERE vs.object_id = OBJECT_ID(@viewname)";

        public static readonly string GetViewColumns =
            @"SELECT 
                  (s.[name]+'.'+  v.[name]) AS ViewName,
				  c.[name] AS ColumnName,
				  c.column_id AS ColumnOrder,
				  t.[name] AS DataType,
				  c.max_length AS MaxLength,
				  c.precision,
				  c.scale
                FROM sys.views v
                JOIN sys.schemas s ON v.schema_id = s.schema_id
                JOIN sys.columns c ON v.object_id = c.object_id
                JOIN sys.types t ON c.user_type_id = t.user_type_id
                where (s.[name]+'.'+v.[name])=@viewname 
            ";

        public static readonly string GetViewCreateScript =
            @"select sqlM.definition as createViewScript FROM  sys.views vs inner join sys.sql_modules  
sqlM ON vs.object_id=sqlM.object_id where sqlM.object_id=OBJECT_ID(@viewname)";

        public static readonly string GetViewDependencies =
            @"
                SELECT  distinct  referenced_entity_name as  [name], is_updated as updated , is_select_all as selected ,referenced_entity_name as column_name,
                 (referenced_entity_name+'.'+ referenced_entity_name)AS FullReferenceName
                FROM sys.dm_sql_referenced_entities( @viewName, 'OBJECT');
                ";
    }

    public static partial class SqlQueryConstant
    {
        public static string GetAllUserDefinedDataTypes =
            @"
        SELECT 
            SCHEMA_NAME(t.schema_id) + '.' + t.name AS Name,
            t.is_nullable AS 'AllowNulls',
            TYPE_NAME(t.system_type_id) AS 'BaseTypeName',
            t.max_length AS 'Length',
            CAST(
                'CREATE TYPE ' + SCHEMA_NAME(t.schema_id) + '.' + t.name + 
                ' FROM ' + TYPE_NAME(t.system_type_id) + 
                ' (' + CAST(t.max_length AS NVARCHAR) + ') ' +  
                CASE WHEN t.is_nullable = 1 THEN 'NULL' ELSE 'NOT NULL' END 
                AS NVARCHAR(100)
            ) AS CreateScript
        FROM sys.types t
        WHERE t.is_user_defined = 1";

        public static string GetUsedDefinedDataTypeReference =
            @"
                SELECT s.name + '.' + o.name AS ObjectName, o.type as ObjectType 
                FROM sys.schemas s 
                JOIN sys.objects o ON o.schema_id = s.schema_id  
                JOIN sys.columns c ON c.object_id = o.object_id  
                JOIN sys.types t ON c.user_type_id = t.user_type_id 
                WHERE 
                t.is_user_defined = 1  
                AND 
                      SCHEMA_NAME(t.schema_id)=@SchemaName
                AND  t.name = @TypeName;
   ";

        public static string GetUserDefinedDataTypeWithExtendedProperties =
            @" 
            SELECT 
                SCHEMA_NAME(t.schema_id) + '.' + t.name AS Name,
                t.is_nullable AS AllowNulls,
                TYPE_NAME(t.system_type_id) AS BaseTypeName,
                t.max_length AS Length,
                CAST(
                    'CREATE TYPE ' + SCHEMA_NAME(t.schema_id) + '.' + t.name + 
                    ' FROM ' + TYPE_NAME(t.system_type_id) + 
                    ' (' + CAST(t.max_length AS NVARCHAR) + ') ' +  
                    CASE WHEN t.is_nullable = 1 THEN 'NULL' ELSE 'NOT NULL' END 
                    AS NVARCHAR(100)
                ) AS CreateScript,
                (SELECT value  
                 FROM ::fn_listextendedproperty('MS_Description', 'SCHEMA', @SchemaName, 'TYPE', @TypeName, NULL, NULL)
                ) AS Description
            FROM sys.types t
            WHERE t.is_user_defined = 1 
            AND  t.name = @TypeName";
        public static string UpsertUserDefinedDataTypeExtendedProperty =
            @"
       DECLARE @ExistingValue sql_variant  ; 
            SELECT @ExistingValue = value  
            FROM sys.extended_properties 
            WHERE 
                major_id = (SELECT TYPE_ID(@SchemaName + '.' + @TypeName)) 
                AND minor_id = 0 
             
                AND name = 'MS_Description';

            IF @ExistingValue IS NULL
            BEGIN
                -- Add new extended property
                EXEC sys.sp_addextendedproperty 
                    @name = N'MS_Description', 
                    @value = @desc, 
                    @level0type = N'SCHEMA', @level0name = @SchemaName, 
                    @level1type = N'TYPE', @level1name = @TypeName;
            END
            ELSE
            BEGIN
                -- Update existing extended property
                EXEC sys.sp_updateextendedproperty 
                    @name = N'MS_Description', 
                    @value = @desc, 
                    @level0type = N'SCHEMA', @level0name = @SchemaName, 
                    @level1type = N'TYPE', @level1name = @TypeName;
            END
";
    }
    public static partial class SqlQueryConstant
    {
        public static string AllXMLSchemaDetails =
            @"
            SELECT   
                (s.name + '.' + xsc.name) AS XMLSchemaCollections, 
                ISNULL(ep.value, '') AS MS_Description  
            FROM sys.xml_schema_collections xsc
            LEFT JOIN sys.extended_properties ep
                ON xsc.xml_collection_id = ep.major_id
            JOIN sys.schemas s ON xsc.schema_id = s.schema_id	 
            WHERE xsc.xml_collection_id <> 1;
            ";
        public static string XMLSchemaDetails =
            @"
            DECLARE @Schema NVARCHAR(128);  
            SELECT @Schema = s.name
            FROM sys.xml_schema_collections xsc
            JOIN sys.schemas s ON xsc.schema_id = s.schema_id
            WHERE xsc.name = @SchemaCollectionName; 

            SELECT 
                CONVERT(NVARCHAR(MAX), 
                    'CREATE XML SCHEMA COLLECTION [' + @Schema + '].[' + @SchemaCollectionName + '] AS ' + CHAR(13) + CHAR(10) +
                    'N''' + CAST((SELECT xml_schema_namespace(@Schema, @SchemaCollectionName)) AS NVARCHAR(MAX)) + ''''
                ) AS SQLScript,
    
                ISNULL(ep.value, '') AS MS_Description,
    
                STUFF((
                     SELECT ', ' + (@Schema + '.' + t.name + '.' + c.name)
                     FROM sys.tables AS t
                     JOIN sys.columns AS c ON t.object_id = c.object_id
                     JOIN sys.xml_schema_collections AS xsc2 ON c.xml_collection_id = xsc2.xml_collection_id
                     WHERE xsc2.name = @SchemaCollectionName
                     FOR XML PATH(''), TYPE
                ).value('.', 'NVARCHAR(MAX)'), 1, 2, '') AS DependentColumns
            FROM sys.xml_schema_collections xsc
            LEFT JOIN sys.extended_properties ep
                ON xsc.xml_collection_id = ep.major_id   
            WHERE xsc.name = @SchemaCollectionName;
            ";

        public static string XMLSchemReference =
            @"
            SELECT
                OBJECT_SCHEMA_NAME(c.object_id) AS TableSchema,
                OBJECT_NAME(c.object_id) AS TableName,
                c.name AS ColumnName,
                s.name AS XMLSchemaCollection
            FROM sys.columns AS c
            INNER JOIN sys.xml_schema_collections AS s
                ON c.xml_collection_id = s.xml_collection_id
            WHERE  s.name=@SchemaCollectionName  
        ";
    }
    public static partial class SqlQueryConstant
    {
        // Query to fetch all Full-Text Catalogs with descriptions
        public const string GetAllFullTextCatalogs =
            @"
    SELECT 
     ftc.name AS [Name],
     s.name AS SchemaName,
     ISNULL(ep.value, '') AS [Description]
 FROM sys.fulltext_catalogs ftc
 LEFT JOIN sys.extended_properties ep 
     ON ftc.fulltext_catalog_id = ep.major_id
 JOIN sys.schemas s 
     ON ftc.principal_id = s.schema_id;
";

        // Query to fetch tables associated with Full-Text Catalogs
        public const string GetFullTextCatalogTables =
            @"
    SELECT 
        ftc.name AS FullTextCatalog,
        s.name AS SchemaName,
        t.name AS TableName
    FROM sys.fulltext_catalogs ftc
    JOIN sys.fulltext_indexes fti 
        ON ftc.fulltext_catalog_id = fti.fulltext_catalog_id
    JOIN sys.tables t 
        ON fti.object_id = t.object_id
    JOIN sys.schemas s 
        ON t.schema_id = s.schema_id;
";

        // Query to fetch Full-Text Indexed Columns
        public const string GetFullTextIndexedColumns =
            @"
    SELECT 
        ftc.name AS FullTextCatalog,
        s.name AS SchemaName,
        t.name AS TableName,
        c.name AS ColumnName,
        ftc.fulltext_catalog_id
    FROM sys.fulltext_catalogs ftc
    JOIN sys.fulltext_indexes fti 
        ON ftc.fulltext_catalog_id = fti.fulltext_catalog_id
    JOIN sys.tables t 
        ON fti.object_id = t.object_id
    JOIN sys.schemas s 
        ON t.schema_id = s.schema_id
    JOIN sys.fulltext_index_columns ftic 
        ON fti.object_id = ftic.object_id
    JOIN sys.columns c 
        ON ftic.column_id = c.column_id AND c.object_id = t.object_id;
";

        // Query to fetch Full-Text Catalog properties
        public const string GetFullTextProperties =
            @"
           SELECT 
             ftc.name AS Name,
             s.name AS SchemaName,
             dp.name AS [Owner],
             ftc.is_default AS IsDefault,  
             ftc.is_accent_sensitivity_on AS IsAccentSensitive, 
             ISNULL(ep.value, '') AS [Description]
         FROM sys.fulltext_catalogs ftc
         LEFT JOIN sys.database_principals dp 
             ON ftc.principal_id = dp.principal_id  
         LEFT JOIN sys.extended_properties ep 
             ON ftc.fulltext_catalog_id = ep.major_id  
         JOIN sys.schemas s 
             ON ftc.principal_id = s.schema_id
    WHERE ftc.name = @FullTextCatalog;
";

        // Query to generate Full-Text Catalog CREATE Script
        public const string GetFullTextCatalogCreateScript =
            @"
     SELECT 
    'CREATE FULLTEXT CATALOG [' + ftc.name + ']'
    + ' WITH ACCENT_SENSITIVITY = ' + CASE WHEN ftc.is_accent_sensitivity_on = 1 THEN 'ON' ELSE 'OFF' END
    + CASE WHEN ftc.is_default = 1 THEN CHAR(13) + 'AS DEFAULT' ELSE '' END
    + CHAR(13) + 'AUTHORIZATION [' + dp.name + ']'
    + CHAR(13) + 'GO' AS FullTextCatalogCreateScript
	,ftc.name
FROM sys.fulltext_catalogs ftc
JOIN sys.database_principals dp 
    ON ftc.principal_id = dp.principal_id
WHERE ftc.name = @FullTextCatalog;
";

        // Query to generate Full-Text Index CREATE Script
        public const string GetFullTextIndexCreateScript =
            @"
    SELECT 
        'CREATE FULLTEXT INDEX ON [' + s.name + '].[' + t.name + ']'
        + ' KEY INDEX [' + i.name + ']'
        + ' ON [' + ftc.name + ']'
        + CHAR(13) + 'GO' AS FullTextIndexCreateScript
    FROM sys.fulltext_indexes fti
    JOIN sys.tables t 
        ON fti.object_id = t.object_id
    JOIN sys.schemas s 
        ON t.schema_id = s.schema_id
    JOIN sys.indexes i 
        ON fti.unique_index_id = i.index_id AND fti.object_id = i.object_id
    JOIN sys.fulltext_catalogs ftc 
        ON fti.fulltext_catalog_id = ftc.fulltext_catalog_id
WHERE ftc.name = @FullTextCatalog;
";
    }
    public static partial class SqlQueryConstant
    {
        public const string GetAllSchemaMetadata =
            @"
                SELECT 
	                s.name AS SchemaName, 
	                ISNULL( p.value,'') AS [Description] 
           
                FROM sys.schemas s
                LEFT JOIN sys.extended_properties p 
                ON s.schema_id = p.major_id 
                AND p.name = 'MS_Description'
                JOIN sys.database_principals u 
                ON s.principal_id = u.principal_id";

        public const string GetSchemaDescription =
            @"
        SELECT 
            s.name AS SchemaName, 
            p.value AS Description
        FROM sys.schemas s
        LEFT JOIN sys.extended_properties p 
            ON s.schema_id = p.major_id 
            AND p.name = 'MS_Description'
        WHERE s.name = @SchemaName;";

        public const string GetSchemaOwner =
            @"
        SELECT 
            s.name AS SchemaName, 
            u.name AS Owner
        FROM sys.schemas s
        JOIN sys.database_principals u 
            ON s.principal_id = u.principal_id
        WHERE s.name = @SchemaName;";

        public const string GetObjectsUsedBySchema =
            @"
        SELECT 
            s.name AS SchemaName, 
            o.name AS ObjectName, 
            o.type_desc AS ObjectType
        FROM sys.objects o
        JOIN sys.schemas s 
            ON o.schema_id = s.schema_id
        WHERE s.name = @SchemaName
        ORDER BY o.name;";

        public const string GenerateSchemaScript =
            @"
        SELECT 
            'CREATE SCHEMA [' + s.name + '] AUTHORIZATION [' + u.name + '];' AS SchemaScript
        FROM sys.schemas s
        JOIN sys.database_principals u 
            ON s.principal_id = u.principal_id
        WHERE s.name = @SchemaName;";
    }
}

