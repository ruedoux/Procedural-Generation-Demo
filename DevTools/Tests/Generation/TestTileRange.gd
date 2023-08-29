extends GutTest

const TileRange: CSharpScript = preload("res://Scripts/CS/Generation/Tiles/TileRange.cs")
const Tile: CSharpScript = preload("res://Scripts/CS/Generation/Tiles/Tile.cs")


func test_constructor_shouldCreateValidObject() -> void:
	# Given
	var noiseFrom := randf() - 1
	var noiseTo := randf()
	var tile = Tile.new()

	# When
	var tileRange = TileRange.new(noiseFrom, noiseTo, tile)

	# Then
	assert_eq(tileRange.noiseFrom, noiseFrom)
	assert_eq(tileRange.noiseTo, noiseTo)
	assert_eq(tileRange.tile.ToString(), tile.ToString())
