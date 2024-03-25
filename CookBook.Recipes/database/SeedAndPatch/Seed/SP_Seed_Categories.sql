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

        (101, 'Breakfast', 1),          -- Course - Breakfast
        (102, 'Starter', 1),            -- Course - Starter
        (103, 'Main dish', 1),          -- Course - Main dish
        (104, 'Side dish', 1),          -- Course - SideDish
        (105, 'Dessert', 1),            -- Course - Dessert
        (106, 'Cake', 1),               -- Course - Cake
        (107, 'Drink', 1),              -- Course - Drink

        (201, 'Asian', 2),              -- Cusine - Asian
        (202, 'French', 2),             -- Cusine - French
        (203, 'Italian', 2),            -- Cusine - Italian
        (204, 'Mediterranean', 2),      -- Cusine - Mediterranean
        (205, 'Mexican', 2),            -- Cusine - Mexican

        (301, 'Spring', 3),             -- Season - Spring
        (302, 'Summer', 3),             -- Season - Summer
        (303, 'Autumn', 3),             -- Season - Autumn
        (304, 'Winter', 3),             -- Season - Winter

        (401, 'Vegetarian', 4),         -- Diet - Vegetarian
        (402, 'Vegan', 4),              -- Diet - Vegan
        (403, 'Gluten-free', 4),        -- Diet - Glueten-free
        (404, 'Lactose-free', 4)        -- Diet - Lactose-free
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
