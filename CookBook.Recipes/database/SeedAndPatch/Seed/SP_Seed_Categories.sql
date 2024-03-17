CREATE PROCEDURE [seed].[SP_Seed_Categories]
AS
BEGIN
    SET IDENTITY_INSERT [dbo].[Categories] ON

    MERGE INTO [dbo].[Categories] AS Target
    USING (VALUES
        (1, 'Course', 0),
        (2, 'Cuisine', 0),
        (3, 'Season', 0),
        (4, 'Diet', 0),

        (5, 'Breakfast', 1),        -- Course - Breakfast
        (6, 'Starter', 1),          -- Course - Starter
        (7, 'Main dish', 1),        -- Course - Main dish
        (8, 'Side dish', 1),        -- Course - SideDish
        (9, 'Dessert', 1),          -- Course - Dessert
        (10, 'Cake', 1),            -- Course - Cake

        (11, 'Asian', 2),           -- Cusine - Asian
        (12, 'French', 2),          -- Cusine - French
        (13, 'Italian', 2),         -- Cusine - Italian
        (14, 'Mediterranean', 2),   -- Cusine - Mediterranean
        (15, 'Mexican', 2),         -- Cusine - Mexican

        (16, 'Spring', 3),          -- Season - Spring
        (17, 'Summer', 3),          -- Season - Summer
        (18, 'Autumn', 3),          -- Season - Autumn
        (19, 'Winter', 3),          -- Season - Winter

        (20, 'Vegetarian', 4),      -- Diet - Vegetarian
        (21, 'Vegan', 4),           -- Diet - Vegan
        (22, 'Gluten-free', 4),     -- Diet - Glueten-free
        (23, 'Lactose-free', 4)     -- Diet - Lactose-free
    )
    AS SOURCE (
        [Id],
        [Name],
        [ParentCategoryId]
    )
    ON (Target.[Id] = Source.[Id])
    WHEN NOT MATCHED BY TARGET THEN
        INSERT (
            [Id],
            [Name],
            [ParentCategoryId]
        )
        VALUES (
            Source.[Id],
            Source.[Name],
            Source.[ParentCategoryId]
        )
    WHEN NOT MATCHED BY SOURCE THEN DELETE;
END
