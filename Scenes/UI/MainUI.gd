extends CanvasLayer

const TileScene = preload("res://Scenes/MapRoot/TileUI/TileUI.tscn")

@onready
var noiseSeed: LineEdit = $C/H/OptionPanel/B/P/SettingsUI/V/NoiseContainer/Container/Noise/V/General/Seed/LineEdit
@onready
var TileContainer: VBoxContainer = $C/H/OptionPanel/B/P/SettingsUI/V/TilesContainer/Container/Tiles/V/TileContainer


func _ready() -> void:
	noiseSeed.text = str(randi() % 2147483647)

	add_tile_instance(0, Color.SKY_BLUE)
	add_tile_instance(0.1, Color.YELLOW)
	add_tile_instance(0.6, Color.LIME_GREEN)
	add_tile_instance(0.8, Color.FOREST_GREEN)
	add_tile_instance(0.9, Color.LIGHT_GRAY)
	add_tile_instance(1, Color.WHITE)


func add_tile_instance(value: float, color: Color) -> void:
	var tileInstance := TileScene.instantiate()
	tileInstance.get_node("Value/LineEdit").text = str(value)
	tileInstance.get_node("PickColor").color = color
	TileContainer.add_child(tileInstance)
