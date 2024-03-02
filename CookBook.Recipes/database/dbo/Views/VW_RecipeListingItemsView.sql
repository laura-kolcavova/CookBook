CREATE VIEW [dbo].[VW_RecipeListingItemsView] AS
SELECT
	r.[Id],
	r.[Title]
FROM [dbo].[Recipes] AS [r]
