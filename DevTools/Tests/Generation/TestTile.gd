extends GutTest

const Tile: CSharpScript = preload("res://Scripts/CS/Generation/Tiles/Tile.cs")


func test_constructor_createsValidObject() -> void:
	# Given
	var tileId := randi_range(-1200, 1200)
	var layer := randi_range(-1200, 1200)

	# When
	var tile = Tile.new(tileId, layer)

	# Then
	assert_eq(tileId, tile.tileId)
	assert_eq(layer, tile.layer)


func test_FromJsonString_properlyMaps() -> void:
	# Given
	var TileId := randi_range(-1200, 1200)
	var Layer := randi_range(-1200, 1200)

	# When
	var tile = Tile.new(TileId, Layer)
	var jsonString: String = tile.ToString()
	var tileDeserialized = Tile.new().FromJsonString(jsonString)

	# Then
	assert_eq(tile.tileId, tileDeserialized.tileId)
	assert_eq(tile.layer, tileDeserialized.layer)
