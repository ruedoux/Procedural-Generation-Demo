extends CanvasLayer

const TileScene = preload("res://Scenes/MapRoot/TileUI/TileUI.tscn")

@onready
var noiseSeed: LineEdit = $C/H/OptionPanel/B/P/S/V/NoiseContainer/Container/Noise/V/Seed/LineEdit

@onready
var TileContainer: VBoxContainer = $C/H/OptionPanel/B/P/S/V/TilesContainer/Container/Tiles/V/TileContainer


func _ready() -> void:
	_random_seed_button_pressed()

	add_tile_instance(0, Color.SKY_BLUE)
	add_tile_instance(0.1, Color.YELLOW)
	add_tile_instance(0.6, Color.LIME_GREEN)
	add_tile_instance(0.8, Color.FOREST_GREEN)
	add_tile_instance(0.9, Color.LIGHT_GRAY)
	add_tile_instance(1, Color.WHITE)


func _random_seed_button_pressed() -> void:
	noiseSeed.text = str(randi() % 2147483647)


func add_tile_instance(value: float, color: Color) -> void:
	var tileInstance := TileScene.instantiate()
	tileInstance.get_node("Value/LineEdit").text = str(value)
	tileInstance.get_node("PickColor").color = color
	TileContainer.add_child(tileInstance)
