CREATE PROCEDURE [seed].[SP_Seed_Categories]
AS
BEGIN
    SET IDENTITY_INSERT [dbo].[Categories] ON

    MERGE INTO [dbo].[Categories] AS Target
    USING (VALUES
        (1, 'Root', 0),

        (2, 'Course', 1),
        (3, 'Cuisine', 1),
        (4, 'Season', 1),
        (5, 'Diet', 1),

        (100, 'Breakfast', 2),          -- Course - Breakfast
        (101, 'Starter', 2),            -- Course - Starter
        (102, 'Main dish', 2),          -- Course - Main dish
        (103, 'Side dish', 2),          -- Course - SideDish
        (104, 'Dessert', 2),            -- Course - Dessert
        (105, 'Cake', 2),               -- Course - Cake
        (106, 'Drink', 2),              -- Course - Drink

        (200, 'Asian', 3),              -- Cusine - Asian
        (201, 'French', 3),             -- Cusine - French
        (202, 'Italian', 3),            -- Cusine - Italian
        (203, 'Mediterranean', 3),      -- Cusine - Mediterranean
        (204, 'Mexican', 3),            -- Cusine - Mexican

        (300, 'Spring', 4),             -- Season - Spring
        (301, 'Summer', 4),             -- Season - Summer
        (302, 'Autumn', 4),             -- Season - Autumn
        (303, 'Winter', 4),             -- Season - Winter

        (400, 'Vegetarian', 5),         -- Diet - Vegetarian
        (401, 'Vegan', 5),              -- Diet - Vegan
        (402, 'Gluten-free', 5),        -- Diet - Glueten-free
        (403, 'Lactose-free', 5)        -- Diet - Lactose-free
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
