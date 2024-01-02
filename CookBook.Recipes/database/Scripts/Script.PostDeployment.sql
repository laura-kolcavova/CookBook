/*
Post-Deployment Script Template
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.
 Use SQLCMD syntax to include a file in the post-deployment script.
 Example:      :r .\myfile.sql
 Use SQLCMD syntax to reference a variable in the post-deployment script.
 Example:      :setvar TableName MyTable
               SELECT * FROM [$(TableName)]
--------------------------------------------------------------------------------------
*/

/*
--------------------------------------------------------------------------------------
Post-Deployment Script - START
--------------------------------------------------------------------------------------
*/

    /*
    --------------------------------------------------------
    8.Seed - START
    --------------------------------------------------------
    */
      PRINT '		8.SeedAndPatch\Seed.sql - START'
      GO

      :r ../SeedAndPatch/Seed.sql
      GO

      PRINT '		8.SeedAndPatch\Seed.sql - END'
      GO
    /*
    --------------------------------------------------------
    8.Seed - END
    --------------------------------------------------------
    */

/*
--------------------------------------------------------------------------------------
Post-Deployment Script - END
--------------------------------------------------------------------------------------
*/