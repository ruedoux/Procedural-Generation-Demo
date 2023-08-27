extends GutTest

const Tile: CSharpScript = preload("res://Scripts/CS/Generation/Tile.cs")


func test_constructor_properlyAssignsFields() -> void:
	# Given
	var TileId := randi_range(-1200, 1200)
	var Layer := randi_range(-1200, 1200)

	# When
	var tile = Tile.new(TileId, Layer)

	# Then
	assert_eq(TileId, tile.TileId)
	assert_eq(Layer, tile.Layer)


func test_FromJsonString_properlyMaps() -> void:
	# Given
	var TileId := randi_range(-1200, 1200)
	var Layer := randi_range(-1200, 1200)

	# When
	var tile = Tile.new(TileId, Layer)
	var jsonString: String = tile.ToString()
	var tileDeserialized = Tile.new().FromJsonString(jsonString)

	# Then
	assert_eq(tile.TileId, tileDeserialized.TileId)
	assert_eq(tile.Layer, tileDeserialized.Layer)
