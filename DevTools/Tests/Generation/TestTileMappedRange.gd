extends GutTest

const TileMappedRange: CSharpScript = preload(
	"res://Scripts/CS/Generation/Tiles/TileMappedRange.cs"
)
const TileRange: CSharpScript = preload("res://Scripts/CS/Generation/Tiles/TileRange.cs")
const Tile: CSharpScript = preload("res://Scripts/CS/Generation/Tiles/Tile.cs")

const CsTest: CSharpScript = preload("res://DevTools/Tests/Generation/CsTest.cs")


func test_constructor_createsValidObject() -> void:
	var csTest = CsTest.new([1, 2, 3])
	print(csTest.arr)

	return
	# Given
	const rangeSize = 100
	var tile = Tile.new(1, 1)
	var tileRange = TileRange.new(-1, 1, tile)
	var tilesRange: Array[TileRange] = [tileRange]

	# When
	var tileMappedRange = TileMappedRange.new(tilesRange, Tile.new(), rangeSize)

	# Then
	print(tileMappedRange.GetRange())
